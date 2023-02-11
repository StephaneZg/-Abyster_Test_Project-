
using System.ComponentModel.DataAnnotations;
using Abyster_Test_Project.SharedKernel;

namespace Abyster_Test_Project.Domain.Categorys;

 public class Category : Common{

   [Required]
   public string libelle {get; set;}
 }