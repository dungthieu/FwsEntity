using Fws.DataAccess.Interface;
using Fws.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fws.Service.Service
{
    public class BaseService<T> : IBaseService<T> where T : class
    {
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IBaseRepository<T> baseRepository;

        protected BaseService(IBaseRepository<T> repository)
        {
            baseRepository = repository;
        }
        public virtual List<T> GetAll()
        {
            return baseRepository.GetAll().ToList();
        }
    }
}
