using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Teste
{
    public class Carro
    {
        public string Marca;
        public string Modelo;
        public int Ano;

        public Carro(string Marca, string Modelo, int Ano)
        {
            this.Marca = Marca;
            this.Modelo = Modelo;
            this.Ano = Ano;
        }
    }
}