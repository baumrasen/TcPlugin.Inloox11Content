using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

using InLoox.PM.Domain.Model.Aggregates.Api;
using Simple.OData.Client;

namespace TcPlugins.HelperInloox11
{
    public static class InlooxDefaults
    {

        #region Constants
        
        private static bool _gotmainsettings = false;
        private static bool _getting_mainsettings = false;
        private static int _callMainCounter = 0;

        private static string _authtoken = "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyy dummy";
        private static Uri _odataClientUri = new Uri("https://inloox.domain.com/odata/");
        private static string _odatapathstring = "";
        private static string _endpointstring = "";
        private static string _emailaddress = "";
        private static string _personalAccessTokenPathString = "login/Manage/PersonalAccessToken";

        private static ODataClient _client = null;

        private static string _defaultPrjRegExpr = "((?<drive>[\\w])|(?<unc>[-_\\d\\w]+))(:|\\\\data|\\\\data\\\\subfolder|:\\\\subfolder)\\\\(?<division>[-\\w]+)\\\\projects\\\\(?<projectnumber>\\d\\d\\d\\d_\\d\\d\\d\\d\\d)";
        private static Regex _prjNrRegExprString = new Regex(_defaultPrjRegExpr);
        private static string _defaultPrjNrTestString = "s:\\subfolder\\division\\projects\\2024_11171\\";

        private static bool _gotauthtoken = false;
        private static bool _keepitopen = true;

        private static TcContentInlooxProjectList _tcipl = null;

        private static int directoryLevel = 0;
        private static bool EnableDebug = false;
        
        #endregion Constants

        #region Properties

        public static void Main(string caller)
        {
            //Console.WriteLine("This is a static method ");
            _callMainCounter++;

            // workaround because some trouble, if set keepdialog open on start application
            if (_callMainCounter > 0 && !_gotmainsettings)
            {
                Console.WriteLine(String.Format("_callMainCounter > 0 - main counter: {0} - {1}", _callMainCounter, caller));
                // don't use async, because you will get trouble with getting the properties
                Thread.Sleep(200);
                //await Task.Delay(200);
            }

            // workaround because some trouble, if set keepdialog open on start application
            while (_getting_mainsettings)
            {
                Console.WriteLine(String.Format("_getting_mainsettings is true - main counter: {0} - {1}", _callMainCounter, caller));
                //await Task.Delay(50);
                // don't use async, because you will get trouble with getting the properties
                Thread.Sleep(50);
            }

            if (!_gotmainsettings && !_getting_mainsettings)
            {

                Console.WriteLine(String.Format("DO NOW _getting_mainsettings - main counter: {0} - {1}", _callMainCounter, caller));
                _getting_mainsettings = true;
                // first read saved settings on initialisation!!!
                _emailaddress = AppSettingsManager.GetValueString("EmailAddress11", @"m.mustermann@domain.com");
                _prjNrRegExprString = new Regex(AppSettingsManager.GetValueString("PrjNrRegExprString", ""));
                _authtoken = AppSettingsManager.GetValueString("AuthToken11", "12345678");
                _odataClientUri = new Uri(AppSettingsManager.GetValueString("Inloox11Url", "https://inloox.domain.com/odata/"));
                _endpointstring = AppSettingsManager.GetValueString("EndPoint11String", "https://inloox.domain.com");
                _odatapathstring = AppSettingsManager.GetValueString("OdataPath11String", "/api/v1/odata/");
                _keepitopen = AppSettingsManager.GetValueBool("KeepDialogOpen", true);

                //_defaultPrjRegExpr = AppSettingsManager.GetValueString("defaultPrjRegExpr", _defaultPrjRegExpr);
                _defaultPrjNrTestString = AppSettingsManager.GetValueString("defaultPrjNrTestString", _defaultPrjNrTestString);

                _gotmainsettings = true;
                _getting_mainsettings = false;
            }
            
        }


        public static TcContentInlooxProjectList TcCIPL
        {
            get
            {
                if (_tcipl == null)
                {
                    throw new Exception("OdataClient should be defined before use TcCIP!");
                }
                return _tcipl;
            }
        }

        public static ODataClient odc
        {
            get
            {
                if (_client == null)
                {
                    throw new Exception("ODataClient should be defined first!");
                }
                return _client;
            }
        }

        //public static ILogger logger
        //{
        //    get
        //    {
        //        return _logger;
        //    }
        //    set
        //    {
        //        _logger = value;
        //    }
        //}

        public static int DirectoryLevel
        {
            get
            {
                return directoryLevel;
            }
            set
            {
                directoryLevel = value;
            }
        }

        public static string DefaultPrjRegExpr
        {
            get
            {
                return _defaultPrjRegExpr;
            }
        }

        public static string DefaultPrjNrTestString
        {
            get
            {
                return _defaultPrjNrTestString;
            }
            set
            {
                _defaultPrjNrTestString = value;
                AppSettingsManager.SetValueString("defaultPrjNrTestString", value);
            }
        }

        public static string PrjNrRegExprString
        {
            get
            {
                if (!_gotmainsettings)
                    Main("get PrjNrRegExprString");

                return _prjNrRegExprString.ToString();
            }
        }

        public static string AuthToken
        {
            get
            {
                if (!_gotmainsettings)
                    Main("get AuthToken");

                return _authtoken;
            }
            set
            {
                if (!_gotmainsettings)
                    Main("set AuthToken");
                _authtoken = value;
            }
        }
        public static string EmailAddress
        {
            get
            {
                if (!_gotmainsettings)
                    Main("get EmailAddress");

                return _emailaddress;
            }
            set
            {
                if (!_gotmainsettings)
                    Main("set EmailAddress");
                _emailaddress = value;
            }
        }

        public static string EndPointString
        {
            get
            {
                return _endpointstring;
            }
            set
            {
                _endpointstring = value;
            }
        }
        public static string OdataPathString
        {
            get
            {
                if (!_gotmainsettings)
                    Main("get OdataPathString");

                return _odatapathstring;
            }
            set
            {
                if (!_gotmainsettings)
                    Main("set OdataPathString");
                _odatapathstring = value;
            }
        }

        public static string PersonalAccessTokenPathString
        {
            get
            {
                return _personalAccessTokenPathString;
            }
        }
        

        public static Uri ODataClientUri
        {
            get
            {
                if (!_gotmainsettings)
                    Main("get EndPointOdata");
                // return new Uri(new Uri(_endpointstring), _odatapathstring); ;
                return _odataClientUri;
            }
        }

        public static bool KeepLoginDialogOpen
        {
            get
            {
                if (!_gotmainsettings)
                    Main("get KeepLoginDialogOpen");
                return _keepitopen;
            }
            set
            {
                if (!_gotmainsettings)
                    Main("set KeepLoginDialogOpen");

                AppSettingsManager.SetValueBool("KeepDialogOpen", value);
                _keepitopen = value;
            }
        }

        public static bool GotAuthToken
        {
            get
            {
                return _gotauthtoken;
            }
        }


        #endregion

        #region Private Methods

        
        public static bool GetDataFromPath(string path, out string drive, out string unc, out string division, out string projectnumber)
        {

            bool shwoLocalDebugInfo = true;
            if (shwoLocalDebugInfo) WriteLine(String.Format("    GetDataFromPath  START: {0}", path));

            drive = "";
            unc = "";
            division = "";
            projectnumber = "";

            if (path == null)
            {
                return false;
            }                

            // Find matches.
            MatchCollection matches = _prjNrRegExprString.Matches(path);

            //String ProjectNumber = "";

            // find first ProjectNumber in Path
            if (matches.Count == 0)
            {
                //r = path.Substring(1) + "\\";                                
                if (shwoLocalDebugInfo) WriteLine(String.Format("      GetDataFromPath (no match found) return: {0} vs {1} / {2} / {3}", drive, unc, division, projectnumber));
                return false;
            }
            else if (matches.Count >= 1)
            {
                

                foreach (Match match in matches)
                {
                    GroupCollection groups = match.Groups;
                    drive = groups["drive"].Value;
                    unc = groups["unc"].Value;
                    division = groups["division"].Value;
                    projectnumber = groups["projectnumber"].Value;
                    if (shwoLocalDebugInfo) WriteLine(String.Format("      GetDataFromPath (match found) return: {0} vs {1} / {2} / {3}", drive, unc, division, projectnumber));
                    return true;
                }

                if (shwoLocalDebugInfo) WriteLine(String.Format("      GetDataFromPath (match found - but return false) return: {0} vs {1} / {2} / {3}", drive, unc, division, projectnumber));
                return false;
                
            }
            else
            {
                //ProjectNumber = "";
                if (shwoLocalDebugInfo) WriteLine(String.Format("      GetDataFromPath (no match found UNDEFINED) return: {0} vs {1} / {2} / {3}", drive, unc, division, projectnumber));
                return false;
            }
           
        }

        private static void WriteLine(string text1)
        {
            Debug.WriteLine(text1, EnableDebug);
        }

        private static void WriteLine(string text1, bool debug)
        {
            if (debug)
                Debug.WriteLine(text1);
        }

        private static string GetJsonValueFromString(string valuename, string data)
        {

            //string AccessToken = null;
            //'Dim myRegexStrCounter As String = String.Format(""(.*)"":\s(.*),\s")
            //'Dim myRegexStrCounter As String = String.Format("""(.*)"":""(.*)""")
            //'Dim myRegexStrCounter As String = String.Format("""([\w\d-_]*)"":""([\w\d-_]*)"",""([\w\d-_]*)"":""([\w\d-_]*)"",""([\w\d-_]*)"":""([\w\d-_]*)")
            String myRegexStrCounter = String.Format(@"""([\w\d\-_,]*)"":""([\s\w\d.\-_,]*)""");
            Regex myRegexCounter = new Regex(myRegexStrCounter);

            //'Dim match As Match = myRegexCounter.Match(responseFromServer)
            MatchCollection matchC = myRegexCounter.Matches(data);

            if (matchC.Count > 0)
            {


                //'Debug.WriteLine("{0} MATCHL: {1}", Now, output)

                //'For Each mc As Capture In match.Captures
                //'    Debug.WriteLine("{0} MATCHC: {1}", Now, mc.Value)
                //'Next

                foreach (Match m in matchC)
                {


                    //' Access first Group and its value.
                    System.Text.RegularExpressions.Group name = m.Groups[1];
                    System.Text.RegularExpressions.Group value = m.Groups[2];
                    if (name.Value == valuename)
                    {


                        //AccessToken = value.Value;
                        return value.Value;
                        //goto AccessTokenIsHere;
                    }

                    //'Console.WriteLine(in_use.Value)
                    //'Console.WriteLine(free.Value)

                }
            }


            //AccessTokenIsHere:;

            return "";
        }

        #endregion Private Methods

        #region Main Functions

        public static bool SetPrjNrRegExprString(string regexpr, out string errormsg)
        {
            errormsg = "";

            try
            {
                _prjNrRegExprString = new Regex(regexpr);
                return true;
            }
            catch (Exception ex)
            {

                errormsg = ex.Message;      
                return false;
            }
        }

        public static bool SetODataClientUriString(string newUriString, string OdataPathString, out string errormsg)
        {
            errormsg = "";

            try
            {
                _odataClientUri = new Uri(new Uri(newUriString), OdataPathString);
                //_odataClientUri = new Uri(newUriString);
                return true;
            }
            catch (Exception ex)
            {

                errormsg = ex.Message;
                return false;
            }
        }


        // return authtoken
        public static bool CheckCurrentToken(out string errormsg)
        {

            Debug.WriteLine(String.Format("      InlooxDefaults - CheckCurrentToken: {0} / {1}************************", _odataClientUri, AuthToken.Left(12)));

            //_inlooxuri = ;
            //_ic = new Container(_inlooxuri);

            string action_id = "";

            errormsg = "";

            //string sUrl String = "https://inloox.domain.com/odata/../api/0/token";

            //   ServicePointManager.ServerCertificateValidationCallback = Function(
            //obj As[Object],
            //certificate As X509Certificate,
            //chain As X509Chain,
            //errors As SslPolicyErrors)(certificate.Subject.Contains(
            //     "CN=dc.domain.com")
            //     )

            //string AccessToken = null;
            //string sUrl = "https://inloox.domain.com/odata/../api/0/token";
            // https://inloox.domain.com/odata/actionview?$top=1
            string sUrl = _odataClientUri + "UserInfo/Me()";

            //sUrl = _uri + "/../api/0/token";

            // to remove SSL error on auth check
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            //WebRequest request = WebRequest.Create("https://inloox.domain.com/odata/../api/0/token");
            WebRequest request = WebRequest.Create(sUrl);

            bool CheckAccessToken = true;

            if (CheckAccessToken)
            {

                try
                {

                    // Set the Method property of the request to POST.
                    request.Method = "GET";

                    // add headers for x-api-key
                    request.Headers.Add("x-api-key", _authtoken);

                    //request.Headers["Authorization "] = "Bearer " + authtoken;

                    request.Timeout = 2000;

                    Debug.WriteLine(String.Format("      InlooxDefaults - CheckCurrentToken - Start WebResponse: {0} / {1}*********************", _odataClientUri, AuthToken.Left(12)));

                    //' Get the response.
                    WebResponse response = request.GetResponse();
                    HttpWebResponse httpresponse = (System.Net.HttpWebResponse)response;

                    //' Display the status.
                    Debug.WriteLine("StatusDescription");
                    Debug.WriteLine(httpresponse.StatusDescription);
                    Debug.WriteLine("");
                    //' Get the stream containing content returned by the server.
                    Stream dataStream = response.GetResponseStream();
                    //' Open the stream using a StreamReader for easy access.
                    StreamReader reader = new StreamReader(dataStream);
                    //' Read the content.
                    String responseFromServer = reader.ReadToEnd();
                    //' Display the content.
                    Debug.WriteLine("responseFromServer");
                    Debug.WriteLine(responseFromServer);
                    //'"access_token":\s(.*),\s
                    Debug.WriteLine("");
                    //' Clean up the streams.
                    reader.Close();
                    dataStream.Close();
                    response.Close();
             

                    //AccessTokenIsHere:;
                    action_id = GetJsonValueFromString("DisplayName", responseFromServer);


                    Debug.WriteLine(String.Format("      InlooxDefaults - SetClient - Start WebResponse: {0} / {1}********************", _odataClientUri, AuthToken.Left(12)));
                    setClient();

                    return true;

                }
                catch (Exception ex)
                {

                    //if (ex.InnerException.GetType() == typeof(System.OverflowException
                    if (ex.GetType() == typeof(WebException))
                    {
                        WebException ex2 = (WebException)ex;
                        //Stream dataStream = request.GetRequestStream();
                        string exeption_message = ex2.Message;
                        Debug.WriteLine("");
                        //' Clean up the streams.
                        //reader.Close();
                        //dataStream.Close();

                        // MsgBox(String.Format("ERROR in Odata! {0}", ex.Message), MsgBoxStyle.Critical, "Fehler in wpfMainWindow Odata")
                        Debug.WriteLine("{0} EXCEPTION in {1} Fehler:{2}", DateTime.Now, "InlooxDefaults", ex);

                        errormsg = String.Format("error: {0}", exeption_message);
                        return false;
                    }

                    Debug.WriteLine("{0} EXCEPTION in {1} Fehler:{2}", DateTime.Now, "InlooxDefaults", ex);
                    errormsg = ex.Message;
                    return false;
                }

            }



            return false;
        }
       

        private static void setClient()
        {
            //var EndPoint = new Uri(EndPointString);
            //var EndPointOdata = new Uri(EndPoint, OdataPathString);

            // Inloox 11
            var settings = new ODataClientSettings(_odataClientUri);
            settings.BeforeRequest += delegate (HttpRequestMessage message)
            {
                message.Headers.Add("x-api-key",_authtoken);
            };
            _client = new ODataClient(settings);

            if (_tcipl == null)
            {
                _gotauthtoken = true;
                _tcipl = new TcContentInlooxProjectList();
            }
            
        }

        public static async Task<IEnumerable<ApiProject>> GetFirst100Projects()
        {
            // this will only return the first 100 projects
            // for paging and filtering see sample 3
            if (odc == null)
                throw new InvalidOperationException("Initialize client first");

            var pl = await odc.For<ApiProject>("Project").FindEntriesAsync();

            return pl;
        }

        public static async Task<IEnumerable<ApiProject>> GetNotArchivedApiProjects()
        {
            if (odc == null)
                throw new InvalidOperationException("Initialize client first");

            var annotations = new ODataFeedAnnotations();
            var pl = (await odc.For<ApiProject>("Project")
                .Filter(p =>
                        (p.IsArchived == false
                        &&
                        p.IsRecycled == false
                        &&
                        p.StatusLabel != "99_Archiviert"
                        &&
                        p.StatusLabel != "999_Archiviert_in_Inloox")
                )
                .FindEntriesAsync(annotations))
                .ToList();

            while (annotations.NextPageLink != null)
            {
                pl.AddRange(await odc
                    .For<ApiProject>("Project")
                    .FindEntriesAsync(annotations.NextPageLink, annotations));

                // loadedFunc($"Loaded {projects.Count()} entries");
            }

            return pl;
        }

        
        public static async Task<ApiProject> GetApiProjectFromNumberAsync(String ProjectNumber)
        {
            if (odc == null)
                throw new InvalidOperationException("Initialize client first");

            Debug.WriteLine(String.Format("============= GetApiProjectFromNumberAsync ====== for {0}", ProjectNumber));

            //var annotations = new ODataFeedAnnotations();
            var pl = (await odc.For<ApiProject>("Project").
                Filter(p =>
                    p.Number == ProjectNumber
                )
                .FindEntriesAsync());

            var c = pl.Count();

            if (c == 1)
                return pl.First();
            else if (c == 0)
                return null;
            else
                throw new InvalidOperationException("More than one match for the project number in GetApiProjectFromNumberAsync");

        }

        public static async Task<CustomApiDynamicProject> GetCustomApiDynamikProjectFromNumberAsync(String ProjectNumber)
        {
            if (odc == null)
                throw new InvalidOperationException("Initialize client first");

            Debug.WriteLine(String.Format("============= GetCustomApiDynamikProjectFromNumberAsync ====== for {0}", ProjectNumber));

            var annotations = new ODataFeedAnnotations();
            var pl = (await odc.For<CustomApiDynamicProject>("DynamicProject").
                Filter(p =>
                    p.Project_Number == ProjectNumber
                )
                .FindEntriesAsync());

            var c = pl.Count();

            if (c == 1)
                return pl.First();
            else if (c == 0)
                return null;
            else
                throw new InvalidOperationException(string.Format("Duplicate project number {0} found", ProjectNumber));

            //return null;
        }

        public static async Task<IEnumerable<CustomApiDynamicProject>> GetProjectsFromNumberAsync(String ProjectNumber)
        {
            // this will only return the first 100 projects
            // for paging and filtering see sample 3
            if (odc == null)
                throw new InvalidOperationException("Initialize client first");

            Debug.WriteLine(String.Format("============= GetProjectsFromNumber ====== for {0}", ProjectNumber));

            var annotations = new ODataFeedAnnotations();
            var pl = (await odc.For<CustomApiDynamicProject>("DynamicProject").
                Filter(p =>
                    p.Project_Number == ProjectNumber
                )
                .FindEntriesAsync(annotations)).ToList();

            while (annotations.NextPageLink != null)
            {
                pl.AddRange(await odc
                    .For<CustomApiDynamicProject>("DynamicProject")
                    .FindEntriesAsync(annotations.NextPageLink, annotations));

                // loadedFunc($"Loaded {projects.Count()} entries");
            }

            return pl;
        }

        public static async Task<IEnumerable<ApiDynamicProject>> GetProjectFromDivision(String DivisionName)
        {
            // this will only return the first 100 projects
            // for paging and filtering see sample 3
            if (odc == null)
                throw new InvalidOperationException("Initialize client first");

            var annotations = new ODataFeedAnnotations();
            var pl = (await odc.For<ApiDynamicProject>("DynamicProject").
                Filter(p =>
                    p.Project_DivisionName == DivisionName
                )
                .FindEntriesAsync(annotations)).ToList();

            while (annotations.NextPageLink != null)
            {
                pl.AddRange(await odc
                    .For<ApiDynamicProject>("DynamicProject")
                    .FindEntriesAsync(annotations.NextPageLink, annotations));

                // loadedFunc($"Loaded {projects.Count()} entries");
            }

            return pl;
        }

        public static async Task<IEnumerable<ApiDynamicProject>> GetProjectFavorites()
        {
            // this will only return the first 100 projects
            // for paging and filtering see sample 3
            if (odc == null)
                throw new InvalidOperationException("Initialize client first");

            var annotations = new ODataFeedAnnotations();
            var pl = (await odc.For<ApiDynamicProject>("DynamicProject").
                Filter(p =>
                    p.Project_IsFavorite == true
                )
                .FindEntriesAsync(annotations)).ToList();

            while (annotations.NextPageLink != null)
            {
                pl.AddRange(await odc
                    .For<ApiDynamicProject>("DynamicProject")
                    .FindEntriesAsync(annotations.NextPageLink, annotations));

                // loadedFunc($"Loaded {projects.Count()} entries");
            }

            return pl;
        }

        public static async Task<ApiAccountInfo> GetAccountInfo()
        {
            if (odc == null)
                throw new InvalidOperationException("Initialize client first");

            return await odc.For<ApiAccountInfo>("AccountInfo").FindEntryAsync();
        }

        public static async Task<IEnumerable<ApiDynamicTimeEntry>> GetAllTimeEntriesForMonthMod(DateTime month)
        {
            if (odc == null)
                throw new InvalidOperationException("Initialize client first");

            var filterStart = new DateTime(month.Year, month.Month, 1);
            var filterEnd = new DateTime(month.Year, month.Month, 1).AddMonths(1);

            var annotations = new ODataFeedAnnotations();
            var timeentries = (await odc.For<ApiDynamicTimeEntry>("DynamicTimeEntry")
                .Filter(k =>
                    k.TimeEntry_StartDateTime > filterStart &&
                    k.TimeEntry_EndDateTime < filterEnd
                )
                .FindEntriesAsync(annotations)).ToList();

            while (annotations.NextPageLink != null)
            {
                timeentries.AddRange(await odc
                    .For<ApiDynamicTimeEntry>("DynamicTimeEntry")
                    .FindEntriesAsync(annotations.NextPageLink, annotations));

                // loadedFunc($"Loaded {timeentries.Count()} entries");
            }

            return timeentries;
        }

        public static async Task<List<ApiDynamicTimeEntry>> GetAllTimeEntriesForMonth(DateTime month, Action<string> loadedFunc)
        {
            if (odc == null)
                throw new InvalidOperationException("Initialize client first");

            var filterStart = new DateTime(month.Year, month.Month, 1);
            var filterEnd = new DateTime(month.Year, month.Month, 1).AddMonths(1);

            var annotations = new ODataFeedAnnotations();
            var timeentries = (await odc.For<ApiDynamicTimeEntry>("DynamicTimeEntry")
                .Filter(k =>
                    k.TimeEntry_StartDateTime > filterStart &&
                    k.TimeEntry_EndDateTime < filterEnd
                )
                .FindEntriesAsync(annotations)).ToList();

            while (annotations.NextPageLink != null)
            {
                timeentries.AddRange(await odc
                    .For<ApiDynamicTimeEntry>("DynamicTimeEntry")
                    .FindEntriesAsync(annotations.NextPageLink, annotations));

                loadedFunc($"Loaded {timeentries.Count()} entries");
            }

            return timeentries;
        }


        public static async Task<ApiTimeEntry> GetTimeEntryFromId(Guid timeentryId)
        {

            if (odc == null)
                throw new InvalidOperationException("Initialize client first");

            var te = await odc.For<ApiTimeEntry>("TimeEntry").Key(timeentryId).FindEntriesAsync();

            return te.First();
        }


        public static void WriteLineHelper(string text1)
        {
            WriteLineHelper(text1, EnableDebug);
        }

        public static void WriteLineHelper(string text1, bool debug)
        {
            if (debug)
            {
                Console.WriteLine(text1);
            }
                
        }

        #endregion Main Functions


    }
}
