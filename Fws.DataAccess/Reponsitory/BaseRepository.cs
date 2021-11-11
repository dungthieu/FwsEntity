using Fws.DataAccess.Interface;
using Fws.Model.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fws.DataAccess.Reponsitory
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        public DbSet<T> Dbset;
        public readonly NotesContext _context;

        public BaseRepository( NotesContext context)
        {
            _context = context;
            // dbset chính là 1 bảng đó. thay vì về sau phải gọi context.table
            Dbset = context.Set<T>();
        }

        public bool Delete(string id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetAll()
        {
          return Dbset.AsQueryable();
        }

        public T Insert(T entity)
        {
            throw new NotImplementedException();
        }

        public T Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
