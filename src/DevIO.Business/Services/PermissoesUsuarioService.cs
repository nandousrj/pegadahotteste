using System;
using System.Linq;
using System.Threading.Tasks;
using DevIO.Business.Interfaces;
using DevIO.Business.Models;
using DevIO.Business.Models.Validations;

namespace DevIO.Business.Services
{
    public class PermissoesUsuarioService : BaseService, IPermissoesUsuarioService
    {
        private readonly IPermissoesUsuarioRepository _permissoesusuarioRepository;

        public PermissoesUsuarioService(IPermissoesUsuarioRepository permissoesusuarioRepository,
                                 INotificador notificador) : base(notificador)
        {
            _permissoesusuarioRepository = permissoesusuarioRepository;
        }

        //TODO: separar a senha do incluir com a do alterar
        public async Task<bool> Incluir(PermissoesUsuario valor) 
        {
            if (!ExecutarValidacao(new PermissoesUsuarioValidation(), valor)
                //  || !ExecutarValidacao(new EnderecoValidation(), fornecedor.Endereco)
                ) return false;

            int idVerificaLogin = await _permissoesusuarioRepository.VerificarLogin(valor.login);

            if (idVerificaLogin > 0)
            {
                Notificar("Já existe um usuário com este login.");
                return false;
            }
            else
            {
                await _permissoesusuarioRepository.IncluirPOC(valor);
                return true;
            }
        }

        public async Task<bool> Alterar(PermissoesUsuario valor)
        {
            if (!ExecutarValidacao(new PermissoesUsuarioValidation(), valor)
                 //  || !ExecutarValidacao(new EnderecoValidation(), fornecedor.Endereco)
                 ) return false;

            await _permissoesusuarioRepository.AlterarPOC(valor);
            return true;
        }

        public async Task<PermissoesUsuario> RetornarUsuario(int id_usuario)
        {
          return  await _permissoesusuarioRepository.RetornarUsuario(id_usuario);           
        }

        //public async Task Excluir(int id)
        //{
        //    await _permissoesusuarioRepository.ExlcuirPOC(id);
        //}

        public void Dispose()
        {
            _permissoesusuarioRepository?.Dispose();
        }
    }
}