using System.Collections.Generic;
using AutoMapper;
using Controllers.Dtos.Borrowers;
using Controllers.IRepositories;
using Microsoft.AspNetCore.Mvc;
using Models.Tables;

namespace Controllers;

public class BorrowerController : Controller
{
    // private readonly IRepository<Borrower> _unitOfWork.BorrowerRepository;
    private readonly IUnitOfWork _unitOfWork;
    
    private readonly IMapper _mapper;

    public BorrowerController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<IActionResult> Index()
    {
        var listBorrower =  await _unitOfWork.BorrowerRepository.GetList();

        return View(_mapper.Map<List<Borrower>, List<BorrowerDto>>(listBorrower));
    }

    [HttpGet]
    public async Task<IActionResult> View(Guid id)
    {
        var borrower = await _unitOfWork.BorrowerRepository.GetById(id);

        return View(_mapper.Map<Borrower, BorrowerDto>(borrower));
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(BorrowerCreateUpdateDto borrower)
    {
        await _unitOfWork.BorrowerRepository.Create(_mapper.Map<BorrowerCreateUpdateDto, Borrower>(borrower));
        await _unitOfWork.CompleteAsync();

        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> Update(Guid id)
    {
        var borrower = await _unitOfWork.BorrowerRepository.GetById(id);

        return View(_mapper.Map<Borrower, BorrowerCreateUpdateDto>(borrower));
    }

    [HttpPost]
    public async Task<IActionResult> Update(BorrowerCreateUpdateDto borrower)
    {
        await _unitOfWork.BorrowerRepository.Update(_mapper.Map<BorrowerCreateUpdateDto, Borrower>(borrower));
        await _unitOfWork.CompleteAsync();

        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _unitOfWork.BorrowerRepository.Delete(await _unitOfWork.BorrowerRepository.GetById(id));
        await _unitOfWork.CompleteAsync();

        return RedirectToAction("Index");
    }
}
