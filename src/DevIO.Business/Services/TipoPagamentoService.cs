using System;
using System.Linq;
using System.Threading.Tasks;
using DevIO.Business.Interfaces;
using DevIO.Business.Models;
using DevIO.Business.Models.Validations;

namespace DevIO.Business.Services
{
    public class TipoPagamentoService : BaseService, ITipoPagamentoService
    {
        private readonly ITipoPagamentoRepository _tipopagamentoRepository;

        public TipoPagamentoService(ITipoPagamentoRepository tipopagamentoRepository,
                                 INotificador notificador) : base(notificador)
        {
            _tipopagamentoRepository = tipopagamentoRepository;
        }


        public async Task<bool> Incluir(TipoPagamento valor) 
        {
            if (!ExecutarValidacao(new TipoPagamentoValidation(), valor)
                //  || !ExecutarValidacao(new EnderecoValidation(), fornecedor.Endereco)
                ) return false;

            var dados = await _tipopagamentoRepository.ObterPorNomePOC(valor.desc_tipo_pagamento.ToUpper().Trim());

            if (dados != null)
            {
                if (dados.desc_tipo_pagamento.ToUpper().Trim() == valor.desc_tipo_pagamento.ToUpper().Trim())
                {
                    Notificar("Já existe um Tipo de Pagamento com esta descrição informada.");
                    return false;
                }
            }

            await _tipopagamentoRepository.IncluirPOC(valor);
            return true;
        }

        public async Task<bool> Alterar(TipoPagamento valor)
        {
            if (!ExecutarValidacao(new TipoPagamentoValidation(), valor)) return false;

            var dados = await _tipopagamentoRepository.ObterPorNomePOC(valor.desc_tipo_pagamento.ToUpper().Trim());
            if (dados.desc_tipo_pagamento.ToUpper().Trim() == valor.desc_tipo_pagamento.ToUpper().Trim()
                && dados.id_tipo_pagamento != valor.id_tipo_pagamento)
            {
                Notificar("Já existe um Tipo de Pagamento com esta descrição informada.");
                return false;
            }

            await _tipopagamentoRepository.AlterarPOC(valor);
            return true;
        }

        public async Task Excluir(int id)
        {
            await _tipopagamentoRepository.ExlcuirPOC(id);
        }

        public void Dispose()
        {
            _tipopagamentoRepository?.Dispose();
        }
    }
}