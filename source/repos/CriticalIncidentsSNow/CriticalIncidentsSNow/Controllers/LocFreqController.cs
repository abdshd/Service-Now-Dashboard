using CriticalIncidentsSNow.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace CriticalIncidentsSNow.Controllers
{
    public class LocFreqController : Controller
    {
        SqlCommand com = new SqlCommand();
        SqlDataReader dr;
        SqlConnection con = new SqlConnection();
        List<object> Locs = new List<object>();
        private readonly ILogger<LocFreqController> _logger;

        public LocFreqController(ILogger<LocFreqController> logger)
        {
            _logger = logger;
            con.ConnectionString = CriticalIncidentsSNow.Properties.Resources.ConnectionString;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ShowLocFreq()
        {
            return View();
        }

        public List<object> FetchData()
        {
            if (Locs.Count > 0)
            {
                Locs.Clear();
            }
            try
            {
                //Critical Locations
                con.Open();
                com.Connection = con;
                com.CommandText = "SELECT TOP (10) REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(location, 'Location 96', 'Brampton'), 'Location 18', 'Markham'), 'Location 93', 'Ottowa'), 'Location 179', 'Mississuaga'), 'Location 51', 'Winnipeg'), 'Location 108', 'Edmonton'), 'Location 41', 'Guelph'), 'Location 161', 'Calgary'), 'Location 143', 'Montreal'), 'Location 204', 'Toronto') as location, count(CASE WHEN priority = '1 - critical' THEN 1 ELSE NULL END) as frequency from incidents.dbo.Incident2 group by location order by count(CASE WHEN priority = '1 - critical' THEN 1 ELSE NULL END) desc";
                dr = com.ExecuteReader();

                List<object> locationsCritical = new List<object>();
                List<object> frequenciesCritical = new List<object>();
                while (dr.Read())
                {
                    var location = dr["location"].ToString();
                    locationsCritical.Add(location);
                    var frequency = dr["frequency"].ToString();
                    frequenciesCritical.Add(frequency);
                }

                Locs.Add(locationsCritical);
                Locs.Add(frequenciesCritical);

                con.Close();

                
                //High Locations
                con.Open();
                com.Connection = con;
                com.CommandText = "SELECT TOP (10) REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(location, 'Location 41', 'Guelph'), 'Location 56', 'Kitchener'), 'Location 55', 'Halifax'), 'Location 128', 'Niagara Falls'), 'Location 51', 'Winnipeg'), 'Location 108', 'Edmonton'), 'Location 93', 'Ottowa'), 'Location 161', 'Calgary'), 'Location 143', 'Montreal'), 'Location 204', 'Toronto') as location, count(CASE WHEN priority = '2 - high' THEN 1 ELSE NULL END) as frequency from incidents.dbo.Incident2 group by location order by count(CASE WHEN priority = '2 - high' THEN 1 ELSE NULL END) desc";
                dr = com.ExecuteReader();

                List<object> locationsHigh = new List<object>();
                List<object> frequenciesHigh = new List<object>();
                while (dr.Read())
                {
                    var location = dr["location"].ToString();
                    locationsHigh.Add(location);
                    var frequency = dr["frequency"].ToString();
                    frequenciesHigh.Add(frequency);
                }

                Locs.Add(locationsHigh);
                Locs.Add(frequenciesHigh);

                con.Close();

                //Moderate Locations
                con.Open();
                com.Connection = con;
                com.CommandText = "SELECT TOP (10) REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(location, 'Location 43', 'Hamilton'), 'Location 188', 'Vaughan'), 'Location 111', 'Kingston'), 'Location 179', 'Mississuaga'), 'Location 51', 'Winnipeg'), 'Location 108', 'Edmonton'), 'Location 93', 'Ottowa'), 'Location 161', 'Calgary'), 'Location 143', 'Montreal'), 'Location 204', 'Toronto') as location, count(CASE WHEN priority = '3 - moderate' THEN 1 ELSE NULL END) as frequency from incidents.dbo.Incident2 group by location order by count(CASE WHEN priority = '3 - moderate' THEN 1 ELSE NULL END) desc";
                dr = com.ExecuteReader();

                List<object> locationsModerate = new List<object>();
                List<object> frequenciesModerate = new List<object>();
                while (dr.Read())
                {
                    var location = dr["location"].ToString();
                    locationsModerate.Add(location);
                    var frequency = dr["frequency"].ToString();
                    frequenciesModerate.Add(frequency);
                }

                Locs.Add(locationsModerate);
                Locs.Add(frequenciesModerate);

                con.Close();

                //Low Locations
                con.Open();
                com.Connection = con;
                com.CommandText = "SELECT TOP (10) REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(location, 'Location 135', 'Laval'), 'Location 96', 'Brampton'), 'Location 188', 'Vaughan'), 'Location 111', 'Kingston'), 'Location 51', 'Winnipeg'), 'Location 108', 'Edmonton'), 'Location 93', 'Ottowa'), 'Location 161', 'Calgary'), 'Location 143', 'Montreal'), 'Location 204', 'Toronto') as location, count(CASE WHEN priority = '4 - low' THEN 1 ELSE NULL END) as frequency from incidents.dbo.Incident2 group by location order by count(CASE WHEN priority = '4 - low' THEN 1 ELSE NULL END) desc";
                dr = com.ExecuteReader();

                List<object> locationsLow = new List<object>();
                List<object> frequenciesLow = new List<object>();
                while (dr.Read())
                {
                    var location = dr["location"].ToString();
                    locationsLow.Add(location);
                    var frequency = dr["frequency"].ToString();
                    frequenciesLow.Add(frequency);
                }

                Locs.Add(locationsLow);
                Locs.Add(frequenciesLow);

                con.Close();

                //Critical States
                con.Open();
                com.Connection = con;
                com.CommandText = "SELECT incident_state, count(CASE WHEN priority = '1 - critical' THEN 1 ELSE NULL END) as frequency from incidents.dbo.Incident2 group by incident_state order by count(CASE WHEN priority = '1 - critical' THEN 1 ELSE NULL END) desc";
                dr = com.ExecuteReader();

                List<object> statesCritical = new List<object>();
                List<object> frequenciesStateCritical = new List<object>();
                while (dr.Read())
                {
                    var state = dr["incident_state"].ToString();
                    if (state=="-100")
                    {
                        continue;
                    }
                    statesCritical.Add(state);
                    var frequency = dr["frequency"].ToString();
                    frequenciesStateCritical.Add(frequency);

                }

                Locs.Add(statesCritical);
                Locs.Add(frequenciesStateCritical);

                con.Close();

                //High States
                con.Open();
                com.Connection = con;
                com.CommandText = "SELECT incident_state, count(CASE WHEN priority = '2 - high' THEN 1 ELSE NULL END) as frequency from incidents.dbo.Incident2 group by incident_state order by count(CASE WHEN priority = '2 - high' THEN 1 ELSE NULL END) desc";
                dr = com.ExecuteReader();

                List<object> statesHigh = new List<object>();
                List<object> frequenciesStateHigh = new List<object>();
                while (dr.Read())
                {
                    var state = dr["incident_state"].ToString();
                    if (state == "-100")
                    {
                        continue;
                    }
                    statesHigh.Add(state);
                    var frequency = dr["frequency"].ToString();
                    frequenciesStateHigh.Add(frequency);

                }

                Locs.Add(statesHigh);
                Locs.Add(frequenciesStateHigh);

                con.Close();

                //Moderate States
                con.Open();
                com.Connection = con;
                com.CommandText = "SELECT incident_state, count(CASE WHEN priority = '3 - moderate' THEN 1 ELSE NULL END) as frequency from incidents.dbo.Incident2 group by incident_state order by count(CASE WHEN priority = '3 - moderate' THEN 1 ELSE NULL END) desc";
                dr = com.ExecuteReader();

                List<object> statesModerate = new List<object>();
                List<object> frequenciesStateModerate = new List<object>();
                while (dr.Read())
                {
                    var state = dr["incident_state"].ToString();
                    if (state == "-100")
                    {
                        continue;
                    }
                    statesModerate.Add(state);
                    var frequency = dr["frequency"].ToString();
                    frequenciesStateModerate.Add(frequency);

                }

                Locs.Add(statesModerate);
                Locs.Add(frequenciesStateModerate);

                con.Close();

                //Low States
                con.Open();
                com.Connection = con;
                com.CommandText = "SELECT incident_state, count(CASE WHEN priority = '4 - low' THEN 1 ELSE NULL END) as frequency from incidents.dbo.Incident2 group by incident_state order by count(CASE WHEN priority = '4 - low' THEN 1 ELSE NULL END) desc";
                dr = com.ExecuteReader();

                List<object> statesLow = new List<object>();
                List<object> frequenciesStateLow = new List<object>();
                while (dr.Read())
                {
                    var state = dr["incident_state"].ToString();
                    if (state == "-100")
                    {
                        continue;
                    }
                    statesLow.Add(state);
                    var frequency = dr["frequency"].ToString();
                    frequenciesStateLow.Add(frequency);

                }

                Locs.Add(statesLow);
                Locs.Add(frequenciesStateLow);

                con.Close();

                // qCritical MTTR
                con.Open();
                com.Connection = con;
                com.CommandText = "SELECT TOP(10) REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(assignment_group, 'Group 3', 'Windows Support'), 'Windows Support1', 'Catalog Request'), 'Group 21', 'ATF Testgroup'), 'Group 57', 'Capacty MGMT'), 'Group 43', 'CAB Approval'), 'Group 62', 'Applications'), 'Group 51', 'Network'), 'Group 61', 'Hardware'), 'Group 66', 'Database'), 'Windows Support7', 'Service Desk') as assignment_group, COALESCE(AVG(CASE WHEN priority = '1 - critical' THEN DATEDIFF(HOUR, CONVERT(DATETIME, opened_at, 103), CONVERT(DATETIME, resolved_at, 103)) END), 0) as mttr "
                    + "FROM incidents.dbo.Incident2 "
                    +"GROUP BY assignment_group "
                    +"ORDER BY COALESCE(AVG(CASE WHEN priority = '1 - critical' THEN DATEDIFF(HOUR, CONVERT(DATETIME, opened_at, 103), CONVERT(DATETIME, resolved_at, 103)) END), 0) DESC";
                dr = com.ExecuteReader();

                List<object> groupCritical = new List<object>();
                List<object> mttrGroupCritical = new List<object>();
                while (dr.Read())
                {
                    var group = dr["assignment_group"].ToString();
                    if (group == "?")
                    {
                        continue;
                    }
                    groupCritical.Add(group);
                    var mttr = dr["mttr"].ToString();
                    mttrGroupCritical.Add(mttr);

                }

                Locs.Add(groupCritical);
                Locs.Add(mttrGroupCritical);

                con.Close();

                //High MTTR
                con.Open();
                com.Connection = con;
                com.CommandText = "SELECT TOP(10) REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(assignment_group, 'Group 58', 'Windows Support'), 'Group 31', 'Catalog Request'), 'Group 23', 'ATF Testgroup'), 'Group 75', 'Capacty MGMT'), 'Group 14', 'CAB Approval'), 'Group 35', 'Applications'), 'Group 33', 'Network'), 'Group 6', 'Hardware'), 'Group 9', 'Database'), 'Group 3', 'Service Desk') as assignment_group, COALESCE(AVG(CASE WHEN priority = '2 - high' THEN DATEDIFF(HOUR, CONVERT(DATETIME, opened_at, 103), CONVERT(DATETIME, resolved_at, 103)) END), 0) as mttr "
                    + "FROM incidents.dbo.Incident2 "
                    + "GROUP BY assignment_group "
                    + "ORDER BY COALESCE(AVG(CASE WHEN priority = '2 - high' THEN DATEDIFF(HOUR, CONVERT(DATETIME, opened_at, 103), CONVERT(DATETIME, resolved_at, 103)) END), 0) DESC";
                dr = com.ExecuteReader();

                List<object> groupHigh = new List<object>();
                List<object> mttrGroupHigh = new List<object>();
                while (dr.Read())
                {
                    var group = dr["assignment_group"].ToString();
                    if (group == "?")
                    {
                        continue;
                    }
                    groupHigh.Add(group);
                    var mttr = dr["mttr"].ToString();
                    mttrGroupHigh.Add(mttr);

                }

                Locs.Add(groupHigh);
                Locs.Add(mttrGroupHigh);

                con.Close();

                //Moderate MTTR
                con.Open();
                com.Connection = con;
                com.CommandText = "SELECT TOP(10) REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(assignment_group, 'Group 63', 'Windows Support'), 'Group 31', 'Catalog Request'), 'Group 15', 'ATF Testgroup'), 'Group 35', 'Capacty MGMT'), 'Group 9', 'CAB Approval'), 'Group 4', 'Applications'), 'Group 2', 'Network'), 'Group 3', 'Hardware'), 'Group 67', 'Database'), 'Group 61', 'Service Desk') as assignment_group, COALESCE(AVG(CASE WHEN priority = '3 - moderate' THEN DATEDIFF(HOUR, CONVERT(DATETIME, opened_at, 103), CONVERT(DATETIME, resolved_at, 103)) END), 0) as mttr "
                    + "FROM incidents.dbo.Incident2 "
                    + "GROUP BY assignment_group "
                    + "ORDER BY COALESCE(AVG(CASE WHEN priority = '3 - moderate' THEN DATEDIFF(HOUR, CONVERT(DATETIME, opened_at, 103), CONVERT(DATETIME, resolved_at, 103)) END), 0) DESC";
                dr = com.ExecuteReader();

                List<object> groupModerate = new List<object>();
                List<object> mttrGroupModerate = new List<object>();
                while (dr.Read())
                {
                    var group = dr["assignment_group"].ToString();
                    if (group == "?")
                    {
                        continue;
                    }
                    groupModerate.Add(group);
                    var mttr = dr["mttr"].ToString();
                    mttrGroupModerate.Add(mttr);

                }

                Locs.Add(groupModerate);
                Locs.Add(mttrGroupModerate);

                con.Close();

                //Low MTTR
                con.Open();
                com.Connection = con;
                com.CommandText = "SELECT TOP(10) REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(assignment_group, 'Group 75', 'Windows Support'), 'Group 31', 'Catalog Request'), 'Group 14', 'ATF Testgroup'), 'Group 10', 'Capacty MGMT'), 'Group 67', 'CAB Approval'), 'Group 35', 'Applications'), 'Group 9', 'Network'), 'Group 33', 'Hardware'), 'Group 66', 'Database'), 'Group 37', 'Service Desk') as assignment_group, COALESCE(AVG(CASE WHEN priority = '4 - low' THEN DATEDIFF(HOUR, CONVERT(DATETIME, opened_at, 103), CONVERT(DATETIME, resolved_at, 103)) END), 0) as mttr "
                    + "FROM incidents.dbo.Incident2 "
                    + "GROUP BY assignment_group "
                    + "ORDER BY COALESCE(AVG(CASE WHEN priority = '4 - low' THEN DATEDIFF(HOUR, CONVERT(DATETIME, opened_at, 103), CONVERT(DATETIME, resolved_at, 103)) END), 0) DESC";
                dr = com.ExecuteReader();

                List<object> groupLow = new List<object>();
                List<object> mttrGroupLow = new List<object>();
                while (dr.Read())
                {
                    var group = dr["assignment_group"].ToString();
                    if (group == "?")
                    {
                        continue;
                    }
                    groupLow.Add(group);
                    var mttr = dr["mttr"].ToString();
                    mttrGroupLow.Add(mttr);

                }

                Locs.Add(groupLow);
                Locs.Add(mttrGroupLow);

                con.Close();

                return Locs;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
