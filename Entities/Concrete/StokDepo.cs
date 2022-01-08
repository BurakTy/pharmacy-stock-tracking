using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.Concrete
{
    public class StokDepo:IEntity
    {
        [Key]
        public short? Kod { get; set; }
        public string Aciklama { get; set; }
        public short DepoTip { get; set; }
        public short Yil { get; set; }
        public string TalepAktif { get; set; }
        public short? Fk_StokTur { get; set; }
    }
}
