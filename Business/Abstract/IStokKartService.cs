using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IStokKartService
    {
        IDataResult<List<StokKart>> GetAll();
        IDataResult<List<StokKart>> GetAllByBarkod(string barkod);
        IDataResult<List<StokKart>> GetAllByName(string name);
        IDataResult<StokKart> GetByKod(Guid  stokKartKod);
        IResult Add(StokKart stokKart);
        IResult Update(StokKart stokKart);
    }
}
