
using Abyster_Test_Project.Contract;
using Abyster_Test_Project.Domain.Categorys.Dto;
using AutoMapper;
using MediatR;

namespace Abyster_Test_Project.Domain.Categorys.Features;

public class GetAllCategories {

    public class QueryListCategory : IRequest<IEnumerable<CategoryDto>>{


        public QueryListCategory()
        {
        }
    }

    public class Handler : IRequestHandler<QueryListCategory, IEnumerable<CategoryDto>>
    {
        private readonly IServiceManager _serviceManager;
        private readonly IMapper _mapper;

        public Handler(IServiceManager serviceManager, IMapper mapper)
        {
            _mapper = mapper;
            _serviceManager = serviceManager;
        }

        public async Task<IEnumerable<CategoryDto>> Handle(QueryListCategory request, CancellationToken cancellationToken)
        {

            var matchCategories = _serviceManager.Category.FindAll(false);
            var matchCategoriesToReturn = _mapper.Map<IEnumerable<CategoryDto>>(matchCategories);
            return matchCategoriesToReturn;
        }
    }
}