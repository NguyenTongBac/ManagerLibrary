using AutoMapper;
using Controllers.Dtos.Borrowers;
using Controllers.IRepositories;
using Microsoft.AspNetCore.Mvc;
using Models.Tables;

namespace Controllers;

public class BorrowerController : Controller
{
    private readonly IRepository<Borrower> _repository;
    
    private readonly IMapper _mapper;

    public BorrowerController(IRepository<Borrower> repository, IMapper mapper)
    {
        _mapper = mapper;
        _repository = repository;
    }
    public IActionResult Index()
    {
        var listBorrower =  _repository.GetList();
        ViewBag.ListBorrower = _mapper.Map<List<Borrower>, List<BorrowerDto>>(listBorrower);
        return View();
    }

    [HttpGet]
    public IActionResult View(Guid id)
    {
        var borrower = _repository.GetById(id);
        return View(_mapper.Map<Borrower, BorrowerDto>(borrower));
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(BorrowerCreateUpdateDto borrower)
    {
        _repository.Create(_mapper.Map<BorrowerCreateUpdateDto, Borrower>(borrower));

        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult Update(Guid id)
    {
        var borrower = _repository.GetById(id);

        return View(_mapper.Map<Borrower, BorrowerCreateUpdateDto>(borrower));
    }

    [HttpPost]
    public IActionResult Update(BorrowerCreateUpdateDto borrower)
    {
        _repository.Update(_mapper.Map<BorrowerCreateUpdateDto, Borrower>(borrower));

        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult Delete(Guid id)
    {
        _repository.Delete(_repository.GetById(id));

        return RedirectToAction("Index");
    }
}
