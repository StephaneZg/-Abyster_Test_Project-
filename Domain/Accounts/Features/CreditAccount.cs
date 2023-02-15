
using Abyster_Test_Project.Contract;
using Abyster_Test_Project.Domain.Account_Journals;
using Abyster_Test_Project.Domain.Accounts.Dto;
using Abyster_Test_Project.Domain.Categorys;
using Abyster_Test_Project.Service.Contract;
using AutoMapper;
using MediatR;

namespace Abyster_Test_Project.Domain.Accounts.Features;

public class CreditAccount
{

    public class CreditAccountCommand : IRequest<bool>
    {
        public readonly CreditAccountRequest creditAccountRequest;
        public CreditAccountCommand(CreditAccountRequest creditAccountRequest)
        {
            this.creditAccountRequest = creditAccountRequest;
        }
    }

    public class Handler : IRequestHandler<CreditAccountCommand, bool>
    {
        private readonly IServiceManager _serviceManager;

        private readonly ICurrentUserService _currentUserService;

        private readonly IMapper _mapper;

        public Handler(IServiceManager serviceManager, ICurrentUserService currentUserService, IMapper mapper)
        {
            _serviceManager = serviceManager;
            _currentUserService = currentUserService;
            _mapper = mapper;
        }
        public async Task<bool> Handle(CreditAccountCommand request, CancellationToken cancellationToken)
        {
            int userId = int.Parse(_currentUserService.UserId);
            AccountJournal journal = new AccountJournal();
            CreditAccountRequest creditAccountRequest = request.creditAccountRequest;
            if (userId == null)
            {
                throw new Exception("User informations is missing.");
            }
            var matchUser = _serviceManager.User.FindByCondition(user => user.Id == userId, true)
                                            .SingleOrDefault();

            if (matchUser == null)
            {
                throw new Exception("User does not exists.");
            }
            var matchUserAccount = _serviceManager.Account.FindByCondition(account => account.user == matchUser, true)
                                            .SingleOrDefault();
            if (matchUserAccount == null)
            {
                throw new Exception("Account does not exists.");
            }

            journal.amount = creditAccountRequest.amount;
            journal.account = matchUserAccount;
            journal.user = matchUser;
            journal.category = getCreditCategory(creditAccountRequest.category);
            _serviceManager.AccountJournal.Create(journal);
            _serviceManager.Save();

            return true;
        }

        private Category getCreditCategory(int category_id)
        {
            var mathcCategory = _serviceManager.Category.FindByCondition(category => category.Id == category_id, true).SingleOrDefault();
            if (mathcCategory == null)
            {
                throw new Exception("Category does not exists.");
            }
            return mathcCategory;
        }
    }
}