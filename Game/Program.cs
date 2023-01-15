using Poker;
public class Program
{
    public static void Main()
    {
        // scorer contains rankings by default but a custom rank like dos dos can be added, defaults are poker ones.
        Scorer scorer = new Scorer();
        scorer.Add_Rank(new TwoTwos("dos dos")); // add custom rank.

        // Define PLayers
        Player A = new Human_Player("Barbaro", 120);
        Player B = new Human_Player("Miguel", 100);
        Player C = new Human_Player("PC", 500);


        // Define settings for the Mini_Rounds. Generate Random Cards by Default.
        // EL repartidor común es uno que le reparte las mismas cartas a los jugadores, como sucede en el poker normalmente,
        // mientras que por defecto en la mini ronda se usará un repartidor random que reparte las cartas aleatoriamente.
        List<Mini_Ronda_Contexto> mini_rondas_contexto = new List<Mini_Ronda_Contexto>()
        {
            new Mini_Ronda_Contexto(2),
            new Mini_Ronda_Contexto(3, new RepartidorComun()),
            new Mini_Ronda_Contexto(1, new RepartidorComun()),
            new Mini_Ronda_Contexto(1, new RepartidorComun()),
        };


        // Define settings for the rounds. 
        Ronda_Context ronda = new Ronda_Context(mini_rondas_contexto);

        Factory factory = new Factory();
        factory.AddPredefined
        (
            (x, parser) => x == "$intercambiardoscartas" ? new IntercambiarDosCartas(parser.Match(Tipo.ParéntesisAbierto), parser.Match(Tipo.Accion), parser.ParseArgument<Player>(), parser.ParseArgument<Player>(), parser.Match(Tipo.ParéntesisCerrado)) : null!
        );
        factory.AddPredefined
        (
            (x, parser) => x == "$añadircarta" ? new AñadirCarta(parser.Match(Tipo.ParéntesisAbierto), parser.Match(Tipo.Accion), parser.ParseArgument<Card>(), parser.ParseArgument<Player>(), parser.Match(Tipo.ParéntesisCerrado)) : null!
        );
        factory.AddPredefined
        (
            (x, parser) => x == "$robarcarta" ? new RobarCarta(parser.Match(Tipo.ParéntesisAbierto), parser.Match(Tipo.Accion), parser.ParseArgument<Card>(), parser.ParseArgument<Player>(), parser.Match(Tipo.ParéntesisCerrado)) : null!
        );
        factory.AddPredefined
        (
            (x, parser) => x == "$banearjugador" ? new BanearJugador(parser.Match(Tipo.ParéntesisAbierto), parser.Match(Tipo.Accion), parser.ParseArgument<Player>(), parser.Match(Tipo.ParéntesisCerrado)) : null!
        );
        factory.AddPredefined
        (
            (x, parser) => x == "$aumentardinero" ? new AumentarDinero(parser.Match(Tipo.ParéntesisAbierto), parser.Match(Tipo.Accion),  parser.Match(Tipo.Argumento), parser.ParseArgument<Player>(), parser.Match(Tipo.ParéntesisCerrado)) : null!
        );

        // Define settings for the game.
        Global_Contexto context = new Global_Contexto(ronda, factory, A, B, C);
        Manager manager = new Manager(scorer, context);
        manager.SimulateGame();
    }
}