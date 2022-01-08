using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfUserOperationClaimDal: EfEntityRepositoryBase<UserOperationClaim, EczaneContext>, IUserOperationClaimDal
    {
        
    }
}
