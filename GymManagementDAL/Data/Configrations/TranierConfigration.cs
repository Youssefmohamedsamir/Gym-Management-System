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
    internal class TranierConfigration : GymUserConfiguration<Trainer>, IEntityTypeConfiguration<Trainer>
    {
        public new void Configure(EntityTypeBuilder<Trainer> builder)
        {
            builder.Property(x => x.CreatedAt)
                .HasColumnName("HireDate")
                .HasColumnType("date");


    //        builder.Property(t => t.CreatedAt)
    //.HasConversion(d => d.ToDateTime(TimeOnly.MinValue),dt => DateOnly.FromDateTime(dt))
    //   .HasColumnType("date");



            base.Configure(builder);


        }
    }
}
