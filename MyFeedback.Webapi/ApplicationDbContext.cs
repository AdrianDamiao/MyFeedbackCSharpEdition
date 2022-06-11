using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyFeedback.Webapi.Models.Areas;
using MyFeedback.Webapi.Models.Colaboradores;
using MyFeedback.Webapi.Models.Empresas;
using MyFeedback.Webapi.Models.Feedbacks;
using MyFeedback.Webapi.Models.Funcoes;
using MyFeedback.Webapi.Models.Sessoes;

public class ApplicationDbContext : IdentityDbContext<Usuario>
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