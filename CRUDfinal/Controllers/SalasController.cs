using System.Numerics;
using CRUDfinal.Models;
using CRUDfinal.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CRUDfinal.Controllers
{
    public class SalasController : Controller
    {
        RepositorySala repo;

        public SalasController()
        {
            this.repo = new RepositorySala();
        }
        public IActionResult Index()
        {
            List<Sala> salas = this.repo.GetSalas();
            return View(salas);
        }

        public IActionResult Detalles(int Sala_cod)
        {
            Sala sala = this.repo.DetalleSala(Sala_cod);
            return View(sala);
        }

        public IActionResult Delete(int Sala_cod)
        {
            this.repo.DeleteAsync(Sala_cod);
            return RedirectToAction("Index");
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(int Hospital_cod, int Sala_cod, string Nombre, string Numcama)
        {
            await this.repo.CreateSalaAsync(Hospital_cod, Sala_cod, Nombre, Numcama);
            return RedirectToAction("Index");
        }

        public IActionResult Update(int Sala_cod)
        {
            Sala sala = this.repo.DetalleSala(Sala_cod);
            return View(sala);
        }
        [HttpPost]
        public async Task<IActionResult> Update(Sala sala)
        {
            await this.repo.UpdateSalasAsync(sala.Hospital_cod, sala.Sala_cod, sala.Nombre, sala.Numcama.ToString());
            return RedirectToAction("Index");
        }

        public IActionResult BuscadorSala()
        {
            ViewData["HOSPITAL_COD"] = this.repo.GetIdHospitales();
            return View();
        }

        [HttpPost]
        public IActionResult BuscadorSala(int Hospital_cod)
        {
            ViewData["HOSPITAL_COD"] = this.repo.GetIdHospitales();
            List<Sala> salas = this.repo.GetSalaIdHopsital(Hospital_cod);
            return View(salas);

        }
    }
}
