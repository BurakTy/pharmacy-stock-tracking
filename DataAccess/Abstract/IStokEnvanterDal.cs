using Core.DataAccess;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Abstract
{
    public interface IStokEnvanterDal : IEntityRepository<StokEnvanter>
    {
       List<StokEnvanterDto> GetAllStok(Func<StokEnvanterDto, bool> filter = null,short? fkDepo=null);

       List<StokEnvanterDto> GetAllBySonKullanma(DateTime min, DateTime max, short? fkDepo = null);
    }
}
