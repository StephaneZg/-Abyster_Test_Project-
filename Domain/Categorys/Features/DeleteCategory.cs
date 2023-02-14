
using Abyster_Test_Project.Contract;
using Abyster_Test_Project.Domain.Categorys.Dto;
using AutoMapper;
using MediatR;

namespace Abyster_Test_Project.Domain.Categorys.Features;

public class DeletCategory {

    public class DeleteCategoryCommand : IRequest<bool>{

        public readonly int category_id;

        public DeleteCategoryCommand(int category_id)
        {
            this.category_id = category_id;
        }
    }

    public class Handler : IRequestHandler<DeleteCategoryCommand, bool>
    {
        private readonly IServiceManager _serviceManager;
        private readonly IMapper _mapper;

        public Handler(IServiceManager serviceManager, IMapper mapper)
        {
            _mapper = mapper;
            _serviceManager = serviceManager;
        }

        public async Task<bool> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            int category_id = request.category_id;

            var matchCategory = _serviceManager.Category.FindByCondition(category => category.Id == category_id, false)
            .SingleOrDefault();
            if(matchCategory == null){
                throw new Exception("Category does not exist");
            }

            _serviceManager.Category.Delete(matchCategory);
            await _serviceManager.Save();

            return true;
        }
    }
}