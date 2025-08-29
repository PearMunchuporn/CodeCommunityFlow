using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeCommunityFlow.DomainModels
{

   
    public class CodeCommunityFlowDbContext:DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // ReportLogs → ReportedByUser
            modelBuilder.Entity<ReportLogs>()
                .HasOptional(r => r.ReportedByUser)
                .WithMany(u => u.ReportLogsReportedByUser)
                .HasForeignKey(r => r.ReportedByUserID)
                .WillCascadeOnDelete(false);

            // ReportLogs → ReportedUser
            modelBuilder.Entity<ReportLogs>()
                .HasOptional(r => r.ReportedUser)
                .WithMany(u => u.ReportLogsReportedUser)
                .HasForeignKey(r => r.ReportedUserID)
                .WillCascadeOnDelete(false);

            // ReportLogs → Questions
            modelBuilder.Entity<ReportLogs>()
                .HasOptional(r => r.Question)
                .WithMany(q => q.ReportLogs)
                .HasForeignKey(r => r.QuestionID)
                .WillCascadeOnDelete(false);

            // ReportLogs → Answers
            modelBuilder.Entity<ReportLogs>()
                .HasOptional(r => r.Answer)
                .WithMany(a => a.ReportLogs)
                .HasForeignKey(r => r.AnswerID)
                .WillCascadeOnDelete(false);


            // AdminAnnouncement -> CommentFromAnnouncement (Cascade)
            modelBuilder.Entity<CommentFromAnnouncement>()
                .HasRequired(c => c.Announcement)
                .WithMany(a => a.CommentFromAnnouncement)
                .HasForeignKey(c => c.AdminAnnouncementID)
                .WillCascadeOnDelete(true); 


        

        }

        public CodeCommunityFlowDbContext()
        {
            this.Configuration.LazyLoadingEnabled = true;
        }

        public DbSet<Categories> Categories { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<Answers> Answers { get; set; }
        public DbSet<Votes> Votes { get; set; }
        public DbSet<Questions> Questions { get; set; }
        public DbSet<HowKnowUs> HowKnowUs { get; set; }
        public DbSet<Contact> ContactUS { get; set; }
       
        public DbSet<Report> Report { get; set; }
        public DbSet<ReportLogs> ReportLogs { get; set; }
        public DbSet<DeleteLogs> DeleteLogs { get; set; }
        public DbSet<ScoreHistoryLogs> ScoreHistoryLogs { get; set; }
        public DbSet<ThanksLogs> ThanksLogs { get; set; }
        public DbSet<AdminUsers> AdminUsers { get; set; }
        public DbSet<AdminAnnouncement> AdminAnnouncements { get; set; }
        public DbSet<CommentFromAnnouncement> CommentFromAnnouncement { get; set; }
        public DbSet<AdminActionLogs> AdminActionLogs { get; set; }


    }
   
}
