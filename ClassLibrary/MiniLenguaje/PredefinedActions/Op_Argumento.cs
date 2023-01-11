namespace Poker;
public static class Operacion
{
    public static string Replaace(string text, IGlobal_Contexto contexto)
    {
        List<string> words = get_all_words(text);
        foreach (var word in words)
        {
            if (contexto.variables.ContainsKey(word))
            {
                if (contexto.variables[word] is bool valor)
                {
                    text = text.Replace(word, valor == true ? "true" : "false");
                }
                if (contexto.variables[word] is int entero)
                {
                    text = text.Replace(word, entero.ToString());
                }
            }
            else
            {
                if (contexto.variables[word] is bool valor)
                {
                    text.Replace(word, "false");
                }
                if (contexto.variables[word] is int entero)
                {
                    text.Replace(word, "0");
                }
            }
        }
        return text;
    }
    public static List<string> get_all_words(string text)
    {
        List<string> words = new List<string>();
        for (int i = 0; i < text.Length; i++)
        {
            if (char.IsLetter(text[i]))
            {
                int start = i;
                string word = "";
                while (i < text.Length && char.IsLetter(text[i]))
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