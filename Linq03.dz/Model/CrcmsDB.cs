namespace Linq03.dz.Model
{
    using System.Data.Entity;

    public partial class CrcmsDB : DbContext
    {
        public CrcmsDB()
            : base("name=CrcmsDB")
        {
        }

        public virtual DbSet<Area> Areas { get; set; }
        public virtual DbSet<Timer> Timers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Area>()
                .Property(e => e.IP)
                .IsUnicode(false);
        }
    }
}
