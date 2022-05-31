using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace ImportTrialAccount.Models
{
    public partial class TapHuanDbContext : DbContext
    {
        public TapHuanDbContext()
        {
        }

        public TapHuanDbContext(DbContextOptions<TapHuanDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<SecUser> SecUsers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=103.48.192.163,59125;Database=pro_elearning_taphuan;User ID=lap_trinh;Password=123@123aBcde;MultipleActiveResultSets=true;Persist Security Info=False;Connection Timeout=300;pooling=true;connection lifetime=0;Min Pool Size=0;max pool size=10000;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SecUser>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.ToTable("sec_User");

                entity.HasIndex(e => new { e.UserName, e.Email, e.UserId, e.UserType, e.IsFirstLogin }, "taphuan_idx_sec_User");

                entity.HasIndex(e => new { e.Email, e.UserId }, "taphuan_idx_sec_User_Email_UserId");

                entity.HasIndex(e => e.UserName, "taphuan_idx_sec_User_UserName");

                entity.HasIndex(e => e.UserName, "taphuan_idx_sec_User_UserName2");

                entity.HasIndex(e => e.UserName, "taphuan_idx_sec_User_UserName_Isdelete");

                entity.HasIndex(e => e.UserId, "taphuan_idx_tra_Teacher_UserId");

                entity.Property(e => e.AddEmail).HasMaxLength(255);

                entity.Property(e => e.Birthday).HasMaxLength(255);

                entity.Property(e => e.ConcurrencyStamp).HasMaxLength(255);

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.Email).HasMaxLength(255);

                entity.Property(e => e.EndDate)
                    .HasColumnType("datetime")
                    .HasComment("Xác định ngày kết thúc người dùng được phép đăng nhập");

                entity.Property(e => e.ExpiredCode).HasColumnType("datetime");

                entity.Property(e => e.ExpiredCodeAddEmail).HasColumnType("datetime");

                entity.Property(e => e.FirstName).HasMaxLength(255);

                entity.Property(e => e.FullName).HasMaxLength(255);

                entity.Property(e => e.IsDelete).HasDefaultValueSql("((0))");

                entity.Property(e => e.IsFirstLogin).HasDefaultValueSql("((1))");

                entity.Property(e => e.IsTraining).HasColumnName("isTraining");

                entity.Property(e => e.LastName).HasMaxLength(255);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(255);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(255);

                entity.Property(e => e.PhoneNumber).HasMaxLength(50);

                entity.Property(e => e.Position).HasMaxLength(255);

                entity.Property(e => e.SecurityStamp).HasMaxLength(255);

                entity.Property(e => e.StartDate)
                    .HasColumnType("datetime")
                    .HasComment("Xác định ngày bắt đầu người dùng được phép đăng nhập");

                entity.Property(e => e.Status).HasDefaultValueSql("((0))");

                entity.Property(e => e.UpdateTime).HasColumnType("datetime");

                entity.Property(e => e.UserName).HasMaxLength(255);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
