using BusinessLayer.DTOs;
using BusinessLayer.DTOs.StockItemDtos;
using BusinessLayer.Services.Abstractions;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Globalization;

namespace WebUI.Controllers
{
    public class StockItemController : Controller
    {
        private const int DefaultPageSize = 10;
        private readonly IStockItemService _stockItemService;
        private readonly IStockUnitService _stockUnitService;
        private readonly IMapper _mapper;

        public StockItemController(IStockItemService stockItemService, IStockUnitService stockUnitService, IMapper mapper)
        {
            _stockItemService = stockItemService;
            _stockUnitService = stockUnitService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int page = 1, int pageSize = DefaultPageSize, string? search = null)
        {
            return await BuildIndexAsync(page, pageSize, search);
        }


        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await LoadStockUnitDropdownAsync();
            LoadStockClassDropdown();

            return View(new StockItemCreateDto());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StockItemCreateDto dto)
        {
            var quantityRaw = Request.Form["Quantity"].ToString();
            var criticalQuantityRaw = Request.Form["CriticalQuantity"].ToString();

            dto.Quantity = NormalizeNumber(dto.Quantity, quantityRaw);
            dto.CriticalQuantity = NormalizeNumber(dto.CriticalQuantity, criticalQuantityRaw);

            if (!ModelState.IsValid)
            {
                await LoadStockUnitDropdownAsync(dto.StockUnitId);
                LoadStockClassDropdown(dto.StockClass);
                var paged = await _stockItemService.GetPagedAsync(1, DefaultPageSize, null, onlyActive: true);
                return View("Index", paged);
            }

            await _stockItemService.CreateAsync(dto);
            TempData["SuccessMessage"] = "Stok item eklendi.";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, StockItemUpdateDto dto)
        {
            var quantityRaw = Request.Form["Quantity"].ToString();
            var criticalQuantityRaw = Request.Form["CriticalQuantity"].ToString();

            dto.Quantity = NormalizeNumber(dto.Quantity, quantityRaw);
            dto.CriticalQuantity = NormalizeNumber(dto.CriticalQuantity, criticalQuantityRaw);

            if (!ModelState.IsValid)
            {
                ViewBag.Id = id;
                await LoadStockUnitDropdownAsync(dto.StockUnitId);
                LoadStockClassDropdown(dto.StockClass);
                var paged = await _stockItemService.GetPagedAsync(1, DefaultPageSize, null, onlyActive: true);
                return View("Index", paged);
            }

            await _stockItemService.UpdateAsync(id, dto);
            TempData["SuccessMessage"] = "Stok item guncellendi.";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Deactivate(Guid id)
        {
            await _stockItemService.DeactivateAsync(id);
            TempData["SuccessMessage"] = "Stok item pasif hale getirildi.";
            return RedirectToAction(nameof(Index));
        }

        private async Task LoadStockUnitDropdownAsync(Guid? selectedId = null)
        {
            var units = await _stockUnitService.GetAllAsync(onlyActive: true);

            ViewBag.StockUnits = units
                .OrderBy(u => u.Code)
                .Select(u => new SelectListItem
                {
                    Value = u.Id.ToString(),
                    Text = string.IsNullOrWhiteSpace(u.Description)
                        ? $"{u.Code} ({u.StockTypeName})"
                        : $"{u.Code} - {u.Description} ({u.StockTypeName})",
                    Selected = selectedId.HasValue && u.Id == selectedId.Value
                })
                .ToList();
        }

        private void LoadStockClassDropdown(string? selected = null)
        {
            ViewBag.StockClasses = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Value = "RawMaterial",
                    Text = "Hammadde",
                    Selected = selected == "RawMaterial"
                }
            };
        }

        private static decimal NormalizeNumber(decimal parsedValue, string rawInput)
        {
            if (string.IsNullOrWhiteSpace(rawInput))
                return parsedValue;

            var cleaned = rawInput.Trim();
            var lastComma = cleaned.LastIndexOf(',');
            var lastDot = cleaned.LastIndexOf('.');

            if (lastComma > -1 && lastDot > -1)
            {
                if (lastComma > lastDot)
                {
                    cleaned = cleaned.Replace(".", "");
                    cleaned = cleaned.Replace(',', '.');
                }
                else
                {
                    cleaned = cleaned.Replace(",", "");
                }
            }
            else if (lastComma > -1)
            {
                cleaned = cleaned.Replace(".", "");
                cleaned = cleaned.Replace(',', '.');
            }
            else if (lastDot > -1)
            {
                cleaned = cleaned.Replace(",", "");
            }

            return decimal.TryParse(cleaned, NumberStyles.Number, CultureInfo.InvariantCulture, out var value)
                ? value
                : parsedValue;
        }

        private async Task<IActionResult> BuildIndexAsync(int page, int pageSize, string? search)
        {
            await LoadStockUnitDropdownAsync();
            LoadStockClassDropdown();

            var paged = await _stockItemService.GetPagedAsync(page, pageSize, search, onlyActive: true);
            return View("Index", paged);
        }
    }
}
