using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using NLog;
using YamlDotNet.Serialization;
using Zio;
using Zio.FileSystems;

namespace Nexus
{
    public class Configuration
    {
        public string InputPath { get; private set; }
        public string OutputPath { get; private set; }
        public string ProjectConfigurationPath { get; private set; }
        public ProjectData ProjectData {get; private set; }
        private MountFileSystem _fileSystem = new MountFileSystem();
        private readonly Logger _logger;
        
        public Configuration(string inputPath, string outputPath)
        {
            InputPath = inputPath;
            OutputPath = outputPath;
            _logger = LogManager.GetCurrentClassLogger();
        }

        public void Read()
        {
            // check the inputPath
            if (!Directory.Exists(InputPath))
            {
                throw new DirectoryNotFoundException("The input path is not a valid directory");
            }

            // change the (probably) relative path to an absolute one
            InputPath = Path.GetFullPath(InputPath);

            // again.. change the (probably) relative path to an absolute one
            OutputPath = Path.GetFullPath(OutputPath);

            _logger.Info($"Source path: '{InputPath}'");
            _logger.Info($"Output path: '{OutputPath}'");

            // check the project configuration file
            foreach (var extension in new[] {"yaml", "yml"})
            {
                var path = Path.Join(InputPath, $"project.{extension}");

                if (File.Exists(path))
                {
                    ProjectConfigurationPath = path;
                    break;
                }
            }

            if (string.IsNullOrWhiteSpace(ProjectConfigurationPath))
            {
                throw new FileNotFoundException("The project configuration file does not exist");
            }

            _logger.Info($"Reading the project file '{ProjectConfigurationPath}'");

            var yamlDeserializer = new Deserializer();
            var configStream = File.OpenText(ProjectConfigurationPath);
            ProjectData = yamlDeserializer.Deserialize<ProjectData>(configStream);

            _fileSystem = new MountFileSystem(new SubFileSystem(new PhysicalFileSystem(), OsPathToUPath(InputPath)));

            foreach (var i in ProjectData.Includes)
            {
                i.Path = Path.GetFullPath(i.Path, InputPath);

                if (!Directory.Exists(i.Path))
                {
                    throw new DirectoryNotFoundException($"The include directory does not exist: '{i.Path}'");
                }

                _fileSystem.Mount(i.Mount, new SubFileSystem(new PhysicalFileSystem(), OsPathToUPath(i.Path)));
            }

            _logger.Info(ProjectData);
        }

        public IEnumerable<string> EnumerateSourceFiles(string path)
        {
            if (_fileSystem.DirectoryExists(path))
            {
                foreach (var i in _fileSystem.EnumerateFiles(path, "*.nx", SearchOption.TopDirectoryOnly))
                {
                    yield return i.ToString();
                }
            }
            
            if (_fileSystem.FileExists(path))
            {
                yield return path;
            }
        }

        public IEnumerable<string> EnumerateDirectories(string path)
        {
            if (_fileSystem.DirectoryExists(path))
            {
                foreach (var i in _fileSystem.EnumerateDirectories(path))
                {
                    yield return i.ToString();
                }
            }
        }

        public Stream OpenFile(string path)
        {
            return _fileSystem.OpenFile(path, FileMode.Open, FileAccess.Read, FileShare.Read);
        }

        public static UPath OsPathToUPath(string path)
        {
            // relative path
            if (!Path.IsPathFullyQualified(path))
            {
                return path;
            }

            var regex = new Regex(@"^\w:");
            var match = regex.Match(Path.GetFullPath(path));

            // Windows path
            if (match.Success)
            {
                var root = Path.GetPathRoot(path);
                var tmp = path.Substring(root.Length);
                return $"/mnt/{char.ToLowerInvariant(root.First())}/{tmp.Replace('\\', '/')}";
            }

            // Linux path
            return path;
        }
    }

    public class ProjectData
    {
        public string Name { get; set; } = string.Empty;
        public string Version { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public IList<string> Authors { get; set; } = new List<string>();
        public string Date { get; set; } = string.Empty;
        public DateTime DateTime => DateTime.Parse(Date);
        public IList<ProjectInclude> Includes { get; set; } = new List<ProjectInclude>();
        public string Main { get; set; } = string.Empty;

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine(new string('-', 20));
            sb.AppendLine($"Name: {Name}");
            
            if (Version != null)
            {
                sb.AppendLine($"Version: {Version}");
            }

            if (!string.IsNullOrWhiteSpace(Description))
            {
                sb.AppendLine($"Description: {Description}");
            }

            if (Authors != null && Authors.Any())
            {
                sb.AppendLine("Authors:");
                foreach (var i in Authors)
                {
                    sb.AppendLine($" - {i}");
                }
            }

            if (Date != null)
            {
                sb.AppendLine($"Date: {DateTime:F}");
            }

            if (Includes.Any())
            {
                sb.AppendLine("Includes:");
                foreach (var i in Includes)
                {
                    sb.AppendLine($" - '{i.Path}' => '{i.Mount}'");
                }
            }

            if (!string.IsNullOrWhiteSpace(Main))
            {
                sb.AppendLine($"Main: {Main}");
            }

            sb.AppendLine(new string('-', 20));
            return sb.ToString();
        }
    }

    public class ProjectInclude
    {
        public string Mount { get; set; }
        public string Path { get; set; }
    }
}