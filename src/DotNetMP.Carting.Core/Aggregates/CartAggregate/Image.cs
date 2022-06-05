namespace DotNetMP.Carting.Core.Aggregates.CartAggregate;

public class Image
{
    public string Url { get; private set; }
    public string AltText { get; private set; }

    public Image(string url, string altText)
    {
        Url = url;
        AltText = altText;
    }
}
