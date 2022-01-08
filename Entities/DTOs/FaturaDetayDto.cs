using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DTOs
{
    public class FaturaDetayDto
    {
      
        public int Fk_Fatura { get; set; }
        public string StokAd { get; set; }
        public string AnaBirimN { get; set; }
        public decimal EklenenStok { get; set; }
        public decimal BirimFiyat { get; set; }
        public Guid Fk_StokKod { get; set; }
        public DateTime SonKulTarih { get; set; }
    }
}
