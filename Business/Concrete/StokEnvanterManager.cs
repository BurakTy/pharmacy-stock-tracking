using Business.Abstract;
using Core.Aspects.Autofac.Transaction;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class StokEnvanterManager : IStokEnvanterService
    {
        private IStokEnvanterDal _stokEnvanterDal;
        private IStokFaturaService _stokFaturaService;

        public StokEnvanterManager(IStokEnvanterDal stokEnvanterDal, IStokFaturaService stokFaturaService)
        {
            _stokEnvanterDal = stokEnvanterDal;
            _stokFaturaService = stokFaturaService;
        }

        [TransactionScopeAspect]
        public IDataResult<int> Add(AddStokEnvanterDto stokEnvanterDto)
        {

            _stokFaturaService.Add(stokEnvanterDto.StokFatura);


            foreach (StokFaturaDetay faturaDetay in stokEnvanterDto.StokFaturaDetay)
            {
                var envanter = new StokEnvanter()
                {
                    Stok = faturaDetay.EklenenStok,
                    Fk_Kod = stokEnvanterDto.StokFatura.Fk_Depo,
                    Fk_StokKod = faturaDetay.Fk_StokKod,
                    SonKulTarih = faturaDetay.SonKulTarih
                };

                var result = _stokEnvanterDal.Get(x => x.Fk_Kod == envanter.Fk_Kod && x.Fk_StokKod == envanter.Fk_StokKod && x.SonKulTarih == envanter.SonKulTarih);
                if (result == null)
                {
                    _stokEnvanterDal.Add(envanter);
                }
                else
                {
                    result.Stok += envanter.Stok;
                    _stokEnvanterDal.Update(result);
                }

                faturaDetay.Fk_Fatura = stokEnvanterDto.StokFatura.Id;
                _stokFaturaService.AddFaturaDetay(faturaDetay);
            }

            return new SuccessDataResult<int>(stokEnvanterDto.StokFatura.Id);
        }

        public IDataResult<List<StokEnvanter>> GetAll(short? fkDepo)
        {
            return new SuccessDataResult<List<StokEnvanter>>(fkDepo == null ? _stokEnvanterDal.GetAll() : _stokEnvanterDal.GetAll(x => x.Fk_Kod == fkDepo));
        }

        public IDataResult<List<StokEnvanterDto>> GetAllBySonKullanma(DateTime min, DateTime max, short? fkDepo)
        {
            var result = fkDepo == null ? _stokEnvanterDal.GetAllBySonKullanma(min, max) : _stokEnvanterDal.GetAllBySonKullanma(min, max, fkDepo);
            return new SuccessDataResult<List<StokEnvanterDto>>(result);
        }

        public IDataResult<List<StokEnvanterDto>> GetAllByStokkod(Guid stokKartKod, short? fkDepo) // prosedur içinden bilgiler geliyor
        {
            var result = fkDepo == null ? _stokEnvanterDal.GetAllStok(x => x.Fk_StokKod == stokKartKod) : _stokEnvanterDal.GetAllStok(x => x.Fk_StokKod == stokKartKod, fkDepo);
            var data = result.GroupBy(x => new { x.Fk_StokKod, x.Fk_Kod });
            return new SuccessDataResult<List<StokEnvanterDto>>(result);
        }

        public IDataResult<List<StokEnvanterDto>> GetAllStokByBarkod(string barkod, short? fkDepo) // prosedur içinden bilgiler geliyor
        {
            var result = fkDepo == null ? _stokEnvanterDal.GetAllStok(x => x.BarkodNo == barkod) : _stokEnvanterDal.GetAllStok(x => x.BarkodNo == barkod, fkDepo);
            return new SuccessDataResult<List<StokEnvanterDto>>(result);

        }

        public IDataResult<List<StokEnvanterDto>> GetAllStokByName(string name, short? fkDepo) // prosedur içinden bilgiler geliyor
        {
            var result = fkDepo == null ? _stokEnvanterDal.GetAllStok(x => x.StokAd.ToLower().Contains(name.ToLower())) : (name != null ? _stokEnvanterDal.GetAllStok(x => x.StokAd.ToLower().Contains(name.ToLower()), fkDepo) : _stokEnvanterDal.GetAllStok(null, fkDepo));
            return new SuccessDataResult<List<StokEnvanterDto>>(result);
        }

        public IDataResult<StokEnvanter> GetByStokKod(Guid stokKartKod, short fkDepo)
        {
            return new SuccessDataResult<StokEnvanter>(_stokEnvanterDal.Get(x => x.Fk_StokKod == stokKartKod && x.Fk_Kod == fkDepo));
        }

        public IDataResult<List<StokEnvanter>> GetByStokKodDetay(Guid stokKartKod, short fkDepo)
        {
            throw new NotImplementedException();
        }

        [TransactionScopeAspect]
        public IResult Update(Guid fkStokKod, short fkDepo, short fkGidenDepo, decimal changeStok)
        {

            if (changeStok > 0)
            {
                var envt = _stokEnvanterDal.GetAll(x => x.Fk_StokKod == fkStokKod && x.Fk_Kod == fkDepo && x.Stok > 0).OrderBy(s => s.SonKulTarih);
                decimal kalan = changeStok;
                foreach (var item in envt)
                {

                    var entGiden = _stokEnvanterDal.Get(x => x.Fk_Kod == fkGidenDepo && x.Fk_StokKod == item.Fk_StokKod && x.SonKulTarih == item.SonKulTarih);

                    if (item.Stok >= kalan)
                    {
                        item.Stok -= kalan;
                        if (entGiden != null)
                        {
                            entGiden.Stok += kalan;
                            _stokEnvanterDal.Update(entGiden);
                        }
                        else
                        {
                            _stokEnvanterDal.Add(new StokEnvanter
                            {
                                Fk_Kod = fkGidenDepo,
                                Fk_StokKod = item.Fk_StokKod,
                                SonKulTarih = item.SonKulTarih,
                                Stok = kalan

                            });
                        }
                        kalan = 0;
                    }
                    else
                    {
                        kalan -= item.Stok;

                        if (entGiden != null)
                        {
                            entGiden.Stok += item.Stok;
                            _stokEnvanterDal.Update(entGiden);
                        }
                        else
                        {
                            _stokEnvanterDal.Add(new StokEnvanter
                            {
                                Fk_Kod = fkGidenDepo,
                                Fk_StokKod = item.Fk_StokKod,
                                SonKulTarih = item.SonKulTarih,
                                Stok = item.Stok

                            });
                        }

                        item.Stok = 0;
                    }

                    _stokEnvanterDal.Update(item);

                    if (kalan == 0)
                        break;

                }
            }

            return new SuccessResult();
        }
       
        [TransactionScopeAspect]
        public IResult IlacCikisUpdate(Guid fkStokKod, short fkDepo, decimal changeStok)
        {

            if (changeStok > 0)
            {
                var envt = _stokEnvanterDal.GetAll(x => x.Fk_StokKod == fkStokKod && x.Fk_Kod == fkDepo && x.Stok > 0).OrderBy(s => s.SonKulTarih);
                decimal kalan = changeStok;
                foreach (var item in envt)
                {

                   // var entGiden = envt.Where(x => x.Fk_Kod == fkDepo && x.Fk_StokKod == item.Fk_StokKod && x.SonKulTarih == item.SonKulTarih).FirstOrDefault();

                    if (item.Stok >= kalan)
                    {
                        item.Stok -= kalan;
                        kalan = 0;
                    }
                    else
                    {
                        kalan -= item.Stok;
                        item.Stok = 0;
                    }
                    _stokEnvanterDal.Update(item);

                    if (kalan == 0)
                        break;

                }
            }

            return new SuccessResult();
        }
        public IResult OnylUpdate(StokEnvanter stokEnvanter)
        {
            _stokEnvanterDal.Update(stokEnvanter);
            return new SuccessResult();
        }
    }
}
