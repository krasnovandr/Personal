using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.EF;
using DataLayer.Interfaces;

namespace DataLayer.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext db;
        //private PhoneRepository phoneRepository;
        //private OrderRepository orderRepository;

        public EFUnitOfWork(string connectionString)
        {
            db = new ApplicationDbContext();
        }
        //public IRepository<Phone> Phones
        //{
        //    get
        //    {
        //        if (phoneRepository == null)
        //            phoneRepository = new PhoneRepository(db);
        //        return phoneRepository;
        //    }
        //}

        //public IRepository<Order> Orders
        //{
        //    get
        //    {
        //        if (orderRepository == null)
        //            orderRepository = new OrderRepository(db);
        //        return orderRepository;
        //    }
        //}

        public void Save()
        {
            db.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
