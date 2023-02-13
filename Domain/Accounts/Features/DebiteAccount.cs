
using Abyster_Test_Project.Contract;
using Abyster_Test_Project.Domain.Account_Journals;
using Abyster_Test_Project.Domain.Accounts.Dto;
using Abyster_Test_Project.Domain.Categorys;
using Abyster_Test_Project.Service.Contract;
using AutoMapper;
using MediatR;

namespace Abyster_Test_Project.Domain.Accounts.Features;

public class DebiteAccount {

    public class DebiteAccountCommand : IRequest<bool>
    {
        public readonly DebiteAccountRequest debiteAccountRequest;
        public DebiteAccountCommand(DebiteAccountRequest debiteAccountRequest)
        {
            this.debiteAccountRequest = debiteAccountRequest;
        }
    }

    public class Handler : IRequestHandler<DebiteAccountCommand, bool>
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
        public async Task<bool> Handle(DebiteAccountCommand request, CancellationToken cancellationToken)
        {
            int userId = int.Parse(_currentUserService.UserId);
            AccountJournal journal = new AccountJournal();
            DebiteAccountRequest debiteAccountRequest = request.debiteAccountRequest;
            if(userId == null){
                throw new Exception("User informations is missing.");
            }
            var matchUser = _serviceManager.User.FindByCondition(user => user.Id == userId, false)
                                            .SingleOrDefault();
            if(matchUser == null){
                throw new Exception("User does not exists.");
            }
            
            var matchUserAccount = _serviceManager.Account.FindByCondition(account => account.user == matchUser, false)
                                            .SingleOrDefault();
            if(matchUserAccount == null){
                throw new Exception("Account does not exists.");
            }

            if(matchUserAccount.balance < debiteAccountRequest.amount){
                throw new Exception("Account amount is not sufficient.");
            }
            
            journal.account = matchUserAccount;
            journal.amount = debiteAccountRequest.amount;
            journal.category = getDebiteCategory(debiteAccountRequest.category);
            _serviceManager.AccountJournal.Create(journal);
            _serviceManager.Save();

            return true;
        }

        private Category getDebiteCategory(int category_id)
        {
            var mathcCategory = _serviceManager.Category.FindByCondition(category => category.Id == category_id, false).SingleOrDefault();
            if(mathcCategory == null){
                throw new Exception("Category does not exists.");
            }
            return mathcCategory;
        }
    }
}