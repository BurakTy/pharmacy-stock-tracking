using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using System.Collections.Generic;

namespace Business.Concrete
{
    public class StokCikisDetayManager : IStokCikisDetayService
    {
        private IStokCikisDetayDal _efStokCikisDetayDal;

        public StokCikisDetayManager(IStokCikisDetayDal efStokCikisDetayDal)
        {
            _efStokCikisDetayDal = efStokCikisDetayDal;
        }

        public IResult Add(StokCikisDetay stokCikisDetay)
        {
            _efStokCikisDetayDal.Add(stokCikisDetay);
            return new SuccessResult();
        }

        
        public IResult AddAll(List<StokCikisDetay> stokCikisDetayList)
        {
            _efStokCikisDetayDal.AddAll(stokCikisDetayList);
            return new SuccessResult();
        }

        public IDataResult<List<StokCikisDetay>> GetAll()
        {
            return new SuccessDataResult<List<StokCikisDetay>>(_efStokCikisDetayDal.GetAll());

        }

        public IDataResult<List<StokCikisDetay>> GetAllByIslemNo(int islemNo)
        {
            return new SuccessDataResult<List<StokCikisDetay>>(_efStokCikisDetayDal.GetAll(s=> s.IslemNo==islemNo));
        }

        public IDataResult<StokCikisDetay> GetByDetayNo(int detayNo)
        {
            return new SuccessDataResult<StokCikisDetay>(_efStokCikisDetayDal.Get(s => s.DetayNo == detayNo));
        }

        public IResult Update(StokCikisDetay stokCikisDetay)
        {
            _efStokCikisDetayDal.Update(stokCikisDetay);
            return new SuccessResult();
        }
    }
}
