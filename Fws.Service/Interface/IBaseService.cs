using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fws.Service.Interface
{
    public interface IBaseService<T>
    {
        public List<T> GetAll();
    }
}
