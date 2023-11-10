using ChoETL;
using Microsoft.AspNetCore.Mvc;

namespace TDDPaysTauxNatalite.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class TauxNataliteController : ControllerBase
{
    private readonly ILogger<TauxNataliteController> _logger;

    public TauxNataliteController(ILogger<TauxNataliteController> logger)
    {
        _logger = logger;
    }
    
    [HttpGet(Name = "GetTauxNatalite")]
    public string Get()
    {
        return "{\"Oupss, le champ est vide\"}";
    }

    [HttpGet("{id}", Name = "GetTauxNataliteByCountry")]
    public string GetById(string? id)
    {
        using (var reader = new ChoCSVReader(
                   AppDomain.CurrentDomain.BaseDirectory + "../../../tauxNatalite.csv")
                   .WithFirstLineHeader().WithDelimiter(","))
        {
            foreach (var tauxNatalite in reader)
            {
                if (tauxNatalite.Pays == id && tauxNatalite.tauxNatalite != null)
                {
                    var resultat = new TauxNataliteModel
                    {
                        taux = tauxNatalite.tauxNatalite
                    };
                    return "{" + resultat.taux + "}";
                }
            }
        }
        return "{\"Oupss, le pays n'existe pas\"}";
    }
}
