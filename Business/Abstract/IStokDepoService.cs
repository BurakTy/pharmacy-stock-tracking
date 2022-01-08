using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IStokDepoService
    {
        IDataResult<List<StokDepo>> GetAll();
        IDataResult<List<StokDepo>> GetAllTalepAktif();
        IDataResult<List<StokDepo>> GetAllByName(string name);
        IDataResult<StokDepo> GetByKod(int  stokDepoKod);
        IResult Add(StokDepo stokDepo);
        IResult Update(StokDepo stokDepo);
    }
}
