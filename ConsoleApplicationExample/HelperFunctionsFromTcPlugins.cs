using System;
using System.Diagnostics;
using System.Windows.Forms;

using TcPlugins.HelperInloox11;
using TcPlugins.ContentInloox11;

namespace ConsoleApplicationExample
{
    internal class HelperFunctionsFromTcPlugins
    {

        private static bool EnableDebug = false;
        private static bool ForceOpenLoginDialog = false;        

        private static string myClassName = "HelperFunctionFromTcPlugin.cs";

        public static bool CheckForLogin()
        {
            if (EnableDebug)
                Debug.WriteLine(String.Format("{0} ################ {1} / CheckForLogin #####################", DateTime.Now, myClassName));

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

        public static InlooxLoginResult DoInlooxLogin()
        {

            if (EnableDebug)
                Debug.WriteLine(String.Format("{0} ################ {1} / DoInlooxLogin #####################", DateTime.Now, myClassName));


            //bool keepitopen = true;
            string errormsg = "";


            if (EnableDebug)
                Debug.WriteLine(String.Format("{0} ################ {1} / DoInlooxLogin authtoken - 1 {2} #####################", DateTime.Now, myClassName, InlooxDefaults.AuthToken.Left(10)));


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
                //InlooxLogin InlooxLoginDialog = new InlooxLogin(this, "InlooxAccessToken");
                // testing console version
                InlooxLogin InlooxLoginDialog = new InlooxLogin("InlooxAccessToken");

                result = (InlooxLoginResult)InlooxLoginDialog.ShowDialog();

                if (result == InlooxLoginResult.OK)
                {
                    if (EnableDebug)
                        Debug.WriteLine(String.Format("{0} ################ {1} / DoInlooxLogin / DialogOK #####################", DateTime.Now, myClassName));

                    //InlooxDefaults.AuthToken = InlooxLoginDialog.AuthToken;
                    Debug.WriteLine("was save authtoken");
                }
                if (String.IsNullOrEmpty(InlooxDefaults.AuthToken) || result != InlooxLoginResult.OK)
                {
                    if (EnableDebug)
                        Debug.WriteLine(String.Format("{0} ################ {1} / DoInlooxLogin / DialogCancel #####################", DateTime.Now, myClassName));

                    return InlooxLoginResult.Cancel;
                }

                if (EnableDebug)
                    Debug.WriteLine(String.Format("{0} ################ {1} / DoInlooxLogin authtoken - 2 {2} #####################", DateTime.Now, myClassName, InlooxDefaults.AuthToken.Left(10)));


            }
            catch (Exception ex)
            {

                throw new Exception(String.Format("Exception in DoInlooxLogin: ", ex.Message));
                //return result;
            }

            return result;
            
        }

        public static void logTimer(int _counter, DateTime _starttime, DateTime? _laststeptime = null)
        {
            if (!_laststeptime.HasValue)
                _laststeptime = DateTime.Now;

            DateTime _endtime = DateTime.Now;
            TimeSpan diff = _endtime - _starttime;
            TimeSpan last = _endtime - (DateTime)_laststeptime;

            Console.WriteLine(String.Format("___logTimer___"));
            Console.WriteLine(String.Format("___logTimer___    [{0}]   --- last  {1}.{2}   --- full  {3}.{4}", _counter, last.Seconds, last.Milliseconds, diff.Seconds, diff.Milliseconds));
            Console.WriteLine(String.Format("___logTimer___"));
        }
                     

    }
}

