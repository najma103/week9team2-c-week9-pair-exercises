using SSGeek.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SSGeek.Controllers
{
    public class CalculatorsController : Controller
    {
        // INSTRUCTIONS
        // As a part of each exercise you will need to 
        // - develop a view for AlienAge, AlienWeight, and AlienTravel that displays a form to submit data
        // - develop a model for the forms to bind to when the form request is submitted
        // - create a new action to process the form submission (e.g. AlienAgeResult, AlienWeightResult, etc.)
        // - create a view that displays the submitted form result

        // GET: Calculators/AlienAge
        public ActionResult AlienAge()
        {
            return View("AlienAge");
        }

        public ActionResult AlienAgeResult(AlienAgeModel model)
        {
            ViewBag.message = "AlienAgeResult";
            
            AlienAgeModel alienAge = new AlienAgeModel();
            string result = Request.Params["Age"];
            alienAge.Age = Convert.ToInt32(result);
            alienAge.PlanetName = Convert.ToString(Request.Params["PlanetName"]);

            if (alienAge.PlanetName == "Mercury" || alienAge.PlanetName == "Venus" 
                 || alienAge.PlanetName == "Mars")
            {
                double earthAge = 365.26 / modelPlanets[alienAge.PlanetName];
                alienAge.AgeResult = earthAge * alienAge.Age;
            } else
            {
                alienAge.AgeResult = alienAge.Age/ modelPlanets[alienAge.PlanetName];
            }

           

            return View("AlienAgeResult", alienAge);
        }
        
        //TODO: Create an AlienWeight and AlienWeightResult Action
        public ActionResult AlienWeight()
        {
            return View("AlienWeight");
        }
        public ActionResult AlienWeightResult(AlienWeightModel model)
        {
            return View("AlienWeightResult", model);
        }
        //TODO: Create an AlienTravel and AlienTravelResult Action


        private Dictionary<string, double> modelPlanets = new Dictionary<string, double>()
        {
            ["Mercury"] = 87.97, 
            ["Venus"] = 224.68,
            ["Mars"] = 686.98,
            ["Jupiter"] = 11.862,
            ["Saturn"] = 29.456,
            ["Uranus"] = 84.07,
            ["Neptune"] = 164.81
        };

        private List<SelectListItem> transportationModes = new List<SelectListItem>()
        {
            new SelectListItem() { Text = "Walking", Value="walking" },
            new SelectListItem() { Text = "Car", Value = "car" },
            new SelectListItem() { Text = "Bullet Train", Value = "bullet train" },
            new SelectListItem() { Text = "Boeing 747", Value = "boeing 747" },
            new SelectListItem() { Text = "Concorde", Value = "concorde" }
        };
    }
}