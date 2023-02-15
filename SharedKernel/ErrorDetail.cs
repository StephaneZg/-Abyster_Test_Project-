
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace Abyster_Test_Project.SharedKernel;

public class ErrorDetail
{
    [NotMapped]
    public int statusCode { get; set; }

    [NotMapped]
    public string message { get; set; }

    public override string ToString() => JsonSerializer.Serialize(this);
}