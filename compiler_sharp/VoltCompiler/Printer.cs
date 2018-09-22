using System.IO;

namespace Volt
{
    public class Printer
    {
        private string _indent;
        private int _indentSize;
        private readonly TextWriter _stream;

        public Printer(TextWriter stream, int indent = 0)
        {
            _indentSize = indent;
            _stream = stream;
            UpdateIndent();
        }

        public void WriteLine()
        {
            _stream.WriteLine();
        }

        public void Write(string input)
        {
            _stream.Write(input);
        }

        public void WriteLine(string input)
        {
            _stream.Write(_indent);
            _stream.WriteLine(input);
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