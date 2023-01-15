namespace Poker;
/// <summary>
/// Only one place to declare predefined actions.
/// </summary>
public class Factory
{
    public Factory()
    {
        this.predefined_actions = new List<Func<string, Parser, object?>>();
        predefined_actions.Add
        (
            (x, parser) => x == "$holdhand" ? new Hand_Hold(parser.Match(Tipo.ParéntesisAbierto), parser.Match(Tipo.Accion), parser.ParseArgument<Hand>(), parser.ParseArgument<Player>(), parser.Match(Tipo.ParéntesisCerrado)) : null
        );
        predefined_actions.Add
        (
            (x, parser) => x == "$holdcard" ? new Card_Hold(parser.Match(Tipo.ParéntesisAbierto), parser.Match(Tipo.Accion), parser.ParseArgument<Card>(), parser.ParseArgument<Player>(), parser.Match(Tipo.ParéntesisCerrado)) : null
        );
        predefined_actions.Add
        (
            (x, parser) => x == "$getcardvalue" ? new Getcardvalue(parser.Match(Tipo.ParéntesisAbierto), parser.Match(Tipo.Accion), parser.ParseArgument<Card>(), parser.ParseArgument<Player>(), parser.Match(Tipo.ParéntesisCerrado)) : null
        );
        predefined_actions.Add
        (
            (x, parser) => x == "$operationbool" ? new OperationBool(parser.Match(Tipo.ParéntesisAbierto), parser.Match(Tipo.Accion), parser.Match(Tipo.Argumento), parser.Match(Tipo.ParéntesisCerrado)) : null
        );
        predefined_actions.Add
        (
            (x, parser) => x == "$operationint" ? new OperationInt(parser.Match(Tipo.ParéntesisAbierto), parser.Match(Tipo.Accion), parser.Match(Tipo.Argumento), parser.Match(Tipo.ParéntesisCerrado)) : null
        );
        predefined_actions.Add
        (
            (x, parser) => x == "$modificarvalorcarta" ? new ModificarValorCarta(parser.Match(Tipo.ParéntesisAbierto), parser.Match(Tipo.Accion), parser.Match(Tipo.Argumento), parser.ParseArgument<Card>(), parser.ParseArgument<Player>() ,  parser.Match(Tipo.ParéntesisCerrado)) : null
        );
        predefined_actions.Add
        (
            (x, parser) => x == "$exist" ? new Exist(parser.Match(Tipo.ParéntesisAbierto), parser.Match(Tipo.Accion), parser.Match(Tipo.Argumento) ,  parser.Match(Tipo.ParéntesisCerrado)) : null!
        );
        predefined_actions.Add
        (
            (x, parser) => x == "$finalround" ? new FinalRoundEffect(parser.Match(Tipo.ParéntesisAbierto), parser.Match(Tipo.Accion), parser.Match(Tipo.Argumento),  (IFirst)parser.ParseAction() ,  parser.Match(Tipo.ParéntesisCerrado)) : null!
        );
        predefined_actions.Add
        (
            (x, parser) => x == "$getdinero" ? new Getdinero(parser.Match(Tipo.ParéntesisAbierto), parser.Match(Tipo.Accion), parser.ParseArgument<Player>() ,  parser.Match(Tipo.ParéntesisCerrado)) : null!
        );
        predefined_actions.Add
        (
            (x, parser) => x == "$asignarbool" ? new Asignarbool(parser.Match(Tipo.ParéntesisAbierto), parser.Match(Tipo.Accion), parser.Match(Tipo.Argumento), (Return<bool>)parser.ParseAction(), parser.Match(Tipo.ParéntesisCerrado)) : null!
        );
        predefined_actions.Add
        (
            (x, parser) => x == "$asignarint" ? new AsignarInt(parser.Match(Tipo.ParéntesisAbierto), parser.Match(Tipo.Accion), parser.Match(Tipo.Argumento), (Return<int>)parser.ParseAction(), parser.Match(Tipo.ParéntesisCerrado)) : null!
        );
    }
    public List<Func<string, Parser, object?>> predefined_actions { get; private set; }
    internal object CreateAction(string text, Parser parser)
    {
        if (text == "$while" || text == "$if")
        {
            Token open = parser.Match(Tipo.ParéntesisAbierto);
            Token signature = text == "$while" ? parser.Match(Tipo.While) : parser.Match(Tipo.IF);
            Token open_question = parser.Match(Tipo.QuestionAbierta);
            Return<bool> condition;
            Return<bool> condition1 = (Return<bool>)this.CreateAction(parser.LookAhead(1).Text, parser);
            if (parser.LookAhead(0).Text == "&&" || parser.LookAhead(0).Text == "||")
            {
                Token op = parser.LookAhead(0).Text == "&&" ? parser.Match(Tipo.And) : parser.Match(Tipo.Or);
                Return<bool> condition2 = (Return<bool>)this.CreateAction(parser.LookAhead(1).Text, parser);
                condition = new BinaryAction(condition1, op, condition2);
            }
            else
            {
                condition = condition1;
            }
            Token closed_question = parser.Match(Tipo.QuestionCerrada);
            Token implies = parser.Match(Tipo.Implies);
            List<IFirst> action1 = new List<IFirst>();
            while (parser.LookAhead(0).Text == "(")
            {
                action1.Add((IFirst)this.CreateAction(parser.LookAhead(1).Text, parser));
            }
            if (text == "$while")
            {
                var closee = parser.Match(Tipo.ParéntesisCerrado);
                return new While_Expresion(open, signature, condition, implies, action1, closee);
            }
            else
            {
                Token not = parser.Match(Tipo.ThirdOption);
                List<IFirst> action2 = new List<IFirst>();
                while (parser.LookAhead(0).Text == "(")
                {
                    action2.Add((IFirst)this.CreateAction(parser.LookAhead(1).Text, parser));
                }
                Token closed = parser.Match(Tipo.ParéntesisCerrado);
                return new IF_Expresion(open, signature, condition, implies, action1, not, action2, closed);
            }
        }
        foreach (var func in predefined_actions)
        {
            var result = func(text, parser);
            if (result != null)
            {
                return result;
            }
        }
        throw new Exception("No se encontró la acción");
    }
    // calls to the factory should have Current = ) parser responsibility.
    public void AddPredefined(Func<string, Parser, object> func)
    {
        predefined_actions.Add(func);
    }
}