
using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class StokHastaManager : IStokHastaService
    {
        IStokHastaDal _stokHastaDal;

        public StokHastaManager(IStokHastaDal stokHastaDal)
        {
            _stokHastaDal = stokHastaDal;
        }

        public IResult Add(StokHasta stokHasta)
        {
            _stokHastaDal.Add(stokHasta);
            return new SuccessResult();
        }

        public IDataResult<List<StokHasta>> GetAll()
        {
            return new SuccessDataResult<List<StokHasta>>(_stokHastaDal.GetAll());
        }

        public IDataResult<List<StokHasta>> GetAllByName(string name)
        {
            return new SuccessDataResult<List<StokHasta>>(_stokHastaDal.GetAll(x=> x.Ad.Contains(name) || x.Soyad.Contains(name)));
        }

        public IDataResult<StokHasta> GetById(int id)
        {
            return new SuccessDataResult<StokHasta>(_stokHastaDal.Get(x => x.Id == id));
        }

        public IResult Update(StokHasta stokHasta)
        {
            _stokHastaDal.Update(stokHasta);
            return new SuccessResult();
        }
    }
}
