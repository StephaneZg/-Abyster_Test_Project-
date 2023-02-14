
using Abyster_Test_Project.Contract;
using Abyster_Test_Project.Domain.Account_Journals.Dto;
using AutoMapper;
using MediatR;

namespace Abyster_Test_Project.Domain.Account_Journals.Features;

public class GetUserOperations
{

    public class QueryUserOperations : IRequest<IEnumerable<AccountJournalDto>>
    {

        public readonly int userId;

        public QueryUserOperations(int userId)
        {
            this.userId = userId;
        }

    }

    public class Handler : IRequestHandler<QueryUserOperations, IEnumerable<AccountJournalDto>>
    {
        private readonly IServiceManager _serviceManager;
        private readonly IMapper _mapper;

        public Handler(IServiceManager serviceManager, IMapper mapper)
        {
            _mapper = mapper;
            _serviceManager = serviceManager;
        }

        public async Task<IEnumerable<AccountJournalDto>> Handle(QueryUserOperations request, CancellationToken cancellationToken)
        {
            int userId = request.userId;
            var matchUser = _serviceManager.User.FindByCondition(user => user.Id == userId, false).SingleOrDefault();
            if (matchUser == null)
            {
                throw new Exception("User does not exists");
            }

            var userOperations = _serviceManager.AccountJournal.FindByCondition(accJ => accJ.account.Id == matchUser.Id, false);
            if (userOperations != null)
            {
                var userOperationsToreturn = _mapper.Map<IEnumerable<AccountJournalDto>>(userOperations);
                return userOperationsToreturn;
            }else return new List<AccountJournalDto>();

        }
    }
}