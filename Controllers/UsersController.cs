using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SiliconValley.Integration.Regres;
using SiliconValley.Integration.Regres.dto;

namespace SiliconValley.Controllers
{
    public class UsersController : Controller
    {
        private readonly ILogger<UsersController> _logger;
        private readonly RegresApiIntegration _integracion;

        public UsersController(ILogger<UsersController> logger, RegresApiIntegration integracion)
        {
            _logger = logger;
            _integracion = integracion;
        }

        public async Task<IActionResult> Index()
        {
            List<Users> users = await _integracion.GetAll();

            // logica para filtrar data del servicio
            List<Users> filtro = users
            .OrderBy(users => users.id)            
            .ToList();
            return View(filtro);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }

        public async Task<IActionResult> Details(int? id)
        {


            Users ?user = await _integracion.GetUserById(id);
            
            return View(user);
        }
        public IActionResult Registro()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registro(Users newUser)
        {
            if (ModelState.IsValid)
            {
                ViewData["msj"]=await _integracion.CreateUser(newUser);
                return View();
            }

            return View();
        }
    
    }
}