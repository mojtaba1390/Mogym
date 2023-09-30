


namespace Mogym.Infrastructure
{
    public class UnitOfWork :IUnitOfWork
    {

        //protected readonly MogymContext _context;
        //public UnitOfWork(MogymContext context)
        //{
        //    _context = context;
        //    //ProductRepository = new ProductRepository(_context);

        //}


        //public IProductRepository ProductRepository { get; set; }


        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
