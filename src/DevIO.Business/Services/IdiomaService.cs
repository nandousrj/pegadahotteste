using System;
using System.Linq;
using System.Threading.Tasks;
using DevIO.Business.Interfaces;
using DevIO.Business.Models;
using DevIO.Business.Models.Validations;

namespace DevIO.Business.Services
{
    public class IdiomaService : BaseService, IIdiomaService
    {
        private readonly IIdiomaRepository _idiomaRepository;

        public IdiomaService(IIdiomaRepository idiomaRepository,
                                 INotificador notificador) : base(notificador)
        {
            _idiomaRepository = idiomaRepository;
        }


        public async Task<bool> Incluir(Idioma valor) 
        {
            if (!ExecutarValidacao(new IdiomaValidation(), valor)
                //  || !ExecutarValidacao(new EnderecoValidation(), fornecedor.Endereco)
                ) return false;

            var dados = await _idiomaRepository.ObterPorNomePOC(valor.desc_idioma.ToUpper().Trim());

            if (dados != null)
            {
                if (dados.desc_idioma.ToUpper().Trim() == valor.desc_idioma.ToUpper().Trim())
                {
                    Notificar("Já existe um Idioma com esta descrição informada.");
                    return false;
                }
            }

            await _idiomaRepository.IncluirPOC(valor);
            return true;
        }

        public async Task<bool> Alterar(Idioma valor)
        {
            if (!ExecutarValidacao(new IdiomaValidation(), valor)) return false;

            var dados = await _idiomaRepository.ObterPorNomePOC(valor.desc_idioma.ToUpper().Trim());
            if (dados.desc_idioma.ToUpper().Trim() == valor.desc_idioma.ToUpper().Trim()
                && dados.id_idioma != valor.id_idioma)
            {
                Notificar("Já existe um Idioma com esta descrição informada.");
                return false;
            }

            await _idiomaRepository.AlterarPOC(valor);
            return true;
        }

        public async Task Excluir(int id)
        {
            await _idiomaRepository.ExlcuirPOC(id);
        }

        public void Dispose()
        {
            _idiomaRepository?.Dispose();
        }
    }
}