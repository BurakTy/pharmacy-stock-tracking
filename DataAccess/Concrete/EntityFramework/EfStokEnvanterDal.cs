using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfStokEnvanterDal : EfEntityRepositoryBase<StokEnvanter, EczaneContext>, IStokEnvanterDal
    {
        public List<StokEnvanterDto> GetAllBySonKullanma(DateTime min, DateTime max, short? fkDepo = null)
        {
            using (var context = new EczaneContext())
            {
                
                string depo = fkDepo != null ? fkDepo.ToString() : "NULL";
                context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
                var result = context.USP_DEPO_ENVANTER.FromSqlRaw($"USP_DEPO_ENVANTER_SKT '{min}','{max}',{depo}");


                return result.ToList();

            }
        }

        public List<StokEnvanterDto> GetAllStok(Func<StokEnvanterDto, bool> filter = null,short? fkDepo = null)
        {

            using (var context = new EczaneContext())
            {
                string depo = fkDepo != null ? fkDepo.ToString() : "NULL";
                context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
                var result = filter==null?context.USP_DEPO_ENVANTER.FromSqlRaw($"USP_DEPO_ENVANTER {depo}"): context.USP_DEPO_ENVANTER.FromSqlRaw($"USP_DEPO_ENVANTER {depo}").ToList().Where(filter);
                
                return result.ToList();

            }
        }

    }
}
