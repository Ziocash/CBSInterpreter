using CBSInterpreter.Models;
using CBSInterpreter.Models.Enums;
using CBSInterpreter.Models.Exceptions;
using System.Text;

namespace CBSInterpreter.Core
{
    public class CBSReader
    {
        private static readonly char EOF = unchecked((char)-1);
        private static readonly char Space = ' ';
        private static readonly char Comma = ',';
        private readonly List<char> _separatorChars = new() { Space, Comma };

        private readonly StreamReader _reader;
        private readonly List<CBSEntry> _entries;
        private readonly StringBuilder _stringBuilder;

        public string FileName { get; internal set; } = @"C:\Windows\Logs\CBS\CBS.log";

        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="CBSReaderException"></exception>
        public CBSReader()
        {
            try
            {
                _reader = new(FileName);
            }
            catch(Exception ex)
            {
                throw new CBSReaderException("Reader exception.", ex);
            }
            _entries = new();
            _stringBuilder = new();
        }

        public List<CBSEntry> GetEntries()
        {
            if (_entries.Count == 0)
                ScanFile();
            return _entries.Where(e => !string.IsNullOrEmpty(e.Value) || !string.IsNullOrWhiteSpace(e.Value)).ToList();
        }

        private void ScanFile()
        {
            while (PeekChar() != EOF)
            {
                CBSEntry entry = new();
                if (char.IsDigit(PeekChar()))
                {
                    ReadUntil(Comma);
                    entry.DateTime = DateTime.Parse(_stringBuilder.ToString());
                    SkipSeparatorChars();
                    _stringBuilder.Clear();
                    ReadUntil(Space);
                    ParseEntryType(entry);
                    SkipSeparatorChars();
                    _stringBuilder.Clear();
                    ReadUntil(Space);
                    ParseEntryContext(entry);
                    _stringBuilder.Clear();
                    ReadUntil('\n');
                    entry.Value = _stringBuilder.ToString();
                }
                else
                {
                    ReadUntil('\n');
                    entry.Value = _stringBuilder.ToString();
                }
                _stringBuilder.Clear();
                if (PeekChar() == '\n')
                {
                    _entries.Add(entry);
                    ReadChar();
                }
            }
            _stringBuilder.Clear();
        }

        private void ReadUntil(char endLine)
        {
            while (PeekChar() != endLine)
                _stringBuilder.Append(ReadChar());
        }

        private void SkipSeparatorChars()
        {
            while (_separatorChars.Contains(PeekChar()))
            {
                ReadChar();
            }
        }

        private void ParseEntryType(CBSEntry entry)
        {
            if (!Enum.TryParse(typeof(CBSEntryType), _stringBuilder.ToString(), true, out _))
                entry.Type = CBSEntryType.Unknown;
            else
                entry.Type = (CBSEntryType)Enum.Parse(typeof(CBSEntryType), _stringBuilder.ToString(), true);
        }

        private void ParseEntryContext(CBSEntry entry)
        {
            if (!Enum.TryParse(typeof(CBSEntryContext), _stringBuilder.ToString(), true, out _))
                entry.Context = CBSEntryContext.Empty;
            else
                entry.Context = (CBSEntryContext)Enum.Parse(typeof(CBSEntryContext), _stringBuilder.ToString(), true);
        }

        private char PeekChar()
        {
            return unchecked((char)_reader.Peek());
        }

        private char ReadChar()
        {
            return unchecked((char)_reader.Read());
        }
    }
}
