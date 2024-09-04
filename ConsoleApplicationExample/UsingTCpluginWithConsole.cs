using System;
using System.Diagnostics;
using System.Threading.Tasks;

using TcPluginBase;
using TcPluginBase.Content;
using TcPluginBase.FileSystem;

using TcPlugins.HelperInloox11;
using TcPlugins.ContentInloox11;

namespace ConsoleApplicationExample
{
    internal class UsingTCpluginWithConsole
    {

        private static DateTime starttime;
        private static DateTime endtime;
        
        private static bool ForceOpenLoginDialog = false;

        private static bool LoginChecked = false;        

#pragma warning disable CS1998 // Bei der asynchronen Methode fehlen "await"-Operatoren. Die Methode wird synchron ausgeführt.
        public static async Task Main(string[] args)
#pragma warning restore CS1998 // Bei der asynchronen Methode fehlen "await"-Operatoren. Die Methode wird synchron ausgeführt.
        {

            starttime = DateTime.Now;
            logTimer(0, starttime);
            Console.WriteLine("console is starting...");
                        
            //ForceOpenLoginDialog = true;

            if (ForceOpenLoginDialog)
            {
                //InlooxDefaults.AuthToken = "";
                InlooxDefaults.KeepLoginDialogOpen = true;
            }
          
            Settings set = new Settings();
            ContentInloox11 ct = new ContentInloox11(set);

            LoginChecked = ct.LoginChecked;

            if (LoginChecked)
            {

                logTimer(0, starttime);
                DateTime lasttime = DateTime.Now;

                for (int i = 0; i < 3; i++)
                {
                    GetValueFlags f = GetValueFlags.DelayIfSlow;

                    if (i > 0)
                        f = GetValueFlags.None;

                    doWorkWDX(f, ct);
                    logTimer(i, starttime, lasttime);
                    lasttime = DateTime.Now;    
                    //Thread.Sleep(1000);
                }

                endtime = DateTime.Now;
                TimeSpan diff = endtime - starttime;
                Console.WriteLine(String.Format("ENTER to close the application ({0}.{1})",diff.Seconds, diff.Milliseconds));
                Console.ReadLine();

            }
            else
            {
                Console.WriteLine(String.Format("Canceled the login dialog!"));
                Console.WriteLine(String.Format("ENTER to close the application."));
                Console.ReadLine();
            }
        }

        private static void logTimer(int _counter, DateTime _starttime, DateTime? _laststeptime = null)
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
        
        private static void doWorkWDX(GetValueFlags flag, ContentInloox11 ct)
        {

            Console.WriteLine("doWorkWDX START now try next step...");

            //StringDictionary set = new StringDictionary();
            //InlooxContent ct = new InlooxContent(set);

            //foreach (string s in ContentTestString())
            foreach (string s in ShortContentTestString())
            {
                string fieldValue5 = "";
                ContentFieldType fieldType5;
                string fieldValue6 = "";
                ContentFieldType fieldType6;
                string fieldValue10 = "";
                ContentFieldType fieldType10;
                string fieldValue12 = "";
                ContentFieldType fieldType12;

                GetValueResult g5 = ct.GetValue(s, 5, 0, 2047, flag, out fieldValue5, out fieldType5);
                GetValueResult g6 = ct.GetValue(s, 6, 0, 2047, flag, out fieldValue6, out fieldType6);
                GetValueResult g10 = ct.GetValue(s, 10, 0, 2047, flag, out fieldValue10, out fieldType10);
                GetValueResult g12 = ct.GetValue(s, 12, 0, 2047, flag, out fieldValue12, out fieldType12);

                Console.WriteLine(String.Format("-------------->  PrjName: {0:12}  Kunde: {1:20} / {2:8} / {3:8} / {4}", fieldValue5.Left(12), fieldValue6.Left(20), fieldValue10.Right(8), fieldValue12.Right(8), s));
                Debug.WriteLine(String.Format("-------------->  PrjName: {0:12}  Kunde: {1:20} / {2:8} / {3:8} / {4}", fieldValue5.Left(12), fieldValue6.Left(20), fieldValue10.Right(8), fieldValue12.Right(8), s));
            }           

            endtime = DateTime.Now;

            TimeSpan diff = endtime - starttime;

            Console.WriteLine(String.Format("doWorkWDX END ({0}.{1})", diff.Seconds, diff.Milliseconds));

            //Console.WriteLine("doWorkWDX  END");
        }

        

        private static string[] ShortContentTestString()
        {
            string[] r = new string[14] {
                    "s:\\Konstruktion\\Projekte\\2023_11039\\",
                    "s:\\Konstruktion\\Projekte\\2023_11043\\",
                    "s:\\Konstruktion\\Projekte\\8024_11350\\",
                    "s:\\Konstruktion\\Projekte\\2023_11045\\",
                    "s:\\Konstruktion\\Projekte\\2023_11133\\",
                    "s:\\Konstruktion\\Projekte\\2023_11069\\",
                    "s:\\Konstruktion\\Projekte\\2023_11070\\",
                    "s:\\Konstruktion\\Projekte\\2023_11080\\",
                    "s:\\Konstruktion\\Projekte\\2023_11092\\",
                    "s:\\Konstruktion\\Projekte\\2023_11093\\",
                    "s:\\Konstruktion\\Projekte\\2023_11094\\",
                    "s:\\Konstruktion\\Projekte\\2023_11109\\",
                    "s:\\Konstruktion\\Projekte\\2023_11110\\",
                    "s:\\Konstruktion\\Projekte\\2023_11112\\"
                };

            return r;
        }

        private static string[] ContentTestString()
        {
            string[] r = new string[175] {
                    "s:\\Konstruktion\\Projekte\\2023_11039\\",
                    "s:\\Konstruktion\\Projekte\\2023_11043\\",
                    "s:\\Konstruktion\\Projekte\\8024_11350\\",
                    "s:\\Konstruktion\\Projekte\\2023_11045\\",
                    "s:\\Konstruktion\\Projekte\\2023_11133\\",
                    "s:\\Konstruktion\\Projekte\\2023_11069\\",
                    "s:\\Konstruktion\\Projekte\\2023_11070\\",
                    "s:\\Konstruktion\\Projekte\\2023_11080\\",
                    "s:\\Konstruktion\\Projekte\\2023_11112\\",
                    "s:\\Konstruktion\\Projekte\\2023_11116\\",
                    "s:\\Konstruktion\\Projekte\\2023_11123\\",
                    "s:\\Konstruktion\\Projekte\\2023_11124\\",
                    "s:\\Konstruktion\\Projekte\\2023_11125\\",
                    "s:\\Konstruktion\\Projekte\\2023_11127\\",
                    "s:\\Konstruktion\\Projekte\\2023_11128\\",
                    "s:\\Konstruktion\\Projekte\\2023_11131\\",
                    "s:\\Konstruktion\\Projekte\\2023_11133\\",
                    "s:\\Konstruktion\\Projekte\\2023_11136\\",
                    "s:\\Konstruktion\\Projekte\\2023_11141\\",
                    "s:\\Konstruktion\\Projekte\\2023_11143\\",
                    "s:\\Konstruktion\\Projekte\\2023_11145\\",
                    "s:\\Konstruktion\\Projekte\\2023_11146\\",
                    "s:\\Konstruktion\\Projekte\\2023_11149\\",
                    "s:\\Konstruktion\\Projekte\\2023_11152\\",
                    "s:\\Konstruktion\\Projekte\\2023_11153\\",
                    "s:\\Konstruktion\\Projekte\\2023_11154\\",
                    "s:\\Konstruktion\\Projekte\\2023_11160\\",
                    "s:\\Konstruktion\\Projekte\\2023_11162\\",
                    "s:\\Konstruktion\\Projekte\\2023_11165\\",
                    "s:\\Konstruktion\\Projekte\\2023_11167\\",
                    "s:\\Konstruktion\\Projekte\\2023_11168\\",
                    "s:\\Konstruktion\\Projekte\\2023_11171\\",
                    "s:\\Konstruktion\\Projekte\\2023_11172\\",
                    "s:\\Konstruktion\\Projekte\\2023_11173\\",
                    "s:\\Konstruktion\\Projekte\\2023_11174\\",
                    "s:\\Konstruktion\\Projekte\\2023_11175\\",
                    "s:\\Konstruktion\\Projekte\\2023_11182\\",
                    "s:\\Konstruktion\\Projekte\\2023_11183\\",
                    "s:\\Konstruktion\\Projekte\\2023_11184\\",
                    "s:\\Konstruktion\\Projekte\\2023_11185\\",
                    "s:\\Konstruktion\\Projekte\\2023_11187\\",
                    "s:\\Konstruktion\\Projekte\\2023_11188\\",
                    "s:\\Konstruktion\\Projekte\\2023_11190\\",
                    "s:\\Konstruktion\\Projekte\\2023_11191\\",
                    "s:\\Konstruktion\\Projekte\\2023_11198\\",
                    "s:\\Konstruktion\\Projekte\\2023_11201\\",
                    "s:\\Konstruktion\\Projekte\\2023_11202\\",
                    "s:\\Konstruktion\\Projekte\\2023_11203\\",
                    "s:\\Konstruktion\\Projekte\\2023_11204\\",
                    "s:\\Konstruktion\\Projekte\\2023_11205\\",
                    "s:\\Konstruktion\\Projekte\\2023_11210\\",
                    "s:\\Konstruktion\\Projekte\\2023_11213\\",
                    "s:\\Konstruktion\\Projekte\\2023_11219\\",
                    "s:\\Konstruktion\\Projekte\\2023_11220\\",
                    "s:\\Konstruktion\\Projekte\\2023_11225\\",
                    "s:\\Konstruktion\\Projekte\\2023_11226\\",
                    "s:\\Konstruktion\\Projekte\\2023_11227\\",
                    "s:\\Konstruktion\\Projekte\\2023_11228\\",
                    "s:\\Konstruktion\\Projekte\\2023_11229\\",
                    "s:\\Konstruktion\\Projekte\\2023_11231\\",
                    "s:\\Konstruktion\\Projekte\\2023_11232\\",
                    "s:\\Konstruktion\\Projekte\\2023_11233\\",
                    "s:\\Konstruktion\\Projekte\\2023_11234\\",
                    "s:\\Konstruktion\\Projekte\\2023_11235\\",
                    "s:\\Konstruktion\\Projekte\\2023_11236\\",
                    "s:\\Konstruktion\\Projekte\\2023_11237\\",
                    "s:\\Konstruktion\\Projekte\\2023_11238\\",
                    "s:\\Konstruktion\\Projekte\\2023_11239\\",
                    "s:\\Konstruktion\\Projekte\\2023_11240\\",
                    "s:\\Konstruktion\\Projekte\\2023_11241\\",
                    "s:\\Konstruktion\\Projekte\\2023_11242\\",
                    "s:\\Konstruktion\\Projekte\\2023_11243\\",
                    "s:\\Konstruktion\\Projekte\\2023_11244\\",
                    "s:\\Konstruktion\\Projekte\\2023_11245\\",
                    "s:\\Konstruktion\\Projekte\\2023_11246\\",
                    "s:\\Konstruktion\\Projekte\\2023_11248\\",
                    "s:\\Konstruktion\\Projekte\\2024_11253\\",
                    "s:\\Konstruktion\\Projekte\\2024_11254\\",
                    "s:\\Konstruktion\\Projekte\\2024_11255\\",
                    "s:\\Konstruktion\\Projekte\\2024_11256\\",
                    "s:\\Konstruktion\\Projekte\\2024_11257\\",
                    "s:\\Konstruktion\\Projekte\\2024_11258\\",
                    "s:\\Konstruktion\\Projekte\\2024_11263\\",
                    "s:\\Konstruktion\\Projekte\\2024_11264\\",
                    "s:\\Konstruktion\\Projekte\\2024_11265\\",
                    "s:\\Konstruktion\\Projekte\\2024_11266\\",
                    "s:\\Konstruktion\\Projekte\\2024_11267\\",
                    "s:\\Konstruktion\\Projekte\\2024_11268\\",
                    "s:\\Konstruktion\\Projekte\\2024_11269\\",
                    "s:\\Konstruktion\\Projekte\\2024_11270\\",
                    "s:\\Konstruktion\\Projekte\\2024_11271\\",
                    "s:\\Konstruktion\\Projekte\\2024_11272\\",
                    "s:\\Konstruktion\\Projekte\\2024_11273\\",
                    "s:\\Konstruktion\\Projekte\\2024_11274\\",
                    "s:\\Konstruktion\\Projekte\\2024_11275\\",
                    "s:\\Konstruktion\\Projekte\\2024_11276\\",
                    "s:\\Konstruktion\\Projekte\\2024_11277\\",
                    "s:\\Konstruktion\\Projekte\\2024_11278\\",
                    "s:\\Konstruktion\\Projekte\\2024_11279\\",
                    "s:\\Konstruktion\\Projekte\\2024_11280\\",
                    "s:\\Konstruktion\\Projekte\\2024_11281\\",
                    "s:\\Konstruktion\\Projekte\\2024_11282\\",
                    "s:\\Konstruktion\\Projekte\\2024_11283\\",
                    "s:\\Konstruktion\\Projekte\\2024_11284\\",
                    "s:\\Konstruktion\\Projekte\\2024_11285\\",
                    "s:\\Konstruktion\\Projekte\\2024_11286\\",
                    "s:\\Konstruktion\\Projekte\\2024_11287\\",
                    "s:\\Konstruktion\\Projekte\\2024_11288\\",
                    "s:\\Konstruktion\\Projekte\\2024_11289\\",
                    "s:\\Konstruktion\\Projekte\\2024_11290\\",
                    "s:\\Konstruktion\\Projekte\\2024_11293\\",
                    "s:\\Konstruktion\\Projekte\\2024_11294\\",
                    "s:\\Konstruktion\\Projekte\\2024_11295\\",
                    "s:\\Konstruktion\\Projekte\\2024_11296\\",
                    "s:\\Konstruktion\\Projekte\\2024_11297\\",
                    "s:\\Konstruktion\\Projekte\\2024_11298\\",
                    "s:\\Konstruktion\\Projekte\\2024_11299\\",
                    "s:\\Konstruktion\\Projekte\\2024_11300\\",
                    "s:\\Konstruktion\\Projekte\\2024_11301\\",
                    "s:\\Konstruktion\\Projekte\\2024_11303\\",
                    "s:\\Konstruktion\\Projekte\\2024_11304\\",
                    "s:\\Konstruktion\\Projekte\\2024_11305\\",
                    "s:\\Konstruktion\\Projekte\\2024_11306\\",
                    "s:\\Konstruktion\\Projekte\\2024_11308\\",
                    "s:\\Konstruktion\\Projekte\\2024_11310\\",
                    "s:\\Konstruktion\\Projekte\\2024_11311\\",
                    "s:\\Konstruktion\\Projekte\\2024_11312\\",
                    "s:\\Konstruktion\\Projekte\\2024_11313\\",
                    "s:\\Konstruktion\\Projekte\\2024_11314\\",
                    "s:\\Konstruktion\\Projekte\\2024_11315\\",
                    "s:\\Konstruktion\\Projekte\\2024_11316\\",
                    "s:\\Konstruktion\\Projekte\\2024_11317\\",
                    "s:\\Konstruktion\\Projekte\\2024_11318\\",
                    "s:\\Konstruktion\\Projekte\\2024_11319\\",
                    "s:\\Konstruktion\\Projekte\\2024_11320\\",
                    "s:\\Konstruktion\\Projekte\\2024_11321\\",
                    "s:\\Konstruktion\\Projekte\\2021_10609\\",
                    "s:\\Konstruktion\\Projekte\\2021_10610\\",
                    "s:\\Konstruktion\\Projekte\\2021_10611\\",
                    "s:\\Konstruktion\\Projekte\\2021_10612\\",
                    "s:\\Konstruktion\\Projekte\\2021_10615\\",
                    "s:\\Konstruktion\\Projekte\\2021_10616\\",
                    "s:\\Konstruktion\\Projekte\\2021_10617\\",
                    "s:\\Konstruktion\\Projekte\\2021_10618\\",
                    "s:\\Konstruktion\\Projekte\\2021_10619\\",
                    "s:\\Konstruktion\\Projekte\\2021_10626\\",
                    "s:\\Konstruktion\\Projekte\\2021_10627\\",
                    "s:\\Konstruktion\\Projekte\\2021_10628\\",
                    "s:\\Konstruktion\\Projekte\\2021_10629\\",
                    "s:\\Konstruktion\\Projekte\\2021_10630\\",
                    "s:\\Konstruktion\\Projekte\\2021_10634\\",
                    "s:\\Konstruktion\\Projekte\\2021_10635\\",
                    "s:\\Konstruktion\\Projekte\\2021_10636\\",
                    "s:\\Konstruktion\\Projekte\\2021_10637\\",
                    "s:\\Konstruktion\\Projekte\\2021_10638\\",
                    "s:\\Konstruktion\\Projekte\\2021_10643\\",
                    "s:\\Konstruktion\\Projekte\\2021_10644\\",
                    "s:\\Konstruktion\\Projekte\\2021_10645\\",
                    "s:\\Konstruktion\\Projekte\\2021_10646\\",
                    "s:\\Konstruktion\\Projekte\\2021_10652\\",
                    "s:\\Konstruktion\\Projekte\\2021_10654\\",
                    "s:\\Konstruktion\\Projekte\\2021_10655\\",
                    "s:\\Konstruktion\\Projekte\\2021_10656\\",
                    "s:\\Konstruktion\\Projekte\\2021_10657\\",
                    "s:\\Konstruktion\\Projekte\\2021_10658\\",
                    "s:\\Konstruktion\\Projekte\\2021_10659\\",
                    "s:\\Konstruktion\\Projekte\\2021_10660\\",
                    "s:\\Konstruktion\\Projekte\\2021_10661\\",
                    "s:\\Konstruktion\\Projekte\\2021_10662\\",
                    "s:\\Konstruktion\\Projekte\\2021_10663\\",
                    "s:\\Konstruktion\\Projekte\\2021_10664\\",
                    "s:\\Konstruktion\\Projekte\\2021_10665\\",
                    "s:\\Konstruktion\\Projekte\\2022_10842\\datei.pdf",
                    "s:\\Konstruktion\\Projekte\\2023_11024\\programm.exe",
                    "s:\\Konstruktion\\Projekte\\2023_11133\\"
                };

            return r;
        }

    }
}

