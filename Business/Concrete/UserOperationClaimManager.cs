using Business.Abstract;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class UserOperationClaimManager : IUserOperationClamService
    {
        private IUserOperationClaimDal _userOperationClaim;

        public UserOperationClaimManager(IUserOperationClaimDal userOperationClaim)
        {
            _userOperationClaim = userOperationClaim;
        }

        public IResult Add(int user, int claim)
        {
            var userClaim = new UserOperationClaim();
            userClaim.OperationClaimId = claim;
            userClaim.UserId = user;
            _userOperationClaim.Add(userClaim);
            return new SuccessResult();
           
        }

        public IResult Delete(int Id)
        {
            _userOperationClaim.Delete(new UserOperationClaim() { Id = Id });
            return new SuccessResult();
        }

    }
}
