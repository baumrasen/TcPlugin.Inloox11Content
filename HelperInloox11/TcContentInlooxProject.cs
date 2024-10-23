using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Text;
using System.Net;
using System.Diagnostics;
using System.Threading;

using System.Threading.Tasks;

namespace TcPlugins.HelperInloox11
{

    [Serializable]
    [ComVisible(true)]

    public class TcContentInlooxProject : ISerializable
    {

        private bool _gotData = false;
        private bool _gettingDataNow = false;

        private CustomApiDynamicProject _customApiDynamicProject = null;

        private string _number = "";        
        private string _clientname = "";
        private string _projectname = "";
        private string _divisionname = "";
        private string _statuslabel = "";
        private string _projectnumberandname = "";
        private string _favorite = "";
        private string _customfield01 = "";
        private string _customfield02 = "";
        private string _customfield03 = "";
        private string _documentfolder = "";
        
        private const String numberField = "Number";  // For serialization
        private const String clientnameField = "ClientName";  // For serialization
        private const String projectnameField = "ProjectName";  // For serialization
        private const String divisionnameField = "DivisionName";  // For serialization
        private const String statusnameField = "StatusLabel";  // For serialization
        private const String projectnumberandnameField = "ProjectNumberAndName";  // For serialization
        private const String favoriteField = "Favorite";  // For serialization
        private const String customfield01Field = "CustomField01";  // For serialization
        private const String customfield02Field = "CustomField02";  // For serialization
        private const String customfield03Field = "CustomField03";  // For serialization
        private const String documentfolderField = "DocumentFolder";  // For serialization

        #region constructor

        public TcContentInlooxProject(string _ProjectNumber)
        {            

            Debug.WriteLine(String.Format("      NEW TcContentInlooxProject from temp ProjectNumber: {0}", _ProjectNumber));

            _number = _ProjectNumber;
            SetPendingAttributes();
            GetAttributesAsync();
        }

        #endregion

        #region readonly_propberties

        public bool GotData
        {
            get
            {
                return _gotData;
            }
            set
            {
                _gotData = value; 
            }   

        }

        public string Number
        {
            get
            {
                return _number;
            }
        }

        public string ClientName
        {
            get
            {
                WaitForGotData("ClientName");

                return _clientname;
            }
        }

        public string ProjectName
        {
            get
            {
                WaitForGotData("ProjectName");

                return _projectname;
            }
        }

        public string DivisionName
        {
            get
            {
                WaitForGotData("DivisionName");

                return _divisionname;
            }
        }

        public string StatusLabel
        {
            get
            {
                WaitForGotData("StatusLabel");

                return _statuslabel;
            }
        }

        public string ProjectNumberAndName
        {
            get
            {
                WaitForGotData("ProjectNumberAndName");

                return _projectnumberandname;
            }
        }

        public string Favorite
        {
            get
            {
                WaitForGotData("Favorite");

                return _favorite;
            }
        }

        public string CustomField01
        {
            get
            {
                WaitForGotData("CustomField01");

                return _customfield01;
            }
        }

        public string CustomField02
        {
            get
            {
                WaitForGotData("CustomField02");

                return _customfield02;
            }
        }

        public string CustomField03
        {
            get
            {
                WaitForGotData("CustomField03");

                return _customfield03;
            }
        }


        public string DocumentFolder
        {
            get
            {
                WaitForGotData("DocumentFolder");

                return _documentfolder;
            }
        }
        public object this[string propertyName]
        {
            get { return this.GetType().GetProperty(propertyName).GetValue(this, null); }
            set { this.GetType().GetProperty(propertyName).SetValue(this, value, null); }
        }

        #endregion

        #region private_functions

        private void SetPendingAttributes()
        {
            _projectname = "pending";
            _clientname = "pending";
            _statuslabel = "pending";
        }

        private void SetNotInInlooxAttributes()
        {
            _projectname = "!?! no data for that in inloox !?!";
            _clientname = "";
            _statuslabel = "";
        }

        private void SetErrorWithGettingInlooxAttributes(string error)
        {
            _projectname = string.Format("!!! {0} !!!", error);
            _clientname = "Error";
            _statuslabel = "Error";
        }
        private async void GetAttributesAsync()
        {
            
            Debug.WriteLine(String.Format("           TcContentInlooxProject GetAttributesAsync: {0} {1}", "ENABLED", _number));

            // daten werden schon abgerufen
            if (_gettingDataNow)
            {
                int n = 0;
                // daten noch nicht da, aber werden schon abgerufen
                do
                {
                    // warten, bis die daten da sind oder die Anzahl der Schleifen überschritten wurde
                    Debug.WriteLine(String.Format("           TcContentInlooxProject GetAttributesAsync (waiting {2}): {0} {1}", "_gettingDataNow", _number, n));
                    n++;
                    if (_gotData)
                        return;
                    Thread.Sleep(50);
                } while (n < 100);

                // nachdem die Daten da sind (oder die Schleifenzahl überschritten wurde), können diese abgerufen werden.
                //return;
            }

            // wenn das programm bis hierher läuft, sollen daten geholt werden
            _gettingDataNow = true;

            if (Number != "")
            {
                // sollte der Standard sein 
                //Debug.WriteLine(String.Format("           TcContentInlooxProject GetAttributesAsync (TempNumber): {0} {1} {2}", "_gettingDataNow", _projectId, _tempnumber));

                try
                {

                    // wenn erst noch daten abgeholt werden müssen
                    if (_gettingDataNow == true)
                    {

                        // wait for do some tesing
                        //await Task.Delay(2500);

                        // var projects = await InlooxDefaults.GetFirst100Projects();
                        CustomApiDynamicProject project = await InlooxDefaults.GetCustomApiDynamikProjectFromNumberAsync(Number);

                        if (project != null)
                        {
                            Debug.WriteLine(String.Format("           TcContentInlooxProject GetAttributesAsync (getting GetApiProjectFromNumberAsync): {0} {1}", "_gettingDataNow", Number));

                            SetAttributesFromApiProcet(project);
                        }
                        else
                        {
                            Debug.WriteLine(String.Format("           TcContentInlooxProject GetAttributesAsync (getting NOT FOUND IN INLOOX): {0} {1}", "_gettingDataNow", Number));
                            // wenn kein Projekt gefunden wurde, dass diese Projektnummer hat, dann...
                            SetNotInInlooxAttributes();
                            GotData = true;
                        }
                    }

                }
                catch (Exception ex)
                {
                    String errormsg = "";

                    //if (ex.InnerException.GetType() == typeof(System.OverflowException
                    if (ex.GetType() == typeof(WebException))
                    {
                        WebException ex2 = (WebException)ex;
                        //Stream dataStream = request.GetRequestStream();
                        string exeption_message = ex2.Message;
                        Debug.WriteLine("");
                        //'"access_token":\s(.*),\s
                        Debug.WriteLine("");

                        // MsgBox(String.Format("ERROR in Odata! {0}", ex.Message), MsgBoxStyle.Critical, "Fehler in wpfMainWindow Odata")
                        Debug.WriteLine("{0} EXCEPTION in {1} Fehler:{2}", DateTime.Now, "TcContentInlooxProjectList", ex);

                        errormsg = String.Format("error: {0}", exeption_message);

                        SetErrorWithGettingInlooxAttributes(errormsg);
                        GotData = true;
                    }
                    else if (ex.GetType() == typeof(InvalidOperationException))
                    {
                        InvalidOperationException ex2 = (InvalidOperationException)ex;
                        string exeption_message = ex2.Message;
                        Debug.WriteLine("");
                        //'"access_token":\s(.*),\s
                        Debug.WriteLine("");

                        // MsgBox(String.Format("ERROR in Odata! {0}", ex.Message), MsgBoxStyle.Critical, "Fehler in wpfMainWindow Odata")
                        Debug.WriteLine("{0} EXCEPTION in {1} Fehler:{2}", DateTime.Now, "TcContentInlooxProjectList", ex);

                        errormsg = String.Format("Error: {0}", exeption_message);

                        SetErrorWithGettingInlooxAttributes(errormsg);
                        GotData = true;

                    }
                    else
                    {
                        Debug.WriteLine("{0} EXCEPTION in {1} Fehler:{2}", DateTime.Now, "TcContentInlooxProjectList", ex);
                        errormsg = ex.Message;

                        SetErrorWithGettingInlooxAttributes(errormsg);
                        GotData = true;
                    }

                    Debug.WriteLine(String.Format("{0}  ###################   EXEPTION1   ############## in GetAttributesAsync: {1}", DateTime.Now, errormsg));
                }
            }

            else
            {
                Debug.WriteLine(String.Format("           TcContentInlooxProject GetAttributesAsync (no match): {0} {1}", "_gettingDataNow", _number));
                throw new Exception("Number is empty! This is not implemented!");
            }

            _gettingDataNow = false;

        }
        
        private void SetAttributesFromApiProcet(CustomApiDynamicProject pv)
        {
            Debug.WriteLine(String.Format("           TcContentInlooxProject SetAttributesFromProjectView: {0}", pv.Project_ProjectId));

            if (pv != null)
            {

                _customApiDynamicProject = pv;

                // _number = (pv.Project_Number);                
                _projectname = RemoveUnwantedChars(pv.Project_Name);
                _clientname = RemoveUnwantedChars(pv.Project_ClientName);
                _divisionname = RemoveUnwantedChars(pv.Project_DivisionName);
                _favorite = ConvertFavoritAttributes(pv.Project_IsFavorite);
                _statuslabel = RemoveUnwantedChars(pv.Project_StatusLabel);
                _documentfolder = (pv.Project_DocumentFolder);
                _projectnumberandname = RemoveUnwantedChars(pv.Project_ProjectNumberAndName);

                // use a custom field from CustomApiDynamicProject.cs
                // Attetion! You has to customize this!
                _customfield01 = RemoveUnwantedChars(pv.Project_Number);
                _customfield02 = RemoveUnwantedChars(pv.Project_Number);
                _customfield03 = RemoveUnwantedChars(pv.Project_Number);

                //Debug.WriteLine(String.Format("            TcContentInlooxProject result GetAttributes: {0} / {1} / {2}", _projectId, _name, _clientname));
            }

            GotData = true;
        }

        private async Task WaitForGotDataAsync(string what)
        {
            int n = 0;
            while (!_gotData)
            {                
                Debug.WriteLine(String.Format("           TcContentInlooxProject WaitForGotDataAsync (waiting {2}): {0} {1}", what, _number, n));
                n++;
                await Task.Delay(25);
            }
        }

        private void WaitForGotData(string what)
        {
            int n = 0;
            while (!_gotData)
            {
                Debug.WriteLine(String.Format("           TcContentInlooxProject WaitForGotDataAsync (waiting {2}): {0} {1}", what, _number, n));
                n++;
                Thread.Sleep(25);
            }
        }

        private string ConvertFavoritAttributes(bool isFav)
        {
            //Debug.WriteLine(String.Format("           TcContentInlooxProject SetIsFavoritAttributes: {0}", isFav));

            if (isFav)
               return "***";
            else
                return "";

        }

        #endregion


        #region SerializationInfo

        [System.Security.SecurityCritical]  // auto-generated
        private TcContentInlooxProject(SerializationInfo info, StreamingContext context)
        {
            // Need to add in a security check here once it has been spec'ed.            
            _number = (String)info.GetValue(numberField, typeof(String));            
            _projectname = (String)info.GetValue(projectnameField, typeof(String));
            _clientname = (String)info.GetValue(clientnameField, typeof(String));
            _divisionname = (String)info.GetValue(divisionnameField, typeof(String));
            _statuslabel = (String)info.GetValue(statusnameField, typeof(String));
            _projectnumberandname = (String)info.GetValue(projectnumberandnameField, typeof(String));
            _favorite = (String)info.GetValue(favoriteField, typeof(String));
            _documentfolder = (String)info.GetValue(documentfolderField, typeof(String));

            // cutom fields
            _customfield01 = (String)info.GetValue(customfield01Field, typeof(String));
            _customfield02 = (String)info.GetValue(customfield02Field, typeof(String));
            _customfield03 = (String)info.GetValue(customfield03Field, typeof(String));
            

            //// Now do a security check.
            //String demandPath = _name + '.';
            //new FileIOPermission(FileIOPermissionAccess.PathDiscovery, demandPath).Demand();
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            // temp disabled
            //return;
            
            info.AddValue("Number", this.Number);
            info.AddValue("ProjectName", this.ProjectName);
            info.AddValue("ClientName", this.ClientName);
            info.AddValue("DivisionName", this.DivisionName);
            info.AddValue("StatusName", this.StatusLabel);
            info.AddValue("ProjectNumberAndName", this.ProjectNumberAndName);
            info.AddValue("Favorite", this.Favorite);            
            info.AddValue("DocumentFolder", this.DocumentFolder);

            // custom fields
            info.AddValue("CustomField01", this.CustomField01);
            info.AddValue("CustomField02", this.CustomField02);
            info.AddValue("CustomField03", this.CustomField03);
        }

        #endregion

        #region helper

        private string RemoveUnwantedChars(string s)
        {
            if ((s == null) || (s == ""))
                return s;

            //s = WriteStringAnsi(s, s.Length);

            //s = s.Replace("´", "_");
            //s = s.Replace("`", "_");
            // s = s.Replace("‘", "_");
            // s = s.Replace("–", "-");

            string modified_string = "";


            foreach (char ch in s.Substring(0, s.Length))
            {
                try
                {
                    byte b = Convert.ToByte(ch);
                    modified_string += ch;
                }
                catch (OverflowException)
                {
                    // Console.WriteLine("Unable to convert u+{0} to a byte.", Convert.ToInt16(ch).ToString("X4"));
                    Debug.WriteLine("Unable to convert u+{0} to a byte.", Convert.ToInt16(ch).ToString("X4"));
                    modified_string += "_";
                }
                // bytes[i++] = Convert.ToByte(ch);
            }
            
            // return s;
            return modified_string;
        }

        #endregion

        // replace chars with error on conversation
        private static string WriteStringAnsi(string str, int length)
        {
            if (String.IsNullOrEmpty(str))
                return str;
            else
            {
                int strLen = str.Length;
                if (length > 0 && strLen >= length)
                    strLen = length - 1;
                int i = 0;
                Byte[] bytes = new Byte[strLen + 1];
                foreach (char ch in str.Substring(0, strLen))
                {
                    try
                    {
                        bytes[i++] = Convert.ToByte(ch);
                        //int b = Convert.ToInt32(ch);
                        //int b2 = Convert.ToInt32(Convert.ToChar("_"));
                        //if (b <= 255)
                        //    bytes[i++] = Convert.ToByte(ch);
                        //else
                        //    bytes[i++] = Convert.ToByte(Convert.ToChar("_"));
                    }
                    catch (Exception ex)
                    {
                        if (ex.Message == "Der Wert für ein unsigniertes Byte war zu groß oder zu klein.")
                            str = str.Replace(ch, Convert.ToChar("_"));
                        //if (ex.InnerException.GetType() == typeof(System.OverflowException))
                        //    str.Replace(ch, Convert.ToChar("_"));
                    }

                }
                bytes[strLen] = 0;

                //Marshal.Copy(bytes, 0, addr, strLen + 1);
                Debug.WriteLine(String.Format("           TcContentInlooxProject WriteStringAnsi: {0}", str));

                return ByteArrayToString(bytes);

            }
        }

        private static string ByteArrayToString(byte[] arr)
        {
            ASCIIEncoding enc = new ASCIIEncoding();
            return enc.GetString(arr);
        }
    }

}
