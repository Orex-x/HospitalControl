using System.Diagnostics;
using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Mvc;
using HospitalControl.Models;
using Microsoft.EntityFrameworkCore;

namespace HospitalControl.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly AppDbContext _db;
    
    private readonly string _set = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? "set " : "export ";

    public HomeController(ILogger<HomeController> logger, AppDbContext db)
    {
        _logger = logger;
        _db = db;
    }

    public IActionResult Index() => View();
    public IActionResult Privacy() => View();
    public IActionResult DataDiagrams() => View(GetDataForCharts());
    public IActionResult EasyData() => View();
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });

    
    public DataChartViewModel GetDataForCharts()
    {
        var model = new DataChartViewModel();
        
            var labels = _db.MedicalServices.Select(x => x.Title).ToList();
            var prices = _db.MedicalServices.Select(x => x.Price).ToList();

            /*model.Labels = labels;
            model.Data = prices;*/

            model.Labels = new List<string>() { "фыв", "фыв", "v" };
            model.Data = new List<int>() { 50000, 100000, 20000 };

       
        return model; 
    }
    
    public async Task ExportData() => await _db.Database.ExecuteSqlRawAsync("call BackupCSV();");
    
    public ViewResult Backup()
     {
         var list = new List<string>();
         const string dirName = @"C:\tmp";
         if (Directory.Exists(dirName))
         {
             var files = Directory.GetFiles(dirName);
             list.AddRange(files.Select(Path.GetFileName)!);
         }
         else
             Directory.CreateDirectory(@"C:\tmp");
         
         return View(list);
    }

    public async Task Dump()
    {
        var date = DateTime.Now;
        var fileName = $"{date.Year}{date.Month}{date.Day}{date.Hour}{date.Minute}";
        await PSqlDump(
            @"C:\Program Files\PostgreSQL\14\bin\", 
            "333", 
            "postgres", 
            "Hospital", 
            $"/tmp/{fileName}");
        RedirectToAction("Backup", "Home");
    } 
    
    public async Task Restore(string name)
    {
        await PSqlRestore(
            @"C:\Program Files\PostgreSQL\14\bin\", 
            "333", 
            "postgres", 
            "Hospital", 
            $"/tmp/{name}");
        RedirectToAction("Backup", "Home");
    }

    private async Task PSqlDump(string pathToExecutableFile, string password, string login, string database, string outputFile)
    {
        string[] commands =
        {
            $"cd {pathToExecutableFile}", 
            $"{_set} PGPASSWORD={password}", 
            $"pg_dump.exe -U {login} {database} > {outputFile}.sql"
        };
        await RunCommands(commands);
    }
    
    private async Task PSqlRestore(string pathToExecutableFile, string password, string login, string database, string inputFile)
    {
        string[] commands =
        {
            $"cd {pathToExecutableFile}", 
            $"{_set} PGPASSWORD={password}", 
            $"psql -U {login} -d {database} -c \"select pg_terminate_backend(pid) from pg_stat_activity where datname = '{database}'",
            $"dropdb -U {login} {database}",
            $"createdb -U {login} {database}",
            $"psql -U {login} -d {database} -f {inputFile}",
        };
        await RunCommands(commands);
    }

    private static async Task RunCommands(string[] commands)
    {
        var process = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                RedirectStandardInput = true,
                UseShellExecute = false

            }
        };
        process.Start();
        await using var pWriter = process.StandardInput;
        if (pWriter.BaseStream.CanWrite)
        {
            foreach (var line in commands)
                await pWriter.WriteLineAsync(line);
        }
    }
}