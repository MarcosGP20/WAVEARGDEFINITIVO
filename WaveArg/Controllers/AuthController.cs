using Data;
using Data.Dtos;
using Data.Entities;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WaveArg.Interfaces;

namespace WaveArg.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly ApplicationDbContext _context; //Consultar y guardar usuarios
        private readonly ITokenService _tokenService; //Generar tokens JWT
        private readonly IPasswordHasher<Usuarios> _passwordHasher; //Para hashear/verificar contraseñas


        public AuthController(ApplicationDbContext context, ITokenService tokenService, IPasswordHasher<Usuarios> passwordHasher)
        {
            _context = context;
            _tokenService = tokenService;
            _passwordHasher = passwordHasher;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            
            //Crea un nuevo usuario, asignandole mail (del request), provider, fecha y rol "Usuario"
            var user = new Usuarios
            {
                Email = request.Email,
                Provider = "Local",
                FechaRegistro = DateTime.UtcNow,
                Rolid = 1
            };


            //Hashea la contraseña
            user.Contraseña = _passwordHasher.HashPassword(user, request.Contraseña);

            //Guardo el usuario
            _context.Usuarios.Add(user);
            await _context.SaveChangesAsync();

            return Ok("Usuario Registrado");
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var user = await _context.Usuarios
        .Include(u => u.Rol)
        .FirstOrDefaultAsync(u => u.Email == request.Email);

            if (user == null)
                return BadRequest("Usuario no encontrado");

            if (string.IsNullOrEmpty(user.Contraseña))
                return BadRequest("Este usuario no tiene contraseña registrada");

            var result = _passwordHasher.VerifyHashedPassword(user, user.Contraseña, request.Contraseña);

            if (result == PasswordVerificationResult.Failed)
                return Unauthorized("Contraseña incorrecta");

            var token = _tokenService.GenerateToken(user);

            return Ok(new
            {
                token,
                rol = user.Rol.Nombre
            });


        }



        [HttpPost("google")]
        public async Task<IActionResult> GoogleLogin(GoogleLoginRequest request)
        {
            var payload = await GoogleJsonWebSignature.ValidateAsync(request.IdToken);

            var user = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == payload.Email && u.Provider == "Google");

            if (user == null)
            {
                user = new Usuarios
                {
                    Email = payload.Email,
                    Provider = "Google",
                    FechaRegistro = DateTime.UtcNow
                };
                _context.Usuarios.Add(user);
                await _context.SaveChangesAsync();
            }

            var token = _tokenService.GenerateToken(user);
            return Ok(new { token });

        }
    }
}
