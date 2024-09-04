using System;
using System.Windows.Forms;
using System.Diagnostics;

using TcPluginBase;
using TcPlugins.HelperInloox11;

namespace TcPlugins.ContentInloox11
{
    public partial class InlooxLogin: Form {
        
        private static Boolean showDebug = false;
        
        public InlooxLogin() {
            InitializeComponent();
        }

        // zur debug hilfe in der testingkonsole
        public InlooxLogin(string storeName)
        {
            InitializeComponent();

            if (showDebug)
                Debug.WriteLine(String.Format(" ################ InlooxLogin #####################"));
            
            DoMainSettings();

            CheckAuthToken();
            CheckRegExpr();

        }

        internal InlooxLogin(TcPlugin plugin, string storeName) {
            InitializeComponent();

            
            if (showDebug)
                Debug.WriteLine(String.Format(" ################ InlooxLogin #####################"));

            DoMainSettings();

            CheckAuthToken();
            CheckRegExpr();

        }

        private void DoMainSettings()
        {
            EmailAddress = InlooxDefaults.EmailAddress;
            // tbAuthToken.Text = InlooxDefaults.AuthToken;
            tbUserPassword.Text = InlooxDefaults.AuthToken;

            EndPointString = InlooxDefaults.EndPointString;
            OdataPathString = InlooxDefaults.OdataPathString;

            CheckedKeepOpen = InlooxDefaults.KeepLoginDialogOpen;
            PrjNrRegExprString = InlooxDefaults.PrjNrRegExprString;

            tbPathContent.Text = InlooxDefaults.DefaultPrjNrTestString;
            tbDefaultRegExpr.Text = InlooxDefaults.DefaultPrjRegExpr;
        }

        private void form_Load(object sender, EventArgs e) {
        }

        private void btnCancel_Click(object sender, EventArgs e) {
            DialogResult = DialogResult.Cancel;
        }

        #region Properties
        public bool CheckedAuthtoken
        {
            get
            {
                return cb_checked.Checked;
            }
            set
            {
                cb_checked.Checked = value;
            }
        }

        public bool CheckedSaved
        {
            get
            {
                return cb_saved.Checked;
            }
            set
            {
                cb_saved.Checked = value;
            }
        }

        public bool CheckedRegExprCheck
        {
            get
            {
                return cb_regexp_checked.Checked;
            }
            set
            {
                cb_regexp_checked.Checked = value;
            }
        }

        public bool CheckedRegExprSaved
        {
            get
            {
                return cb_regexp_saved.Checked;
            }
            set
            {
                cb_regexp_saved.Checked = value;
            }
        }

        private bool CheckedKeepOpen
        {
            get
            {
                return cbKeepItOpen.Checked;
            }
            set
            {
                cbKeepItOpen.Checked = value;
            }
        }


        public string EmailAddress
        {
            get
            {
                return tbUserName.Text;
            }
            set
            {
                tbUserName.Text = value;
            }
        }

        public string UserPassword
        {
            get
            {
                return tbUserPassword.Text;
            }
            set
            {
                tbUserPassword.Text = value;
            }
        }

        public string EndPointString
        {
            get
            {
                return tbEndPointString.Text;
            }
            set
            {
                tbEndPointString.Text = value;
                InlooxDefaults.EndPointString = value;
                tbUrl.Text = InlooxDefaults.ODataClientUri.ToString();

            }
        }

        public string OdataPathString
        {
            get
            {
                return tbOdataPathString.Text;
            }
            set
            {
                tbOdataPathString.Text = value;
                InlooxDefaults.OdataPathString = value;
                tbUrl.Text = InlooxDefaults.ODataClientUri.ToString();

            }
        }

        
        public string ErrorMsg
        {
            get
            {
                return tbError.Text;
            }
            set
            {
                tbError.Text = value;
            }
        }

        public string PrjNrRegExprString
        {
            get
            {
                return tbPrjNrRegExpr.Text;
            }
            set            {
                tbPrjNrRegExpr.Text = value;
            }
        }

        #endregion

        private void btnOK_Click(object sender, EventArgs e) {

            SaveAuthTokenToRegistry();
            SaveEmailAddressToRegistry();
            SaveInlooxUrlToRegistry();

            DialogResult = DialogResult.OK;
        }

       
        private void bCheck_Click(object sender, EventArgs e)
        {            
            CheckAuthToken();
        }

        private void tbOdataPathString_TextChanged(object sender, EventArgs e)
        {
            OdataPathString = this.tbOdataPathString.Text;
            CheckForValidUri();
            tbUrl.Text = InlooxDefaults.ODataClientUri.ToString();

        }

        private void tbEndPointString_TextChanged(object sender, EventArgs e)
        {
            EndPointString = this.tbEndPointString.Text;
            CheckForValidUri();
            tbUrl.Text = InlooxDefaults.ODataClientUri.ToString();
            tbTokenPath.Text = String.Format("{0}/{1}", InlooxDefaults.EndPointString, InlooxDefaults.PersonalAccessTokenPathString);
        }

        private void tbUserName_TextChanged(object sender, EventArgs e)
        {
            EmailAddress = this.tbUserName.Text;
        }

        private void tbUserPassword_TextChanged(object sender, EventArgs e)
        {
            UserPassword = this.tbUserPassword.Text;

            // only for information
            this.tbAuthToken.Text = String.Format("{0}************************", UserPassword.Left(10));

            CheckForEnablingOkButton();
            
        }

        private void tbUrl_TextChanged(object sender, EventArgs e)
        {
            //InlooxDefaults.ODataClientUri = new Uri(InlooxDefaults.EndPointOdata.ToString());
            //CheckForValidUri();
            CheckAuthToken();
            CheckForEnablingOkButton();
        }

        private void tbTokenPath_TextChanged(object sender, EventArgs e)
        {
            //TokenPath = this.tbTokenPath.Text;
        }

        private void tbAuthToken_TextChanged(object sender, EventArgs e)
        {
            // cb_saved.Checked = false;
            CheckedSaved = false;
            if (showDebug)
            {                
                Debug.WriteLine(String.Format(" ################ tbAuthToken_TextChanged ##################### 1   {0}", InlooxDefaults.AuthToken.Left(10)));
                Debug.WriteLine(String.Format(" ################ tbAuthToken_TextChanged ##################### 2   {0}", this.tbAuthToken.Text.Left(10)));
            }
            // InlooxDefaults.AuthToken = this.tbAuthToken.Text;
            InlooxDefaults.AuthToken = this.tbUserPassword.Text;
        }

        private void bSaveAuthToken_Click(object sender, EventArgs e)
        {
            SaveAuthTokenToRegistry();
        }

        private bool CheckAuthToken()
        {

            if (showDebug)
                Debug.WriteLine(String.Format(" ################ CheckAuthToken #####################"));

            string _er = "";
            CheckedAuthtoken = InlooxDefaults.CheckCurrentToken(out _er);

            //ErrorMsg = _er;
            AddErrorMsgToDialog(_er, "CheckAuthToken");

            SaveAuthTokenToRegistry();
            CheckForEnablingOkButton();
            return CheckedAuthtoken;
        }

        private void AddErrorMsgToDialog(string msg, string what)
        {
            if (msg == "" || msg == null)
            {
                ErrorMsg = String.Format("{0} was i.O. {1}{2}", what, "\r\n", ErrorMsg);
            }
            else
            {
                ErrorMsg = String.Format("Error with {0}: {1}{2}{3}", what, msg, "\r\n", ErrorMsg);
            }
        
        } 

        private void CheckForEnablingOkButton()
        {
            if (cb_checked.Checked && cb_saved.Checked && cb_regexp_checked.Checked && cb_regexp_saved.Checked)
            {
                btnOK.Enabled = true;
                lHintToOkay.Visible = false;
            }
            else
            {
                btnOK.Enabled = false;
                lHintToOkay.Visible = true;
            }
        }

        private bool SaveAuthTokenToRegistry(bool forceSave = false)
        {
            if (CheckedAuthtoken || forceSave)
            {
                AppSettingsManager.SetValueString("AuthToken11", InlooxDefaults.AuthToken);
                // cb_saved.Checked = true;
                CheckedSaved = true;
                return true;
            }
            return false;
        }

        
        private bool SaveRegExprToRegistry()
        {
            if (CheckedRegExprCheck)
            {
                AppSettingsManager.SetValueString("PrjNrRegExprString", InlooxDefaults.PrjNrRegExprString);
                // cb_saved.Checked = true;
                CheckedRegExprSaved = true;
                return true;
            }
            return false;
        }

        private bool SaveEmailAddressToRegistry()
        {
            if (CheckedAuthtoken)
            {
                AppSettingsManager.SetValueString("EmailAddress11", EmailAddress);
                //cb_saved.Checked = true;
                return true;
            }
            return false;
        }

        private bool SaveInlooxUrlToRegistry()
        {
            if (CheckedAuthtoken)
            {

                AppSettingsManager.SetValueString("Inloox11Url", InlooxDefaults.ODataClientUri.ToString());
                AppSettingsManager.SetValueString("EndPoint11String", InlooxDefaults.EndPointString);
                AppSettingsManager.SetValueString("OdataPath11String", InlooxDefaults.OdataPathString);

                //cb_saved.Checked = true;
                return true;
            }
            return false;
        }

        private bool SaveKeepDialogOpenToRegistry()
        {
            InlooxDefaults.KeepLoginDialogOpen = cbKeepItOpen.Checked;
            return true;
        }

        private void bGetAuthToken_Click(object sender, EventArgs e)
        {

            throw new NotImplementedException("Get authtoken over this application is not possible");
         
        }

        private void tbPrjNrRegExpr_TextChanged(object sender, EventArgs e)
        {
            Debug.WriteLine(String.Format(" ################ tbPrjNrRegExpr_TextChanged ##################### 2   {0}", this.tbPrjNrRegExpr.Text));
            CheckForValidRegExpr();
            CheckForEnablingOkButton();
        }

        private bool CheckForValidRegExpr()
        {

            if (showDebug)
                Debug.WriteLine(String.Format(" ################ CheckRegExpr #####################"));

            string _er = "";
            bool result = false;

            CheckedRegExprCheck = InlooxDefaults.SetPrjNrRegExprString(this.tbPrjNrRegExpr.Text, out _er);

            AddErrorMsgToDialog(_er, "CheckForValidRegExpr");
            //ErrorMsg = _er;

            CheckRegExpr();

            return result;

        }

        private bool CheckForValidUri()
        {

            if (showDebug)
                Debug.WriteLine(String.Format(" ################ CheckForValidUri #####################"));

            string _er = "";
            bool result = false;

            bool _validUrlCheck = false;

            _validUrlCheck = InlooxDefaults.SetODataClientUriString(this.tbEndPointString.Text, this.tbOdataPathString.Text, out _er);

            AddErrorMsgToDialog(_er, "CheckForValidUri");
            //ErrorMsg = _er;

            return result;

        }
        private void CheckRegExpr()
        {
            string drive;
            string unc;
            string division;
            string projectnumber;

            if (CheckedRegExprCheck)
            {
                bool gotinfo = InlooxDefaults.GetDataFromPath(tbPathContent.Text, out drive, out unc, out division, out projectnumber);

                cbRegExprCheckResult.Checked = gotinfo;
                tbDrive.Text = drive;
                tbUnc.Text = unc;
                tbDivision.Text = division;
                tbProjectNumber.Text = projectnumber;

                if (gotinfo)
                {
                    bSaveRegExpr.Enabled = true;
                    SaveRegExprToRegistry();
                    CheckForEnablingOkButton();
                }

                else
                {
                    bSaveRegExpr.Enabled = false;
                }
            } 
            else
            {                
                cb_regexp_saved.Checked = false;
                cbRegExprCheckResult.Checked = false;
                tbDrive.Text = "";
                tbUnc.Text = "";
                tbDivision.Text = "";
                tbProjectNumber.Text = "error in regular expression";
            }

            
        }

        private void bCheckRegExpr_Click(object sender, EventArgs e)
        {
            CheckForValidRegExpr();
        }
       
        private void cbRemoveTokenFromRegistry_Click(object sender, EventArgs e)
        {
            InlooxDefaults.AuthToken = "";            
            this.tbUserPassword.Text = InlooxDefaults.AuthToken;

            CheckedAuthtoken = false;
            cb_saved.Checked = false;

            SaveAuthTokenToRegistry(true);
        }

        private void bResetRegExpr_Click(object sender, EventArgs e)
        {
            tbPrjNrRegExpr.Text = tbDefaultRegExpr.Text;
        }

        private void cbKeepItOpen_CheckedChanged(object sender, EventArgs e)
        {
            SaveKeepDialogOpenToRegistry();
        }

        private void tbPathContent_TextChanged(object sender, EventArgs e)
        {
            InlooxDefaults.DefaultPrjNrTestString = tbPathContent.Text;
            CheckRegExpr();
            CheckForEnablingOkButton();
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }


        private void bSaveRegExpr_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }



        private void panel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label7_Click_1(object sender, EventArgs e)
        {

        }



        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click_2(object sender, EventArgs e)
        {

        }

        private void label7_Click_3(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
