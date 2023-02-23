using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SixMinAPI.Models;

namespace SixMinAPI.Data
{
    public class AppDataContext :DbContext
    {
        public AppDataContext(DbContextOptions<AppDataContext> options):base(options)
        {
            
        }

        public DbSet<Command> Commands=> Set<Command>();
        
    }
}