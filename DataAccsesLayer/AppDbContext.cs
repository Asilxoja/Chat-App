using DataAccsesLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccsesLayer;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
}
