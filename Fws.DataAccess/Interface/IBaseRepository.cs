using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fws.DataAccess.Interface
{
    public interface IBaseRepository<T>
    {
        public IEnumerable<T> GetAll();

        T Insert(T entity);

        T Update(T entity);

        bool Delete(string id);


    }
}
