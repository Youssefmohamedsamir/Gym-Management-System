﻿using GymManagementDAL.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Entity
{
    public abstract class GymUser:BaseEntity
    {
        public int id { get; set; } 
        public string Name { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Phone { get; set; } = null!;

        public DateOnly DateOfBirth { get; set; } 

        public Gender Gender { get; set; }

        public Address Address { get; set; } = null!;
    }
    [Owned]
    public class Address
    {
        public int BuildingNo { get; set; }
        public string Street { get; set; } = null!;
        public string City { get; set; } = null!;
        public int BuildingNumber { get; set; }
    }
}
