using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Concrete
{
    public class StokEnvanter : IEntity
    {
        public Guid Fk_StokKod { get; set; }
        public short Fk_Kod { get; set; }
        public DateTime SonKulTarih { get; set; }
        public decimal Stok { get; set; }
    }
}
