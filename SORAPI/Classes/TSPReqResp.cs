namespace SORAPI.Classes
{
    public class TSPReqResp
    {
        // Basic TSP Information
        public string TspId { get; set; }
        public string TspName { get; set; }
        public string TspCategory { get; set; }
        public string TspStatus { get; set; }

        // Service Dates
        public DateTime? ServiceStartDate { get; set; }
        public DateTime? ServiceEndDate { get; set; }

        // Contact Information
        public string ContactPerson { get; set; }
        public string ContactEmail { get; set; }
        public string ContactPhone { get; set; }
        public string Address { get; set; }

        // Financial and Identification Information
        public string ServiceAgreementId { get; set; }
        public string PanEntryMode { get; set; }
        public string BankName { get; set; }
        public string BankBin { get; set; }
        public string CardBin { get; set; }
        public string ReceivingInstitutionBin { get; set; }
        public string ServiceCode { get; set; }
        public string AuthorizationNumber { get; set; }
        public string CardAccepterName { get; set; }
        public string TransactionCurrencyCode { get; set; }
        public string PersonalIdentificationNumber { get; set; }
        public string AdditionalData { get; set; }
        public string CompanyName { get; set; }

        // Geographic Information
        public string Channels { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Pincode { get; set; }
        public string GeoLocation { get; set; }

        // Authorization and Status
        public bool IsAuthorization { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? AuthorisedOn { get; set; }
        public string AuthorisedBy { get; set; }

        // Connection Information
        public string TspIpAddress { get; set; }
        public int? TspPort { get; set; }
        public string TspUrl { get; set; }
        public string TspLocation { get; set; }

        // Transaction Limits
        public decimal? DailyLimit { get; set; }
        public decimal? MonthlyLimit { get; set; }
        public decimal? YearlyLimit { get; set; }
        public decimal? MinTxnAmount { get; set; }
        public decimal? MaxTxnAmount { get; set; }
        public int? DailyCount { get; set; }
        public int? MonthlyCount { get; set; }
        public int? YearlyCount { get; set; }

        // Account Information
        public string AccountNumber { get; set; }
        public string AccountType { get; set; }
        public string SchemeType { get; set; }
        public string AccountStatus { get; set; }
        public DateTime? Expiry { get; set; }

        // Program Information
        public string ProgramId { get; set; }
        public decimal? Limit { get; set; }

        // Remarks and Actions
        public string Remark { get; set; }

        // Modification Details
        public string ModifiedBy { get; set; }
    }
}
