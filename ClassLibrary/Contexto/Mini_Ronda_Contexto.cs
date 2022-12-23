namespace Poker;
public class Mini_Ronda_Contexto
{
    public Mini_Ronda_Contexto(int cant_cartas, IRepartidor? repartidor)
    {
        Cant_Cartas = cant_cartas;
        _Repartidor = repartidor;
    }
    public Mini_Ronda_Contexto(int cant_cartas) : this(cant_cartas, null){}
    public int Cant_Cartas { get; }
    private IRepartidor? _Repartidor;
    public IRepartidor Repartidor
    {
        get
        {
            if (_Repartidor == null)
            {
                return new RandomRepartidor();
            }
            return _Repartidor;
        }
        internal set
        {
            _Repartidor = value;
        }
    }
}
