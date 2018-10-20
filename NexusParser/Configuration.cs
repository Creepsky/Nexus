﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        
        public Configuration(string inputPath, string outputPath)
        {
            InputPath = inputPath;
            OutputPath = outputPath;
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

            // check the outputPath
            if (!Directory.Exists(OutputPath))
            {
                throw new DirectoryNotFoundException("The output path is not a valid directory");
            }

            // again.. change the (probably) relative path to an absolute one
            OutputPath = Path.GetFullPath(OutputPath);

            Console.WriteLine($"Source path: '{InputPath}'");
            Console.WriteLine($"Output path: '{OutputPath}'");

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

            Console.WriteLine($"Reading the project file '{ProjectConfigurationPath}'");

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

            Console.WriteLine(ProjectData);
        }

        public IEnumerable<string> EnumeratePath(string path)
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

        private static UPath OsPathToUPath(string path)
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
        public IList<Include> Includes { get; set; } = new List<Include>();
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

    public class Include
    {
        public string Mount { get; set; }
        public string Path { get; set; }
    }
}