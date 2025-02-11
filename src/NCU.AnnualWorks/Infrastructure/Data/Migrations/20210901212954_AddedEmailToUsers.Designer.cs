﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NCU.AnnualWorks.Infrastructure.Data;

namespace NCU.AnnualWorks.Infrastructure.Data.Migrations
{
    [DbContext(typeof(ApiDbContext))]
    [Migration("20210901212954_AddedEmailToUsers")]
    partial class AddedEmailToUsers
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.15")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("NCU.AnnualWorks.Core.Models.DbModels.Answer", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)");

                    b.Property<long>("CreatedById")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("ModifiedAt")
                        .ValueGeneratedOnUpdate()
                        .HasColumnType("datetime(6)");

                    b.Property<long?>("ModifiedById")
                        .HasColumnType("bigint");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4")
                        .HasMaxLength(2500);

                    b.HasKey("Id");

                    b.HasIndex("CreatedById");

                    b.HasIndex("ModifiedById");

                    b.ToTable("Answers");
                });

            modelBuilder.Entity("NCU.AnnualWorks.Core.Models.DbModels.File", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("Checksum")
                        .IsRequired()
                        .HasColumnType("varchar(64) CHARACTER SET utf8mb4")
                        .HasMaxLength(64);

                    b.Property<string>("ContentType")
                        .IsRequired()
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4")
                        .HasMaxLength(255);

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)");

                    b.Property<long>("CreatedById")
                        .HasColumnType("bigint");

                    b.Property<string>("Extension")
                        .IsRequired()
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4")
                        .HasMaxLength(255);

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4")
                        .HasMaxLength(255);

                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime?>("ModifiedAt")
                        .ValueGeneratedOnUpdate()
                        .HasColumnType("datetime(6)");

                    b.Property<long?>("ModifiedById")
                        .HasColumnType("bigint");

                    b.Property<string>("Path")
                        .IsRequired()
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4")
                        .HasMaxLength(255);

                    b.Property<long>("Size")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasAlternateKey("Guid");

                    b.HasIndex("CreatedById");

                    b.HasIndex("ModifiedById");

                    b.ToTable("Files");
                });

            modelBuilder.Entity("NCU.AnnualWorks.Core.Models.DbModels.Keyword", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)");

                    b.Property<long>("CreatedById")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("ModifiedAt")
                        .ValueGeneratedOnUpdate()
                        .HasColumnType("datetime(6)");

                    b.Property<long?>("ModifiedById")
                        .HasColumnType("bigint");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4")
                        .HasMaxLength(255);

                    b.HasKey("Id");

                    b.HasAlternateKey("Text");

                    b.HasIndex("CreatedById");

                    b.HasIndex("ModifiedById");

                    b.ToTable("Keywords");
                });

            modelBuilder.Entity("NCU.AnnualWorks.Core.Models.DbModels.Question", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)");

                    b.Property<long?>("CreatedById")
                        .HasColumnType("bigint");

                    b.Property<bool>("IsActive")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsRequired")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime?>("ModifiedAt")
                        .ValueGeneratedOnUpdate()
                        .HasColumnType("datetime(6)");

                    b.Property<long?>("ModifiedById")
                        .HasColumnType("bigint");

                    b.Property<long>("Order")
                        .HasColumnType("bigint")
                        .HasMaxLength(3);

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("varchar(500) CHARACTER SET utf8mb4")
                        .HasMaxLength(500);

                    b.HasKey("Id");

                    b.HasIndex("CreatedById");

                    b.HasIndex("ModifiedById");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("NCU.AnnualWorks.Core.Models.DbModels.Review", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)");

                    b.Property<long>("CreatedById")
                        .HasColumnType("bigint");

                    b.Property<string>("Grade")
                        .IsRequired()
                        .HasColumnType("varchar(3) CHARACTER SET utf8mb4")
                        .HasMaxLength(3);

                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<bool>("IsConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime?>("ModifiedAt")
                        .ValueGeneratedOnUpdate()
                        .HasColumnType("datetime(6)");

                    b.Property<long?>("ModifiedById")
                        .HasColumnType("bigint");

                    b.Property<long>("ThesisId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasAlternateKey("Guid");

                    b.HasIndex("CreatedById");

                    b.HasIndex("ModifiedById");

                    b.HasIndex("ThesisId");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("NCU.AnnualWorks.Core.Models.DbModels.ReviewQnA", b =>
                {
                    b.Property<long>("ReviewId")
                        .HasColumnType("bigint");

                    b.Property<long>("QuestionId")
                        .HasColumnType("bigint");

                    b.Property<long>("AnswerId")
                        .HasColumnType("bigint");

                    b.HasKey("ReviewId", "QuestionId", "AnswerId");

                    b.HasIndex("AnswerId")
                        .IsUnique();

                    b.HasIndex("QuestionId");

                    b.ToTable("ReviewQnAs");
                });

            modelBuilder.Entity("NCU.AnnualWorks.Core.Models.DbModels.Settings", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("CourseCode")
                        .IsRequired()
                        .HasColumnType("varchar(100) CHARACTER SET utf8mb4")
                        .HasMaxLength(100);

                    b.Property<DateTime?>("Deadline")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("ModifiedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime(6)");

                    b.Property<long?>("ModifiedById")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("ModifiedById");

                    b.ToTable("Settings");
                });

            modelBuilder.Entity("NCU.AnnualWorks.Core.Models.DbModels.Thesis", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("Abstract")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4")
                        .HasMaxLength(4000);

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)");

                    b.Property<long>("CreatedById")
                        .HasColumnType("bigint");

                    b.Property<long>("FileId")
                        .HasColumnType("bigint");

                    b.Property<string>("Grade")
                        .HasColumnType("varchar(3) CHARACTER SET utf8mb4")
                        .HasMaxLength(3);

                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<bool>("Hidden")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime?>("ModifiedAt")
                        .ValueGeneratedOnUpdate()
                        .HasColumnType("datetime(6)");

                    b.Property<long?>("ModifiedById")
                        .HasColumnType("bigint");

                    b.Property<long>("PromoterId")
                        .HasColumnType("bigint");

                    b.Property<long>("ReviewerId")
                        .HasColumnType("bigint");

                    b.Property<string>("TermId")
                        .IsRequired()
                        .HasColumnType("varchar(20) CHARACTER SET utf8mb4")
                        .HasMaxLength(20);

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("varchar(1000) CHARACTER SET utf8mb4")
                        .HasMaxLength(1000);

                    b.HasKey("Id");

                    b.HasAlternateKey("Guid");

                    b.HasIndex("CreatedById");

                    b.HasIndex("FileId")
                        .IsUnique();

                    b.HasIndex("ModifiedById");

                    b.HasIndex("PromoterId");

                    b.HasIndex("ReviewerId");

                    b.ToTable("Theses");
                });

            modelBuilder.Entity("NCU.AnnualWorks.Core.Models.DbModels.ThesisAdditionalFile", b =>
                {
                    b.Property<long>("FileId")
                        .HasColumnType("bigint");

                    b.Property<long>("ThesisId")
                        .HasColumnType("bigint");

                    b.HasKey("FileId", "ThesisId");

                    b.HasIndex("ThesisId");

                    b.ToTable("ThesisAdditionalFiles");
                });

            modelBuilder.Entity("NCU.AnnualWorks.Core.Models.DbModels.ThesisAuthor", b =>
                {
                    b.Property<long>("AuthorId")
                        .HasColumnType("bigint");

                    b.Property<long>("ThesisId")
                        .HasColumnType("bigint");

                    b.HasKey("AuthorId", "ThesisId");

                    b.HasIndex("ThesisId");

                    b.ToTable("ThesisAuthors");
                });

            modelBuilder.Entity("NCU.AnnualWorks.Core.Models.DbModels.ThesisKeyword", b =>
                {
                    b.Property<long>("ThesisId")
                        .HasColumnType("bigint");

                    b.Property<long>("KeywordId")
                        .HasColumnType("bigint");

                    b.HasKey("ThesisId", "KeywordId");

                    b.HasIndex("KeywordId");

                    b.ToTable("ThesisKeywords");
                });

            modelBuilder.Entity("NCU.AnnualWorks.Core.Models.DbModels.ThesisLog", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<int>("ModificationType")
                        .HasColumnType("int")
                        .HasComment("");

                    b.Property<long>("ThesisId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("Timestamp")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("ThesisId");

                    b.HasIndex("UserId");

                    b.ToTable("ThesisLogs");
                });

            modelBuilder.Entity("NCU.AnnualWorks.Core.Models.DbModels.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<bool>("AdminAccess")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("tinyint(1)")
                        .HasDefaultValue(false);

                    b.Property<bool>("CustomAccess")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("tinyint(1)")
                        .HasDefaultValue(false);

                    b.Property<string>("Email")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4")
                        .HasMaxLength(255);

                    b.Property<DateTime?>("FirstLoginAt")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("LastLoginAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("UsosId")
                        .IsRequired()
                        .HasColumnType("varchar(20) CHARACTER SET utf8mb4")
                        .HasMaxLength(20);

                    b.HasKey("Id");

                    b.HasAlternateKey("UsosId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("NCU.AnnualWorks.Core.Models.DbModels.Answer", b =>
                {
                    b.HasOne("NCU.AnnualWorks.Core.Models.DbModels.User", "CreatedBy")
                        .WithMany("CreatedAnswers")
                        .HasForeignKey("CreatedById")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NCU.AnnualWorks.Core.Models.DbModels.User", "ModifiedBy")
                        .WithMany("ModifiedAnswers")
                        .HasForeignKey("ModifiedById");
                });

            modelBuilder.Entity("NCU.AnnualWorks.Core.Models.DbModels.File", b =>
                {
                    b.HasOne("NCU.AnnualWorks.Core.Models.DbModels.User", "CreatedBy")
                        .WithMany("CreatedFiles")
                        .HasForeignKey("CreatedById")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NCU.AnnualWorks.Core.Models.DbModels.User", "ModifiedBy")
                        .WithMany("ModifiedFiles")
                        .HasForeignKey("ModifiedById");
                });

            modelBuilder.Entity("NCU.AnnualWorks.Core.Models.DbModels.Keyword", b =>
                {
                    b.HasOne("NCU.AnnualWorks.Core.Models.DbModels.User", "CreatedBy")
                        .WithMany("CreatedKeywords")
                        .HasForeignKey("CreatedById")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NCU.AnnualWorks.Core.Models.DbModels.User", "ModifiedBy")
                        .WithMany("ModifiedKeywords")
                        .HasForeignKey("ModifiedById");
                });

            modelBuilder.Entity("NCU.AnnualWorks.Core.Models.DbModels.Question", b =>
                {
                    b.HasOne("NCU.AnnualWorks.Core.Models.DbModels.User", "CreatedBy")
                        .WithMany("CreatedQuestions")
                        .HasForeignKey("CreatedById");

                    b.HasOne("NCU.AnnualWorks.Core.Models.DbModels.User", "ModifiedBy")
                        .WithMany("ModifiedQuestions")
                        .HasForeignKey("ModifiedById");
                });

            modelBuilder.Entity("NCU.AnnualWorks.Core.Models.DbModels.Review", b =>
                {
                    b.HasOne("NCU.AnnualWorks.Core.Models.DbModels.User", "CreatedBy")
                        .WithMany("CreatedReviews")
                        .HasForeignKey("CreatedById")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NCU.AnnualWorks.Core.Models.DbModels.User", "ModifiedBy")
                        .WithMany("ModifiedReviews")
                        .HasForeignKey("ModifiedById");

                    b.HasOne("NCU.AnnualWorks.Core.Models.DbModels.Thesis", "Thesis")
                        .WithMany("Reviews")
                        .HasForeignKey("ThesisId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("NCU.AnnualWorks.Core.Models.DbModels.ReviewQnA", b =>
                {
                    b.HasOne("NCU.AnnualWorks.Core.Models.DbModels.Answer", "Answer")
                        .WithOne("ReviewQnA")
                        .HasForeignKey("NCU.AnnualWorks.Core.Models.DbModels.ReviewQnA", "AnswerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NCU.AnnualWorks.Core.Models.DbModels.Question", "Question")
                        .WithMany("ReviewQnAs")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NCU.AnnualWorks.Core.Models.DbModels.Review", "Review")
                        .WithMany("ReviewQnAs")
                        .HasForeignKey("ReviewId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("NCU.AnnualWorks.Core.Models.DbModels.Settings", b =>
                {
                    b.HasOne("NCU.AnnualWorks.Core.Models.DbModels.User", "ModifiedBy")
                        .WithMany("ModifiedSettings")
                        .HasForeignKey("ModifiedById");
                });

            modelBuilder.Entity("NCU.AnnualWorks.Core.Models.DbModels.Thesis", b =>
                {
                    b.HasOne("NCU.AnnualWorks.Core.Models.DbModels.User", "CreatedBy")
                        .WithMany("CreatedTheses")
                        .HasForeignKey("CreatedById")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NCU.AnnualWorks.Core.Models.DbModels.File", "File")
                        .WithOne("Thesis")
                        .HasForeignKey("NCU.AnnualWorks.Core.Models.DbModels.Thesis", "FileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NCU.AnnualWorks.Core.Models.DbModels.User", "ModifiedBy")
                        .WithMany("ModifiedTheses")
                        .HasForeignKey("ModifiedById");

                    b.HasOne("NCU.AnnualWorks.Core.Models.DbModels.User", "Promoter")
                        .WithMany("PromotedTheses")
                        .HasForeignKey("PromoterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NCU.AnnualWorks.Core.Models.DbModels.User", "Reviewer")
                        .WithMany("ReviewedTheses")
                        .HasForeignKey("ReviewerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("NCU.AnnualWorks.Core.Models.DbModels.ThesisAdditionalFile", b =>
                {
                    b.HasOne("NCU.AnnualWorks.Core.Models.DbModels.File", "File")
                        .WithMany("ThesisAdditionalFiles")
                        .HasForeignKey("FileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NCU.AnnualWorks.Core.Models.DbModels.Thesis", "Thesis")
                        .WithMany("ThesisAdditionalFiles")
                        .HasForeignKey("ThesisId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("NCU.AnnualWorks.Core.Models.DbModels.ThesisAuthor", b =>
                {
                    b.HasOne("NCU.AnnualWorks.Core.Models.DbModels.User", "Author")
                        .WithMany("ThesisAuthors")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NCU.AnnualWorks.Core.Models.DbModels.Thesis", "Thesis")
                        .WithMany("ThesisAuthors")
                        .HasForeignKey("ThesisId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("NCU.AnnualWorks.Core.Models.DbModels.ThesisKeyword", b =>
                {
                    b.HasOne("NCU.AnnualWorks.Core.Models.DbModels.Keyword", "Keyword")
                        .WithMany("ThesisKeywords")
                        .HasForeignKey("KeywordId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NCU.AnnualWorks.Core.Models.DbModels.Thesis", "Thesis")
                        .WithMany("ThesisKeywords")
                        .HasForeignKey("ThesisId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("NCU.AnnualWorks.Core.Models.DbModels.ThesisLog", b =>
                {
                    b.HasOne("NCU.AnnualWorks.Core.Models.DbModels.Thesis", "Thesis")
                        .WithMany("ThesisLogs")
                        .HasForeignKey("ThesisId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NCU.AnnualWorks.Core.Models.DbModels.User", "User")
                        .WithMany("ThesisLogs")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
