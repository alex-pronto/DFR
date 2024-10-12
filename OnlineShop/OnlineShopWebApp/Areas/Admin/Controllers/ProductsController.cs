using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Helpers;
using OnlineShopWebApp.Models;
using System;

namespace OnlineShopWebApp.Areas.Admin.Controllers
{
    [Area(Constants.AdminRoleName)]
    [Authorize(Roles = Constants.AdminRoleName)]
    public class ProductsController : Controller
    {
        public IDanceFloorsRepository danceFloorsRepository;
        public IDanceFloorsHistoryChangesRepository danceFloorsHistoryChangesRepository;

        public ProductsController(IDanceFloorsRepository danceFloorsRepository, IDanceFloorsHistoryChangesRepository danceFloorsHistoryChangesRepository)
        {
            this.danceFloorsRepository = danceFloorsRepository;
            this.danceFloorsHistoryChangesRepository = danceFloorsHistoryChangesRepository;
        }


        public IActionResult Index()
        {
            var danceFloors = danceFloorsRepository.GetDanceFloors();
            var danceFloorViewModels = danceFloors.ToDanceFoorsViewModel();
            return View(danceFloorViewModels);
        }

        public IActionResult Edit(Guid Id)
        {
            var danceFloor = danceFloorsRepository.GetFloor(Id);
            var danceFloorViewModel = danceFloor.ToDanceFloorViewModel();
            return View(danceFloorViewModel);
        }

        [HttpPost]
        public IActionResult Edit(DanceFloorHistoryChanges danceFloorInformationChanges)
        {
            if (ModelState.IsValid)
            {
                var danceFloor = danceFloorsRepository.GetFloor(danceFloorInformationChanges.Id);

                danceFloor.Name = danceFloorInformationChanges.Name;
                danceFloor.Cost = danceFloorInformationChanges.Cost;
                danceFloor.Description = danceFloorInformationChanges.Description;
                danceFloorsRepository.Update(danceFloor);
                danceFloorsHistoryChangesRepository.Add(danceFloorInformationChanges);
                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        public IActionResult Delete(Guid Id)
        {
            //var danceFloorToDelete = danceFloorsHistoryChangesRepository.GetFloor(Id);
            //var danceFloor = danceFloorsRepository.GetFloor(Id);
            //if (danceFloorToDelete == null)
            //{
            //var danceFloor = danceFloorsRepository.GetFloor(Id);
            //DanceFloorHistoryChanges danceFloorInformationChanges = new DanceFloorHistoryChanges();
            //danceFloorInformationChanges.Id = Id;
            //danceFloorInformationChanges.Name = danceFloor.Name;
            //danceFloorInformationChanges.Cost = danceFloor.Cost;
            //danceFloorInformationChanges.Description = danceFloor.Description;
            //  danceFloorInformationChanges.Status = DanceFloorHistoryChanges.SetStatus.Deleted;
            //
            //  danceFloorsHistoryChangesRepository.Add(danceFloorInformationChanges);
            //}
            //else
            //{
            //  danceFloorToDelete.Status = DanceFloorHistoryChanges.SetStatus.Deleted;
            //}


            danceFloorsRepository.Delete(Id);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Add()
        {

            return View();
        }
        [HttpPost]
        public IActionResult Add(DanceFloorHistoryChanges createdDanceFloor)
        {
            if (ModelState.IsValid)
            {
                var danceFloorDb = new DanceFloor
                {
                    Name = createdDanceFloor.Name,
                    Cost = createdDanceFloor.Cost,
                    Description = createdDanceFloor.Description,
                };
                createdDanceFloor.Id = Guid.NewGuid(); // здесь айди будет генерироваться в базе данных но пока что так 
                createdDanceFloor.Status = DanceFloorHistoryChanges.SetStatus.Created;
                danceFloorsHistoryChangesRepository.Add(createdDanceFloor);

                danceFloorsRepository.Add(danceFloorDb);

                return RedirectToAction(nameof(Index));
            }
            return View();



        }
    }
}
