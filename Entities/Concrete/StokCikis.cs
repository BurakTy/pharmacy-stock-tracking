using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.Concrete
{
    public class StokCikis:IEntity
    {
        [Key]
        public int IslemNo { get; set; }
        public DateTime IslemTarih { get; set; }
        public short IslemTip { get; set; }
        public string Aciklama { get; set; }
        public short Fk_Login { get; set; }
        public short? Fk_IsteyenDepo { get; set; }
        public DateTime? OnayTarihi { get; set; }
        public short? Fk_OnayLogin { get; set; }
        public short? Fk_IstekDepo { get; set; }
    }
}
