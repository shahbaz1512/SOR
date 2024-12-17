using Serilog;
using SORAPI.Classes;
using SORAPI.Interface;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;

namespace SORAPI.DataValidator
{
    public class VaildateRequest : Datavalidator
    {
        Response _response = new Response();
        string certPath = "E:\\Documents\\MaximusNew.pfx";
        string certPassword = "P@ss1234";

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

        public async Task<string> SSLDecrypt(string encryptedMessage)
        {
            string decryptedMessage = string.Empty;
            try
            {
                X509Certificate2 cert = new X509Certificate2(certPath, certPassword);
                // Get the public key
                using (RSA privateKey = cert.GetRSAPrivateKey())
                {
                    // Decrypt the message using the private key
                    decryptedMessage = EncryptDecrypt.Decrypt(encryptedMessage, privateKey);
                    Console.WriteLine("Decrypted Message: \n" + decryptedMessage);
                }
                return decryptedMessage;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred during SSLDecrypt.");
                return encryptedMessage;
            }
        }

        public async Task<string> SSLEncrypt(string PlainMessage)
        {
            string encryptedMessage = string.Empty;
            try
            {
                X509Certificate2 cert = new X509Certificate2(certPath, certPassword);
                // Get the public key
                using (RSA publicKey = cert.GetRSAPublicKey())
                {
                    // Encrypt the message using the public key
                    encryptedMessage = EncryptDecrypt.Encrypt(PlainMessage, publicKey);
                    Console.WriteLine("Encrypted Message: \n" + encryptedMessage);
                }
                return encryptedMessage;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred during SSLencrypt.");
                return PlainMessage;
            }
        }


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

    }
}
