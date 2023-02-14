
using Abyster_Test_Project.Contract;
using Abyster_Test_Project.Domain.Account_Journals.Dto;
using AutoMapper;
using MediatR;

namespace Abyster_Test_Project.Domain.Account_Journals.Features;

public class GetAllUserOperations{

    public class QueryUserListOperations : IRequest<IEnumerable<AccountJournalDto>>{

    }

    public class Handler : IRequestHandler<QueryUserListOperations, IEnumerable<AccountJournalDto>>
    {
        private readonly IServiceManager _serviceManager;
        private readonly IMapper _mapper;

        public Handler(IServiceManager serviceManager, IMapper mapper)
        {
            _mapper = mapper;
            _serviceManager = serviceManager;
        }

        public async Task<IEnumerable<AccountJournalDto>> Handle(QueryUserListOperations request, CancellationToken cancellationToken)
        {
            var userOperations = _serviceManager.AccountJournal.FindAll(false);
            if (userOperations != null)
            {
                var userOperationsToreturn = _mapper.Map<IEnumerable<AccountJournalDto>>(userOperations);
                return userOperationsToreturn;
            }else return new List<AccountJournalDto>();
        }
    }
}