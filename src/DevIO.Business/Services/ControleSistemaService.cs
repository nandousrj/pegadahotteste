using System;
using System.Linq;
using System.Threading.Tasks;
using DevIO.Business.Interfaces;
using DevIO.Business.Models;
using DevIO.Business.Models.Validations;

namespace DevIO.Business.Services
{
    public class ControleSistemaService : BaseService, IControleSistemaService
    {
        private readonly IControleSistemaRepository _controlesistemaRepository;

        public ControleSistemaService(IControleSistemaRepository controlesistemaRepository,
                                 INotificador notificador) : base(notificador)
        {
            _controlesistemaRepository = controlesistemaRepository;
        }


        public async Task<bool> Incluir(ControleSistema valor) 
        {
            if (!ExecutarValidacao(new ControleSistemaValidation(), valor)
                //  || !ExecutarValidacao(new EnderecoValidation(), fornecedor.Endereco)
                ) return false;

            var dados = await _controlesistemaRepository.ObterPorNomePOC(valor.desc_controle.ToUpper().Trim());

            if (dados != null)
            {
                if (dados.desc_controle.ToUpper().Trim() == valor.desc_controle.ToUpper().Trim())
                {
                    Notificar("Já existe um Controle Sistema com esta descrição informada.");
                    return false;
                }
            }

            await _controlesistemaRepository.IncluirPOC(valor);
            return true;
        }

        public async Task<bool> Alterar(ControleSistema valor)
        {
            if (!ExecutarValidacao(new ControleSistemaValidation(), valor)) return false;

            var dados = await _controlesistemaRepository.ObterPorNomePOC(valor.desc_controle.ToUpper().Trim());
            if (dados.desc_controle.ToUpper().Trim() == valor.desc_controle.ToUpper().Trim()
                && dados.id_controle_sistema != valor.id_controle_sistema)
            {
                Notificar("Já existe um Controle Sistema com esta descrição informada.");
                return false;
            }

            await _controlesistemaRepository.AlterarPOC(valor);
            return true;
        }

        public async Task Excluir(int id)
        {
            await _controlesistemaRepository.ExlcuirPOC(id);
        }

        public void Dispose()
        {
            _controlesistemaRepository?.Dispose();
        }
    }
}