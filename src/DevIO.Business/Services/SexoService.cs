using System;
using System.Linq;
using System.Threading.Tasks;
using DevIO.Business.Interfaces;
using DevIO.Business.Models;
using DevIO.Business.Models.Validations;

namespace DevIO.Business.Services
{
    public class SexoService : BaseService, ISexoService
    {
        private readonly ISexoRepository _sexoRepository;

        public SexoService(ISexoRepository sexoRepository,
                                 INotificador notificador) : base(notificador)
        {
            _sexoRepository = sexoRepository;
        }


        public async Task<bool> Incluir(Sexo valor) 
        {
            if (!ExecutarValidacao(new SexoValidation(), valor)
                //  || !ExecutarValidacao(new EnderecoValidation(), fornecedor.Endereco)
                ) return false;

            var dados = await _sexoRepository.ObterPorNomePOC(valor.desc_sexo.ToUpper().Trim());

            if (dados != null)
            {
                if (dados.desc_sexo.ToUpper().Trim() == valor.desc_sexo.ToUpper().Trim())
                {
                    Notificar("Já existe um Sexo com esta descrição informada.");
                    return false;
                }
            }

            await _sexoRepository.IncluirPOC(valor);
            return true;
        }

        public async Task<bool> Alterar(Sexo valor)
        {
            if (!ExecutarValidacao(new SexoValidation(), valor)) return false;

            var dados = await _sexoRepository.ObterPorNomePOC(valor.desc_sexo.ToUpper().Trim());
            if (dados.desc_sexo.ToUpper().Trim() == valor.desc_sexo.ToUpper().Trim()
                && dados.id_sexo != valor.id_sexo)
            {
                Notificar("Já existe um Sexo com esta descrição informada.");
                return false;
            }

            await _sexoRepository.AlterarPOC(valor);
            return true;
        }

        public async Task Excluir(int id)
        {
            await _sexoRepository.ExlcuirPOC(id);
        }

        public void Dispose()
        {
            _sexoRepository?.Dispose();
        }
    }
}