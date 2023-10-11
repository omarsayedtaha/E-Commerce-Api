using AdminPanel.Helper;
using AdminPanel.Models;
//using AspNetCore;
using System.Data;
using AutoMapper;
using AutoMapper.Internal.Mappers;
using CoreLayer.Entities;
using CoreLayer.Repository;
using Microsoft.AspNetCore.Mvc;

namespace AdminPanel.Controllers
{
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductController(IUnitOfWork unitOfWork , 
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
             _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            var products = await _unitOfWork.Repository<Product>().GetAllAsync();
            var mappedProducts = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductViewModel>>(products);
            
            return View(mappedProducts);
        }

        public async Task<IActionResult> Create()
        {
            return View(); 
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Image is not null)
                    model.PictureUrl = PictureSettings.UploadFile(model.Image, "products");
                else
                    model.PictureUrl = "images/products/hat-react2.png";

                var mappedProduct = _mapper.Map<ProductViewModel,Product>(model);
                await _unitOfWork.Repository<Product>().Add(mappedProduct);
                await _unitOfWork.CompleteAsync();
                return RedirectToAction(nameof(Index));
            }
   
            return View(model);
        }
      
        public async Task<IActionResult> Edit(int id)
        {
            var product = await _unitOfWork.Repository<Product>().GetByIdAsync(id);
            var mappedProduct =  _mapper.Map<Product, ProductViewModel>(product);
            return View(mappedProduct); 
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id , ProductViewModel model)
        {
            if (id != model.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                if (model.Image is not null)
                {
                    if (model.PictureUrl is not null)
                    {
                        PictureSettings.DeleteFile(model.PictureUrl, "product");
                        model.PictureUrl=PictureSettings.UploadFile(model.Image, "product");
                    }
                    else
                        model.PictureUrl = PictureSettings.UploadFile(model.Image, "product");

                }
				else
					model.PictureUrl = "images//products//hat-react2.png";




				//var Product =await _unitOfWork.Repository<Product>().GetByIdAsync(id);
				var mappedProduct = _mapper.Map<ProductViewModel, Product>(model);
                _unitOfWork.Repository<Product>().Update(mappedProduct);
                 var resutl = await _unitOfWork.CompleteAsync();

                if (resutl>0)
                    return RedirectToAction(nameof(Index));

            }
      

            return View(model);
        }

       public async Task<IActionResult> Delete(int id)
        {
            var product = await _unitOfWork.Repository<Product>().GetByIdAsync(id);
            var mappedProduct = _mapper.Map<Product, ProductViewModel>(product);

            return View(mappedProduct);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id , ProductViewModel model)
        {
            if (id != model.Id)
                return NotFound();

            if (model.PictureUrl is not null)
                PictureSettings.DeleteFile("product" , model.PictureUrl);

            var mappedproduct = _mapper.Map<ProductViewModel, Product>(model);

               _unitOfWork.Repository<Product>().Delete(mappedproduct);

            await _unitOfWork.CompleteAsync();

            return RedirectToAction(nameof(Index));
        }
    }

 




}
