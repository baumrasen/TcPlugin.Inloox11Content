using System;
using System.Threading.Tasks;

using TcPlugins.HelperInloox11;
using InLoox.PM.Domain.Model.Aggregates.Api;

namespace ConsoleApplicationExample
{
    internal class InlooxClientOnly
    {

        private static DateTime starttime;
        private static DateTime endtime;

        private static bool ForceOpenLoginDialog = false;

        private static bool LoginChecked = false;        

        public static async Task Main(string[] args)
        {

            starttime = DateTime.Now;
            HelperFunctionsFromTcPlugins.logTimer(0, starttime);
            Console.WriteLine("console is starting...");           
            
            ForceOpenLoginDialog = false;

            if (ForceOpenLoginDialog)
            {
                //InlooxDefaults.AuthToken = "";
                InlooxDefaults.KeepLoginDialogOpen = true;
            }

            LoginChecked = HelperFunctionsFromTcPlugins.CheckForLogin();

            if (LoginChecked)
            {

                // example 1: Show name of your account
                var accountInfo = await InlooxDefaults.GetAccountInfo();

                Console.WriteLine(accountInfo.Name);
                HelperFunctionsFromTcPlugins.logTimer(0, starttime);

                var projects100 = await InlooxDefaults.GetFirst100Projects();

                foreach (ApiProject p in projects100)
                {
                    Console.WriteLine(p.Number + " - " + p.Name);
                }

                var projects = await InlooxDefaults.GetProjectsFromNumberAsync("2023_11043");

                foreach (CustomApiDynamicProject p in projects)
                {
                    //Console.WriteLine("CustomApiDynamicProject " + p.Project_Number + " - " + p.Project_Number);
                    // customize for testing custom field
                    Console.WriteLine("CustomApiDynamicProject " + p.Project_Number + " - " + p.Project_Number);
                }

                var project = await InlooxDefaults.GetApiProjectFromNumberAsync("2023_11043");

                Console.WriteLine("ApiProject " + project.Number + " - " + project.Name);


                await InlooxDefaults.GetAllTimeEntriesForMonth(DateTime.Now, a => Console.WriteLine(a));

                var timeentries = await InlooxDefaults.GetAllTimeEntriesForMonthMod(DateTime.Now);

                foreach (ApiDynamicTimeEntry t in timeentries)
                {
                    // Console.WriteLine(t.TimeEntry_PerformedByDisplayName + " - " + t.TimeEntry_DurationHours + " - " + t.TimeEntry_DisplayName + " - " + t.Project_Number + " - " + t.TimeEntry_StartDateTime + " - taskItemId " + t.TaskItem_TaskItemId);
                    Console.WriteLine(t.TimeEntry_PerformedByDisplayName + " - " + t.TimeEntry_DurationHours + " - " + t.TimeEntry_DisplayName + " - " + t.Project_Number + " - " + t.TimeEntry_StartDateTime + " - " + t.TaskItem_Name);

                    var te = await InlooxDefaults.GetTimeEntryFromId(t.TimeEntry_TimeEntryId);
                    Console.WriteLine(te.TaskItemId);
                }

                doContent();

                endtime = DateTime.Now;
                TimeSpan diff = endtime - starttime;
                Console.WriteLine(String.Format("ENTER to close the application ({0}.{1})", diff.Seconds, diff.Milliseconds));
                Console.ReadLine();

            }
            else
            {
                Console.WriteLine(String.Format("Canceled the login dialog!"));
                Console.WriteLine(String.Format("ENTER to close the application."));
                Console.ReadLine();
            }

            
        }

        private static async void doContent()
        {

            Boolean gotinfo = true;
            String division = "Creo";
            String projectnumber = "2024_11294";

            ApiDynamicProject p = null;

            if ((gotinfo) && (division != ""))
            {
                var p2 = await InlooxDefaults.GetProjectsFromNumberAsync(projectnumber);

                foreach (ApiDynamicProject _p in p2)
                {
                    //Console.WriteLine(p.Project_Number + " - " + p.Project_Name);
                    p = _p;
                }

                //p = InlooxDefaults.GetApiProjectFromNumber(projectnumber);
            }
            else if ((gotinfo) && (projectnumber != ""))
            {                
                //iinfo = iil.ReturnFromLocalCacheWithTempProjectNumber(projectnumber);
                var p2 = await InlooxDefaults.GetProjectsFromNumberAsync(projectnumber);

                foreach (ApiDynamicProject _p in p2)
                {
                    //Console.WriteLine(p.Project_Number + " - " + p.Project_Name);
                    p = _p;
                }
                //p = InlooxDefaults.GetApiProjectFromNumber(projectnumber);

            }

        }

    }
}

