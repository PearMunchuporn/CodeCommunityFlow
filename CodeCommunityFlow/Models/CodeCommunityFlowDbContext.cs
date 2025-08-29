using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace CodeCommunityFlow.Models
{
    public partial class CodeCommunityFlowDbContext : DbContext
    {
        public CodeCommunityFlowDbContext()
        {
        }

        public CodeCommunityFlowDbContext(DbContextOptions<CodeCommunityFlowDbContext> options)
            : base(options)
        {
          
        }

        public virtual DbSet<Answers> Answers { get; set; }
        public virtual DbSet<Categories> Categories { get; set; }
        public virtual DbSet<ContactUs> ContactUs { get; set; }
        public virtual DbSet<HowKnowUs> HowKnowUs { get; set; }
        public virtual DbSet<Questions> Questions { get; set; }
     
        public virtual DbSet<Report> Report { get; set; }
        public virtual DbSet<ReportLogs> ReportLogs { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<Votes> Votes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=LAPTOP-MDUOSCAT\\SQLEXPRESS;Database=CodeCommunityProject;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Answers>(entity =>
            {
                entity.HasKey(e => e.AnswerId)
                    .HasName("PK__Answers__D482502499C188E3");

                entity.Property(e => e.AnswerId).HasColumnName("AnswerID");

                entity.Property(e => e.AnswerDateTime).HasColumnType("datetime");

                entity.Property(e => e.AnswerText).IsUnicode(false);

                entity.Property(e => e.Image).IsUnicode(false);

                entity.Property(e => e.QuestionId).HasColumnName("QuestionID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.Answers)
                    .HasForeignKey(d => d.QuestionId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__Answers__Questio__48CFD27E");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Answers)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__Answers__UserID__47DBAE45");
            });

            modelBuilder.Entity<Categories>(entity =>
            {
                entity.HasKey(e => e.CategoryId);

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.CatagoryName).IsUnicode(false);
            });

            modelBuilder.Entity<ContactUs>(entity =>
            {
                entity.HasKey(e => e.ContactId);

                entity.ToTable("ContactUS");

                entity.Property(e => e.ContactId).HasColumnName("ContactID");

                entity.Property(e => e.Company).IsUnicode(false);

                entity.Property(e => e.Country).IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.HowHearUsId).HasColumnName("HowHearUsID");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.WorkEmail)
                    .IsRequired()
                    .IsUnicode(false);

                entity.HasOne(d => d.HowHearUs)
                    .WithMany(p => p.ContactUs)
                    .HasForeignKey(d => d.HowHearUsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ContactUS_HowKnowUs");
            });

            modelBuilder.Entity<HowKnowUs>(entity =>
            {
                entity.HasKey(e => e.HowHearUsId);

                entity.Property(e => e.HowHearUsId).HasColumnName("HowHearUsID");

                entity.Property(e => e.Know).IsUnicode(false);
            });

            modelBuilder.Entity<Questions>(entity =>
            {
                entity.HasKey(e => e.QuestionId)
                    .HasName("PK__Question__0DC06F8C9E307472");

                entity.Property(e => e.QuestionId).HasColumnName("QuestionID");

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.Image).IsUnicode(false);

                entity.Property(e => e.QuestionContent).IsUnicode(false);

                entity.Property(e => e.QuestionDateTime).HasColumnType("datetime");

                entity.Property(e => e.QuestionName).IsUnicode(false);

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Questions)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__Questions__Categ__3C69FB99");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Questions)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__Questions__UserI__3B75D760");
            });


            modelBuilder.Entity<Report>(entity =>
            {
                entity.Property(e => e.ReportId).HasColumnName("ReportID");

                entity.Property(e => e.ReportReason)
                    .HasMaxLength(200)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ReportLogs>(entity =>
            {
                entity.HasKey(e => e.LogId);

                entity.Property(e => e.LogId).HasColumnName("LogID");

                entity.Property(e => e.AnswerId).HasColumnName("AnswerID");

                entity.Property(e => e.QuestionId).HasColumnName("QuestionID");

            

                entity.Property(e => e.ReportReasonId).HasColumnName("ReportReasonID");

                entity.Property(e => e.ReportedByUserId).HasColumnName("ReportedByUserID");

                entity.Property(e => e.ReportedTime).HasColumnType("datetime");

                entity.Property(e => e.ReportedUserId).HasColumnName("ReportedUserID");

                entity.HasOne(d => d.Answer)
                    .WithMany(p => p.ReportLogs)
                    .HasForeignKey(d => d.AnswerId)

                    .HasConstraintName("FK_ReportLogs_Answers");

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.ReportLogs)
                    .HasForeignKey(d => d.QuestionId)
                    .HasConstraintName("FK_ReportLogs_Questions");

          
                entity.HasOne(d => d.ReportReason)
                    .WithMany(p => p.ReportLogs)
                    .HasForeignKey(d => d.ReportReasonId)
                    .HasConstraintName("FK_ReportLogs_Report");

                entity.HasOne(d => d.ReportedByUser)
                    .WithMany(p => p.ReportLogsReportedByUser)
                    .HasForeignKey(d => d.ReportedByUserId)
                    .HasConstraintName("FK_ReportLogs_Reporter");

                entity.HasOne(d => d.ReportedUser)
                    .WithMany(p => p.ReportLogsReportedUser)
                    .HasForeignKey(d => d.ReportedUserId)
                    .HasConstraintName("FK_ReportLogs_ReportedUser");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.Email).IsUnicode(false);

                entity.Property(e => e.PasswordHash).IsUnicode(false);

                entity.Property(e => e.UserName).IsUnicode(false);

                entity.Property(e => e.WarningMessage).IsUnicode(false);
            });

            modelBuilder.Entity<Votes>(entity =>
            {
                entity.HasKey(e => e.VoteId)
                    .HasName("PK__Votes__52F015E269CC46AA");

                entity.Property(e => e.VoteId).HasColumnName("VoteID");

                entity.Property(e => e.AnswerId).HasColumnName("AnswerID");

         

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Answer)
                    .WithMany(p => p.Votes)
                    .HasForeignKey(d => d.AnswerId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__Votes__AnswerID__4CA06362");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Votes)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Votes__UserID__4BAC3F29");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
