using CriticalIncidentsSNow.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Data.SqlClient;

namespace CriticalIncidentsSNow.Controllers
{
    public class HomeController : Controller
    {
        SqlCommand com = new SqlCommand();
        SqlDataReader dr;
        SqlConnection con = new SqlConnection();
        List<Incident> Incidents = new List<Incident>();
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            con.ConnectionString = CriticalIncidentsSNow.Properties.Resources.ConnectionString;
        }

        public IActionResult data()
        {
            FetchData();
            return View(Incidents);
        }

        private void FetchData()
        {
            if(Incidents.Count > 0)
            {
                Incidents.Clear();
            }
            try
            {
                con.Open();
                com.Connection = con;
                com.CommandText = "SELECT * from incidents.dbo.Incident where priority='1 - Critical' and active='TRUE' and incident_state in ('Active', 'New') ORDER BY opened_at ASC";
                dr = com.ExecuteReader();
                while(dr.Read())
                {
                    Incidents.Add(new Incident()
                    {
                        number = dr["number"].ToString()
                        ,
                        incident_state = dr["incident_state"].ToString()
                        ,
                        active = dr["active"].ToString()
                        ,
                        opened_by = dr["opened_by"].ToString()
                        ,
                        opened_at = dr["opened_at"].ToString()
                        ,
                        location = dr["location"].ToString()
                        ,
                        category = dr["category"].ToString()
                        ,
                        impact = dr["impact"].ToString()
                        ,
                        priority = dr["priority"].ToString()
                        ,
                        assignment_group = dr["assignment_group"].ToString()
                        ,
                        resolved_by = dr["resolved_by"].ToString()
                        ,
                        resolved_at = dr["resolved_at"].ToString()
                        ,
                        closed_at = dr["closed_At"].ToString()
                    });
                }
                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IActionResult Info()
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