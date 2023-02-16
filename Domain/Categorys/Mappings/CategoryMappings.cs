
using System.Net;
using Abyster_Test_Project.Domain.Account_Journals;
using Abyster_Test_Project.Domain.Account_Journals.Dto;
using Abyster_Test_Project.Domain.Categorys.Dto;
using Abyster_Test_Project.Domain.Users.Dtos;
using AutoMapper;

namespace Abyster_Test_Project.Domain.Categorys.Mappings;

public class CategoryMappings : Profile {


    public CategoryMappings(){
        CreateMap<Category, CategoryDto>().ReverseMap();
        CreateMap<Category, AddCategoryDto>().ReverseMap();

    }

}
