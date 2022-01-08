using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfUserDal : EfEntityRepositoryBase<User, EczaneContext>, IUserDal
    {
        public List<OperationClaim> GetClaims(User user)
        {

            using (var context = new EczaneContext())
            {
                var result = from operationClaim in context.STOK_YETKI
                             join userOperationClaim in context.STOK_USER_YETKI
                                 on operationClaim.Id equals userOperationClaim.OperationClaimId
                             where userOperationClaim.UserId == user.Id
                             select new OperationClaim { Id = operationClaim.Id, Name = operationClaim.Name };
                return result.ToList();

            }
        }
    }

}
