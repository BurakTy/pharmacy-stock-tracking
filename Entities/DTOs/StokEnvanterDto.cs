using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.DTOs
{

    public class StokEnvanterDto
    {
        [Key]
        public Guid Fk_StokKod { get; set; }
        public decimal Toplam { get; set; }
        public short AnaBirim { get; set; }
        public string AnaBirimN { get; set; }
        public short Fk_Kod { get; set; }
        public string BarkodNo { get; set; }
        public string StokAd { get; set; }
        public string DepoAd { get; set; }
        public short StokTur { get; set; }
        public DateTime? SonKulTarih { get; set; }
        public decimal? SonFiyat { get; set; }
    }
}
