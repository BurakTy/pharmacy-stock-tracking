using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.Concrete
{
    public class StokRaf:IEntity
    {
        [Key]
        public int RafId { get; set; } 
        public short Fk_Depo { get; set; } 
        public string Aciklama { get; set; }
    }
}
