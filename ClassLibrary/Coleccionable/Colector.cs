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
            return File.ReadAllLines(pathID).ToList();
        }
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
        File.AppendAllLines(pathID, new List<string> { efecto });
    }
    public string remove_efecto()
    {
        var lines = System.IO.File.ReadAllLines(pathID);
        string effect = get_efectos[get_efectos.Count - 1];
        System.IO.File.WriteAllLines(pathID, lines.Take(lines.Length - 1).ToArray());
        return effect;
    }
}
