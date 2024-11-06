using Microsoft.AspNetCore.Mvc;
using ProductSystem.BusinessLogic.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using ProductSystem.DataAccess.Models;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Linq;
using System;
using ProductSystem.BusinessLogic.ViewModels;

namespace ProductSystem.Presentation.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly UserManager<ApplicationUser> _userManager;

        public ProductController(IProductService productService, ICategoryService categoryService, UserManager<ApplicationUser> userManager)
        {
            _productService = productService;
            _categoryService = categoryService;
            _userManager = userManager;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Index(string searchString, string category = "All")
        {
            var products = await _productService.GetAll();

            // Populate categories for the view
            ViewBag.Categories = await _categoryService.GetAllCategories();

            // Filter by category if specified
            if (category != "All")
            {
                products = products.Where(p => p.Category.Name == category).ToList();
            }

            // Search by name if specified
            if (!string.IsNullOrEmpty(searchString))
            {
                products = products.Where(p => p.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            return View(products);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var categories = await _categoryService.GetAllCategories();
            var users = _userManager.Users.ToList();

            var viewModel = new ProductViewModel
            {
                Categories = categories.Select(c => new SelectOption { Id = c.Id.ToString(), Name = c.Name }).ToList(),
                Users = users.Select(u => new SelectOption { Id = u.Id, Name = u.UserName }).ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            if (!ModelState.IsValid)
            {
                var categories = await _categoryService.GetAllCategories();
                ViewBag.Categories = categories;
                return View(product);
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            product.CreatedByUserId = userId;
            await _productService.Add(product);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var product = await _productService.GetById(id);
            if (product == null)
            {
                return NotFound();
            }

            var categories = await _categoryService.GetAllCategories();
            ViewBag.Categories = categories;

            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Product product)
        {
            if (!ModelState.IsValid)
            {
                var categories = await _categoryService.GetAllCategories();
                ViewBag.Categories = categories;
                return View(product);
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _productService.Update(product.Id, product, userId);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _productService.DeleteById(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var product = await _productService.GetById(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }
    }
}
