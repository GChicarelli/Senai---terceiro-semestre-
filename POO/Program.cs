using Carro;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace POO;

class Program
{
    Carro a = new Carro()
        {
            Marca = "BMW",
            Modelo = "320i",
            Ano = "2025"
        };




    static void Main()
    {
        Console.Write("Digite o nome do Carro: ");
        string Nome = Console.ReadLine();

        Console.Write("Digite o Modelo do Carro: ");
        string Modelo = Console.ReadLine();

        Console.Write("Digite o nível do Carro: ");
        int Ano = int.Parse(Console.ReadLine());

        Carro p1 = new Carro(nome, Modelo, Ano);

        Console.WriteLine("\nCarro criado com sucesso!");
        Console.WriteLine($"Nome: {p1.Nome}");
        Console.WriteLine($"Modelo: {p1.Modelo}");
        Console.WriteLine($"Ano: {p1.Ano}");
    }




}
