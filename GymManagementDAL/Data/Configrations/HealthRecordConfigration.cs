using GymManagementDAL.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Data.Configrations
{
    internal class HealthRecordConfigration : IEntityTypeConfiguration<HealthRecord>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<HealthRecord> builder)
        {
            builder.ToTable("Members")
                .HasKey(X =>X.id);

            builder.HasOne<Member>()
                .WithOne(X => X.HealthRecord)
                .HasForeignKey<HealthRecord>(hr => hr.id);
                

        }
    }
}
