namespace AnálisisCodigo
{
    public sealed class SourceText
    {
        private readonly string _text;

        public SourceText(string text)
        {
            _text = text;
            Lines = ParseLines(this, text);
        }

        public override string ToString()
        {
            return _text;
        }

        public string ToString(int start, int length)
        {
            return _text.Substring(start, length);
        }

        public string ToString(TextSpan span) => ToString(span.Start, span.Length);

        public int GetLineIndex(int position)
        {
            var lower = 0;
            var upper = Lines.Count - 1;
            while (lower <= upper)
            {
                var index = lower + (upper - lower) / 2;
                var start = Lines[index].start;
                if (position == start)
                {
                    return index;
                }

                if (start > position)
                {
                    upper = index - 1;
                }
                else
                {
                    lower = index + 1;
                }
            }
            return lower - 1;
        }

        private List<TextLine> ParseLines(SourceText sourceText, string text)
        {
            var result = new List<TextLine>();

            var position = 0;
            var linestart = 0;

            while (position < text.Length)
            {
                var linebreakwidth = GetLineBreakWidth(text, position);
                if (linebreakwidth == 0)
                {
                    position++;
                }
                else
                {
                    AddLine(result, sourceText, position, linestart, linebreakwidth);
                    position += linebreakwidth;
                    linestart = position;
                }
            }

            if (position > linestart)
            {
                AddLine(result, sourceText, position, linestart, 0);
            }

            return result;
        }

        private void AddLine(List<TextLine> result, SourceText sourceText, int position, int linestart, int lineBreakWidth)
        {
            var line_length = position - linestart;
            var lineLengthIncluidngLineBreak = line_length + lineBreakWidth;
            var line = new TextLine(sourceText, linestart, line_length, lineLengthIncluidngLineBreak);
            result.Add(line);
        }

        private int GetLineBreakWidth(string text, int i)
        {
            var c = text[i];
            var l = i+1 >= text.Length ? '\0' : text[i + 1];
            if (c == '\r' && l == '\n')
            {
                return 2;
            }
            if (c == '\r' || c == '\n')
            {
                return 1;
            }
            return 0;
        }
        public char this[int index] => _text[index];
        public int Length => _text.Length;
        public List<TextLine> Lines { get; }
        public static SourceText From(string text)
        {
            return new SourceText(text);
        }
    }
}