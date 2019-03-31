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

        #region Numbers

        public IActionResult Process0()
        {
            return ProcessNumber("0");
        }

        public IActionResult Process1()
        {
            return ProcessNumber("1");
        }

        public IActionResult Process2()
        {
            return ProcessNumber("2");
        }

        public IActionResult Process3()
        {
            return ProcessNumber("3");
        }

        public IActionResult Process4()
        {
            return ProcessNumber("4");
        }

        public IActionResult Process5()
        {
            return ProcessNumber("5");
        }

        public IActionResult Process6()
        {
            return ProcessNumber("6");
        }

        public IActionResult Process7()
        {
            return ProcessNumber("7");
        }

        public IActionResult Process8()
        {
            return ProcessNumber("8");
        }

        public IActionResult Process9()
        {
            return ProcessNumber("9");
        }

        private IActionResult ProcessNumber(string number)
        {
            _machineBll.EnterNumber(number);

            return RedirectToAction("Index");
        }

        #endregion

        #region Function Keys

        public IActionResult ProcessDel()
        {
            _machineBll.Delete();

            return RedirectToAction("Index");
        }

        public IActionResult ProcessClear()
        {
            _machineBll.Clear();

            return RedirectToAction("Index");
        }

        public IActionResult ProcessDone()
        {
            _machineBll.Done();

            return RedirectToAction("Index");
        }

        public IActionResult ProcessCancel()
        {
            _machineBll.Cancel();

            return RedirectToAction("Index");
        }

        #endregion
    }
}