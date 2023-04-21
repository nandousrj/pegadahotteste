using System;
using System.Linq;
using System.Threading.Tasks;
using DevIO.Business.Interfaces;
using DevIO.Business.Models;
using DevIO.Business.Models.Validations;

namespace DevIO.Business.Services
{
    public class CategoriaService : BaseService, ICategoriaService
    {
        private readonly ICategoriaRepository _categoriaRepository;

        public CategoriaService(ICategoriaRepository categoriaRepository,
                                 INotificador notificador) : base(notificador)
        {
            _categoriaRepository = categoriaRepository;
        }


        public async Task<bool> Incluir(Categoria categoria) // estava sem o bool
        {
            if (!ExecutarValidacao(new CategoriaValidation(), categoria)
                //  || !ExecutarValidacao(new EnderecoValidation(), fornecedor.Endereco)
                ) return false;

            var dados = await _categoriaRepository.ObterPorNomePOC(categoria.desc_categoria.ToUpper().Trim());

            if (dados != null)
            {
                if (dados.desc_categoria.ToUpper().Trim() == categoria.desc_categoria.ToUpper().Trim())
                {
                    Notificar("Já existe uma categoria com esta descrição informada.");
                    return false;
                }
            }

            await _categoriaRepository.IncluirPOC(categoria);
            return true;
        }

        public async Task<bool> Alterar(Categoria categoria)
        {
            if (!ExecutarValidacao(new CategoriaValidation(), categoria)) return false;

            var dados = await _categoriaRepository.ObterPorNomePOC(categoria.desc_categoria.ToUpper().Trim());
            if (dados.desc_categoria.ToUpper().Trim() == categoria.desc_categoria.ToUpper().Trim()
                && dados.id_categoria != categoria.id_categoria)
            {
                Notificar("Já existe uma categoria com esta descrição informada.");
                return false;
            }

            await _categoriaRepository.AlterarPOC(categoria);
            return true;
        }

        public async Task Excluir(int id)
        {
            await _categoriaRepository.ExlcuirPOC(id);
        }

        public void Dispose()
        {
            _categoriaRepository?.Dispose();
        }
    }
}