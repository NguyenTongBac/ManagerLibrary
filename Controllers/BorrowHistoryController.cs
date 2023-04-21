using AutoMapper;
using Controllers.Dtos.BorrowHistories;
using Controllers.IRepositories;
using Microsoft.AspNetCore.Mvc;
using Models.Tables;
using Microsoft.EntityFrameworkCore;
using Controllers.Dtos.Items;
using Controllers.Dtos.Borrowers;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Controllers;

public class BorrowHistoryController : Controller
{
    private readonly IRepository<BorrowHistory> _repository;
    private readonly IRepository<Borrower> _repositoryBorrower;
    private readonly IRepository<Item> _repositoryItem;
    private readonly IMapper _autoMapper;

    public BorrowHistoryController(IRepository<BorrowHistory> repository, IMapper autoMapper, IRepository<Item> repositoryItem, IRepository<Borrower> repositoryBorrower)
    {
        _repositoryItem = repositoryItem;
        _repository = repository;
        _autoMapper = autoMapper;
        _repositoryBorrower = repositoryBorrower;
    }
    public IActionResult Index()
    {
        var borrowHistories = _repository.GetList();
        ViewBag.borrowHistories = _autoMapper.Map<List<BorrowHistory>, List<BorrowHistoryDto>>(borrowHistories);

        return View();
    }

    public IActionResult View(Guid id)
    {
        var borrowHistory = _repository.GetQueryable()
        .Include(x => x.BorrowHistoryDetails).ThenInclude(x => x.Item)
        .Include(x => x.Borrower)
        .FirstOrDefault(x => x.Id == id);
        
        return View(_autoMapper.Map<BorrowHistory, BorrowHistoryDto>(borrowHistory));
    }

    [HttpGet]
    public IActionResult Create()
    {
        ViewBag.ListItem = _autoMapper.Map<List<Item>, List<ItemDto>>(_repositoryItem.GetList());
        var listBorrower = _autoMapper.Map<List<Borrower>, List<BorrowerDto>>(_repositoryBorrower.GetList());
        ViewBag.ListBorrower = new SelectList(listBorrower);


        return View();
    }

    public IActionResult Create(BorrowHistory borrowHistory)
    {
        if(ModelState.IsValid)
        {
            _repository.Create(borrowHistory);

            return RedirectToAction("Index");
        }

        return View();
    }
}
