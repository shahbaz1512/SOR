using DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PARTNERSORAPI.Classes;
using Serilog;
using SORAPI.Classes;
using SORAPI.Interface;
using System.Net.Http;

namespace SORAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Corporate : ControllerBase
    {
        // private readonly IConfiguration _configuration;
        private readonly ProcessSyncTransactionInterface _ProcessAsyncTransactionInterface;
        private readonly Datavalidator _datavalidator;
        private readonly TransactionRecords _transactionRecords;
        private readonly Commondetail _commondetail;
        private readonly Crypto _crypto;

        //  interface is injected dependency in controller
        public Corporate(ProcessSyncTransactionInterface TransactionInterfaceInterface, Datavalidator datavalidator , TransactionRecords transactionRecords , Commondetail commondetail , Crypto crypto)  //////Constructor class  with dependncy injection  
        {
            _ProcessAsyncTransactionInterface = TransactionInterfaceInterface ?? throw new ArgumentNullException(nameof(TransactionInterfaceInterface));
            _datavalidator = datavalidator ?? throw new ArgumentException(nameof(TransactionInterfaceInterface));
            _transactionRecords = transactionRecords ?? throw new ArgumentException(nameof(transactionRecords));
            _commondetail = commondetail ?? throw new ArgumentException(nameof(Commondetail));
            _crypto = crypto ?? throw new ArgumentException(nameof(Crypto));
        }
        Request _request = new Request();
        Response _response = new Response();
        [HttpPost("partneronbaoard")]
        public async Task<IActionResult> partnerOnbaoard()
        {
            try
            {
                var requestBody = await new StreamReader(Request.Body).ReadToEndAsync();
                if (string.IsNullOrEmpty(requestBody.Trim()))
                {
                    Log.Information("Received an empty request body.");
                    return BadRequest("Invalid request body.");
                }
                requestBody = await _crypto.SSLEncrypt(requestBody);
                _request = JsonConvert.DeserializeObject<Request>(requestBody);
                Log.Information("partnerOnbaoard request received:  " + _request.Referencenumber);
                _request.ACTION = "0";
                await _transactionRecords.InsertTransactionRecords(_request,_response);
                if (!_datavalidator.DataValidators(enumTransactionType.ParttnerOnboard, _request))
                {
                    _response.ResponseCode = await _commondetail.GetResponseCodeAsync(ConstResponseCode.AllMendatoryFieldsRequired);
                    _response.ResponseDescription =  await _commondetail.GetResponseCodeDescriptionAsync(_response.ResponseCode);
                    return StatusCode(StatusCodes.Status200OK, _response);
                }
                _response = await _ProcessAsyncTransactionInterface.InsertPartnerInformation(_request);
                _transactionRecords.InsertTransactionRecords(_request,_response);
                Log.Information("partnerOnbaoard response sent :  " + _request.Referencenumber);
                return StatusCode(StatusCodes.Status200OK, _response);
            }
            catch (Exception ex)
            {
                string excep = "An error occurred during processing." + ex.Message + " " + ex.Source + " " + ex.StackTrace;
                Log.Error(ex, excep);
                _transactionRecords.InsertTransactionRecords(_request, _response);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred during processing.");
            }
        }

        [HttpPost("PartnerTopUp")]
        public async Task<IActionResult> PartnerTopUp()
        {
            try
            {
                var requestBody = await new StreamReader(Request.Body).ReadToEndAsync();
                if (string.IsNullOrEmpty(requestBody.Trim()))
                {
                    Log.Information("Received an empty request body.");
                    return BadRequest("Invalid request body.");
                }
                requestBody = await _crypto.SSLEncrypt(requestBody);
                _request = JsonConvert.DeserializeObject<Request>(requestBody);
                _request.ACTION = "0";
                await _transactionRecords.InsertTransactionRecords(_request, _response);
                Log.Information("PartnerTopUp request received:  " + _request.Referencenumber);
                _datavalidator.DataValidators(enumTransactionType.PartnerTopUp, _request);
                _response = await _ProcessAsyncTransactionInterface.InsertPartnerTopup(_request);
                _transactionRecords.InsertTransactionRecords(_request, _response);
                Log.Information("PartnerTopUp response sent :  " + _request.Referencenumber);
                return StatusCode(StatusCodes.Status200OK, _response);
            }
            catch (Exception ex)
            {
                string excep = "An error occurred during processing." + ex.Message + " " + ex.Source + " " + ex.StackTrace;
                Log.Error(ex, excep);
                _transactionRecords.InsertTransactionRecords(_request, _response);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred during processing.");
            }
        }

        [HttpPost("PartnerBlock")]
        public async Task<IActionResult> PartnerBlock()
        {
            try
            {
                var requestBody = await new StreamReader(Request.Body).ReadToEndAsync();
                if (string.IsNullOrEmpty(requestBody.Trim()))
                {
                    Log.Information("Received an empty request body.");
                    return BadRequest("Invalid request body.");
                }
                requestBody = await _crypto.SSLDecrypt(requestBody);
                _request = JsonConvert.DeserializeObject<Request>(requestBody);
                Log.Information("PartnerBlock request received: " + _request.Referencenumber);
                _request.ACTION = "0";
                await _transactionRecords.InsertTransactionRecords(_request, _response);
                _datavalidator.DataValidators(enumTransactionType.PartnerBlock, _request);
                _response = await _ProcessAsyncTransactionInterface.BlockPartner(_request);
                _transactionRecords.InsertTransactionRecords(_request, _response);
                Log.Information("PartnerBlock response sent :  " + _request.Referencenumber);
                return StatusCode(StatusCodes.Status200OK, _response);
            }
            catch (Exception ex)
            {
                string excep = "An error occurred during processing." + ex.Message + " " + ex.Source + " " + ex.StackTrace;
                Log.Error(ex, excep);
                _transactionRecords.InsertTransactionRecords(_request, _response);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred during processing.");
            }
        }

        [HttpPost("PartnerUnblock")]
        public async Task<IActionResult> PartnerUnblock()
        {
            try
            {
                var requestBody = await new StreamReader(Request.Body).ReadToEndAsync();
                if (string.IsNullOrEmpty(requestBody.Trim()))
                {
                    Log.Information("Received an empty request body.");
                    return BadRequest("Invalid request body.");
                }
                requestBody = await _crypto.SSLDecrypt(requestBody);
                _request = JsonConvert.DeserializeObject<Request>(requestBody);
                Log.Information("PartnerUnblock request received: " + _request.Referencenumber);
                _request.ACTION = "0";
                await _transactionRecords.InsertTransactionRecords(_request, _response);
                _datavalidator.DataValidators(enumTransactionType.ParttnerOnboard, _request);
                _response = await _ProcessAsyncTransactionInterface.UnblockPartner(_request);
                _transactionRecords.InsertTransactionRecords(_request, _response);
                Log.Information("PartnerBlock response sent: " + _request.Referencenumber);
                return StatusCode(StatusCodes.Status200OK, _response);
            }
            catch (Exception ex)
            {
                string excep = "An error occurred during processing." + ex.Message + " " + ex.Source + " " + ex.StackTrace;
                Log.Error(ex, excep);
                _transactionRecords.InsertTransactionRecords(_request, _response);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred during processing.");
            }
        }

        [HttpPost("CheckPartnerLimit")]
        public async Task<IActionResult> CheckPartnerLimit()
        {
            try
            {
                var requestBody = await new StreamReader(Request.Body).ReadToEndAsync();
                requestBody = await _crypto.SSLDecrypt(requestBody);
                _request = JsonConvert.DeserializeObject<Request>(requestBody);
                Log.Information("CheckPartnerLimit request received: " +  _request.Referencenumber);
                _request.ACTION = "0";
                await _transactionRecords.InsertTransactionRecords(_request, _response);
                _datavalidator.DataValidators(enumTransactionType.ParttnerOnboard, _request);
                _response = await _ProcessAsyncTransactionInterface.CheckPartnerLimit(_request);
                _transactionRecords.InsertTransactionRecords(_request, _response);
                Log.Information("CheckPartnerLimit response sent: " + _request.Referencenumber);
                return StatusCode(StatusCodes.Status200OK, _response);
            }
            catch (Exception ex)
            {
                string excep = "An error occurred during processing." + ex.Message + " " + ex.Source + " " + ex.StackTrace;
                Log.Error(ex, excep);
                _transactionRecords.InsertTransactionRecords(_request, _response);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred during processing.");
            }
        }

        [HttpPost("GetPartnerDetails")]
        public async Task<IActionResult> GetPartnerDetails()
        {
            try
            {
                var requestBody = await new StreamReader(Request.Body).ReadToEndAsync();
                requestBody = await _crypto.SSLDecrypt(requestBody);
                _request = JsonConvert.DeserializeObject<Request>(requestBody);
                Log.Information("ReqAuth received: {RequestBody}", requestBody);
                 _datavalidator.DataValidators(enumTransactionType.ParttnerOnboard, _request);
                _response = await _ProcessAsyncTransactionInterface.InsertPartnerInformation(_request);
                return StatusCode(StatusCodes.Status200OK, _response);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred during processing.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred during processing.");
            }
        }

        [HttpPost("UpdatePartnerDetails")]
        public async Task<IActionResult> UpdatePartnerDetails()
        {
            try
            {
                var requestBody = await new StreamReader(Request.Body).ReadToEndAsync();
                requestBody = await _crypto.SSLDecrypt(requestBody);
                _request = JsonConvert.DeserializeObject<Request>(requestBody);
                Log.Information("ReqAuth received: {RequestBody}", requestBody);
                 _datavalidator.DataValidators(enumTransactionType.ParttnerOnboard, _request);
                _response = await _ProcessAsyncTransactionInterface.InsertPartnerInformation(_request);
                return StatusCode(StatusCodes.Status200OK, _response);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred during processing.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred during processing.");
            }
        }

        [HttpPost("PartnerAuthorization")]// maker checker from portal
        public async Task<IActionResult> PartnerAuthorization()
        {
            try
            {
                var requestBody = await new StreamReader(Request.Body).ReadToEndAsync();
                requestBody = await _crypto.SSLDecrypt(requestBody);
                _request = JsonConvert.DeserializeObject<Request>(requestBody);
                Log.Information("PartnerAuthorization received: {RequestBody}", requestBody);
                _datavalidator.DataValidators(enumTransactionType.PartnerAuthorization, _request);
                _response = await _ProcessAsyncTransactionInterface.InsertPartnerInformation(_request);
                return StatusCode(StatusCodes.Status200OK, _response);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred during processing.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred during processing.");
            }
        }

        [HttpPost("PartnerValidation")]
        public async Task<IActionResult> PartnerValidation()
        {
            try
            {
                var requestBody = await new StreamReader(Request.Body).ReadToEndAsync();
                requestBody = await _crypto.SSLDecrypt(requestBody);
                _request = JsonConvert.DeserializeObject<Request>(requestBody);
                Log.Information("ReqAuth received: {RequestBody}", requestBody);
                _datavalidator.DataValidators(enumTransactionType.ParttnerOnboard, _request);
                _response = await _ProcessAsyncTransactionInterface.InsertPartnerInformation(_request);
                return StatusCode(StatusCodes.Status200OK, _response);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred during processing.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred during processing.");
            }
        }


    }
}

