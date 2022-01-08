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
    public class EfStokCikisDal : EfEntityRepositoryBase<StokCikis, EczaneContext>, IStokCikisDal
    {
        public List<StokCikisDetayDto> GetCikisDetay(int islemNo)
        {
            using (EczaneContext context = new EczaneContext())
            {
               // context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking; 
                var result = context.USP_STOK_CIKIS_DETAY.FromSqlRaw($"USP_STOK_CIKIS_DETAY {islemNo}");
                return result!=null? result.ToList():new List<StokCikisDetayDto>();
            }
        }
        public List<StokCikisDto> GetOnaylanacak(DateTime min, DateTime max, short istekDepo)
        {
            using (EczaneContext context = new EczaneContext())
            {
                var result = from sc in context.STOK_CIKIS
                             join usOnay in context.STOK_USER on sc.Fk_OnayLogin.Value equals usOnay.Id into iit
                             from usOnay in iit.DefaultIfEmpty()
                             join us in context.STOK_USER on sc.Fk_Login equals us.Id
                             join sd in context.STOK_DEPO on sc.Fk_IstekDepo equals sd.Kod
                             join sdistenilen in context.STOK_DEPO on sc.Fk_IsteyenDepo equals sdistenilen.Kod
                             where sc.IslemTarih>=min.Date && sc.IslemTarih<=max.Date && sc.Fk_IstekDepo == istekDepo && sc.IslemTip==1
                             orderby sc.IslemTarih descending
                             select new StokCikisDto
                             {
                                 Aciklama = sc.Aciklama,
                                 IslemNo = sc.IslemNo,
                                 IslemTip = sc.IslemTip.ToString(),
                                 IslemTarih = sc.IslemTarih,
                                 OnayTarihi = sc.OnayTarihi,
                                 LoginName = $"{us.FirstName} {us.LastName}",
                                 OnayLoginName = $"{usOnay.FirstName} {usOnay.LastName}",
                                 IsteyenDepo = sdistenilen.Aciklama,
                                 IstekDepo = sd.Aciklama,
                             };

                return result.ToList();
            }
        }
    }
}
