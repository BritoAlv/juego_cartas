namespace Poker;

/// <summary>
/// This is a IColector implementation
/// </summary>
public class Colector : IColector
{
    public List<string> get_efectos
    {
        get
        {
            return Process(File.ReadAllText(pathID));
        }
    }
    private List<string> Process(string file)
    {
        file = file.TrimEnd('\n');
        List<string> result = new List<string>();
        result = file.Split("\n)", StringSplitOptions.RemoveEmptyEntries).ToList();
        for (int i = 0; i < result.Count; i++)
        {
            result[i] = result[i] + "\n)";
        }
        return result;
    }
    private string pathID;
    public Colector(string id)
    {
        string path = Directory.GetCurrentDirectory();
        path = Path.Join(path, "..", "Effects");
        pathID = Path.Join(path, id + ".txt");
        //Si no existe el archivo lo crea
        if (!File.Exists(pathID))
        {
            File.Create(pathID);
        }
    }
    public void add_efecto(string efecto)
    {
        List<string> nueva = this.get_efectos;
        nueva.Add(efecto);
        string result = "";
        foreach (var efect in nueva)
        {
            result = result + efect;
        }
        File.WriteAllText(pathID, result);
    }
    public string remove_efecto()
    {
        List<string> nueva = this.get_efectos;
        string effect = nueva[nueva.Count - 1];
        nueva.RemoveAt(nueva.Count - 1);
        string result = "";
        foreach (var efect in nueva)
        {
            result = result + efect;
        }
        File.WriteAllText(pathID, result);
        return effect;
    }
}