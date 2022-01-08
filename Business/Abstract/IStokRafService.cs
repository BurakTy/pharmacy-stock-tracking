using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IStokRafService
    {
        IDataResult<List<StokRaf>> GetAll(short fkDepo);
        IResult Add(StokRaf stokRaf);
        IResult Update(StokRaf stokRaf);
    }

   
}
