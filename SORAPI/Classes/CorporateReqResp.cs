namespace SORAPI.Classes
{
    public class CorporateRequest
    {
        // Basic Corporation Information
        public string CorpId { get; set; }
        public string CorpName { get; set; }
        public string Description { get; set; }
        public string TspId { get; set; }
        public string ProgramId { get; set; }
        public string KycType { get; set; }
        public string Address { get; set; }
        public string Pincode { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string OperatingHours { get; set; }
        public string Region { get; set; }
        public string Turnover { get; set; }
        public string CustomerSegment { get; set; }
        public string NationalIdType { get; set; }
        public string NationalIdNumber { get; set; }
        public string MailingType { get; set; }

        // Authorization Information
        public bool AuthorizationFlag { get; set; }
        public string AuthorizedBy { get; set; }

        // Audit Information
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }


        // Account Information
        public string AccountType { get; set; }
        public string AccountNumber { get; set; }
        public string SchemeType { get; set; }
        public string AccountStatus { get; set; }
        public decimal? LastFetchedBalance { get; set; }
        public string OperationMode { get; set; }

        // Contact Information
        public string ContactNumber { get; set; }
        public string EmailId { get; set; }

        // Financial Information
        public decimal? AvailableBalance { get; set; }
        public decimal? LedgerBalance { get; set; }

        // Transaction Information
        public string ProgramType { get; set; }
        public string Channel { get; set; }
        public string TransMode { get; set; }
        public string BinNumber { get; set; }
        public string PaymentMode { get; set; }
        public decimal? OpeningBalance { get; set; }
        public decimal? ClosingBalance { get; set; }
        public decimal? TopUpAmount { get; set; }
        public string RemitterAccountNumber { get; set; }
        public string RemitterName { get; set; }
        public string RemitterBankName { get; set; }
        public string BeneficiaryBankName { get; set; }
        public string BeneficiaryAccountNumber { get; set; }
        public string Attachment { get; set; }
        public string Status { get; set; }
        public string Narration { get; set; }
        public string ReferenceNumber { get; set; }
        public string TransactionType { get; set; }
        public string TransactionId { get; set; }
        public DateTime? DateTime { get; set; }

        // Remarks and Reserved Fields
        public string Remarks { get; set; }
        public string Referencenumber { get; set; }
        public string ReserveFields2 { get; set; }
        public string ReserveFields3 { get; set; }
        public string ReserveFields4 { get; set; }
        public string ReserveFields5 { get; set; }
    }
    public class CorporateResponse
    {
        public string ResponseCode { get; set; }
        public string ResponseDescription { get; set; }
        public string CorpId { get; set; }
        public string CorpName { get; set; }
        public decimal AvailableBalance { get; set; }

    }
}

