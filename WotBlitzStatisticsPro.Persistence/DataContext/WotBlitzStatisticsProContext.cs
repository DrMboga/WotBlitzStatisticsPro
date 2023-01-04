using Microsoft.EntityFrameworkCore;

namespace WotBlitzStatisticsPro.Persistence.DataContext
{
    public class WotBlitzStatisticsProContext: DbContext
    {
        public DbSet<State> State { get; set; } = null!;
        public DbSet<DictionaryVehicle> VehiclesDictionary { get; set; } = null!;
        public DbSet<DictionaryVehicleModule> VehicleModulesDictionary { get; set; } = null!;
        public DbSet<DictionaryNextVehicle> VehiclesTreeDictionary { get; set; } = null!;

        public WotBlitzStatisticsProContext(DbContextOptions<WotBlitzStatisticsProContext> opts): base(opts)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<State>(e => {
                e.HasKey(s => s.Id);
            });

            modelBuilder.Entity<DictionaryVehicle>(e => {
                e.HasKey(s => s.TankId);
                e.Property(s => s.TankId).ValueGeneratedNever();
                e.HasIndex(s => s.Tier);
                e.HasIndex(s => s.Type);
            });


            modelBuilder.Entity<DictionaryVehicleModule>(e => {
                e.HasKey(s => s.ModuleId);
                e.Property(s => s.ModuleId).ValueGeneratedNever();
            });

            // https://www.entityframeworktutorial.net/efcore/configure-one-to-many-relationship-using-fluent-api-in-ef-core.aspx
            modelBuilder.Entity<DictionaryVehicleModule>()
                .HasOne<DictionaryVehicle>(s => s.Tank)
                .WithMany(g => g.VehicleModules)
                .HasForeignKey(s => s.TankId);

            modelBuilder.Entity<DictionaryNextVehicle>(e => {
                e.HasKey(s => s.Id);
                e.Property(s => s.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<DictionaryNextVehicle>()
                .HasOne<DictionaryVehicle>(s => s.Tank)
                .WithMany(g => g.NextVehicles)
                .HasForeignKey(s => s.TankId);
        }

    }
}