namespace SORAPI.Classes
{
    public class CustomerRequest
    {
        // Basic Customer Information
        public string CustomerId { get; set; }
        public string TspId { get; set; }
        public string ProgramId { get; set; }
        public string CorpId { get; set; }
        public string Salutation { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public DateTime? Dob { get; set; }
        public string MaritalStatus { get; set; }
        public string MobileNumber { get; set; }
        public string EmailId { get; set; }
        public string OperationMode { get; set; }
        public string CustomerCategory { get; set; }
        public string Occupation { get; set; }
        public string CompanyName { get; set; }
        public string ProfilePicture { get; set; }
        public string KycStatus { get; set; }

        // Address Information
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Pincode { get; set; }

        // ID Proof Information
        public string IdProofType { get; set; }
        public string IdInformation { get; set; }

        // Authorization Information
        public bool IsAuthorization { get; set; }
        public string AuthorizedBy { get; set; }
        public DateTime? AuthorizedOn { get; set; }

        // Audit Information
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }

        // Reserved Fields
        public string ReferenceNumber { get; set; }
        public string ACTION { get; set; }
        public string Reserve3 { get; set; }
        public string Reserve4 { get; set; }

        // Account Information
        public string AccountType { get; set; }
        public string AccountNumber { get; set; }
        public string SchemeType { get; set; }
        public string AccountStatus { get; set; }
        public decimal? LastCbsBalance { get; set; }
        public string Remark { get; set; }

        // Wallet Information
        public string WalletId { get; set; }
        public string CardNumber { get; set; }
        public decimal? WalletBalance { get; set; }
        public decimal? CbsBalance { get; set; }
        public string ProgramCode { get; set; }
        public string PartnerId { get; set; }
        public DateTime? Expiry { get; set; }

        // Transaction Information
        public string TransId { get; set; }
        public string TxnFlag { get; set; }
        public decimal? TxnAmount { get; set; }
        public decimal? OpeningBalance { get; set; }
        public decimal? ClosingBalance { get; set; }
        public string TxnType { get; set; }
        public string ChannelId { get; set; }
        public string Narration { get; set; }
        public string TransactionReferenceNumber { get; set; }

        // Transaction Audit Information
        public string Source { get; set; }
        public bool IsAuth { get; set; }

        // Transaction Type and Status
        public string CommandType { get; set; }
        public string TransMode { get; set; }
        public string TransSource { get; set; }
        public string ProcessingCode { get; set; }
        public decimal? TransactionAmount { get; set; }
        public string SystemTraceAuditNumber { get; set; }
        public DateTime? TransmissionDateTime { get; set; }
        public DateTime? LocalTransactionDateTime { get; set; }
        public string MerchantCategory { get; set; }
        public string AcquirerInstitutionCode { get; set; }
        public string ReceivingInstitutionCode { get; set; }
        public string AuthorizationNumber { get; set; }
        public string ResponseCode { get; set; }
        public string TerminalId { get; set; }
        public string TerminalLocation { get; set; }
        public string GeoTag { get; set; }
        public string CardAccepterName { get; set; }
        public decimal? AdditionalAmounts { get; set; }
        public string TransCurrencyCode { get; set; }
        public string AdditionalIssuerData { get; set; }
        public string OriginalDataElements { get; set; }

        // Related Account and Customer Information
        public string FromAccountNumber { get; set; }
        public string ToAccountNumber { get; set; }
        public string PanNumber { get; set; }
        public string AadharNumber { get; set; }


        // Fraud and Investigation Information
        public string InvestigatorId { get; set; }
        public DateTime? InvestigationStartDate { get; set; }
        public DateTime? InvestigationEndDate { get; set; }
        public string InvestigationOutcome { get; set; }
        public decimal? FraudScore { get; set; }
        public string ResolutionComments { get; set; }
        public string AlertLevel { get; set; }
        public string RelatedTransactions { get; set; }
        public bool NotifyCustomer { get; set; }
    }

    public class CustomerResponse
    {
        public string ResponseCode { get; set; }
        public string ResponseDescription { get; set; }
        public string CorpId { get; set; }
        public string CorpName { get; set; }
        public decimal AvailableBalance { get; set; }

    }

}
