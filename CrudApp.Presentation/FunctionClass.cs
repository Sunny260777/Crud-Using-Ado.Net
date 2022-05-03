using CrudApp.DataModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace CrudApp.Presentation
{
    public class FunctionClass : IDisposable
    {

        public virtual string JSONResponse(string Status, string desc, dynamic ds, string error)
        {
            using (ResponseModel obj = new ResponseModel())
            {
                obj.status = Status;
                obj.desc = desc.Replace("\r", "").Replace("\t", "").Replace("\n", "");
                obj.data = ds;
                obj.error = error;
                return JsonConvert.SerializeObject(obj, Formatting.Indented);
            }
        }


        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~FunctionClass() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion


        public Boolean stringToBoolean(string val)
        {
            try
            {
                if (string.IsNullOrEmpty(val))
                    return false;
                else if (val == "1")
                    return true;
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public string ConvertTo_yyyyMMdd(string ddMMyyyy)
        {
            try
            {
                if (string.IsNullOrEmpty(ddMMyyyy))
                    return "";

                string[] dates = ddMMyyyy.Split('/');
                if (dates.Length == 3)
                {
                    return dates[2] + "-" + dates[1] + "-" + dates[0];
                }
                else
                    return "";
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        public string ConvertTo_HHmmss(string HHmmtt)
        {
            try
            {
                if (string.IsNullOrEmpty(HHmmtt))
                    return "";
                string hours = string.Empty;
                string minutes = string.Empty;

                string[] sb_HHmm = HHmmtt.Trim().Split(':');
                string[] mmtt = sb_HHmm[1].Trim().Split(' ');
                hours = sb_HHmm[0].Trim();
                hours = mmtt[1].Trim() == "PM" && Convert.ToInt32(hours) < 12 ? Convert.ToString(Convert.ToInt32(hours) + 12) : mmtt[1].Trim() == "AM" && Convert.ToInt32(hours) == 12 ? "00" : Convert.ToInt32(hours) < 10 ? "0" + Convert.ToInt32(hours).ToString() : hours;
                minutes = mmtt[0];
                return hours + ":" + minutes + ":00";
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public string ConvertTo_yyyyMMddHHmmss(string ddMMyyyy, string HHmmtt)
        {
            try
            {
                string hours = string.Empty;
                string minutes = string.Empty;

                string[] sb_ddMMyyyy = ddMMyyyy.Trim().Split('/');
                string[] sb_HHmm = HHmmtt.Trim().Split(':');
                string[] mmtt = sb_HHmm[1].Trim().Split(' ');
                hours = sb_HHmm[0].Trim();
                hours = mmtt[1].Trim() == "PM" && Convert.ToInt32(hours) < 12 ? Convert.ToString(Convert.ToInt32(hours) + 12) : mmtt[1].Trim() == "AM" && Convert.ToInt32(hours) == 12 ? "00" : Convert.ToInt32(hours) < 10 ? "0" + Convert.ToInt32(hours).ToString() : hours;
                minutes = mmtt[0];
                return sb_ddMMyyyy[2] + "-" + sb_ddMMyyyy[1] + "-" + sb_ddMMyyyy[0] + " " + hours + ":" + minutes + ":00";
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public int StringToInt(string Value)
        {
            try
            {
                Value = Value.Trim();
                if (Value == "")
                    return 0;
                foreach (char c in Value)
                {
                    if (!((c >= '0' && c <= '9') || c == '.' || c == '-'))
                        return 0;
                }
                return Convert.ToInt32(Value);
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public static string EncryptString(string text)
        {
            var key = Encoding.UTF8.GetBytes("E546C8DF278CD5931069B522E695D4F2");

            using (var aesAlg = Aes.Create())
            {
                using (var encryptor = aesAlg.CreateEncryptor(key, aesAlg.IV))
                {
                    using (var msEncrypt = new MemoryStream())
                    {
                        using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                        using (var swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(text);
                        }

                        var iv = aesAlg.IV;

                        var decryptedContent = msEncrypt.ToArray();

                        var result = new byte[iv.Length + decryptedContent.Length];

                        Buffer.BlockCopy(iv, 0, result, 0, iv.Length);
                        Buffer.BlockCopy(decryptedContent, 0, result, iv.Length, decryptedContent.Length);

                        return Convert.ToBase64String(result);
                    }
                }
            }
        }
        public static string DecryptString(string cipherText)
        {
            var fullCipher = Convert.FromBase64String(cipherText);

            var iv = new byte[16];
            var cipher = new byte[16];

            Buffer.BlockCopy(fullCipher, 0, iv, 0, iv.Length);
            Buffer.BlockCopy(fullCipher, iv.Length, cipher, 0, iv.Length);
            var key = Encoding.UTF8.GetBytes("E546C8DF278CD5931069B522E695D4F2");

            using (var aesAlg = Aes.Create())
            {
                using (var decryptor = aesAlg.CreateDecryptor(key, iv))
                {
                    string result;
                    using (var msDecrypt = new MemoryStream(cipher))
                    {
                        using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                        {
                            using (var srDecrypt = new StreamReader(csDecrypt))
                            {
                                result = srDecrypt.ReadToEnd();
                            }
                        }
                    }

                    return result;
                }
            }
        }
    }
}