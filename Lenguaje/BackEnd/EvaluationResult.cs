namespace AnálisisCodigo
{
    public class EvaluationResult
    {
        public EvaluationResult(IEnumerable<Diagnostics> diagnostics, object value)
        {
            Diagnostics = diagnostics.ToArray();
            Value = value;
        }

        public IReadOnlyList<Diagnostics> Diagnostics { get; }
        public object Value { get; }
    }
}