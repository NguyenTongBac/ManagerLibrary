using AutoMapper;
using Controllers.Dtos.Items;
using Controllers.IRepositories;
using Controllers.Dtos;
using Microsoft.AspNetCore.Mvc;
using Models.Tables;
using System.Linq.Dynamic.Core;
using Models.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;

namespace Controllers;

public class ItemController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _autoMapper;

    public ItemController(IUnitOfWork unitOfWork, IMapper autoMapper)
    {
        _unitOfWork = unitOfWork;
        _autoMapper = autoMapper;
    }

    [HttpPost]
    public JsonResult GetListItem(ItemSearchDto condition)
    {   
        PageResultDto<ItemDto> result = new PageResultDto<ItemDto>();
        var items = _unitOfWork.ItemRepository.GetQueryable()
        .Where(x => x.Name.Contains(condition.Keyword)).OrderBy(condition.Sorting);
        
        result.TotalCount = items.ToList().Count();
        var list = items.Skip(condition.SkipCount).Take(condition.MaxResultCount);

        result.Items = _autoMapper.Map<List<Item>, List<ItemDto>>(list.ToList());

        return Json(result);
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var items = _autoMapper.Map<List<Item>, List<ItemDto>>(await _unitOfWork.ItemRepository.GetList());
        
        return View(items);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var selectCategory = new SelectList(Enum.GetValues(typeof(Category)));
        ViewBag.SelectCategory = selectCategory;

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(ItemCreateUpdateDto item)
    {
        if(ModelState.IsValid)
        {
            await _unitOfWork.ItemRepository.Create(_autoMapper.Map<ItemCreateUpdateDto, Item>(item));
            await _unitOfWork.CompleteAsync();

            return RedirectToAction("Index");
        }

        var selectCategory = new SelectList(Enum.GetValues(typeof(Category)));
        ViewBag.SelectCategory = selectCategory;

        return View();
    }

    [HttpGet]
    public async Task<IActionResult> Update(Guid id)
    {
        var item = await _unitOfWork.ItemRepository.GetById(id);

        return View(_autoMapper.Map<Item, ItemCreateUpdateDto>(item));
    }

    [HttpPost]
    public async Task<IActionResult> Update(ItemCreateUpdateDto item)
    {
        if(ModelState.IsValid)
        {
            await _unitOfWork.ItemRepository.Update(_autoMapper.Map<ItemCreateUpdateDto, Item>(item));
            await _unitOfWork.CompleteAsync();

            return RedirectToAction("Index");
        }

        return View();
    }

    [HttpGet]
    [Route("Item/Delete/{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var item = await _unitOfWork.ItemRepository.GetById(id);

        await _unitOfWork.ItemRepository.Delete(item);
        await _unitOfWork.CompleteAsync();

        return RedirectToAction("Index");
    }

    [HttpGet]
    [Route("Item/View/{id}")]
    public async Task<IActionResult> View(Guid id)
    {
        var item = await _unitOfWork.ItemRepository.GetById(id);
        
        return View(_autoMapper.Map<Item, ItemDto>(item));
    }
}
