using System;
using System.Linq;
using System.Threading.Tasks;
using DevIO.Business.Interfaces;
using DevIO.Business.Models;
using DevIO.Business.Models.Validations;

namespace DevIO.Business.Services
{
    public class GarotaCategoriaService : BaseService, IGarotaCategoriaService
    {
        private readonly IGarotaCategoriaRepository _garotacategoriaRepository;

        public GarotaCategoriaService(IGarotaCategoriaRepository garotacategoriaRepository,
                                 INotificador notificador) : base(notificador)
        {
            _garotacategoriaRepository = garotacategoriaRepository;
        }


        public async Task<bool> Incluir(GarotaCategoria valor)
        {
            if (!ExecutarValidacao(new GarotaCategoriaValidation(), valor)
                //  || !ExecutarValidacao(new EnderecoValidation(), fornecedor.Endereco)
                ) return false;

            await _garotacategoriaRepository.IncluirPOC(valor);
            return true;
        }

        public async Task<bool> Alterar(GarotaCategoria valor)
        {
            if (!ExecutarValidacao(new GarotaCategoriaValidation(), valor)) return false;
          
            await _garotacategoriaRepository.AlterarPOC(valor);
            return true;
        }
      

        //public async Task Excluir(int id)
        //{
        //    await _GarotaRepository.ExlcuirPOC(id);
        //}

        public void Dispose()
        {
            _garotacategoriaRepository?.Dispose();
        }
    }
}