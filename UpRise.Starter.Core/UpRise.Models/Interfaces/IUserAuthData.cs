namespace UpRise.Models.Interfaces
{
    public interface IUserAuthData
    {
        int Id { get; }
        string Email { get; }
        IEnumerable<string> Roles { get; }

    }
}