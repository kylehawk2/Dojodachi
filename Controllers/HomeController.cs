using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Dojodachi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Session;
using Newtonsoft.Json;

namespace Dojodachi.Controllers
{
    public class HomeController : Controller
    {
        public Random chance = new Random();

        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {   
            if(HttpContext.Session.GetString("status") == null){

                HttpContext.Session.SetString("status", "They are fine!");
                HttpContext.Session.SetString("alive", "alive");
                HttpContext.Session.SetInt32("fullness", 20);
                HttpContext.Session.SetInt32("happiness", 20);
                HttpContext.Session.SetInt32("energy", 50);
                HttpContext.Session.SetInt32("meals", 3);
                HttpContext.Session.SetString("message", "");
            }

            int? fullness = HttpContext.Session.GetInt32("fullness");
            int? happiness = HttpContext.Session.GetInt32("happiness");
            int? energy = HttpContext.Session.GetInt32("energy");
            int? meals = HttpContext.Session.GetInt32("meals");
            string message = HttpContext.Session.GetString("message");
            string status = HttpContext.Session.GetString("status");
            string alive = HttpContext.Session.GetString("alive");

            ViewBag.fullness = (int) fullness;
            ViewBag.happiness = (int) happiness;
            ViewBag.energy = (int) energy;
            ViewBag.meals = (int) meals;
            ViewBag.message = message;
            ViewBag.status = status;
            ViewBag.alive = alive;

            if(fullness ==100 && happiness == 100 && energy == 100)
            {
                HttpContext.Session.SetString("message", "Winner Winner Chicken Dinner");
            }
            if(fullness == 0)
            {
                HttpContext.Session.SetString("message", "Oh no! Your Dachi starved to Death!");
            }
            if(fullness == 0)
            {
                HttpContext.Session.SetString("message", "Oh no! Your Dachi has lost the will to live!");
            }
            
            return View("Index");
        }
        [HttpGet]
        [Route("feed")]
        public IActionResult Feed()
        {   
            int? meals = HttpContext.Session.GetInt32("meals");
            int? fullness = HttpContext.Session.GetInt32("fullness") + chance.Next(5,11);

            if(meals > 0)
            {
                meals -= 1;
                HttpContext.Session.SetInt32("meals", (int) meals);
                HttpContext.Session.SetInt32("fullness", (int) fullness);
            }
            if(meals == 0)
            {
                HttpContext.Session.SetString("message", "You have nor more meals!");
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        [Route("play")]
        public IActionResult Play()
        {
            int? energy = HttpContext.Session.GetInt32("energy");
            int? happiness = HttpContext.Session.GetInt32("happiness") + chance.Next(5,10);
        if(energy > 0)
        {  
            energy -=5;
            HttpContext.Session.SetInt32("energy", (int) energy);
            HttpContext.Session.SetInt32("happiness", (int) happiness);
        
        }
        if (energy == 0)
        {
            HttpContext.Session.SetString("message", "You need a cup of coffee");
        }
        if (happiness == 0)
        {
            HttpContext.Session.SetString("message", "You are a sad PANDA :( ");
        }
        return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("work")]
        public IActionResult Work()
        {
            int? energy = HttpContext.Session.GetInt32("energy");
            int? meals = HttpContext.Session.GetInt32("meals") + chance.Next(1,3);
        
        if (energy > 0)
        {
            energy -=5;
            HttpContext.Session.SetInt32("energy", (int) energy);
            HttpContext.Session.SetInt32("meals", (int) meals);
        }
        return RedirectToAction("Index");
        }
        [HttpGet]
        [Route("sleep")]

        public IActionResult Sleep()
        {
            int? energy = HttpContext.Session.GetInt32("energy");
            int? happiness = HttpContext.Session.GetInt32("happiness");
            int? fullness = HttpContext.Session.GetInt32("fullness");
        
        if (energy > 0)
        {
            energy +=15;
            HttpContext.Session.SetInt32("energy", (int) energy);
        }
        if (happiness == 0)
        {
            happiness -=5;
            HttpContext.Session.SetInt32("happiness", (int) happiness);
        }
        if (fullness > 0)
        {
            fullness -=5;
            HttpContext.Session.SetInt32("fullness", (int) fullness);
        }
        return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("clear")]
        public IActionResult Clear()
        {
        return View("Index");
        }
    }
}
