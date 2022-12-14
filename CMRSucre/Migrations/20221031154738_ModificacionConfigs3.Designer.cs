// <auto-generated />
using System;
using CMRSucre.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CMRSucre.Migrations
{
    [DbContext(typeof(CMRSucreContext))]
    [Migration("20221031154738_ModificacionConfigs3")]
    partial class ModificacionConfigs3
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("CMRSucre.Models.Certificado", b =>
                {
                    b.Property<int>("CertificadoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CertificadoId"), 1L, 1);

                    b.Property<int>("Cantidad")
                        .HasColumnType("int");

                    b.Property<DateTime>("Emision")
                        .HasColumnType("datetime2");

                    b.Property<int>("SocioId")
                        .HasColumnType("int");

                    b.HasKey("CertificadoId");

                    b.HasIndex("SocioId");

                    b.ToTable("Certificados");
                });

            modelBuilder.Entity("CMRSucre.Models.Config", b =>
                {
                    b.Property<int>("ConfigId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ConfigId"), 1L, 1);

                    b.Property<DateTime>("Fecha_Cierre")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Utilidad")
                        .HasPrecision(14, 1)
                        .HasColumnType("decimal(14,1)");

                    b.HasKey("ConfigId");

                    b.ToTable("Configs");
                });

            modelBuilder.Entity("CMRSucre.Models.Socio", b =>
                {
                    b.Property<int>("SocioId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SocioId"), 1L, 1);

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(90)
                        .HasColumnType("nvarchar(90)");

                    b.HasKey("SocioId");

                    b.ToTable("Socios");
                });

            modelBuilder.Entity("CMRSucre.Models.Certificado", b =>
                {
                    b.HasOne("CMRSucre.Models.Socio", "Socio")
                        .WithMany("Certificados")
                        .HasForeignKey("SocioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Socio");
                });

            modelBuilder.Entity("CMRSucre.Models.Socio", b =>
                {
                    b.Navigation("Certificados");
                });
#pragma warning restore 612, 618
        }
    }
}
