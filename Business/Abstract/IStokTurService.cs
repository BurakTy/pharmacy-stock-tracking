using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IStokTurService
    {
        IDataResult<List<StokTur>> GetAll();
        IResult Add(StokTur stokTur);
        IResult Update(StokTur stokTur);
    }

}
