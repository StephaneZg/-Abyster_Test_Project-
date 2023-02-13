

using System.Linq.Expressions;
using Abyster_Test_Project.Contract;
using Abyster_Test_Project.Data;
using Abyster_Test_Project.Domain.Categorys;
using Abyster_Test_Project.SharedKernel;

namespace Abyster_Test_Project.Domain.Account_Journals.Services;

public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository {

    private DatabaseContext _context;
    public CategoryRepository(DatabaseContext context) : base(context)
    {
        _context = context;
    }

}