using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.Concrete
{
    public class StokTur:IEntity
    {
        [Key]
        public int Id { get; set; } 
        public string TurAdi { get; set; } 
        public bool IsAktif { get; set; }
    }
}
