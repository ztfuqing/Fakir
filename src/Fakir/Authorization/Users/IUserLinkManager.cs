using System.Threading.Tasks;

namespace Fakir.Authorization.Users
{
    public interface IUserLinkManager
    {
        Task Link(long userId, long targetUserId);

        Task<bool> AreUsersLinked(long firstUserId, long secondUserId);

        Task Unlink(long userId);
    }
}
