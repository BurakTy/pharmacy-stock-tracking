using Core.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace Entities.Concrete
{
    public class StokFaturaDetay : IEntity
    {
        [Key]
        public int? Id { get; set; }
        public int? Fk_Fatura { get; set; }
        public Guid Fk_StokKod { get; set; }
        public decimal EklenenStok { get; set; }
        public decimal BirimFiyat { get; set; }
        public DateTime SonKulTarih {get;set;}
        public short? Fk_Birim { get; set; }
       
    }

}
