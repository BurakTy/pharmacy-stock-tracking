using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DTOs
{
    public class StokKartDto : StokKart 
    {
        public decimal DepoStok {get;set;}
        public decimal CepDepoStok {get;set;}
    }
}
