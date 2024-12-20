using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PARTNERSORAPI.Classes;
using Serilog;
using SORAPI.Classes;
using SORAPI.Interface;

namespace SORAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TSP : ControllerBase
    {
        private readonly ProcessSyncTransactionInterface _ProcessAsyncTransactionInterface;
        private readonly Datavalidator _datavalidator;
        private readonly TransactionRecords _transactionRecords;
        private readonly Commondetail _commondetail;
        private readonly Crypto _crypto;
        //  interface is injected dependency in controller
        public TSP(ProcessSyncTransactionInterface TransactionInterfaceInterface, Datavalidator datavalidator, TransactionRecords transactionRecords, Commondetail commondetail, Crypto crypto)  //////Constructor class  with dependncy injection  
        {
            _ProcessAsyncTransactionInterface = TransactionInterfaceInterface ?? throw new ArgumentNullException(nameof(TransactionInterfaceInterface));
            _datavalidator = datavalidator ?? throw new ArgumentException(nameof(TransactionInterfaceInterface));
            _transactionRecords = transactionRecords ?? throw new ArgumentException(nameof(transactionRecords));
            _commondetail = commondetail ?? throw new ArgumentException(nameof(Commondetail));
            _crypto = crypto ?? throw new ArgumentException(nameof(Crypto));
        }
        TSPRequest _request = new();
        TSPResponse _response = new();
        [HttpPost("TSPonbaoard")]
        public async Task<IActionResult> TSPonbaoard()
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
                _request = JsonConvert.DeserializeObject<TSPRequest>(requestBody);
                Log.Information("TSPOnbaoard request received:  " + _request.ReferenceNumber);
                _request.ACTION = "0";
                await _transactionRecords.InsertTSPTransactionRecords(_request, _response);
                if (!_datavalidator.TSPDataValidators(enumTransactionType.TSPOnboard, _request))
                {
                    _response.ResponseCode = await _commondetail.GetResponseCodeAsync(ConstResponseCode.AllMendatoryFieldsRequired);
                    _response.ResponseDescription = await _commondetail.GetResponseCodeDescriptionAsync(_response.ResponseCode);
                    return StatusCode(StatusCodes.Status200OK, _response);
                }
                _response = await _ProcessAsyncTransactionInterface.InsertTSPRegistration(_request);
                _transactionRecords.InsertTSPTransactionRecords(_request, _response);
                Log.Information("partnerOnbaoard response sent :  " + _request.ReferenceNumber);
                return StatusCode(StatusCodes.Status200OK, _response);
            }
            catch (Exception ex)
            {
                string excep = "An error occurred during processing." + ex.Message + " " + ex.Source + " " + ex.StackTrace;
                Log.Error(ex, excep);
                _transactionRecords.InsertTSPTransactionRecords(_request, _response);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred during processing.");
            }
        }

    }
}
