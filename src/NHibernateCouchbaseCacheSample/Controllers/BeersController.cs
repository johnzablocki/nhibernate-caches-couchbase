using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NHibernateCouchbaseCacheSample.Models;

namespace NHibernateCouchbaseCacheSample.Controllers
{
    public class BeersController : Controller
    {
		public BeerRepository BeerRepository { get; set; }
		
		public BeersController()
		{
			BeerRepository = new BeerRepository();
		}

		public BeersController(BeerRepository beerRepository)
		{
			BeerRepository = beerRepository;
		}

        //
        // GET: /Beers/

        public ActionResult Index()
        {			
            return View(BeerRepository.FindAll());
        }

        //
        // GET: /Beers/Details/5

        public ActionResult Details(int id)
        {
            return View(BeerRepository.Find(id));
        }

        //
        // GET: /Beers/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Beers/Create

        [HttpPost]
        public ActionResult Create(Beer beer)
        {
			if (ModelState.IsValid)
			{
				BeerRepository.Create(beer);
				return RedirectToAction("Index");
			}
			else
			{
				return View();
			}
        }

        //
        // GET: /Beers/Edit/5

        public ActionResult Edit(int id)
        {
            return View(BeerRepository.Find(id));
        }

        //
        // POST: /Beers/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, Beer beer)
        {
			if (ModelState.IsValid)
			{
				var savedBeer = BeerRepository.Find(id);
				savedBeer.Name = beer.Name;
				savedBeer.Brewery = beer.Brewery;
				savedBeer.ABV = beer.ABV;

				BeerRepository.Update(savedBeer);
				return RedirectToAction("Index");
			}
			else
			{
				return View();
			}
        }

        //
        // GET: /Beers/Delete/5

        public ActionResult Delete(int id)
        {
            return View(BeerRepository.Find(id));
        }

        //
        // POST: /Beers/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
			// TODO: Add delete logic here
			BeerRepository.Delete(id);
			return RedirectToAction("Index");
        }
    }
}
