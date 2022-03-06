using Microsoft.EntityFrameworkCore;
using MyFeedback.Models.Areas;
using MyFeedback.Models.Colaboradores;
using MyFeedback.Models.Empresas;
using MyFeedback.Models.Feedbacks;
using MyFeedback.Models.Funcoes;

public class ApplicationDbContext : DbContext
{
    public DbSet<Area> Areas { get; set; }
    public DbSet<Colaborador> Colaboradores { get; set; }
    public DbSet<Empresa> Empresas { get; set; }
    public DbSet<Feedback> Feedbacks { get; set; }
    public DbSet<Funcao> Funcoes { get; set; }
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
}