using Microsoft.EntityFrameworkCore;

namespace WotBlitzStatisticsPro.Persistence.DataContext
{
    public class WotBlitzStatisticsProContext: DbContext
    {
        public DbSet<State> State { get; set; } = null!;
        public DbSet<DictionaryVehicle> VehiclesDictionary { get; set; } = null!;
        public DbSet<DictionaryVehicleModule> VehicleModulesDictionary { get; set; } = null!;
        public DbSet<DictionaryVehicleModuleRelation> VehicleModulesDictionaryRelation { get; set; } = null!;
        public DbSet<DictionaryNextVehicle> VehiclesTreeDictionary { get; set; } = null!;
        public DbSet<PlayerSession> PlayerSessions { get; set; } = null!;
        public DbSet<PlayerTankSession> PlayerTankSessions { get; set; } = null!;
        public DbSet<ResourcePlanning> ResourcePlanning { get; set; } = null!;

        public WotBlitzStatisticsProContext(DbContextOptions<WotBlitzStatisticsProContext> opts): base(opts)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ResourcePlanning>(e => {
                e.HasKey(p => p.PlanId);
                e.Property(p => p.PlanId).ValueGeneratedOnAdd();
                e.HasIndex(p => p.AccountId);
            });

            modelBuilder.Entity<PlayerSession>(e => {
                e.HasKey(s => s.PlayerSessionId);
                e.Property(s => s.PlayerSessionId).ValueGeneratedOnAdd();
                e.HasIndex(s => s.AccountId);
                e.HasIndex(s => s.LastBattleTime);
            });

            modelBuilder.Entity<PlayerTankSession>(e => {
                e.HasKey(s => s.PlayerTankSessionId);
                e.Property(s => s.PlayerTankSessionId).ValueGeneratedOnAdd();
                e.HasIndex(s => s.TankId);
                e.HasIndex(s => s.LastBattleTime);
            });

            modelBuilder.Entity<PlayerTankSession>()
                .HasOne<PlayerSession>(s => s.PlayerSession)
                .WithMany(g => g.TanksSession)
                .HasForeignKey(s => s.PlayerSessionId);


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
            
            // https://www.entityframeworktutorial.net/efcore/configure-many-to-many-relationship-in-ef-core.aspx
            modelBuilder.Entity<DictionaryVehicleModuleRelation>().HasKey(mr => new { mr.ModuleId, mr.TankId });
            modelBuilder.Entity<DictionaryVehicleModuleRelation>()
                .HasOne<DictionaryVehicleModule>(sc => sc.Module)
                .WithMany(s => s.VehicleModulesRelation)
                .HasForeignKey(sc => sc.ModuleId);
            modelBuilder.Entity<DictionaryVehicleModuleRelation>()
                .HasOne<DictionaryVehicle>(sc => sc.Tank)
                .WithMany(s => s.VehicleModulesRelation)
                .HasForeignKey(sc => sc.TankId);

            // https://www.entityframeworktutorial.net/efcore/configure-one-to-many-relationship-using-fluent-api-in-ef-core.aspx
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