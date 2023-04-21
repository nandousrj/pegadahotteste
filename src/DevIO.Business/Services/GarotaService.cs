using System;
using System.Linq;
using System.Threading.Tasks;
using DevIO.Business.Interfaces;
using DevIO.Business.Models;
using DevIO.Business.Models.Validations;

namespace DevIO.Business.Services
{
    public class GarotaService : BaseService, IGarotaService
    {
        private readonly IGarotaRepository _garotaRepository;

        public GarotaService(IGarotaRepository garotaRepository,
                                 INotificador notificador) : base(notificador)
        {
            _garotaRepository = garotaRepository;
        }


        public async Task<bool> Incluir(Garota valor)
        {
            if (!ExecutarValidacao(new GarotaValidation(), valor)
                //  || !ExecutarValidacao(new EnderecoValidation(), fornecedor.Endereco)
                ) return false;

            await _garotaRepository.IncluirPOC(valor);
            return true;
        }

        public async Task<bool> Alterar(Garota valor)
        {
            if (!ExecutarValidacao(new GarotaValidation(), valor)) return false;
          
            await _garotaRepository.AlterarPOC(valor);
            return true;
        }
      

        //public async Task Excluir(int id)
        //{
        //    await _GarotaRepository.ExlcuirPOC(id);
        //}

        public void Dispose()
        {
            _garotaRepository?.Dispose();
        }
    }
}