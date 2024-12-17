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
    }

    public interface Datavalidator
    {
        public bool DataValidators(enumTransactionType _transtype, Request request);

        Task<string> SSLDecrypt(string encryptedMessage);

        Task<string> SSLEncrypt(string PlainMessage);

    }

    public interface TransactionRecords
    {
        Task<Response> InsertTransactionRecords(Request request ,Response response);   
    }

    public interface Commondetail
    {
        Task<string> GetResponseCodeAsync(string Responsecode);
        Task<string> GetResponseCodeDescriptionAsync(string ResponseDesc); 
    }
}
