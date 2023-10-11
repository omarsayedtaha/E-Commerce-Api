using CoreLayer.Entities;
using CoreLayer.Repository;
using Microsoft.AspNetCore.Mvc;

namespace AdminPanel.Controllers
{
	public class BrandsController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;

		public BrandsController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}
		public async Task<IActionResult> Index()
		{
			var productbrands = await _unitOfWork.Repository<ProductBrand>().GetAllAsync();

			return View(productbrands);
		}
		
		public async Task<IActionResult> Create()
		{

			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Create(ProductBrand model)
		{
			if (ModelState.IsValid)
			{
				try
				{
                    await _unitOfWork.Repository<ProductBrand>().Add(model);
					await _unitOfWork.CompleteAsync();
					return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
				{
					ModelState.AddModelError("Name", "This brand already exist");
					return View("Index", await _unitOfWork.Repository<ProductBrand>().GetAllAsync());
				}
				

			}

			return View(model);
		}

       
        public async Task<IActionResult> Delete(int id)
        {
			var productbrand = await _unitOfWork.Repository<ProductBrand>().GetByIdAsync(id);
		    _unitOfWork.Repository<ProductBrand>().Delete(productbrand);
			await _unitOfWork.CompleteAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
