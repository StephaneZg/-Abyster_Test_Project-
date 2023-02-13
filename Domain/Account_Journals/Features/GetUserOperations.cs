
using Abyster_Test_Project.Domain.Account_Journals.Dto;
using MediatR;

namespace Abyster_Test_Project.Domain.Account_Journals.Features;

public class GetUserOperations{

    public class QueryuserListOperations : IRequest<IEnumerable<AccountJournalDto>>{

    }

    public class Handler : IRequestHandler<QueryuserListOperations, IEnumerable<AccountJournalDto>>
    {
        public Task<IEnumerable<AccountJournalDto>> Handle(QueryuserListOperations request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}