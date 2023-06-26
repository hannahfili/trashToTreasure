using Microsoft.AspNetCore.Mvc;
using TrashToTreasure.Interfaces;
using TrashToTreasure.Models.Models;

namespace TrashToTreasure.Controllers
{
    public class AdvertisementController : Controller
    {
        public readonly IUnitOfWork UnitOfWork;
        public AdvertisementController(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> CreateAdvertisementAsync(Advertisement advertisement)
        {
            await UnitOfWork.AvertisementRepository.AddAsync(advertisement);
            int result = 0;
            if(advertisement.Title!=null && advertisement.Description != null)
            {
                result = UnitOfWork.Save();
            }
            if (result!=0) await UnitOfWork.AvertisementRepository.GetByIdAsync(advertisement.Id);
            //return View(result);
            return RedirectToAction(nameof(Created), new { result, advertisement });
        }
        public IActionResult Created(int result, Advertisement advertisement)
        {
            ViewBag.Result = result;
            return View(advertisement);
        }
        public IActionResult Create()
        {
            var ad = new Advertisement();
            return View(ad);
        }
    }
}
