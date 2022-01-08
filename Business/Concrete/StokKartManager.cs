using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Entities.DTOs;
using System;
using Core.Utilities.Bussines;

namespace Business.Concrete
{

    public class StokKartManager : IStokKartService
    {
        private readonly IStokKartDal _stokKartDal;

        public StokKartManager(IStokKartDal stokKartDal)
        {
            _stokKartDal = stokKartDal;

        }

        [ValidationAspect(typeof(StokKartValidator))]
        public IResult Add(StokKart stokKart)
        {
            // stokKart.StokKod = Guid.NewGuid();
            _stokKartDal.Add(stokKart);
            return new SuccessResult(Messages.ProductAdded);
        }

        public IDataResult<List<StokKart>> GetAll()
        {
           // var result = _stokKartDal.GetAll().Select(s => new StokKart { StokKod = s.StokKod,BarkodNo=s.BarkodNo,StokAd=s.StokAd}).ToList<StokKart>();
            return new SuccessDataResult<List<StokKart>>(_stokKartDal.GetAll(), Messages.ProductsListed);
        }

        public IDataResult<List<StokKart>> GetAllByBarkod(string barkod)
        {
            return new SuccessDataResult<List<StokKart>>(_stokKartDal.GetAll(s=> s.BarkodNo.Contains(barkod)), Messages.ProductsListed);
        }

        public IDataResult<List<StokKart>> GetAllByName(string name)
        {
            return new SuccessDataResult<List<StokKart>>(_stokKartDal.GetAll(s => s.StokAd.Contains(name)), Messages.ProductsListed);
        }


        public IDataResult<StokKart> GetByKod(Guid stokKartKod)
        {
            return new SuccessDataResult<StokKart>(_stokKartDal.Get(s => s.StokKod== stokKartKod), Messages.ProductsListed);
        }

        [ValidationAspect(typeof(StokKartValidator))]
        public IResult Update(StokKart stokKart)
        {
            _stokKartDal.Update(stokKart);
            return new SuccessResult(Messages.ProductAdded);
        }

        
    }
}
