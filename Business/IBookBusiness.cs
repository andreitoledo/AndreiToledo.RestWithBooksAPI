using AndreiToledo.RestWithBooksAPI.Data.VO;
using AndreiToledo.RestWithBooksAPI.Hypermedia.Utils;
using System.Collections.Generic;

namespace AndreiToledo.RestWithBooksAPI.Business
{
    public interface IBookBusiness
    {
        BookVO Create(BookVO book);
        BookVO FindByID(long id);
        List<BookVO> FindAll();
        PagedSearchVO<BookVO> FindWithPagedSearch(
            string title, string sortDirection, int pageSize, int page);
        BookVO Update(BookVO book);
        void Delete(long id);
    }
}
