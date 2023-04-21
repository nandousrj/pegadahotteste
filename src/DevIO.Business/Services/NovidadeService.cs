using System;
using System.Linq;
using System.Threading.Tasks;
using DevIO.Business.Interfaces;
using DevIO.Business.Models;
using DevIO.Business.Models.Validations;

namespace DevIO.Business.Services
{
    public class NovidadeService : BaseService, INovidadeService
    {
        private readonly INovidadeRepository _novidadeRepository;

        public NovidadeService(INovidadeRepository novidadeRepository,
                                 INotificador notificador) : base(notificador)
        {
            _novidadeRepository = novidadeRepository;
        }


        public async Task<bool> Incluir(Novidade valor) 
        {
            if (!ExecutarValidacao(new NovidadeValidation(), valor)
                //  || !ExecutarValidacao(new EnderecoValidation(), fornecedor.Endereco)
                ) return false;

            var dados = await _novidadeRepository.ObterPorNomePOC(valor.desc_novidade.ToUpper().Trim());

            if (dados != null)
            {
                if (dados.desc_novidade.ToUpper().Trim() == valor.desc_novidade.ToUpper().Trim())
                {
                    Notificar("Já existe um Novidade com esta descrição informada.");
                    return false;
                }
            }

            await _novidadeRepository.IncluirPOC(valor);
            return true;
        }

        public async Task<bool> Alterar(Novidade valor)
        {
            if (!ExecutarValidacao(new NovidadeValidation(), valor)) return false;

            var dados = await _novidadeRepository.ObterPorNomePOC(valor.desc_novidade.ToUpper().Trim());
            if (dados.desc_novidade.ToUpper().Trim() == valor.desc_novidade.ToUpper().Trim()
                && dados.id_novidade != valor.id_novidade)
            {
                Notificar("Já existe um Novidade com esta descrição informada.");
                return false;
            }

            await _novidadeRepository.AlterarPOC(valor);
            return true;
        }

        public async Task Excluir(int id)
        {
            await _novidadeRepository.ExlcuirPOC(id);
        }

        public void Dispose()
        {
            _novidadeRepository?.Dispose();
        }
    }
}