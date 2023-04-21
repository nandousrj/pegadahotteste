using System;
using System.Linq;
using System.Threading.Tasks;
using DevIO.Business.Interfaces;
using DevIO.Business.Models;
using DevIO.Business.Models.Validations;

namespace DevIO.Business.Services
{
    public class TrabalhoService : BaseService, ITrabalhoService
    {
        private readonly ITrabalhoRepository _trabalhoRepository;

        public TrabalhoService(ITrabalhoRepository trabalhoRepository,
                                 INotificador notificador) : base(notificador)
        {
            _trabalhoRepository = trabalhoRepository;
        }


        public async Task<bool> Incluir(Trabalho Trabalho) // estava sem o bool
        {
            if (!ExecutarValidacao(new TrabalhoValidation(), Trabalho)
                //  || !ExecutarValidacao(new EnderecoValidation(), fornecedor.Endereco)
                ) return false;

            var dados = await _trabalhoRepository.ObterPorNomePOC(Trabalho.desc_trabalho.ToUpper().Trim());

            if (dados != null)
            {
                if (dados.desc_trabalho.ToUpper().Trim() == Trabalho.desc_trabalho.ToUpper().Trim())
                {
                    Notificar("Já existe um Trabalho com esta descrição informada.");
                    return false;
                }
            }

            await _trabalhoRepository.IncluirPOC(Trabalho);
            return true;
        }

        public async Task<bool> Alterar(Trabalho Trabalho)
        {
            if (!ExecutarValidacao(new TrabalhoValidation(), Trabalho)) return false;

            var dados = await _trabalhoRepository.ObterPorNomePOC(Trabalho.desc_trabalho.ToUpper().Trim());
            if (dados.desc_trabalho.ToUpper().Trim() == Trabalho.desc_trabalho.ToUpper().Trim()
                && dados.id_trabalho != Trabalho.id_trabalho)
            {
                Notificar("Já existe um Trabalho com esta descrição informada.");
                return false;
            }

            await _trabalhoRepository.AlterarPOC(Trabalho);
            return true;
        }

        public async Task Excluir(int id)
        {
            await _trabalhoRepository.ExlcuirPOC(id);
        }

        public void Dispose()
        {
            _trabalhoRepository?.Dispose();
        }
    }
}