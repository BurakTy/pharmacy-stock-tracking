using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.Concrete
{
    public class StokKart:IEntity
    {
        [Key]
        public Guid? StokKod { get; set; }
        public string StokAd { get; set; }
        public short StokTur { get; set; }
        public string BarkodNo { get; set; }
        public string JenerikAdi { get; set; }
        public string AtcKodu { get; set; }
        public string EtkenMadde { get; set; }
        public short AnaBirim { get; set; }
        public short? Birim1 { get; set; }
        public short? Birim1Adet { get; set; }
        public short? GrupKod { get; set; }
        public short? Fk_Raf { get; set; }
    }
}
