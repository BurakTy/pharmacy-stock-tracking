using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.Concrete
{
    public class StokIlacCikis:IEntity
    {
        [Key]
        public int IlacCikisId { get; set; }
        public int Fk_IslemNo { get; set; }
        public int Fk_HastaId { get; set; }
        public Guid Fk_StokKod { get; set; }
        public decimal Adet { get; set; }
        public decimal BirimFiyat { get; set; }
    }
}
