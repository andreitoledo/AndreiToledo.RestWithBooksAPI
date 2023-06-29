using AndreiToledo.RestWithBooksAPI.Data.VO;
using AndreiToledo.RestWithBooksAPI.Model;

namespace AndreiToledo.RestWithBooksAPI.Repository
{
    public interface IUserRepository
    {
        User ValidateCredentials(UserVO user);

        User ValidateCredentials(string username);

        User RefreshUserInfo(User user);
    }
}
