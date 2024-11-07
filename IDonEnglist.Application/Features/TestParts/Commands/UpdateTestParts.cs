using AutoMapper;
using IDonEnglist.Application.DTOs.TestPart;
using IDonEnglist.Application.Exceptions;
using IDonEnglist.Application.Models.Identity;
using IDonEnglist.Application.Persistence.Contracts;
using IDonEnglist.Application.Utils;
using IDonEnglist.Domain;
using MediatR;

namespace IDonEnglist.Application.Features.TestParts.Commands
{
    public class UpdateTestParts : IRequest<List<int>>
    {
        public List<UpdateTestPartDTO> UpdateData { get; set; }
        public CurrentUser CurrentUser { get; set; }
    }

    public class UpdateTestPartsHandler : IRequestHandler<UpdateTestParts, List<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateTestPartsHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<List<int>> Handle(UpdateTestParts request, CancellationToken cancellationToken)
        {
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                var testPartIds = request.UpdateData.Select(x => int.Parse(x.Id));

                var oldTestParts = await _unitOfWork.TestPartRepository.GetAllListAsync(p => testPartIds.Contains(p.Id));

                if (oldTestParts.Count() != testPartIds.Count())
                {
                    foreach (var id in testPartIds)
                    {
                        var testPart = oldTestParts.Find(x => x.Id == id)
                            ?? throw new NotFoundException(nameof(TestPart), id);
                    }
                }

                var updatedTestParts = new List<TestPart>();

                foreach (var testPart in request.UpdateData)
                {

                    var old = oldTestParts.Find(p => p.Id == Int32.Parse(testPart.Id));
                    if (old != null)
                    {
                        var newTestPart = _mapper.Map(testPart, old);
                        newTestPart.Code = SlugGenerator.GenerateSlug(newTestPart.Name);

                        updatedTestParts.Add(newTestPart);
                    }
                }

                await _unitOfWork.TestPartRepository.UpdateRangeAsync(updatedTestParts, request.CurrentUser);
                await _unitOfWork.Save();

                return updatedTestParts.Select(x => x.Id).ToList();
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }
    }
}
