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
    internal class MemberShipConfigration : IEntityTypeConfiguration<MemberShip>
    {
        public void Configure(EntityTypeBuilder<MemberShip> builder)
        {
            builder.Property(x => x.CreatedAt)
                .HasColumnName("StartDate")
                .HasColumnType("datetime2")
            .HasDefaultValueSql("Getdate()");

            builder.HasKey(x => new { x.MemberId, x.PlanId });
            builder.Ignore(x => x.id);
        }

       
    }
}
