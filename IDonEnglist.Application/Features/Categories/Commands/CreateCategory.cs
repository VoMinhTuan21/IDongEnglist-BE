using AutoMapper;
using IDonEnglist.Application.DTOs.Category;
using IDonEnglist.Application.DTOs.Category.Validators;
using IDonEnglist.Application.Exceptions;
using IDonEnglist.Application.Models.Identity;
using IDonEnglist.Application.Persistence.Contracts;
using IDonEnglist.Application.Utils;
using IDonEnglist.Domain;
using MediatR;

namespace IDonEnglist.Application.Features.Categories.Commands
{
    public class CreateCategory : IRequest<CategoryDTO>
    {
        public CreateCategoryDTO CreateData { get; set; }
        public CurrentUser CurrentUser { get; set; }
    }

    public class CreateCategoryHandler : IRequestHandler<CreateCategory, CategoryDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateCategoryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<CategoryDTO> Handle(CreateCategory request, CancellationToken cancellationToken)
        {

            await ValidateRequest(request);

            SetDefaultCodeIfEmpty(request);

            await CheckForDuplicateNameOrCode(request);

            var temp = _mapper.Map<Category>(request.CreateData);
            temp.CreatedBy = request.CurrentUser.Id;

            var category = await _unitOfWork.CategoryRepository.AddAsync(_mapper.Map<Category>(request.CreateData));
            await _unitOfWork.Save();

            return _mapper.Map<CategoryDTO>(category);
        }
        private async Task ValidateRequest(CreateCategory request)
        {
            var validator = new CreateCategoryDTOValidator(_unitOfWork.CategoryRepository);
            var validationResult = await validator.ValidateAsync(request.CreateData);

            if (!validationResult.IsValid)
            {
                throw new ValidatorException(validationResult);
            }
        }

        private void SetDefaultCodeIfEmpty(CreateCategory request)
        {
            if (string.IsNullOrEmpty(request.CreateData.Code))
            {
                request.CreateData.Code = SlugGenerator.GenerateSlug(request.CreateData.Name);
            }
        }

        private async Task CheckForDuplicateNameOrCode(CreateCategory request)
        {
            var existingCategory = await _unitOfWork.CategoryRepository.GetOneAsync(
                c => c.Name == request.CreateData.Name || c.Code == request.CreateData.Code);

            if (existingCategory != null)
            {
                throw new BadRequestException("Name or Code has been used.");
            }
        }
    }
}
