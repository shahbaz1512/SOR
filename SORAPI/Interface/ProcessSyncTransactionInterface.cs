using DAL;
using Dapper;
using Npgsql;
using SORAPI.Classes;
using System.Data;

namespace SORAPI.Interface
{
    public interface ProcessSyncTransactionInterface
    {
        Task<Response> InsertPartnerInformation(Request request);
        Task<Response> InsertPartnerTopup(Request request);
        Task<Response> BlockPartner(Request request);
        Task<Response> UnblockPartner(Request request);
        Task<Response> CheckPartnerLimit(Request request);

        //TSP
        Task<TSPResponse> InsertTSPRegistration(TSPRequest tsprequest);

        //PM
        Task<ProgrammanagerResponse> InsertPMRegistration(ProgrammanagerRequest pmrequest);

        //Customer
        Task<CustomerResponse> InsertCustomerRegistration(CustomerRequest custrequest);
    }

    public interface Datavalidator
    {
        public bool DataValidators(enumTransactionType _transtype, Request request);
        public bool TSPDataValidators(enumTransactionType _transtype, TSPRequest request);
        public bool PMDataValidators(enumTransactionType _transtype, ProgrammanagerRequest request);
        public bool CustomerDataValidators(enumTransactionType _transtype, CustomerRequest request);
    }

    public interface TransactionRecords
    {
        //Corporate
        Task<Response> InsertTransactionRecords(Request request ,Response response);

        //TSP
        Task<TSPResponse> InsertTSPTransactionRecords(TSPRequest request, TSPResponse response);

        //PM
        Task<ProgrammanagerResponse> InsertPMTransactionRecords(ProgrammanagerRequest request, ProgrammanagerResponse response);

        //Customer
        Task<CustomerResponse> InsertCustomerTransactionRecords(CustomerRequest request, CustomerResponse response);

    }

    public interface Commondetail
    {
        Task<string> GetResponseCodeAsync(string Responsecode);
        Task<string> GetResponseCodeDescriptionAsync(string ResponseDesc); 
    }

    public interface Crypto
    {
        Task<string> SSLDecrypt(string encryptedMessage);

        Task<string> SSLEncrypt(string PlainMessage);
    }
}
