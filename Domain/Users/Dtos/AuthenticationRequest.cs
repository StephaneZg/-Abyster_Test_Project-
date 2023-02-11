
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Abyster_Test_Project.SharedKernel;

namespace Abyster_Test_Project.Domain.Users.Dtos;

public class AuthenticationRequest {

    [Required]
    [EmailAddress]
    [DataType(DataType.EmailAddress)]
    public string emailAddress { get; set; }

    [Required]
    [MinLength(8)]
    public string password { get; set; }
}