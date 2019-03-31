using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Acme.VendingMachine.BusinessLogic;
using Microsoft.AspNetCore.Mvc;

namespace Acme.VendingMachine.Web.ViewComponents
{
    public class DisplayViewComponent : ViewComponent
    {
        private readonly IMachineBll _machineBll;
        private IList<string> _messages = new List<string>();

        public DisplayViewComponent(IMachineBll MachineBll)
        {
            _machineBll = MachineBll;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var machine = await Task.Run(() => _machineBll.GetMachine());
            return View(machine);
        }
    }
}
