using AutoMapper;
using IDonEnglist.Application.DTOs.CategorySkill;
using IDonEnglist.Application.DTOs.CategorySkill.Validator;
using IDonEnglist.Application.Exceptions;
using IDonEnglist.Application.Models.Identity;
using IDonEnglist.Application.Persistence.Contracts;
using IDonEnglist.Application.ViewModels.CategorySkill;
using IDonEnglist.Domain;
using IDonEnglist.Domain.Common;
using MediatR;

namespace IDonEnglist.Application.Features.CategorySkills.Commands
{
    public class CreateCategorySkill : IRequest<CategorySkillViewModel>
    {
        public CreateCategorySkillDTO CreateData { get; set; }
        public CurrentUser CurrentUser { get; set; }
    }

    public class CreateCategorySkillHandler : IRequestHandler<CreateCategorySkill, CategorySkillViewModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateCategorySkillHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<CategorySkillViewModel> Handle(CreateCategorySkill request, CancellationToken cancellationToken)
        {
            await ValidateRequest(request);

            var temp = _mapper.Map<CategorySkill>(request.CreateData);

            var newCategorySkill = await _unitOfWork.CategorySkillRepository.AddAsync(temp, request.CurrentUser);
            await _unitOfWork.Save();

            return _mapper.Map<CategorySkillViewModel>(newCategorySkill);
        }
        private async Task ValidateRequest(CreateCategorySkill request)
        {
            var validator = new CreateCategorySkillDTOValidator();
            var validationResult = await validator.ValidateAsync(request.CreateData);

            if (!validationResult.IsValid)
            {
                throw new ValidatorException(validationResult);
            }

            await CheckExistCategory(request.CreateData.CategoryId);
            await CheckExistSkill(request.CreateData.CategoryId, request.CreateData.Skill);
        }
        private async Task CheckExistCategory(int CategoryId)
        {
            var exist = await _unitOfWork.CategoryRepository.ExistsAsync(CategoryId);

            if (!exist)
            {
                throw new NotFoundException(nameof(Category), CategoryId);
            }
        }
        private async Task CheckExistSkill(int categoryId, Skill skill)
        {
            var categorySkill = await _unitOfWork.CategorySkillRepository
                .GetOneAsync(ct => ct.CategoryId == categoryId && ct.Skill == skill);

            if (categorySkill is not null)
            {
                throw new BadRequestException($"This category already has had {skill} skill");
            }
        }
    }
}
