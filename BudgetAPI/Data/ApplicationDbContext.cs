using BudgetAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BudgetAPI.Data.Configuration
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<Income> Incomes { get; set; }

        public DbSet<User> Users { get; set; }
    }
}