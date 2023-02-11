
using System.ComponentModel.DataAnnotations;

namespace Abyster_Test_Project.SharedKernel;
public abstract class Common {

    [Key]
    public int Id {get; set; }
    public DateTime createdAt {get; set; }

    public string createdBy {get; set; }

    public DateTime updatedAt {get; set; }

    public string updatedBy {get; set; }

    public void UpdateCreationProperties(DateTime createdAtTime, string createdByUser)
    {
        createdAt = createdAtTime;
        createdBy = createdByUser;
    }
    
    public void UpdateModifiedProperties(DateTime lastModifiedOn, string lastModifiedBy)
    {
        updatedAt = lastModifiedOn;
        updatedBy = lastModifiedBy;
    }
}