using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.Concrete
{
    public class StokBirim : IEntity
    {
        [Key]
        public short Id { get; set; }
        public string BirimAdi { get; set; }
        public string Kisaltma { get; set; }
        public bool IsAktif { get; set; }
    }
}
