using System;
using System.Linq;
using System.Threading.Tasks;
using DevIO.Business.Interfaces;
using DevIO.Business.Models;
using DevIO.Business.Models.Validations;

namespace DevIO.Business.Services
{
    public class PermissoesSistemaService : BaseService, IPermissoesSistemaService
    {
        private readonly IPermissoesSistemaRepository _permissoessistemaRepository;

        public PermissoesSistemaService(IPermissoesSistemaRepository permissoessistemaRepository,
                                 INotificador notificador) : base(notificador)
        {
            _permissoessistemaRepository = permissoessistemaRepository;
        }


        public async Task<bool> Incluir(PermissoesSistema valor) 
        {
            if (!ExecutarValidacao(new PermissoesSistemaValidation(), valor)
                //  || !ExecutarValidacao(new EnderecoValidation(), fornecedor.Endereco)
                ) return false;

            var dados = await _permissoessistemaRepository.ObterPorNomePOC(valor.nome.ToUpper().Trim());

            if (dados != null)
            {
                if (dados.nome.ToUpper().Trim() == valor.nome.ToUpper().Trim())
                {
                    Notificar("Já existe um Nome com esta descrição informada.");
                    return false;
                }
            }

            await _permissoessistemaRepository.IncluirPOC(valor);
            return true;
        }

        public async Task<bool> Alterar(PermissoesSistema valor)
        {
            if (!ExecutarValidacao(new PermissoesSistemaValidation(), valor)) return false;

            var dados = await _permissoessistemaRepository.ObterPorNomePOC(valor.nome.ToUpper().Trim());
            if (dados.nome.ToUpper().Trim() == valor.nome.ToUpper().Trim()
                && dados.id_sistema != valor.id_sistema)
            {
                Notificar("Já existe um Nome com esta descrição informada.");
                return false;
            }

            await _permissoessistemaRepository.AlterarPOC(valor);
            return true;
        }

        public async Task Excluir(int id)
        {
            await _permissoessistemaRepository.ExlcuirPOC(id);
        }

        public void Dispose()
        {
            _permissoessistemaRepository?.Dispose();
        }
    }
}