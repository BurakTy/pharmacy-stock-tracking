using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class StokDepoManager : IStokDepoService
    {
        private IStokDepoDal _stokDepoDal;

        public StokDepoManager(IStokDepoDal stokDepoDal)
        {
            _stokDepoDal = stokDepoDal;

        }

        public IResult Add(StokDepo stokDepo)
        {
            _stokDepoDal.Add(stokDepo);
            return new SuccessResult(Messages.ProductAdded);
        }

        public IDataResult<List<StokDepo>> GetAll()
        {
            return new SuccessDataResult<List<StokDepo>>(_stokDepoDal.GetAll(), Messages.ProductsListed);
        }

        public IDataResult<List<StokDepo>> GetAllByName(string name)
        {
            return new SuccessDataResult<List<StokDepo>>(_stokDepoDal.GetAll(s => s.Aciklama.Contains(name)), Messages.ProductsListed);
        }

        public IDataResult<List<StokDepo>> GetAllTalepAktif()
        {
            return new SuccessDataResult<List<StokDepo>>(_stokDepoDal.GetAll(p=>p.TalepAktif=="E"), Messages.ProductsListed);
        }

        public IDataResult<StokDepo> GetByKod(int StokDepoKod)
        {
            return new SuccessDataResult<StokDepo>(_stokDepoDal.Get(s => s.Kod== StokDepoKod), Messages.ProductsListed);
        }

        public IResult Update(StokDepo StokDepo)
        {
            _stokDepoDal.Update(StokDepo);
            return new SuccessResult(Messages.ProductAdded);
        }
    }
}
