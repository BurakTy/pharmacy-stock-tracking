using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.Concrete
{
    public class StokFatura:IEntity
    {   
        [Key]
        public int Id { get; set; }
        public string FaturaNo  { get; set; }
        public DateTime FaturaTarih  { get; set; }
        public string BakanlikOnayNo  { get; set; }
        public string BakanlikProtokolNo  { get; set; }
        public string IhaleNo { get; set; }
        public string GelenBilgiUnvan { get; set; }
        public DateTime GelenFaturaTarih { get; set; }
        public string GelenFaturaNo { get; set; }
        public DateTime KayitTarih { get; set; }
        public int Fk_Login { get; set; }
        public short Fk_Depo { get; set; }
    }
}
