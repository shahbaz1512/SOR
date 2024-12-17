namespace SORAPI.Classes
{
    public class ProgrammanagerReqResp
    {
        public string ProgramId { get; set; }
        public string ProgramName { get; set; }
        public string TspId { get; set; }
        public string ProgramCategory { get; set; }
        public string Channel { get; set; }

        // Contact Information
        public string ContactPerson { get; set; }
        public string ContactEmail { get; set; }
        public string ContactPhone { get; set; }
        public string ProgramAddress { get; set; }
        public string Address { get; set; }

        // Account Information
        public string AccountNumber { get; set; }
        public string GlAccountNumber { get; set; }
        public string AccountType { get; set; }
        public string SchemeType { get; set; }
        public string AccountStatus { get; set; }
        public DateTime? Expiry { get; set; }

        // Transaction Limits
        public decimal? MaxLimit { get; set; }
        public decimal? DailyLimit { get; set; }
        public decimal? MonthlyLimit { get; set; }
        public decimal? YearlyLimit { get; set; }
        public decimal? MinTxnAmount { get; set; }
        public decimal? MaxTxnAmount { get; set; }
        public int? DailyCount { get; set; }
        public int? MonthlyCount { get; set; }
        public int? YearlyCount { get; set; }
        public decimal? Limit { get; set; }

        // Geolocation and PM Information
        public string PmIpAddress { get; set; }
        public int? PmPort { get; set; }
        public string PmUrl { get; set; }
        public string PmLocation { get; set; }
        public string GeoLocation { get; set; }

        // Authorization and Status
        public bool IsAuthorization { get; set; }
        public string Remarks { get; set; }

        // Audit Information
        public string CreatedBy { get; set; }
        public string AuthorisedBy { get; set; }
        public string ModifiedBy { get; set; }

        public string change { get; set; }

        public string change2 { get; set; }

        public string change3 { get; set; }


    }
}
