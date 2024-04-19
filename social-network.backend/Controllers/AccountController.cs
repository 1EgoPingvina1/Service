using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using social_network.backend.DTOs;
using social_network.backend.Entities.Identity;
using social_network.backend.Interfaces;
using social_network.backend.mongodb.model;
using social_network.backend.mongodb.settings;

namespace social_network.backend.Controllers
{

    public class AccountController : BaseController
    {
        private readonly UserManager<User> _userManager;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        public readonly IMongodbSettings _settings;

        public AccountController(UserManager<User> userManager,
               ITokenService tokenService,
               IMapper mapper,
               IEmailService emailService,
               IMongodbSettings settings)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _mapper = mapper;
            _emailService = emailService;
            _settings = settings;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDTO>> Register(RegisterDTO registerDto)
        {
            if (await UserExists(registerDto.Username)) return BadRequest("Пользователь уже существует");

            var user = _mapper.Map<User>(registerDto);

            user.UserName = registerDto.Username.ToLower();

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded) return BadRequest(result.Errors);

            var roleResult = await _userManager.AddToRoleAsync(user, "Member");

            if (!roleResult.Succeeded) return BadRequest(result.Errors);

            return new UserDTO
            {
                Username = user.UserName,
                Token = await _tokenService.CreateToken(user)
            };
        }

        [HttpPost("login")]
        [Obsolete]
        public async Task<ActionResult<UserDTO>> Login(LoginDTO loginDTO)
        {
            var user = await _userManager.Users
                .SingleOrDefaultAsync(x => x.UserName == loginDTO.Username);

            if (user == null) 
                return Unauthorized("Неверный логин/пароль");

            var result = await _userManager.CheckPasswordAsync(user, loginDTO.Password);

            if (!result) 
                return Unauthorized("Неверный пароль!");

            if (user.TwoFactorEnabled)
            {
                var code = await _userManager.GenerateTwoFactorTokenAsync(user, "Email");

                await _emailService.SendTwoFactorCodeAsync(user.Email, code);
                var client = new MongoClient("mongodb://mongoadmin:ADMIN@localhost:27017/?authSource=admin");
                var database = client.GetDatabase("EmailAuthorize");
                var collection = database.GetCollection<UserToken>("userToken");
                await collection.Indexes.CreateOneAsync(
                    Builders<UserToken>.IndexKeys.Ascending(k => k.WasDeleted),
                    new CreateIndexOptions
                    {
                        ExpireAfter = new TimeSpan(0, 1, 0)
                    });
                var token = new UserToken
                {
                    UserName = user.UserName,
                    Token = code,
                    WasDeleted = DateTime.UtcNow
                };
                
                collection.InsertOne(token);
                return Ok($"Код подтверждения аккаунта был отправлен на почту {user.Email}");
            }
            return new UserDTO
            {
                Username = user.UserName,
                Token = await _tokenService.CreateToken(user),
            };
        }

        [HttpPost("login-2FA")]
        public async Task<IActionResult> LoginWithTwoFactoryAuthorize(string code,string username)
        {
            var client = new MongoClient("mongodb://mongoadmin:ADMIN@localhost:27017/?authSource=admin");
            var database = client.GetDatabase("EmailAuthorize");
            var collection ="userToken";
            var user = await _userManager.Users.SingleOrDefaultAsync(u => u.UserName == username);
            var tokencheck = await database.GetCollection<UserToken>(collection).Find(t => t.Token == code).FirstOrDefaultAsync();
            if(tokencheck != null)
            {
                return Ok(new UserDTO
                {
                    Username = user.UserName,
                    Token = await _tokenService.CreateToken(user),
                });
            }
            return Unauthorized("Аккаунт не был подтверждён");
        }

        [HttpPost("EnableEmailAuthorize")]
        public async Task<ActionResult<IdentityUser>> EnableTwoFactoreAuthorize(int userId)
        {
            var user = await _userManager.Users.SingleOrDefaultAsync(i => i.Id == userId);
            if (user != null)
            {
                user.TwoFactorEnabled = true;
                user.EmailConfirmed = true;
                return Ok(await _userManager.UpdateAsync(user));
            }
            return BadRequest("User was not found");
        }

        [HttpPost("DisableEmailAuthorize")]
        public async Task<ActionResult<IdentityUser>> DisableTwoFactoreAuthorize(int userId){
            var user = await _userManager.Users.SingleOrDefaultAsync(i => i.Id == userId);
            if (user != null)
            {
                user.TwoFactorEnabled = false;
                user.EmailConfirmed = false;
                return Ok(await _userManager.UpdateAsync(user));
            }
            return BadRequest("User was not found");
        }

        private async Task<bool> UserExists(string username) => 
            await _userManager.Users.AnyAsync(x => x.UserName == username.ToLower());
    }
}