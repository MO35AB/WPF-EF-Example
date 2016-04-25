using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata;

namespace FlowMetEFTest.Models
{
    public partial class FlowMetContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(@"Server=DESKTOP-PDK0O6K\SQLEXPRESS;Database=FlowMet;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Field>(entity =>
            {
                entity.Property(e => e.Label)
                    .IsRequired()
                    .HasMaxLength(75)
                    .HasColumnType("varchar");
            });

            modelBuilder.Entity<Measurement>(entity =>
            {
                entity.Property(e => e.MeasurementType).HasDefaultValue(false);

                entity.HasOne(d => d.Session).WithMany(p => p.Measurement).HasForeignKey(d => d.SessionID);
            });

            modelBuilder.Entity<MeasurementPosition>(entity =>
            {
                entity.Property(e => e.Digit).HasDefaultValue(1);

                entity.Property(e => e.Location).HasDefaultValue(false);

                entity.Property(e => e.Side).HasDefaultValue(false);
            });

            modelBuilder.Entity<Patient>(entity =>
            {
                entity.Property(e => e.Identifier)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnType("varchar")
                    .HasDefaultValueSql("getdate()");

                entity.Property(e => e.Notes).HasColumnType("varchar");

                entity.Property(e => e.Sex).HasDefaultValue(false);
            });

            modelBuilder.Entity<Session>(entity =>
            {
                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.StartDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("getdate()");

                entity.HasOne(d => d.MeasurementPosition).WithMany(p => p.Session).HasForeignKey(d => d.MeasurementPositionID);

                entity.HasOne(d => d.Study).WithMany(p => p.Session).HasForeignKey(d => d.StudyID);
            });

            modelBuilder.Entity<Study>(entity =>
            {
                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("getdate()");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.Notes).HasColumnType("varchar");

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.HasOne(d => d.Patient).WithMany(p => p.Study).HasForeignKey(d => d.PatientID);
            });

            modelBuilder.Entity<StudyField>(entity =>
            {
                entity.Property(e => e.Value)
                    .IsRequired()
                    .HasMaxLength(250)
                    .HasColumnType("varchar");

                entity.HasOne(d => d.Field).WithMany(p => p.StudyField).HasForeignKey(d => d.FieldID);

                entity.HasOne(d => d.Study).WithMany(p => p.StudyField).HasForeignKey(d => d.StudyID);
            });
        }

        public virtual DbSet<Field> Field { get; set; }
        public virtual DbSet<Measurement> Measurement { get; set; }
        public virtual DbSet<MeasurementPosition> MeasurementPosition { get; set; }
        public virtual DbSet<Patient> Patient { get; set; }
        public virtual DbSet<Session> Session { get; set; }
        public virtual DbSet<Study> Study { get; set; }
        public virtual DbSet<StudyField> StudyField { get; set; }
    }
}