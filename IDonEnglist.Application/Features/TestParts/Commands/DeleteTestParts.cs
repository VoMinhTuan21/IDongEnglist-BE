using AutoMapper;
using IDonEnglist.Application.Exceptions;
using IDonEnglist.Application.Models.Identity;
using IDonEnglist.Application.Persistence.Contracts;
using IDonEnglist.Domain;
using MediatR;

namespace IDonEnglist.Application.Features.TestParts.Commands
{
    public class DeleteTestParts : IRequest<List<int>>
    {
        public CurrentUser CurrentUser { get; set; }
        public List<int> DeleteData { get; set; }
    }

    public class DeleteTestPartsHandler : IRequestHandler<DeleteTestParts, List<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public DeleteTestPartsHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<List<int>> Handle(DeleteTestParts request, CancellationToken cancellationToken)
        {
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                var oldTestParts = await _unitOfWork.TestPartRepository.GetAllListAsync(p => request.DeleteData.Contains(p.Id));
                if (oldTestParts.Count() != request.DeleteData.Count())
                {
                    foreach (var id in request.DeleteData)
                    {
                        var testPart = oldTestParts.Find(x => x.Id == id)
                            ?? throw new NotFoundException(nameof(TestPart), id);
                    }
                }
                await _unitOfWork.TestPartRepository.DeleteRangeAsync(oldTestParts.Select(tp => tp.Id).ToList(), request.CurrentUser);
                await _unitOfWork.Save();
                await _unitOfWork.CommitTransactionAsync();
                return request.DeleteData;
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw ex;
            }
        }
    }
}
