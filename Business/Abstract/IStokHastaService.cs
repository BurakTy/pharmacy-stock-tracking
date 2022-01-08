using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IStokHastaService
    {
        IDataResult<List<StokHasta>> GetAll();
        IDataResult<List<StokHasta>> GetAllByName(string name);
        IDataResult<StokHasta> GetById(int id);
        IResult Add(StokHasta stokHasta);
        IResult Update(StokHasta stokHasta);
    }
}
