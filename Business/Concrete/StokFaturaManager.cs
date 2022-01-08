using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class StokFaturaManager : IStokFaturaService
    {
        private IStokFaturaDal _stokFaturaDal;
        private IStokFaturaDetayDal _stokFaturaDetayDal;

        public StokFaturaManager(IStokFaturaDal stokFaturaDal, IStokFaturaDetayDal stokFaturaDetayDal)
        {
            _stokFaturaDal = stokFaturaDal;
            _stokFaturaDetayDal = stokFaturaDetayDal;
        }

        public IResult Add(StokFatura stokFatura)
        {
            _stokFaturaDal.Add(stokFatura);
            return new SuccessResult("Fatura Eklendi");
        }

        public IResult AddFaturaDetay(StokFaturaDetay faturaDetay)
        {
            _stokFaturaDetayDal.Add(faturaDetay);
            return new SuccessResult("Fatura Detay Eklendi");
        }

        public IDataResult<List<StokFatura>> GetAllByDepo(short fkDepo)
        {
            throw new NotImplementedException();
        }

        public IDataResult<List<StokKartDto>> GetAllbyStokKod(string stokKod, short? fkDepo)
        {
            throw new NotImplementedException();
        }

        public IDataResult<List<StokFatura>> GetAllByTarih(DateTime min, DateTime max, short? fkDepo)
        {
            var result = fkDepo != null ? _stokFaturaDal.GetAll(x => x.FaturaTarih >= min && x.FaturaTarih <= max) : _stokFaturaDal.GetAll(x => x.FaturaTarih >= min && x.FaturaTarih <= max && x.Fk_Depo == fkDepo);
            return new SuccessDataResult<List<StokFatura>>(result);
        }

        public IDataResult<StokFatura> GetFaturaBilgi(int id)
        {
            var result = _stokFaturaDal.Get(x => x.Id == id);
            return new SuccessDataResult<StokFatura>(result);
        }

        public IDataResult<List<FaturaDetayDto>> GetFaturaDetay(int fkFatura)
        {
            var result = _stokFaturaDetayDal.GetAllFaturaDetayDto(null, fkFatura);
            return new SuccessDataResult<List<FaturaDetayDto>>(result);
        }

        public IResult Update(StokFatura stokFatura)
        {
            _stokFaturaDal.Update(stokFatura);
            return new SuccessResult("Fatura Güncellendi");
        }
    }
}
