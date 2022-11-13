using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GET_Biblioteka.Data;
using GET_Biblioteka.Models;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using GET_Biblioteka.Services;

namespace GET_Biblioteka.Controllers
{
    [Authorize]
    public class KnjigaController : Controller
    {
        private readonly InterfaceKnjigaService _service;
        private readonly InterfaceRezervacijaService _reservationService;
        public KnjigaController(InterfaceKnjigaService service, InterfaceRezervacijaService reservationService)
        {
            _service = service;
            _reservationService = reservationService;
        }


        public async Task<IActionResult> Index()
        {
            var authResult = await HttpContext.AuthenticateAsync();
            var userId = authResult.Principal.FindFirst(ClaimTypes.NameIdentifier).Value;
            KnjigeViewModel bvm = _service.GetAllBooks(userId);
            if (_service.FindUserRole(userId).Equals(true))
                return View(bvm);
            else
                return View("IndexKorisnik", bvm);
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult UserIndex(KnjigeViewModel bvm)
        {
            return View(bvm);
        }
        [HttpPost]
        public IActionResult Create(Knjiga newBook)
        {
            _service.Create(newBook);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> CreateReservation([FromForm(Name = "bookId")] int bookId)
        {
            var authResult = await HttpContext.AuthenticateAsync();
            var userId = authResult.Principal.FindFirst(ClaimTypes.NameIdentifier).Value.ToString();
            var result = _reservationService.Create(bookId, userId);
            return RedirectToAction("Index", "Knjiga");
        }

    }
}
