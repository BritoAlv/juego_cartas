namespace Poker;
public interface IColector
{
    List<string> get_efectos { get; }
    void add_efecto(string efecto);
    string remove_efecto();
}
public class JsonColector : IColector
{
    public List<string> get_efectos{ get; }
    private string pathID;
    public JsonColector(string id)
    {
        string path = Directory.GetCurrentDirectory();
        path = Path.Join(path, "..", "Effects");
        pathID = Path.Join(path, id + ".json");
        //Si no existe el archivo lo crea
        if (File.Exists(pathID))
        {
            get_efectos = File.ReadAllLines(pathID).ToList();
        }
        else
        {
            get_efectos = new List<string>();
            File.Create(pathID);
        }
    }
    public void add_efecto(string efecto)
    {
        get_efectos.Add(efecto);
        File.WriteAllLines(pathID, get_efectos);
    }
    public string remove_efecto()
    {
        string effect = get_efectos[get_efectos.Count - 1];
        get_efectos.Remove(effect);
        File.WriteAllLines(pathID, get_efectos);
        return effect;
    }
}
