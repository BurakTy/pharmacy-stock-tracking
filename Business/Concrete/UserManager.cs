using Business.Abstract;
using Business.Constants;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        private IUserDal _userDal;
        private IUserOperationClamService _userOperationClamService;

        public UserManager(IUserDal userDal, IUserOperationClamService userOperationClamService)
        {
            _userDal = userDal;
            _userOperationClamService = userOperationClamService;
        }

        public IDataResult<List<OperationClaim>> GetClaims(User user)
        {
            return new SuccessDataResult<List<OperationClaim>>(_userDal.GetClaims(user));
        }

        public IResult Add(User user)
        {
            _userDal.Add(user);
            return new SuccessResult();
        }

        public IDataResult<User> GetByMail(string email)
        {
            var sonuc = _userDal.Get(u => u.Email == email);
            return new SuccessDataResult<User>(_userDal.Get(u => u.Email == email));
        }

        public IResult Update(User user)
        {
            _userDal.Update(user);
            return new SuccessResult();
        }


        public IResult AddClaims(int user, int claim)
        {
            _userOperationClamService.Add(user, claim);
            return new SuccessResult();
        }

    }
}
