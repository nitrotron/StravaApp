using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class DataContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Oauth> Oauths {get; set;}
    public DbSet<Athlete> Athletes {get; set;}
    public DbSet<Activity> Activities {get; set;}
}