using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IStokFaturaService
    {
        IDataResult<List<StokFatura>> GetAllByDepo(short fkDepo);
        IDataResult<List<StokFatura>> GetAllByTarih(DateTime min,DateTime max, short? fkDepo);
        IResult Add(StokFatura stokFatura);
        IResult Update(StokFatura stokFatura);
        IDataResult<StokFatura> GetFaturaBilgi(int id);
        IDataResult<List<FaturaDetayDto>> GetFaturaDetay(int fkFatura);
        IResult AddFaturaDetay(StokFaturaDetay faturaDetay);

        IDataResult<List<StokKartDto>> GetAllbyStokKod(string stokKod, short? fkDepo);

    }
}
