namespace CriticalIncidentsSNow.Models
{
    public class Incident
    {
        public string number { get; set; }
        public string incident_state { get; set; }
        public string active { get; set; }
        public string opened_by { get; set; }
        public string opened_at { get; set; }
        public string location { get; set; }
        public string category { get; set; }
        public string impact { get; set; }
        public string priority { get; set; }
        public string assignment_group { get; set; }
        public string resolved_by { get; set; }
        public string resolved_at { get; set; }
        public string closed_at { get; set; }
    }
}
