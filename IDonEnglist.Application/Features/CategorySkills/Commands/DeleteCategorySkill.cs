using IDonEnglist.Application.Exceptions;
using IDonEnglist.Application.Models.Identity;
using IDonEnglist.Application.Persistence.Contracts;
using IDonEnglist.Domain;
using MediatR;

namespace IDonEnglist.Application.Features.CategorySkills.Commands
{
    public class DeleteCategorySkill : IRequest<int>
    {
        public int Id { get; set; }
        public CurrentUser CurrentUser { get; set; }
    }
    public class DeleteCategorySkillHandler : IRequestHandler<DeleteCategorySkill, int>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteCategorySkillHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<int> Handle(DeleteCategorySkill request, CancellationToken cancellationToken)
        {
            var exist = await _unitOfWork.CategorySkillRepository.ExistsAsync(request.Id);

            if (!exist)
            {
                throw new NotFoundException(nameof(CategorySkill), request.Id);
            }

            await _unitOfWork.CategorySkillRepository.DeleteAsync(request.Id, request.CurrentUser);
            await _unitOfWork.Save();

            return request.Id;
        }
    }
}
