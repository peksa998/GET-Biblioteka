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

        // prikaz ViewModela knjiga, u zavisnosti od role
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

        // renderuje prikaz za create
        public IActionResult Create()
        {
            return View();
        }

        // ovo ne treba, jer UserIndex pozivam direktno preko View
        public IActionResult UserIndex(KnjigeViewModel bvm)
        {
            return View(bvm);
        }

        // poziv akcije iz Knjiga/Create.cshtml, uzima podatke sa forme i prosledjuje kao Knjiga
        [HttpPost]
        public IActionResult Create(Knjiga newBook)
        {
            _service.Create(newBook);
            return RedirectToAction("Index");
        }

        // poziv akcije iz Knjiga/IndexKorisnik.cshtml, uzima sa forme id knjige, trazi id od registrovanog korisnika
        // pa prosledjuje te podatke za kreiranje rezervacije
        // zatim poziva Index da opet renderuje View
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
