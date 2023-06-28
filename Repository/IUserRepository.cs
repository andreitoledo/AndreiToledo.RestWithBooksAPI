using AndreiToledo.RestWithBooksAPI.Data.VO;
using AndreiToledo.RestWithBooksAPI.Model;

namespace AndreiToledo.RestWithBooksAPI.Repository
{
    public interface IUserRepository
    {
        User ValidateCredentials(UserVO user);
    }
}
