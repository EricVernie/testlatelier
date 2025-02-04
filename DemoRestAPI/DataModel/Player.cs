
public class Rootobject
{
    public List<Player> players { get; set; }
}

public class Player
{
    public int id { get; set; }
    public string firstname { get; set; }
    public string lastname { get; set; }
    public string shortname { get; set; }
    public string sex { get; set; }
    public Country country { get; set; }
    public string picture { get; set; }
    public Data data { get; set; }
}

public class Country
{
    public string picture { get; set; }
    public string code { get; set; }
}

public class Data
{
    public int rank { get; set; }
    public int points { get; set; }
    public int weight { get; set; }
    public int height { get; set; }
    public int age { get; set; }
    public int[] last { get; set; }
}
