using System;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.EFCore.Data
{
    public partial class db_cubicall_game_devContext : DbContext
    {
        public db_cubicall_game_devContext()
        {
        }

        public db_cubicall_game_devContext(DbContextOptions<db_cubicall_game_devContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TblAvatar> TblAvatar { get; set; }
        public virtual DbSet<TblBadgeMaster> TblBadgeMaster { get; set; }
        public virtual DbSet<TblBusinessType> TblBusinessType { get; set; }
        public virtual DbSet<TblCmsRoleFunction> TblCmsRoleFunction { get; set; }
        public virtual DbSet<TblCmsRoles> TblCmsRoles { get; set; }
        public virtual DbSet<TblCmsUsers> TblCmsUsers { get; set; }
        public virtual DbSet<TblCouponsredeemed> TblCouponsredeemed { get; set; }
        public virtual DbSet<TblCubefaceBatchMaster> TblCubefaceBatchMaster { get; set; }
        public virtual DbSet<TblCubesCrosswordGridDetails> TblCubesCrosswordGridDetails { get; set; }
        public virtual DbSet<TblCubesFaceBadgeMaster> TblCubesFaceBadgeMaster { get; set; }
        public virtual DbSet<TblCubesFacesAttemptnoDetails> TblCubesFacesAttemptnoDetails { get; set; }
        public virtual DbSet<TblCubesFacesColourDetails> TblCubesFacesColourDetails { get; set; }
        public virtual DbSet<TblCubesFacesGameDetails> TblCubesFacesGameDetails { get; set; }
        public virtual DbSet<TblCubesFacesGameMappingwithHierarchy> TblCubesFacesGameMappingwithHierarchy { get; set; }
        public virtual DbSet<TblCubesFacesMaster> TblCubesFacesMaster { get; set; }
        public virtual DbSet<TblCubesGameDetailsUserLog> TblCubesGameDetailsUserLog { get; set; }
        public virtual DbSet<TblCubesGameMasterUserLog> TblCubesGameMasterUserLog { get; set; }
        public virtual DbSet<TblCubesGridrowcolLog> TblCubesGridrowcolLog { get; set; }
        public virtual DbSet<TblCubesPertilesAnswerDetails> TblCubesPertilesAnswerDetails { get; set; }
        public virtual DbSet<TblCubesPertilesQuestionDetails> TblCubesPertilesQuestionDetails { get; set; }
        public virtual DbSet<TblCubesQuestionMappingwithHierarchy> TblCubesQuestionMappingwithHierarchy { get; set; }
        public virtual DbSet<TblFacesAttemptnoMappingwithHierarchy> TblFacesAttemptnoMappingwithHierarchy { get; set; }
        public virtual DbSet<TblGameIdMaster> TblGameIdMaster { get; set; }
        public virtual DbSet<TblGameMaster> TblGameMaster { get; set; }
        public virtual DbSet<TblGameMissionsRulesMaster> TblGameMissionsRulesMaster { get; set; }
        public virtual DbSet<TblGameMissionsUserLog> TblGameMissionsUserLog { get; set; }
        public virtual DbSet<TblGameWellcomeMsg> TblGameWellcomeMsg { get; set; }
        public virtual DbSet<TblHeirarchyBatchesMaster> TblHeirarchyBatchesMaster { get; set; }
        public virtual DbSet<TblIndustry> TblIndustry { get; set; }
        public virtual DbSet<TblLoginSecurityQuestion> TblLoginSecurityQuestion { get; set; }
        public virtual DbSet<TblOrganization> TblOrganization { get; set; }
        public virtual DbSet<TblOrganizationHierarchy> TblOrganizationHierarchy { get; set; }
        public virtual DbSet<TblOrganizationtype> TblOrganizationtype { get; set; }
        public virtual DbSet<TblRewardsRedeemMaster> TblRewardsRedeemMaster { get; set; }
        public virtual DbSet<TblRoleCmsUserMapping> TblRoleCmsUserMapping { get; set; }
        public virtual DbSet<TblServiceLevelBandsDetails> TblServiceLevelBandsDetails { get; set; }
        public virtual DbSet<TblServiceLevelScoringDetails> TblServiceLevelScoringDetails { get; set; }
        public virtual DbSet<TblServiceLevelStreakScoringDetails> TblServiceLevelStreakScoringDetails { get; set; }
        public virtual DbSet<TblUserSecurityQuestionLog> TblUserSecurityQuestionLog { get; set; }
        public virtual DbSet<TblUsers> TblUsers { get; set; }
        public virtual DbSet<TblUsersLog> TblUsersLog { get; set; }
        public virtual DbSet<TblUsersMappingwithBatchid> TblUsersMappingwithBatchid { get; set; }
        public virtual DbSet<TblUsersMappingwithHierarchy> TblUsersMappingwithHierarchy { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

                optionsBuilder.UseMySql("server=13.127.109.239;port=3306;user=tgc_rani_db;password=hQVUH#4G;database=db_cubicall_game_dev", x => x.ServerVersion("5.7.34-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TblAvatar>(entity =>
            {
                entity.HasKey(e => e.IdAvatar)
                    .HasName("PRIMARY");

                entity.ToTable("tbl_avatar");

                entity.Property(e => e.IdAvatar)
                    .HasColumnName("Id_Avatar")
                    .HasColumnType("int(6)");

                entity.Property(e => e.AvatarVoice)
                    .HasColumnName("Avatar_Voice")
                    .HasColumnType("varchar(200)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.IdCmsUser)
                    .HasColumnName("Id_CmsUser")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdOrganization)
                    .HasColumnName("ID_ORGANIZATION")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasColumnType("char(1)")
                    .HasDefaultValueSql("'A'")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.UpdatedDateTime)
                    .HasColumnName("Updated_Date_Time")
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .ValueGeneratedOnAddOrUpdate();

                entity.Property(e => e.Url)
                    .HasColumnName("url")
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<TblBadgeMaster>(entity =>
            {
                entity.HasKey(e => e.IdMasterBadge)
                    .HasName("PRIMARY");

                entity.ToTable("tbl_badge_master");

                entity.Property(e => e.IdMasterBadge)
                    .HasColumnName("Id_Master_Badge")
                    .HasColumnType("int(6)");

                entity.Property(e => e.BadgeImgUrl)
                    .HasColumnName("Badge_Img_URL")
                    .HasColumnType("varchar(500)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.BadgeName)
                    .HasColumnType("varchar(500)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.IdOrganization)
                    .HasColumnName("ID_ORGANIZATION")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasColumnType("char(1)")
                    .HasDefaultValueSql("'A'")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.UpdatedDateTime)
                    .HasColumnName("Updated_Date_Time")
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .ValueGeneratedOnAddOrUpdate();
            });

            modelBuilder.Entity<TblBusinessType>(entity =>
            {
                entity.HasKey(e => e.IdBusinessType)
                    .HasName("PRIMARY");

                entity.ToTable("tbl_business_type");

                entity.HasIndex(e => e.BusinessTypeName)
                    .HasName("BUSINESS_TYPE_NAME_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.IdBusinessType)
                    .HasName("ID_BUSINESS_TYPE_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.IdBusinessType)
                    .HasColumnName("ID_BUSINESS_TYPE")
                    .HasColumnType("int(11)");

                entity.Property(e => e.BusinessTypeName)
                    .IsRequired()
                    .HasColumnName("BUSINESS_TYPE_NAME")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Description)
                    .HasColumnName("DESCRIPTION")
                    .HasColumnType("varchar(200)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasColumnName("STATUS")
                    .HasColumnType("char(1)")
                    .HasDefaultValueSql("'A'")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.UpdatedDateTime)
                    .HasColumnName("UPDATED_DATE_TIME")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<TblCmsRoleFunction>(entity =>
            {
                entity.HasKey(e => e.IdFunction)
                    .HasName("PRIMARY");

                entity.ToTable("tbl_cms_role_function");

                entity.Property(e => e.IdFunction)
                    .HasColumnName("Id_Function")
                    .HasColumnType("int(6)");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasColumnType("varchar(500)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.FunctionName)
                    .HasColumnName("Function_Name")
                    .HasColumnType("varchar(500)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.IdOrganization)
                    .HasColumnName("ID_ORGANIZATION")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasColumnType("char(1)")
                    .HasDefaultValueSql("'A'")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.UpdatedDateTime)
                    .HasColumnName("Updated_Date_Time")
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .ValueGeneratedOnAddOrUpdate();
            });

            modelBuilder.Entity<TblCmsRoles>(entity =>
            {
                entity.HasKey(e => e.IdCmsRole)
                    .HasName("PRIMARY");

                entity.ToTable("tbl_cms_roles");

                entity.HasIndex(e => e.IdOrganization)
                    .HasName("fk1_tbl_cms_roles_idx");

                entity.Property(e => e.IdCmsRole)
                    .HasColumnName("Id_CMS_Role")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Description)
                    .HasColumnType("varchar(200)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.IdOrganization)
                    .HasColumnName("ID_ORGANIZATION")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdsFunction)
                    .HasColumnName("Ids_Function")
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.RoleName)
                    .IsRequired()
                    .HasColumnName("Role_Name")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasColumnType("char(1)")
                    .HasDefaultValueSql("'A'")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.UpdatedDateTime)
                    .HasColumnName("Updated_Date_time")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<TblCmsUsers>(entity =>
            {
                entity.HasKey(e => e.IdCmsUser)
                    .HasName("PRIMARY");

                entity.ToTable("tbl_cms_users");

                entity.HasIndex(e => e.IdOrganization)
                    .HasName("ID_ORGANIZATION");

                entity.Property(e => e.IdCmsUser)
                    .HasColumnName("Id_CmsUser")
                    .HasColumnType("int(6)");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.EmployeeId)
                    .HasColumnName("Employee_ID")
                    .HasColumnType("varchar(300)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.IdCmsRole)
                    .HasColumnName("Id_CMS_Role")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdOrgHierarchy)
                    .HasColumnName("Id_Org_hierarchy")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdOrganization)
                    .HasColumnName("ID_ORGANIZATION")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Password)
                    .HasColumnType("varchar(30)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.PhoneNo)
                    .HasColumnName("Phone_No")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasColumnType("char(1)")
                    .HasDefaultValueSql("'A'")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.UpdatedDateTime)
                    .HasColumnName("Updated_Date_Time")
                    .HasColumnType("datetime");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasColumnName("User_Name")
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.HasOne(d => d.IdOrganizationNavigation)
                    .WithMany(p => p.TblCmsUsers)
                    .HasForeignKey(d => d.IdOrganization)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("tbl_cms_usersfk_1");
            });

            modelBuilder.Entity<TblCouponsredeemed>(entity =>
            {
                entity.HasKey(e => e.IdCouponsRedeemed)
                    .HasName("PRIMARY");

                entity.ToTable("tbl_couponsredeemed");

                entity.Property(e => e.IdCouponsRedeemed)
                    .HasColumnName("id_CouponsRedeemed")
                    .HasColumnType("int(6)");

                entity.Property(e => e.CardType)
                    .HasColumnName("Card_type")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CouponCode)
                    .HasColumnType("varchar(200)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.CouponDescription)
                    .HasColumnType("varchar(200)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.CouponId)
                    .HasColumnName("CouponID")
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.CouponTitle)
                    .HasColumnType("varchar(200)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.ExpiryDate)
                    .HasColumnType("varchar(200)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.IdOrganization)
                    .HasColumnName("id_organization")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdRewards)
                    .HasColumnName("id_rewards")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdUser)
                    .HasColumnName("id_user")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Image)
                    .HasColumnType("varchar(200)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Link)
                    .HasColumnType("varchar(300)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.PointsUsed)
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasColumnName("status")
                    .HasColumnType("char(1)")
                    .HasDefaultValueSql("'A'")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.UpdatedDateTime)
                    .HasColumnName("Updated_Date_Time")
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .ValueGeneratedOnAddOrUpdate();

                entity.Property(e => e.WebsiteName)
                    .HasColumnType("varchar(200)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<TblCubefaceBatchMaster>(entity =>
            {
                entity.HasKey(e => e.CubefaceBatchId)
                    .HasName("PRIMARY");

                entity.ToTable("tbl_cubeface_batch_master");

                entity.Property(e => e.CubefaceBatchId)
                    .HasColumnName("cubeface_batch_Id")
                    .HasColumnType("int(6)");

                entity.Property(e => e.CubesFacesId)
                    .HasColumnName("Cubes_Faces_Id")
                    .HasColumnType("int(6)");

                entity.Property(e => e.IdBatch)
                    .HasColumnName("Id_Batch")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasColumnType("char(1)")
                    .HasDefaultValueSql("'A'")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.UpdatedDateTime)
                    .HasColumnName("Updated_Date_Time")
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .ValueGeneratedOnAddOrUpdate();
            });

            modelBuilder.Entity<TblCubesCrosswordGridDetails>(entity =>
            {
                entity.HasKey(e => e.IdGrid)
                    .HasName("PRIMARY");

                entity.ToTable("tbl_cubes_crossword_grid_details");

                entity.Property(e => e.IdGrid)
                    .HasColumnName("Id_Grid")
                    .HasColumnType("int(6)");

                entity.Property(e => e.Grid)
                    .HasColumnType("varchar(500)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.IdCmsUser)
                    .HasColumnName("Id_CmsUser")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdOrganization)
                    .HasColumnName("ID_ORGANIZATION")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasColumnType("char(1)")
                    .HasDefaultValueSql("'A'")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.KeyNo)
                    .HasColumnName("Key_No")
                    .HasColumnType("int(11)");

                entity.Property(e => e.UpdatedDateTime)
                    .HasColumnName("Updated_Date_Time")
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .ValueGeneratedOnAddOrUpdate();
            });

            modelBuilder.Entity<TblCubesFaceBadgeMaster>(entity =>
            {
                entity.HasKey(e => e.BadgeId)
                    .HasName("PRIMARY");

                entity.ToTable("tbl_cubes_face_badge_master");

                entity.Property(e => e.BadgeId)
                    .HasColumnName("Badge_Id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.BadgeImgUrl)
                    .HasColumnName("Badge_Img_URL")
                    .HasColumnType("varchar(200)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.BadgeName)
                    .HasColumnName("Badge_Name")
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.BadgePoints)
                    .HasColumnName("Badge_Points")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CubesFaceCompleteTime)
                    .HasColumnName("Cubes_FaceCompleteTime")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CubesFacesGameId)
                    .HasColumnName("Cubes_Faces_Game_Id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdGame)
                    .HasColumnName("Id_Game")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdMasterBadge)
                    .HasColumnName("Id_Master_Badge")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdOrganization)
                    .HasColumnName("ID_ORGANIZATION")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasColumnType("char(1)")
                    .HasDefaultValueSql("'A'")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.UpdatedDateTime)
                    .HasColumnName("updated_date_time")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<TblCubesFacesAttemptnoDetails>(entity =>
            {
                entity.HasKey(e => e.AttemptNoId)
                    .HasName("PRIMARY");

                entity.ToTable("tbl_cubes_faces_attemptno_details");

                entity.Property(e => e.AttemptNoId)
                    .HasColumnName("AttemptNo_Id")
                    .HasColumnType("int(6)");

                entity.Property(e => e.AttemptNo).HasColumnType("int(2)");

                entity.Property(e => e.AttemptNoMapId)
                    .HasColumnName("AttemptNo_Map_Id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CubesFacesId)
                    .HasColumnName("Cubes_Faces_Id")
                    .HasColumnType("int(6)");

                entity.Property(e => e.GamePoints)
                    .HasColumnName("Game_Points")
                    .HasColumnType("varchar(2)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.IdOrganization)
                    .HasColumnName("ID_ORGANIZATION")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasColumnType("char(1)")
                    .HasDefaultValueSql("'A'")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.UpdatedDateTime)
                    .HasColumnName("Updated_Date_Time")
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .ValueGeneratedOnAddOrUpdate();
            });

            modelBuilder.Entity<TblCubesFacesColourDetails>(entity =>
            {
                entity.HasKey(e => e.ColourId)
                    .HasName("PRIMARY");

                entity.ToTable("tbl_cubes_faces_colour_details");

                entity.Property(e => e.ColourId)
                    .HasColumnName("Colour_Id")
                    .HasColumnType("int(6)");

                entity.Property(e => e.Colour)
                    .HasColumnType("varchar(200)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.CubesFacesId)
                    .HasColumnName("Cubes_Faces_Id")
                    .HasColumnType("int(6)");

                entity.Property(e => e.IdOrganization)
                    .HasColumnName("ID_ORGANIZATION")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasColumnType("char(1)")
                    .HasDefaultValueSql("'A'")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.UpdatedDateTime)
                    .HasColumnName("Updated_Date_Time")
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .ValueGeneratedOnAddOrUpdate();

                entity.Property(e => e.WaitTime)
                    .HasColumnName("Wait_Time")
                    .HasColumnType("int(11)");
            });

            modelBuilder.Entity<TblCubesFacesGameDetails>(entity =>
            {
                entity.HasKey(e => e.CubesFacesGameId)
                    .HasName("PRIMARY");

                entity.ToTable("tbl_cubes_faces_game_details");

                entity.Property(e => e.CubesFacesGameId)
                    .HasColumnName("Cubes_Faces_Game_Id")
                    .HasColumnType("int(6)");

                entity.Property(e => e.CategoryName)
                    .IsRequired()
                    .HasColumnName("Category_Name")
                    .HasColumnType("varchar(20)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.CubesFacesId)
                    .HasColumnName("Cubes_Faces_Id")
                    .HasColumnType("int(6)");

                entity.Property(e => e.CubesFacesMapId)
                    .HasColumnName("Cubes_Faces_Map_Id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.FaceGameIntro)
                    .HasColumnName("Face_Game_Intro")
                    .HasColumnType("varchar(3000)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.FaceGameIntroVedio)
                    .HasColumnName("Face_Game_Intro_Vedio")
                    .HasColumnType("varchar(200)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.GameAttemptNo)
                    .HasColumnName("Game_Attempt_No")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdOrganization)
                    .HasColumnName("ID_ORGANIZATION")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasColumnType("char(1)")
                    .HasDefaultValueSql("'A'")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.OverAllTimer)
                    .HasColumnName("OverAll_Timer")
                    .HasColumnType("int(6)");

                entity.Property(e => e.PerTileTimer)
                    .HasColumnName("Per_Tile_Timer")
                    .HasColumnType("int(6)");

                entity.Property(e => e.UpdatedDateTime)
                    .HasColumnName("Updated_Date_Time")
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .ValueGeneratedOnAddOrUpdate();
            });

            modelBuilder.Entity<TblCubesFacesGameMappingwithHierarchy>(entity =>
            {
                entity.HasKey(e => e.MapFacesGid)
                    .HasName("PRIMARY");

                entity.ToTable("tbl_cubes_faces_game_mappingwith_hierarchy");

                entity.Property(e => e.MapFacesGid)
                    .HasColumnName("Map_Faces_GId")
                    .HasColumnType("int(6)");

                entity.Property(e => e.CubesFacesMapId)
                    .HasColumnName("Cubes_Faces_Map_Id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdOrgHierarchy)
                    .HasColumnName("Id_Org_hierarchy")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdOrganization)
                    .HasColumnName("ID_ORGANIZATION")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasColumnType("char(1)")
                    .HasDefaultValueSql("'A'")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.UpdatedDateTime)
                    .HasColumnName("Updated_Date_Time")
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .ValueGeneratedOnAddOrUpdate();
            });

            modelBuilder.Entity<TblCubesFacesMaster>(entity =>
            {
                entity.HasKey(e => e.CubesFacesId)
                    .HasName("PRIMARY");

                entity.ToTable("tbl_cubes_faces_master");

                entity.Property(e => e.CubesFacesId)
                    .HasColumnName("Cubes_Faces_Id")
                    .HasColumnType("int(6)");

                entity.Property(e => e.Color)
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.FacesName)
                    .IsRequired()
                    .HasColumnName("Faces_Name")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasColumnType("char(1)")
                    .HasDefaultValueSql("'A'")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.UpdatedDateTime)
                    .HasColumnName("Updated_Date_Time")
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .ValueGeneratedOnAddOrUpdate();
            });

            modelBuilder.Entity<TblCubesGameDetailsUserLog>(entity =>
            {
                entity.HasKey(e => e.IdLog)
                    .HasName("PRIMARY");

                entity.ToTable("tbl_cubes_game_details_user_log");

                entity.Property(e => e.IdLog)
                    .HasColumnName("id_log")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Aht)
                    .HasColumnName("AHT")
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.AttemptNo)
                    .HasColumnName("attempt_no")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CallsWaiting)
                    .HasColumnName("Calls_Waiting")
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.CubesFacesGameId)
                    .HasColumnName("Cubes_Faces_Game_Id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.FcrPercentage)
                    .HasColumnName("FCR_Percentage")
                    .HasColumnType("int(11)");

                entity.Property(e => e.GamePoints)
                    .HasColumnName("Game_Points")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdGame)
                    .HasColumnName("Id_Game")
                    .HasColumnType("int(6)");

                entity.Property(e => e.IdUser)
                    .HasColumnName("id_user")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IsCompleted)
                    .HasColumnName("Is_Completed")
                    .HasColumnType("int(2)");

                entity.Property(e => e.IsRightAns).HasColumnType("int(11)");

                entity.Property(e => e.PerTileAnswerId)
                    .HasColumnName("PerTile_Answer_Id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.PerTileId)
                    .HasColumnName("PerTile_Id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.PerTileQuestionId)
                    .HasColumnName("PerTile_Question_Id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Quality).HasColumnType("int(11)");

                entity.Property(e => e.QuestionId)
                    .HasColumnName("Question_Id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ServiceLevel)
                    .HasColumnName("Service_Level")
                    .HasColumnType("int(11)");

                entity.Property(e => e.StreakPoints)
                    .HasColumnName("Streak_Points")
                    .HasColumnType("int(11)");

                entity.Property(e => e.TimetakenToComplete)
                    .HasColumnName("Timetaken_to_complete")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.UpdatedDateTime)
                    .HasColumnName("updated_date_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.UserInput)
                    .HasColumnName("User_input")
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.UserPlayCount)
                    .HasColumnName("User_Play_Count")
                    .HasColumnType("int(11)");

                entity.Property(e => e.WaitTime)
                    .HasColumnName("Wait_Time")
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<TblCubesGameMasterUserLog>(entity =>
            {
                entity.HasKey(e => e.IdLog)
                    .HasName("PRIMARY");

                entity.ToTable("tbl_cubes_game_master_user_log");

                entity.Property(e => e.IdLog)
                    .HasColumnName("id_log")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Aht)
                    .HasColumnName("AHT")
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.BonusPoints)
                    .HasColumnName("Bonus_Points")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CallsWaiting)
                    .HasColumnName("Calls_Waiting")
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.CubesFacesGameId)
                    .HasColumnName("Cubes_Faces_Game_Id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.FcrPercentage)
                    .HasColumnName("FCR_Percentage")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdGame)
                    .HasColumnName("Id_Game")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdMasterBadge)
                    .HasColumnName("Id_Master_Badge")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdOrganization)
                    .HasColumnName("ID_ORGANIZATION")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdUser)
                    .HasColumnName("id_user")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IsCompleted)
                    .HasColumnName("Is_Completed")
                    .HasColumnType("int(2)");

                entity.Property(e => e.PerTileId)
                    .HasColumnName("PerTile_Id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Quality).HasColumnType("int(11)");

                entity.Property(e => e.ServiceLevel)
                    .HasColumnName("Service_Level")
                    .HasColumnType("int(11)");

                entity.Property(e => e.TimetakenToComplete)
                    .HasColumnName("timetaken_to_complete")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.TotalPoints).HasColumnType("int(11)");

                entity.Property(e => e.UpdatedDateTime)
                    .HasColumnName("updated_date_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.UserPlayCount)
                    .HasColumnName("User_Play_Count")
                    .HasColumnType("int(11)");

                entity.Property(e => e.WaitTime)
                    .HasColumnName("Wait_Time")
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<TblCubesGridrowcolLog>(entity =>
            {
                entity.HasKey(e => e.IdLog)
                    .HasName("PRIMARY");

                entity.ToTable("tbl_cubes_gridrowcol_log");

                entity.Property(e => e.IdLog)
                    .HasColumnName("Id_Log")
                    .HasColumnType("int(6)");

                entity.Property(e => e.ColumnNo).HasColumnType("int(11)");

                entity.Property(e => e.Direction)
                    .HasColumnType("varchar(11)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.IdOrganization)
                    .HasColumnName("ID_ORGANIZATION")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdUser)
                    .HasColumnName("id_user")
                    .HasColumnType("int(11)");

                entity.Property(e => e.KeyNo)
                    .HasColumnName("Key_No")
                    .HasColumnType("int(11)");

                entity.Property(e => e.QuestionId)
                    .HasColumnName("Question_Id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.RowNo).HasColumnType("int(11)");

                entity.Property(e => e.UpdatedDateTime)
                    .HasColumnName("Updated_Date_Time")
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .ValueGeneratedOnAddOrUpdate();
            });

            modelBuilder.Entity<TblCubesPertilesAnswerDetails>(entity =>
            {
                entity.HasKey(e => e.PerTileAnswerId)
                    .HasName("PRIMARY");

                entity.ToTable("tbl_cubes_pertiles_answer_details");

                entity.Property(e => e.PerTileAnswerId)
                    .HasColumnName("PerTile_Answer_Id")
                    .HasColumnType("int(6)");

                entity.Property(e => e.Answer)
                    .IsRequired()
                    .HasColumnType("varchar(2000)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.CubesFacesGameId)
                    .HasColumnName("Cubes_Faces_Game_Id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdOrganization)
                    .HasColumnName("ID_ORGANIZATION")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasColumnType("char(1)")
                    .HasDefaultValueSql("'A'")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.IsRightAns).HasColumnType("int(1)");

                entity.Property(e => e.PerTileId)
                    .HasColumnName("PerTile_Id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.QuestionId)
                    .HasColumnName("Question_Id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.UpdatedDateTime)
                    .HasColumnName("Updated_Date_Time")
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .ValueGeneratedOnAddOrUpdate();
            });

            modelBuilder.Entity<TblCubesPertilesQuestionDetails>(entity =>
            {
                entity.HasKey(e => e.PerTileQuestionId)
                    .HasName("PRIMARY");

                entity.ToTable("tbl_cubes_pertiles_question_details");

                entity.Property(e => e.PerTileQuestionId)
                    .HasColumnName("PerTile_Question_Id")
                    .HasColumnType("int(6)");

                entity.Property(e => e.ColumnNo).HasColumnType("int(11)");

                entity.Property(e => e.Complexity)
                    .HasColumnName("complexity")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CubesFacesGameId)
                    .HasColumnName("Cubes_Faces_Game_Id")
                    .HasColumnType("int(6)");

                entity.Property(e => e.Direction)
                    .HasColumnType("varchar(11)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.FeedBack)
                    .HasColumnType("varchar(500)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.IdOrganization)
                    .HasColumnName("ID_ORGANIZATION")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasColumnType("char(1)")
                    .HasDefaultValueSql("'A'")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.IsApproved).HasColumnType("int(1)");

                entity.Property(e => e.IsDraft).HasColumnType("int(1)");

                entity.Property(e => e.PerTileId)
                    .HasColumnName("PerTile_Id")
                    .HasColumnType("int(6)");

                entity.Property(e => e.Question)
                    .IsRequired()
                    .HasColumnType("varchar(2000)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.QuestionClue)
                    .IsRequired()
                    .HasColumnName("Question_Clue")
                    .HasColumnType("varchar(2000)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.QuestionId)
                    .HasColumnName("Question_Id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.QuestionSet).HasColumnType("int(11)");

                entity.Property(e => e.RowNo).HasColumnType("int(11)");

                entity.Property(e => e.UpdatedDateTime)
                    .HasColumnName("Updated_Date_Time")
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .ValueGeneratedOnAddOrUpdate();
            });

            modelBuilder.Entity<TblCubesQuestionMappingwithHierarchy>(entity =>
            {
                entity.HasKey(e => e.MapId)
                    .HasName("PRIMARY");

                entity.ToTable("tbl_cubes_question_mappingwith_hierarchy");

                entity.Property(e => e.MapId)
                    .HasColumnName("Map_Id")
                    .HasColumnType("int(6)");

                entity.Property(e => e.IdOrgHierarchy)
                    .HasColumnName("Id_Org_hierarchy")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasColumnType("char(1)")
                    .HasDefaultValueSql("'A'")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.QuestionId)
                    .HasColumnName("Question_Id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.UpdatedDateTime)
                    .HasColumnName("Updated_Date_Time")
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .ValueGeneratedOnAddOrUpdate();
            });

            modelBuilder.Entity<TblFacesAttemptnoMappingwithHierarchy>(entity =>
            {
                entity.HasKey(e => e.MapAttemptNoId)
                    .HasName("PRIMARY");

                entity.ToTable("tbl_faces_attemptno_mappingwith_hierarchy");

                entity.Property(e => e.MapAttemptNoId)
                    .HasColumnName("Map_AttemptNo_Id")
                    .HasColumnType("int(6)");

                entity.Property(e => e.AttemptNoMapId)
                    .HasColumnName("AttemptNo_Map_Id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdOrgHierarchy)
                    .HasColumnName("Id_Org_hierarchy")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdOrganization)
                    .HasColumnName("ID_ORGANIZATION")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasColumnType("char(1)")
                    .HasDefaultValueSql("'A'")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.UpdatedDateTime)
                    .HasColumnName("Updated_Date_Time")
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .ValueGeneratedOnAddOrUpdate();
            });

            modelBuilder.Entity<TblGameIdMaster>(entity =>
            {
                entity.HasKey(e => e.IdGame)
                    .HasName("PRIMARY");

                entity.ToTable("tbl_game_id_master");

                entity.Property(e => e.IdGame)
                    .HasColumnName("Id_Game")
                    .HasColumnType("int(6)");

                entity.Property(e => e.GameName)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasColumnType("char(1)")
                    .HasDefaultValueSql("'A'")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.UpdatedDateTime)
                    .HasColumnName("Updated_Date_Time")
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .ValueGeneratedOnAddOrUpdate();
            });

            modelBuilder.Entity<TblGameMaster>(entity =>
            {
                entity.ToTable("tbl_game_master");

                entity.Property(e => e.Id).HasColumnType("int(6)");

                entity.Property(e => e.GameIntro)
                    .HasColumnName("Game_Intro")
                    .HasColumnType("varchar(3000)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.GameIntroVideo)
                    .HasColumnName("Game_Intro_Video")
                    .HasColumnType("varchar(200)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.GameName)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.IdGame)
                    .HasColumnName("Id_Game")
                    .HasColumnType("int(6)");

                entity.Property(e => e.IdOrganization)
                    .HasColumnName("ID_ORGANIZATION")
                    .HasColumnType("int(6)");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasColumnType("char(1)")
                    .HasDefaultValueSql("'A'")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.UpdatedDateTime)
                    .HasColumnName("Updated_Date_Time")
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .ValueGeneratedOnAddOrUpdate();
            });

            modelBuilder.Entity<TblGameMissionsRulesMaster>(entity =>
            {
                entity.HasKey(e => e.IdMissionsRules)
                    .HasName("PRIMARY");

                entity.ToTable("tbl_game_missions_rules_master");

                entity.Property(e => e.IdMissionsRules)
                    .HasColumnName("Id_Missions_Rules")
                    .HasColumnType("int(6)");

                entity.Property(e => e.CategorieNo)
                    .HasColumnName("Categorie_no")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdOrganization)
                    .HasColumnName("ID_ORGANIZATION")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasColumnType("char(1)")
                    .HasDefaultValueSql("'A'")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.MissionNo)
                    .HasColumnName("Mission_No")
                    .HasColumnType("int(11)");

                entity.Property(e => e.MissionPoints)
                    .HasColumnName("Mission_Points")
                    .HasColumnType("int(11)");

                entity.Property(e => e.MissionTime)
                    .HasColumnName("Mission_Time")
                    .HasColumnType("int(11)");

                entity.Property(e => e.QuestionCount)
                    .HasColumnName("Question_count")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ServiceLevelColor)
                    .HasColumnName("service_level_Color")
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.StreakNo)
                    .HasColumnName("Streak_no")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Text)
                    .IsRequired()
                    .HasColumnType("varchar(1000)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.UpdatedDateTime)
                    .HasColumnName("Updated_Date_Time")
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .ValueGeneratedOnAddOrUpdate();
            });

            modelBuilder.Entity<TblGameMissionsUserLog>(entity =>
            {
                entity.HasKey(e => e.IdMissionsUserLog)
                    .HasName("PRIMARY");

                entity.ToTable("tbl_game_missions_user_log");

                entity.Property(e => e.IdMissionsUserLog)
                    .HasColumnName("Id_Missions_User_Log")
                    .HasColumnType("int(6)");

                entity.Property(e => e.IdOrganization)
                    .HasColumnName("ID_ORGANIZATION")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdUser)
                    .HasColumnName("id_user")
                    .HasColumnType("int(11)");

                entity.Property(e => e.MissionNo)
                    .HasColumnName("Mission_No")
                    .HasColumnType("int(11)");

                entity.Property(e => e.MissionPoints)
                    .HasColumnName("Mission_Points")
                    .HasColumnType("int(11)");

                entity.Property(e => e.UpdatedDateTime)
                    .HasColumnName("Updated_Date_Time")
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .ValueGeneratedOnAddOrUpdate();
            });

            modelBuilder.Entity<TblGameWellcomeMsg>(entity =>
            {
                entity.HasKey(e => e.IdMsg)
                    .HasName("PRIMARY");

                entity.ToTable("tbl_game_wellcome_msg");

                entity.Property(e => e.IdMsg)
                    .HasColumnName("Id_msg")
                    .HasColumnType("int(6)");

                entity.Property(e => e.IdOrganization)
                    .HasColumnName("ID_ORGANIZATION")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasColumnType("char(1)")
                    .HasDefaultValueSql("'A'")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.IsFirstTimeLogin)
                    .HasColumnType("int(1)")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.TitleMsg)
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.UpdatedDateTime)
                    .HasColumnName("Updated_Date_Time")
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .ValueGeneratedOnAddOrUpdate();

                entity.Property(e => e.WellComeMsg)
                    .HasColumnType("varchar(1000)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<TblHeirarchyBatchesMaster>(entity =>
            {
                entity.HasKey(e => e.IdBatch)
                    .HasName("PRIMARY");

                entity.ToTable("tbl_heirarchy_batches_master");

                entity.Property(e => e.IdBatch)
                    .HasColumnName("Id_Batch")
                    .HasColumnType("int(6)");

                entity.Property(e => e.BatchName)
                    .IsRequired()
                    .HasColumnName("Batch_Name")
                    .HasColumnType("varchar(200)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.IdCmsUser)
                    .HasColumnName("Id_CmsUser")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdOrgHierarchy)
                    .HasColumnName("Id_Org_hierarchy")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdOrganization)
                    .HasColumnName("ID_ORGANIZATION")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasColumnType("char(1)")
                    .HasDefaultValueSql("'A'")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.UpdatedDateTime)
                    .HasColumnName("Updated_Date_Time")
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .ValueGeneratedOnAddOrUpdate();
            });

            modelBuilder.Entity<TblIndustry>(entity =>
            {
                entity.HasKey(e => e.IdIndustry)
                    .HasName("PRIMARY");

                entity.ToTable("tbl_industry");

                entity.HasIndex(e => e.IdIndustry)
                    .HasName("ID_INDUSTRY_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.Industryname)
                    .HasName("INDUSTRYNAME_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.IdIndustry)
                    .HasColumnName("ID_INDUSTRY")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Description)
                    .HasColumnName("DESCRIPTION")
                    .HasColumnType("varchar(200)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Industryname)
                    .IsRequired()
                    .HasColumnName("INDUSTRYNAME")
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasColumnName("STATUS")
                    .HasColumnType("char(1)")
                    .HasDefaultValueSql("'A'")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.UpdatedDateTime)
                    .HasColumnName("UPDATED_DATE_TIME")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<TblLoginSecurityQuestion>(entity =>
            {
                entity.HasKey(e => e.IdSecurityQuestion)
                    .HasName("PRIMARY");

                entity.ToTable("tbl_login_security_question");

                entity.Property(e => e.IdSecurityQuestion)
                    .HasColumnName("Id_Security_Question")
                    .HasColumnType("int(6)");

                entity.Property(e => e.IdCmsUser)
                    .HasColumnName("Id_CmsUser")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasColumnType("char(1)")
                    .HasDefaultValueSql("'A'")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.SecurityQuestion)
                    .HasColumnName("Security_Question")
                    .HasColumnType("varchar(300)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.UpdatedDateTime)
                    .HasColumnName("Updated_Date_Time")
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .ValueGeneratedOnAddOrUpdate();
            });

            modelBuilder.Entity<TblOrganization>(entity =>
            {
                entity.HasKey(e => e.IdOrganization)
                    .HasName("PRIMARY");

                entity.ToTable("tbl_organization");

                entity.Property(e => e.IdOrganization)
                    .HasColumnName("ID_ORGANIZATION")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ContactEmail)
                    .HasColumnName("Contact_Email")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.DefaultEmail)
                    .HasColumnName("Default_Email")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Description)
                    .HasColumnType("varchar(2000)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.DomainEmailId)
                    .HasColumnName("Domain_EmailId")
                    .HasColumnType("varchar(200)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.IdBusinessType)
                    .HasColumnName("ID_BUSINESS_TYPE")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdCmsUser)
                    .HasColumnName("Id_CmsUser")
                    .HasColumnType("int(6)");

                entity.Property(e => e.IdIndustry)
                    .HasColumnName("ID_INDUSTRY")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdOrganizationType)
                    .HasColumnName("Id_OrganizationType")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Logo)
                    .HasColumnType("varchar(450)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Name)
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.OrganizationCode)
                    .HasColumnName("Organization_Code")
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.OrganizationName)
                    .HasColumnName("Organization_Name")
                    .HasColumnType("text")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.PhoneNo)
                    .HasColumnType("varchar(15)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.SenderPassword)
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasColumnType("char(1)")
                    .HasDefaultValueSql("'A'")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.UpdatedDateTime)
                    .HasColumnName("Updated_Date_time")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<TblOrganizationHierarchy>(entity =>
            {
                entity.HasKey(e => e.IdOrgHierarchy)
                    .HasName("PRIMARY");

                entity.ToTable("tbl_organization_hierarchy");

                entity.Property(e => e.IdOrgHierarchy)
                    .HasColumnName("Id_Org_hierarchy")
                    .HasColumnType("int(6)");

                entity.Property(e => e.HierarchyName)
                    .HasColumnName("Hierarchy_Name")
                    .HasColumnType("varchar(300)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.IdCmsUser)
                    .HasColumnName("Id_CmsUser")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdOrganization)
                    .HasColumnName("ID_ORGANIZATION")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasColumnType("char(1)")
                    .HasDefaultValueSql("'A'")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.ParentIdOrgHierarchy)
                    .HasColumnName("Parent_Id_Org_hierarchy")
                    .HasColumnType("int(11)");

                entity.Property(e => e.UpdatedDateTime)
                    .HasColumnName("Updated_Date_Time")
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .ValueGeneratedOnAddOrUpdate();
            });

            modelBuilder.Entity<TblOrganizationtype>(entity =>
            {
                entity.HasKey(e => e.IdOrganizationType)
                    .HasName("PRIMARY");

                entity.ToTable("tbl_organizationtype");

                entity.Property(e => e.IdOrganizationType)
                    .HasColumnName("Id_OrganizationType")
                    .HasColumnType("int(6)");

                entity.Property(e => e.IdOrganization)
                    .HasColumnName("ID_ORGANIZATION")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasColumnType("char(1)")
                    .HasDefaultValueSql("'A'")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.OrganizationType)
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.UpdatedDateTime)
                    .HasColumnName("Updated_Date_Time")
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .ValueGeneratedOnAddOrUpdate();
            });

            modelBuilder.Entity<TblRewardsRedeemMaster>(entity =>
            {
                entity.HasKey(e => e.IdRewards)
                    .HasName("PRIMARY");

                entity.ToTable("tbl_rewards_redeem_master");

                entity.Property(e => e.IdRewards)
                    .HasColumnName("id_rewards")
                    .HasColumnType("int(6)");

                entity.Property(e => e.AccountId)
                    .HasColumnName("AccountID")
                    .HasColumnType("varchar(200)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.EmailAddress)
                    .HasColumnType("varchar(200)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.IdGame)
                    .HasColumnName("id_game")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdOrg)
                    .HasColumnName("id_org")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdUser)
                    .HasColumnName("id_user")
                    .HasColumnType("int(11)");

                entity.Property(e => e.PartnerCode)
                    .HasColumnType("varchar(200)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.ProviderCode)
                    .HasColumnType("varchar(200)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.RedeemType)
                    .HasColumnType("varchar(200)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.TotalPoints).HasColumnType("int(11)");

                entity.Property(e => e.TransactionId)
                    .HasColumnName("TransactionID")
                    .HasColumnType("varchar(200)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.UserName)
                    .HasColumnType("varchar(200)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<TblRoleCmsUserMapping>(entity =>
            {
                entity.HasKey(e => e.IdRoleCmsUserMapping)
                    .HasName("PRIMARY");

                entity.ToTable("tbl_role_cms_user_mapping");

                entity.Property(e => e.IdRoleCmsUserMapping)
                    .HasColumnName("Id_Role_CMS_User_Mapping")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdCmsRole)
                    .HasColumnName("Id_CMS_Role")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdCmsUser)
                    .HasColumnName("Id_CmsUser")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdOrganization)
                    .HasColumnName("ID_ORGANIZATION")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Status)
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("'A'")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.UpdatedDateTime)
                    .HasColumnName("Updated_Date_time")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<TblServiceLevelBandsDetails>(entity =>
            {
                entity.HasKey(e => e.ServiceLevelId)
                    .HasName("PRIMARY");

                entity.ToTable("tbl_service_level_bands_details");

                entity.Property(e => e.ServiceLevelId)
                    .HasColumnName("Service_level_Id")
                    .HasColumnType("int(6)");

                entity.Property(e => e.Colour)
                    .HasColumnType("varchar(200)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.CubesFacesId)
                    .HasColumnName("Cubes_Faces_Id")
                    .HasColumnType("int(6)");

                entity.Property(e => e.IdOrganization)
                    .HasColumnName("ID_ORGANIZATION")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasColumnType("char(1)")
                    .HasDefaultValueSql("'A'")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Percentage).HasColumnType("int(11)");

                entity.Property(e => e.UpdatedDateTime)
                    .HasColumnName("Updated_Date_Time")
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .ValueGeneratedOnAddOrUpdate();
            });

            modelBuilder.Entity<TblServiceLevelScoringDetails>(entity =>
            {
                entity.HasKey(e => e.ServiceLevelScoringId)
                    .HasName("PRIMARY");

                entity.ToTable("tbl_service_level_scoring_details");

                entity.Property(e => e.ServiceLevelScoringId)
                    .HasColumnName("Service_Level_Scoring_Id")
                    .HasColumnType("int(6)");

                entity.Property(e => e.BonusPoints)
                    .HasColumnName("Bonus_Points")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CubesFacesId)
                    .HasColumnName("Cubes_Faces_Id")
                    .HasColumnType("int(6)");

                entity.Property(e => e.IdOrganization)
                    .HasColumnName("ID_ORGANIZATION")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasColumnType("char(1)")
                    .HasDefaultValueSql("'A'")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.LevelEndsColour)
                    .HasColumnName("Level_Ends_Colour")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.UpdatedDateTime)
                    .HasColumnName("Updated_Date_Time")
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .ValueGeneratedOnAddOrUpdate();
            });

            modelBuilder.Entity<TblServiceLevelStreakScoringDetails>(entity =>
            {
                entity.HasKey(e => e.StreakId)
                    .HasName("PRIMARY");

                entity.ToTable("tbl_service_level_streak_scoring_details");

                entity.Property(e => e.StreakId)
                    .HasColumnName("Streak_Id")
                    .HasColumnType("int(6)");

                entity.Property(e => e.CubesFacesId)
                    .HasColumnName("Cubes_Faces_Id")
                    .HasColumnType("int(6)");

                entity.Property(e => e.IdOrganization)
                    .HasColumnName("ID_ORGANIZATION")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasColumnType("char(1)")
                    .HasDefaultValueSql("'A'")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Streak).HasColumnType("int(11)");

                entity.Property(e => e.StreakPoints)
                    .HasColumnName("Streak_Points")
                    .HasColumnType("int(11)");

                entity.Property(e => e.UpdatedDateTime)
                    .HasColumnName("Updated_Date_Time")
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .ValueGeneratedOnAddOrUpdate();
            });

            modelBuilder.Entity<TblUserSecurityQuestionLog>(entity =>
            {
                entity.HasKey(e => e.IdQuestionLog)
                    .HasName("PRIMARY");

                entity.ToTable("tbl_user_security_question_log");

                entity.Property(e => e.IdQuestionLog)
                    .HasColumnName("Id_Question_Log")
                    .HasColumnType("int(6)");

                entity.Property(e => e.IdOrganization)
                    .HasColumnName("ID_ORGANIZATION")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdSecurityQuestion)
                    .HasColumnName("Id_Security_Question")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdUser)
                    .HasColumnName("id_user")
                    .HasColumnType("int(11)");

                entity.Property(e => e.SecurityQuestionAns)
                    .HasColumnName("Security_Question_Ans")
                    .HasColumnType("varchar(300)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.UpdatedDateTime)
                    .HasColumnName("Updated_Date_Time")
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .ValueGeneratedOnAddOrUpdate();
            });

            modelBuilder.Entity<TblUsers>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PRIMARY");

                entity.ToTable("tbl_users");

                entity.Property(e => e.UserId)
                    .HasColumnName("User_Id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.AadharCardImage)
                    .HasColumnName("Aadhar_Card_Image")
                    .HasColumnType("varchar(300)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.AadharNumber)
                    .HasColumnName("Aadhar_Number")
                    .HasColumnType("varchar(300)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.BirthDate)
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.CountryCode)
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.CurrentCity)
                    .HasColumnName("Current_City")
                    .HasColumnType("varchar(300)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.CurrentPincode)
                    .HasColumnName("Current_Pincode")
                    .HasColumnType("varchar(300)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.CurrentState)
                    .HasColumnName("Current_State")
                    .HasColumnType("varchar(300)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.CurrentStreetAddress1)
                    .HasColumnName("Current_Street_Address1")
                    .HasColumnType("varchar(500)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.CurrentStreetAddress2)
                    .HasColumnName("Current_Street_Address2")
                    .HasColumnType("varchar(500)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Email)
                    .HasColumnType("varchar(300)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.EmployeeDesignation)
                    .HasColumnName("Employee_Designation")
                    .HasColumnType("varchar(200)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.EmplyeeId)
                    .HasColumnName("Emplyee_Id")
                    .HasColumnType("varchar(200)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.FirstName)
                    .HasColumnName("First_Name")
                    .HasColumnType("varchar(200)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.IdBranch)
                    .HasColumnName("Id_Branch")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdDepartment)
                    .HasColumnName("Id_Department")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdOrganization)
                    .HasColumnName("ID_ORGANIZATION")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdReportingManager)
                    .HasColumnName("Id_Reporting_Manager")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdRole)
                    .HasColumnName("Id_Role")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IsFirstTimeLogin)
                    .HasColumnName("Is_First_Time_Login")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.LastName)
                    .HasColumnName("Last_Name")
                    .HasColumnType("varchar(200)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.LoginType).HasColumnType("int(6)");

                entity.Property(e => e.LoginUserId)
                    .IsRequired()
                    .HasColumnName("Login_User_Id")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.MiddleName)
                    .HasColumnName("Middle_Name")
                    .HasColumnType("varchar(200)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Otp)
                    .HasColumnName("OTP")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.PanCardImage)
                    .HasColumnName("Pan_Card_Image")
                    .HasColumnType("varchar(300)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.PanNumber)
                    .HasColumnName("Pan_Number")
                    .HasColumnType("varchar(300)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Password)
                    .HasColumnType("varchar(500)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.PermanentCity)
                    .HasColumnName("Permanent_City")
                    .HasColumnType("varchar(200)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.PermanentPincode)
                    .HasColumnName("Permanent_Pincode")
                    .HasColumnType("varchar(300)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.PermanentState)
                    .HasColumnName("Permanent_State")
                    .HasColumnType("varchar(300)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.PermanentStreetAddress1)
                    .HasColumnName("Permanent_Street_Address1")
                    .HasColumnType("varchar(500)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.PermanentStreetAddress2)
                    .HasColumnName("Permanent_Street_Address2")
                    .HasColumnType("varchar(500)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.PhoneNo)
                    .HasColumnType("varchar(15)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.ProfilePicture)
                    .HasColumnName("Profile_Picture")
                    .HasColumnType("varchar(300)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasColumnType("char(1)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.TrainerIdUser)
                    .HasColumnName("Trainer_id_user")
                    .HasColumnType("int(11)");

                entity.Property(e => e.UpdatedDateTime)
                    .HasColumnName("Updated_Date_time")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<TblUsersLog>(entity =>
            {
                entity.HasKey(e => e.IdLog)
                    .HasName("PRIMARY");

                entity.ToTable("tbl_users_log");

                entity.Property(e => e.IdLog)
                    .HasColumnName("Id_Log")
                    .HasColumnType("int(6)");

                entity.Property(e => e.IdOrganization)
                    .HasColumnName("ID_ORGANIZATION")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdUser)
                    .HasColumnName("Id_User")
                    .HasColumnType("int(6)");

                entity.Property(e => e.UpdatedDateTime)
                    .HasColumnName("Updated_Date_Time")
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .ValueGeneratedOnAddOrUpdate();
            });

            modelBuilder.Entity<TblUsersMappingwithBatchid>(entity =>
            {
                entity.HasKey(e => e.MapBatchId)
                    .HasName("PRIMARY");

                entity.ToTable("tbl_users_mappingwith_batchid");

                entity.Property(e => e.MapBatchId)
                    .HasColumnName("Map_Batch_Id")
                    .HasColumnType("int(6)");

                entity.Property(e => e.IdBatch)
                    .HasColumnName("Id_Batch")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasColumnType("char(1)")
                    .HasDefaultValueSql("'A'")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.UpdatedDateTime)
                    .HasColumnName("Updated_Date_Time")
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .ValueGeneratedOnAddOrUpdate();

                entity.Property(e => e.UserId)
                    .HasColumnName("User_Id")
                    .HasColumnType("int(11)");
            });

            modelBuilder.Entity<TblUsersMappingwithHierarchy>(entity =>
            {
                entity.HasKey(e => e.UserMapId)
                    .HasName("PRIMARY");

                entity.ToTable("tbl_users_mappingwith_hierarchy");

                entity.Property(e => e.UserMapId)
                    .HasColumnName("UserMap_Id")
                    .HasColumnType("int(6)");

                entity.Property(e => e.IdOrgHierarchy)
                    .HasColumnName("Id_Org_hierarchy")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasColumnType("char(1)")
                    .HasDefaultValueSql("'A'")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.UpdatedDateTime)
                    .HasColumnName("Updated_Date_Time")
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .ValueGeneratedOnAddOrUpdate();

                entity.Property(e => e.UserId)
                    .HasColumnName("User_Id")
                    .HasColumnType("int(11)");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
