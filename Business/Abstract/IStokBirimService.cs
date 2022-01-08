using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IStokBirimService
    {
        IDataResult<List<StokBirim>> GetAll();
        IResult Add(StokBirim stokBirim);
        IResult Update(StokBirim stokBirim);
    }

   
}
