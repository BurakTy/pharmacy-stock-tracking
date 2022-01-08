using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DTOs
{
    public class AddStokCikisDto
    {
        public List<StokCikisDetay> stokCikisDetay { get; set; }
        public StokCikis stokCikis { get; set; }
    }
   
}
