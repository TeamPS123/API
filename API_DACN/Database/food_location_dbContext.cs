using System;
using API_DACN.Model.ViewModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace API_DACN.Database
{
    public partial class food_location_dbContext : DbContext
    {

        public food_location_dbContext(DbContextOptions<food_location_dbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<CategoryRestaurant> CategoryRestaurants { get; set; }
        public virtual DbSet<Food> Foods { get; set; }
        public virtual DbSet<Image> Images { get; set; }
        public virtual DbSet<Menu> Menus { get; set; }
        public virtual DbSet<Promotion> Promotions { get; set; }
        public virtual DbSet<Rate> Rates { get; set; }
        public virtual DbSet<ReserveFood> ReserveFoods { get; set; }
        public virtual DbSet<ReserveTable> ReserveTables { get; set; }
        public virtual DbSet<Restaurant> Restaurants { get; set; }
        public virtual DbSet<RestaurantDetail> RestaurantDetails { get; set; }
        public virtual DbSet<Review> Reviews { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserComment> UserComments { get; set; }
        public virtual DbSet<UserLike> UserLikes { get; set; }
        public DbSet<NextIdViewModel> NextIdViewModel { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("devSy")
                .HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category", "dbo");

                entity.Property(e => e.Id)
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .HasDefaultValueSql("([dbo].[AUTO_CategoryID]())");

                entity.Property(e => e.KeyWord)
                    .IsUnicode(false)
                    .HasColumnName("key_word");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<CategoryRestaurant>(entity =>
            {
                entity.ToTable("CategoryRestaurant", "dbo");

                entity.Property(e => e.Icon).HasColumnName("icon");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(256);
            });

            modelBuilder.Entity<Food>(entity =>
            {
                entity.ToTable("Food", "dbo");

                entity.Property(e => e.Id)
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .HasDefaultValueSql("([dbo].[AUTO_FoodID]())");

                entity.Property(e => e.CategoryId)
                    .IsRequired()
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.KeyWord)
                    .IsUnicode(false)
                    .HasColumnName("key_word");

                entity.Property(e => e.MenuId)
                    .IsRequired()
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Unit)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Foods)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Food_Category");

                entity.HasOne(d => d.Menu)
                    .WithMany(p => p.Foods)
                    .HasForeignKey(d => d.MenuId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Food_Menu");
            });

            modelBuilder.Entity<Image>(entity =>
            {
                entity.ToTable("Image", "dbo");

                entity.Property(e => e.CategoryId)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.FoodId)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Link).IsRequired();

                entity.Property(e => e.Path).IsUnicode(false);

                entity.Property(e => e.RestaurantId)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.UserId)
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Menu>(entity =>
            {
                entity.ToTable("Menu", "dbo");

                entity.Property(e => e.Id)
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .HasDefaultValueSql("([dbo].[AUTO_MenuID]())");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.RestaurantId)
                    .IsRequired()
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Restaurant)
                    .WithMany(p => p.Menus)
                    .HasForeignKey(d => d.RestaurantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Menu_Restaurant");
            });

            modelBuilder.Entity<Promotion>(entity =>
            {
                entity.ToTable("Promotion", "dbo");

                entity.Property(e => e.Id)
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .HasDefaultValueSql("([dbo].[AUTO_PromotionID]())");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.RestaurantId)
                    .IsRequired()
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .HasDefaultValueSql("([dbo].[AUTO_PromotionID]())");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.Value)
                    .HasMaxLength(255)
                    .HasColumnName("value");

                entity.HasOne(d => d.Restaurant)
                    .WithMany(p => p.Promotions)
                    .HasForeignKey(d => d.RestaurantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Promotion_Restaurant");
            });

            modelBuilder.Entity<Rate>(entity =>
            {
                entity.ToTable("Rate", "dbo");

                entity.Property(e => e.Content).IsRequired();

                entity.Property(e => e.Date)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.RestaurantId)
                    .IsRequired()
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(7)
                    .IsUnicode(false);

                entity.HasOne(d => d.Restaurant)
                    .WithMany(p => p.Rates)
                    .HasForeignKey(d => d.RestaurantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Rate__Restaurant__6477ECF3");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Rates)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Rate__UserId__656C112C");
            });

            modelBuilder.Entity<ReserveFood>(entity =>
            {
                entity.HasKey(e => new { e.FoodId, e.ReserveTable });

                entity.ToTable("ReserveFood", "dbo");

                entity.Property(e => e.FoodId)
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.ReserveTable)
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.HasOne(d => d.Food)
                    .WithMany(p => p.ReserveFoods)
                    .HasForeignKey(d => d.FoodId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ReserveFood_Food");

                entity.HasOne(d => d.ReserveTableNavigation)
                    .WithMany(p => p.ReserveFoods)
                    .HasForeignKey(d => d.ReserveTable)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ReserveFood_ResrveTable");
            });

            modelBuilder.Entity<ReserveTable>(entity =>
            {
                entity.ToTable("ReserveTable", "dbo");

                entity.Property(e => e.Id)
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .HasDefaultValueSql("([dbo].[AUTO_ReserveTableID]())");

                entity.Property(e => e.Day)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.Note)
                    .HasMaxLength(256)
                    .HasColumnName("note");

                entity.Property(e => e.PhoneNumber).HasMaxLength(15);

                entity.Property(e => e.PromotionId)
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.RestaurantId)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Time)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(7)
                    .IsUnicode(false);

                entity.HasOne(d => d.Promotion)
                    .WithMany(p => p.ReserveTables)
                    .HasForeignKey(d => d.PromotionId)
                    .HasConstraintName("FK_ResrveTable_Promotion");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ReserveTables)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ResrveTable_User");
            });

            modelBuilder.Entity<Restaurant>(entity =>
            {
                entity.ToTable("Restaurant", "dbo");

                entity.Property(e => e.Id)
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .HasDefaultValueSql("([dbo].[AUTO_RestaurantID]())");

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.CloseTime)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.District)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.Line).IsRequired();

                entity.Property(e => e.LongLat)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.Name).IsRequired();

                entity.Property(e => e.OpenTime)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.PhoneRestaurant)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.StatusCo)
                    .HasMaxLength(20)
                    .HasColumnName("statusCO");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(7)
                    .IsUnicode(false);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Restaurants)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Restaurant_User");
            });

            modelBuilder.Entity<RestaurantDetail>(entity =>
            {
                entity.HasKey(e => new { e.RestaurantId, e.CategoryId })
                    .HasName("PK__Restaura__26D5DF352072BB60");

                entity.ToTable("RestaurantDetails", "dbo");

                entity.Property(e => e.RestaurantId)
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.RestaurantDetails)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RestaurantDetails_CategoryRestaurant");

                entity.HasOne(d => d.Restaurant)
                    .WithMany(p => p.RestaurantDetails)
                    .HasForeignKey(d => d.RestaurantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RestaurantDetails_Restaurant");
            });

            modelBuilder.Entity<Review>(entity =>
            {
                entity.ToTable("Review", "pamlelr98");

                entity.Property(e => e.Content).IsRequired();

                entity.Property(e => e.Date).HasMaxLength(20);

                entity.Property(e => e.RestaurantId)
                    .IsRequired()
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(7)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User", "dbo");

                entity.Property(e => e.Id)
                    .HasMaxLength(7)
                    .IsUnicode(false)
                    .HasDefaultValueSql("([dbo].[AUTO_UserID]())");

                entity.Property(e => e.Gender)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IsBusiness).HasColumnName("isBusiness");

                entity.Property(e => e.PassswordHash)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<UserComment>(entity =>
            {
                entity.ToTable("UserComment", "pamlelr98");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Content).IsRequired();

                entity.Property(e => e.Date)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("date");

                entity.Property(e => e.ReviewId).HasColumnName("reviewId");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(7)
                    .IsUnicode(false)
                    .HasColumnName("userId");

                entity.HasOne(d => d.Review)
                    .WithMany(p => p.UserComments)
                    .HasForeignKey(d => d.ReviewId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UserComme__revie__7F2BE32F");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserComments)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UserComme__userI__00200768");
            });

            modelBuilder.Entity<UserLike>(entity =>
            {
                entity.ToTable("UserLike", "pamlelr98");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.ReviewId).HasColumnName("reviewId");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(7)
                    .IsUnicode(false)
                    .HasColumnName("userId");

                entity.HasOne(d => d.Review)
                    .WithMany(p => p.UserLikes)
                    .HasForeignKey(d => d.ReviewId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UserLike__review__7B5B524B");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserLikes)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UserLike__userId__7C4F7684");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
