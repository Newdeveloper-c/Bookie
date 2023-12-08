﻿using Bookie.DataAccess.Repository.IRepository;
using Bookie.Models.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Bookie.Web.Areas.Admin.Services;

public class ProductService : IProductService
{
    private readonly IUnitOfWork _unitOfWork;

    public ProductService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task AddProductAsync(Product product)
    {
        await _unitOfWork.Product.AddAsync(product);

        await _unitOfWork.SaveAsync();
    }

    public async Task DeleteProductAsync(Product product)
    {
        _unitOfWork.Product.Remove(product);
        await _unitOfWork.SaveAsync();
    }

    public async Task<IEnumerable<Product>> GetAllProductsAsync()
        => await _unitOfWork.Product.GetAllAsync("Category")
            ?? Enumerable.Empty<Product>();

    public async Task UpdateProductAsync(Product product)
    {
        _unitOfWork.Product.Update(product);
        await _unitOfWork.SaveAsync();
    }

    public async Task<Product?> GetProductAsync(int? productId)
        => await _unitOfWork.Product.GetAsync(c => c.Id == productId, "Category");

    public async Task<IEnumerable<SelectListItem>> GetAllCategoriesAsync()
        => (await _unitOfWork.Category.GetAllAsync())
            .Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString()
            });

    public async Task<SelectListItem> GetCategoryAsync(int categoryId)
        => new SelectListItem
        {
            Text = (await _unitOfWork.Category.GetAsync(c => c.Id == categoryId))?.Name,
            Value = categoryId.ToString()
        };
}
