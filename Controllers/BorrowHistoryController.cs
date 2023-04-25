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
        var borrowHistories = _repository.GetQueryable().Include(x=> x.Borrower).Include(x => x.BorrowHistoryDetails).ToList();

        return View(_autoMapper.Map<List<BorrowHistory>, List<BorrowHistoryDto>>(borrowHistories));
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

        ViewBag.ListBorrower = new List<SelectListItem>(listBorrower.Select(x => new SelectListItem{
            Selected = false,
            Text = x.Name,
            Value = x.Id.ToString()
        }));

        return View(new BorrowHistoryCreateDto());
    }

    [HttpPost]
    public IActionResult Create(BorrowHistoryCreateDto borrowHistoryCreateDto)
    {
        var borrowHistory = _autoMapper.Map<BorrowHistoryCreateDto, BorrowHistory>(borrowHistoryCreateDto);
        
        borrowHistory.BorrowHistoryDetails = borrowHistory.BorrowHistoryDetails.Where(x => x.Quantity > 0).ToList();
        borrowHistory.Cost = borrowHistory.BorrowHistoryDetails.Sum(x => x.Price);

        if(ModelState.IsValid)
        {
            _repository.Create(borrowHistory);

            return RedirectToAction("Index");
        }

        ViewBag.ListItem = _autoMapper.Map<List<Item>, List<ItemDto>>(_repositoryItem.GetList());
        var listBorrower = _autoMapper.Map<List<Borrower>, List<BorrowerDto>>(_repositoryBorrower.GetList());

        ViewBag.ListBorrower = new List<SelectListItem>(listBorrower.Select(x => new SelectListItem{
            Selected = false,
            Text = x.Name,
            Value = x.Id.ToString()
        }));

        return View();
    }

    public IActionResult Delete(Guid id)
    {
        var item = _repository.GetQueryable().Include(x => x.BorrowHistoryDetails).FirstOrDefault(x => x.Id == id);

        _repository.Delete(item);

        return RedirectToAction("Index");
    }
}
