using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Skipper.Models;

namespace Skipper.Data
{
    public class SkipperContext : DbContext
    {
        public SkipperContext (DbContextOptions<SkipperContext> options)
            : base(options)
        {
        }

        public DbSet<Skipper.Models.User> User { get; set; } = default!;
    }
}
