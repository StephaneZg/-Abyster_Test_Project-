
using Abyster_Test_Project.SharedKernel;

namespace Abyster_Test_Project.Domain.Account_Journals.Dto;

public class AccountJournalDto : Common{

    public Double amount { get; set; }

    public string CategoryName { get; set; }

    public int category_id { get; set; }

    public int user_id { get; set; }

    public string firstName { get; set; }

    public string lastName { get; set; }

    public string emailAddress { get; set; }


}