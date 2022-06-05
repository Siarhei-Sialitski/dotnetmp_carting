using System.ComponentModel.DataAnnotations;

namespace DotNetMP.Carting.Infrastructure.Data.Options;

public class LiteDbOptions
{
    public const string Path = "LiteDbOptions";

    [Required]
    public string ConnectionString { get; set; } = null!;
}
