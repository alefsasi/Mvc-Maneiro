using Mvc_Maneiro.Models;
using Mvc_Maneiro.Services;
using NHibernate;
using NHibernate.Linq;
using Rotativa;
using Rotativa.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Mvc_Maneiro.Controllers
{
    public class HomeController : Controller
    {

        private readonly AlunoService _alunoService;

        public HomeController()
        {
            _alunoService = new AlunoService();
        }

        // GET: Home
        public ActionResult Index(string desc)
        {
            if (TempData["erroExcludeMessage"] != null)
            {
                ViewBag.ErroMensagem = TempData["erroExcludeMessage"].ToString();
            }

            return View(_alunoService.GetAlunos());
        }

        [Route("Buscar/{descricao}")]
        public ActionResult Buscar(string descricao)
        {
            var alunos = new List<Aluno>();
            if (!string.IsNullOrEmpty(descricao))
            {
                alunos = _alunoService.GetAlunosByDescription(descricao);
            }
            else
            {
                alunos = _alunoService.GetAlunos();
            }
            return View("index", alunos);
        }
        // GET: Home/Details/5
        public ActionResult Details(int id)
        {
            var aluno = _alunoService.GetAlunoById(id);

            if (aluno == null)
            {
                return RedirectToAction("Index");
            }

            return View(aluno);
        }

        // GET: Home/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Home/Create
        [HttpPost]
        public ActionResult Create(Aluno aluno)
        {
            try
            {
                _alunoService.CriarAluno(aluno);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        // GET: Home/Edit/5
        public ActionResult Edit(int id)
        {
            var aluno = _alunoService.GetAlunoById(id);
            if (aluno == null)
            {
                return RedirectToAction("Index");
            }
            return View(aluno);
        }


        // POST: Home/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Aluno aluno)
        {
            try
            {
                _alunoService.EditarAluno(id, aluno);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        // GET: Home/Delete/5
        public ActionResult Delete(int id)
        {
            var aluno = _alunoService.GetAlunoById(id);

            if (aluno == null)
            {
                TempData["erroExcludeMessage"] = "Erro ao Excluir o usuário";
                return RedirectToAction("Index");
            }

            return View(aluno);

        }
        [HttpPost]
        public ActionResult Delete(int id, Aluno aluno)
        {

            try
            {
                aluno = _alunoService.GetAlunoById(id);
                _alunoService.DeletarAluno(aluno);
            }
            catch
            {
                TempData["erroExcludeMessage"] = "Erro ao Excluir o usuário";
            }

            return RedirectToAction("Index");


        }
        [Route("GerarRelatorio/ListarAlunos")]
        public ActionResult ListarAlunos()
        {
            var pdf = new ViewAsPdf()
            {
                ViewName = "ListaAlunos",
                PageSize = Size.A4,
                IsGrayScale = true,
                PageOrientation = Orientation.Landscape,
                Model = _alunoService.GetAlunos(),
                CustomSwitches =
            $"--footer-left \" Página: [page]/[toPage]\" --footer-font-size \"10\" --footer-spacing 1 --footer-font-name \"Segoe UI\""

            };

            return pdf;
        }



        [HttpPost]
        public string ResponderEnquete(int opcao)
        {
            if (opcao == 1)
                return "Você respondeu SIM para essa enquete";
            else
                return "Você respondeu de qualquer forma!";
        }
    }


}
