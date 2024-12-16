using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using NumberToWordConverter.Models;
using NumberToWordConverter.service;

namespace NumberToWordConverter.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private NumberConverterService numberConverterService = new NumberConverterService();

        public IActionResult ProcessResult(NumberConverterModel numberConverter)
        {
            String input = numberConverter.input;
            if(input == null)
            {
                numberConverter.output = "Please insert numbers";
                return View("FailedConvert", numberConverter);
            }
            String cleanInput = input.Replace(",", "");            

            if (!double.TryParse(cleanInput, out double d) && !int.TryParse(cleanInput, out int i))
            {
                numberConverter.output = "Please enter a Valid number";
                return View("FailedConvert", numberConverter);
            }

            double inputNumber = double.Parse(cleanInput);
            String words = numberConverterService.ToWord(inputNumber);
            numberConverter.output = words;

            return View("SuccessConvert", numberConverter);
        }

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
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
