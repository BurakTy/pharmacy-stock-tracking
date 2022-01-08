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
    public class StokTurManager : IStokTurService
    {
        private IStokTurDal _stokTurDal;

        public StokTurManager(IStokTurDal stokTurDal)
        {
            _stokTurDal = stokTurDal;

        }

        public IResult Add(StokTur stokRaf)
        {
            _stokTurDal.Add(stokRaf);
            return new SuccessResult(Messages.ProductAdded);
        }

        public IDataResult<List<StokTur>> GetAll()
        {
            return new SuccessDataResult<List<StokTur>>(_stokTurDal.GetAll(), Messages.ProductsListed);
        }

        public IResult Update(StokTur stokTur)
        {
            _stokTurDal.Update(stokTur);
            return new SuccessResult(Messages.ProductAdded);
        }
    }
}
