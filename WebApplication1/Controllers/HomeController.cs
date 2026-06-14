using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Dapper;
using Microsoft.Extensions.Logging;
using WebApplication1.Models;
using System.Data;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        // TODO: Move connection string to appsettings.json for production readiness
        private readonly string _connString = "Server=localhost;Database=seniorth_db;User Id=appuser;Password=appuser;TrustServerCertificate=True;";

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Renders the main factory layout dashboard.
        /// </summary>
        public IActionResult Index()
        {
            using (var db = new SqlConnection(_connString))
            {
                // 1. Fetch plotted resources from SQL Server (Fallback to empty list if query execution yields null)
                var dbRows = db.Query("SELECT * FROM MachineLayouts WHERE IsPlotted = 'Y'").ToList()
                             ?? new List<dynamic>();

                // 💡 Guard Clause: If no mapped configurations exist, return an empty array view model to prevent client-side script breakage
                if (!dbRows.Any())
                {
                    return View(Array.Empty<MachineLayout>());
                }

                // 2. Map dynamic recordsets onto tightly-typed MachineLayout entity matrices safely
                var MockMachineLayouts = dbRows.Select(mac => new MachineLayout
                {
                    MachineId = mac.MachineId?.ToString() ?? "",
                    ResourceLocation = mac.ResourceLocation?.ToString() ?? "",
                    Plant = mac.Plant?.ToString() ?? "",
                    Area = mac.Area?.ToString() ?? "",
                    recordType = mac.recordType?.ToString() ?? "NULL",
                    Status = mac.Status?.ToString() ?? "NULL",
                    RequestDate = mac.RequestDate?.ToString() ?? "-",
                    ColorCode = mac.ColorCode?.ToString() ?? "#D3D3D3",

                    LeftPct = mac.LeftPct,
                    TopPct = mac.TopPct,
                    WidthPct = mac.WidthPct,
                    HeightPct = mac.HeightPct,
                    ViewBoxW = (int)(mac.ViewBoxW ?? 0),
                    ViewBoxH = (int)(mac.ViewBoxH ?? 0),
                    X1 = (int)(mac.X1 ?? 0),
                    Y1 = (int)(mac.Y1 ?? 0),
                    X2 = (int)(mac.X2 ?? 0),
                    Y2 = (int)(mac.Y2 ?? 0),
                    BoxX = (int)(mac.BoxX ?? 0),
                    BoxY = (int)(mac.BoxY ?? 0),
                    BoxW = (int)(mac.BoxW ?? 0),
                    BoxH = (int)(mac.BoxH ?? 0),
                    FontSize = mac.FontSize,
                    FontWeight = mac.FontWeight?.ToString() ?? "normal",
                    IsPlotted = mac.IsPlotted
                }).ToArray();

                return View(MockMachineLayouts);
            }
        }

        /// <summary>
        /// Serves the Spatial Grid Map Management Interface.
        /// </summary>
        [HttpGet]
        [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
        public IActionResult Editor()
        {
            using (var db = new SqlConnection(_connString))
            {
                // 1. Fetch layout configurations from SQL Server (Fallback encapsulation provided)
                var dbRows = db.Query("SELECT * FROM MachineLayouts WHERE IsPlotted = 'Y'").ToList()
                             ?? new List<dynamic>();

                // 💡 Guard Clause: Return empty sequence array if database contains zero plotted nodes
                if (!dbRows.Any())
                {
                    return View(Array.Empty<MachineLayout>());
                }

                // 2. Map rows to typed entities for Initial Page Configuration State
                var MockMachineLayouts = dbRows.Select(mac => new MachineLayout
                {
                    MachineId = mac.MachineId?.ToString() ?? "",
                    ResourceLocation = mac.ResourceLocation?.ToString() ?? "",
                    Plant = mac.Plant?.ToString() ?? "",
                    Area = mac.Area?.ToString() ?? "",
                    recordType = mac.recordType?.ToString() ?? "NULL",
                    Status = mac.Status?.ToString() ?? "NULL",
                    RequestDate = mac.RequestDate?.ToString() ?? "-",
                    ColorCode = mac.ColorCode?.ToString() ?? "#D3D3D3",

                    LeftPct = mac.LeftPct,
                    TopPct = mac.TopPct,
                    WidthPct = mac.WidthPct,
                    HeightPct = mac.HeightPct,
                    ViewBoxW = (int)(mac.ViewBoxW ?? 0),
                    ViewBoxH = (int)(mac.ViewBoxH ?? 0),
                    X1 = (int)(mac.X1 ?? 0),
                    Y1 = (int)(mac.Y1 ?? 0),
                    X2 = (int)(mac.X2 ?? 0),
                    Y2 = (int)(mac.Y2 ?? 0),
                    BoxX = (int)(mac.BoxX ?? 0),
                    BoxY = (int)(mac.BoxY ?? 0),
                    BoxW = (int)(mac.BoxW ?? 0),
                    BoxH = (int)(mac.BoxH ?? 0),
                    FontSize = mac.FontSize,
                    FontWeight = mac.FontWeight?.ToString() ?? "normal",
                    IsPlotted = mac.IsPlotted
                }).ToArray();

                return View(MockMachineLayouts);
            }
        }

        /// <summary>
        /// API Endpoint: Retrieves serialized real-time spatial positioning metrics for a specific factory plant unit.
        /// </summary>
        [HttpGet]
        [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)] // 🔥 Enforce aggressive downstream cache invalidation
        public IActionResult GetLatestMachineLayout(string plantName)
        {
            // Guard against empty payloads
            if (string.IsNullOrWhiteSpace(plantName))
            {
                return Json(Array.Empty<object>());
            }

            using (var db = new SqlConnection(_connString))
            {
                // 1. Fetch localized asset data coordinates via Parameterized Dapper Pipeline
                string sqlQuery = @"SELECT * FROM MachineLayouts 
                                    WHERE IsPlotted = 'Y' 
                                    AND LOWER(TRIM(Plant)) = LOWER(TRIM(@PlantName))";

                var dbRows = db.Query(sqlQuery, new { PlantName = plantName }).ToList()
                             ?? new List<dynamic>();

                // 💡 Guard Clause: Fallback to an empty JSON array string literal if localized set yields zero records
                if (!dbRows.Any())
                {
                    return Json(Array.Empty<object>());
                }

                // 2. Perform object mapping to standard structural data transfer contracts
                var latestLayouts = dbRows.Select(mac => new MachineLayout
                {
                    MachineId = mac.MachineId?.ToString() ?? "",
                    ResourceLocation = mac.ResourceLocation?.ToString() ?? "",
                    Plant = mac.Plant?.ToString() ?? "",
                    Area = mac.Area?.ToString() ?? "",
                    recordType = mac.recordType?.ToString() ?? "NULL",
                    Status = mac.Status?.ToString() ?? "NULL",
                    RequestDate = mac.RequestDate?.ToString() ?? "-",
                    ColorCode = mac.ColorCode?.ToString() ?? "#D3D3D3",

                    LeftPct = mac.LeftPct,
                    TopPct = mac.TopPct,
                    WidthPct = mac.WidthPct,
                    HeightPct = mac.HeightPct,
                    ViewBoxW = (int)(mac.ViewBoxW ?? 0),
                    ViewBoxH = (int)(mac.ViewBoxH ?? 0),
                    X1 = (int)(mac.X1 ?? 0),
                    Y1 = (int)(mac.Y1 ?? 0),
                    X2 = (int)(mac.X2 ?? 0),
                    Y2 = (int)(mac.Y2 ?? 0),
                    BoxX = (int)(mac.BoxX ?? 0),
                    BoxY = (int)(mac.BoxY ?? 0),
                    BoxW = (int)(mac.BoxW ?? 0),
                    BoxH = (int)(mac.BoxH ?? 0),
                    FontSize = mac.FontSize,
                    FontWeight = mac.FontWeight?.ToString() ?? "normal",
                    IsPlotted = mac.IsPlotted
                }).ToArray();

                // 🎯 Dispatch structured JSON response schema to sync UI state changes dynamically
                return Json(latestLayouts);
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}