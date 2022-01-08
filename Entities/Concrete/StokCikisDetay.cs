using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.Concrete
{
    public class StokCikisDetay:IEntity
    {
        [Key]
       public int DetayNo { get; set; }
       public int IslemNo { get; set; }
       public Guid Fk_StokKod { get; set; }
       public decimal Istenilen { get; set; }
       public decimal Verilen { get; set; }
    }
}
