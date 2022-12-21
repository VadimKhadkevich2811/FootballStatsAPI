namespace FootballStats.Application.Common.Wrappers.Links;

public abstract class LinkResourceBase
{
    public ICollection<Link> Links { get; set; } = new List<Link>();
}