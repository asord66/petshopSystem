using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProdutosPetShop
{
    class Produto
    {
        string nome, descricao, marca;
        int quant;
        double valorCompra, valorVenda;

        public string Nome { get => nome; set => nome = value; }
        public string Descricao { get => descricao; set => descricao = value; }
        public string Marca { get => marca; set => marca = value; }
        public int Quant { get => quant; set => quant = value; }
        public double ValorCompra { get => valorCompra; set => valorCompra = value; }
        public double ValorVenda { get => valorVenda; set => valorVenda = value; }
    }
}
