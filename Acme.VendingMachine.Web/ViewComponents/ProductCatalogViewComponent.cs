using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Acme.VendingMachine.BusinessLogic;
using Microsoft.AspNetCore.Mvc;

namespace Acme.VendingMachine.Web.ViewComponents
{
    public class ProductCatalogViewComponent : ViewComponent
    {
        private readonly IProductBll _productBll;

        public ProductCatalogViewComponent(IProductBll productBll)
        {
            _productBll = productBll;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var products = await Task.Run(() => _productBll.GetAllProducts());
            return View(products);
        }
    }
}
