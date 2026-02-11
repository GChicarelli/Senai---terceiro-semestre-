using System;
using System.Collections.Generic;

namespace VHburguer.Domains;

public partial class Log_AlteracaoProduto
{
    public int Log_AltecaoProdutoID { get; set; }

    public DateTime DataAlteracao { get; set; }

    public string? NomeAnterior { get; set; }

    public decimal? ValorAnterior { get; set; }

    public int? ProdutoID { get; set; }

    public virtual Produto? Produto { get; set; }
}
