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
    public class StokBirimManager : IStokBirimService
    {
        private IStokBirimDal _stokBirimDal;

        public StokBirimManager(IStokBirimDal stokBirimDal)
        {
            _stokBirimDal = stokBirimDal;

        }

        public IResult Add(StokBirim stokBirim)
        {
            _stokBirimDal.Add(stokBirim);
            return new SuccessResult(Messages.ProductAdded);
        }

        public IDataResult<List<StokBirim>> GetAll()
        {
            return new SuccessDataResult<List<StokBirim>>(_stokBirimDal.GetAll(), Messages.ProductsListed);
        }

       
        public IResult Update(StokBirim stokBirim)
        {
            _stokBirimDal.Update(stokBirim);
            return new SuccessResult(Messages.ProductAdded);
        }
    }
}
