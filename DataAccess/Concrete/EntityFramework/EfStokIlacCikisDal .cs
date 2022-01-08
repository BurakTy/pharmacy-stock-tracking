using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfStokIlacCikisDal : EfEntityRepositoryBase<StokIlacCikis, EczaneContext>, IStokIlacCikisDal
    {
        public List<StokIlacCikis> GetIlacCikisDetail(int IslemNo)
        {
            using (var context = new EczaneContext())
            {

                var result = new List<StokIlacCikis>(); 


                return result.ToList();

            }
        }
    }
}
