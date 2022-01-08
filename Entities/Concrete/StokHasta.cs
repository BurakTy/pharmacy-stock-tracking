using Core.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace Entities.Concrete
{
    public class StokHasta : IEntity
    {
        [Key]
        public int? Id { get; set; }
        public string KimlikNo { get; set; }
        public int? Uyruk { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public short? Yas { get; set; }
        public char Cinsiyet { get; set; }
        public DateTime? KayitTarih { get; set; }
        public DateTime GirisTarih { get; set; }
        public DateTime? CikisTarih { get; set; }
        public bool? IsAktif { get; set; }
        public string KimlikNoGizli { get => "**********"+ KimlikNo.Substring(KimlikNo.Length-3); }
        
       
    }
}
