using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Business.Abstract
{
    public interface IStokEnvanterService
    {
        IDataResult<int> Add(AddStokEnvanterDto stokEnvanterDto);
        IResult Update(Guid fkStokKod, short fkDepo, short fkGidenDepo, decimal changeStok);
        IResult IlacCikisUpdate(Guid fkStokKod, short fkDepo,decimal changeStok);
        IResult OnylUpdate(StokEnvanter stokEnvanter);

        IDataResult<List<StokEnvanter>> GetAll(short? fkDepo = null);
        IDataResult<List<StokEnvanterDto>> GetAllByStokkod(Guid stokKartKod, short? fkDepo=null);
        IDataResult<List<StokEnvanter>> GetByStokKodDetay(Guid stokKartKod, short fkDepo);// Sonkullanma tarihleriyle beraber
        IDataResult<StokEnvanter> GetByStokKod(Guid stokKartKod, short fkDepo); // Sadece stok miktarı

        IDataResult<List<StokEnvanterDto>> GetAllBySonKullanma(DateTime min, DateTime max, short? fkDepo = null);
        IDataResult<List<StokEnvanterDto>> GetAllStokByName(string name, short? fkDepo = null);
        IDataResult<List<StokEnvanterDto>> GetAllStokByBarkod(string barkod, short? fkDepo = null);
    }
}
