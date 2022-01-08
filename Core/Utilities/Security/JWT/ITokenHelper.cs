using Core.Entities.Concrete;
using System.Collections.Generic;

namespace Core.Utilities.Security.JWT
{
    public interface ITokenHelper
    {
        AccessToken CreateToken(User user,string depo, short depoTip, List<OperationClaim> operationClaims);
        bool CheckToken(string token);
    }
}
