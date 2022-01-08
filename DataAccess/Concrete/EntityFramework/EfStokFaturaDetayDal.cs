using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfStokFaturaDetayDal : EfEntityRepositoryBase<StokFaturaDetay, EczaneContext>, IStokFaturaDetayDal
    {
        public List<FaturaDetayDto> GetAllFaturaDetayDto(Func<FaturaDetayDto, bool> filter = null, int? fkFaturaId = null)
        {
            using (var context = new EczaneContext())
            {
                string faturaId = fkFaturaId != null ? fkFaturaId.ToString() : "NULL";
                context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
                var result = filter == null ? context.USP_STOK_FATURA_DETAY.FromSqlRaw($"USP_STOK_FATURA_DETAY {faturaId}") : context.USP_STOK_FATURA_DETAY.FromSqlRaw($"USP_STOK_FATURA_DETAY {faturaId}").ToList().Where(filter);

                return result.ToList();

            }
        }
    }
}
