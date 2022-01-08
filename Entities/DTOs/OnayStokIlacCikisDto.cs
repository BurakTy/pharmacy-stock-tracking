using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DTOs
{
    public class OnayStokIlacCikisDto
    {
        public List<StokIlacCikis> stokIlacCikis { get; set; }
        public StokCikis stokCikis { get; set; }
    }
}
