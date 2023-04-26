
using Models.Tables;

namespace Controllers.IRepositories;

public interface IUnitOfWork
{
    IRepository<Item> ItemRepository { get; }
    
    IRepository<Borrower> BorrowerRepository { get; }

    IRepository<BorrowHistory> BorrowHistoryRepository { get; }

    Task CompleteAsync();
}