using VHBurguer.Domains;
using VHBurguer.DTOs.AutenticacaoDto;
using VHBurguer.DTOs.CategoriaDto;
using VHBurguer.DTOs.LogProdutoDto;
using VHBurguer.DTOs.ProdutoDto;
using VHBurguer.Interfaces;

namespace VHBurguer.Applications.Services
{
    public class LogAlteracaoProdutoService
    {
        private readonly ILogAlteracaoProdutoRepository _repository;

        public LogAlteracaoProdutoService(ILogAlteracaoProdutoRepository repostory)
        {
            _repository = repostory;
        }

        public List<LerCategoriaDto> Listar()
        {
            List<Log_AlteracaoProduto> logs = _repository.Listar();

            List<LerLogProdutoDto> listaLogProduto = logs.Select(l => new LerLogProdutoDto
            {
                LogID = l.Log_AlteracaoProdutoID,
                ProdutoID = l.FK_ProdutoID,
                NomeAnterior = l.NomeAnterior,
                PrecoAnterior = l.PrecoAnterior,
                DataAlteracao = l.DataAlteracao
            }).ToList();

            return listaLogProduto;
        }

        public List<LerLogProdutoDto> ListarPorProduto(int produtoId)
        {
            List<Log_AlteracaoProduto> logs = _repository.ListarPorProduto(produtoId);

            List<LerLogProdutoDto> listaLogProduto = logs.Select(log => new LerLogProdutoDto
            {
                LogID = log.Log_AlteracaoProdutoID,
                ProdutoID = log.ProdutoID,
                NomeANterior = log.NomeAnterior,
                PrecoAnterior = log.PrecoAnterior,
                DataAlteracao = log.DataAlteracao
            }).ToList();

            return listaLogProduto;
        }
    }
}