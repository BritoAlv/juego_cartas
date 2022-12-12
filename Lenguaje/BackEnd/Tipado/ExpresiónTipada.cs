namespace AnálisisCodigo.Tipado
{
    internal abstract class ExpresionTipada : NodoTipado
    {
        // refers to the data type it holds.
        public abstract Type Type { get; }
    }



}