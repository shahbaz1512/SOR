using DAL;
using Dapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using Npgsql;
using PARTNERSORAPI.Classes;
using Serilog;
using SORAPI.Classes;
using SORAPI.Controllers;
using SORAPI.Interface;
using System;
using System.Data;
using System.Diagnostics.Metrics;
using System.Reflection;
using System.Xml.Linq;
using static System.Collections.Specialized.BitVector32;

namespace SORAPI.DALC
{
    public class DALC : ProcessSyncTransactionInterface, TransactionRecords
    {
        private readonly Commondetail _commondetail;
        public DALC (Commondetail commondetail)
        {
            _commondetail = commondetail;   
        }
        Response _response = new();
        TSPResponse _tspresponse = new();
        ProgrammanagerResponse _pmresponse = new(); 
        CustomerResponse _custresponse = new();
        public async Task<Response> InsertPartnerInformation(Request request)
        {
            string jsonString = string.Empty;
            string notice = string.Empty;
            var conn = new NpgsqlConnection(ConfigDb.Constring);
            try
            {
                jsonString = JsonConvert.SerializeObject(request);
                using (conn)
                {
                    conn.Notice += async (o, e) =>
                    {
                        //Console.WriteLine($"NOTICE: {e.Notice.MessageText}");
                        _response.ResponseCode  = e.Notice.MessageText;
                    };
                    await conn.OpenAsync();
                    using (var cmd = new NpgsqlCommand("CALL partnerregistration(@jsonParam)", conn))
                    {
                        cmd.Parameters.AddWithValue("jsonParam", NpgsqlTypes.NpgsqlDbType.Jsonb, jsonString);
                        await cmd.ExecuteNonQueryAsync();
                        conn.CloseAsync();
                        _response.ResponseCode = string.IsNullOrEmpty(_response.ResponseCode)? await _commondetail.GetResponseCodeAsync(ConstResponseCode.Approved):_response.ResponseCode;
                        _response.ResponseDescription =await _commondetail.GetResponseCodeDescriptionAsync(_response.ResponseCode);
                        request.ACTION = "1";
                    }
                }
            }
            catch (Exception ex)
            {
                conn.CloseAsync();
                Log.Error($"Error: {ex.Message}");
                _response.ResponseCode = await _commondetail.GetResponseCodeAsync(ConstResponseCode.UnableToProcess); 
                _response.ResponseDescription = await _commondetail.GetResponseCodeDescriptionAsync(_response.ResponseCode);
                request.ACTION = "1";
            }
            return _response;
        }

        public async Task<Response> InsertPartnerTopup(Request request)
        {
            string jsonString = string.Empty;
            var conn = new NpgsqlConnection(ConfigDb.Constring);
            try
            {
                jsonString = JsonConvert.SerializeObject(request);
                using (conn)
                {
                    await conn.OpenAsync();
                    using (var cmd = new NpgsqlCommand("CALL InsertUpdatePartnerLiquidity(@jsonParam)", conn))
                    {
                        cmd.Parameters.AddWithValue("jsonParam", NpgsqlTypes.NpgsqlDbType.Jsonb, jsonString);
                         await cmd.ExecuteNonQueryAsync();
                         conn.CloseAsync();
                        _response.ResponseCode = await _commondetail.GetResponseCodeAsync(ConstResponseCode.Approved);
                        _response.ResponseDescription = await _commondetail.GetResponseCodeDescriptionAsync(_response.ResponseCode);
                        request.ACTION = "1";
                    }
                }
            }
            catch (Exception ex)
            {
                conn.CloseAsync();
                Log.Error($"Error: {ex.Message}");
                _response.ResponseCode = await _commondetail.GetResponseCodeAsync(ConstResponseCode.UnableToProcess);
                _response.ResponseDescription = await _commondetail.GetResponseCodeDescriptionAsync(_response.ResponseCode);
                request.ACTION = "1";
            }
            return _response;
        }

        public async Task<Response> BlockPartner(Request request)
        {
            var conn = new NpgsqlConnection(ConfigDb.Constring);
            try
            {
                using (conn)
                {
                    await conn.OpenAsync();
                    using (var cmd = new NpgsqlCommand("CALL Partner_block_unblock(@partner_id, @action)", conn))
                    {
                        cmd.Parameters.Add(new NpgsqlParameter<string>("partner_id", request.PartnerId));
                        cmd.Parameters.Add(new NpgsqlParameter<string>("action", request.TransactionType));
                        await cmd.ExecuteNonQueryAsync();
                        conn.CloseAsync();
                        _response.ResponseCode = await _commondetail.GetResponseCodeAsync(ConstResponseCode.Approved);
                        _response.ResponseDescription = await _commondetail.GetResponseCodeDescriptionAsync(_response.ResponseCode);
                        request.ACTION = "1";
                    }
                }
            }
            catch (Exception ex)
            {
                conn.CloseAsync();
                Log.Error($"Error: {ex.Message}");
                _response.ResponseCode = await _commondetail.GetResponseCodeAsync(ConstResponseCode.UnableToProcess);
                _response.ResponseDescription = await _commondetail.GetResponseCodeDescriptionAsync(_response.ResponseCode);
                request.ACTION = "1";
            }
            return _response;
        }

        public async Task<Response> UnblockPartner(Request request)
        {
            var conn = new NpgsqlConnection(ConfigDb.Constring);
            try
            {
                using (conn)
                {
                    await conn.OpenAsync();
                    using (var cmd = new NpgsqlCommand("CALL Partner_block_unblock(@partner_id, @action)", conn))
                    {
                        cmd.Parameters.Add(new NpgsqlParameter<string>("partner_id", request.PartnerId));
                        cmd.Parameters.Add(new NpgsqlParameter<string>("action", request.TransactionType));
                        await cmd.ExecuteNonQueryAsync();
                        conn.CloseAsync();
                        _response.ResponseCode = await _commondetail.GetResponseCodeAsync(ConstResponseCode.Approved);
                        _response.ResponseDescription = await _commondetail.GetResponseCodeDescriptionAsync(_response.ResponseCode);
                        request.ACTION = "1";
                    }
                }
            }
            catch (Exception ex)
            {
                conn.CloseAsync();
                Log.Error($"Error: {ex.Message}");
                _response.ResponseCode = await _commondetail.GetResponseCodeAsync(ConstResponseCode.UnableToProcess);
                _response.ResponseDescription = await _commondetail.GetResponseCodeDescriptionAsync(_response.ResponseCode);
                request.ACTION = "1";
            }
            return _response;
        }
        public async Task<Response> CheckPartnerLimit(Request request)
        {
            decimal? closingAmount = null;
            var conn = new NpgsqlConnection(ConfigDb.Constring);
            try
            {
                using (conn)
                {
                    await conn.OpenAsync();
                    using (var cmd = new NpgsqlCommand("GetPartnerLimit", conn))
                    {

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new NpgsqlParameter<string>("partner_code", request.PartnerId));
                        cmd.Parameters.Add(new NpgsqlParameter<string>("channel_input", request.TransactionType));
                        var closingAmountParam = new NpgsqlParameter("closing_amount", NpgsqlTypes.NpgsqlDbType.Numeric)
                        {
                            Direction = ParameterDirection.Output
                        };
                        cmd.Parameters.Add(closingAmountParam);
                        await cmd.ExecuteNonQueryAsync();
                        _response.PartnerAvailableLimit = (decimal)closingAmountParam.Value;
                        conn.CloseAsync();
                        _response.ResponseCode = await _commondetail.GetResponseCodeAsync(ConstResponseCode.Approved);
                        _response.ResponseDescription = await _commondetail.GetResponseCodeDescriptionAsync(_response.ResponseCode);
                        request.ACTION = "1";
                    }
                }
            }
            catch (Exception ex)
            {
                conn.CloseAsync();
                Log.Error($"Error: {ex.Message}");
                _response.ResponseCode = await _commondetail.GetResponseCodeAsync(ConstResponseCode.UnableToProcess);
                _response.ResponseDescription = await _commondetail.GetResponseCodeDescriptionAsync(_response.ResponseCode);
                request.ACTION = "1";
            }
            return _response;
        }

        //TSP Transactions

        public async Task<TSPResponse> InsertTSPRegistration(TSPRequest tsprequest)
        {
            string jsonString = string.Empty;
            string notice = string.Empty;
            var conn = new NpgsqlConnection(ConfigDb.Constring);
            try
            {
                jsonString = JsonConvert.SerializeObject(tsprequest);
                using (conn)
                {
                    conn.Notice += async (o, e) =>
                    {
                        //Console.WriteLine($"NOTICE: {e.Notice.MessageText}");
                        _response.ResponseCode = e.Notice.MessageText;
                    };
                    await conn.OpenAsync();
                    using (var cmd = new NpgsqlCommand("CALL partnerregistration(@jsonParam)", conn))
                    {
                        cmd.Parameters.AddWithValue("jsonParam", NpgsqlTypes.NpgsqlDbType.Jsonb, jsonString);
                        await cmd.ExecuteNonQueryAsync();
                        conn.CloseAsync();
                        _response.ResponseCode = string.IsNullOrEmpty(_response.ResponseCode) ? await _commondetail.GetResponseCodeAsync(ConstResponseCode.Approved) : _response.ResponseCode;
                        _response.ResponseDescription = await _commondetail.GetResponseCodeDescriptionAsync(_response.ResponseCode);
                        tsprequest.ACTION = "1";
                    }
                }
            }
            catch (Exception ex)
            {
                conn.CloseAsync();
                Log.Error($"Error: {ex.Message}");
                _response.ResponseCode = await _commondetail.GetResponseCodeAsync(ConstResponseCode.UnableToProcess);
                _response.ResponseDescription = await _commondetail.GetResponseCodeDescriptionAsync(_response.ResponseCode);
                tsprequest.ACTION = "1";
            }
            return _tspresponse;
        }

        //PM Transactions

        public async Task<ProgrammanagerResponse> InsertPMRegistration(ProgrammanagerRequest pmrequest)
        {
            string jsonString = string.Empty;
            string notice = string.Empty;
            var conn = new NpgsqlConnection(ConfigDb.Constring);
            try
            {
                jsonString = JsonConvert.SerializeObject(pmrequest);
                using (conn)
                {
                    conn.Notice += async (o, e) =>
                    {
                        //Console.WriteLine($"NOTICE: {e.Notice.MessageText}");
                        _response.ResponseCode = e.Notice.MessageText;
                    };
                    await conn.OpenAsync();
                    using (var cmd = new NpgsqlCommand("CALL partnerregistration(@jsonParam)", conn))
                    {
                        cmd.Parameters.AddWithValue("jsonParam", NpgsqlTypes.NpgsqlDbType.Jsonb, jsonString);
                        await cmd.ExecuteNonQueryAsync();
                        conn.CloseAsync();
                        _response.ResponseCode = string.IsNullOrEmpty(_response.ResponseCode) ? await _commondetail.GetResponseCodeAsync(ConstResponseCode.Approved) : _response.ResponseCode;
                        _response.ResponseDescription = await _commondetail.GetResponseCodeDescriptionAsync(_response.ResponseCode);
                        pmrequest.ACTION = "1";
                    }
                }
            }
            catch (Exception ex)
            {
                conn.CloseAsync();
                Log.Error($"Error: {ex.Message}");
                _response.ResponseCode = await _commondetail.GetResponseCodeAsync(ConstResponseCode.UnableToProcess);
                _response.ResponseDescription = await _commondetail.GetResponseCodeDescriptionAsync(_response.ResponseCode);
                pmrequest.ACTION = "1";
            }
            return _pmresponse;
        }

        //Customer Transactions

        public async Task<CustomerResponse> InsertCustomerRegistration(CustomerRequest pmrequest)
        {
            string jsonString = string.Empty;
            string notice = string.Empty;
            var conn = new NpgsqlConnection(ConfigDb.Constring);
            try
            {
                jsonString = JsonConvert.SerializeObject(pmrequest);
                using (conn)
                {
                    conn.Notice += async (o, e) =>
                    {
                        //Console.WriteLine($"NOTICE: {e.Notice.MessageText}");
                        _response.ResponseCode = e.Notice.MessageText;
                    };
                    await conn.OpenAsync();
                    using (var cmd = new NpgsqlCommand("CALL partnerregistration(@jsonParam)", conn))
                    {
                        cmd.Parameters.AddWithValue("jsonParam", NpgsqlTypes.NpgsqlDbType.Jsonb, jsonString);
                        await cmd.ExecuteNonQueryAsync();
                        conn.CloseAsync();
                        _response.ResponseCode = string.IsNullOrEmpty(_response.ResponseCode) ? await _commondetail.GetResponseCodeAsync(ConstResponseCode.Approved) : _response.ResponseCode;
                        _response.ResponseDescription = await _commondetail.GetResponseCodeDescriptionAsync(_response.ResponseCode);
                        pmrequest.ACTION = "1";
                    }
                }
            }
            catch (Exception ex)
            {
                conn.CloseAsync();
                Log.Error($"Error: {ex.Message}");
                _response.ResponseCode = await _commondetail.GetResponseCodeAsync(ConstResponseCode.UnableToProcess);
                _response.ResponseDescription = await _commondetail.GetResponseCodeDescriptionAsync(_response.ResponseCode);
                pmrequest.ACTION = "1";
            }
            return _custresponse;
        }

        // for transactions and transaction history

        public async Task<Response> InsertTransactionRecords(Request request , Response response)
        {
            string jsonreq = string.Empty;
            string jsonresp = string.Empty;
            var conn = new NpgsqlConnection(ConfigDb.Constring);
            try
            {
                jsonreq = JsonConvert.SerializeObject(request);
                jsonresp = JsonConvert.SerializeObject(response);
                using (conn)
                {
                    await conn.OpenAsync();
                    using (var cmd = new NpgsqlCommand("CALL insertupdatepartnertransactions(@jsonParam,@jsonParamresp)", conn))
                    {
                        cmd.Parameters.AddWithValue("jsonParam", NpgsqlTypes.NpgsqlDbType.Jsonb, jsonreq);
                        cmd.Parameters.AddWithValue("jsonParamresp", NpgsqlTypes.NpgsqlDbType.Jsonb, jsonresp);
                        await cmd.ExecuteNonQueryAsync();
                        conn.CloseAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                conn.CloseAsync();
                Log.Error($"Error: {ex.Message}");
            }
            return _response;
        }

        // TSP Transaction History
        public async Task<TSPResponse> InsertTSPTransactionRecords(TSPRequest request, TSPResponse response)
        {
            string jsonreq = string.Empty;
            string jsonresp = string.Empty;
            var conn = new NpgsqlConnection(ConfigDb.Constring);
            try
            {
                jsonreq = JsonConvert.SerializeObject(request);
                jsonresp = JsonConvert.SerializeObject(response);
                using (conn)
                {
                    await conn.OpenAsync();
                    using (var cmd = new NpgsqlCommand("CALL insertupdatepartnertransactions(@jsonParam,@jsonParamresp)", conn))
                    {
                        cmd.Parameters.AddWithValue("jsonParam", NpgsqlTypes.NpgsqlDbType.Jsonb, jsonreq);
                        cmd.Parameters.AddWithValue("jsonParamresp", NpgsqlTypes.NpgsqlDbType.Jsonb, jsonresp);
                        await cmd.ExecuteNonQueryAsync();
                        conn.CloseAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                conn.CloseAsync();
                Log.Error($"Error: {ex.Message}");
            }
            return _tspresponse;
        }

        // program manager TRansaction History

        public async Task<ProgrammanagerResponse> InsertPMTransactionRecords(ProgrammanagerRequest request, ProgrammanagerResponse response)
        {
            string jsonreq = string.Empty;
            string jsonresp = string.Empty;
            var conn = new NpgsqlConnection(ConfigDb.Constring);
            try
            {
                jsonreq = JsonConvert.SerializeObject(request);
                jsonresp = JsonConvert.SerializeObject(response);
                using (conn)
                {
                    await conn.OpenAsync();
                    using (var cmd = new NpgsqlCommand("CALL insertupdatepartnertransactions(@jsonParam,@jsonParamresp)", conn))
                    {
                        cmd.Parameters.AddWithValue("jsonParam", NpgsqlTypes.NpgsqlDbType.Jsonb, jsonreq);
                        cmd.Parameters.AddWithValue("jsonParamresp", NpgsqlTypes.NpgsqlDbType.Jsonb, jsonresp);
                        await cmd.ExecuteNonQueryAsync();
                        conn.CloseAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                conn.CloseAsync();
                Log.Error($"Error: {ex.Message}");
            }
            return _pmresponse;
        }

        // customer TRansaction History

        public async Task<CustomerResponse> InsertCustomerTransactionRecords(CustomerRequest request, CustomerResponse response)
        {
            string jsonreq = string.Empty;
            string jsonresp = string.Empty;
            var conn = new NpgsqlConnection(ConfigDb.Constring);
            try
            {
                jsonreq = JsonConvert.SerializeObject(request);
                jsonresp = JsonConvert.SerializeObject(response);
                using (conn)
                {
                    await conn.OpenAsync();
                    using (var cmd = new NpgsqlCommand("CALL insertupdatepartnertransactions(@jsonParam,@jsonParamresp)", conn))
                    {
                        cmd.Parameters.AddWithValue("jsonParam", NpgsqlTypes.NpgsqlDbType.Jsonb, jsonreq);
                        cmd.Parameters.AddWithValue("jsonParamresp", NpgsqlTypes.NpgsqlDbType.Jsonb, jsonresp);
                        await cmd.ExecuteNonQueryAsync();
                        conn.CloseAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                conn.CloseAsync();
                Log.Error($"Error: {ex.Message}");
            }
            return _custresponse;
        }
        public static DataTable SelectResponseCodes()
        {
            DataTable dt = new DataTable();
            string response = string.Empty;
            var con = new NpgsqlConnection(ConfigDb.Constring);
            try
            {
                using (con)
                {
                    con.Open();
                    using (var transaction = con.BeginTransaction())
                    {
                        using (var cmd = new NpgsqlCommand("CALL getresponsecode('my_cursor')", con, transaction))
                        {
                            cmd.ExecuteNonQuery();
                        }
                        using (var cmd = new NpgsqlCommand("FETCH ALL FROM my_cursor", con, transaction))

                        using (var reader = cmd.ExecuteReader())
                        {
                            dt.Load(reader);
                            con.Close();
                        }
                    }
                    return dt;
                }
            }
            catch (Exception ex)
            {
                con.Close();
                return dt;
            }
        }

        public enum enumResponseCode
        {
            Unknown = -1,
            Approved = 0,
        }

    }
}