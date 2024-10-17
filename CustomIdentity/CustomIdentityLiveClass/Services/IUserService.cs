using CustomIdentityLiveClass.Dtos;
using CustomIdentityLiveClass.Types;

namespace CustomIdentityLiveClass.Services
{
    public interface IUserService
    {

        Task<ServiceMessage> AddUser(AddUserDto user);

        Task<ServiceMessage<UserInfoDto>> LoginUser(LoginUserDto user);
     

    }
}


