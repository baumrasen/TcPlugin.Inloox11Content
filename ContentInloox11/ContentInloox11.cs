using System;
using System.Globalization;
using System.IO;
using System.Security;

using System.Windows.Forms;

using TcPluginBase;
using TcPluginBase.Content;
using TcPlugins.HelperInloox11;

using Microsoft.Extensions.Logging;


namespace TcPlugins.ContentInloox11 {
    public sealed class ContentInloox11 : ContentPlugin {

        #region constants        
        private const int FieldCount = 13;
        private const int FileTypeIdx = 0;
        private const int Size32Idx = 1;
        private const int CreateDateIdx = 2;
        private const int CreateTimeIdx = 3;
        private const int CreateDateTimeIdx = 4;
        private const int ProjektNameIdx = 5;
        private const int ClientNameIdx = 6;
        private const int DivisionNameIdx = 7;
        private const int StatusNameIdx = 8;
        private const int FavoriteIdx = 9;
        private const int CustomField01Idx = 10;
        private const int CustomField02Idx = 11;
        private const int CustomField03Idx = 12;

        private bool ForceOpenLoginDialog = false;
        private bool EnableDebug = false;

        public static Microsoft.Extensions.Logging.ILogger logger = null;

        private string myClassName = "InlooxContent.cs";       

        private bool _loginchecked = false;

        private const int IndexForCompareFiles = 10000;
        
        private static readonly string[] FieldNames = new[] {
            "FileType", 
            "Size_F", 
            "CreateDate", 
            "CreateTime" , 
            "CreateDateTime", 
            "ProjektName", 
            "ClientName", 
            "DivisionName", 
            "StatusName", 
            "Favorite",
            "CustomField01",
            "CustomField02",
            "CustomField03"
        };

        private static readonly string[] FieldUnits = new[] {
            "", "", "", "", "", "", "", "", "", "", "", "", ""
        };

        private static readonly ContentFieldType[] FieldTypes = new[] {
            ContentFieldType.WideString,
            ContentFieldType.NumericFloating,
            ContentFieldType.Date,
            ContentFieldType.Time,
            ContentFieldType.DateTime,
            ContentFieldType.String,
            ContentFieldType.String,
            ContentFieldType.String,
            ContentFieldType.String,
            ContentFieldType.String,
            ContentFieldType.String,
            ContentFieldType.String,
            ContentFieldType.String
        };
        
        private static Settings pluginsettings = null;

        #endregion


        #region Constructors

        public ContentInloox11(Settings pluginSettings) : base(pluginSettings)
        {
            Title = "InlooxContent";
            DetectString = "!(EXT=\"TXT\" | EXT=\"PDF\")";

            pluginsettings = pluginSettings;

            // enable for debuging helper in early state of plugin loading
            //MessageBox.Show("InlooxContent will now be started", "Load InlooxContent");

            Boolean BoolParseResult;
            ForceOpenLoginDialog = Boolean.TryParse(GetValueFromKey(pluginSettings, "ForceOpenLoginDialog"), out BoolParseResult);
            EnableDebug = Boolean.TryParse(GetValueFromKey(pluginSettings, "EnableDebug"), out BoolParseResult);

            // default log level
            LogLevel defaultLL = LogLevel.Warning;

            if (EnableDebug)
                defaultLL = LogLevel.Debug;

            // create a logger factory
            var loggerFactory = LoggerFactory.Create(
                builder => builder
                            // add console as logging target
                            .AddConsole()
                            // add debug output as logging target
                            .AddDebug()
                            // change console formatter
                            .AddSimpleConsole(options =>
                            {
                                options.IncludeScopes = true;
                                options.SingleLine = true;
                                options.TimestampFormat = "HH:mm:ss ";
                            })
                            // set minimum level to log
                            .SetMinimumLevel(defaultLL)
            );

            // create a logger
            logger = loggerFactory.CreateLogger("ICP");

            // logging test
            //logger.LogTrace("Trace message");
            //logger.LogDebug("Debug message");
            //logger.LogInformation("Info message");
            //logger.LogWarning("Warning message");
            //logger.LogError("Error message");
            //logger.LogCritical("Critical message");

            logger.LogDebug(String.Format("{0} ################ {1} / START #####################", DateTime.Now, myClassName));

            logger.LogDebug(String.Format("{0} ################ {1} / START pluginSettings: {2} = {3}", DateTime.Now, myClassName, "ForceOpenLoginDialog", ForceOpenLoginDialog));
            logger.LogDebug(String.Format("{0} ################ {1} / START pluginSettings: {2} = {3}", DateTime.Now, myClassName, "EnableDebug", EnableDebug));
            
                

            if (ForceOpenLoginDialog)
            {
                //InlooxDefaults.AuthToken = "";
                logger.LogDebug(String.Format("{0} ################ {1} / START ForceOpenLoginDialog is forced to be open!", DateTime.Now, myClassName));
                InlooxDefaults.KeepLoginDialogOpen = true;
            }

            logger.LogInformation(String.Format("DO NOW CheckForLogin"));
            _loginchecked = CheckForLogin();
            logger.LogInformation(String.Format("RESULT CheckForLogin {0}", _loginchecked));

        }

        private bool CheckForLogin()
        {            
            logger.LogDebug(String.Format("{0} ################ {1} / CheckForLogin #####################", DateTime.Now, myClassName));
                        

            logger.LogInformation(String.Format("DoInlooxLogin {0}", "NOW"));
            InlooxLoginResult ilR = DoInlooxLogin();

            if (ilR == InlooxLoginResult.OK)
                return true;
            else
            {
                InlooxDefaults.KeepLoginDialogOpen = true;
                MessageBox.Show("Without checked logindata, the plugin is not working!", "Logindata not checked!");
                return false;
            }

        }

        #endregion

        // Uses the foreach statement which hides the complexity of the enumerator.
        // NOTE: The foreach statement is the preferred way of enumerating the contents of a collection.
        private static string GetValueFromKey(Settings settings, string key)
        {

            string result;
            if (settings.TryGetValue(key, out result))
            {
                return result;
            }
            
            return "";
        }

        private InlooxLoginResult DoInlooxLogin()
        {

            logger.LogDebug(String.Format("{0} ################ {1} / DoInlooxLogin #####################", DateTime.Now, myClassName));

            //bool keepitopen = true;
            string errormsg = "";
            
            logger.LogDebug(String.Format("{0} ################ {1} / DoInlooxLogin authtoken - 1 {2} #####################", DateTime.Now, myClassName, InlooxDefaults.AuthToken.Left(10)));

            InlooxLoginResult result = InlooxLoginResult.Undefined;

            try
            {

                bool checkauthtoken = false;

                if (!ForceOpenLoginDialog)
                {
                    checkauthtoken = InlooxDefaults.CheckCurrentToken(out errormsg);
                }

                if (InlooxDefaults.GotAuthToken && !InlooxDefaults.KeepLoginDialogOpen)
                {
                    //gotauthtoken = true;
                    return InlooxLoginResult.OK;
                }

                // tc plugin version
                InlooxLogin InlooxLoginDialog = new InlooxLogin(this, "InlooxAccessToken");
                // testing console version
                //InlooxLogin InlooxLoginDialog = new InlooxLogin("InlooxAccessToken");

                result = (InlooxLoginResult)InlooxLoginDialog.ShowDialog();

                if (result == InlooxLoginResult.OK)
                {                    
                    logger.LogDebug(String.Format("{0} ################ {1} / DoInlooxLogin / DialogOK #####################", DateTime.Now, myClassName));

                    //InlooxDefaults.AuthToken = InlooxLoginDialog.AuthToken;
                    logger.LogDebug("was save authtoken");
                }
                if (String.IsNullOrEmpty(InlooxDefaults.AuthToken) || result == InlooxLoginResult.Cancel)
                {                    
                    logger.LogDebug(String.Format("{0} ################ {1} / DoInlooxLogin / DialogCancel #####################", DateTime.Now, myClassName));

                    return InlooxLoginResult.Cancel;
                }
                
                logger.LogDebug(String.Format("{0} ################ {1} / DoInlooxLogin authtoken - 2 {2} #####################", DateTime.Now, myClassName, InlooxDefaults.AuthToken.Left(10)));


            }
            catch (Exception ex)
            {

                throw new Exception(String.Format("Exception in DoInlooxLogin: ", ex.Message));
                //return result;
            }

            return result;

        }

        #region IContentPlugin Members

        public override ContentFieldType GetSupportedField(int fieldIndex, out string fieldName,
                out string units, int maxLen) {
            logger.LogDebug(String.Format("        ContentTest - GetSupportedField (fieldIndex): {0}", fieldIndex));
            fieldName = String.Empty;
            units = String.Empty;
            if (fieldIndex == IndexForCompareFiles) {
                fieldName = "Compare as text";
                return ContentFieldType.CompareContent;
            }
            if (fieldIndex < 0 || fieldIndex >= FieldCount)
                return ContentFieldType.NoMoreFields;
            fieldName = FieldNames[fieldIndex];
            if (fieldName.Length > maxLen)
                fieldName = fieldName.Substring(0, maxLen);
            units = FieldUnits[fieldIndex];
            if (units.Length > maxLen)
                units = units.Substring(0, maxLen);
            return FieldTypes[fieldIndex];
        }

        public override GetValueResult GetValue(string fileName, int fieldIndex, int unitIndex,
                int maxLen, GetValueFlags flags, out string fieldValue, out ContentFieldType fieldType) {

            if ((flags & GetValueFlags.DelayIfSlow) != 0)
            {
                logger.LogDebug(String.Format("        ContentTest - GetValue-Delay fileName: {0} / {1} / {2} / {3} / {4}", fileName, fieldIndex, unitIndex, maxLen, flags));
            } 
            else
            {
                logger.LogDebug(String.Format("        ContentTest - GetValue-NoDelay fileName: {0} / {1} / {2} / {3} / {4}", fileName, fieldIndex, unitIndex, maxLen, flags));
            }

            string drive;
            string unc;
            string division;
            string projectnumber;
            //Boolean do_dir_file_exists_check = false;

            getAborted = false;
            GetValueResult result = GetValueResult.FieldEmpty;
            fieldType = ContentFieldType.NoMoreFields;
            fieldValue = null;

            //return result;

            bool gotinfo = InlooxDefaults.GetDataFromPath(fileName, out drive, out unc, out division, out projectnumber);
            if (gotinfo)
            {
                logger.LogDebug(String.Format("        ContentTest - GetValue  {0}{1} / {2} / {3}", drive, unc, division, projectnumber));
            }                
            else
            {
                logger.LogDebug(String.Format("x-x x-x ContentTest - GetValue - NO MATCH for Projectnumber, etc"));
                return result;
            }

            if (String.IsNullOrEmpty(fileName))
                return result;
                            
            if (Directory.Exists(fileName))
            //if (file_from_filename == "")
            {

                DirectoryInfo info = new DirectoryInfo(fileName);                
                TcContentInlooxProject iinfo = null;

                if (gotinfo && !_loginchecked)
                {
                    //CheckForLogin();
                    return result;
                }

                if ((gotinfo) && (projectnumber != ""))
                {                    
                    iinfo = InlooxDefaults.TcCIPL.GetTcContentInlooxProjectFromProjectNumber(projectnumber);
                }

                string timeString = info.CreationTime.ToString("G");
                switch (fieldIndex) {
                    case FileTypeIdx:
                        fieldValue = "Folder";
                        fieldType = ContentFieldType.WideString;
                        break;
                    case Size32Idx:
                        if ((flags & GetValueFlags.DelayIfSlow) != 0) {
                            fieldValue = "?";
                            result = GetValueResult.OnDemand;
                        } else {
                            try {
                                long size = GetDirectorySize(fileName);
                                fieldType = ContentFieldType.NumericFloating; 
                                fieldValue = GetSizeValue(size);
                            } catch (IOException) {
                                // Directory changed, stop long operation
                                result = GetValueResult.FieldEmpty;
                            }
                        }
                        break;
                    case CreateDateTimeIdx:
                        fieldValue = timeString;
                        fieldType = ContentFieldType.DateTime;
                        break;
                    case CreateDateIdx:
                        fieldValue = timeString;
                        fieldType = ContentFieldType.Date;
                        break;
                    case CreateTimeIdx:
                        fieldValue = timeString;
                        fieldType = ContentFieldType.Time;
                        break;
                    case ProjektNameIdx:
                        SetStringContentFields(iinfo, "ProjectName", flags, out fieldValue, out fieldType, out result);
                        break;
                    case ClientNameIdx:
                        SetStringContentFields(iinfo, "ClientName", flags, out fieldValue, out fieldType, out result);                                     
                        break;
                    case DivisionNameIdx:
                        SetStringContentFields(iinfo, "DivisionName", flags, out fieldValue, out fieldType, out result);                                     
                        break;
                    case StatusNameIdx:
                        SetStringContentFields(iinfo, "StatusLabel", flags, out fieldValue, out fieldType, out result);                                         
                        break;
                    case FavoriteIdx:
                        SetStringContentFields(iinfo, "Favorite", flags, out fieldValue, out fieldType, out result);                                           
                        break;
                    case CustomField01Idx:
                        SetStringContentFields(iinfo, "CustomField01", flags, out fieldValue, out fieldType, out result);
                        break;
                    case CustomField02Idx:
                        SetStringContentFields(iinfo, "CustomField02", flags, out fieldValue, out fieldType, out result);
                        break;
                    case CustomField03Idx:
                        SetStringContentFields(iinfo, "CustomField03", flags, out fieldValue, out fieldType, out result);
                        break;
                    default:
                        result = GetValueResult.NoSuchField;
                        break;
                }
                return result;

            }
            else if (File.Exists(fileName))
            {
                   
                FileInfo info = new FileInfo(fileName);
                string timeString = info.CreationTime.ToString("G");
                switch (fieldIndex) {
                    case FileTypeIdx:
                        string fileType;
                        switch (info.Extension.ToLower()) {
                            case ".exe":
                            case ".dll":
                            case ".sys":
                            case ".com":
                                fileType = "Program";
                                break;
                            case ".zip":
                            case ".rar":
                            case ".cab":
                            case ".7z":
                                fileType = "Archive";
                                break;
                            case ".bmp":
                            case ".jpg":
                            case ".png":
                            case ".gif":
                                fileType = "Это Image";
                                break;
                            case ".mp3":
                            case ".avi":
                            case ".wav":
                                fileType = "Multimedia";
                                break;
                            case ".htm":
                            case ".html":
                                fileType = "Web Page";
                                break;
                            default:
                                fileType = "File";
                                break;
                        }
                        if (!String.IsNullOrEmpty(fileType)) {
                            fieldValue = fileType;
                            fieldType = ContentFieldType.WideString;
                        }
                        break;
                    case Size32Idx:
                        long size = info.Length;
                        fieldType = ContentFieldType.NumericFloating;
                        fieldValue = GetSizeValue(size);
                        break;
                    case CreateDateTimeIdx:
                        fieldValue = timeString;
                        fieldType = ContentFieldType.DateTime;
                        break;
                    case CreateDateIdx:
                        fieldValue = timeString;
                        fieldType = ContentFieldType.Date;
                        break;
                    case CreateTimeIdx:
                        fieldValue = timeString;
                        fieldType = ContentFieldType.Time;
                        break;
                    default:
                        result = GetValueResult.NoSuchField;
                        break;
                }
                return result;
            } 
            else
            {
                result = GetValueResult.FileError;
                return result;
            }
            
            //if (!fieldType.Equals(ContentFieldType.NoMoreFields))
            //    result = GetValueResult.Success;

            //return result;
        }

        private void SetStringContentFields(TcContentInlooxProject iinfo, string propertyName, GetValueFlags flags, out string fieldValue, out ContentFieldType fieldType, out GetValueResult result)
        {

            int current_type = 0;
            fieldType = ContentFieldType.String;

            switch (current_type)
            {
                case 0:
                    if (iinfo == null)
                    {
                        fieldValue = "";                        
                        result = GetValueResult.Success;
                    }
                    else
                    {
                        if ((flags & GetValueFlags.DelayIfSlow) != 0)
                        {
                            fieldValue = "...pending";
                            result = GetValueResult.Delayed;
                        }
                        else
                        {
                            fieldValue = (string)iinfo[propertyName];
                            result = GetValueResult.Success;                            
                        }

                    }
                    break;
                
                case 1:
                    if (iinfo == null)
                    {
                        fieldValue = "";
                        result = GetValueResult.Success;
                    }
                    else
                    {
                        if ((flags & GetValueFlags.DelayIfSlow) != 0)
                        {
                            fieldValue = "...delayed 1";
                            result = GetValueResult.Delayed;
                        }
                        else
                        {
                            if ((string)iinfo[propertyName] == "")
                            {
                                fieldValue = "...still delayed 1";
                                result = GetValueResult.Success;
                            }
                            else
                            {
                                fieldValue = (string)iinfo[propertyName];
                                result = GetValueResult.Success;
                            }
                        }

                    }
                    break;

                default:
                    if (iinfo == null)
                    {
                        fieldValue = "";
                        result = GetValueResult.Success;
                    }
                    else
                    {
                        if ((flags & GetValueFlags.DelayIfSlow) != 0)
                        {
                            fieldValue = (string)iinfo[propertyName];
                            result = GetValueResult.Delayed;
                        }
                        else
                        {
                            fieldValue = (string)iinfo[propertyName];
                            result = GetValueResult.Success;
                        }

                    }
                    break;

            }
            
        }

        public override void StopGetValue(string fileName) {
            getAborted = true;
        }

        public override DefaultSortOrder GetDefaultSortOrder(int fieldIndex) {
            if (fieldIndex == Size32Idx)        // field Size_32 - order descending
                return DefaultSortOrder.Desc;
            return DefaultSortOrder.Asc;
        }

        //public override  void PluginUnloading()
        //{
        //    // do nothing
        //}

        public override SupportedFieldOptions GetSupportedFieldFlags(int fieldIndex) {
            logger.LogDebug(String.Format("        ContentTest - GetSupportedFieldFlags (fieldIndex): {0}", fieldIndex));
            if (fieldIndex == -1)
                return SupportedFieldOptions.Edit | SupportedFieldOptions.SubstMask;
            switch (fieldIndex) {
                case CreateDateTimeIdx:
                    return SupportedFieldOptions.Edit | SupportedFieldOptions.SubstDateTime;
                case CreateDateIdx:
                    return SupportedFieldOptions.Edit | SupportedFieldOptions.SubstDate;
                case CreateTimeIdx:
                    return SupportedFieldOptions.Edit | SupportedFieldOptions.SubstTime;
                case ProjektNameIdx:
                    return SupportedFieldOptions.SubstAttributeString;
                case ClientNameIdx:
                    return SupportedFieldOptions.SubstAttributeString;
                case DivisionNameIdx:
                    return SupportedFieldOptions.SubstAttributeString;
                case StatusNameIdx:
                    return SupportedFieldOptions.SubstAttributeString;
                case FavoriteIdx:
                    return SupportedFieldOptions.SubstAttributeString;
                case CustomField01Idx:
                    return SupportedFieldOptions.SubstAttributeString;
                case CustomField02Idx:
                    return SupportedFieldOptions.SubstAttributeString;
                case CustomField03Idx:
                    return SupportedFieldOptions.SubstAttributeString;
                default:
                    return SupportedFieldOptions.None;
            }
        }

        public override SetValueResult SetValue(string fileName, int fieldIndex, int unitIndex,
                ContentFieldType fieldType, string fieldValue, SetValueFlags flags) {

            logger.LogDebug(String.Format("        ContentTest - GetSupportedFieldFlags (fieldIndex/fileName): {0}/{1}", fieldIndex, fileName));

            if (String.IsNullOrEmpty(fileName) && fieldIndex < 0)    // change attributes operation has ended
                return SetValueResult.NoSuchField;
            if (String.IsNullOrEmpty(fieldValue))
                return SetValueResult.NoSuchField;
            SetValueResult result = SetValueResult.NoSuchField;
            DateTime created = DateTime.Parse(fieldValue);
            bool dateOnly = (flags & SetValueFlags.OnlyDate) != 0;
            if (Directory.Exists(fileName)) {
                DirectoryInfo dirInfo = new DirectoryInfo(fileName);
                if (SetCombinedDateTime(ref created, dirInfo.CreationTime, fieldType, dateOnly)) {
                    Directory.SetCreationTime(fileName, created);
                    result = SetValueResult.Success;
                }
            } else if (File.Exists(fileName)) {
                FileInfo fileInfo = new FileInfo(fileName);
                if (SetCombinedDateTime(ref created, fileInfo.CreationTime, fieldType, dateOnly)) {
                    File.SetCreationTime(fileName, created);
                    result = SetValueResult.Success;
                }
            } else
                result = SetValueResult.FileError;
            return result;
        }

        public override bool GetDefaultView(out string viewContents, out string viewHeaders, out string viewWidths,
                out string viewOptions, int maxLen) {

            switch (InlooxDefaults.DirectoryLevel)
            {
                case 0:
                    logger.LogDebug(String.Format("       GetDefaultView 0"));
                    viewContents = "[=<fs>.ClientName]\\n[=<fs>.ProjektName]\\n[=<fs>.Size_F]\\n[=<fs>.CreateDateTime]";
                    viewHeaders = "Kunde\\nProjektname\\nSize(Float)\\nCreated";
                    viewWidths = "100,30,60,60,-80,-80";
                    //viewOptions = "auto-adjust-width|1"; 
                    viewOptions = "-1|1";
                    return true;

                default:
                    logger.LogDebug(String.Format("       GetDefaultView 1"));
                    viewContents = "[=<fs>.FileType]\\n[=<fs>.Size_F]\\n[=<fs>.CreateDateTime]";
                    viewHeaders = "File Type\\nSize(Float)\\nCreated";
                    viewWidths = "80,30,80,-80,-80";
                    viewOptions = "-1|1";
                    return true;
            }            
        }

        public override void SendStateInformation(StateChangeInfo state, string path) {
            // do nothing, just for trace

            logger.LogDebug(String.Format("       SendStateInformation {0} : {1}", state, path));

        }

        public override ContentCompareResult CompareFiles(int compareIndex, string fileName1, 
                string fileName2, ContentFileDetails fileDetails, out int iconResourceId) {
            iconResourceId = -1;
            // You can call ContentProgressProc(int) here to inform TC about comparing progress. 
            return ContentCompareResult.CannotCompare;
        }

        public bool LoginChecked
        {
            get
            {
                return _loginchecked;
            }
        }

        #endregion IContentPlugin Members

        #region Private Methods

        private bool getAborted;

        private static bool SetCombinedDateTime(ref DateTime newTime, DateTime currentTime,
                ContentFieldType fieldType, bool dateOnly) {
            bool result = true;
            switch (fieldType) {
                case ContentFieldType.DateTime:
                    if (dateOnly) {
                        newTime = new DateTime(
                            newTime.Year, newTime.Month, newTime.Day,
                            currentTime.Hour, currentTime.Minute, currentTime.Second);
                    }
                    break;
                case ContentFieldType.Date:
                    newTime = new DateTime(
                        newTime.Year, newTime.Month, newTime.Day,
                        currentTime.Hour, currentTime.Minute, currentTime.Second);
                    break;
                case ContentFieldType.Time:
                    newTime = new DateTime(
                        currentTime.Year, currentTime.Month, currentTime.Day,
                        newTime.Hour, newTime.Minute, newTime.Second);
                    break;
                default:
                    result = false;
                    break;
            }
            return result;
        }

        private long GetDirectorySize(string dirPath) {
            if (getAborted)
                throw new IOException();
            long dirSize = 0;
            try {
                DirectoryInfo di = new DirectoryInfo(dirPath);
                foreach (FileInfo fi in di.GetFiles())
                    dirSize += fi.Length;
                foreach (DirectoryInfo cd in di.GetDirectories())
                    dirSize += GetDirectorySize(cd.FullName);
            } catch (SecurityException) { }
            return dirSize;
        }

        private static string GetSizeValue(long size) {
            string result = size.ToString(CultureInfo.InvariantCulture);
            double dSize = size;
            string altStr = null;
            if (dSize > 1024.0) {
                dSize /= 1024.0;
                altStr = String.Format("|{0:0} Kb", dSize);
            }
            if (dSize > 1024.0) {
                dSize /= 1024.0;
                altStr = String.Format("|{0:0} Mb", dSize);
            }
            if (dSize > 1024.0) {
                dSize /= 1024.0;
                altStr = String.Format("|{0:0} Gb", dSize);
            }
            if (!String.IsNullOrEmpty(altStr))
                result += altStr;
            return result;
        }

        #endregion Private Methods
    }
}
