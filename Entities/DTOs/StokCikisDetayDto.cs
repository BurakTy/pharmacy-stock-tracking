
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.DTOs
{

    public class StokCikisDetayDto
    {
        [Key]

        
        //public DateTime IslemTarih { get; set; }
        //public DateTime OnayTarihi { get; set; }
        
        
        public int DetayNo { get; set; }
        public int IslemNo { get; set; }
        public Guid Fk_StokKod { get; set; }
        public string StokAd { get; set; }
        public string StokTurN { get; set; }
        public short StokTur { get; set; }
        public string AnaBirimN { get; set; }
        public short AnaBirim { get; set; }
        public decimal? Stok { get; set; }
        public decimal? CepStok { get; set; }
        public decimal? Istenilen { get; set; }
        public decimal? Verilen { get; set; }
      //  public short? Fk_IsteyenDepo { get; set; }
        public string IsteyenDepo { get; set; }
        public string IstekDepo { get; set; }
        public short? Fk_IstekDepo { get; set; }
        public string BarkodNo { get; set; }
    }
}
