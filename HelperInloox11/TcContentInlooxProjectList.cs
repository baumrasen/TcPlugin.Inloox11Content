using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

using InlooxOdataClientApp.Default;
using InLoox.PM.Domain.Model.Aggregates.Api;
using Simple.OData.Client;
using System.Diagnostics.Eventing.Reader;

namespace TcPlugins.HelperInloox11
{
    public class TcContentInlooxProjectList
    {

        private static List<TcContentInlooxProject> _tccontentinlooxprojectslist = new List<TcContentInlooxProject>();

        #region contructor

        public TcContentInlooxProjectList()
        {
            // initialize
        }

        #endregion

        #region Properties
        

        public List<TcContentInlooxProject> TcCIPL
        {
            get
            {

                return _tccontentinlooxprojectslist;
            }
        }              

        #endregion
              

        private static void logTimer(int _counter, DateTime _starttime, DateTime? _laststeptime = null)
        {
            if (!_laststeptime.HasValue)
                _laststeptime = DateTime.Now;

            DateTime _endtime = DateTime.Now;
            TimeSpan diff = _endtime - _starttime;
            TimeSpan last = _endtime - (DateTime)_laststeptime;
            
        }


        

        private int CurrentInlooxProjectsCount()
        {


            int inlooxcount = 0;

            if (_tccontentinlooxprojectslist != null)
            {
                inlooxcount = _tccontentinlooxprojectslist.Count();
            }

            return inlooxcount;
        }

        
        public TcContentInlooxProject GetTcContentInlooxProjectFromProjectNumber(string pn)
        {

            Debug.WriteLine(String.Format("          GetTcContentInlooxProjectFromProjectNumber (0)   Number {0}", pn));
            // wenn noch nix da ist...
            if (_tccontentinlooxprojectslist == null)
            {
                throw new Exception("Not implemented! _tccontentinlooxprojectslist should be a list on every time!");
            }


            TcContentInlooxProject p = null;

            // first, try to get Data from Memory
            Debug.WriteLine(String.Format("          GetTcContentInlooxProjectFromProjectNumber (1)   Number {0}", pn));
            
            p = _tccontentinlooxprojectslist.Find(item => item.Number == pn);    
            if (p != null)
            {
                return p;
            }                
            else
            {
                // now take the data from inloox direct
                Debug.WriteLine(String.Format("          GetTcContentInlooxProjectFromProjectNumber (2)   Number {0}", pn));

                try
                {
                              
                    TcContentInlooxProject new_p = new TcContentInlooxProject(pn);
                    Debug.WriteLine(String.Format("          GetTcContentInlooxProjectFromProjectNumber (2)   Missing Project!!! Number {0}", pn));                    
                    TcCIPL.Add(new_p);                    

                    //return new ApiProject();
                    return new_p;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(String.Format("          EXCEPTION GetTcContentInlooxProjectFromProjectNumber   {0}", ex.Message));
                }
            }            
            
            return null;

        }


    }
}
