using Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IPdfService
    {
       // IDataResult<byte[]> StokCikisOnayPdf(int IslemNo);
        IDataResult<byte[]> BakanlikFormatFatura(int IslemNo);
        IDataResult<byte[]> StokCikisPdf(int IslemNo);
        IDataResult<byte[]> FaturaPdf(int PkFatura);
        IDataResult<byte[]> IlacCikisPdf(int IslemNo);
    }
}
