using IDonEnglist.Domain;
using IDonEnglist.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace IDonEnglist.Persistence
{
    public class IDonEnglistDBContext : DbContext
    {
        public IDonEnglistDBContext(DbContextOptions<IDonEnglistDBContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Answer>()
                .HasOne(a => a.SampleAudio)
                .WithOne(a => a.Answer)
                .HasForeignKey<Answer>(a => a.SampleAudioId)
                .IsRequired(false);

            modelBuilder.Entity<Category>()
                .HasOne(a => a.Parent)
                .WithMany(a => a.Children)
                .HasForeignKey(a => a.ParentId)
                .IsRequired(false);

            modelBuilder.Entity<Collection>()
                .HasOne(a => a.Thumbnail)
                .WithOne(a => a.Collection)
                .HasForeignKey<Collection>(a => a.ThumbnailId)
                .IsRequired(false);

            modelBuilder.Entity<Test>()
                .HasOne(a => a.Audio)
                .WithOne(a => a.Test)
                .HasForeignKey<Test>(a => a.AudioId)
                .IsRequired(false);

            modelBuilder.Entity<UserAnswer>()
                .HasOne(a => a.AnswerAudio)
                .WithOne(a => a.UserAnswer)
                .HasForeignKey<UserAnswer>(a => a.AnswerAudioId)
                .IsRequired(false);

            modelBuilder.Entity<Passage>()
                .Property(a => a.Content)
                .HasColumnType("text");

            modelBuilder.Entity<QuestionGroupMedia>()
                .HasOne(a => a.Media)
                .WithOne(a => a.QuestionGroupMedia)
                .HasForeignKey<QuestionGroupMedia>(a => a.MediaId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<QuestionGroupMedia>()
                .HasOne(a => a.QuestionGroup)
                .WithMany(a => a.QuestionGroupMedias)
                .HasForeignKey(a => a.QuestionGroupId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TestSectionTakenHistory>()
                .HasOne(a => a.TestPartTakenHistory)
                .WithMany(a => a.Sections)
                .HasForeignKey(a => a.TestPartTakenHistoryId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TestSectionTakenHistory>()
                .HasOne(a => a.TestSection)
                .WithMany(a => a.TestSectionTakenHistories)
                .HasForeignKey(a => a.TestSectionId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserAnswer>()
                .HasOne(a => a.TestSectionTakenHistory)
                .WithMany(a => a.UserAnswers)
                .HasForeignKey(a => a.TestSectionTakenHistoryId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Permission>()
                .HasIndex(a => a.Name).IsUnique();

            modelBuilder.Entity<Permission>()
                .HasOne(a => a.Parent)
                .WithMany(a => a.Children)
                .HasForeignKey(a => a.ParentId)
                .IsRequired(false);

            modelBuilder.Entity<TestType>()
                .HasOne(a => a.CategorySkill)
                .WithMany(a => a.TestTypes)
                .HasForeignKey(a => a.CategorySkillId)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.ApplyConfigurationsFromAssembly(typeof(IDonEnglistDBContext).Assembly);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<BaseDomainEntity>())
            {
                entry.Entity.UpdatedDate = DateTime.Now;

                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedDate = DateTime.Now;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }

        public DbSet<Answer> Answers { get; set; }
        public DbSet<AnswerChoice> AnswerChoices { get; set; }
        public DbSet<AuthProvider> AuthProviders { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CategorySkill> CategorySkills { get; set; }
        public DbSet<Collection> Collections { get; set; }
        public DbSet<FinalTest> FinalTests { get; set; }
        public DbSet<Media> Medias { get; set; }
        public DbSet<Passage> Passages { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<QuestionGroup> QuestionGroups { get; set; }
        public DbSet<QuestionGroupMedia> QuestionGroupMedias { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<Test> Tests { get; set; }
        public DbSet<TestPart> TestParts { get; set; }
        public DbSet<TestPartTakenHistory> TestPartTakenHistories { get; set; }
        public DbSet<TestSection> TestSections { get; set; }
        public DbSet<TestSectionTakenHistory> TestSectionTakenHistories { get; set; }
        public DbSet<TestTakenHistory> TestTakenHistories { get; set; }
        public DbSet<TestType> TestTypes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserAnswer> UserAnswers { get; set; }
        public DbSet<UserSocialAccount> UserSocialAccounts { get; set; }
    }
}
