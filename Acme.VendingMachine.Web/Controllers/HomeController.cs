﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Acme.VendingMachine.Web.Models;
using Acme.VendingMachine.BusinessLogic;
using Acme.VendingMachine.Model;

namespace Acme.VendingMachine.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ProductBll bll = new ProductBll();
            IList<Product> products = bll.GetAllProducts();
            return View(products);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
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
}
