
using System.ComponentModel.DataAnnotations;
using Abyster_Test_Project.SharedKernel;

namespace Abyster_Test_Project.Domain.Users.Dtos;

public class RegistrationRequest {

    [Required(ErrorMessage = "firstname is required")]
    public string firstName {get; set;}

    [Required(ErrorMessage = "lastname is required")]
    public string lastName {get; set;}

    [Required(ErrorMessage = "email address is required")]
    [EmailAddress]
    [DataType(DataType.EmailAddress)]
    public string emailAddress { get; set; }

    [Required(ErrorMessage ="password is required")]
    [DataType(DataType.Password)]
    [MinLength(8, ErrorMessage = "password must be at least 8 characters")]
    public string password { get; set; }

    [Compare("password")]
    [DataType(DataType.Password)]
    [Required(ErrorMessage = "confirmed password is required")]
    public string confirmedPassord {get; set;}
}