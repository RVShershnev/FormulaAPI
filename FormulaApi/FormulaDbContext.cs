using FormulaApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace FormulaApi
{
    public class FormulaDbContext : DbContext
    {
        public FormulaDbContext(DbContextOptions<FormulaDbContext> options) : base(options)
        {
        }

        public DbSet<LongCalculation> Items { get; set; }
    }
}
