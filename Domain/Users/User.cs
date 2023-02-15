
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abyster_Test_Project.Domain.Roles;
using Abyster_Test_Project.SharedKernel;

namespace Abyster_Test_Project.Domain.Users;

public class User : Common{
    public User()
    {
        roles = new List<Role>();
    }

    [Required]
    public string firstName { get; set; }

    public string lastName { get; set; }

    [Required]
    public string emailAddress { get; set; }

    [Required]
    public string password { get; set; }

    [Column(TypeName = "Boolean")]
    public bool isActive {get; set;}

    public string? token {get; set;}

    public string? refreshToken {get; set;}

    public DateTime? refreshTokenExpireTime {get; set;}

    public IEnumerable<Role> roles { get; set;}

    [Column(TypeName = "Boolean")]
    public bool initialized {get; set;}

    
}