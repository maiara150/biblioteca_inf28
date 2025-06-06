public class Usuario : Pessoa
{
    public string Matricula { get; set; }

    public Usuario(string nome, string matricula)
    {
        Nome = nome;
        Matricula = matricula;
    }

    public override string ToString()
    {
        return $"{Nome} (Matrícula: {Matricula})";
    }
}