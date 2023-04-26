using Controllers.IRepositories;
using Models.Entities;
using Models.Repositories;
using Models.Tables;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly ManagerLibraryDbContext _context;
    
    private readonly ILogger _logger;

    public IRepository<Item> ItemRepository { get; private set; }

    public IRepository<Borrower> BorrowerRepository { get; private set; }

    public IRepository<BorrowHistory> BorrowHistoryRepository { get; private set; }

    public UnitOfWork(ManagerLibraryDbContext context, ILoggerFactory loggerFactory)
    {
        _context = context;
        _logger = loggerFactory.CreateLogger("logs");
        ItemRepository = new Repository<Item>(_context);
        BorrowerRepository = new Repository<Borrower>(_context);
        BorrowHistoryRepository = new Repository<BorrowHistory>(_context);
    }

    public async Task CompleteAsync()
    {
        await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}