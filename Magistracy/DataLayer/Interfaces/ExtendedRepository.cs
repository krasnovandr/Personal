using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Interfaces
{
    public interface ExtendedRepository<T> :IRepository<T> where T : class
    {
        T Get(string id);
        void Delete(string id);
    }
}
