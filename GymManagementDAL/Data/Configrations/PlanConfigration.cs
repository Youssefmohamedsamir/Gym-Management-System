using GymManagementDAL.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Data.Configrations
{
    internal class PlanConfigration : IEntityTypeConfiguration<Plan>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Plan> builder)
        {
            builder.HasKey(x => x.id);
            builder.Property(x => x.Name)
                .HasColumnType("varchar")
                .HasMaxLength(50);

            builder.Property(x => x.Description)
              .HasColumnType("varchar")
              .HasMaxLength(200);

            builder.Property(x => x.Price)
              .HasPrecision(10, 2)
              .HasMaxLength(50);

            builder.ToTable(Tb =>
            {
               Tb.HasCheckConstraint("ConstrainDurationValidation", "DurationDays Between 1 AND 365");
            });

        }
    }
}
