using AutoMapper;
using IDonEnglist.Application.DTOs.Answer;
using IDonEnglist.Application.DTOs.AnswerChoice;
using IDonEnglist.Application.DTOs.AuthProvider;
using IDonEnglist.Application.DTOs.Category;
using IDonEnglist.Application.DTOs.CategorySkill;
using IDonEnglist.Application.DTOs.Collection;
using IDonEnglist.Application.DTOs.FinalTest;
using IDonEnglist.Application.DTOs.Media;
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
using IDonEnglist.Application.ViewModels.Role;
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
            CreateMap<CreateCategoryDTO, Category>();
            CreateMap<UpdateCategoryDTO, Category>();
            #endregion

            CreateMap<CategorySkill, CategorySkillDTO>().ReverseMap();
            CreateMap<Collection, CollectionDTO>().ReverseMap();
            CreateMap<FinalTest, FinalTestDTO>().ReverseMap();
            CreateMap<Media, MediaDTO>().ReverseMap();
            CreateMap<Passage, PassageDTO>().ReverseMap();
            CreateMap<Permission, PermissionDTO>().ReverseMap();
            CreateMap<Question, QuestionDTO>().ReverseMap();
            CreateMap<QuestionGroup, QuestionGroupDTO>().ReverseMap();
            CreateMap<QuestionGroupMedia, QuestionGroupMediaDTO>().ReverseMap();

            #region role
            CreateMap<Role, RoleDTO>().ReverseMap();
            CreateMap<CreateRoleDTO, Role>();
            CreateMap<Role, RoleViewModel>();
            #endregion

            CreateMap<RolePermission, RolePermissionDTO>().ReverseMap();
            CreateMap<Test, TestDTO>().ReverseMap();
            CreateMap<TestPart, TestPartDTO>().ReverseMap();
            CreateMap<TestPartTakenHistory, TestPartTakenHistoryDTO>().ReverseMap();
            CreateMap<TestSection, TestSectionDTO>().ReverseMap();
            CreateMap<TestSectionTakenHistory, TestSectionTakenHistoryDTO>().ReverseMap();
            CreateMap<TestTakenHistory, TestTakenHistoryDTO>().ReverseMap();
            CreateMap<TestType, TestTypeDTO>().ReverseMap();
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
