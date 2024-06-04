using WebApiCatalogo.Catalogo.Infrastucture.Context;

namespace WebApiCatalogo.Catalogo.Infrastucture.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private IProdutoRepository? _produtoRepo;

        private ICategoriaRepository? _categoriaRepo;

        protected AppDbContext _dbContext;
        public UnitOfWork(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IProdutoRepository ProdutoRepository
        {
            get
            {
                if (_produtoRepo == null)
                {
                    _produtoRepo = new ProdutoRepository(_dbContext);
                }
                return _produtoRepo;
            }
        }

        public ICategoriaRepository CategoriaRepository
        {
            get
            {
                if (_categoriaRepo == null)
                {
                    _categoriaRepo = new CategoriaRepository(_dbContext);
                }
                return _categoriaRepo;
            }
        }

        public void Commit()
        {
            _dbContext.SaveChanges();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
