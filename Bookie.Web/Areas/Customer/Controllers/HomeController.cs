using Bookie.Models.ViewModels;
using Bookie.Web.Areas.Customer.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Bookie.Web.Areas.Customer.Controllers;

[Area("customer")]
public class HomeController : Controller
{
    private readonly ICustomerService _customerService;

    public HomeController(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    public async Task<IActionResult> IndexAsync()
    {
        var products = await _customerService.GetAllProductsAsync();
        return View(products);
    }

    public async Task<IActionResult> Details(int id)
    {
        var product = await _customerService.GetProductAsync(id);   
        return View(product);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
