using System.IO;

namespace Nexus.gen
{
    public class Printer
    {
        private string _indent;
        private int _indentSize;
        private bool _needIndent;
        private readonly TextWriter _stream;

        public Printer(TextWriter stream)
            : this(stream, 0)
        { }
        
        public Printer(TextWriter stream, int indent)
        {
            _indentSize = indent;
            _stream = stream;
            _needIndent = true;
            UpdateIndent();
        }

        public void Write(string input)
        {
            if (_needIndent)
            {
                _stream.Write(_indent);
            }
            _stream.Write(input);
            _needIndent = false;
        }

        public void WriteLine()
        {
            WriteLine(string.Empty);
        }

        public void WriteLine(string input)
        {
            if (_needIndent)
            {
                _stream.Write(_indent);
            }
            _stream.WriteLine(input);
            _needIndent = true;
        }

        public void Push()
        {
            ++_indentSize;
            UpdateIndent();
        }

        public void Pop()
        {
            --_indentSize;
            UpdateIndent();
        }

        private void UpdateIndent()
        {
            _indent = CreateIndent(_indentSize);
        }

        public static string CreateIndent(int indent)
        {
            return new string(' ', indent * 4);
        }
    }
}