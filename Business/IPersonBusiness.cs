using AndreiToledo.RestWithBooksAPI.Data.VO;
using System.Collections.Generic;

namespace AndreiToledo.RestWithBooksAPI.Business
{
    public interface IPersonBusiness
    {
        PersonVO Create(PersonVO person);
        PersonVO FindByID(long id);
        List<PersonVO> FindAll();
        PersonVO Update(PersonVO person);
        PersonVO Disable(long id);
        void Delete(long id);        
    }
}
