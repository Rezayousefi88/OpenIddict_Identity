using EntityCode.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;

namespace EntityCode
{
    public class DataBaseContext : IdentityDbContext<ApplicationUser>
    {
        private readonly IConfiguration _configuration;
        public DataBaseContext()
        {

        }
        public DataBaseContext(IConfiguration configuration, DbContextOptions<DataBaseContext> options) : base(options)
        {
            _configuration = configuration;
        }

        public virtual DbSet<Employ> Employ { get; set; }
        public virtual DbSet<Driver> Person { get; set; }
        public virtual DbSet<City> City { get; set; }
        public virtual DbSet<Freight> Freight { get; set; }
        public virtual DbSet<Loader> Loader { get; set; }
        public virtual DbSet<PackageType> PackageType { get; set; }
        public virtual DbSet<TruckType> TruckType { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<City>(entity =>
            {
                entity.HasKey(e => e.ID);
                //entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CityName).HasMaxLength(50);

                entity.Property(e => e.Ename).HasMaxLength(50);
            });


            modelBuilder.Entity<Employ>(entity =>
            {
                //entity.Property(e => e.Id).HasColumnName("ID");
                entity.HasKey(e => e.ID);
                entity.Property(e => e.EmployCode)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.Employ)
                    .HasForeignKey(d => d.PersonId)
                    .HasConstraintName("FK_Employ_Person");
            });

            modelBuilder.Entity<Driver>(entity =>
            {
                entity.HasKey(e => e.ID);
                //entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.LastName).HasMaxLength(50);
            });

            modelBuilder.Entity<Freight>(entity =>
            {
                entity.HasKey(e => e.ID);
                //entity.Property(e => e.ID).HasColumnName("ID");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(300);

                entity.Property(e => e.DestinationCityId).HasColumnName("DestinationCityID");

                entity.Property(e => e.ExpireDate).HasMaxLength(50);

                entity.Property(e => e.ExpireTime).HasMaxLength(50);

                entity.Property(e => e.LoadDate).HasMaxLength(50);

                entity.Property(e => e.GoodName).HasMaxLength(50);

                entity.Property(e => e.LoadTime).HasMaxLength(50);

                entity.Property(e => e.SourceCityId).HasColumnName("SourceCityID");

                entity.Property(e => e.Tell).HasMaxLength(11);

                entity.HasOne(d => d.LoaderTypeNavigation)
                    .WithMany(p => p.Freight)
                    .HasForeignKey(d => d.LoaderType)
                    .HasConstraintName("FK_Freight_Loader");

                entity.HasOne(d => d.PackageTypeNavigation)
                    .WithMany(p => p.Freight)
                    .HasForeignKey(d => d.PackageType)
                    .HasConstraintName("FK_Freight_PackageType");

                entity.HasOne(d => d.TruckTypeNavigation)
                    .WithMany(p => p.Freight)
                    .HasForeignKey(d => d.TruckType)
                    .HasConstraintName("FK_Freight_TruckType");
            });

            modelBuilder.Entity<Loader>(entity =>
            {
                //entity.Property(e => e.ID).HasColumnName("ID").ValueGeneratedNever();

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<PackageType>(entity =>
            {
                //entity.Property(e => e.ID)
                //    .HasColumnName("ID")
                //    .ValueGeneratedNever();

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<TruckType>(entity =>
            {
                //entity.Property(e => e.ID)
                //    .HasColumnName("ID")
                //    .ValueGeneratedNever();

                entity.Property(e => e.Name).HasMaxLength(50);
            });


        }

    }
}
