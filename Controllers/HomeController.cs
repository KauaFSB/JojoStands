using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Jojo.Models;

namespace Jojo.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        List<Stand> stands = GetPokemons();
        List<Tipo> tipos = GetTipos();
        ViewData["Tipos"] = tipos;
        return View(stands);
    }

    public IActionResult Details(int id)
    {
        List<Stand> stands = GetStands();
        List<Tipo> tipos = GetTipos();
        DetailsVM details = new() {
            Tipos = tipos,
            Atual = stands.FirstOrDefault(p => p.Numero == id),
            Anterior = stands.OrderByDescending(p => p.Numero).FirstOrDefault(p => p.Numero < id),
            Proximo = stands.OrderBy(p => p.Numero).FirstOrDefault(p => p.Numero > id),
        };
        return View(details);
    }
    private List<Stand> GetStands()
    {
        using (StreamReader leitor = new("Data\\stands.json"))
        {
            string dados = leitor.ReadToEnd();
            return JsonSerializer.Deserialize<List<Stand>>(dados);
        }
    }

     private List<Tipo> GetTipos()
    {
        using (StreamReader leitor = new("Data\\tipos.json"))
        {
            string dados = leitor.ReadToEnd();
            return JsonSerializer.Deserialize<List<Tipo>>(dados);
        }
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
