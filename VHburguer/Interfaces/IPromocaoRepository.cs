using VHBurguer.Domains;

namespace VHBurguer.Interfaces
{
    public interface IPromocaoRepository
    {
        public interface IPromocaoRepository
        {
            List<Promocao> Listar();

            Promocao ObterPorID(int id);

            bool NomeExiste(string nome, int? promocaoIdAtual = null);

            void Adicionar(Promocao promocao);

            void Atualizar(Promocao promocao)  ;

            void Remover(int id);
        } 
    }
}
