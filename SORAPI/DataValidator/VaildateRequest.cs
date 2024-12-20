using Serilog;
using SORAPI.Classes;
using SORAPI.Interface;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;

namespace SORAPI.DataValidator
{
    public class VaildateRequest : Datavalidator
    {
        //Response _response = new Response();
        //string certPath = "E:\\Documents\\MaximusNew.pfx";
        //string certPassword = "P@ss1234";

        public bool DataValidators(enumTransactionType _TransType, Request request)
        {
            try
            {
                switch (_TransType)
                {
                    case enumTransactionType.ParttnerOnboard:
                        return ValidatePartnerOnboardRequest(request);

                    case enumTransactionType.PartnerValidation:
                        return ValidatePartnerRequest (request);

                    case enumTransactionType.UpdatePartnerDetails:
                        return ValidateUpdatePartnerRequest(request);

                    case enumTransactionType.GetPartnerDetails:
                        return ValidateGetPartnerRequest(request);

                    case enumTransactionType.PartnerBlock:
                        return ValidateBlockRequest(request);

                    case enumTransactionType.PartnerUnblock:
                        return ValidateUnblockRequest(request);

                    case enumTransactionType.CheckPartnerLimit:
                        return ValidateChkPartnerLimitRequest(request);

                    case enumTransactionType.PartnerTopUp:
                        return ValidatePartnerTopupRequest(request);

                    case enumTransactionType.PartnerAuthorization:
                        return ValidatePartnerAuthorizationRequest(request);

                    default:
                        return false; // Handle unknown transaction types if necessary
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred during datavalidator processing.");
                return false;
            }
        }

        //public async Task<string> SSLDecrypt(string encryptedMessage)
        //{
        //    string decryptedMessage = string.Empty;
        //    try
        //    {
        //        X509Certificate2 cert = new X509Certificate2(certPath, certPassword);
        //        // Get the public key
        //        using (RSA privateKey = cert.GetRSAPrivateKey())
        //        {
        //            // Decrypt the message using the private key
        //            decryptedMessage = EncryptDecrypt.Decrypt(encryptedMessage, privateKey);
        //            Console.WriteLine("Decrypted Message: \n" + decryptedMessage);
        //        }
        //        return decryptedMessage;
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Error(ex, "An error occurred during SSLDecrypt.");
        //        return encryptedMessage;
        //    }
        //}

        //public async Task<string> SSLEncrypt(string PlainMessage)
        //{
        //    string encryptedMessage = string.Empty;
        //    try
        //    {
        //        X509Certificate2 cert = new X509Certificate2(certPath, certPassword);
        //        // Get the public key
        //        using (RSA publicKey = cert.GetRSAPublicKey())
        //        {
        //            // Encrypt the message using the public key
        //            encryptedMessage = EncryptDecrypt.Encrypt(PlainMessage, publicKey);
        //            Console.WriteLine("Encrypted Message: \n" + encryptedMessage);
        //        }
        //        return encryptedMessage;
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Error(ex, "An error occurred during SSLencrypt.");
        //        return PlainMessage;
        //    }
        //}


        private bool ValidatePartnerOnboardRequest(Request request)
        {
            return !AreAnyFieldsNullOrEmpty(
                request.PartnerId,
                request.Partnername,
                request.HasSubAggregators,
                request.Turnover.ToString(),
                request.Programs,
                request.Programtype,
                request.Accountnumber,
                request.Contactname,
                request.Contactnumber
            );
        }

        private bool ValidatePartnerRequest(Request request)
        {
            return !AreAnyFieldsNullOrEmpty(
                request.PartnerId,
                request.Partnername,
                request.HasSubAggregators,
                request.Turnover.ToString(),
                request.Programs,
                request.Programtype
            );
        }

        private bool ValidateUpdatePartnerRequest(Request request)
        {
            return !AreAnyFieldsNullOrEmpty(
                request.PartnerId,
                request.Partnername,
                request.HasSubAggregators,
                request.Turnover.ToString(),
                request.Programs,
                request.Programtype
            );
        }

        private bool ValidateGetPartnerRequest(Request request)
        {
            return !AreAnyFieldsNullOrEmpty(
                request.PartnerId,
                request.Partnername,
                request.HasSubAggregators,
                request.Turnover.ToString(),
                request.Programs,
                request.Programtype
            );
        }

        private bool ValidateBlockRequest(Request request)
        {
            return !AreAnyFieldsNullOrEmpty(
                request.PartnerId,
                request.Partnername,
                request.Channels,
                request.Referencenumber
            );
        }

        private bool ValidateUnblockRequest(Request request)
        {
            return !AreAnyFieldsNullOrEmpty(
                request.PartnerId,
                request.Partnername,
                request.Channels,
                request.Referencenumber
            );
        }

        private bool ValidateChkPartnerLimitRequest(Request request)
        {
            return !AreAnyFieldsNullOrEmpty(
                request.PartnerId,
                request.Partnername,
                request.HasSubAggregators,
                request.Turnover.ToString(),
                request.Programs,
                request.Programtype
            );
        }

        private bool ValidatePartnerTopupRequest(Request request)
        {
            return !AreAnyFieldsNullOrEmpty(
                request.PartnerId,
                request.Partnername,
                request.HasSubAggregators,
                request.Turnover.ToString(),
                request.Programs,
                request.Programtype
            );
        }

        private bool ValidatePartnerAuthorizationRequest(Request request)
        {
            return !AreAnyFieldsNullOrEmpty(
                request.PartnerId,
                request.Partnername,
                request.HasSubAggregators,
                request.Turnover.ToString(),
                request.Programs,
                request.Programtype
            );
        }

        private bool AreAnyFieldsNullOrEmpty(params string[] fields)
        {
            return fields.Any(string.IsNullOrEmpty);
        }

        // TSP Validation
        public bool TSPDataValidators(enumTransactionType _TransType, TSPRequest tsprequest)
        {
            try
            {
                switch (_TransType)
                {
                    case enumTransactionType.TSPOnboard:
                        return ValidateTSPOnboardRequest(tsprequest);

                    case enumTransactionType.TSPValidation:
                        return ValidateTSPRequest(tsprequest);

                    case enumTransactionType.UpdateTSPDetails:
                        return ValidateUpdateTSPRequest(tsprequest);

                    case enumTransactionType.GetTSPDetails:
                        return ValidateGetTSPRequest(tsprequest);

                    case enumTransactionType.TSPBlock:
                        return ValidateTSPBlockRequest(tsprequest);

                    case enumTransactionType.TSPUnblock:
                        return ValidateTSPUnblockRequest(tsprequest);

                    case enumTransactionType.CheckTSPLimit:
                        return ValidateChkTSPLimitRequest(tsprequest);

                    case enumTransactionType.TSPAuthorization:
                        return ValidateTSPAuthorizationRequest(tsprequest);

                    default:
                        return false; // Handle unknown transaction types if necessary
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred during datavalidator processing.");
                return false;
            }
        }

        private bool ValidateTSPOnboardRequest(TSPRequest tsprequest)
        {
            return !AreAnyFieldsNullOrEmpty(
                tsprequest.TspId,
                tsprequest.TspName,
                tsprequest.PersonalIdentificationNumber,
                tsprequest.ProgramId,
                tsprequest.AccountNumber,
                tsprequest.ContactPerson,
                tsprequest.ContactPhone
            );
        }

        private bool ValidateTSPRequest(TSPRequest tsprequest)
        {
            return !AreAnyFieldsNullOrEmpty(
                tsprequest.TspId,
                tsprequest.TspName,
                tsprequest.PersonalIdentificationNumber,
                tsprequest.ProgramId,
                tsprequest.AccountNumber,
                tsprequest.ContactPerson,
                tsprequest.ContactPhone
            );
        }

        private bool ValidateUpdateTSPRequest(TSPRequest tsprequest)
        {
            return !AreAnyFieldsNullOrEmpty(
                tsprequest.TspId,
                tsprequest.TspName,
                tsprequest.PersonalIdentificationNumber,
                tsprequest.ProgramId,
                tsprequest.AccountNumber,
                tsprequest.ContactPerson,
                tsprequest.ContactPhone
            );
        }

        private bool ValidateGetTSPRequest(TSPRequest tsprequest)
        {
            return !AreAnyFieldsNullOrEmpty(
                tsprequest.TspId,
                tsprequest.TspName,
                tsprequest.PersonalIdentificationNumber,
                tsprequest.ProgramId,
                tsprequest.AccountNumber,
                tsprequest.ContactPerson,
                tsprequest.ContactPhone
            );
        }

        private bool ValidateTSPBlockRequest(TSPRequest tsprequest)
        {
            return !AreAnyFieldsNullOrEmpty(
                tsprequest.TspId,
                tsprequest.TspName,
                tsprequest.PersonalIdentificationNumber,
                tsprequest.ProgramId,
                tsprequest.AccountNumber,
                tsprequest.ContactPerson,
                tsprequest.ContactPhone
            );
        }

        private bool ValidateTSPUnblockRequest(TSPRequest tsprequest)
        {
            return !AreAnyFieldsNullOrEmpty(
                tsprequest.TspId,
                tsprequest.TspName,
                tsprequest.PersonalIdentificationNumber,
                tsprequest.ProgramId,
                tsprequest.AccountNumber,
                tsprequest.ContactPerson,
                tsprequest.ContactPhone
            );
        }

        private bool ValidateChkTSPLimitRequest(TSPRequest tsprequest)
        {
            return !AreAnyFieldsNullOrEmpty(
                tsprequest.TspId,
                tsprequest.TspName,
                tsprequest.PersonalIdentificationNumber,
                tsprequest.ProgramId,
                tsprequest.AccountNumber,
                tsprequest.ContactPerson,
                tsprequest.ContactPhone
            );
        }

        private bool ValidateTSPAuthorizationRequest(TSPRequest tsprequest)
        {
            return !AreAnyFieldsNullOrEmpty(
                tsprequest.TspId,
                tsprequest.TspName,
                tsprequest.PersonalIdentificationNumber,
                tsprequest.ProgramId,
                tsprequest.AccountNumber,
                tsprequest.ContactPerson,
                tsprequest.ContactPhone
            );
        }



        // Programmanager Validation
        public bool PMDataValidators(enumTransactionType _TransType, ProgrammanagerRequest pmrequest)
        {
            try
            {
                switch (_TransType)
                {
                    case enumTransactionType.TSPOnboard:
                        return ValidatePMOnboardRequest(pmrequest);

                    case enumTransactionType.TSPValidation:
                        return ValidatePMRequest(pmrequest);

                    case enumTransactionType.UpdateTSPDetails:
                        return ValidateUpdatePMRequest(pmrequest);

                    case enumTransactionType.GetTSPDetails:
                        return ValidateGetPMRequest(pmrequest);

                    case enumTransactionType.TSPBlock:
                        return ValidatePMBlockRequest(pmrequest);

                    case enumTransactionType.TSPUnblock:
                        return ValidatePMUnblockRequest(pmrequest);

                    case enumTransactionType.CheckTSPLimit:
                        return ValidateChkPMLimitRequest(pmrequest);

                    case enumTransactionType.TSPAuthorization:
                        return ValidatePMAuthorizationRequest(pmrequest);

                    default:
                        return false; // Handle unknown transaction types if necessary
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred during datavalidator processing.");
                return false;
            }
        }

        private bool ValidatePMOnboardRequest(ProgrammanagerRequest pmrequest)
        {
            return !AreAnyFieldsNullOrEmpty(
                pmrequest.ProgramId,
                pmrequest.ProgramName,
                pmrequest.ProgramId,
                pmrequest.AccountNumber,
                pmrequest.ContactPerson,
                pmrequest.ContactPhone
            );
        }

        private bool ValidatePMRequest(ProgrammanagerRequest pmrequest)
        {
            return !AreAnyFieldsNullOrEmpty(
                pmrequest.ProgramId,
                pmrequest.ProgramName,
                pmrequest.ProgramId,
                pmrequest.AccountNumber,
                pmrequest.ContactPerson,
                pmrequest.ContactPhone
            );
        }

        private bool ValidateUpdatePMRequest(ProgrammanagerRequest pmrequest)
        {
            return !AreAnyFieldsNullOrEmpty(
                pmrequest.ProgramId,
                pmrequest.ProgramName,
                pmrequest.ProgramId,
                pmrequest.AccountNumber,
                pmrequest.ContactPerson,
                pmrequest.ContactPhone
            );
        }

        private bool ValidateGetPMRequest(ProgrammanagerRequest pmrequest)
        {
            return !AreAnyFieldsNullOrEmpty(
                pmrequest.ProgramId,
                pmrequest.ProgramName,
                pmrequest.ProgramId,
                pmrequest.AccountNumber,
                pmrequest.ContactPerson,
                pmrequest.ContactPhone
            );
        }

        private bool ValidatePMBlockRequest(ProgrammanagerRequest pmrequest)
        {
            return !AreAnyFieldsNullOrEmpty(
                pmrequest.ProgramId,
                pmrequest.ProgramName,
                pmrequest.ProgramId,
                pmrequest.AccountNumber,
                pmrequest.ContactPerson,
                pmrequest.ContactPhone
            );
        }

        private bool ValidatePMUnblockRequest(ProgrammanagerRequest pmrequest)
        {
            return !AreAnyFieldsNullOrEmpty(
                pmrequest.ProgramId,
                pmrequest.ProgramName,
                pmrequest.ProgramId,
                pmrequest.AccountNumber,
                pmrequest.ContactPerson,
                pmrequest.ContactPhone
            );
        }

        private bool ValidateChkPMLimitRequest(ProgrammanagerRequest pmrequest)
        {
            return !AreAnyFieldsNullOrEmpty(
                pmrequest.ProgramId,
                pmrequest.ProgramName,
                pmrequest.ProgramId,
                pmrequest.AccountNumber,
                pmrequest.ContactPerson,
                pmrequest.ContactPhone
            );
        }

        private bool ValidatePMAuthorizationRequest(ProgrammanagerRequest pmrequest)
        {
            return !AreAnyFieldsNullOrEmpty(
                pmrequest.ProgramId,
                pmrequest.ProgramName,
                pmrequest.ProgramId,
                pmrequest.AccountNumber,
                pmrequest.ContactPerson,
                pmrequest.ContactPhone
            );
        }


        // Programmanager Validation
        public bool CustomerDataValidators(enumTransactionType _TransType, CustomerRequest Custrequest)
        {
            try
            {
                switch (_TransType)
                {
                    case enumTransactionType.TSPOnboard:
                        return ValidateCustOnboardRequest(Custrequest);

                    case enumTransactionType.TSPValidation:
                        return ValidateCustRequest(Custrequest);

                    case enumTransactionType.UpdateTSPDetails:
                        return ValidateUpdateCustRequest(Custrequest);

                    case enumTransactionType.GetTSPDetails:
                        return ValidateGetCustRequest(Custrequest);

                    case enumTransactionType.TSPBlock:
                        return ValidateCustBlockRequest(Custrequest);

                    case enumTransactionType.TSPUnblock:
                        return ValidateCustUnblockRequest(Custrequest);

                    case enumTransactionType.CheckTSPLimit:
                        return ValidateChkCustLimitRequest(Custrequest);

                    case enumTransactionType.TSPAuthorization:
                        return ValidateCustAuthorizationRequest(Custrequest);

                    default:
                        return false; // Handle unknown transaction types if necessary
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred during datavalidator processing.");
                return false;
            }
        }

        private bool ValidateCustOnboardRequest(CustomerRequest Custrequest)
        {
            return !AreAnyFieldsNullOrEmpty(
                Custrequest.CustomerId,
                Custrequest.ProgramCode,
                Custrequest.ProgramId,
                Custrequest.AccountNumber,
                Custrequest.FirstName,
                Custrequest.MobileNumber
            );
        }

        private bool ValidateCustRequest(CustomerRequest Custrequest)
        {
            return !AreAnyFieldsNullOrEmpty(
                Custrequest.CustomerId,
                Custrequest.ProgramCode,
                Custrequest.ProgramId,
                Custrequest.AccountNumber,
                Custrequest.FirstName,
                Custrequest.MobileNumber
            );
        }

        private bool ValidateUpdateCustRequest(CustomerRequest Custrequest)
        {
            return !AreAnyFieldsNullOrEmpty(
                Custrequest.CustomerId,
                Custrequest.ProgramCode,
                Custrequest.ProgramId,
                Custrequest.AccountNumber,
                Custrequest.FirstName,
                Custrequest.MobileNumber
            );
        }

        private bool ValidateGetCustRequest(CustomerRequest Custrequest)
        {
            return !AreAnyFieldsNullOrEmpty(
                Custrequest.CustomerId,
                Custrequest.ProgramCode,
                Custrequest.ProgramId,
                Custrequest.AccountNumber,
                Custrequest.FirstName,
                Custrequest.MobileNumber
            );
        }

        private bool ValidateCustBlockRequest(CustomerRequest Custrequest)
        {
            return !AreAnyFieldsNullOrEmpty(
                Custrequest.CustomerId,
                Custrequest.ProgramCode,
                Custrequest.ProgramId,
                Custrequest.AccountNumber,
                Custrequest.FirstName,
                Custrequest.MobileNumber
            );
        }

        private bool ValidateCustUnblockRequest(CustomerRequest Custrequest)
        {
            return !AreAnyFieldsNullOrEmpty(
                Custrequest.CustomerId,
                Custrequest.ProgramCode,
                Custrequest.ProgramId,
                Custrequest.AccountNumber,
                Custrequest.FirstName,
                Custrequest.MobileNumber
            );
        }

        private bool ValidateChkCustLimitRequest(CustomerRequest Custrequest)
        {
            return !AreAnyFieldsNullOrEmpty(
                Custrequest.CustomerId,
                Custrequest.ProgramCode,
                Custrequest.ProgramId,
                Custrequest.AccountNumber,
                Custrequest.FirstName,
                Custrequest.MobileNumber
            );
        }

        private bool ValidateCustAuthorizationRequest(CustomerRequest Custrequest)
        {
            return !AreAnyFieldsNullOrEmpty(
                Custrequest.CustomerId,
                Custrequest.ProgramCode,
                Custrequest.ProgramId,
                Custrequest.AccountNumber,
                Custrequest.FirstName,
                Custrequest.MobileNumber
            );
        }
    }
}
