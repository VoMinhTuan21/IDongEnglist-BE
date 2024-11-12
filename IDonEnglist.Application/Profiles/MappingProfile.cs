using AutoMapper;
using IDonEnglist.Application.DTOs.Answer;
using IDonEnglist.Application.DTOs.AnswerChoice;
using IDonEnglist.Application.DTOs.AuthProvider;
using IDonEnglist.Application.DTOs.Category;
using IDonEnglist.Application.DTOs.CategorySkill;
using IDonEnglist.Application.DTOs.Collection;
using IDonEnglist.Application.DTOs.FinalTest;
using IDonEnglist.Application.DTOs.Media;
using IDonEnglist.Application.DTOs.Media.Validators;
using IDonEnglist.Application.DTOs.Passage;
using IDonEnglist.Application.DTOs.Permission;
using IDonEnglist.Application.DTOs.Question;
using IDonEnglist.Application.DTOs.QuestionGroup;
using IDonEnglist.Application.DTOs.QuestionGroupMedia;
using IDonEnglist.Application.DTOs.Role;
using IDonEnglist.Application.DTOs.RolePermission;
using IDonEnglist.Application.DTOs.Test;
using IDonEnglist.Application.DTOs.TestPart;
using IDonEnglist.Application.DTOs.TestPartTakenHistory;
using IDonEnglist.Application.DTOs.TestSection;
using IDonEnglist.Application.DTOs.TestSectionTakenHistory;
using IDonEnglist.Application.DTOs.TestTakenHistory;
using IDonEnglist.Application.DTOs.TestType;
using IDonEnglist.Application.DTOs.User;
using IDonEnglist.Application.DTOs.UserAnswer;
using IDonEnglist.Application.DTOs.UserSocialAccount;
using IDonEnglist.Application.ViewModels.Category;
using IDonEnglist.Application.ViewModels.CategorySkill;
using IDonEnglist.Application.ViewModels.Collection;
using IDonEnglist.Application.ViewModels.FinalTest;
using IDonEnglist.Application.ViewModels.Media;
using IDonEnglist.Application.ViewModels.Permission;
using IDonEnglist.Application.ViewModels.Role;
using IDonEnglist.Application.ViewModels.TestPart;
using IDonEnglist.Application.ViewModels.TestType;
using IDonEnglist.Application.ViewModels.User;
using IDonEnglist.Domain;

namespace IDonEnglist.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Answer, AnswerDTO>().ReverseMap();
            CreateMap<AnswerChoice, AnswerChoiceDTO>().ReverseMap();
            CreateMap<AuthProvider, AuthProviderDTO>().ReverseMap();

            #region category
            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<CreateCategoryDTO, Category>()
                .ForMember(s => s.Skills, opt => opt.Ignore());
            CreateMap<UpdateCategoryDTO, Category>()
                .ForMember(s => s.Skills, opt => opt.Ignore())
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<Category, CategoryViewModel>()
                .ForAllMembers(opts =>
                {
                    opts.AllowNull();
                    opts.Condition((src, dest, srcMember) => srcMember != null);
                });

            CreateMap<Category, CategoryDetailViewModel>()
                .ForMember(dest => dest.Skills, opt => opt.MapFrom(src => src.Skills.Select(sk => sk.Skill)))
                .ForMember(dest => dest.Children, opt => opt.MapFrom(src => src.Children))
                .ForAllMembers(opts =>
                {
                    opts.AllowNull();
                    opts.Condition((src, dest, srcMember) => srcMember != null);
                });
            CreateMap<Category, CategoryMiniViewModel>();
            #endregion

            #region category skill
            CreateMap<CategorySkill, CategorySkillDTO>().ReverseMap();
            CreateMap<CreateCategorySkillDTO, CategorySkill>();
            CreateMap<CategorySkill, CategorySkillViewModel>();
            CreateMap<CategorySkill, CategorySkillMiniViewModel>();
            CreateMap<CategorySkill, CategorySkillWithParentViewModel>();
            #endregion

            #region collection
            CreateMap<Collection, CollectionDTO>().ReverseMap();
            CreateMap<CreateCollectionDTO, Collection>()
                .ForMember(s => s.Thumbnail, opt => opt.Ignore());
            CreateMap<UpdateCollectionDTO, Collection>()
                .ForMember(s => s.Thumbnail, opt => opt.Ignore())
                .ForAllMembers(opts =>
                {
                    opts.AllowNull();
                    opts.Condition((src, dest, srcMember) => srcMember != null);
                });
            CreateMap<Collection, CollectionViewModel>()
                .ForMember(dest => dest.Thumbnail, opt => opt.MapFrom(src => src.Thumbnail.Url));
            CreateMap<Collection, CollectionViewModelMin>();
            #endregion

            #region final test
            CreateMap<FinalTest, FinalTestDTO>().ReverseMap();
            CreateMap<CreateFinalTestDTO, FinalTest>();
            CreateMap<FinalTest, FinalTestViewModel>();
            CreateMap<UpdateFinalTestDTO, FinalTest>();
            #endregion

            #region media
            CreateMap<Media, MediaDTO>().ReverseMap();
            CreateMap<CreateMediaDTO, Media>();
            CreateMap<UpdateMediaDTO, Media>()
                .ForAllMembers(opts =>
                {
                    opts.AllowNull();
                    opts.Condition((src, dest, srcMember) =>
                        srcMember != null &&
                        (!(srcMember is string str) || !string.IsNullOrWhiteSpace(str)) &&
                        (!(srcMember is Enum) || !srcMember.Equals(Activator.CreateInstance(srcMember.GetType())))
                    );
                });
            CreateMap<Media, MediaViewModel>();

            #endregion
            CreateMap<Passage, PassageDTO>().ReverseMap();

            #region permission
            CreateMap<Permission, PermissionDTO>().ReverseMap();
            CreateMap<CreatePermissionDTO, Permission>();
            CreateMap<Permission, PermissionViewModel>()
                .ForMember(dest => dest.Children, opt => opt.MapFrom(src => src.Children.Select(c => new PermissionViewModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    Code = c.Code
                })))
                .PreserveReferences();
            #endregion

            CreateMap<Question, QuestionDTO>().ReverseMap();
            CreateMap<QuestionGroup, QuestionGroupDTO>().ReverseMap();
            CreateMap<QuestionGroupMedia, QuestionGroupMediaDTO>().ReverseMap();

            #region role
            CreateMap<Role, RoleDTO>().ReverseMap();
            CreateMap<CreateRoleDTO, Role>();
            CreateMap<UpdateRoleDTO, Role>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<Role, RoleViewModel>()
                .ForMember(dest => dest.Permissions, opt => opt.MapFrom(src => src.RolePermissions
                    .Select(p => new PermissionViewModel
                    {
                        Id = p.Permission.Id,
                        Name = p.Permission.Name,
                        Code = p.Permission.Code,
                        Parent = new PermissionViewModel
                        {
                            Id = p.Permission.Parent.Id,
                            Name = p.Permission.Parent.Name,
                            Code = p.Permission.Parent.Code,
                        }
                    })
                    )
                );
            #endregion

            CreateMap<RolePermission, RolePermissionDTO>().ReverseMap();
            CreateMap<Test, TestDTO>().ReverseMap();

            #region test part
            CreateMap<TestPart, TestPartDTO>().ReverseMap();
            CreateMap<CreateTestPartDTO, TestPart>()
                .ForMember(dest => dest.NumberOfQuestions, opt => opt.MapFrom(src => src.Questions));
            CreateMap<TestPart, TestPartViewModel>()
                .ForMember(dest => dest.Questions, opt => opt.MapFrom(src => src.NumberOfQuestions));
            CreateMap<UpdateTestPartDTO, TestPart>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.NumberOfQuestions, opt => opt.MapFrom(src => src.Questions));
            CreateMap<UpdateTestPartDTO, CreateTestPartDTO>();
            #endregion

            CreateMap<TestPartTakenHistory, TestPartTakenHistoryDTO>().ReverseMap();
            CreateMap<TestSection, TestSectionDTO>().ReverseMap();
            CreateMap<TestSectionTakenHistory, TestSectionTakenHistoryDTO>().ReverseMap();
            CreateMap<TestTakenHistory, TestTakenHistoryDTO>().ReverseMap();

            #region test type
            CreateMap<TestType, TestTypeDTO>().ReverseMap();
            CreateMap<CreateTestTypeDTO, TestType>()
                .ForMember(dest => dest.NumberOfParts, opt => opt.MapFrom(src => src.Parts.Count))
                .ForMember(dest => dest.NumberOfQuestions, opt => opt.MapFrom(src => src.Questions));
            CreateMap<TestType, TestTypeItemListViewModel>()
                .ForMember(dest => dest.Parts, opt => opt.MapFrom(src => src.NumberOfParts))
                .ForMember(dest => dest.Questions, opt => opt.MapFrom(src => src.NumberOfQuestions));
            CreateMap<TestType, TestTypeDetailViewModel>()
                .ForMember(dest => dest.Parts, opt => opt.MapFrom(src => src.TestParts))
                .ForMember(dest => dest.Questions, opt => opt.MapFrom(src => src.NumberOfQuestions));
            CreateMap<UpdateTestTypeDTO, TestType>()
                .ForMember(dest => dest.NumberOfParts, opt => opt.MapFrom(src => src.Parts.Count))
                .ForMember(dest => dest.NumberOfQuestions, opt => opt.MapFrom(src => src.Questions));
            #endregion

            CreateMap<UserAnswer, UserAnswerDTO>().ReverseMap();

            #region user
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<SignUpUserDTO, CheckUserExistDTO>();
            CreateMap<SignUpUserDTO, User>().ForMember(dest => dest.Password, opt => opt.Ignore());
            CreateMap<User, LoginUserViewModel>();
            CreateMap<User, RegisterUserViewModel>();
            #endregion

            CreateMap<UserSocialAccount, UserSocialAccountDTO>().ReverseMap();
        }
    }
}
