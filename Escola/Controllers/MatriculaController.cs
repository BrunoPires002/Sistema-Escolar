using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Escola.Models;

namespace Escola.Controllers
{
    public class MatriculaController : Controller
    {
        private readonly BancoDeDados _context;

        public MatriculaController(BancoDeDados context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Matriculas.Include(p => p.Aluno).Include(p => p.Curso).ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var matricula = await _context.Matriculas.Include(x=>x.Aluno).Include(x => x.Curso)
                .FirstOrDefaultAsync(m => m.IdMatricula == id);
            if (matricula == null)
            {
                return NotFound();
            }

            var model = new MatriculaModel
            {
                Id = matricula.IdMatricula,
                IdCurso = matricula.Curso.IdCurso,
                IdAluno = matricula.Aluno.IdAluno
            };

            return View(matricula);
        }

        public IActionResult Create()
        {
            ViewBag.Alunos = new SelectList(_context.Alunos, "IdAluno", "NomeAluno");
            ViewBag.Cursos = new SelectList(_context.Cursos, "IdCurso", "NomeCurso");
            return View(new MatriculaModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MatriculaModel matricula)
        {
            if (ModelState.IsValid)
            {
                var entity = new Matricula
                {
                    IdMatricula = matricula.Id,
                    Aluno = await _context.Alunos.FindAsync(matricula.IdAluno),
                    Curso = await _context.Cursos.FindAsync(matricula.IdCurso)

                };

                _context.Matriculas.Add(entity);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(matricula);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var matricula = await _context.Matriculas.Include(x => x.Aluno).Include(x => x.Curso).FirstOrDefaultAsync(m => m.IdMatricula == id);
            if (matricula == null)
            {
                return NotFound();
            }

            var model = new MatriculaModel
            {
                Id = matricula.IdMatricula,
                IdCurso = matricula.Curso.IdCurso,
                IdAluno = matricula.Aluno.IdAluno
            };

            return View(matricula);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdMatricula")] Matricula matricula)
        {
            if (id != matricula.IdMatricula)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(matricula);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MatriculaExists(matricula.IdMatricula))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(matricula);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var matricula = await _context.Matriculas
                .FirstOrDefaultAsync(m => m.IdMatricula == id);
            if (matricula == null)
            {
                return NotFound();
            }

            await _context.Matriculas.Include(p => p.Aluno).Include(p => p.Curso).ToListAsync();
            return View(matricula);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var matricula = await _context.Matriculas.FindAsync(id);
            _context.Matriculas.Remove(matricula);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MatriculaExists(int id)
        {
            return _context.Matriculas.Any(e => e.IdMatricula == id);
        }
    }
}
