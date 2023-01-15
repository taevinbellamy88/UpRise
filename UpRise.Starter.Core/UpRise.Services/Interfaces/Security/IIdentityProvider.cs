namespace UpRise
{
    public interface IIdentityProvider<T>
    {
        T GetCurrentUserId();
    }
}