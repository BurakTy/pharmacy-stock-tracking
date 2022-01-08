using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IStokCikisService
    {
        IDataResult<List<StokCikis>> GetAll();
        IDataResult<List<StokCikis>> GetAllByIslemTip(short islemTip);
        IDataResult<List<StokCikis>> GetAllByTarih(DateTime min, DateTime max,short? isteyenDepo);
        IDataResult<List<StokCikisDto>> GetOnaylanacak(DateTime min, DateTime max,short istekDepo);
        IDataResult<List<StokCikisDetayDto>> GetCikisDetay(int islemNo);
        IDataResult<List<StokCikis>> GetByFkDepo(short fkDepo, bool isteyen);
        IDataResult<List<StokCikis>> GetByFkLogin(int fkLogin);
        IDataResult<List<StokCikis>> GetByFkOnaylayan(int fkOnaylayan);
        IDataResult<StokCikis> GetByIslemNo(int islemNo);
        IDataResult<StokCikis> Add(StokCikis stokCikis, List<StokCikisDetay> stokCikisDetay);
        IDataResult<StokCikis> IlacCikisOnayla(StokCikis stokCikis, List<StokIlacCikis> stokIlacCikis);
        IResult Update(StokCikis stokCikis);
        IDataResult<StokCikis> Onayla(StokCikis stokCikis, List<StokCikisDetay> stokCikisDetay);

    }
}
