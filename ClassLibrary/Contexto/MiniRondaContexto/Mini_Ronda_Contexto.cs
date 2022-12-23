namespace Poker;
public class Mini_Ronda_Contexto
{
    private IRepartidor? _repartidor;
    public Mini_Ronda_Contexto(int cant_cartas, IRepartidor? repartidor)
    {
        Cant_Cartas = cant_cartas;
        _repartidor = repartidor;
    }
    public Mini_Ronda_Contexto(int cant_cartas) : this(cant_cartas, null) { }
    public int Cant_Cartas { get; }
    public IRepartidor Repartidor
    {
        get
        {
            if (_repartidor is null)
            {
                _repartidor = new Repartidor();
            }
            return _repartidor;
        }
    }
}