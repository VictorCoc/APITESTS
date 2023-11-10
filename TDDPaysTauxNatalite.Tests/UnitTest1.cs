namespace TDDPaysTauxNatalite.Tests;

using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

public class UnitTest1
{
    /*
     * GIVEN le pays que je demande est "France"
     * WHEN je demande le taux de natalité
     * THEN je récupère un json {1.84}
     */
    
    [Fact]
    public async Task IsGetTauxNataliteByCountryOk()
    {
        await using var _factory = new WebApplicationFactory<Program>();
        var client = _factory.CreateClient();
        var response = await client.GetAsync("TauxNatalite/France");
        
        Assert.Equal("{1.84}", response.Content.ReadAsStringAsync().Result);
    }
    
    /*
     * GIVEN le pays que je demande est "Fransse"
     * WHEN je demande le taux de natalité
     * THEN je récupère un json {"Oupss, le pays n'existe pas"}
     */
    
    [Fact]
    public async Task IsGetTauxNataliteByCountryKo()
    {
        await using var _factory = new WebApplicationFactory<Program>();
        var client = _factory.CreateClient();
        var response = await client.GetAsync("TauxNatalite/Fransse");
        
        Assert.Equal("{\"Oupss, le pays n'existe pas\"}", response.Content.ReadAsStringAsync().Result);
    }
    
    /*
     * GIVEN le pays que je demande est ""
     * WHEN je demande le taux de natalité
     * THEN je récupère un json {"Oupss, le champ est vide"}
     */
    
    [Fact]
    public async Task IsGetTauxNataliteByCountryEmpty()
    {
        await using var _factory = new WebApplicationFactory<Program>();
        var client = _factory.CreateClient();
        var response = await client.GetAsync("TauxNatalite/");
        
        Assert.Equal("{\"Oupss, le champ est vide\"}", response.Content.ReadAsStringAsync().Result);
    }
}