using Core.DataAccess;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;

namespace DataAccess.Abstract
{
    public interface IStokFaturaDetayDal : IEntityRepository<StokFaturaDetay>
    {

        List<FaturaDetayDto> GetAllFaturaDetayDto(Func<FaturaDetayDto, bool> filter = null, int? fkFaturaId = null);
    }
}
