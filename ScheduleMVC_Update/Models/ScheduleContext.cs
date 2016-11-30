namespace ScheduleMVC_Update.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ScheduleContext : DbContext
    {
        internal object ScheduleMainSets;

        public ScheduleContext()
            : base("name=ScheduleContext")
        {
        }

        public virtual DbSet<AuditorySet> AuditorySet { get; set; }
        public virtual DbSet<DisciplineSet> DisciplineSet { get; set; }
        public virtual DbSet<GroupSet> GroupSet { get; set; }
        public virtual DbSet<LecturerSet> LecturerSet { get; set; }
        public virtual DbSet<PairSet> PairSet { get; set; }
        public virtual DbSet<ScheduleMainSet> ScheduleMainSet { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AuditorySet>()
                .HasMany(e => e.ScheduleMainSet)
                .WithRequired(e => e.AuditorySet)
                .HasForeignKey(e => e.AuditoryAuditoryId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DisciplineSet>()
                .HasMany(e => e.ScheduleMainSet)
                .WithRequired(e => e.DisciplineSet)
                .HasForeignKey(e => e.DisciplineDisciplineId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<GroupSet>()
                .HasMany(e => e.ScheduleMainSet)
                .WithRequired(e => e.GroupSet)
                .HasForeignKey(e => e.GroupGroupId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<LecturerSet>()
                .HasMany(e => e.ScheduleMainSet)
                .WithRequired(e => e.LecturerSet)
                .HasForeignKey(e => e.LecturerLecturerId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PairSet>()
                .HasMany(e => e.ScheduleMainSet)
                .WithRequired(e => e.PairSet)
                .HasForeignKey(e => e.PairPairId)
                .WillCascadeOnDelete(false);
        }
    }
}
