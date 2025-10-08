using GymManagementDAL.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Data.Configrations
{
    internal class GymUserConfiguration<T> : IEntityTypeConfiguration<T> where T : GymUser
    {
        public void Configure(EntityTypeBuilder<T> builder)
        {
            builder.Property(x => x.Name)
                .HasColumnType("varchar")
                .HasMaxLength(50);

            builder.Property(x => x.Email)
                .HasColumnType("varchar")
                .HasMaxLength(100);

            builder.Property(x => x.Phone)
                .HasColumnType("varchar")
                .HasMaxLength(11);


            builder.ToTable(Tb =>
            {
                Tb.HasCheckConstraint("ConstrainEmailValidation", "Email Like '_%@_%._%'");
                Tb.HasCheckConstraint("ConstrainPhoneValidation", "Phone Like '01%' and Phone Not Like '%[^0-9]%'");
            });
            builder.HasIndex(x => x.Email).IsUnique();
            builder.HasIndex(x => x.Phone).IsUnique();

            builder.OwnsOne(x => x.Address, AddessBuilder =>
            {
                AddessBuilder.Property(a => a.BuildingNo)
                .HasColumnName("BuildingNo")
                .HasColumnType("int");

                AddessBuilder.Property(a => a.Street)
                .HasColumnName("Street")
                .HasColumnType("varchar")
                .HasMaxLength(30);

                AddessBuilder.Property(a => a.City)
                .HasColumnName("City")
                .HasColumnType("varchar")
                .HasMaxLength(30);
            });


        }
    }
}
