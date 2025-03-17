using AutoMapper;
using IDonEnglist.Application.DTOs.CategorySkill;
using IDonEnglist.Application.Exceptions;
using IDonEnglist.Application.Models.Identity;
using IDonEnglist.Application.Persistence.Contracts;
using IDonEnglist.Application.ViewModels.CategorySkill;
using IDonEnglist.Domain;
using IDonEnglist.Domain.Common;
using MediatR;

namespace IDonEnglist.Application.Features.CategorySkills.Commands
{
    public class UpdateCategorySkill : IRequest<IList<CategorySkillViewModel>>
    {
        public UpdateCategorySkillDTO UpdateData { get; set; }
        public CurrentUser CurrentUser { get; set; }
    }

    public class UpdateCategorySkillHandler : IRequestHandler<UpdateCategorySkill, IList<CategorySkillViewModel>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateCategorySkillHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IList<CategorySkillViewModel>> Handle(UpdateCategorySkill request, CancellationToken cancellationToken)
        {
            try
            {
                var existedSkills = await _unitOfWork.CategorySkillRepository
                    .GetAllListAsync(c => c.CategoryId == request.UpdateData.CategoryId, null, false, true);

                // Use a HashSet for fast lookups
                var existingSkillsSet = new HashSet<Skill>(existedSkills.Select(c => c.Skill));

                // Determine skills to delete and add
                var skillsToDelete = existedSkills.Where(item => !request.UpdateData.Skills.Contains(item.Skill)).ToList();
                var skillsToAdd = request.UpdateData.Skills.Where(skill => !existingSkillsSet.Contains(skill)).ToList();
                var skillsToRestore = existedSkills.Where(item => request.UpdateData.Skills.Contains(item.Skill) && item.DeletedDate != null).ToList();

                // Mark skills for deletion
                foreach (var item in skillsToDelete)
                {
                    await _unitOfWork.CategorySkillRepository.DeleteAsync(item.Id, request.CurrentUser);
                }

                // Add new skills
                foreach (var skill in skillsToAdd)
                {
                    var newCategorySkill = new CategorySkill
                    {
                        CategoryId = request.UpdateData.CategoryId,
                        Skill = skill,
                    };

                    await _unitOfWork.CategorySkillRepository.AddAsync(newCategorySkill, request.CurrentUser);
                }

                // Restore previously deleted skills
                foreach (var skill in skillsToRestore)
                {
                    skill.DeletedDate = null;
                    skill.DeletedBy = null;
                    await _unitOfWork.CategorySkillRepository.UpdateAsync(skill, request.CurrentUser);
                }

                // Save changes
                await _unitOfWork.Save();

                // Fetch updated category skills
                var categorySkills = await _unitOfWork.CategorySkillRepository
                    .GetAllListAsync(c => c.CategoryId == request.UpdateData.CategoryId);

                return _mapper.Map<IList<CategorySkillViewModel>>(categorySkills);
            }
            catch (Exception)
            {
                throw new BadRequestException("Update category skills fail");
            }
        }
    }
}
