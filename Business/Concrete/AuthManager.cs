using Business.Abstract;
using Business.Constants;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.JWT;
using Entities.DTOs;
using System.Collections.Generic;

namespace Business.Concrete
{
    public class AuthManager : IAuthService
    {
        private IUserService _userService;
        private IStokDepoService _depoService;
        private ITokenHelper _tokenHelper;
        public AuthManager(IUserService userService, ITokenHelper tokenHelper, IStokDepoService depoService)
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
            _depoService = depoService;
        }

        public IDataResult<User> Register(UserForRegisterDto userForRegisterDto)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(userForRegisterDto.Password, out passwordHash, out passwordSalt);
            var user = new User
            {
                Email = userForRegisterDto.Email,
                FirstName = userForRegisterDto.FirstName,
                LastName = userForRegisterDto.LastName,
                Fk_Depo = userForRegisterDto.Fk_Depo,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Fk_User = userForRegisterDto.Fk_User,
                RegisterDate = System.DateTime.Now,
                Status=true
            };
            _userService.Add(user);

            foreach(int claim in userForRegisterDto.Claims)
            {
               if(claim!=1 && claim != 2)
                {
                    _userService.AddClaims(user.Id, claim);
                }
            }

            if (_depoService.GetByKod(user.Fk_Depo).Data.DepoTip == 1) // Ana Depo Yetkisi
            {
                _userService.AddClaims(user.Id, 7);
            }

            return new SuccessDataResult<User>(user, Messages.UserRegistered);
        }

        public IDataResult<User> Login(UserForLoginDto userForLoginDto)
        {
            var userToCheck = _userService.GetByMail(userForLoginDto.Email).Data;
            if (userToCheck == null || !userToCheck.Status)
            {
                return new ErrorDataResult<User>(Messages.UserNotFound);
            }

            if (!HashingHelper.VerifyPasswordHash(userForLoginDto.Password, userToCheck.PasswordHash, userToCheck.PasswordSalt))
            {
                return new ErrorDataResult<User>(Messages.PasswordError);
            }

            return new SuccessDataResult<User>(userToCheck, Messages.SuccessfulLogin);
        }
        public IDataResult<User> RePasword(UserForRegisterDto userForRegisterDto)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(userForRegisterDto.Password, out passwordHash, out passwordSalt);
            var user = _userService.GetByMail(userForRegisterDto.Email).Data;
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            _userService.Update(user);
            return new SuccessDataResult<User>(user, Messages.SuccessPaswordChange);

        }

        public IResult UserExists(string email)
        {
            if (_userService.GetByMail(email).Data != null)
            {
                return new ErrorResult(Messages.UserAlreadyExists);
            }
            return new SuccessResult();
        }

        public IDataResult<AccessToken> CreateAccessToken(User user)
        {
            var claims = _userService.GetClaims(user).Data;
            var depo = _depoService.GetByKod(user.Fk_Depo);
            var accessToken = _tokenHelper.CreateToken(user,depo.Data.Aciklama,depo.Data.DepoTip, claims);
            return new SuccessDataResult<AccessToken>(accessToken, Messages.AccessTokenCreated);
        }

        public bool CheckToken(string authorization)
        {
            return _tokenHelper.CheckToken(authorization);
        }
    }
}
