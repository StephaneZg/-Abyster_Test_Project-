using System.ComponentModel.DataAnnotations;
using Abyster_Test_Project.SharedKernel;

namespace Abyster_Test_Project.Domain.Categorys.Dto;

public class UpdateCategoryDto : Common{
    
    public int categoryid {get; set;}
    [Required]
    public string libelle {get; set;}
}