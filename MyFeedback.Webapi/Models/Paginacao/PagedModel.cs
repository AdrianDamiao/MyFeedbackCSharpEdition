using System;
using System.Collections.Generic;

public class PagedModel<TModel>
{
    const int TamanhoMaximoPagina = 5;
    private int _tamanhoPagina;

    public int TamanhoPagina
    {
        get => _tamanhoPagina;
        set => _tamanhoPagina = (value > TamanhoMaximoPagina) ? TamanhoMaximoPagina : value;
    }

    public int PaginaAtual { get; set; }
    public int TotalItens { get; set; }
    public int TotalPaginas { get; set; }
    public IList<TModel> Itens { get; set; }

    public PagedModel()
    {
        Itens = new List<TModel>();
    }
}