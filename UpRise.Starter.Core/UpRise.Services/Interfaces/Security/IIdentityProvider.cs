namespace UpRise.Services.Interfaces.Security
{
    public interface IIdentityProvider<T>
    {
        T GetCurrentUserId();
    }
}