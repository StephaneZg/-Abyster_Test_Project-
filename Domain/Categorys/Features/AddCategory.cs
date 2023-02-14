
using Abyster_Test_Project.Contract;
using Abyster_Test_Project.Domain.Categorys.Dto;
using AutoMapper;
using MediatR;

namespace Abyster_Test_Project.Domain.Categorys.Features;

public class AddCategory
{


    public class AddCategoryCommand : IRequest<CategoryDto>
    {

        public readonly AddCategoryDto addCategoryDto;
        public AddCategoryCommand(AddCategoryDto addCategoryDto)
        {
            this.addCategoryDto = addCategoryDto;
        }
    }

    public class Handler : IRequestHandler<AddCategoryCommand, CategoryDto>
    {
        private readonly IServiceManager _serviceManager;
        private readonly IMapper _mapper;

        public Handler(IServiceManager serviceManager, IMapper mapper)
        {
            _mapper = mapper;
            _serviceManager = serviceManager;
        }

        public async Task<CategoryDto> Handle(AddCategoryCommand request, CancellationToken cancellationToken)
        {
            AddCategoryDto categoryDto = request.addCategoryDto;

            var matchCategory = _serviceManager.Category.FindByCondition(category => category.libelle == categoryDto.libelle, false)
            .SingleOrDefault();
            if (matchCategory != null)
            {
                throw new Exception("Category duplication detected");
            }

            Category categoryToSave = _mapper.Map<Category>(categoryDto);
            _serviceManager.Category.Create(categoryToSave);
            await _serviceManager.Save();

            return _mapper.Map<CategoryDto>(categoryToSave);
        }
    }
}