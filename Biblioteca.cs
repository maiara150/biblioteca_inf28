
public class Biblioteca
{
    public List<Livro> Livros { get; set; } = new List<Livro>();
    public List<Usuario> Usuarios { get; set; } = new List<Usuario>();
    public List<Emprestimo> Emprestimos { get; set; } = new List<Emprestimo>();

    public void CadastrarLivro(string titulo, string autor, string isbn, int quantidade)
    {
        Livros.Add(new Livro(titulo, autor, isbn, quantidade));
        Console.WriteLine("Livro cadastrado com sucesso!");
    }

    public void CadastrarUsuario(string nome, string matricula)
    {
        Usuarios.Add(new Usuario(nome, matricula));
        Console.WriteLine("Usuário cadastrado com sucesso!");
    }

    public void RegistrarEmprestimo(string isbn, string matricula, DateTime dataDevolucao)
    {
        Livro? livro = Livros.FirstOrDefault(l => l.ISBN == isbn);
        Usuario? usuario = Usuarios.FirstOrDefault(u => u.Matricula == matricula);

        if (livro == null)
        {
            Console.WriteLine("Livro não encontrado.");
            return;
        }

        if (usuario == null)
        {
            Console.WriteLine("Usuário não encontrado.");
            return;
        }

        if (livro.Quantidade <= 0)
        {
            Console.WriteLine("Livro indisponível para empréstimo.");
            return;
        }

        livro.Quantidade--; // diminui a quantidade disponível
        var periodo = new PeriodoEmprestimo(DateTime.Today, dataDevolucao);
        Emprestimos.Add(new Emprestimo(livro, usuario, periodo));
        Console.WriteLine("Empréstimo registrado com sucesso!");
        Console.WriteLine("Empréstimo registrado com sucesso!");
        Console.WriteLine($"Período do empréstimo: {periodo}");
    }

    public void RegistrarDevolucao(string isbn, string matricula)
    {
        var emprestimo = Emprestimos.FirstOrDefault(e =>
            e.Livro.ISBN == isbn &&
            e.Usuario.Matricula == matricula &&
            !e.Finalizado
        );

        if (emprestimo == null)
        {
            Console.WriteLine("Empréstimo não encontrado ou já finalizado.");
            return;
        }

        emprestimo.Finalizar();
        Console.WriteLine("Devolução registrada com sucesso!");
    }

    public void ListarLivros()
    {
        Console.WriteLine("\nLista de Livros:");
        foreach (var livro in Livros)
        {
            Console.WriteLine(livro);
        }
    }

    public void ExibirRelatorios()
    {
        Console.WriteLine("\n--- Relatórios ---");

        Console.WriteLine("\n Livros Disponíveis:");
        foreach (var livro in Livros.Where(l => l.Quantidade > 0))
            Console.WriteLine(livro);

        Console.WriteLine("\n Livros Emprestados:");
        foreach (var emprestimo in Emprestimos.Where(e => !e.Finalizado))
            Console.WriteLine(emprestimo);

        Console.WriteLine("\n Usuários com livros emprestados:");
        foreach (var usuario in Usuarios)
        {
            var emprestimosUsuario = Emprestimos
                .Where(e => e.Usuario.Matricula == usuario.Matricula && !e.Finalizado)
                .ToList();

            if (emprestimosUsuario.Count > 0)
            {
                Console.WriteLine($"\n{usuario}:");
                foreach (var emp in emprestimosUsuario)
                    Console.WriteLine($" - {emp.Livro.Titulo} ({emp.Periodo})");
            }
        }
    }
}