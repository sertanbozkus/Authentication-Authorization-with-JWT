using CustomIdentityLiveClass.Context;
using CustomIdentityLiveClass.Dtos;
using CustomIdentityLiveClass.Entities;
using CustomIdentityLiveClass.Services;
using CustomIdentityLiveClass.Types;

namespace CustomIdentityLiveClass.Managers
{
    public class UserManager : IUserService
    {
        private readonly CustomIdentityDbContext _db;
        public UserManager(CustomIdentityDbContext db)
        {
            _db = db;

        }
        public async Task<ServiceMessage> AddUser(AddUserDto user)
        {
            var newUser = new UserEntity
            {
                Email = user.Email,
                Password = user.Password
            };

            _db.Users.Add(newUser);
            _db.SaveChanges();

            return new ServiceMessage
            {
                IsSucceed = true,
                Message = "Kayıt başarıyla oluşturuldu."
            };
        }

        //ajda@gmail.com
        //12345 ---------- denenen bilgiler.
        public async Task<ServiceMessage<UserInfoDto>> LoginUser(LoginUserDto user)
        {
            var userEntity = _db.Users.Where(x => x.Email.ToLower() == user.Email.ToLower()).FirstOrDefault();

            if(userEntity is null)
            {
                return new ServiceMessage<UserInfoDto> {

                    IsSucceed = false,
                    Message = "Kullanıcı adı veya şifre hatalı"
                };
            }

            if(userEntity.Password == user.Password)
            {

                return new ServiceMessage<UserInfoDto>
                {
                    IsSucceed = true,
                    Data = new UserInfoDto
                    {
                        Id = userEntity.Id,
                        Email = userEntity.Email,
                        UserType = userEntity.UserType,
                    }



                };


            }
            else
            {
                return new ServiceMessage<UserInfoDto>
                {

                    IsSucceed = false,
                    Message = "Kullanıcı adı veya şifre hatalı"
                };
            }


        }
    }
}
