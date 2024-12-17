using SORAPI.DALC;
using SORAPI.Interface;
using System.Data;
using static SORAPI.DALC.DALC;

namespace PARTNERSORAPI.Classes
{
    public class Commondetails : Commondetail
    {
        public  List<ResponseCodeDetails> _responceCodes = null;

        public   async Task<List<ResponseCodeDetails>> GetResponseCodesAsync()
        {
            if (_responceCodes == null)
            {
                try
                {
                    _responceCodes = new List<ResponseCodeDetails>();
                    DataTable DTResponseCodes = await Task.Run(() => DALC.SelectResponseCodes());

                    foreach (DataRow row in DTResponseCodes.Rows)
                    {
                        ResponseCodeDetails _responseCode = new ResponseCodeDetails()
                        {
                            ResponseCode = row[0].ToString(),
                            ErrorMessage = row[1].ToString(),
                            ResponseCodeHOST = row[2].ToString(),
                        };
                        _responceCodes.Add(_responseCode);
                    }
                }
                catch (Exception ex)
                {
                    // Handle exception (log it, etc.)
                }
            }
            return _responceCodes;
        }

        public async Task<string> GetResponseCodeAsync(string EnumResponseCode)
        {
            var RespCode = await GetResponseCodesAsync();

            string responseCode = (from RCode in RespCode.AsParallel()
                                   where RCode.ResponseCodeHOST == EnumResponseCode
                                   select RCode.ResponseCode).FirstOrDefault();

            if (string.IsNullOrEmpty(responseCode))
                responseCode = "91";

            return responseCode;
        }

        public async Task<string> GetResponseCodeDescriptionAsync(string _responseCode)
        {
            string Msg = string.Empty;

            try
            {
                var responseCodes = await GetResponseCodesAsync();
                Msg = (from RCode in responseCodes.AsParallel()
                       where RCode.ResponseCodeHOST == _responseCode
                       select RCode.ErrorMessage).FirstOrDefault();
            }
            catch (Exception ex)
            {
                // Handle exception (log it, etc.)
            }

            if (string.IsNullOrEmpty(Msg))
                Msg = "UNABLE TO PROCESS";

            return Msg;
        }
    }
    public class ResponseCodeDetails
    {
        public string ResponseCode = string.Empty;
        public string ErrorMessage = string.Empty;
        public enumResponseCode ResponseCodeEnum;
        public string ResponseCodeHOST;
    }
    public class ConstResponseCode
    {
        public const string UnableToProcess = "91";
        public const string Approved = "00";
        public const string PartnerAlreadyExists = "11";
        public const string AllMendatoryFieldsRequired = "12";
    }
}
