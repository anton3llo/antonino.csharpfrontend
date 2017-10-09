using HendoHealth.Library.Entity;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace HendoHealth.Library
{
    public static class UnixTime
    {

        private static readonly DateTime UTCUnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

        public static long ToUnixTime(DateTime time)
        {
            return (long)(time - UTCUnixEpoch).TotalSeconds;
        }


        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "unix", Justification = "UNIX is a domain term")]
        public static DateTime FromUnixTime(long unixTime)
        {
            if (unixTime < 0)
                throw new ArgumentOutOfRangeException(nameof(unixTime));

            return UTCUnixEpoch.AddSeconds(unixTime);
        }
    }
    public class Connect
    {
        public string client_id = "";
        public string client_secret = "";
        public string redirect_uri = "";
        public string sc = "";
        public string sv_OpenApiBP = "";
        public string sv_OpenApiWeight = "";
        public string sv_OpenApiBG = "";
        public string sv_OpenApiSpO2 = "";
        public string sv_OpenApiActivity = "";
        public string sv_OpenApiSleep = "";
        public string sv_OpenApiUserInfo = "";
        public string access_token = "";
        public string temporaryCode = "";
        public string locale = "default";
        public ApiErrorEntity Error;

        private string response_type_code = "code";
        private string response_type_refresh_token = "refresh_token";
        private string grant_type_authorization_code = "authorization_code";
        private string APIName_BP = "OpenApiBP";
        private string APIName_Weight = "OpenApiWeight";
        private string APIName_BG = "OpenApiBG";
        private string APIName_BO = "OpenApiSpO2";
        private string APIName_UI = "OpenApiUserInfo";
        private string APIName_AA = "OpenApiActivity";
        private string APIName_SL = "OpenApiSleep";

        private string DefaultUserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; SV1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)";

        // url per l'accesso developer
        public string url_authorization = "https://oauthuser.ihealthlabs.eu/OpenApiV2/OAuthv2/userauthorization/";
        private string url_bp_data = "https://openapi.ihealthlabs.eu/openapiv2/user/{0}/bp.json/";
        private string url_bp_data_xml = "https://api.ihealthlabs.com:8443/openapiv2/user/{0}/bp.xml/";
        private string url_weight_data = "https://openapi.ihealthlabs.eu/openapiv2/user/{0}/weight.json/";
        private string url_weight_data_xml = "https://api.ihealthlabs.com:8443/openapiv2/user/{0}/weight.xml/";
        private string url_bg_data = "https://openapi.ihealthlabs.eu/openapiv2/user/{0}/glucose.json/";
        private string url_bg_data_xml = "https://api.ihealthlabs.com:8443/openapiv2/user/{0}/glucose.xml/";
        private string url_bo_data = "https://openapi.ihealthlabs.eu/openapiv2/user/{0}/spo2.json/";
        private string url_bo_data_xml = "https://api.ihealthlabs.com:8443/openapiv2/user/{0}/spo2.xml/";

        private string url_bp_data_client = "https://api.ihealthlabs.com:8443/openapiv2/application/bp.json/";
        private string url_weight_data_client = "https://api.ihealthlabs.com:8443/openapiv2/application/weight.json/";
        private string url_bg_data_client = "https://api.ihealthlabs.com:8443/openapiv2/application/glucose.json/";
        private string url_bo_data_client = "https://api.ihealthlabs.com:8443/openapiv2/application/spo2.json/";
        

        /* url per accesso sandbox per test
        public string url_authorization = "http://sandboxapi.ihealthlabs.com/OpenApiV2/OAuthv2/userauthorization/";
        private string url_bp_data = "http://sandboxapi.ihealthlabs.com/openapiv2/user/{0}/bp.json";
        private string url_bp_data_xml = "https://api.ihealthlabs.com:8443/openapiv2/user/{0}/bp.xml/";
        private string url_weight_data = "http://sandboxapi.ihealthlabs.com/openapiv2/user/{0}/weight.json/";
        private string url_weight_data_xml = "https://api.ihealthlabs.com:8443/openapiv2/user/{0}/weight.xml/";
        private string url_bg_data = "http://sandboxapi.ihealthlabs.com/openapiv2/user/{0}/glucose.json/";
        private string url_bg_data_xml = "https://api.ihealthlabs.com:8443/openapiv2/user/{0}/glucose.xml/";
        private string url_bo_data = "http://sandboxapi.ihealthlabs.com/openapiv2/user/{0}/spo2.json/";
        private string url_bo_data_xml = "https://api.ihealthlabs.com:8443/openapiv2/user/{0}/spo2.xml/";

        private string url_bp_data_client = "https://api.ihealthlabs.com:8443/openapiv2/application/bp.json/";
        private string url_weight_data_client = "https://api.ihealthlabs.com:8443/openapiv2/application/weight.json/";
        private string url_bg_data_client = "https://api.ihealthlabs.com:8443/openapiv2/application/glucose.json/";
        private string url_bo_data_client = "https://api.ihealthlabs.com:8443/openapiv2/application/spo2.json/";*/




        public Connect()
        {

        }
        public void GetCode()
        {
            var url = url_authorization
                + "?client_id=" + client_id
                + "&response_type=" + response_type_code
                + "&redirect_uri=" + HttpUtility.UrlEncode(redirect_uri)
                + "&APIName=" + APIName_BO + " " + APIName_BG + " " + APIName_BP + " " + APIName_Weight;
            HttpContext.Current.Response.Redirect(url);
        }
        public bool GetAccessToken(string code, string client_para, HttpContext httpContext)
        {
            var url = url_authorization
            + "?client_id=" + client_id
            + "&client_secret=" + client_secret
            + "&client_para=" + client_para
            + "&code=" + code
            + "&grant_type=" + grant_type_authorization_code
            + "&redirect_uri=" + HttpUtility.UrlEncode(redirect_uri);

            var ResultString = this.HttpGet(url);

            if (ResultString.StartsWith("{\"Error\":"))
            {
                httpContext.Response.Write(url);
                httpContext.Response.Write("<br/>");
                this.Error = JsonDeserializ<ApiErrorEntity>(ResultString);
                return false;
            }
            else
            {
                httpContext.Response.Write(url);
                httpContext.Response.Write("<br/>");
                httpContext.Response.Write(ResultString);
                AccessTokenEntity accessToken = this.JsonDeserializ<AccessTokenEntity>(ResultString);
                httpContext.Session["token"] = accessToken;
                return true;
            }
        }
        public bool RefreshAccessToken(string code, string client_para, HttpContext httpContext, out AccessTokenEntity aAccessTokenEntity)
        {
            if (((AccessTokenEntity)httpContext.Session["token"]) != null)
            {
                string url = url_authorization
                    + "?client_id=" + client_id
                    + "&client_secret=" + client_secret
                    + "&client_para=" + client_para
                    + "&refresh_token=" + ((AccessTokenEntity)httpContext.Session["token"]).RefreshToken
                    + "&response_type=" + response_type_refresh_token
                    + "&redirect_uri=" + HttpUtility.UrlEncode(redirect_uri);

                string ResultString = HttpGet(url);

                if (ResultString.StartsWith("{\"Error\":"))
                {
                    httpContext.Response.Write(url);
                    httpContext.Response.Write("<br/>");
                    this.Error = JsonDeserializ<ApiErrorEntity>(ResultString);
                    aAccessTokenEntity = null;
                    return false;
                }
                else
                {
                    httpContext.Response.Write(url);
                    httpContext.Response.Write("<br/>");
                    httpContext.Response.Write(ResultString);
                    AccessTokenEntity accessToken = this.JsonDeserializ<AccessTokenEntity>(ResultString);
                    httpContext.Session["token"] = accessToken;
                    aAccessTokenEntity = accessToken;
                    return true;
                }
            }
            else
            {
                this.Error = new ApiErrorEntity("Do not has AccessToken.", "Do not has AccessToken.", 0000);
                aAccessTokenEntity = null;
                return false;
            }
        }
        public DownloadBPDataResultEntity DownloadBPData(HttpContext httpContext, int? pageIndex, DateTime? startTime, DateTime? endTime, string aBPUnit)
        {
            if (httpContext.Session["token"] != null)
            {
                string url = string.Format(url_bp_data, ((AccessTokenEntity)httpContext.Session["token"]).UserID)
                    + "?access_token=" + ((AccessTokenEntity)httpContext.Session["token"]).AccessToken
                    + "&client_id=" + client_id
                    + "&client_secret=" + client_secret
                    + "&redirect_uri=" + HttpUtility.UrlEncode(redirect_uri)
                    + "&sc=" + sc
                    + "&sv=" + sv_OpenApiBP
                    + "&locale=" + aBPUnit;

                if (pageIndex.HasValue)
                    url += "&page_index=" + pageIndex.Value;

                if (startTime.HasValue)
                    url += "&start_time=" + UnixTime.ToUnixTime(startTime.Value).ToString();

                if (endTime.HasValue)
                    url += "&end_time=" + endTime.Value.ToString();

                string ResultString = this.HttpGet(url);

                if (ResultString.StartsWith("{\"Error\":"))
                {
                    Error = JsonDeserializ<ApiErrorEntity>(ResultString);
                    return null;
                }
                else
                {
                    return this.JsonDeserializ<DownloadBPDataResultEntity>(ResultString);
                }
            }
            else
            {
                return null;
            }
        }
        /*public DownloadBPDataResultEntity DownloadClientBPData(HttpContext httpContext, int? pageIndex, DateTime? startTime, DateTime? endTime, string aBPUnit)
        {
            if (httpContext.Session["token"] != null)
            {
                string url = "";

                url = url_bp_data_client
              + "?access_token=" + ((AccessTokenEntity)httpContext.Session["token"]).AccessToken
              + "&client_id=" + client_id
              + "&client_secret=" + client_secret
              + "&redirect_uri=" + HttpUtility.UrlEncode(redirect_uri)
              + "&sc=" + sc
              + "&sv=" + sv_OpenApiBP
              + "&locale=" + aBPUnit;

                if (pageIndex.HasValue)
                    url += "&page_index=" + pageIndex.Value;

                if (startTime.HasValue)
                    url += "&start_time=" + UnixTime.ToUnixTime(startTime.Value).ToString();

                if (endTime.HasValue)
                    url += "&end_time=" + UnixTime.ToUnixTime(endTime.Value).ToString();

                string ResultString = this.HttpGet(url);

                if (ResultString.StartsWith("{\"Error\":"))
                {
                    Error = JsonDeserializ<ApiErrorEntity>(ResultString);
                    return null;
                }
                else
                {
                    return this.JsonDeserializ<DownloadBPDataResultEntity>(ResultString);
                }
            }
            else
            {
                return null;
            }
        }*/
        public DownloadWeightDataResultEntity DownloadWeightData(HttpContext httpContext, int? pageIndex, DateTime? startTime, DateTime? endTime, string aWeightUnit)
        {
            if (httpContext.Session["token"] != null)
            {
                string url = string.Format(url_weight_data, ((AccessTokenEntity)httpContext.Session["token"]).UserID)
                    + "?access_token=" + ((AccessTokenEntity)httpContext.Session["token"]).AccessToken
                    + "&client_id=" + client_id
                    + "&client_secret=" + client_secret
                    + "&redirect_uri=" + HttpUtility.UrlEncode(redirect_uri)
                    + "&sc=" + sc
                    + "&sv=" + sv_OpenApiWeight
                    + "&locale=" + aWeightUnit;

                if (pageIndex.HasValue)
                    url += "&page_index=" + pageIndex.Value;

                if (startTime.HasValue)
                    url += "&start_time=" + UnixTime.ToUnixTime(startTime.Value).ToString();

                if (endTime.HasValue)
                    url += "&end_time=" + UnixTime.ToUnixTime(endTime.Value).ToString();

                string ResultString = this.HttpGet(url);
                if (ResultString.StartsWith("{\"Error\":"))
                {
                    Error = JsonDeserializ<ApiErrorEntity>(ResultString);
                    return null;
                }
                else
                {
                    return this.JsonDeserializ<DownloadWeightDataResultEntity>(ResultString);
                }
            }
            else
            {
                return null;
            }
        }
        /*public DownloadWeightDataResultEntity DownloadClientWeightData(HttpContext httpContext, int? pageIndex, DateTime? startTime, DateTime? endTime, string aWeightUnit)
        {
            if (httpContext.Session["token"] != null)
            {
                string url = "";
                url = url_weight_data_client
              + "?access_token=" + ((AccessTokenEntity)httpContext.Session["token"]).AccessToken
              + "&client_id=" + client_id
              + "&client_secret=" + client_secret
              + "&redirect_uri=" + HttpUtility.UrlEncode(redirect_uri)
              + "&sc=" + sc
              + "&sv=" + sv_OpenApiWeight
              + "&locale=" + aWeightUnit;

                if (pageIndex.HasValue)
                    url += "&page_index=" + pageIndex.Value;

                if (startTime.HasValue)
                    url += "&start_time=" + UnixTime.ToUnixTime(startTime.Value).ToString();

                if (endTime.HasValue)
                    url += "&end_time=" + UnixTime.ToUnixTime(endTime.Value).ToString();

                string ResultString = this.HttpGet(url);

                if (ResultString.StartsWith("{\"Error\":"))
                {
                    Error = JsonDeserializ<ApiErrorEntity>(ResultString);
                    return null;
                }
                else
                {
                    httpContext.Response.Write(ResultString);
                    return this.JsonDeserializ<DownloadWeightDataResultEntity>(ResultString);
                }

            }
            else
            {
                return null;
            }
        }*/
        public DownloadBGDataResultEntity DownloadBGData(HttpContext httpContext, int? pageIndex, DateTime? startTime, DateTime? endTime, string aBGUnit)
        {
            if (httpContext.Session["token"] != null)
            {
                string url = string.Format(url_bg_data, ((AccessTokenEntity)httpContext.Session["token"]).UserID)
                    + "?access_token=" + ((AccessTokenEntity)httpContext.Session["token"]).AccessToken
                    + "&client_id=" + client_id
                    + "&client_secret=" + client_secret
                    + "&redirect_uri=" + HttpUtility.UrlEncode(redirect_uri)
                    + "&sc=" + sc
                    + "&sv=" + sv_OpenApiBG
                    + "&locale=" + aBGUnit;
                if (pageIndex.HasValue)
                    url += "&page_index=" + pageIndex.Value;

                if (startTime.HasValue)
                    url += "&start_time=" + UnixTime.ToUnixTime(startTime.Value).ToString();

                if (endTime.HasValue)
                    url += "&end_time=" + UnixTime.ToUnixTime(endTime.Value).ToString();

                string ResultString = this.HttpGet(url);
                if (ResultString.StartsWith("{\"Error\":"))
                {
                    Error = JsonDeserializ<ApiErrorEntity>(ResultString);
                    return null;
                }
                else
                {
                    return this.JsonDeserializ<DownloadBGDataResultEntity>(ResultString);
                }
            }
            else
            {
                return null;
            }
        }
        /*public DownloadBGDataResultEntity DownloadClientBGData(HttpContext httpContext, int? pageIndex, DateTime? startTime, DateTime? endTime, string aBGUnit)
        {
            if (httpContext.Session["token"] != null)
            {
                string
                    url = url_bg_data_client
                  + "?access_token=" + ((AccessTokenEntity)httpContext.Session["token"]).AccessToken
                  + "&client_id=" + client_id
                  + "&client_secret=" + client_secret
                  + "&redirect_uri=" + HttpUtility.UrlEncode(redirect_uri)
                  + "&sc=" + sc
                  + "&sv=" + sv_OpenApiBG
                  + "&locale=" + aBGUnit;

                if (pageIndex.HasValue)
                    url += "&page_index=" + pageIndex.Value;

                if (startTime.HasValue)
                    url += "&start_time=" + UnixTime.ToUnixTime(startTime.Value).ToString();

                if (endTime.HasValue)
                    url += "&end_time=" + UnixTime.ToUnixTime(endTime.Value).ToString();

                string ResultString = this.HttpGet(url);

                if (ResultString.StartsWith("{\"Error\":"))
                {
                    Error = JsonDeserializ<ApiErrorEntity>(ResultString);
                    return null;
                }
                else
                {
                    return this.JsonDeserializ<DownloadBGDataResultEntity>(ResultString);
                }
            }
            else
            {
                Error = new ApiErrorEntity("", "Deny", -1);
                return null;
            }
        }*/
        public DownloadBODataResultEntity DownloadBOData(HttpContext httpContext, int? pageIndex, DateTime? startTime, DateTime? endTime, string aBOUnit)
        {
            if (httpContext.Session["token"] != null)
            {
                string url = string.Format(url_bo_data, ((AccessTokenEntity)httpContext.Session["token"]).UserID)
                    + "?access_token=" + ((AccessTokenEntity)httpContext.Session["token"]).AccessToken
                    + "&client_id=" + client_id
                    + "&client_secret=" + client_secret
                    + "&redirect_uri=" + HttpUtility.UrlEncode(redirect_uri)
                    + "&sc=" + sc
                    + "&sv=" + sv_OpenApiSpO2
                    + "&locale=" + aBOUnit;

                if (pageIndex.HasValue)
                    url += "&page_index=" + pageIndex.Value;

                if (startTime.HasValue)
                    url += "&start_time=" + UnixTime.ToUnixTime(startTime.Value).ToString();

                if (endTime.HasValue)
                    url += "&end_time=" + UnixTime.ToUnixTime(endTime.Value).ToString();

                string ResultString = this.HttpGet(url);
                if (ResultString.StartsWith("{\"Error\":"))
                {
                    Error = JsonDeserializ<ApiErrorEntity>(ResultString);
                    return null;
                }
                else
                {
                    return this.JsonDeserializ<DownloadBODataResultEntity>(ResultString);
                }
            }
            else
            {
                return null;
            }
        }
        /*public DownloadBODataResultEntity DownloadClientBOData(HttpContext httpContext, int? pageIndex, DateTime? startTime, DateTime? endTime, string aBOUnit)
        {
            if (httpContext.Session["token"] != null)
            {
                string url = url_bo_data_client
                    + "?access_token=" + ((AccessTokenEntity)httpContext.Session["token"]).AccessToken
                    + "&client_id=" + client_id
                    + "&client_secret=" + client_secret
                    + "&redirect_uri=" + HttpUtility.UrlEncode(redirect_uri)
                    + "&sc=" + sc
                    + "&sv=" + sv_OpenApiSpO2
                    + "&locale=" + aBOUnit;

                if (pageIndex.HasValue)
                    url += "&page_index=" + pageIndex.Value;

                if (startTime.HasValue)
                    url += "&start_time=" + UnixTime.ToUnixTime(startTime.Value).ToString();

                if (endTime.HasValue)
                    url += "&end_time=" + UnixTime.ToUnixTime(endTime.Value).ToString();

                string ResultString = this.HttpGet(url);
                if (ResultString.StartsWith("{\"Error\":"))
                {
                    Error = JsonDeserializ<ApiErrorEntity>(ResultString);
                    return null;
                }
                else
                {
                    return this.JsonDeserializ<DownloadBODataResultEntity>(ResultString);
                }
            }
            else
            {
                return null;
            }
        }*/
        public T JsonDeserializ<T>(string Json)
        {
            using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(Json)))
            {
                DataContractJsonSerializer serializer1 = new DataContractJsonSerializer(typeof(T));
                T p1 = (T)serializer1.ReadObject(ms);
                return (T)p1;
            }
        }
        public string HttpGet(string url)
        {
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            request.Method = "GET";
            request.UserAgent = DefaultUserAgent;

            string ResultString = "";
            HttpWebResponse httpresponse = request.GetResponse() as HttpWebResponse;
            using (StreamReader reader = new StreamReader(httpresponse.GetResponseStream(), System.Text.Encoding.UTF8))
            {
                ResultString = reader.ReadToEnd();
            }
            return ResultString;
        }
        private string HttpPost(string url, string str)
        {
            Encoding encoding = Encoding.UTF8;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "post";
            request.Accept = "text/html, application/xhtml+xml, */*";
            request.ContentType = "application/x-www-form-urlencoded";

            string tempJson = str;
            byte[] buffer = Encoding.UTF8.GetBytes(tempJson);
            request.ContentLength = buffer.Length;
            request.GetRequestStream().Write(buffer, 0, buffer.Length);
            string ResultString = "";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            using (StreamReader reader = new StreamReader(response.GetResponseStream(), System.Text.Encoding.UTF8))
            {
                ResultString = reader.ReadToEnd();
            }
            return ResultString;
        }
    }
}