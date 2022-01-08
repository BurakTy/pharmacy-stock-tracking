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
    public class StokRafManager : IStokRafService
    {
        private IStokRafDal _stokRafDal;

        public StokRafManager(IStokRafDal stokRafDal)
        {
            _stokRafDal = stokRafDal;

        }

        public IResult Add(StokRaf stokRaf)
        {
            _stokRafDal.Add(stokRaf);
            return new SuccessResult(Messages.ProductAdded);
        }

        public IDataResult<List<StokRaf>> GetAll(short fkDepo)
        {
            return new SuccessDataResult<List<StokRaf>>(_stokRafDal.GetAll(x=> x.Fk_Depo==fkDepo), Messages.ProductsListed);
        }

       
        public IResult Update(StokRaf stokRaf)
        {
            _stokRafDal.Update(stokRaf);
            return new SuccessResult(Messages.ProductUpdate);
        }
    }
}
