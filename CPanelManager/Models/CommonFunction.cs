using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CPanelManager.ViewModels.Account;
using CPanelManager.Helpers;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Principal;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using System.Net;
using CPanelManager.ViewModels;
using System.Text;
using System.Net.Mail;
using System.IO;
using System.Web;
using System.Security.Cryptography;
using System.Configuration;
using System.Data;
using System.Reflection;

namespace CPanelManager.Models
{
    public class CommonFunction
    {
        private readonly ILogger _logger;
        private static IConfiguration _Configure;
        private static string apiBaseUrlNew;

        public CommonFunction(ILoggerFactory loggerFactory, IConfiguration configuration)
        {
            _logger = loggerFactory.CreateLogger<CommonFunction>();
            _Configure = configuration;
            apiBaseUrlNew = _Configure.GetValue<string>("WebAPIBaseUrl");
        }

        public enum Mode
        {
            SAVE,
            UPDATE,
            DELETE,
            SELECT
        }
        public static Task<string> GetWebAPI(string endpoint)
        {
            try
            {
                //StringContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                using (var client = new HttpClient())
                {
                    var responseTask = client.GetAsync(endpoint);
                    responseTask.Wait();

                    var result = responseTask.Result;

                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsStringAsync();
                        readTask.Wait();

                        return readTask;
                    }
                    else //web api sent error response 
                    {
                        //log response status here..
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static Task<string> PostWebAPI(string endpoint,object obj)
        {
            try
            {
                //StringContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                using (var client = new HttpClient())
                {
                    string data = JsonConvert.SerializeObject(obj);
                    StringContent content = new StringContent(data, System.Text.Encoding.UTF8, "application/json");

                    var responseTask = client.PostAsync(endpoint, content);
                    responseTask.Wait();

                    var result = responseTask.Result;

                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsStringAsync();
                        readTask.Wait();

                        return readTask;
                    }
                    else //web api sent error response 
                    {
                        //log response status here..
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static Task<string> PutWebAPI(string endpoint, object obj)
        {
            try
            {
                //StringContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                using (var client = new HttpClient())
                {
                    string data = JsonConvert.SerializeObject(obj);
                    StringContent content = new StringContent(data, System.Text.Encoding.UTF8, "application/json");

                    var responseTask = client.PutAsync(endpoint, content);
                    responseTask.Wait();

                    var result = responseTask.Result;

                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsStringAsync();
                        readTask.Wait();

                        return readTask;
                    }
                    else //web api sent error response 
                    {
                        //log response status here..
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static Task<string> DeleteWebAPI(string endpoint)
        {
            try
            {
                //StringContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                using (var client = new HttpClient())
                {
                    //string data = JsonConvert.SerializeObject(obj);
                    //StringContent content = new StringContent(data, System.Text.Encoding.UTF8, "application/json");

                    var responseTask = client.DeleteAsync(endpoint);
                    responseTask.Wait();

                    var result = responseTask.Result;

                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsStringAsync();
                        readTask.Wait();

                        return readTask;
                    }
                    else //web api sent error response 
                    {
                        //log response status here..
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static long NextMasterId(string TableName, string apiBaseUrl)
        {
            try
            {
                string endpoint = apiBaseUrl + "NextMasterId?TableName=" + TableName;
                IEnumerable<NextMasterId> objNextMasterIdList = null;

                if (CommonFunction.GetWebAPI(endpoint) != null)
                {
                    objNextMasterIdList = JsonConvert.DeserializeObject<IEnumerable<NextMasterId>>(CommonFunction.GetWebAPI(endpoint).Result).ToList();
                }
                else
                {
                    objNextMasterIdList = Enumerable.Empty<NextMasterId>().ToList();
                }

                return objNextMasterIdList.Select(o => o.MasterId).FirstOrDefault();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public static long NextMasterIdAssets(string TableName, string apiBaseUrl)
        {
            try
            {
                string endpoint = apiBaseUrl + "AssetsNextMasterId?TableName=" + TableName;
                IEnumerable<NextMasterId> objNextMasterIdList = null;

                if (CommonFunction.GetWebAPI(endpoint) != null)
                {
                    objNextMasterIdList = JsonConvert.DeserializeObject<IEnumerable<NextMasterId>>(CommonFunction.GetWebAPI(endpoint).Result).ToList();
                }
                else
                {
                    objNextMasterIdList = Enumerable.Empty<NextMasterId>().ToList();
                }

                return objNextMasterIdList.Select(o => o.MasterId).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<DropDownFill> DropDownFill(string _TableName, long _MasterId, string _Type, string _apiBaseUrl)
        {
            try
            {
                string endpoint = _apiBaseUrl + "DropDownFill?TableName=" + _TableName + "&MasterId=" + _MasterId  + "&Type=" + _Type;
                IEnumerable<DropDownFill> objDropDownFillList = null;

                if (CommonFunction.GetWebAPI(endpoint) != null)
                {
                    objDropDownFillList = JsonConvert.DeserializeObject<IEnumerable<DropDownFill>>(CommonFunction.GetWebAPI(endpoint).Result).ToList();
                }
                else
                {
                    objDropDownFillList = Enumerable.Empty<DropDownFill>().ToList();
                }

                return objDropDownFillList.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<DropDownFill> DropDownFillAssets(string _TableName, long _MasterId, string _Type, string _apiBaseUrl)
        {
            try
            {
                string endpoint = _apiBaseUrl + "AssetsDropDownFill?TableName=" + _TableName + "&MasterId=" + _MasterId + "&Type=" + _Type;
                IEnumerable<DropDownFill> objDropDownFillList = null;

                if (CommonFunction.GetWebAPI(endpoint) != null)
                {
                    objDropDownFillList = JsonConvert.DeserializeObject<IEnumerable<DropDownFill>>(CommonFunction.GetWebAPI(endpoint).Result).ToList();
                }
                else
                {
                    objDropDownFillList = Enumerable.Empty<DropDownFill>().ToList();
                }

                return objDropDownFillList.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string CheckAvailable(string _TableName, long _MasterId, string _Type, string _apiBaseUrl)
        {
            try
            {
                string endpoint = _apiBaseUrl + "DropDownFill?TableName=" + _TableName + "&MasterId=" + _MasterId + "&Type=" + _Type;
                IEnumerable<CheckAvailable> objCheckAvailableList = null;

                if (CommonFunction.GetWebAPI(endpoint) != null)
                {
                    objCheckAvailableList = JsonConvert.DeserializeObject<IEnumerable<CheckAvailable>>(CommonFunction.GetWebAPI(endpoint).Result).ToList();
                }
                else
                {
                    objCheckAvailableList = Enumerable.Empty<CheckAvailable>().ToList();
                }

                return objCheckAvailableList.Select(o=>o.NameAvailable).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string CheckAvailableAssets(string _TableName, long _MasterId, string _Type, string _apiBaseUrl)
        {
            try
            {
                string endpoint = _apiBaseUrl + "AssetsDropDownFill?TableName=" + _TableName + "&MasterId=" + _MasterId + "&Type=" + _Type;
                IEnumerable<CheckAvailable> objCheckAvailableList = null;

                if (CommonFunction.GetWebAPI(endpoint) != null)
                {
                    objCheckAvailableList = JsonConvert.DeserializeObject<IEnumerable<CheckAvailable>>(CommonFunction.GetWebAPI(endpoint).Result).ToList();
                }
                else
                {
                    objCheckAvailableList = Enumerable.Empty<CheckAvailable>().ToList();
                }

                return objCheckAvailableList.Select(o => o.NameAvailable).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static GlobalMessageViewModel GlobalMessage(int MessageType, int MessageColor, int MessageIcon, string MessageTitle, string MessageText, string MessageTextI)
        {
            var objGetGlobalMessageViewModel = new GlobalMessageViewModel();
            objGetGlobalMessageViewModel.GlobalMessageType = MessageType;
            objGetGlobalMessageViewModel.GlobalMessageColor = MessageColor;
            objGetGlobalMessageViewModel.GlobalMessageIcon = MessageIcon;

            objGetGlobalMessageViewModel.GlobalMessageTitle = MessageTitle; // "Hi " + FullName;
            objGetGlobalMessageViewModel.GlobalMessageText = MessageText;// "You Have Successfully Login!";
            objGetGlobalMessageViewModel.GlobalMessageTextI = MessageTextI; // "Your Last Successfull Login Was on Dated " + DateTime.Now.ToString("dd-MMM-yyyy hh:mm") + "!";
            objGetGlobalMessageViewModel.GlobalMessageDate = DateTime.Now.ToString("dd-MMM-yyyy");
            objGetGlobalMessageViewModel.GlobalMessageTime = DateTime.Now.ToString("hh:mm:ss");

            return objGetGlobalMessageViewModel;
        }

        public static ViewModels.ErrorMessageViewModel HandleErrorInfo(Exception _exception, string _Controler, string _ActionResult)
        {
            ErrorMessageViewModel objErrorMessageViewModel = new ErrorMessageViewModel();
            objErrorMessageViewModel.Exception = _exception;
            objErrorMessageViewModel.Controler = _Controler;
            objErrorMessageViewModel.ActionResult = _ActionResult;

            return objErrorMessageViewModel;
        }

        public static long? UserAuthentication(HttpContext context)
        {
            long EnterById = 0;
            System.Security.Claims.ClaimsPrincipal IUser = new ClaimsPrincipal();
            

            ProfileMenuRightsViewModel objProfileMenuRightsViewModel = context.Session.GetObjectFromJson<ProfileMenuRightsViewModel>("MenuDetail");

            if (objProfileMenuRightsViewModel != null && objProfileMenuRightsViewModel.ValidateAccountViewModelList != null && objProfileMenuRightsViewModel.ValidateAccountViewModelList.Count > 0)
            {
                EnterById = objProfileMenuRightsViewModel.ValidateAccountViewModelList.Select(a => a.MasterLoginId).FirstOrDefault();
            }
            
            return EnterById;
        }

        public static ValidateAccountViewModel ActionResultAuthentication(HttpContext context, string strControlerURL)
        {

            ProfileMenuRightsViewModel objProfileMenuRightsViewModel = context.Session.GetObjectFromJson<ProfileMenuRightsViewModel>("MenuDetail");
            
            ValidateAccountViewModel objValidateAccountViewModel = new ValidateAccountViewModel();

            if (objProfileMenuRightsViewModel != null && objProfileMenuRightsViewModel.ValidateAccountViewModelList.Count > 0)
            {
                long ProfileMasterId = objProfileMenuRightsViewModel.ValidateAccountViewModelList.Select(a => a.MasterProfileId).FirstOrDefault() ;

                long iMenuMasterId = objProfileMenuRightsViewModel.ValidateAccountViewModelList.Where(a => a.FunctionLink == strControlerURL).Select(a => a.MasterFunctionId).FirstOrDefault();

                objValidateAccountViewModel = objProfileMenuRightsViewModel.ValidateAccountViewModelList.Where(a => a.MasterFunctionId == iMenuMasterId).FirstOrDefault();
            }
            return objValidateAccountViewModel;
        }

        public static CPanelManager.ViewModels.PaginationViewModel IndexPagination(int TotalCount, int PageSize, int LimitStart, int LimitEnd, string SearchCondition, int Page = 1, int Pageno = 0)
        {
            CPanelManager.ViewModels.PaginationViewModel objPaginationViewModel = new CPanelManager.ViewModels.PaginationViewModel();

            var count = TotalCount;
            var totalPages = (count / PageSize) - (count % PageSize == 0 ? 1 : 0);
            var CurrentPage = Page != null ? (int)Page : 1;
            var StartPage = CurrentPage - 2;
            var endPage = CurrentPage + 1;

            if (StartPage <= 0)
            {
                endPage -= (StartPage - 1);
                StartPage = 1;
            }
            if (endPage > totalPages)
            {
                endPage = totalPages;
                if (endPage > 5)
                {
                    StartPage = endPage - 4;
                }
            }

            int _TotalCount = 0, _LimitStart = 0, _LimitEnd = 0;

            if (Pageno != 0)
            {
                if (TotalCount == 0) { }
                else { LimitStart++; }


                _LimitStart = LimitStart;
                _LimitEnd = LimitEnd;
                _TotalCount = TotalCount;

                if (_TotalCount < _LimitEnd)
                {
                    _LimitEnd = _TotalCount;
                }
                else
                {
                    _LimitEnd = LimitEnd;
                }
            }
            else
            {
            }
            // For showing entries End //

            objPaginationViewModel.SearchCondition = SearchCondition;
            objPaginationViewModel.CurrentPage = CurrentPage;
            objPaginationViewModel.TotalPages = totalPages;
            objPaginationViewModel.StartPage = StartPage;
            objPaginationViewModel.EndPage = endPage;
            objPaginationViewModel.LimitStart = _LimitStart;
            objPaginationViewModel.LimitEnd = _LimitEnd;
            objPaginationViewModel.TotalCount = TotalCount;

            return objPaginationViewModel;
        }

        public static string RamdomCode(string RegistrationType, string endpoint)
        {
            //string endpoint = "https://adminwebapi.kritms.com/API/";

            Random r = new Random();
            String strCode = "", strNewCode = "";
            int code1 = 0, code2 = 0, code3 = 0, code4 = 0, code5 = 0, code6 = 0;
            long code7 = 0;

            if (RegistrationType == "VERIFICATIONCODE" || RegistrationType == "ONETIMEPASSWORD") //Login Type 0 for Generate Verification Code
            {
                strCode = r.Next(0, 9).ToString();
                code1 = r.Next(0, 9);
            }
            else if (RegistrationType == "EMPLOYEE") //Login Type 0 for Admin User Code Code
            {
                strCode = "IRS-";
                code7 = NextMasterId("ADMasterEmployee", endpoint);
                
                strNewCode = code7.ToString();
                while (strNewCode.Length < 6)
                {
                    strNewCode = "0" + strNewCode;
                }
            }
            else
            {
            }

            if (RegistrationType == "EMPLOYEE")
            {
                strCode = strCode + strNewCode;
            }
            else
            {
                strCode = strCode + code1.ToString() + code2.ToString() + code3.ToString() + code4.ToString() + code5.ToString();
            }

            return strCode;
        }

        public static int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }


        // Generate a random string with a given size    
        public static string RandomString(int size, bool lowerCase)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (lowerCase)
                return builder.ToString().ToLower();
            return builder.ToString();
        }

        // Generate a random password    
        public static string RandomPassword()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(RandomString(3, false));
            builder.Append(RandomNumber(10, 99));
            builder.Append(RandomString(2, true));
            builder.Append(RandomNumber(10, 99));
            builder.Append(RandomString(1, false));
            return builder.ToString();
        }

        public static string CreateRandomPassword(int length = 15)
        {
            // Create a string of characters, numbers, special characters that allowed in the password  
            //string validChars = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*?_-";
            string validChars = "ABCDEFGHJKLMNOPQRSTUVWXYZ0123456789";
            Random random = new Random();

            // Select one random character at a time from the string  
            // and create an array of chars  
            char[] chars = new char[length];
            for (int i = 0; i < length; i++)
            {
                chars[i] = validChars[random.Next(0, validChars.Length)];
            }
            return new string(chars);
        }

        public static bool SendMail(String MailTo, string Subject, String MailBody)
        {
            string MailFrom = "account@angelmart.in";
            String MailServer = "angelmart.in";
            int MailPort = 25;
            String MailPassword = "Angelmart1@34";

            //NameValueCollection myKeys = System.Configuration.ConfigurationManager.AppSettings;

            //string MailFrom = myKeys["EmailFrom"];
            //string MailPassword = myKeys["EmailPassword"];
            //string MailServer = myKeys["EmailServer"];
            //int MailPort = int.Parse(myKeys["EmailPort"]);

            using (MailMessage ObjMail = new MailMessage(MailFrom, MailTo))
            {

                ObjMail.Subject = Subject;
                ObjMail.Body = MailBody;
                ObjMail.BodyEncoding = Encoding.UTF8;
                ObjMail.IsBodyHtml = true;
                SmtpClient client = new SmtpClient();
                client.Host = MailServer;
                client.EnableSsl = false;

                System.Net.NetworkCredential basicCredential = new System.Net.NetworkCredential(MailFrom, MailPassword);
                client.UseDefaultCredentials = true;
                client.Credentials = basicCredential;
                client.Port = MailPort;
                try
                {
                    client.Send(ObjMail);
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }

        public static string SendMessage(string MobileNumber, string Message)
        {
            //Your authentication key
            string authKey = "17454AHmcZxynGs5cd68f99";
            //Multiple mobiles numbers separated by comma
            string mobileNumber = MobileNumber;
            //Sender ID,While using route4 sender id should be 6 characters long.
            string senderId = "CEPLHR";
            //Your message to send, Add URL encoding here.
            string message = WebUtility.HtmlEncode(Message);

            //Prepare you post parameters
            StringBuilder sbPostData = new StringBuilder();
            sbPostData.AppendFormat("authkey={0}", authKey);
            sbPostData.AppendFormat("&mobiles={0}", mobileNumber);
            sbPostData.AppendFormat("&message={0}", message);
            sbPostData.AppendFormat("&sender={0}", senderId);
            sbPostData.AppendFormat("&route={0}", "4");

            try
            {
                //Call Send SMS API
                string sendSMSUri = "http://sms.ssdindia.com/api/sendhttp.php";
                //Create HTTPWebrequest
                HttpWebRequest httpWReq = (HttpWebRequest)WebRequest.Create(sendSMSUri);
                //Prepare and Add URL Encoded data
                UTF8Encoding encoding = new UTF8Encoding();
                byte[] data = encoding.GetBytes(sbPostData.ToString());
                //Specify post method
                httpWReq.Method = "POST";
                httpWReq.ContentType = "application/x-www-form-urlencoded";
                httpWReq.ContentLength = data.Length;
                using (System.IO.Stream stream = httpWReq.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }
                //Get the response
                HttpWebResponse response = (HttpWebResponse)httpWReq.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream());
                string responseString = reader.ReadToEnd();

                //Close the response
                reader.Close();
                response.Close();
            }
            catch (SystemException ex)
            {
                ex.Message.ToString();
            }
            return null;
        }

        public static string Encrypt(string toEncrypt, bool useHashing)
        {
            byte[] keyArray;
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);

            // Get the key from config file
            string key = "CPanelManager";// _Configure.GetValue<string>("SecurityKey");
            //System.Windows.Forms.MessageBox.Show(key);
            if (useHashing)
            {
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                hashmd5.Clear();
            }
            else
                keyArray = UTF8Encoding.UTF8.GetBytes(key);

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = keyArray;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            tdes.Clear();
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }
        /// <summary>
        /// DeCrypt a string using dual encryption method. Return a DeCrypted clear string
        /// </summary>
        /// <param name="cipherString">encrypted string</param>
        /// <param name="useHashing">Did you use hashing to encrypt this data? pass true is yes</param>
        /// <returns></returns>
        public static string Decrypt(string cipherString, bool useHashing)
        {
            byte[] keyArray;
            byte[] toEncryptArray = Convert.FromBase64String(cipherString);


            // Get the key from config file
            string key = "CPanelManager";// _Configure.GetValue<string>("SecurityKey");
            //System.Windows.Forms.MessageBox.Show(key);

            if (useHashing)
            {
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                hashmd5.Clear();
            }
            else
                keyArray = UTF8Encoding.UTF8.GetBytes(key);

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = keyArray;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            tdes.Clear();
            return UTF8Encoding.UTF8.GetString(resultArray);
        }

        public static DataTable GetDataTableFromObjects(object[] objects)
        {
            if (objects != null && objects.Length > 0)
            {
                Type t = objects[0].GetType();

                DataTable dt = new DataTable(t.Name);

                foreach (PropertyInfo pi in t.GetProperties())
                {
                    dt.Columns.Add(new DataColumn(pi.Name));
                }

                foreach (var o in objects)
                {
                    DataRow dr = dt.NewRow();

                    foreach (DataColumn dc in dt.Columns)
                    {
                        dr[dc.ColumnName] = o.GetType().GetProperty(dc.ColumnName).GetValue(o, null);
                    }

                    dt.Rows.Add(dr);
                }

                return dt;
            }
            return null;
        }
    }
}
