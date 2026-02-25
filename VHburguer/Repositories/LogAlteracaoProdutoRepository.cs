using VHBurguer.Contexts;
using VHBurguer.Domains;

namespace VHBurguer.Repositories
{
    public class LogAlteracaoProdutoRepository : ILogAlteracaoProdutoRespository
    {
        private readonly VH_BurguerContext _context;

        public LogAlteracaoProdutoRepository(VH_BurguerContext context)
        {
            _context = context;
        }

        public List<Log_AlteracaoProduto> Listar()
        {
            List<Log_AlteracaoProduto> log = _context.Log_AlteracaoProduto.OrderByDescending(l => l.DataAlteracao).ToList();
            return log;
        }

        public List<Log_AlteracaoProduto> ListarPorProduto(int produtoId)
        {
            List<Log_AlteracaoProduto>AlteracaoProduto = _context.Log_AlteracaoProduto
                .Where(log => log.ProdutoID  == produtoId)
                .OrderByDescending(l => l.DataAlteracao)
                .ToList();
            return AlteracaoProduto;
        }

    }
}
