namespace AnálisisCodigo.Tipado
{
    internal sealed class BoundScope
    {
        private Dictionary<string, VariableSymbol> _variables = new Dictionary<string, VariableSymbol>();

        public BoundScope Parent { get; }

        public BoundScope(BoundScope parent)
        {
            Parent = parent;
        }

        public bool TryDeclare(VariableSymbol variable)
        {
            if (_variables.ContainsKey(variable.Name))
            {
                return false;
            }
            _variables.Add(variable.Name, variable);
            return true;
        }

        public bool TryLookUp(string name, out VariableSymbol variable)
        {
            if(_variables.TryGetValue(name, out variable))
            {
                return true;
            }
            if (Parent == null)
            {
                return false;
            }
            return Parent.TryLookUp(name, out variable);
        }

        public List<VariableSymbol> GetDeclaredVariables()
        {
            return _variables.Values.ToList();
        }
    }



}