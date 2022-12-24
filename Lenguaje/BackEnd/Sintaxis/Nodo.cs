namespace AnálisisCodigo.Sintaxis
{
    /// <summary>
    /// This are the nodes in our abstract syntax tree.
    /// </summary>
    public abstract class Nodo : Iprintable
    {
        public abstract Tipo tipo { get; }
        public abstract object value { get; }
        public virtual string valor => value.ToString();
        public IEnumerable<Iprintable> GetChildrenIprintables()
        {
            return Hijos();
        }
        public abstract IEnumerable<Nodo> Hijos();
    }
}