using System;
using System.Linq;
using System.Threading.Tasks;
using DevIO.Business.Interfaces;
using DevIO.Business.Models;
using DevIO.Business.Models.Validations;

namespace DevIO.Business.Services
{
    public class AtendeService : BaseService, IAtendeService
    {
        private readonly IAtendeRepository _atendeRepository;

        public AtendeService(IAtendeRepository atendeRepository,
                                 INotificador notificador) : base(notificador)
        {
            _atendeRepository = atendeRepository;
        }


        public async Task<bool> Incluir(Atende valor) 
        {
            if (!ExecutarValidacao(new AtendeValidation(), valor)
                //  || !ExecutarValidacao(new EnderecoValidation(), fornecedor.Endereco)
                ) return false;

            var dados = await _atendeRepository.ObterPorNomePOC(valor.desc_atende.ToUpper().Trim());

            if (dados != null)
            {
                if (dados.desc_atende.ToUpper().Trim() == valor.desc_atende.ToUpper().Trim())
                {
                    Notificar("Já existe um Atendimento com esta descrição informada.");
                    return false;
                }
            }

            await _atendeRepository.IncluirPOC(valor);
            return true;
        }

        public async Task<bool> Alterar(Atende valor)
        {
            if (!ExecutarValidacao(new AtendeValidation(), valor)) return false;

            var dados = await _atendeRepository.ObterPorNomePOC(valor.desc_atende.ToUpper().Trim());
            if (dados.desc_atende.ToUpper().Trim() == valor.desc_atende.ToUpper().Trim()
                && dados.id_atende != valor.id_atende)
            {
                Notificar("Já existe um Atendiemento com esta descrição informada.");
                return false;
            }

            await _atendeRepository.AlterarPOC(valor);
            return true;
        }

        public async Task Excluir(int id)
        {
            await _atendeRepository.ExlcuirPOC(id);
        }

        public void Dispose()
        {
            _atendeRepository?.Dispose();
        }
    }
}