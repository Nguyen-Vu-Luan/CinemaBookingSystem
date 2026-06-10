using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace CinemaBooking.Models
{
    public class CinemaDbContext : DbContext
    {
        public CinemaDbContext() : base("CinemaConnection") { }

        public DbSet<NguoiDung> NguoiDungs { get; set; }
        public DbSet<TheLoai> TheLoais { get; set; }
        public DbSet<Phim> Phims { get; set; }
        public DbSet<SuatChieu> SuatChieus { get; set; }
        public DbSet<GheNgoi> GheNgois { get; set; }
        public DbSet<Ve> Ves { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
        }
    }
}