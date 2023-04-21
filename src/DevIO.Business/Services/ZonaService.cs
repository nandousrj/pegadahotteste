using System;
using System.Linq;
using System.Threading.Tasks;
using DevIO.Business.Interfaces;
using DevIO.Business.Models;
using DevIO.Business.Models.Validations;

namespace DevIO.Business.Services
{
    public class ZonaService : BaseService, IZonaService
    {
        private readonly IZonaRepository _zonaRepository;

        public ZonaService(IZonaRepository zonaRepository,
                                 INotificador notificador) : base(notificador)
        {
            _zonaRepository = zonaRepository;
        }


        public async Task<bool> Incluir(Zona valor) 
        {
            if (!ExecutarValidacao(new ZonaValidation(), valor)
                //  || !ExecutarValidacao(new EnderecoValidation(), fornecedor.Endereco)
                ) return false;

            var dados = await _zonaRepository.ObterPorNomePOC(valor.desc_zona.ToUpper().Trim());

            if (dados != null)
            {
                if (dados.desc_zona.ToUpper().Trim() == valor.desc_zona.ToUpper().Trim())
                {
                    Notificar("Já existe um Zona com esta descrição informada.");
                    return false;
                }
            }

            await _zonaRepository.IncluirPOC(valor);
            return true;
        }

        public async Task<bool> Alterar(Zona valor)
        {
            if (!ExecutarValidacao(new ZonaValidation(), valor)) return false;

            var dados = await _zonaRepository.ObterPorNomePOC(valor.desc_zona.ToUpper().Trim());
            if (dados.desc_zona.ToUpper().Trim() == valor.desc_zona.ToUpper().Trim()
                && dados.id_zona != valor.id_zona)
            {
                Notificar("Já existe um Zona com esta descrição informada.");
                return false;
            }

            await _zonaRepository.AlterarPOC(valor);
            return true;
        }

        public async Task Excluir(int id)
        {
            await _zonaRepository.ExlcuirPOC(id);
        }

        public void Dispose()
        {
            _zonaRepository?.Dispose();
        }
    }
}