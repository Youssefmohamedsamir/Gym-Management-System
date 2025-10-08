using GymManagementDAL.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Data.Configrations
{
    internal class SessionConfigration : IEntityTypeConfiguration<Session>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Session> builder)
        {
            builder.Property(x => x.Capacity)
                .HasPrecision(1 , 25);

            builder.ToTable(Tb =>
            {
              Tb.HasCheckConstraint("ConstrainCapacityValidation", "Capacity Between 1 AND 25");
            });

            builder.HasOne(X => X.Category)
                .WithMany(t => t.Sessions)
                .HasForeignKey(s => s.CategoryId);

            builder.HasOne(X => X.Trainer)
                .WithMany(t => t.Sessions)
                .HasForeignKey(s => s.TrainerId);

        }
    }
}
