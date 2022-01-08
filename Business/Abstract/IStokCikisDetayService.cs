using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IStokCikisDetayService
    {
        IDataResult<List<StokCikisDetay>> GetAll();
        IDataResult<List<StokCikisDetay>> GetAllByIslemNo(int islemNo);
        IDataResult<StokCikisDetay> GetByDetayNo(int  detayNo);
        IResult Add(StokCikisDetay stokCikisDetay);
        IResult AddAll(List<StokCikisDetay> stokCikisDetayList);
        IResult Update(StokCikisDetay stokCikisDetay);
    }
}
