﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MovieStoreMVC.Models.Domain
{
    public class DataBaseContext:IdentityDbContext<ApplicationUser>
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options):base(options)
        {
            
        }
    }
}
