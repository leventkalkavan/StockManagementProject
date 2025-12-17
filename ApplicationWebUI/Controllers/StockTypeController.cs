using BusinessLayer.DTOs.StockTypeDtos;
using BusinessLayer.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

namespace WebUI.Controllers
{
    public class StockTypeController : Controller
    {
        private readonly IStockTypeService _stockTypeService;
        private readonly IMapper _mapper;

        public StockTypeController(IStockTypeService stockTypeService, IMapper mapper)
        {
            _stockTypeService = stockTypeService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var stockTypes = await _stockTypeService.GetAllAsync(onlyActive: true);
            return View(stockTypes);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StockTypeCreateDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            await _stockTypeService.CreateAsync(dto);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, StockTypeUpdateDto dto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Id = id;
                return View(dto);
            }

            await _stockTypeService.UpdateAsync(id, dto);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Deactivate(Guid id)
        {
            await _stockTypeService.DeactivateAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
