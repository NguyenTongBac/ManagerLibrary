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
    private readonly IRepository<Item> _repository;
    private readonly IMapper _autoMapper;

    public ItemController(IRepository<Item> repository, IMapper autoMapper)
    {
        _repository = repository;
        _autoMapper = autoMapper;
    }

    [HttpPost]
    public JsonResult GetListItem(ItemSearchDto condition)
    {   
        PageResultDto<ItemDto> result = new PageResultDto<ItemDto>();
        var items = _repository.GetQueryable()
        .Where(x => x.Name.Contains(condition.Keyword)).OrderBy(condition.Sorting);
        
        result.TotalCount = items.ToList().Count();
        var list = items.Skip(condition.SkipCount).Take(condition.MaxResultCount);

        result.Items = _autoMapper.Map<List<Item>, List<ItemDto>>(list.ToList());

        return Json(result);
    }

    [HttpGet]
    public IActionResult Index()
    {
        ViewBag.ListItem = _autoMapper.Map<List<Item>, List<ItemDto>>(_repository.GetList());
        return View();
    }

    [HttpGet]
    public IActionResult Create()
    {
        var selectCategory = new SelectList(Enum.GetValues(typeof(Category)));
        ViewBag.SelectCategory = selectCategory;

        return View();
    }

    [HttpPost]
    public IActionResult Create(ItemCreateUpdateDto item)
    {
        if(ModelState.IsValid)
        {
            _repository.Create(_autoMapper.Map<ItemCreateUpdateDto, Item>(item));
            
            return RedirectToAction("Index");
        }
        var selectCategory = new SelectList(Enum.GetValues(typeof(Category)));
        ViewBag.SelectCategory = selectCategory;

        return View();
    }

    [HttpGet]
    public IActionResult Update(Guid id)
    {
        var item = _repository.GetById(id);

        return View(_autoMapper.Map<Item, ItemCreateUpdateDto>(item));
    }

    [HttpPost]
    public IActionResult Update(ItemCreateUpdateDto item)
    {
        if(ModelState.IsValid)
        {
            _repository.Update(_autoMapper.Map<ItemCreateUpdateDto, Item>(item));

            return RedirectToAction("Index");
        }

        return View();
    }

    [HttpGet]
    [Route("Item/Delete/{id}")]
    public IActionResult Delete(Guid id)
    {
        var item = _repository.GetById(id);
        _repository.Delete(item);

        return RedirectToAction("Index");
    }

    [HttpGet]
    [Route("Item/View/{id}")]
    public IActionResult View(Guid id)
    {
        var item = _repository.GetById(id);
        
        return View(_autoMapper.Map<Item, ItemDto>(item));
    }
}
