using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Core.Aspects.Autofac.Transaction;
using DataAccess.Abstract;
using Core.Utilities.Security.JWT;
using Business.Constants;
using Core.Utilities.Bussines;

namespace Business.Concrete
{
    public class StokCikisManager : IStokCikisService
    {
        private readonly IStokCikisDal _efStokCikisDal;
        private readonly IStokIlacCikisDal _stokIlacCikisDal;
        private readonly IStokCikisDetayService _stokCikisDetayService;
        private readonly IStokEnvanterService _stokEnvanterService;
       

        public StokCikisManager(IStokCikisDal efStokCikisDal, IStokCikisDetayService stokCikisDetayService, IStokEnvanterService stokEnvanterService, IStokIlacCikisDal stokIlacCikisDal )
        {
            _efStokCikisDal = efStokCikisDal;
            _stokCikisDetayService=stokCikisDetayService;
            _stokEnvanterService = stokEnvanterService;
            _stokIlacCikisDal=stokIlacCikisDal;
    }

        [TransactionScopeAspect] //işlem tip 1 : ana depo talep işlem , işlem tip 2 : cep depolar arası işlem
        public IDataResult<StokCikis> Add(StokCikis stokCikis,List<StokCikisDetay> stokCikisDetay)
        {
            _efStokCikisDal.Add(stokCikis);

            foreach (var item in stokCikisDetay)
            {
                item.IslemNo = stokCikis.IslemNo;
            }
            _stokCikisDetayService.AddAll(stokCikisDetay);

            return new SuccessDataResult<StokCikis>(stokCikis);
        }

        [TransactionScopeAspect]
        public IDataResult<StokCikis> Onayla(StokCikis stokCikis, List<StokCikisDetay> stokCikisDetay)
        {
            IResult result = BussinesRules.Run(
               CheckOnayStok(stokCikisDetay,(short)stokCikis.Fk_IstekDepo) 
            );

            if (result!=null)
            {
                return new ErrorDataResult<StokCikis>(result.Message);
            }

            foreach (var item in stokCikisDetay)
            {
                _stokCikisDetayService.Update(item);
                _stokEnvanterService.Update(item.Fk_StokKod, (short)stokCikis.Fk_IstekDepo,(short)stokCikis.Fk_IsteyenDepo, item.Verilen);
            }

            stokCikis.OnayTarihi = DateTime.Now;
            _efStokCikisDal.Update(stokCikis);

             return new SuccessDataResult<StokCikis>(stokCikis);
        }
        [TransactionScopeAspect]
        public IDataResult<StokCikis> IlacCikisOnayla(StokCikis stokCikis, List<StokIlacCikis> stokIlacCikis)
        {
            IResult result = BussinesRules.Run(
               CheckIlacCikis(stokIlacCikis, (short)stokCikis.Fk_IstekDepo)
            );

            if (result != null)
            {
                return new ErrorDataResult<StokCikis>(result.Message);
            }

            _efStokCikisDal.Add(stokCikis);

            foreach (var item in stokIlacCikis)
            {
                item.Fk_IslemNo = stokCikis.IslemNo;
                _stokIlacCikisDal.Add(item);
                _stokEnvanterService.IlacCikisUpdate(item.Fk_StokKod, (short)stokCikis.Fk_IstekDepo, item.Adet);
            }

            return new SuccessDataResult<StokCikis>(stokCikis);
        }
        public IDataResult<List<StokCikis>> GetAll()
        {
            return new SuccessDataResult<List<StokCikis>>(_efStokCikisDal.GetAll());
        }

        public IDataResult<List<StokCikis>> GetAllByIslemTip(short islemTip)
        {
            return new SuccessDataResult<List<StokCikis>>(_efStokCikisDal.GetAll(s => s.IslemTip == islemTip));
        }

        public IDataResult<List<StokCikis>> GetAllByTarih(DateTime min, DateTime max,short? isteyenDepo)
        {
            return new SuccessDataResult<List<StokCikis>>(isteyenDepo == null? _efStokCikisDal.GetAll(s => s.IslemTarih >= min.Date && s.IslemTarih <= max.Date && s.OnayTarihi==null): _efStokCikisDal.GetAll(s => s.IslemTarih >= min && s.IslemTarih <= max && s.Fk_IsteyenDepo== isteyenDepo));
        }

        public IDataResult<List<StokCikis>> GetByFkDepo(short fkDepo, bool isteyen)
        {
            if (isteyen)
            {
                return new SuccessDataResult<List<StokCikis>>(_efStokCikisDal.GetAll(s => s.Fk_IsteyenDepo == fkDepo && s.IslemTip==1));
            }
            return new SuccessDataResult<List<StokCikis>>(_efStokCikisDal.GetAll(s => s.Fk_IstekDepo == fkDepo && s.IslemTip == 1));
        }

        public IDataResult<List<StokCikis>> GetByFkLogin(int fkLogin)
        {
            return new SuccessDataResult<List<StokCikis>>(_efStokCikisDal.GetAll(s => s.Fk_Login == fkLogin));
        }

        public IDataResult<List<StokCikis>> GetByFkOnaylayan(int fkOnaylayan)
        {
            return new SuccessDataResult<List<StokCikis>>(_efStokCikisDal.GetAll(s => s.Fk_OnayLogin == fkOnaylayan));
        }

        public IDataResult<StokCikis> GetByIslemNo(int islemNo)
        {
            return new SuccessDataResult<StokCikis>(_efStokCikisDal.Get(s => s.IslemNo == islemNo));
        }

        public IDataResult<List<StokCikisDetayDto>> GetCikisDetay(int islemNo)
        {
            return new SuccessDataResult<List<StokCikisDetayDto>>(_efStokCikisDal.GetCikisDetay(islemNo));
        }

        public IDataResult<List<StokCikisDto>> GetOnaylanacak(DateTime min, DateTime max, short istekDepo)
        {
            return new SuccessDataResult<List<StokCikisDto>>(_efStokCikisDal.GetOnaylanacak(min,max,istekDepo));
        }

        public IResult Update(StokCikis stokCikis)
        {
            _efStokCikisDal.Update(stokCikis);
            return new SuccessResult();
        }

        private IResult CheckOnayStok(List<StokCikisDetay> stokdetayList,short fkDepo)
        {
            foreach (var stokdetay in stokdetayList)
            {
                // var result = _stokEnvanterService.GetByStokKod(stokdetay.Fk_StokKod, fkDepo);
                var result = _stokEnvanterService.GetAllByStokkod(stokdetay.Fk_StokKod, fkDepo);
                if (result.Data!=null)
                {
                    if(result.Data[0].Toplam < stokdetay.Verilen)
                        return new ErrorResult($"Stok->{result} , Verilen ->{stokdetay.Verilen} {Messages.InsufficientStock}");
                }
                else
                {
                    return new ErrorResult(Messages.OutOfStock);
                }
            }
            
            return new SuccessResult();
        }
        
        private IResult CheckIlacCikis(List<StokIlacCikis> stokIlacCikis,short fkDepo)
        {
            var data = stokIlacCikis.GroupBy(x => x.Fk_StokKod).Select(z => new StokIlacCikis { Fk_StokKod = z.First().Fk_StokKod, Adet = z.Sum(x => x.Adet) });

                foreach (var stokdetay in data)
            {
                // var result = _stokEnvanterService.GetByStokKod(stokdetay.Fk_StokKod, fkDepo);
                var result = _stokEnvanterService.GetAllByStokkod(stokdetay.Fk_StokKod, fkDepo);
                if (result.Data!=null)
                {
                    if(result.Data[0].Toplam < stokdetay.Adet)
                        return new ErrorResult($"Stok->{result.Data[0].StokAd} , Verilen ->{stokdetay.Adet}, {Messages.InsufficientStock}");
                }
                else
                {
                    return new ErrorResult($"{ Messages.OutOfStock }");
                }
            }
            
            return new SuccessResult();
        }

    }
}
