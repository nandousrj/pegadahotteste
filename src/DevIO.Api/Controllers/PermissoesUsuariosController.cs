using AutoMapper;
using DevIO.Api.Extensions;
using DevIO.Api.ViewModels;
using DevIO.Business.Intefaces;
using DevIO.Business.Interfaces;
using DevIO.Business.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DevIO.Api.Controllers
{
    //  [Route("api/PermissoesUsuarios")]
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/PermissoesUsuarios")]
    public class PermissoesUsuariosController : MainController
    {
        private readonly IPermissoesUsuarioRepository _permissoesusuarioRepository;
        private readonly IPermissoesUsuarioService _permissoesusuarioService;
        private readonly AppSettings _appSettings;
        private readonly ILogger<PermissoesUsuariosController> _logger;
        private readonly IMapper _mapper;


        public PermissoesUsuariosController(IPermissoesUsuarioRepository permissoesusuarioRepository,
                                     IPermissoesUsuarioService permissoesusuarioService,
                                     IOptions<AppSettings> appSettings,
                                     INotificador notificador,
                                     ILogger<PermissoesUsuariosController> logger,
                                     IMapper mapper/*,
                                     IUser user*/) : base(notificador/*, user*/)
        {
            _permissoesusuarioRepository = permissoesusuarioRepository;
            _permissoesusuarioService = permissoesusuarioService;
            _appSettings = appSettings.Value;
            _logger = logger;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet("VerificarLogin")]
        private async Task<int> VerificarLogin(string login)
        {
            return await _permissoesusuarioRepository.VerificarLogin(login);
        }

        [AllowAnonymous]
        [HttpGet("VerificaUsuarioSistema")]
        private async Task<int> VerificaUsuarioSistema(int id_usuario, int id_sistema)
        {
            return await _permissoesusuarioRepository.VerificaUsuarioSistema(id_usuario, id_sistema);
        }


        [AllowAnonymous]
        [HttpGet("ConsultarUsuario")]
        public async Task<ActionResult<PermissoesUsuarioViewModel>> ObterPorId(string login)
        {
            var dados = _mapper.Map<PermissoesUsuarioViewModel>(await _permissoesusuarioRepository.ConsultarUsuario(login));

            if (dados == null) return NotFound(); //pra retornar NotFound tem que ter Action Result

            return dados;
        }


        [AllowAnonymous]
        [HttpGet("RetornarUsuarioId")]
        public async Task<ActionResult<PermissoesUsuarioViewModel>> RetornarUsuarioId(int id_usuario, int id_sistema)
        {
            var dados = _mapper.Map<PermissoesUsuarioViewModel>(await _permissoesusuarioRepository.RetornarUsuarioId(id_usuario, id_sistema));

            if (dados == null) return NotFound(); //pra retornar NotFound tem que ter Action Result

            return dados;
        }


        [AllowAnonymous]
        [HttpGet("obtertodos/{id:int}")]
        public async Task<IEnumerable<PermissoesUsuarioViewModel>> RetornarTodos(int id = 0)
        {
            var dados = _mapper.Map<IEnumerable<PermissoesUsuarioViewModel>>(await _permissoesusuarioRepository.RetornarTodos(id));

            return dados;
        }


        [AllowAnonymous]
        [HttpGet("RetornarUsuarioSemTermo")]
        public async Task<IEnumerable<PermissoesUsuarioViewModel>> RetornarUsuarioSemTermo(int id_sistema)
        {
            var dados = _mapper.Map<IEnumerable<PermissoesUsuarioViewModel>>(await _permissoesusuarioRepository.RetornarUsuarioSemTermo(id_sistema));

            return dados;
        }


        [AllowAnonymous]
        [HttpGet("RetornarUsuario")]
        public async Task<ActionResult<PermissoesUsuarioViewModel>> RetornarUsuario(int id_usuario)
        {
            var dados = _mapper.Map<PermissoesUsuarioViewModel>(await _permissoesusuarioRepository.RetornarUsuario(id_usuario));

            if (dados == null) return NotFound(); //pra retornar NotFound tem que ter Action Result

            return dados;
        }

        [AllowAnonymous]
        [HttpGet("RetornarTodosSistema")]
        public async Task<ActionResult<PermissoesUsuarioViewModel>> RetornarTodosSistema(int id_sistema)
        {
            var dados = _mapper.Map<PermissoesUsuarioViewModel>(await _permissoesusuarioRepository.RetornarTodosSistema(id_sistema));

            if (dados == null) return NotFound(); //pra retornar NotFound tem que ter Action Result

            return dados;
        }

        [AllowAnonymous]
        [HttpGet("RetornarUsuarioAcessoPagina")]
        public async Task<IEnumerable<PermissoesUsuarioViewModel>> RetornarUsuarioAcessoPagina(int id_menu)
        {
            var dados = _mapper.Map<IEnumerable<PermissoesUsuarioViewModel>>(await _permissoesusuarioRepository.RetornarUsuarioAcessoPagina(id_menu));

            return dados;
        }


        //fusinforj@gmail.com - 1980nerj
        [AllowAnonymous]
        [HttpPost("ValidarLogin")]
        //  public async Task<ActionResult<PermissoesUsuarioViewModel>> ValidarLogin(string login, string senha, int id_sistema)
        //   public async Task<ActionResult<AuthenticateResponse>> ValidarLogin(string login, string senha, int id_sistema)
        public async Task<ActionResult<AuthenticateResponse>> ValidarLogin(LoginUserViewModel usuarioLogin)
        {

            // var dados = _mapper.Map<PermissoesUsuario>(dadosViewModel);

            var valor = _mapper.Map<PermissoesUsuarioViewModel>(await _permissoesusuarioRepository.ValidarLogin(usuarioLogin.login, usuarioLogin.senha, usuarioLogin.id_sistema));

            if (valor != null)
            {
                if (valor.quantidade_tentativa == 0)
                {
                    var token = generateJwtToken(valor);
                    User user = new User
                    {
                        Id = valor.id_usuario,
                        FirstName = valor.login,
                        LastName = valor.email,
                        Username = valor.login
                    };
                    return new AuthenticateResponse(user, token);

                    //todo
                    // aqui fazer o processo do token
                    // return CustomResponse(await GerarJwt(valor));
                }
                else
                {
                    if (valor.quantidade_tentativa > 0 && valor.quantidade_tentativa <= 5)
                    {
                        NotificarErro("Login ou senha incorreto.");
                        return NotFound();
                    }
                    else if (valor.quantidade_tentativa == -10)
                    {
                        NotificarErro("Seu login foi bloqueado. <br/>Favor entrar em contato com o administrador do sistema.");
                        return NotFound();
                    }
                    else
                    {
                        NotificarErro("Login ou senha incorreto.");
                        return NotFound();
                    }
                }
            }
            else
            {
                NotificarErro("Login ou senha incorreto.");
                return NotFound();
            }

            //  if (valor == null) return NotFound(); //pra retornar NotFound tem que ter Action Result

            //  return valor;
            return null;
        }


        [AllowAnonymous]
        [HttpPost("AtivarUsuario")]
        public async Task AtivarUsuario(int id_usuario_acao, int id_usuario, string ip, int id_sistema)
        {
            await _permissoesusuarioRepository.AtivarUsuario(id_usuario_acao, id_usuario, ip, id_sistema);
        }

        [AllowAnonymous]
        [HttpGet("VerificarTrocaSenha")]
        public async Task<int> VerificarTrocaSenha(int id_usuario)
        {
            return await _permissoesusuarioRepository.VerificarTrocaSenha(id_usuario);
        }

        [AllowAnonymous]
        [HttpGet("VerificarUsuarioSistema")]
        private async Task<int> VerificarUsuarioSistema(int id_usuario, int id_sistema)
        {
            return await _permissoesusuarioRepository.VerificarUsuarioSistema(id_usuario, id_sistema);
        }

        [AllowAnonymous]
        [HttpPost("VerificarUsuarioSistema")]
        private async Task<int> AlterarSenha(PermissoesUsuarioViewModel dadosViewModel)
        {
            var dados = _mapper.Map<PermissoesUsuario>(dadosViewModel);
            return await _permissoesusuarioRepository.AlterarSenha(dados);
        }

        [AllowAnonymous]
        [HttpPost("ResetarSenha")]
        private async Task<int> ResetarSenha(PermissoesUsuarioViewModel dadosViewModel)
        {
            var dados = _mapper.Map<PermissoesUsuario>(dadosViewModel);
            return await _permissoesusuarioRepository.ResetarSenha(dados);
        }


        // [ClaimsAuthorize("PermissoesUsuario", "Adicionar")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<PermissoesUsuarioViewModel>> Adicionar(PermissoesUsuarioViewModel dadosViewModel)
        {
            //   if (ModelState.IsValid) return BadRequest();
            var dados = _mapper.Map<PermissoesUsuario>(dadosViewModel);
            var result = await _permissoesusuarioService.Incluir(dados);

            if (!result) return CustomResponse(dadosViewModel);//BadRequest();

            return Ok(dados);
        }

        //   [ClaimsAuthorize("PermissoesUsuario", "Atualizar")]
        [AllowAnonymous]
        [HttpPut("{id:int}")]
        public async Task<ActionResult<PermissoesUsuarioViewModel>> Atualizar(int id, PermissoesUsuarioViewModel dadosViewModel)
        {

            if (id != dadosViewModel.id_usuario)
            {
                NotificarErro("O id informado não é o mesmo que foi passado na query");
                return CustomResponse(dadosViewModel);
            }

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _permissoesusuarioService.Alterar(_mapper.Map<PermissoesUsuario>(dadosViewModel));

            return CustomResponse(dadosViewModel);
        }


        [AllowAnonymous]
        [HttpPost("GravarAceiteTermo")]
        public async Task GravarAceiteTermo(int id_usuario, int id_sistema)
        {
            await _permissoesusuarioRepository.GravarAceiteTermo(id_usuario, id_sistema);
        }



        private async Task<LoginResponseNewViewModel> GerarJwt2(PermissoesUsuarioViewModel dados)
        {
            //  var user = await _userManager.FindByEmailAsync(email);
            //    var claims = await _userManager.GetClaimsAsync(user);
            //   var userRoles = await _userManager.GetRolesAsync(user);

            List<Claim> claims = new List<Claim>();

            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, dados.id_usuario.ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, dados.email.ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())); // id do token gerado
            claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, ToUnixEpochDate(DateTime.UtcNow).ToString())); // not value vbefore
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64)); // Issuer at

            //foreach (var userRole in userRoles)
            //{
            //    claims.Add(new Claim("role", userRole));
            //}

            //var identityClaims = new ClaimsIdentity();
            //identityClaims.AddClaims(claims);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _appSettings.Emissor,
                Audience = _appSettings.ValidoEm,
                //   Expires = DateTime.UtcNow.AddHours(_appSettings.ExpiracaoHoras), // utc de universal, por ser tipos de datas em países diferentes(USA é yyy-mm-dd)
                Expires = DateTime.UtcNow.AddMinutes(_appSettings.ExpiracaoMinutos), // utc de universal, por ser tipos de datas em países diferentes(USA é yyy-mm-dd)
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            });

            var encodedToken = tokenHandler.WriteToken(token);

            var response = new LoginResponseNewViewModel
            {
                AccessToken = encodedToken,
                //  ExpiresIn = TimeSpan.FromHours(_appSettings.ExpiracaoHoras).TotalSeconds,
                ExpiresIn = TimeSpan.FromMinutes(_appSettings.ExpiracaoMinutos).TotalSeconds,
                UserToken = new PermissoesUsuarioTokenViewModel
                {
                    id_usuario = dados.id_usuario.ToString(),
                    login = dados.login.ToString(),
                    email = dados.email.ToString(),
                    id_perfil = dados.id_perfil.ToString(),
                    Claims = claims.Select(c => new ClaimViewModel { Type = c.Type, Value = c.Value })
                }
            };

            return response;
        }


        private async Task<LoginResponseNewViewModel> GerarJwt(PermissoesUsuarioViewModel dados)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", dados.id_usuario.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var teste = tokenHandler.WriteToken(token);
            var response = new LoginResponseNewViewModel
            {
                AccessToken = teste
            };

            return response;
        }

        private string generateJwtToken(PermissoesUsuarioViewModel dados)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", dados.id_usuario.ToString()) }),
                Expires = DateTime.UtcNow.AddMinutes(_appSettings.ExpiracaoMinutos),//DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }


        private static long ToUnixEpochDate(DateTime date)
            => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);


    }
}
