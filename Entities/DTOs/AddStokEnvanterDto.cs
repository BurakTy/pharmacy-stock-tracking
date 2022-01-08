using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DTOs
{
    public class AddStokEnvanterDto
    {
        public List<StokFaturaDetay> StokFaturaDetay { get; set; }
        public StokFatura StokFatura { get; set; }
    }
}
