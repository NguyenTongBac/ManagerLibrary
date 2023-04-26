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
    private readonly IUnitOfWork _unitOfWork;

    private readonly IMapper _autoMapper;

    public BorrowHistoryController(IUnitOfWork unitOfWork,IMapper autoMapper)
    {
        _autoMapper = autoMapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<IActionResult> Index()
    {
        var borrowHistories = await _unitOfWork.BorrowHistoryRepository.GetQueryable().Include(x=> x.Borrower).Include(x => x.BorrowHistoryDetails).ToListAsync();

        return View(_autoMapper.Map<List<BorrowHistory>, List<BorrowHistoryDto>>(borrowHistories));
    }

    public async Task<IActionResult> View(Guid id)
    {
        var borrowHistory = await _unitOfWork.BorrowHistoryRepository.GetQueryable()
        .Include(x => x.BorrowHistoryDetails).ThenInclude(x => x.Item)
        .Include(x => x.Borrower)
        .FirstOrDefaultAsync(x => x.Id == id);
        
        return View(_autoMapper.Map<BorrowHistory, BorrowHistoryDto>(borrowHistory));
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        ViewBag.ListItem = _autoMapper.Map<List<Item>, List<ItemDto>>(await _unitOfWork.ItemRepository.GetList());
        var listBorrower = _autoMapper.Map<List<Borrower>, List<BorrowerDto>>(await _unitOfWork.BorrowerRepository.GetList());
        ViewBag.ListBorrower = new List<SelectListItem>(listBorrower.Select(x => new SelectListItem{
            Selected = false,
            Text = x.Name,
            Value = x.Id.ToString()
        }));

        return View(new BorrowHistoryCreateDto());
    }

    [HttpPost]
    public async Task<IActionResult> Create(BorrowHistoryCreateDto borrowHistoryCreateDto)
    {
        var borrowHistory = _autoMapper.Map<BorrowHistoryCreateDto, BorrowHistory>(borrowHistoryCreateDto);
        
        borrowHistory.BorrowHistoryDetails = borrowHistory.BorrowHistoryDetails.Where(x => x.Quantity > 0).ToList();
        borrowHistory.Cost = borrowHistory.BorrowHistoryDetails.Sum(x => x.Price);

        if(ModelState.IsValid)
        {
            await _unitOfWork.BorrowHistoryRepository.Create(borrowHistory);
            await _unitOfWork.CompleteAsync();

            return RedirectToAction("Index");
        }

        ViewBag.ListItem = _autoMapper.Map<List<Item>, List<ItemDto>>(await _unitOfWork.ItemRepository.GetList());
        var listBorrower = _autoMapper.Map<List<Borrower>, List<BorrowerDto>>(await _unitOfWork.BorrowerRepository.GetList());
        ViewBag.ListBorrower = new List<SelectListItem>(listBorrower.Select(x => new SelectListItem{
            Selected = false,
            Text = x.Name,
            Value = x.Id.ToString()
        }));

        return View();
    }

    public async Task<IActionResult> Delete(Guid id)
    {
        var item = await _unitOfWork.BorrowHistoryRepository.GetQueryable().Include(x => x.BorrowHistoryDetails).FirstOrDefaultAsync(x => x.Id == id);

        await _unitOfWork.BorrowHistoryRepository.Delete(item);
        await _unitOfWork.CompleteAsync();

        return RedirectToAction("Index");
    }
}
