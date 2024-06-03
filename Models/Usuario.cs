namespace ApiGabi.Models;

public class Usuario
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? apellido { get; set; }
    public int edad { get; set; }
    public bool esMayorDeEdad { get; set; }
}