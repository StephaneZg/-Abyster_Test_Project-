
using Abyster_Test_Project.SharedKernel;

namespace Abyster_Test_Project.Domain.Users.Dtos;

public class AuthenticationResponse : ErrorDetail {

    public string firstName { get; set; }

    public string lastName { get; set; }

    public string emailAddress { get; set; }

    public string password { get; set; }

    public bool isActive {get; set;}

     public string token {get; set;}

    public string refreshToken {get; set;}

}