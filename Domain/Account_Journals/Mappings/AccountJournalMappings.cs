
using System.Net;
using Abyster_Test_Project.Domain.Account_Journals;
using Abyster_Test_Project.Domain.Account_Journals.Dto;
using Abyster_Test_Project.Domain.Users.Dtos;
using AutoMapper;

namespace Abyster_Test_Project.Domain.Users.Mappings;

public class AccountJournalMappings : Profile {


    public AccountJournalMappings(){
        CreateMap<AccountJournal, AccountJournalDto>()
                .ForMember(dto => dto.firstName, obj => obj.MapFrom(src => src.user.firstName))
                .ForMember(dto => dto.lastName, obj => obj.MapFrom(src => src.user.lastName))
                .ForMember(dto => dto.CategoryName, obj => obj.MapFrom(src => src.category.libelle))
                .ForMember(dto => dto.category_id, obj => obj.MapFrom(src => src.category.Id))
                .ForMember(dto => dto.emailAddress, obj => obj.MapFrom(src => src.user.emailAddress))
                .ForMember(dto => dto.user_id, obj => obj.MapFrom(src => src.user.Id))
                .ReverseMap();



    }

}
