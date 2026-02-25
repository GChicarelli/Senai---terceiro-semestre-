using Microsoft.Identity.Client;

namespace VHBurguer.DTOs.LogProdutoDto
{
    public class LerLogProdutoDto
    {
        public int LogID { get; set; }
        public int? ProdutoID { get; set; }
        public string NomeAnterior { get; set; } = null;
        public string? PecoAnterior { get; set; }
        public DateTime DataAlteracao { get; set; }
    }
}
