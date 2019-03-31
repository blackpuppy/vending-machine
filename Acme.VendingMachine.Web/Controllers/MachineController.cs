using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Acme.VendingMachine.BusinessLogic;
using Microsoft.AspNetCore.Mvc;

namespace Acme.VendingMachine.Web.Controllers
{
    public class MachineController : Controller
    {
        private readonly IMachineBll _machineBll;

        public MachineController(IMachineBll MachineBll)
        {
            _machineBll = MachineBll;
        }

        public IActionResult Index()
        {
            var machine = _machineBll.GetMachine();
            return View(machine);
        }

        public async Task<IActionResult> Process7Async()
        {
            var machine = await Task.Run(() => _machineBll.GetMachine());
            machine.AppendMessage("7");
            return View("~/Views/Home/Index", machine);
        }
    }
}