using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DTOs
{
    public class StokCikisDto
    {
        public int IslemNo { get; set; }
        public DateTime IslemTarih { get; set; }
        public DateTime? OnayTarihi { get; set; }
        public string IsteyenDepo { get; set; }
        public string IstekDepo { get; set; }
        public string Aciklama { get; set; }
        public string LoginName { get; set; }
        public string OnayLoginName { get; set; }
        public string IslemTip { get; set; }
    }
}
