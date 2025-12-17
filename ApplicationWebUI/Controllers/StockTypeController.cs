using BusinessLayer.DTOs.StockTypeDtos;
using BusinessLayer.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

namespace WebUI.Controllers
{
    public class StockTypeController : Controller
    {
        private const int DefaultPageSize = 10;
        private readonly IStockTypeService _stockTypeService;
        private readonly IMapper _mapper;

        public StockTypeController(IStockTypeService stockTypeService, IMapper mapper)
        {
            _stockTypeService = stockTypeService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int page = 1, int pageSize = DefaultPageSize, string? search = null)
        {
            return await BuildIndexAsync(page, pageSize, search);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StockTypeCreateDto dto)
        {
            if (!ModelState.IsValid)
            {
                var paged = await _stockTypeService.GetPagedAsync(1, DefaultPageSize, null, onlyActive: true);
                return View("Index", paged);
            }

            await _stockTypeService.CreateAsync(dto);
            TempData["SuccessMessage"] = "Stok türü eklendi.";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, StockTypeUpdateDto dto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Id = id;
                var paged = await _stockTypeService.GetPagedAsync(1, DefaultPageSize, null, onlyActive: true);
                return View("Index", paged);
            }

            await _stockTypeService.UpdateAsync(id, dto);
            TempData["SuccessMessage"] = "Stok türü güncellendi.";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Deactivate(Guid id)
        {
            await _stockTypeService.DeactivateAsync(id);
            TempData["SuccessMessage"] = "Stok türü pasif hale getirildi.";
            return RedirectToAction(nameof(Index));
        }

        private async Task<IActionResult> BuildIndexAsync(int page, int pageSize, string? search)
        {
            var paged = await _stockTypeService.GetPagedAsync(page, pageSize, search, onlyActive: true);
            return View("Index", paged);
        }
    }
}
