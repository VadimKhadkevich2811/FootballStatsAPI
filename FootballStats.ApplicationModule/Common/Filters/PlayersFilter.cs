namespace FootballStats.ApplicationModule.Common.Filters;

public class PlayersFilter
{
    public string Name { get; set; }
    public string LastName { get; set; }

    public PlayersFilter()
    {
        Name = "";
        LastName = "";
    }

    public PlayersFilter(string name, string lastname)
    {
        Name = name;
        LastName = lastname;
    }
}