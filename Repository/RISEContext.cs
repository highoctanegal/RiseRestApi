using Microsoft.EntityFrameworkCore;
using RiseRestApi.Models;

namespace RiseRestApi.Repository
{
    public partial class RiseContext : DbContext
    {
        public RiseContext()
        {
        }

        public RiseContext(DbContextOptions<RiseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Address> Address { get; set; }
        public virtual DbSet<Assessment> Assessment { get; set; }
        public virtual DbSet<AssessmentResponse> AssessmentResponse { get; set; }
        public virtual DbSet<AssessmentStatus> AssessmentStatus { get; set; }
        public virtual DbSet<Coach> Coach { get; set; }
        public virtual DbSet<InterventionType> InterventionType { get; set; }
        public virtual DbSet<Note> Note { get; set; }
        public virtual DbSet<Organization> Organization { get; set; }
        public virtual DbSet<Person> Person { get; set; }
        public virtual DbSet<PersonRole> PersonRole { get; set; }
        public virtual DbSet<Question> Question { get; set; }
        public virtual DbSet<Rating> Rating { get; set; }
        public virtual DbSet<RiseProgram> Program { get; set; }
        public virtual DbSet<SkillSet> SkillSet { get; set; }
        public virtual DbSet<Survey> Survey { get; set; }
        public virtual DbSet<SurveyQuestion> SurveyQuestion { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=rise-sql-server.database.windows.net;Initial Catalog=rise-sql-db;User Id=ken.knecht;Password=4ZJZ7AmsIY32");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>(entity =>
            {

            });
            
            modelBuilder.Entity<Assessment>(entity =>
            {
                entity.HasIndex(e => e.AssessingPersonId)
                    .HasName("IX_Assessment_AssessingPerson");

                entity.HasIndex(e => e.CoachPersonId)
                    .HasName("IX_Assessment_CoachPerson");

                entity.HasIndex(e => e.NoteId)
                    .HasName("IX_Assessment_Note");

                entity.HasIndex(e => e.RegardingPersonId)
                    .HasName("IX_Assessment_RegardingPerson");

                entity.Property(e => e.LastUpdateDate).HasColumnType("datetime");

                entity.Property(e => e.ReviewDate).HasColumnType("datetime");

                entity.Property(e => e.SubmitDate).HasColumnType("datetime");

                entity.HasOne(d => d.AssessingPerson)
                    .WithMany(p => p.AssessmentAssessingPerson)
                    .HasForeignKey(d => d.AssessingPersonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Assessment_AssessingPerson");

                entity.HasOne(d => d.AssessmentStatus)
                    .WithMany(p => p.Assessment)
                    .HasForeignKey(d => d.AssessmentStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Assessment_AssessmentStatus");

                entity.HasOne(d => d.CoachPerson)
                    .WithMany(p => p.AssessmentCoachPerson)
                    .HasForeignKey(d => d.CoachPersonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Assessment_CoachPerson");

                entity.HasOne(d => d.Note)
                    .WithMany(p => p.Assessment)
                    .HasForeignKey(d => d.NoteId)
                    .HasConstraintName("FK_Assessment_Note");

                entity.HasOne(d => d.RegardingPerson)
                    .WithMany(p => p.AssessmentRegardingPerson)
                    .HasForeignKey(d => d.RegardingPersonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Assessment_RegardingPerson");

                entity.HasOne(d => d.Survey)
                    .WithMany(p => p.Assessment)
                    .HasForeignKey(d => d.SurveyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Assessment_Survey");
            });

            modelBuilder.Entity<AssessmentResponse>(entity =>
            {
                entity.HasIndex(e => e.AssessmentId)
                    .HasName("IX_AssessmentResponse_Assessment");

                entity.HasIndex(e => e.NoteId)
                    .HasName("IX_AssessmentResponse_Note");

                entity.HasIndex(e => e.RatingId)
                    .HasName("IX_AssessmentResponse_Rating");

                entity.HasOne(d => d.Assessment)
                    .WithMany(p => p.AssessmentResponse)
                    .HasForeignKey(d => d.AssessmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AssessmentResponse_Assessment");

                entity.HasOne(d => d.Note)
                    .WithMany(p => p.AssessmentResponse)
                    .HasForeignKey(d => d.NoteId)
                    .HasConstraintName("FK_AssessmentResponse_Note");

                entity.HasOne(d => d.Rating)
                    .WithMany(p => p.AssessmentResponse)
                    .HasForeignKey(d => d.RatingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AssessmentResponse_QuestionRating");
            });

            modelBuilder.Entity<AssessmentStatus>(entity =>
            {
                entity.ToTable("AssessmentStatus", "system_type");

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.Text)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<InterventionType>(entity =>
            {
                entity.ToTable("InterventionType", "system_type");

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.Text)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Note>(entity =>
            {
                entity.HasIndex(e => e.AuthorPersonId)
                    .HasName("IX_Note_AuthorPerson");

                entity.HasIndex(e => e.ReferencePersonId)
                    .HasName("IX_Note_ReferencePerson");

                entity.Property(e => e.NoteDate).HasColumnType("datetime");

                entity.Property(e => e.NoteText)
                    .IsRequired()
                    .IsUnicode(false);

                entity.HasOne(d => d.AuthorPerson)
                    .WithMany(p => p.NoteAuthorPerson)
                    .HasForeignKey(d => d.AuthorPersonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Note_AuthorPerson");

                entity.HasOne(d => d.ReferencePerson)
                    .WithMany(p => p.NoteReferencePerson)
                    .HasForeignKey(d => d.ReferencePersonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Note_ReferencePerson");
            });

            modelBuilder.Entity<Organization>(entity =>
            {
                entity.Property(e => e.OrganizationName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Admin)
                    .WithMany(p => p.Organizations)
                    .HasForeignKey(d => d.AdminPersonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Organization_AdminPerson");
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(p => p.Address);

                entity.HasOne(p => p.CurrentOrganization);
            });

            modelBuilder.Entity<PersonProgram>(entity =>
            {
                entity.Property(e => e.PersonId)
                    .IsRequired();
                entity.Property(e => e.ProgramId)
                    .IsRequired();
                entity.HasOne(d => d.Person)
                    .WithMany(p => p.PersonPrograms)
                    .HasForeignKey(d => d.PersonProgramId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PersonProgram_Person");
                entity.HasOne(d => d.Program)
                    .WithMany(p => p.PersonPrograms)
                    .HasForeignKey(d => d.PersonProgramId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PersonProgram_Program");
            });

            modelBuilder.Entity<PersonRole>(entity =>
            {
                entity.Property(e => e.PersonId)
                    .IsRequired();
                entity.Property(e => e.RoleId)
                    .IsRequired();
                entity.HasOne(d => d.Person)
                    .WithMany(p => p.PersonRoles)
                    .HasForeignKey(d => d.PersonRoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PersonRole_Person");
                entity.HasOne(d => d.Role)
                    .WithMany(p => p.PersonRoles)
                    .HasForeignKey(d => d.PersonRoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PersonRole_Role");
            });

            modelBuilder.Entity<RiseProgram>(entity =>
            {
                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.ProgramName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Organization)
                    .WithMany(p => p.Programs)
                    .HasForeignKey(d => d.OrganizationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Program_Organization");
            });

            modelBuilder.Entity<Question>(entity =>
            {
                entity.Property(e => e.QuestionDescription).IsUnicode(false);

                entity.Property(e => e.QuestionText)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Rating>(entity =>
            {
                entity.HasIndex(e => e.QuestionId)
                    .HasName("IX_Rating_Question");

                entity.Property(e => e.RatingText).IsUnicode(false);

                entity.Property(e => e.RatingValue)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.Rating)
                    .HasForeignKey(d => d.QuestionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Rating_Question");
            });

            modelBuilder.Entity<SkillSet>(entity =>
            {
                entity.Property(e => e.SkillSetName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Survey>(entity =>
            {
                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.SurveyName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SurveyQuestion>(entity =>
            {
                entity.HasIndex(e => e.SurveyQuestionId)
                    .HasName("IX_SurveyQuestion_Question");

                entity.HasIndex(e => e.SurveyId)
                    .HasName("IX_SurveyQuestion_Survey");

                entity.HasIndex(e => e.SkillSetId)
                    .HasName("IX_SurveyQuestion_SkillSet");

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.SurveyQuestion)
                    .HasForeignKey(d => d.QuestionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SurveyQuestion_Question");

                entity.HasOne(d => d.SkillSet)
                    .WithMany(p => p.SurveyQuestion)
                    .HasForeignKey(d => d.SkillSetId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SurveyQuestion_SkillSet");

                entity.HasOne(d => d.Survey)
                    .WithMany(p => p.SurveyQuestion)
                    .HasForeignKey(d => d.SurveyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SurveyQuestion_Survey");
            });
            
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
