using Core.DataAccess;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;

namespace DataAccess.Abstract
{
    public interface IStokCikisDal : IEntityRepository<StokCikis>
    {
        List<StokCikisDetayDto> GetCikisDetay(int islemNo);
        List<StokCikisDto> GetOnaylanacak(DateTime min, DateTime max, short istekDepo);
    }
}
