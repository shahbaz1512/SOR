namespace SORAPI.Classes
{
    //public class Request
    //{
    //    public int Id { get; set; }
    //    public string PartnerId { get; set; }
    //    public string Salutation { get; set; }
    //    public string FirstName { get; set; }
    //    public string MiddleName { get; set; }
    //    public string LastName { get; set; }
    //    public string Gender { get; set; }
    //    public DateOnly Dob { get; set; }
    //    public string MaritalStatus { get; set; }
    //    public string MobileNumber { get; set; }
    //    public string PanNumber { get; set; }
    //    public string AdhaarNumber { get; set; }
    //    public string EmailId { get; set; }
    //    public string OperationMode { get; set; }
    //    public string CustomerCategory { get; set; }
    //    public string Occupation { get; set; }
    //    public string CompanyName { get; set; }
    //    public string ProfilePicture { get; set; }
    //    public string KycStatus { get; set; }
    //    public string AccountType { get; set; }
    //    public string ChannelCode { get; set; }
    //    public string ProgramId { get; set; }
    //    public string partner { get; set; }
    //    public string Address1 { get; set; }
    //    public string Address2 { get; set; }
    //    public string Address3 { get; set; }
    //    public string Country { get; set; }
    //    public string State { get; set; }
    //    public string City { get; set; }
    //    public string PinCode { get; set; }
    //    public string IdProofType { get; set; }
    //    public string IdInformation { get; set; }
    //    public DateOnly IssueDate { get; set; }
    //    public DateOnly ExpiryDate { get; set; }
    //    public string AccessType { get; set; }
    //    public string Remarks { get; set; }
    //    public string IsAuthorization { get; set; }
    //    public string AuthorizedBy { get; set; }
    //    public string AuthorizedOn { get; set; }
    //    public string MailingType { get; set; }
    //    public string Reason { get; set; }
    //    public string Createdby { get; set; }
    //    public string Modifiedby { get; set; }
    //    public int Isremoved { get; set; }
    //    public string TopupAmount { get; set; }
    //    public string DailyLimit { get; set; }
    //    public string MonthlyLimit { get; set; }
    //    public string YearlyLimit { get; set; }
    //    public string ReserveFeild5 { get; set; }
    //    public string ReserveFeild6 { get; set; }
    //    public string ReserveFeild7 { get; set; }
    //    public string ReserveFeild8 { get; set; }
    //    public string ReserveFeild9 { get; set; }
    //    public string ReserveFeild10 { get; set; }
    //}

    public class Response
    {
        public string ResponseCode { get; set; }
        public string ResponseDescription { get; set; }
        public string PartnerId { get; set; }
        public string PartnerDetails { get; set; }
        public decimal PartnerAvailableLimit { get; set; }
        public string PartnerTopupDetails { get; set; }
    }

    public class Request
    {
        public int Id { get; set; }
        public string PartnerId { get; set; }
        public string Partnername { get; set; }
        public string Description { get; set; }
        public string Accounttype { get; set; }
        public string Schemetype { get; set; }
        public string Accountstatus { get; set; }
        public string CBSbalance { get; set; }
        public string Accountnumber { get; set; }
        public string Contactnumber { get; set; }
        public string Contactname { get; set; }
        public string Emailid { get; set; }
        public string Channels { get; set; }
        public string Address { get; set; }
        public string Pincode { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string OperatingHours { get; set; }
        public string Region { get; set; }
        public string HasSubAggregators { get; set; }
        public string Programs { get; set; }
        public string Programtype { get; set; }
        public string Transactionmode { get; set; }
        public string TransactionType { get; set; }
        public string Operationmode { get; set; }
        public string Bin { get; set; }
        public string Kyctype { get; set; }
        public double Turnover { get; set; }
        public double AvailableBalance { get; set; }
        public double LedgerBalance { get; set; }
        public string Referencenumber { get; set; }
        public string paymentmode { get; set; }
        public DateTime LocaltransactionDateTime { get; set; }
        public double TopupAmount { get; set; }
        public double TransactionAmount { get; set; }
        public string BeneficaryName { get; set; }  
        public string BeneficaryAccountNumber { get; set; }
        public string BeneficaryBankName { get; set; }
        public string RemitterName { get; set; }
        public string RemitterAccountNumber { get; set; }
        public string RemitterBankName { get; set; }
        public string TransactionId { get; set; }  
        public string TransParticular {  get; set; }
        public string CustomerSegment { get; set; }
        public string NationalIdType { get; set; }
        public string NationalIdNumber { get; set; }
        public string MailingType { get; set; }
        public bool AuthorizationFlag { get; set; }
        public string AuthorizedBy { get; set; }
        public DateTime AuthorizedOn { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public string Remarks { get; set; }
        public string Narration { get; set; }
        public string ACTION { get; set; }
        public string Reserve2 { get; set; }
        public string Reserve3 { get; set; }
        public string Reserve4 { get; set; }
        public bool IsRemoved { get; set; }
    }
    public enum enumTransactionType
    {
        ParttnerOnboard = 01,
        PartnerAuthorization = 02,
        CheckPartnerLimit = 03,
        PartnerBlock = 04,
        PartnerUnblock = 05,
        PartnerValidation = 06,
        GetPartnerDetails = 07,
        UpdatePartnerDetails = 08,
        PartnerTopUp = 09
    }

}
