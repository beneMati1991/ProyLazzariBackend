namespace Abstractions
{
    public interface ITokenParameter
    {
        int Id { get; set; }
        string Email { get; set; }
        string Usuario { get; set; }
        string Password { get; set; }
        string Rol { get; set; }
        int? CID { get; set; }
    }
}
