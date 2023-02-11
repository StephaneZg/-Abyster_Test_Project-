
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abyster_Test_Project.Domain.Categorys;
using Abyster_Test_Project.Domain.Users;
using Abyster_Test_Project.SharedKernel;

namespace Abyster_Test_Project.Domain.Accounts;

public class Account : Common{

    [Required]
    public long montant {get; set; }

    [ForeignKey(name: "category_id")]
    public Category category {get; set; }

    [ForeignKey(name: "user_id")]
    public User user {get; set; }
}