namespace Utilities_aspnet.Dashboard {
    public class DashboardDto {
        public int CountUsers { get; set; }
        public int CountProducts { get; set; }
        public int CountAds { get; set; }
        public int CountProjects { get; set; }
        public int CountTutorials { get; set; }
        public int CountEvents { get; set; }
        public int CountCompanies { get; set; }
        public int CountDailyPrices { get; set; }
        public int CountTenders { get; set; }
        public int CountServices { get; set; }
        public int CountMagazine { get; set; }
        public int CountBookmarks { get; set; }
        public int CountComments { get; set; }
        public int CountReports { get; set; }
        public int CountTags { get; set; }
        public int CountBrands { get; set; }
        public int CountCategories { get; set; }
        public int CountSpecialities { get; set; }
    }

    public class FilterDashboardDto {
        public DateTime StardDate { get; set; } = DateTime.Now.AddYears(-1);
        public DateTime EndDate { get; set; } = DateTime.Now;
    }
}