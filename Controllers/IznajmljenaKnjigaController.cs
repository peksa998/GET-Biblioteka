using System;
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
    public class IznajmljenaKnjigaController : Controller
    {
        private readonly InterfaceIznajmljenaKnjigaService _service;
        public IznajmljenaKnjigaController(InterfaceIznajmljenaKnjigaService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (_service.FindUserRole(userId).Equals(true))
            {
                IznajmljeneKnjigeViewModel bvm = _service.GetAllIssuedBook();
                return View(bvm);
            }
            else
            {
                IznajmljeneKnjigeViewModel bvm = _service.GetAllIssuedBooksByUser(userId);
                return View("IndexKorisnik", bvm);
            }
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult UserIndex(IznajmljenaKnjigaViewModel bvm)
        {
            return View(bvm);
        }

        public IActionResult CreateIssuedBook([FromForm(Name = "reservationId")] int ReservationId, [FromForm(Name = "userId")] string UserId, [FromForm(Name = "bookId")] int BookId)
        {
            _service.CreateIssuedBook(ReservationId, UserId, BookId);
            return RedirectToAction("Index", "Rezervacija");
        }

        [HttpPost]
        public IActionResult ReturnBook([FromForm(Name = "issuedBookId")] int IssuedBookId)
        {
            _service.ReturnBook(IssuedBookId);
            return RedirectToAction("Index", "IznajmljenaKnjiga");
        }

    }
}
