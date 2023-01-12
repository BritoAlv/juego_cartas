namespace Poker;
public class OperationInt : Return<int>
{
    public OperationInt(Token open_parenthesis, Token signature, Token argumento, Token closed_parenthesis) : base(open_parenthesis, signature, closed_parenthesis)
    {
        Argumento = argumento;
    }
    public Token Argumento { get; }
    public override IEnumerable<int> Evaluate(IGlobal_Contexto contexto)
    {
        string operation = Operacion.Replaace(Argumento.Text, contexto);
        return new List<int>{(int)Eval.Evaluador.Evaluator(operation)};
    }
    public override bool Evaluate_Top(IGlobal_Contexto contexto)
    {
        return Evaluate(contexto).Count() > 0;
    }
    public override IEnumerable<Iprintable> GetChildrenIprintables()
    {
        yield return Open_Parenthesis;
        yield return Argumento;
        yield return Closed_Parenthesis;
    }
    public string Replaace(string text, IGlobal_Contexto contexto)
    {
        List<string> words = get_all_words(text);
        foreach (var word in words)
        {
            if (contexto.variables.ContainsKey(word))
            {
                text.Replace(word, ((int)contexto.variables[word]).ToString());
            }
            else
            {
                text.Replace(word, "0");
            }
        }
        return text;
    }
    private List<string> get_all_words(string text)
    {
        List<string> words = new List<string>();
        for (int i = 0; i < text.Length; i++)
        {
            if (char.IsLetter(text[i]))
            {
                int start = i;
                string word = "";
                while (char.IsLetter(text[i]))
                {
                    word = word + text[i];
                    i++;
                }
                words.Add(word);
            }
        }
        return words;
    }
}