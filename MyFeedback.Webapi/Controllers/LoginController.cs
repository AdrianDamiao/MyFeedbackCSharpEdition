using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MyFeedback.Webapi.Models.Sessoes;

namespace MyFeedback.Webapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private IConfiguration _configuration; 

        public LoginController(UserManager<Usuario> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("registro")]
        public async Task<IActionResult> RegistraUsuario([FromBody] RegisterModel model)
        {
            var usuarioExiste = await _userManager.FindByNameAsync(model.Username);

            if(usuarioExiste != null)
                return StatusCode(StatusCodes.Status409Conflict, new Response { Status = "Erro", Mensagem = "Usuário já existente."}); 
        
            Usuario usuario = new Usuario
            {
                UserName = model.Username,
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
            };

            var result = await _userManager.CreateAsync(usuario, model.Password);

            if(!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Erro", Mensagem = "Erro ao criar usuário, tente novamente." });
        
            return Ok(new Response { Status = "Sucesso", Mensagem = "Usuário criado com sucesso!" });
        }

        [HttpPost]
        [Route("registro-admin")]
        public async Task<IActionResult> RegistraUsuarioAdmin([FromBody] RegisterModel model)
        {
            var usuarioExiste = await _userManager.FindByNameAsync(model.Username);

            if(usuarioExiste != null)
                return StatusCode(StatusCodes.Status409Conflict, new Response { Status = "Erro", Mensagem = "Usuário já existente."}); 
        
            Usuario usuario = new Usuario
            {
                UserName = model.Username,
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
            };

            var result = await _userManager.CreateAsync(usuario, model.Password);

            if(!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Erro", Mensagem = "Erro ao criar usuário, tente novamente." });

            if (!await _roleManager.RoleExistsAsync("Admin"))
                await _roleManager.CreateAsync(new IdentityRole("Admin"));
            if (!await _roleManager.RoleExistsAsync("Usuario"))
                await _roleManager.CreateAsync(new IdentityRole("Usuario"));
            if (await _roleManager.RoleExistsAsync("Admin"))
            {
                await _userManager.AddToRoleAsync(usuario, "Admin");
            }

            return Ok(new Response { Status = "Sucesso", Mensagem = "Usuário criado com sucesso!" });
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var usuario = await _userManager.FindByNameAsync(model.Username);

            if(usuario != null && await _userManager.CheckPasswordAsync(usuario, model.Password))
            {
                var rolesDoUsuario = await _userManager.GetRolesAsync(usuario);
                var claimsDeAutenticacao = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, usuario.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                foreach(var role in rolesDoUsuario)
                {
                    claimsDeAutenticacao.Add(new Claim(ClaimTypes.Role, role));
                }

                var chaveDeAutenticacao = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"]));
                var token = new JwtSecurityToken(
                    issuer: null,
                    audience: null,
                    expires: DateTime.Now.AddHours(12),
                    claims: claimsDeAutenticacao,
                    signingCredentials: new SigningCredentials(chaveDeAutenticacao, SecurityAlgorithms.HmacSha256)
                );

                return Ok(new 
                { 
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expires = token.ValidTo,
                });
            }

            return Unauthorized("Usuário ou senha inexistentes.");
        }
    }
}