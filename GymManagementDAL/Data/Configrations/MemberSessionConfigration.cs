﻿using GymManagementDAL.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Data.Configrations
{
    internal class MemberSessionConfigration : IEntityTypeConfiguration<MemberSession>
    {
        public void Configure(EntityTypeBuilder<MemberSession> builder)
        {
           builder.Property(x => x.CreatedAt)
                 .HasColumnName("BookingDate")
             .HasDefaultValueSql("Getdate()");

            builder.HasKey(x => new { x.MemberId, x.SessionId });
            builder.Ignore(x => x.id);
        }
    }
}
