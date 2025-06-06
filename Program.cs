public struct PeriodoEmprestimo
{
    public DateTime DataInicio { get; set; }
    public DateTime DataFim { get; set; }

    public PeriodoEmprestimo(DateTime inicio, DateTime fim)
    {
        DataInicio = inicio;
        DataFim = fim;
    }

    public override string ToString()
    {
        return $"{DataInicio:dd/MM/yyyy} até {DataFim:dd/MM/yyyy}";
    }
}

public class Emprestimo
{
    public Livro Livro { get; set; }
    public Usuario Usuario { get; set; }
    public PeriodoEmprestimo Periodo { get; set; }
    public bool Finalizado { get; private set; }

    public Emprestimo(Livro livro, Usuario usuario, PeriodoEmprestimo periodo)
    {
        Livro = livro;
        Usuario = usuario;
        Periodo = periodo;
        Finalizado = false;
    }

    public void Finalizar()
    {
        Finalizado = true;
        Livro.Quantidade++; // devolve o livro
    }

    public override string ToString()
    {
        return $"{Livro.Titulo} emprestado para {Usuario.Nome} de {Periodo} - " +
               (Finalizado ? "Devolvido" : "Em aberto");
    }
}



class Program
{
    static void Main(string[] args)
    {
        Biblioteca biblioteca = new Biblioteca();
        int opcao;

        do
        {
            Console.WriteLine("\n=== MENU BIBLIOTECA ===");
            Console.WriteLine("1 - Cadastrar Livro");
            Console.WriteLine("2 - Cadastrar Usuário");
            Console.WriteLine("3 - Registrar Empréstimo");
            Console.WriteLine("4 - Registrar Devolução");
            Console.WriteLine("5 - Listar Livros");
            Console.WriteLine("6 - Exibir Relatórios");
            Console.WriteLine("0 - Sair");
            Console.Write("Escolha uma opção: ");
            int.TryParse(Console.ReadLine(), out opcao);
            Console.WriteLine();

            switch (opcao)
            {
                case 1:
                    Console.Write("Título: ");
                    string titulo = Console.ReadLine()!;
                    Console.Write("Autor: ");
                    string autor = Console.ReadLine()!;
                    Console.Write("ISBN: ");
                    string isbn = Console.ReadLine()!;
                    Console.Write("Quantidade: ");
                    int qtd;
                    int.TryParse(Console.ReadLine(), out qtd);
                    biblioteca.CadastrarLivro(titulo, autor, isbn, qtd);
                    break;

                case 2:
                    Console.Write("Nome do usuário: ");
                    string nome = Console.ReadLine()!;
                    Console.Write("Matrícula: ");
                    string matricula = Console.ReadLine()!;
                    biblioteca.CadastrarUsuario(nome, matricula);
                    break;

                case 3:
                     Console.Write("ISBN do livro: ");
                     string isbnEmp = Console.ReadLine()!;
                     Console.Write("Matrícula do usuário: ");
                     string matEmp = Console.ReadLine()!;
                     Console.Write("Data de devolução prevista (dd/MM/yyyy): ");
                     string dataDevolucaoStr = Console.ReadLine()!;
                     if (!DateTime.TryParseExact(dataDevolucaoStr, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime dataDevolucao))
                     {
                        Console.WriteLine("Data inválida. Use o formato dd/MM/yyyy.");
                        break;
                    }
                      biblioteca.RegistrarEmprestimo(isbnEmp, matEmp, dataDevolucao);
                      break;
                case 4:
                    Console.Write("ISBN do livro: ");
                    string isbnDev = Console.ReadLine()!;
                    Console.Write("Matrícula do usuário: ");
                    string matDev = Console.ReadLine()!;
                    biblioteca.RegistrarDevolucao(isbnDev, matDev);
                    break;

                case 5:
                    biblioteca.ListarLivros();
                    break;

                case 6:
                    biblioteca.ExibirRelatorios();
                    break;

                case 0:
                    Console.WriteLine("Saindo...");
                    break;

                default:
                    Console.WriteLine("Opção inválida.");
                    break;
            }

        } while (opcao != 0);
    }
}
