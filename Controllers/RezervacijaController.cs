﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GET_Biblioteka.Data;
using GET_Biblioteka.Models;
using Microsoft.AspNetCore.Authorization;
using GET_Biblioteka.Services;
using System.Security.Claims;

namespace GET_Biblioteka.Controllers
{
    [Authorize]
    public class RezervacijaController : Controller
    {
        private readonly InterfaceRezervacijaService _service;
        public RezervacijaController(InterfaceRezervacijaService service)
        {
            _service = service;
        }

        // prikaz ViewModela Rezervacija, u zavisnosti od role
        public IActionResult Index()
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            if (_service.FindUserRole(userId).Equals(true))
            {
                // vraca listu ViewModel Rezervacija
                var bvm = _service.GetAllReservations();
                return View(bvm);
            }
            else
            {
                // vraca listu ViewModel Rezervacija za odredjenog korisnika
                var bvm = _service.GetAllReservationsByUser(userId);
                return View("IndexKorisnik", bvm);
            }
        }

        // poziv akcije iz Rezervacija/Index.cshtml, uzima id rezervacije i prosledjuje za brisanje
        // zatim poziva Index da opet renderuje View
        [HttpPost]
        public IActionResult DeleteReservation([FromForm(Name = "reservationId")] int ReservationId)
        {
            _service.DeleteReservation(ReservationId);
            return RedirectToAction("Index", "Rezervacija");
        }
    }
}
