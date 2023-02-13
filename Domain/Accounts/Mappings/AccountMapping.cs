
using Abyster_Test_Project.Domain.Accounts.Dto;
using AutoMapper;

namespace Abyster_Test_Project.Domain.Accounts.Mappings;

public class AccountMapping : Profile {


    public AccountMapping(){
        CreateMap<Account, AccountDto>().ReverseMap();
    }

}
