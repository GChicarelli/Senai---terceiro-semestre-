namespace Teste;

class Program
{
    static void Main(string[] args)
    {
        Console.Write("Digite o Marca do Carro: ");
        string Marca = Console.ReadLine();

        Console.Write("Digite o Modelo do Carro: ");
        string Modelo = Console.ReadLine();

        Console.Write("Digite o ano do Carro: ");
        int Ano = int.Parse(Console.ReadLine());

        Carro p1 = new Carro(Marca, Modelo, Ano);

        Console.WriteLine("\nCarro criado com sucesso!");
        Console.WriteLine($"Marca: {p1.Marca}");
        Console.WriteLine($"Modelo: {p1.Modelo}");
        Console.WriteLine($"Ano: {p1.Ano}");
    }
}
