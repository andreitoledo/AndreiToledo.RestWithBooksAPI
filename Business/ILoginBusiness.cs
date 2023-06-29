using AndreiToledo.RestWithBooksAPI.Data.VO;

namespace AndreiToledo.RestWithBooksAPI.Business
{
    public interface ILoginBusiness
    {
        TokenVO ValidateCredentials(UserVO user);

        //TokenVO ValidateCredentials(TokenVO token);

        //bool RevokeToken(string userName);
    }
}
