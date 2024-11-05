﻿// <auto-generated />
using System;
using Hola.Health.AdministrationService.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Volo.Abp.EntityFrameworkCore;

#nullable disable

namespace Hola.Health.AdministrationService.Migrations
{
    [DbContext(typeof(AdministrationServiceDbContext))]
    [Migration("20241104125026_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("_Abp_DatabaseProvider", EfCoreDatabaseProvider.PostgreSql)
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Volo.Abp.EntityFrameworkCore.DistributedEvents.IncomingEventRecord", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("CreationTime");

                    b.Property<byte[]>("EventData")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.Property<string>("EventName")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("ExtraProperties")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("ExtraProperties");

                    b.Property<string>("MessageId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("Processed")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("ProcessedTime")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("MessageId");

                    b.HasIndex("Processed", "CreationTime");

                    b.ToTable("AbpEventInbox", (string)null);
                });

            modelBuilder.Entity("Volo.Abp.EntityFrameworkCore.DistributedEvents.OutgoingEventRecord", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("CreationTime");

                    b.Property<byte[]>("EventData")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.Property<string>("EventName")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("ExtraProperties")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("ExtraProperties");

                    b.HasKey("Id");

                    b.HasIndex("CreationTime");

                    b.ToTable("AbpEventOutbox", (string)null);
                });

            modelBuilder.Entity("Volo.Abp.FeatureManagement.FeatureDefinitionRecord", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<string>("AllowedProviders")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("DefaultValue")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("Description")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("ExtraProperties")
                        .HasColumnType("text")
                        .HasColumnName("ExtraProperties");

                    b.Property<string>("GroupName")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<bool>("IsAvailableToHost")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsVisibleToClients")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<string>("ParentName")
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<string>("ValueType")
                        .HasMaxLength(2048)
                        .HasColumnType("character varying(2048)");

                    b.HasKey("Id");

                    b.HasIndex("GroupName");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("AbpFeatures", (string)null);
                });

            modelBuilder.Entity("Volo.Abp.FeatureManagement.FeatureGroupDefinitionRecord", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("ExtraProperties")
                        .HasColumnType("text")
                        .HasColumnName("ExtraProperties");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("AbpFeatureGroups", (string)null);
                });

            modelBuilder.Entity("Volo.Abp.FeatureManagement.FeatureValue", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)");

                    b.Property<string>("ProviderName")
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.HasKey("Id");

                    b.HasIndex("Name", "ProviderName", "ProviderKey")
                        .IsUnique();

                    b.ToTable("AbpFeatureValues", (string)null);
                });

            modelBuilder.Entity("Volo.Abp.LanguageManagement.External.LocalizationResourceRecord", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<string>("BaseResources")
                        .HasMaxLength(1280)
                        .HasColumnType("character varying(1280)")
                        .HasColumnName("BaseResources");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("CreationTime");

                    b.Property<string>("DefaultCulture")
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)")
                        .HasColumnName("DefaultCulture");

                    b.Property<DateTime?>("LastModificationTime")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("LastModificationTime");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)")
                        .HasColumnName("Name");

                    b.Property<string>("SupportedCultures")
                        .HasMaxLength(640)
                        .HasColumnType("character varying(640)")
                        .HasColumnName("SupportedCultures");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("AbpLocalizationResources", (string)null);
                });

            modelBuilder.Entity("Volo.Abp.LanguageManagement.External.LocalizationTextRecord", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("CreationTime");

                    b.Property<string>("CultureName")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)")
                        .HasColumnName("CultureName");

                    b.Property<DateTime?>("LastModificationTime")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("LastModificationTime");

                    b.Property<string>("ResourceName")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)")
                        .HasColumnName("ResourceName");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasMaxLength(1048576)
                        .HasColumnType("character varying(1048576)")
                        .HasColumnName("Value");

                    b.HasKey("Id");

                    b.HasIndex("ResourceName", "CultureName")
                        .IsUnique();

                    b.ToTable("AbpLocalizationTexts", (string)null);
                });

            modelBuilder.Entity("Volo.Abp.LanguageManagement.Language", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("character varying(40)")
                        .HasColumnName("ConcurrencyStamp");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("CreationTime");

                    b.Property<Guid?>("CreatorId")
                        .HasColumnType("uuid")
                        .HasColumnName("CreatorId");

                    b.Property<string>("CultureName")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)")
                        .HasColumnName("CultureName");

                    b.Property<Guid?>("DeleterId")
                        .HasColumnType("uuid")
                        .HasColumnName("DeleterId");

                    b.Property<DateTime?>("DeletionTime")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("DeletionTime");

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("character varying(32)")
                        .HasColumnName("DisplayName");

                    b.Property<string>("ExtraProperties")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("ExtraProperties");

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false)
                        .HasColumnName("IsDeleted");

                    b.Property<bool>("IsEnabled")
                        .HasColumnType("boolean")
                        .HasColumnName("IsEnabled");

                    b.Property<DateTime?>("LastModificationTime")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("LastModificationTime");

                    b.Property<Guid?>("LastModifierId")
                        .HasColumnType("uuid")
                        .HasColumnName("LastModifierId");

                    b.Property<string>("UiCultureName")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)")
                        .HasColumnName("UiCultureName");

                    b.HasKey("Id");

                    b.HasIndex("CultureName");

                    b.ToTable("AbpLanguages", (string)null);
                });

            modelBuilder.Entity("Volo.Abp.LanguageManagement.LanguageText", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("CreationTime");

                    b.Property<Guid?>("CreatorId")
                        .HasColumnType("uuid")
                        .HasColumnName("CreatorId");

                    b.Property<string>("CultureName")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)")
                        .HasColumnName("CultureName");

                    b.Property<DateTime?>("LastModificationTime")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("LastModificationTime");

                    b.Property<Guid?>("LastModifierId")
                        .HasColumnType("uuid")
                        .HasColumnName("LastModifierId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(512)
                        .HasColumnType("character varying(512)")
                        .HasColumnName("Name");

                    b.Property<string>("ResourceName")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)")
                        .HasColumnName("ResourceName");

                    b.Property<Guid?>("TenantId")
                        .HasColumnType("uuid")
                        .HasColumnName("TenantId");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasMaxLength(65536)
                        .HasColumnType("character varying(65536)")
                        .HasColumnName("Value");

                    b.HasKey("Id");

                    b.HasIndex("TenantId", "ResourceName", "CultureName");

                    b.ToTable("AbpLanguageTexts", (string)null);
                });

            modelBuilder.Entity("Volo.Abp.PermissionManagement.PermissionDefinitionRecord", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("ExtraProperties")
                        .HasColumnType("text")
                        .HasColumnName("ExtraProperties");

                    b.Property<string>("GroupName")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<bool>("IsEnabled")
                        .HasColumnType("boolean");

                    b.Property<byte>("MultiTenancySide")
                        .HasColumnType("smallint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<string>("ParentName")
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<string>("Providers")
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<string>("StateCheckers")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("GroupName");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("AbpPermissions", (string)null);
                });

            modelBuilder.Entity("Volo.Abp.PermissionManagement.PermissionGrant", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<string>("ProviderKey")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)");

                    b.Property<string>("ProviderName")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)");

                    b.Property<Guid?>("TenantId")
                        .HasColumnType("uuid")
                        .HasColumnName("TenantId");

                    b.HasKey("Id");

                    b.HasIndex("TenantId", "Name", "ProviderName", "ProviderKey")
                        .IsUnique();

                    b.ToTable("AbpPermissionGrants", (string)null);
                });

            modelBuilder.Entity("Volo.Abp.PermissionManagement.PermissionGroupDefinitionRecord", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("ExtraProperties")
                        .HasColumnType("text")
                        .HasColumnName("ExtraProperties");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("AbpPermissionGroups", (string)null);
                });

            modelBuilder.Entity("Volo.Abp.SettingManagement.Setting", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)");

                    b.Property<string>("ProviderName")
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasMaxLength(2048)
                        .HasColumnType("character varying(2048)");

                    b.HasKey("Id");

                    b.HasIndex("Name", "ProviderName", "ProviderKey")
                        .IsUnique();

                    b.ToTable("AbpSettings", (string)null);
                });

            modelBuilder.Entity("Volo.Abp.SettingManagement.SettingDefinitionRecord", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<string>("DefaultValue")
                        .HasMaxLength(2048)
                        .HasColumnType("character varying(2048)");

                    b.Property<string>("Description")
                        .HasMaxLength(512)
                        .HasColumnType("character varying(512)");

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("ExtraProperties")
                        .HasColumnType("text")
                        .HasColumnName("ExtraProperties");

                    b.Property<bool>("IsEncrypted")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsInherited")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsVisibleToClients")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<string>("Providers")
                        .HasMaxLength(1024)
                        .HasColumnType("character varying(1024)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("AbpSettingDefinitions", (string)null);
                });

            modelBuilder.Entity("Volo.Abp.TextTemplateManagement.TextTemplates.TextTemplateContent", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("character varying(40)")
                        .HasColumnName("ConcurrencyStamp");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(65535)
                        .HasColumnType("character varying(65535)");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("CreationTime");

                    b.Property<Guid?>("CreatorId")
                        .HasColumnType("uuid")
                        .HasColumnName("CreatorId");

                    b.Property<string>("CultureName")
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)");

                    b.Property<Guid?>("DeleterId")
                        .HasColumnType("uuid")
                        .HasColumnName("DeleterId");

                    b.Property<DateTime?>("DeletionTime")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("DeletionTime");

                    b.Property<string>("ExtraProperties")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("ExtraProperties");

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false)
                        .HasColumnName("IsDeleted");

                    b.Property<DateTime?>("LastModificationTime")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("LastModificationTime");

                    b.Property<Guid?>("LastModifierId")
                        .HasColumnType("uuid")
                        .HasColumnName("LastModifierId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<Guid?>("TenantId")
                        .HasColumnType("uuid")
                        .HasColumnName("TenantId");

                    b.HasKey("Id");

                    b.ToTable("AbpTextTemplateContents", (string)null);
                });

            modelBuilder.Entity("Volo.Abp.TextTemplateManagement.TextTemplates.TextTemplateDefinitionContentRecord", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<Guid>("DefinitionId")
                        .HasColumnType("uuid");

                    b.Property<string>("FileContent")
                        .HasColumnType("text");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("DefinitionId");

                    b.ToTable("AbpTextTemplateDefinitionContentRecords", (string)null);
                });

            modelBuilder.Entity("Volo.Abp.TextTemplateManagement.TextTemplates.TextTemplateDefinitionRecord", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<string>("DefaultCultureName")
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)");

                    b.Property<string>("DisplayName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("ExtraProperties")
                        .HasColumnType("text")
                        .HasColumnName("ExtraProperties");

                    b.Property<bool>("IsInlineLocalized")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsLayout")
                        .HasColumnType("boolean");

                    b.Property<string>("Layout")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("LocalizationResourceName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<string>("RenderEngine")
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("AbpTextTemplateDefinitionRecords", (string)null);
                });

            modelBuilder.Entity("Volo.Abp.TextTemplateManagement.TextTemplates.TextTemplateDefinitionContentRecord", b =>
                {
                    b.HasOne("Volo.Abp.TextTemplateManagement.TextTemplates.TextTemplateDefinitionRecord", null)
                        .WithMany()
                        .HasForeignKey("DefinitionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
