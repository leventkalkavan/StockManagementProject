using BusinessLayer.DTOs.StockUnitDtos;
using BusinessLayer.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using AutoMapper;
using System.Globalization;

namespace WebUI.Controllers
{
    public class StockUnitController : Controller
    {
        private const int DefaultPageSize = 10;
        private readonly IStockUnitService _stockUnitService;
        private readonly IStockTypeService _stockTypeService;
        private readonly IMapper _mapper;

        public StockUnitController(IStockUnitService stockUnitService, IStockTypeService stockTypeService, IMapper mapper)
        {
            _stockUnitService = stockUnitService;
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
        public async Task<IActionResult> Create(StockUnitCreateDto dto)
        {
            var buyingRaw = Request.Form["BuyingPrice"].ToString();
            var sellingRaw = Request.Form["SellingPrice"].ToString();

            dto.BuyingPrice = NormalizePrice(dto.BuyingPrice, buyingRaw);
            dto.SellingPrice = NormalizePrice(dto.SellingPrice, sellingRaw);

            if (!ModelState.IsValid)
            {
                await LoadStockTypeDropdownAsync(dto.StockTypeId);
                LoadQuantityUnitDropdown(dto.QuantityUnit);
                LoadCurrencyDropdowns(dto.BuyingCurrency, dto.SellingCurrency);

                ViewBag.OpenCreateModal = true;
                ViewBag.CreateModel = dto;
                var paged = await _stockUnitService.GetPagedAsync(1, DefaultPageSize, null, onlyActive: true);
                return View("Index", paged);
            }

            await _stockUnitService.CreateAsync(dto);
            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, StockUnitUpdateDto dto)
        {
            var buyingRaw = Request.Form["BuyingPrice"].ToString();
            var sellingRaw = Request.Form["SellingPrice"].ToString();

            dto.BuyingPrice = NormalizePrice(dto.BuyingPrice, buyingRaw);
            dto.SellingPrice = NormalizePrice(dto.SellingPrice, sellingRaw);

            if (!ModelState.IsValid)
            {
                await LoadStockTypeDropdownAsync(dto.StockTypeId);
                LoadQuantityUnitDropdown(dto.QuantityUnit);
                LoadCurrencyDropdowns(dto.BuyingCurrency, dto.SellingCurrency);

                ViewBag.OpenEditModal = true;
                ViewBag.EditId = id;
                ViewBag.EditModel = dto;
                var paged = await _stockUnitService.GetPagedAsync(1, DefaultPageSize, null, onlyActive: true);
                return View("Index", paged);
            }

            await _stockUnitService.UpdateAsync(id, dto);
            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Deactivate(Guid id)
        {
            await _stockUnitService.DeactivateAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private static decimal NormalizePrice(decimal parsedValue, string rawInput)
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

        private async Task LoadStockTypeDropdownAsync(Guid? selectedId = null)
        {
            var types = await _stockTypeService.GetAllAsync(onlyActive: true);

            ViewBag.StockTypes = types
                .OrderBy(t => t.Name)
                .Select(t => new SelectListItem
                {
                    Value = t.Id.ToString(),
                    Text = t.Name,
                    Selected = selectedId.HasValue && t.Id == selectedId.Value
                })
                .ToList();
        }
        private void LoadQuantityUnitDropdown(QuantityUnit? selected = null)
        {
            var values = Enum.GetValues(typeof(QuantityUnit)).Cast<QuantityUnit>();

            string Label(QuantityUnit v) => v switch
            {
                QuantityUnit.Piece => "Adet",
                QuantityUnit.Kg => "Gram",
                QuantityUnit.Meter => "Metre",
                QuantityUnit.Liter => "Litre",
                QuantityUnit.M3 => "m3",
                _ => v.ToString()
            };

            ViewBag.QuantityUnits = values
                .Select(v => new SelectListItem
                {
                    Value = ((int)v).ToString(),
                    Text = Label(v),
                    Selected = selected.HasValue && v == selected.Value
                })
                .ToList();
        }

        private void LoadCurrencyDropdowns(Currency? buyingSelected = null, Currency? sellingSelected = null)
        {
            ViewBag.BuyingCurrencies = BuildCurrencyDropdown(buyingSelected);
            ViewBag.SellingCurrencies = BuildCurrencyDropdown(sellingSelected);
        }

        private static List<SelectListItem> BuildCurrencyDropdown(Currency? selected = null)
        {
            var values = Enum.GetValues(typeof(Currency)).Cast<Currency>();

            string Label(Currency v) => v switch
            {
                Currency.TurkishLira => "Turkish Lira (TRY)",
                Currency.Euro => "Euro (EUR)",
                Currency.Dollar => "Dollar (USD)",
                _ => v.ToString()
            };

            return values
                .Select(v => new SelectListItem
                {
                    Value = ((int)v).ToString(),
                    Text = Label(v),
                    Selected = selected.HasValue && v == selected.Value
                })
                .ToList();
        }

        private async Task<IActionResult> BuildIndexAsync(int page, int pageSize, string? search)
        {
            var paged = await _stockUnitService.GetPagedAsync(page, pageSize, search, onlyActive: true);

            await LoadStockTypeDropdownAsync();
            LoadQuantityUnitDropdown();
            LoadCurrencyDropdowns();

            return View("Index", paged);
        }
    }
}
