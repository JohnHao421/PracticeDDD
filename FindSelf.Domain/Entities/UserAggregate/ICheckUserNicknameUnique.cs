namespace FindSelf.Domain.Entities.UserAggregate
{
    public interface ICheckUserNicknameUnique
    {
        bool IsUnique(string nickname);
    }
}