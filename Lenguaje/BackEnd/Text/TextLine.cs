namespace AnálisisCodigo
{
    public sealed class TextLine
    {
        public readonly int start;
        private readonly int length;
        private readonly int length_Incluing_Line_Break;
        public TextLine(SourceText text, int Start, int length, int length_incluing_line_break)
        {
            Text = text;
            this.length = length;
            start = Start;
            length_Incluing_Line_Break = length_incluing_line_break;
        }
        public TextSpan Span => new TextSpan(start, length);
        public int End => start + length;
        public TextSpan SpanIncludingLineBreak => new TextSpan(start, length_Incluing_Line_Break);
        public SourceText Text { get; }

        public override string ToString()
        {
            return Text.ToString(Span);
        }
    }
}