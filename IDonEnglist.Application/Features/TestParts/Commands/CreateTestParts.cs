using AutoMapper;
using IDonEnglist.Application.DTOs.TestPart;
using IDonEnglist.Application.Models.Identity;
using IDonEnglist.Application.Persistence.Contracts;
using IDonEnglist.Application.Utils;
using IDonEnglist.Application.ViewModels.TestPart;
using IDonEnglist.Domain;
using MediatR;

namespace IDonEnglist.Application.Features.TestParts.Commands
{
    public class CreateTestParts : IRequest<List<TestPartViewModel>>
    {
        public List<CreateTestPartDTO> CreateData { get; set; }
        public CurrentUser CurrentUser { get; set; }
        public int TestTypeId { get; set; }
    }

    public class CreateTestPartHandler : IRequestHandler<CreateTestParts, List<TestPartViewModel>>
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        public CreateTestPartHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<List<TestPartViewModel>> Handle(CreateTestParts request, CancellationToken cancellationToken)
        {
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                var testParts = _mapper.Map<List<TestPart>>(request.CreateData);

                testParts.ForEach(x =>
                {
                    x.TestTypeId = request.TestTypeId;
                    x.Code = SlugGenerator.GenerateSlug(x.Name);
                });

                await _unitOfWork.TestPartRepository.AddRangeAsync(testParts, request.CurrentUser);
                await _unitOfWork.Save();

                await _unitOfWork.CommitTransactionAsync();

                return _mapper.Map<List<TestPartViewModel>>(testParts);
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }
    }
}
