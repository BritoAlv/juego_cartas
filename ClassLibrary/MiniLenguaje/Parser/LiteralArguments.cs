namespace Poker;
public abstract class LiteralArguments : Iprintable
{
    protected LiteralArguments(List<Token> tokens)
    {
        Descriptions = ParseDescription(tokens);
    }
    public abstract string valor { get; }
    public List<DescriptionArgument> Descriptions { get; private set; }
    public IEnumerable<Iprintable> GetChildrenIprintables()
    {
        foreach (var description in Descriptions)
        {
            yield return description;
        }
    }
    private List<DescriptionArgument> ParseDescription(List<Token> tokens)
    {
        List<DescriptionArgument> result = new List<DescriptionArgument>();
        int position = 0;
        Token Current()
        {
            if (position < tokens.Count)
            {
                return tokens[position];
            }
            return new SyntaxToken(Tipo.Wrong, "\0");
        }
        UnaryDescriptionArgument ParseUnaryDescription()
        {
            var objeto = Current();
            position = position + 1;
            var description = Current();
            position = position + 1;
            return new UnaryDescriptionArgument(objeto, description);
        }
        while (Current().Tipo != Tipo.Wrong)
        {
            var left = ParseUnaryDescription();
            if (Current().Text == "&&" || Current().Text == "||")
            {
                var operador = Current();
                position++;
                var right = ParseUnaryDescription();
                result.Add(new BinaryDescriptionArgument(left, operador, right));
                continue;
            }
            result.Add(left);
        }
        return result;
    }
}

