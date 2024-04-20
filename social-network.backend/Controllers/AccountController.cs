//using AutoMapper;
//using MongoDB.Driver;
//using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Options;
using social_network.backend.DTOs;
//using social_network.backend.Entities.Identity;
//using social_network.backend.Interfaces;
//using social_network.backend.mongodb.model;
//using social_network.backend.mongodb.settings;
using MediatR;
using social_network.backend.CQRS.AccountService.Commands;

namespace social_network.backend.Controllers
{

    public class AccountController : BaseController
    {
        //private readonly UserManager<User> _userManager;
        //private readonly ITokenService _tokenService;
        //private readonly IEmailService _emailService;
        //private readonly MongodbSettings _mongodbSettings;
        //private readonly IMongoClient _client;
        //private readonly IMongoDatabase _database;
        //private readonly IMongoCollection<UserToken> _collection;
        private readonly IMediator _mediator;
        public AccountController(IMediator mediator)
        //    UserManager<User> userManager,
        //       ITokenService tokenService,
        //       IMapper mapper,
        //       IEmailService emailService,
        //       IOptions<MongodbSettings> mongodbSettings)
        {
            _mediator = mediator;
            //_userManager = userManager;
            //_tokenService = tokenService;
            //_emailService = emailService;
            //_mongodbSettings = mongodbSettings.Value;
            //_client = new MongoClient(_mongodbSettings.ConnectionString);
            //_database = _client.GetDatabase(_mongodbSettings.DatabaseName);
            //_collection = _database.GetCollection<UserToken>(_mongodbSettings.CollectionName);
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDTO>> Register(RegisterDTO registerDto)
            => await _mediator.Send(new RegisterCommand{RegisterDTO = registerDto });

        [HttpPost("login")]
        public async Task<ActionResult<UserDTO>> Login(LoginDTO loginDTO)
        => await _mediator.Send(new LoginCommand{ LoginDTO = loginDTO });

        [HttpPost("login-2FA")]
        public async Task<ActionResult<UserDTO>> LoginWithTwoFactoryAuthorize(string code, string username)
        => await _mediator.Send(new TwoFactoryAuthorizeCommand { Username = username,Code = code });

            //var user = await _userManager.Users.SingleOrDefaultAsync(u => u.UserName == username);
            //var tokencheck = await _database.GetCollection<UserToken>(_collection.ToString()).Find(t => t.Token == code).FirstOrDefaultAsync();
            //if (tokencheck != null)
            //{
            //    return Ok(new UserDTO
            //    {
            //        Username = user.UserName,
            //        Token = await _tokenService.CreateToken(user),
            //    });
            //}
            //return Unauthorized("Аккаунт не был подтверждён");
        
        //var user = await _userManager.Users.SingleOrDefaultAsync(x => x.UserName == loginDTO.Username);
        //if (user == null)
        //    return Unauthorized("Неверный логин/пароль");

        //var result = await _userManager.CheckPasswordAsync(user, loginDTO.Password);
        //if (!result)
        //    return Unauthorized("Неверный пароль!");

        //if (user.TwoFactorEnabled)
        //    return await HandleTwoFactorLogin(user);

        //return await GenerateTokenAndReturnUserDTO(user);


        //private async Task<ActionResult<UserDTO>> HandleTwoFactorLogin(User user)
        //{
        //    var code = await _userManager.GenerateTwoFactorTokenAsync(user, "Email");
        //    await _emailService.SendTwoFactorCodeAsync(user.Email, code);
        //    await SaveUserTokenToMongoDB(user.UserName, code);
        //    return Ok($"Код подтверждения аккаунта был отправлен на почту {user.Email}");
        //}

        //private async Task<ActionResult<UserDTO>> GenerateTokenAndReturnUserDTO(User user)
        //{
        //    var token = await _tokenService.CreateToken(user);
        //    return new UserDTO
        //    {
        //        Username = user.UserName,
        //        Token = token
        //    };
        //}

        //private async Task SaveUserTokenToMongoDB(string userName, string code)
        //{
        //    var token = new UserToken
        //    {
        //        UserName = userName,
        //        Token = code,
        //        WasDeleted = DateTime.Now
        //    };
        //    var indexModel = new CreateIndexModel<UserToken>(
        //        Builders<UserToken>.IndexKeys.Ascending(k => k.WasDeleted),
        //        new CreateIndexOptions
        //        {
        //            ExpireAfter = TimeSpan.FromSeconds(30)
        //        });
        //    await _collection.Indexes.CreateOneAsync(indexModel);
        //    await _collection.InsertOneAsync(token);
        //}




        //[HttpPost("EnableEmailAuthorize")]
        //public async Task<ActionResult<IdentityUser>> EnableTwoFactoreAuthorize(int userId)
        //{
        //    var user = await _userManager.Users.SingleOrDefaultAsync(i => i.Id == userId);
        //    if (user != null)
        //    {
        //        user.TwoFactorEnabled = true;
        //        user.EmailConfirmed = true;
        //        return Ok(await _userManager.UpdateAsync(user));
        //    }
        //    return BadRequest("User was not found");
        //}

        //[HttpPost("DisableEmailAuthorize")]
        //public async Task<ActionResult<IdentityUser>> DisableTwoFactoreAuthorize(int userId){
        //    var user = await _userManager.Users.SingleOrDefaultAsync(i => i.Id == userId);
        //    if (user != null)
        //    {
        //        user.TwoFactorEnabled = false;
        //        user.EmailConfirmed = false;
        //        return Ok(await _userManager.UpdateAsync(user));
        //    }
        //    return BadRequest("User was not found");
        //}
    }
}