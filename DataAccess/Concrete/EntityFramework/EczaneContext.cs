using System.Linq;
using System.Collections.Generic;
using System.Text;
using Core.Entities.Concrete;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework
{
    public class EczaneContext : DbContext
    {
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           optionsBuilder.UseSqlServer(@"Server=(localdb)\ProjectsV13;Database=ECZANE;Trusted_Connection=true");
        }

        public  DbSet<StokBirim> STOK_BIRIM { get; set; }
        public  DbSet<StokCikis> STOK_CIKIS { get; set; }
        public  DbSet<StokCikisDetay> STOK_CIKIS_DETAY { get; set; }
        public  DbSet<StokDepo> STOK_DEPO { get; set; }
        public  DbSet<StokEnvanter> STOK_DEPO_ENVANTER { get; set; }
        public  DbSet<StokFatura> STOK_FATURA { get; set; }
        public  DbSet<StokFaturaDetay> STOK_FATURA_DETAY { get; set; }
        public  DbSet<StokHasta> STOK_HASTA { get; set; }
        public  DbSet<StokKart> STOK_KART { get; set; }
        public  DbSet<StokRaf> STOK_RAF { get; set; }
        public  DbSet<StokTur> STOK_TUR { get; set; }
        public  DbSet<User> STOK_USER { get; set; }
        public  DbSet<OperationClaim> STOK_YETKI { get; set; }
        public  DbSet<UserOperationClaim> STOK_USER_YETKI { get; set; }
        public  DbSet<StokEnvanterDto> USP_DEPO_ENVANTER { get; set; }
        public  DbSet<StokCikisDetayDto> USP_STOK_CIKIS_DETAY { get; set; }
        public  DbSet<StokIlacCikis> STOK_ILAC_CIKIS { get; set; }
        public  DbSet<FaturaDetayDto> USP_STOK_FATURA_DETAY { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StokEnvanter>()
                .HasKey(e => new { e.Fk_Kod, e.Fk_StokKod, e.SonKulTarih });
            modelBuilder.Entity<FaturaDetayDto>().HasNoKey();
            

        }
    }
}
