using Inloox11OdataClientApp.Default;
using IQmedialab.InLoox.Data.BusinessObjects;
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

namespace AV.TotalCommander.TcPlugins.InlooxHelper
{
    public class Inloox11InfoList
    {

        private bool _gotData = false;
        private bool _gettingDataNow = false;

        private static Container _ic = null;
        private static string _authtoken = "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyy dummy aus InlooxInfoList";
        private static Uri _inlooxuri = new Uri("http://v-smg-101.ibb.time-partner.com:8989/odata/");

        private int inlooxfavcounter = 0;
        private int inlooxfav2counter = 0;

        private int inlooxprojectcounter = 0;
        private int inlooxproject2counter = 0;

        private List<InlooxInfo> inlooxInfoList = new List<InlooxInfo>();
        private List<string> divisionList = new List<string>();
        private List<Guid> favoriteIdList = new List<Guid>();

        private Guid[] favoritesIdArray = null;
        private InlooxInfo[] fullFavoritesInfoArray = null;

        private Guid[] projectIdArray = null;
        private InlooxInfo[] fullProjectsInfoArray = null;

        private static DateTime LastFavCheck;
        private static DateTime LastProjectCheck;
        // 30 Sekunden
        private static TimeSpan DoCheckAgainAfter = new TimeSpan(0, 0, 30);

        private Thread workerThread1;

        #region contructor

        public Inloox11InfoList()
        {
            Initialice_IC();            
        }

        #endregion

        #region Properties
        public InlooxInfo[] FullFavoritesInfoArray
        {
            get
            {
                //if (UseFavCacheNow() && (fullFavoritesInfoArray != null))
                //{
                //    return fullFavoritesInfoArray;
                //}
                //else
                //{
                //    //BuildLogicalFavoriteArray();
                //    return fullFavoritesInfoArray;
                //}

                return fullFavoritesInfoArray;
            }
        }

        public Guid[] FavoritesIdArray
        {
            get
            {
                //if (UseFavCacheNow() && (favoritesIdArray != null))
                //{
                //    return favoritesIdArray;
                //}
                //else
                //{
                //    //BuildLogicalFavoriteArray();
                //    return favoritesIdArray;
                //}

                return favoritesIdArray;
            }
        }

        public InlooxInfo[] FullProjectsInfoArray
        {
            get
            {
                //if (UseProjectCacheNow() && (fullProjectsInfoArray != null))
                //{
                //    return fullProjectsInfoArray;
                //}
                //else
                //{
                //    //BuildLogicalProjectsLists();
                //    return fullProjectsInfoArray;
                //}
                return inlooxInfoList.ToArray();
            }
        }

        public List<InlooxInfo> FullProjectsInfoList
        {
            get
            {
                //if (UseProjectCacheNow() && (fullProjectsInfoArray != null))
                //{
                //    return fullProjectsInfoArray;
                //}
                //else
                //{
                //    //BuildLogicalProjectsLists();
                //    return fullProjectsInfoArray;
                //}
                return inlooxInfoList;
            }
        }

        public Guid[] ProjectsIdArray
        {
            get
            {
                //if (UseFavCacheNow() && (projectIdArray != null))
                //{
                //    return projectIdArray;
                //}
                //else
                //{
                //    //BuildLogicalProjectsLists();
                //    return projectIdArray;
                //}

                return projectIdArray;
            }
        }
        #endregion

        private void BuildLogicalFavoriteArray()
        {

            //if (UseFavCacheNow() && (favoritesIdArray != null))
            //{
            //    return;
            //}

            Debug.WriteLine(String.Format("        BuildLogicalFavoriteArray START: {0}", DateTime.Now));

            // else
            var fav = _ic.favorite;
            inlooxfavcounter = CurrentInlooxFavoritesCount();

            Guid[] tempResult = new Guid[inlooxfavcounter];
            InlooxInfo[] tempResult2 = new InlooxInfo[inlooxfavcounter];

            int count = 0;
            foreach (Favorite f in fav)
            {

                if (f.ProjectId != null)
                {
                    Debug.WriteLine(String.Format("      BuildLogicalFavoriteArray - FavId/BusinessObjectId: {0} / {1}", f.FavoriteId, f.BusinessObjectId));
                    Guid pid = (Guid)f.ProjectId;

                    // Abbruch, wenn die ProjektId schon enthalten ist
                    if (tempResult.Contains(pid))
                    {
                        Debug.WriteLine(String.Format("        Get Inloox Fav (Pid) - pID    still EXIST: {0}", pid));
                        break;
                    }


                    InlooxInfo ii = new InlooxInfo(pid, _ic);
                    //string d = GetDocumentFolder(pid);
                    string d = ii.DocumentFolder;
                    //if (Directory.Exists(d))
                    //{
                    //    Debug.WriteLine(String.Format("        Get Inloox Fav (Pid) - Dir     EXIST: {0}", d));

                        tempResult[count] = (Guid)f.ProjectId;
                        tempResult2[count] = ii;
                        count++;
                    //}
                    //else
                    //{
                    //    Debug.WriteLine(String.Format("        Get Inloox Fav (Pid) - Dir not EXIST: {0}", d));
                    //}
                }
                else if (f.BusinessObjectId != null)
                {
                    Debug.WriteLine(String.Format("      BuildLogicalFavoriteArray - FavId/BusinessObjectId: {0} / {1}", f.FavoriteId, f.BusinessObjectId));
                    Guid pid = (Guid)f.BusinessObjectId;

                    // Abbruch, wenn die ProjektId schon enthalten ist
                    if (tempResult.Contains(pid))
                    {
                        Debug.WriteLine(String.Format("        Get Inloox Fav (Pid) - pID    still EXIST: {0}", pid));
                        break;
                    }

                    InlooxInfo ii = new InlooxInfo(pid, _ic);
                    //string d = GetDocumentFolder(pid);
                    //string d = ii.DocumentFolder;
                    //if (Directory.Exists(d))
                    //{
                        //Debug.WriteLine(String.Format("        Get Inloox Fav (BOid) - Dir     EXIST: {0}", d));
                        tempResult[count] = (Guid)f.BusinessObjectId;
                        tempResult2[count] = ii;
                        count++;
                    //}
                    //else
                    //{
                    //    Debug.WriteLine(String.Format("        Get Inloox Fav (BOid) - Dir not EXIST: {0}", d));
                    //}
                }
            }

            Guid[] result = new Guid[count];
            int count_r = 0;

            foreach (Guid g in tempResult)
            {
                Debug.WriteLine(String.Format("        Get Inloox Fav (build result): {0} - {1}", count_r, result.Length));
                if ((g != new Guid()) && count_r < result.Length)
                {
                    result[count_r++] = g;
                }
            }

            InlooxInfo[] result2 = new InlooxInfo[count];
            count_r = 0;

            foreach (InlooxInfo g in tempResult2)
            {
                Debug.WriteLine(String.Format("        Get Inloox Fav (build result2): {0} - {1}", count_r, result.Length));
                if ((g != null) && count_r < result.Length)
                {
                    result2[count_r++] = g;
                }
            }

            Debug.WriteLine(String.Format("        BuildLogicalFavoriteArray ENDE: {0}", DateTime.Now));

            favoritesIdArray = result;
            fullFavoritesInfoArray = result2;

        }

         private void BuildLogicalProjectsLists()
        {

            //if (UseProjectCacheNow() && (projectIdArray != null))
            //{
            //    return;
            //}

            Debug.WriteLine(String.Format("        BuildLogicalProjectsLists START: {0}", DateTime.Now));

            // else
            var projectview = _ic.projectview;
            inlooxprojectcounter = CurrentInlooxProjectsCount();

            Guid[] tempResult = new Guid[inlooxprojectcounter];
            InlooxInfo[] tempResult2 = new InlooxInfo[inlooxprojectcounter];

            int count = 0;
            foreach (ProjectView pv in projectview)
            {
                if (pv.ProjectId != null)
                {
                    Debug.WriteLine(String.Format("      BuildLogicalProjectsLists - pId/Number: {0} / {1}", pv.ProjectId, pv.Number));
                    Guid pid = (Guid)pv.ProjectId;

                    // Abbruch, wenn die ProjektId schon enthalten ist
                    if (tempResult.Contains(pid))
                    {
                        Debug.WriteLine(String.Format("        Get Inloox Fav (Pid) - pID    still EXIST: {0}", pid));
                        break;
                    }

                    InlooxInfo ii = new InlooxInfo(pid, _ic);
                    //string d = GetDocumentFolder(pid);
                    //string d = ii.DocumentFolder;
                    //if (Directory.Exists(d))
                    //{
                    //    Debug.WriteLine(String.Format("        Get Inloox Fav (Pid) - Dir     EXIST: {0}", d));

                        tempResult[count] = (Guid)pv.ProjectId;
                        tempResult2[count] = ii;
                        count++;
                    //}
                    //else
                    //{
                    //    Debug.WriteLine(String.Format("        Get Inloox Fav (Pid) - Dir not EXIST: {0}", d));
                    //}
                }
                //else if (f.BusinessObjectId != null)
                //{
                //    Debug.WriteLine(String.Format("      BuildLogicalProjectsLists - FavId/BusinessObjectId: {0} / {1}", f.FavoriteId, f.BusinessObjectId));
                //    Guid pid = (Guid)f.BusinessObjectId;

                //    // Abbruch, wenn die ProjektId schon enthalten ist
                //    if (tempResult.Contains(pid))
                //    {
                //        Debug.WriteLine(String.Format("        Get Inloox Fav (Pid) - pID    still EXIST: {0}", pid));
                //        break;
                //    }

                //    InlooxInfo ii = new InlooxInfo(pid, _ic);
                //    //string d = GetDocumentFolder(pid);
                //    string d = ii.DocumentFolder;
                //    if (Directory.Exists(d))
                //    {
                //        Debug.WriteLine(String.Format("        Get Inloox Fav (BOid) - Dir     EXIST: {0}", d));
                //        tempResult[count] = (Guid)f.BusinessObjectId;
                //        tempResult2[count] = ii;
                //        count++;
                //    }
                //    else
                //    {
                //        Debug.WriteLine(String.Format("        Get Inloox Fav (BOid) - Dir not EXIST: {0}", d));
                //    }
                //}
            }

            Guid[] result = new Guid[count];
            int count_r = 0;

            foreach (Guid g in tempResult)
            {
                Debug.WriteLine(String.Format("        Get Inloox Fav (build result): {0} - {1}", count_r, result.Length));
                if ((g != new Guid()) && count_r < result.Length)
                {
                    result[count_r++] = g;
                }
            }

            InlooxInfo[] result2 = new InlooxInfo[count];
            count_r = 0;

            foreach (InlooxInfo g in tempResult2)
            {
                Debug.WriteLine(String.Format("        Get Inloox Fav (build result2): {0} - {1}", count_r, result.Length));
                if ((g != null) && count_r < result.Length)
                {
                    result2[count_r++] = g;
                }
            }

            Debug.WriteLine(String.Format("        BuildLogicalProjectsLists ENDE: {0}", DateTime.Now));

            projectIdArray = result;
            fullProjectsInfoArray = result2;

        }

        private bool UseFavCacheNow()
        {
            //if (LastCheck == null)
            //{
            //    inlooxfav2counter = CurrentInlooxFavoritesCount();
            //    LastCheck = DateTime.Now;
            //    // do check, because of first check
            //    Debug.WriteLine(String.Format("              UseCacheNow InlooxInfo: {0}", DateTime.Now));
            //    return false;
            //}
            if (LastFavCheck.Add(DoCheckAgainAfter) > DateTime.Now)
            {
                // do not check, because of last check is not long ago
                Debug.WriteLine(String.Format("              UseFavCacheNow InlooxInfo: {0} > {1}", LastFavCheck.Add(DoCheckAgainAfter), DateTime.Now));
                return true;
            }
            else if (inlooxfav2counter == CurrentInlooxFavoritesCount())
            {
                // do check count, because of last check is long ago (check equal)
                Debug.WriteLine(String.Format("              UseFavCacheNow InlooxInfo: {0} < {1} Count was checked (check equal)", LastFavCheck.Add(DoCheckAgainAfter), DateTime.Now));
                LastFavCheck = DateTime.Now;
                return true;
            }
            else
            {
                // do check count, because of last check is long ago (check not equal)
                Debug.WriteLine(String.Format("              UseFavCacheNow InlooxInfo: {0} < {1} Count was checked (check not equal)", LastFavCheck.Add(DoCheckAgainAfter), DateTime.Now));
                inlooxfav2counter = CurrentInlooxFavoritesCount();
                LastFavCheck = DateTime.Now;
                return false;
            }
        }

        private bool UseProjectCacheNow()
        {
            //if (LastCheck == null)
            //{
            //    inlooxfav2counter = CurrentInlooxFavoritesCount();
            //    LastCheck = DateTime.Now;
            //    // do check, because of first check
            //    Debug.WriteLine(String.Format("              UseCacheNow InlooxInfo: {0}", DateTime.Now));
            //    return false;
            //}
            if (LastProjectCheck.Add(DoCheckAgainAfter) > DateTime.Now)
            {
                // do not check, because of last check is not long ago
                Debug.WriteLine(String.Format("              UseProjectCacheNow InlooxInfo: {0} > {1}", LastProjectCheck.Add(DoCheckAgainAfter), DateTime.Now));
                return true;
            }
            else if (inlooxproject2counter == CurrentInlooxProjectsCount())
            {
                // do check count, because of last check is long ago (check equal)
                Debug.WriteLine(String.Format("              UseProjectCacheNow InlooxInfo: {0} < {1} Count was checked (check equal)", LastProjectCheck.Add(DoCheckAgainAfter), DateTime.Now));
                LastProjectCheck = DateTime.Now;
                return true;
            }
            else
            {
                // do check count, because of last check is long ago (check not equal)
                Debug.WriteLine(String.Format("              UseProjectCacheNow InlooxInfo: {0} < {1} Count was checked (check not equal)", LastProjectCheck.Add(DoCheckAgainAfter), DateTime.Now));
                inlooxproject2counter = CurrentInlooxProjectsCount();
                LastProjectCheck = DateTime.Now;
                return false;
            }
        }

        private int CurrentInlooxFavoritesCount()
        {
            var fav = _ic.favorite;
            int inlooxcount = _ic.favorite.Count();
            return inlooxcount;
        }

        private int CurrentInlooxProjectsCount()
        {
            var p = _ic.projectview;
            int inlooxcount = _ic.projectview.Count();
            return inlooxcount;
        }

        public string GetLogicalPathFromNumber(String ProjectNumber)
        {

            //var fav = _ic.Favorite;

            // foreach (Favorite f in fav)            
            //{

            //    if (f.ProjectId != null)
            //    {
            //        Guid pid = (Guid)f.ProjectId;
            //        string d = GetDocumentFolder(pid);
            //        if (Directory.Exists(d))
            //            if (d.Contains(ProjectNumber))
            //                return d;
            //    }
            //    else if (f.BusinessObjectId != null)
            //    {
            //        Guid pid = (Guid)f.BusinessObjectId;
            //        string d = GetDocumentFolder(pid);
            //        if (Directory.Exists(d))
            //            if (d.Contains(ProjectNumber))
            //                return d;
            //    }

            //}

            Debug.WriteLine(String.Format("{0}              GetLogicalPathFromNumber InlooxInfo: {1} ", DateTime.Now, ProjectNumber));

            if (FullFavoritesInfoArray == null)
                Debug.WriteLine(String.Format("{0}    ##!!!##     GetLogicalPathFromNumber InlooxInfo: {1} {2} is NULL", DateTime.Now, ProjectNumber, "FullFavoritesInfoArray"));

            foreach (InlooxInfo f in FullFavoritesInfoArray)
            {

                if (f.ProjectId != null)
                {
                    Guid pid = (Guid)f.ProjectId;
                    string d = GetDocumentFolder(pid);
                    if (Directory.Exists(d))
                        if (d.Contains(ProjectNumber))
                            return d;
                }

            }

            return "//";
        }

        public InlooxInfo GetInlooxInfoFromDocumentFolder(string df)
        {

            Debug.WriteLine(String.Format("          GetInlooxInfoFromDocumentFolder   Folder/id {0}", df));
            foreach (InlooxInfo ii in FullFavoritesInfoArray)
            {
                if (ii.DocumentFolder == df)
                    return ii;
            }

            // old system should not be used because of bad performache
            Guid id = GetProjectIdFromDocumentFolder(df);
            Debug.WriteLine(String.Format("          GetInlooxInfoFromDocumentFolder   Folder/id {0}/{1}", df, id));
            return new InlooxInfo(id, ic);
        }

        public InlooxInfo GetInlooxInfoFromProjectNumber(string pn)
        {

            // wenn noch nix da ist...
            if (FullProjectsInfoArray == null)
            {
                InlooxInfo nii = new InlooxInfo(pn, _ic);
                inlooxInfoList.Add(nii);
                return nii;
            }

            // first, try to get Data from Memory
            Debug.WriteLine(String.Format("          GetInlooxInfoFromProjectNumber (1)   Number {0}", pn));
            foreach (InlooxInfo ii in FullProjectsInfoArray)
            {
                if (ii.GotData)
                    if (ii.Number == pn)
                        return ii;
            }

            // now take the data from inloox direct
            Debug.WriteLine(String.Format("          GetInlooxInfoFromProjectNumber (2)   Number {0}", pn));

            Guid pid = GetFromServer_ProjectId_etc_FromProjectNumber(pn);            
            return GetFromLocal_InlooxInfo_FromProjectId(pid);


        }

        public InlooxInfo GetInlooxInfoFromDivision(string d, string pn)
        {

            // wenn noch nix da ist...
            //if (FullProjectsInfoArray == null)
            //{
            //    return CheckForNewDivision(d,pn);
            //}
            //else if (FullProjectsInfoArray.Count() == 0)
            //{
            //    return CheckForNewDivision(d,pn);
            //}

            if (inlooxInfoList.Count == 0)
            {

                Debug.WriteLine(String.Format("          GetInlooxInfoFromDivision - ListCount=0   Division {0} Number {1}", d, pn));
                return CheckForNewDivision(d, pn);
            }

            // first, try to get Data from Memory
            Debug.WriteLine(String.Format("          GetInlooxInfoFromProjectNumber (1)   Div/Number {0}/{1}", d, pn));
            //foreach (InlooxInfo ii in FullProjectsInfoArray)
            //{
            //    if (ii.GotData)
            //    {
            //        if (ii.Number == pn)
            //            return ii;
            //    }
            //    else if (ii.TempNumber == pn)
            //        return ii;
            //    // Performache Bremse !!! --> 
            //    // Lag an Debug-Ausgabe
            //    else if (ii.Number == pn)
            //        return ii;

            //}

            InlooxInfo rflcwpn = ReturnFromLocalCacheWithTempProjectNumber(pn);
            if (rflcwpn != null)
                return rflcwpn;

            // did not found anything
            // create new InlooxInfo
            //InlooxInfo nii = new InlooxInfo(pn, _ic);


            // and return it

            // now take the data from inloox direct
            Debug.WriteLine(String.Format("          GetInlooxInfoFromProjectNumber (2)   Number {0}", pn));
            //return nii;
            return CheckForAddOdExistingInlooxInfo(pn);

            //Guid pid = GetFromServer_ProjectId_etc_FromProjectNumber(d, pn);
            //return GetFromLocal_InlooxInfo_FromProjectId(pid);

        }

        private InlooxInfo ReturnFromLocalCacheWithTempProjectNumber(string pn)
        {
            //foreach (InlooxInfo ii in FullProjectsInfoArray)
            //{
            //    if (ii.GotData)
            //    {
            //        if (ii.Number == pn)
            //            return ii;
            //    }
            //    else if (ii.TempNumber == pn)
            //        return ii;
            //    // Performache Bremse !!! --> 
            //    // Lag an Debug-Ausgabe
            //    else if (ii.Number == pn)
            //        return ii;
            //}

            InlooxInfo i2 = inlooxInfoList.Find(x => x.TempNumber == pn);

            //if (i2 == null)
            //    i2 = inlooxInfoList.Find(x => x.Number == pn);

            return i2;
        }

        // division, projectnumber
        private InlooxInfo CheckForNewDivision(string d, string pn)
        {

            // schauen, ob das Projekt schon vorhanden ist
            foreach (InlooxInfo ii in inlooxInfoList)
            {
                if ((ii.TempNumber == pn) || (ii.Number == pn))
                    return ii;
            }

            if (!divisionList.Contains(d))
            {
                
                divisionList.Add(d);

                // start worker!
                //bool w = await Start_ForWorker_GetFromServer_ProjectId_etc_FromDivision_Async();

                Start_ForWorker_GetFromServer_ProjectId_etc_FromDivision();

            }                

            //InlooxInfo nii = new InlooxInfo(pn, _ic);

            //// wenn nicht, wird es hinzugefügt
            //inlooxInfoList.Add(nii);


            //Guid pid2 = GetFromServer_ProjectId_etc_FromProjectNumber(d, pn);

            //return nii;
            return CheckForAddOdExistingInlooxInfo(pn);

        }

        private InlooxInfo CheckForAddOdExistingInlooxInfo(string pn)
        {
            InlooxInfo nii = new InlooxInfo(pn, _ic);

            //// nochmal prüfen, ob das Projekt schon da ist
            //foreach (InlooxInfo ii in inlooxInfoList)
            //{
            //    if (ii.TempNumber == pn)
            //        return nii;
            //}

            // nochmal prüfen, ob das Projekt schon da ist NEW
            InlooxInfo rflcwpn = ReturnFromLocalCacheWithTempProjectNumber(pn);
            if (rflcwpn != null)
                return rflcwpn;

            // wenn nicht, wird es hinzugefügt
            inlooxInfoList.Add(nii);

            return nii;
        }

        public InlooxInfo GetInlooxInfoFromProjectNumberOfFavorites(string pn)
        {

            Debug.WriteLine(String.Format("          GetInlooxInfoFromProjectNumber   Number {0}", pn));
            foreach (InlooxInfo ii in FullFavoritesInfoArray)
            {
                if (ii.Number == pn)
                    return ii;
            }

            return null;

        }

        public InlooxInfo GetFromLocal_InlooxInfo_FromProjectId(Guid pid)
        {

            // wenn noch nix da ist...
            if (FullProjectsInfoArray == null)
            {

                // schauen, ob das Projekt schon vorhanden ist
                foreach (InlooxInfo ii in inlooxInfoList)
                {
                    if (ii.ProjectId == pid)
                        return ii;
                }

                InlooxInfo nii = new InlooxInfo(pid, _ic);

                // wenn nicht, wird es hinzugefügt
                inlooxInfoList.Add(nii);
                //Guid pid2 = GetFromServer_ProjectId_etc_FromProjectNumber(d, pn);
                return nii;

            }

            Debug.WriteLine(String.Format("          GetFromLocal_InlooxInfo_FromProjectId   PID {0}", pid));
            foreach (InlooxInfo ii in FullProjectsInfoArray)
            {
                if (ii.ProjectId == pid)
                    return ii;
            }

            return null;

        }

        public InlooxInfo GetFromLocal_InlooxInfo_FromTempProjectNumber(string number)
        {

            // wenn noch nix da ist...
            if (FullProjectsInfoArray == null)
            {

                // schauen, ob das Projekt schon vorhanden ist
                foreach (InlooxInfo ii in inlooxInfoList)
                {
                    if (ii.TempNumber == number)
                        return ii;
                }

                InlooxInfo nii = new InlooxInfo(number, _ic);

                // wenn nicht, wird es hinzugefügt
                inlooxInfoList.Add(nii);
                //Guid pid2 = GetFromServer_ProjectId_etc_FromProjectNumber(d, pn);
                return nii;

            }

            Debug.WriteLine(String.Format("          GetFromLocal_InlooxInfo_FromTempProjectNumber   PID {0}", number));
            foreach (InlooxInfo ii in FullProjectsInfoArray)
            {
                if (ii.TempNumber == number)
                    return ii;
            }

            // nicht anlegen, da man so nicht weiß, ob es schon in der Liste ist...
            //if (local_ii == null)
            // kein Projekt gefunden - also eins anlegen
            //InlooxInfo local_ii = new InlooxInfo(number, _ic);
            //return local_ii;

            return null;

        }


        public Guid GetFromServer_ProjectId_etc_FromProjectNumber(string division, string pn)
        {
        
            Debug.WriteLine(String.Format("          GetFromServer_ProjectId_etc_FromProjectNumber   Division/Number {0}/{1}", division, pn));

            // quick exit
            return new Guid();

            //var projects = from p in _ic.Projectview
            //               where p.DivisionName == division
            //               select p;

            //Guid return_pid = new Guid();

            //foreach (var p in projects)
            //    if (p != null)
            //    {
            //        InlooxInfo local_ii = GetFromLocal_InlooxInfo_FromProjectId(p.ProjectId);
            //        if (local_ii == null)
            //            local_ii = new InlooxInfo(p.ProjectId, _ic);
            //        local_ii.SetAttributesFromProjectView(p);

            //        if (local_ii.Number == pn)
            //            return_pid = local_ii.ProjectId;
            //    }

            //return return_pid;

        }

        public Guid GetFromServer_ProjectId_etc_FromProjectNumber(string pn)
        {

            Debug.WriteLine(String.Format("          GetFromServer_ProjectId_etc_FromProjectNumber   Number {0}", pn));

            var projects = from p in _ic.projectview
                           where p.Number == pn
                           select p;

            foreach (var p in projects)
                if (p != null)
                {
                    InlooxInfo local_ii = GetFromLocal_InlooxInfo_FromProjectId(p.ProjectId);
                    local_ii.SetAttributesFromProjectView(p);
                    return p.ProjectId;
                }

            return new Guid();

        }

        public Guid GetProjectIdFromDocumentFolder(String df)
        {

            // remove '\\' at the end of df
            if (df.Substring(df.Length - 1, 1) == "\\")
                df = df.Substring(0, df.Length - 1);

            //var fav = _ic.Favorite;

            //foreach (Favorite f in fav)
            //{

            //    if (f.ProjectId != null)
            //    {
            //        Guid pid = (Guid)f.ProjectId;
            //        string d = GetDocumentFolder(pid);
            //        if (d == df)
            //            if (Directory.Exists(d))
            //                return pid;
            //    }

            //    else if (f.BusinessObjectId != null)
            //    {
            //        Guid pid = (Guid)f.BusinessObjectId;
            //        string d = GetDocumentFolder(pid);
            //        if (d == df)
            //            if (Directory.Exists(d))
            //                return pid;
            //    }



            //}

            foreach (InlooxInfo f in FullFavoritesInfoArray)
            {

                if (f.ProjectId != null)
                {
                    Guid pid = (Guid)f.ProjectId;
                    string d = GetDocumentFolder(pid);
                    if (d == df)
                        if (Directory.Exists(d))
                            return pid;
                }

            }

            return new Guid();
        }

        private string GetDocumentFolder(Guid id)
        {
            //var projects = from p in _ic.Projectview
            //               where p.ProjectId == id
            //               select p;
            //foreach (var p in projects)
            //    if (p != null)
            //        return (p.DocumentFolder);
            //return (p.DocumentFolder.Replace("\\\\v-sfs-101\\Daten\\","S:\\"));

            //if (fullFavoritesInfoArray == null)
            //    FavoritesList();

            //wait until FullFavoritesInfoArray is not null;

            foreach (InlooxInfo f in FullFavoritesInfoArray)
            {

                if (f.ProjectId != null)
                {
                    Guid pid = (Guid)f.ProjectId;
                    if (pid == id)
                        return f.DocumentFolder;
                }

            }

            // else return nothing
            return "";
        }

        private string GetProjectNumber(Guid id)
        {
            //var projects = from p in _ic.Projectview
            //               where p.ProjectId == id
            //               select p;
            //foreach (var p in projects)
            //    if (p != null)
            //        return (p.Number);


            foreach (InlooxInfo f in FullFavoritesInfoArray)
            {

                if (f.ProjectId != null)
                {
                    Guid pid = (Guid)f.ProjectId;
                    if (pid == id)
                        return f.Number;
                }

            }

            // else return nothing
            return "";
        }

        #region Helper

        public string GetInlooxInfo()
        {
            return ic.BaseUri.AbsoluteUri;
        }

        public string GetInlooxInfo2()
        {
            int fpilc = 0;
            int ffilc = 0;

            if (FullProjectsInfoArray != null)
                fpilc = FullProjectsInfoArray.Count();

            if (favoriteIdList != null)
                ffilc = favoriteIdList.Count;

            String info = String.Format("DivisionListCount: {0}, inlooxInfoListCount: {3}, FullProjectsInfoListCount: {1}, FullFavoritesInfoListCount: {2}", divisionList.Count, fpilc, ffilc, inlooxInfoList.Count);
            return info;
        }

        //public InlooxInfoList(string _DocumentFolder)
        //{
        //    Debug.WriteLine("      NEW InlooxInfo from DocumentFolder: {0}", _DocumentFolder);
        //    _projectId = GetProjectIdFromDocumentFolder(_DocumentFolder);
        //}


        private static void Initialice_IC()
        {

            // Doku
            // https://docs.microsoft.com/en-us/aspnet/web-api/overview/odata-support-in-aspnet-web-api/odata-v3/calling-an-odata-service-from-a-net-client

            // Uri inlooxuri = new Uri("http://v-swc-101.ibb.time-partner.com:8989/odata/");
            //Uri inlooxuri = new Uri("http://v-smg-101.ibb.time-partner.com:8989/odata/");

            _ic = new Container(_inlooxuri);            
            _authtoken = AppSettingsManager.GetValueString("AuthToken", "12345678");
            _ic.authToken = _authtoken;
        }


        // return authtoken
        public bool CheckCurrentToken(Uri _uri, string authtoken, out string errormsg)
        {

            Debug.WriteLine(String.Format("      InlooxInfoList - CheckCurrentToken: {0} / {1}", _uri, authtoken));

            _inlooxuri = _uri;
            _ic = new Container(_inlooxuri);

            string action_id = "";

            errormsg = "";

            //string sUrl String = "http://v-swc-101:8989/odata/../api/0/token";

            //   ServicePointManager.ServerCertificateValidationCallback = Function(
            //obj As[Object],
            //certificate As X509Certificate,
            //chain As X509Chain,
            //errors As SslPolicyErrors)(certificate.Subject.Contains(
            //     "CN=v-swc-101.ibb.time-partner.com")
            //     )

            //string AccessToken = null;
            //string sUrl = "http://v-swc-101:8989/odata/../api/0/token";
            // http://v-swc-101:8989/odata/actionview?$top=1
            string sUrl = _uri + "actionview?$top=1";
            
            //sUrl = _uri + "/../api/0/token";

            //WebRequest request = WebRequest.Create("http://v-swc-101:8989/odata/../api/0/token");
            WebRequest request = WebRequest.Create(sUrl);

            bool CheckAccessToken = true;

            if (CheckAccessToken)
            {

                try
                {

                    // Set the Method property of the request to POST.
                    request.Method = "GET";

                    // Create POST data and convert it to a byte array.
                    //Dim postData As String = "username=ibb%5Ca.vogt&password=TheWordgrant_type=password"
                    //string postData = "";
                    //byte[] byteArray = Encoding.UTF8.GetBytes(postData);
                    //// Set the ContentType property of the WebRequest.
                    //request.ContentType = "application/x-www-form-urlencoded";
                    //// Set the ContentLength property of the WebRequest.
                    //request.ContentLength = byteArray.Length;

                    request.Headers.Add("Authorization:Bearer " + authtoken);
                    //request.Headers["Authorization "] = "Bearer " + authtoken;

                    ////// geht nicht :-(
                    ////request.Credentials = CredentialCache.DefaultCredentials;
                    //CredentialCache.DefaultNetworkCredentials.Domain = "ibb";
                    //CredentialCache.DefaultNetworkCredentials.UserName = "a.vogt";
                    //request.Credentials = CredentialCache.DefaultNetworkCredentials;

                    //request.Headers("Accept") = "application/json"

                    //'Login
                    //'request.Credentials = New NetworkCredential(username, password)
                    //'request.Credentials = System.Net.CredentialCache.DefaultCredentials

                    //' Get the request stream.
                    //Stream dataStream = request.GetRequestStream();
                    //' Write the data to the request stream.
                    //dataStream.Write(byteArray, 0, byteArray.Length);
                    //' Close the Stream object.
                    //dataStream.Close();

                    request.Timeout = 2000;

                    Debug.WriteLine(String.Format("      InlooxInfoList - CheckCurrentToken - Start WebResponse: {0} / {1}", _uri, authtoken));

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

                    ////'Dim myRegexStrCounter As String = String.Format(""(.*)"":\s(.*),\s")
                    ////'Dim myRegexStrCounter As String = String.Format("""(.*)"":""(.*)""")
                    ////'Dim myRegexStrCounter As String = String.Format("""([\w\d-_]*)"":""([\w\d-_]*)"",""([\w\d-_]*)"":""([\w\d-_]*)"",""([\w\d-_]*)"":""([\w\d-_]*)")
                    //String myRegexStrCounter = String.Format(@"""([\w\d-_]*)"":""([\w\d-_]*)""");
                    //Regex myRegexCounter = new Regex(myRegexStrCounter);

                    ////'Dim match As Match = myRegexCounter.Match(responseFromServer)
                    //MatchCollection matchC = myRegexCounter.Matches(responseFromServer);

                    //if (matchC.Count > 0)
                    //{


                    //    //'Debug.WriteLine("{0} MATCHL: {1}", Now, output)

                    //    //'For Each mc As Capture In match.Captures
                    //    //'    Debug.WriteLine("{0} MATCHC: {1}", Now, mc.Value)
                    //    //'Next

                    //    foreach (Match m in matchC)
                    //    {


                    //        //' Access first Group and its value.
                    //        System.Text.RegularExpressions.Group name = m.Groups[1];
                    //        System.Text.RegularExpressions.Group value = m.Groups[2];
                    //        if (name.Value == "access_token")
                    //        {


                    //            AccessToken = value.Value;
                    //             goto AccessTokenIsHere;
                    //         }

                    //        //'Console.WriteLine(in_use.Value)
                    //        //'Console.WriteLine(free.Value)

                    //     }
                    //}


                    //AccessTokenIsHere:;

                    action_id = GetJsonValueFromString("ActionId", responseFromServer);

                    // now set the preferences
                    _inlooxuri = _uri;
                    _authtoken = authtoken;

                    Debug.WriteLine(String.Format("INLOOX-Server: {0}", InlooxDefaults.IiL.GetInlooxInfo()));

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
                        ////' Get the stream containing content returned by the server.
                        //dataStream = ex2.Response.GetResponseStream();
                        //' Open the stream using a StreamReader for easy access.
                        //StreamReader reader = new StreamReader(dataStream);
                        //' Read the content.
                        //String responseFromServer = reader.ReadToEnd();
                        ////' Display the content.
                        //Debug.WriteLine("responseFromServer");
                        //Debug.WriteLine(responseFromServer);
                        //'"access_token":\s(.*),\s
                        Debug.WriteLine("");
                        //' Clean up the streams.
                        //reader.Close();
                        //dataStream.Close();

                        // MsgBox(String.Format("ERROR in Odata! {0}", ex.Message), MsgBoxStyle.Critical, "Fehler in wpfMainWindow Odata")
                        Debug.WriteLine("{0} EXCEPTION in {1} Fehler:{2}", DateTime.Now, "InlooxInfoList", ex);

                        errormsg = String.Format("error: {0}", exeption_message);
                        return false;
                    }

                    Debug.WriteLine("{0} EXCEPTION in {1} Fehler:{2}", DateTime.Now, "InlooxInfoList", ex);
                    errormsg = ex.Message;
                    return false;
                }

            }



            return false;
        }

        // return is working / is not working
        public bool SetLoginPreferences(Uri _uri, string tokenpath, string username, string password, out string authtoken, out string errormsg)
        {

            Debug.WriteLine(String.Format("      InlooxInfoList - SetLoginPreferences: {0} / {1}", _uri, username));

            _inlooxuri = _uri;
            _ic = new Container(_inlooxuri);

            authtoken = "";
            errormsg = "";

            //string sUrl String = "http://v-swc-101:8989/odata/../api/0/token";

            //   ServicePointManager.ServerCertificateValidationCallback = Function(
            //obj As[Object],
            //certificate As X509Certificate,
            //chain As X509Chain,
            //errors As SslPolicyErrors)(certificate.Subject.Contains(
            //     "CN=v-swc-101.ibb.time-partner.com")
            //     )

            //string AccessToken = null;
            //string sUrl = "http://v-swc-101:8989/odata/../api/0/token";
            string sUrl = _uri + tokenpath;
            //sUrl = _uri + "/../api/0/token";

            //WebRequest request = WebRequest.Create("http://v-swc-101:8989/odata/../api/0/token");
            WebRequest request = WebRequest.Create(sUrl);

            bool GetAccessToken = true;

            if (GetAccessToken)
            {
            
                try
                {

                    // Set the Method property of the request to POST.
                    request.Method = "POST";

                    // Create POST data and convert it to a byte array.
                    //Dim postData As String = "username=ibb%5Ca.vogt&password=TheWordgrant_type=password"
                    string postData = "username=" + username + "&password=" + password + "&grant_type=password";
                    byte[] byteArray = Encoding.UTF8.GetBytes(postData);
                    // Set the ContentType property of the WebRequest.
                    request.ContentType = "application/x-www-form-urlencoded";
                    // Set the ContentLength property of the WebRequest.
                    request.ContentLength = byteArray.Length;

                    ////// geht nicht :-(
                    ////request.Credentials = CredentialCache.DefaultCredentials;
                    //CredentialCache.DefaultNetworkCredentials.Domain = "ibb";
                    //CredentialCache.DefaultNetworkCredentials.UserName = "a.vogt";
                    //request.Credentials = CredentialCache.DefaultNetworkCredentials;

                    //request.Headers("Accept") = "application/json"

                    //'Login
                    //'request.Credentials = New NetworkCredential(username, password)
                    //'request.Credentials = System.Net.CredentialCache.DefaultCredentials

                    //' Get the request stream.
                    Stream dataStream = request.GetRequestStream();
                    //' Write the data to the request stream.
                    dataStream.Write(byteArray, 0, byteArray.Length);
                    //' Close the Stream object.
                    dataStream.Close();

                    request.Timeout = 2000;

                    Debug.WriteLine(String.Format("      InlooxInfoList - SetLoginPreferences - Start WebResponse: {0} / {1}", _uri, username));

                    //' Get the response.
                    WebResponse response = request.GetResponse();
                    HttpWebResponse httpresponse = (System.Net.HttpWebResponse)response;

                    //' Display the status.
                    Debug.WriteLine("StatusDescription");
                    Debug.WriteLine(httpresponse.StatusDescription);
                    Debug.WriteLine("");
                    //' Get the stream containing content returned by the server.
                    dataStream = response.GetResponseStream();
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

                    ////'Dim myRegexStrCounter As String = String.Format(""(.*)"":\s(.*),\s")
                    ////'Dim myRegexStrCounter As String = String.Format("""(.*)"":""(.*)""")
                    ////'Dim myRegexStrCounter As String = String.Format("""([\w\d-_]*)"":""([\w\d-_]*)"",""([\w\d-_]*)"":""([\w\d-_]*)"",""([\w\d-_]*)"":""([\w\d-_]*)")
                    //String myRegexStrCounter = String.Format(@"""([\w\d-_]*)"":""([\w\d-_]*)""");
                    //Regex myRegexCounter = new Regex(myRegexStrCounter);

                    ////'Dim match As Match = myRegexCounter.Match(responseFromServer)
                    //MatchCollection matchC = myRegexCounter.Matches(responseFromServer);

                    //if (matchC.Count > 0)
                    //{


                    //    //'Debug.WriteLine("{0} MATCHL: {1}", Now, output)

                    //    //'For Each mc As Capture In match.Captures
                    //    //'    Debug.WriteLine("{0} MATCHC: {1}", Now, mc.Value)
                    //    //'Next

                    //    foreach (Match m in matchC)
                    //    {


                    //        //' Access first Group and its value.
                    //        System.Text.RegularExpressions.Group name = m.Groups[1];
                    //        System.Text.RegularExpressions.Group value = m.Groups[2];
                    //        if (name.Value == "access_token")
                    //        {


                    //            AccessToken = value.Value;
                    //             goto AccessTokenIsHere;
                    //         }

                    //        //'Console.WriteLine(in_use.Value)
                    //        //'Console.WriteLine(free.Value)

                    //     }
                    //}


                    //AccessTokenIsHere:;

                    authtoken = GetJsonValueFromString("access_token", responseFromServer);

                    // now set the preferences
                    _inlooxuri = _uri;
                    _authtoken = authtoken;

                    Debug.WriteLine(String.Format("INLOOX-Server: {0}", InlooxDefaults.IiL.GetInlooxInfo()));

                    return true;

                    }
                    catch (Exception ex)
                    {

                    //if (ex.InnerException.GetType() == typeof(System.OverflowException
                    if (ex.GetType() == typeof(WebException))
                    {
                        try
                        {
                            WebException ex2 = (WebException)ex;
                            Stream dataStream = request.GetRequestStream();
                            Debug.WriteLine("");
                            ////' Get the stream containing content returned by the server.
                            dataStream = ex2.Response.GetResponseStream();
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

                            // MsgBox(String.Format("ERROR in Odata! {0}", ex.Message), MsgBoxStyle.Critical, "Fehler in wpfMainWindow Odata")
                            Debug.WriteLine("{0} EXCEPTION in {1} Fehler:{2}", DateTime.Now, "InlooxInfoList", ex);

                            if (responseFromServer.Contains("error"))
                                errormsg = GetJsonValueFromString("error_description", responseFromServer);
                            return false;
                        }
                        catch (Exception ex2)
                        {
                            errormsg = "unknown ex2";
                        }
                      }

                        Debug.WriteLine("{0} EXCEPTION in {1} Fehler:{2}", DateTime.Now, "InlooxInfoList", ex);
                        errormsg = ex.Message;
                        return false;
                    }

                }
            


            return false;
        }

        private string GetJsonValueFromString(string valuename, string data)
        {

            //string AccessToken = null;
            //'Dim myRegexStrCounter As String = String.Format(""(.*)"":\s(.*),\s")
            //'Dim myRegexStrCounter As String = String.Format("""(.*)"":""(.*)""")
            //'Dim myRegexStrCounter As String = String.Format("""([\w\d-_]*)"":""([\w\d-_]*)"",""([\w\d-_]*)"":""([\w\d-_]*)"",""([\w\d-_]*)"":""([\w\d-_]*)")
            String myRegexStrCounter = String.Format(@"""([\w\d\-_]*)"":""([\s\w\d.\-_]*)""");
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

        public static Container ic
        {
            get
            {
                return _ic;
            }
        }
        #endregion

        #region worker

        //public void ForWorker_GetFromServer_ProjectId_etc_FromDivision(string division)
        //public bool ForWorker_GetFromServer_ProjectId_etc_FromDivision()
        public void ForWorker_GetFromServer_ProjectId_etc()
        {


            _gettingDataNow = true;

            //Debug.WriteLine(String.Format("           InlooxInfoList ForWorker_GetFromServer_ProjectId_etc: {0}", "DISABLED"));
            //return;

            Debug.WriteLine(String.Format("           InlooxInfoList ForWorker_GetFromServer_ProjectId_etc: {0}", "ENABLED"));

            // first build Favorit list


            try
            {
                var fav = from p in _ic.favorite
                          select p;

                foreach (Favorite f in fav)
                {

                    if (f.ProjectId != null)
                    {
                        Debug.WriteLine(String.Format("      BuildLogicalFavoriteArray - FavId/BusinessObjectId: {0} / {1}", f.FavoriteId, f.BusinessObjectId));
                        Guid pid = (Guid)f.ProjectId;

                        // Abbruch, wenn die ProjektId schon enthalten ist
                        if (favoriteIdList.Contains(pid))
                        {
                            Debug.WriteLine(String.Format("        Get Inloox Fav (Pid) - pID    still EXIST: {0}", pid));
                            break;
                        }

                        favoriteIdList.Add(pid);

                    }
                    else if (f.BusinessObjectId != null)
                    {
                        Debug.WriteLine(String.Format("      BuildLogicalFavoriteArray - FavId/BusinessObjectId: {0} / {1}", f.FavoriteId, f.BusinessObjectId));
                        Guid pid = (Guid)f.BusinessObjectId;

                        favoriteIdList.Add(pid);
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
                    ////' Get the stream containing content returned by the server.
                    //dataStream = ex2.Response.GetResponseStream();
                    //' Open the stream using a StreamReader for easy access.
                    //StreamReader reader = new StreamReader(dataStream);
                    //' Read the content.
                    //String responseFromServer = reader.ReadToEnd();
                    ////' Display the content.
                    //Debug.WriteLine("responseFromServer");
                    //Debug.WriteLine(responseFromServer);
                    //'"access_token":\s(.*),\s
                    Debug.WriteLine("");
                    //' Clean up the streams.
                    //reader.Close();
                    //dataStream.Close();

                    // MsgBox(String.Format("ERROR in Odata! {0}", ex.Message), MsgBoxStyle.Critical, "Fehler in wpfMainWindow Odata")
                    Debug.WriteLine("{0} EXCEPTION in {1} Fehler:{2}", DateTime.Now, "InlooxInfoList", ex);

                    errormsg = String.Format("error: {0}", exeption_message);
                }
                else
                {
                    Debug.WriteLine("{0} EXCEPTION in {1} Fehler:{2}", DateTime.Now, "InlooxInfoList", ex);
                    errormsg = ex.Message;
                }

                Debug.WriteLine(String.Format("{0}  ###################   EXEPTION2   ############## in GetAttribute: {1}", DateTime.Now, errormsg));
            }


            // weiter

            string division = "";

            if (divisionList.First() != "")
                division = divisionList.First();

            Debug.WriteLine(String.Format("          ForWorker_GetFromServer_ProjectId_etc_FromDivision   Division{0}", division));

            try
            {
                // first pull projectview from first division
                var projects = from p in _ic.projectview
                               where p.DivisionName == division
                               select p;

                foreach (var p in projects)
                    if (p != null)
                    {
                        InlooxInfo local_ii = GetFromLocal_InlooxInfo_FromTempProjectNumber(p.Number);
                        if (local_ii == null)
                        {
                            local_ii = new InlooxInfo(p.Number, _ic);
                            inlooxInfoList.Add(local_ii);
                        }

                        local_ii.SetAttributesFromProjectView(p);

                        if (favoriteIdList.Contains(local_ii.ProjectId))
                            local_ii.SetIsFavoritAttributes(true);

                        //if (local_ii.Number == pn)
                        //    return_pid = local_ii.ProjectId;
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
                    ////' Get the stream containing content returned by the server.
                    //dataStream = ex2.Response.GetResponseStream();
                    //' Open the stream using a StreamReader for easy access.
                    //StreamReader reader = new StreamReader(dataStream);
                    //' Read the content.
                    //String responseFromServer = reader.ReadToEnd();
                    ////' Display the content.
                    //Debug.WriteLine("responseFromServer");
                    //Debug.WriteLine(responseFromServer);
                    //'"access_token":\s(.*),\s
                    Debug.WriteLine("");
                    //' Clean up the streams.
                    //reader.Close();
                    //dataStream.Close();

                    // MsgBox(String.Format("ERROR in Odata! {0}", ex.Message), MsgBoxStyle.Critical, "Fehler in wpfMainWindow Odata")
                    Debug.WriteLine("{0} EXCEPTION in {1} Fehler:{2}", DateTime.Now, "InlooxInfoList", ex);

                    errormsg = String.Format("error: {0}", exeption_message);
                }
                else
                {
                    Debug.WriteLine("{0} EXCEPTION in {1} Fehler:{2}", DateTime.Now, "InlooxInfoList", ex);
                    errormsg = ex.Message;
                }

                Debug.WriteLine(String.Format("{0}  ###################   EXEPTION3   ############## in GetAttribute: {1}", DateTime.Now, errormsg));
            }
            //// now build Favorit list

            //var fav = from p in _ic.Favorite                           
            //               select p;

            //foreach (Favorite f in fav)
            //{

            //    if (f.ProjectId != null)
            //    {
            //        Debug.WriteLine(String.Format("      BuildLogicalFavoriteArray - FavId/BusinessObjectId: {0} / {1}", f.FavoriteId, f.BusinessObjectId));
            //        Guid pid = (Guid)f.ProjectId;

            //        // Abbruch, wenn die ProjektId schon enthalten ist
            //        if (favoriteIdList.Contains(pid))
            //        {
            //            Debug.WriteLine(String.Format("        Get Inloox Fav (Pid) - pID    still EXIST: {0}", pid));
            //            break;
            //        }

            //        //InlooxInfo ii = new InlooxInfo(pid, _ic);

            //        InlooxInfo local_ii = GetFromLocal_InlooxInfo_FromProjectId(pid);
            //        if (local_ii == null)
            //        {
            //            local_ii = new InlooxInfo(pid, _ic);
            //            inlooxInfoList.Add(local_ii);
            //        }

            //        local_ii.SetIsFavoritAttributes(true);

            //    }
            //    else if (f.BusinessObjectId != null)
            //    {
            //        Debug.WriteLine(String.Format("      BuildLogicalFavoriteArray - FavId/BusinessObjectId: {0} / {1}", f.FavoriteId, f.BusinessObjectId));
            //        Guid pid = (Guid)f.BusinessObjectId;

            //        InlooxInfo local_ii = GetFromLocal_InlooxInfo_FromProjectId(pid);
            //        if (local_ii == null)
            //        {
            //            local_ii = new InlooxInfo(pid, _ic);
            //            inlooxInfoList.Add(local_ii);
            //        }

            //        local_ii.SetIsFavoritAttributes(true);
            //    }
            //}


            try
            {
                // first pull all other from projectview - now use ProjectId because of new Favorites with id
                var projects2 = from p in _ic.projectview
                                where p.DivisionName != division
                                select p;

                foreach (var p in projects2)
                    if (p != null)
                    {
                        InlooxInfo local_ii = GetFromLocal_InlooxInfo_FromProjectId(p.ProjectId);
                        if (local_ii == null)
                        {
                            local_ii = new InlooxInfo(p.Number, _ic);
                            inlooxInfoList.Add(local_ii);
                        }
                        local_ii.SetAttributesFromProjectView(p);

                        if (favoriteIdList.Contains(p.ProjectId))
                            local_ii.SetIsFavoritAttributes(true);

                        //if (local_ii.Number == pn)
                        //    return_pid = local_ii.ProjectId;
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
                    ////' Get the stream containing content returned by the server.
                    //dataStream = ex2.Response.GetResponseStream();
                    //' Open the stream using a StreamReader for easy access.
                    //StreamReader reader = new StreamReader(dataStream);
                    //' Read the content.
                    //String responseFromServer = reader.ReadToEnd();
                    ////' Display the content.
                    //Debug.WriteLine("responseFromServer");
                    //Debug.WriteLine(responseFromServer);
                    //'"access_token":\s(.*),\s
                    Debug.WriteLine("");
                    //' Clean up the streams.
                    //reader.Close();
                    //dataStream.Close();

                    // MsgBox(String.Format("ERROR in Odata! {0}", ex.Message), MsgBoxStyle.Critical, "Fehler in wpfMainWindow Odata")
                    Debug.WriteLine("{0} EXCEPTION in {1} Fehler:{2}", DateTime.Now, "InlooxInfoList", ex);

                    errormsg = String.Format("error: {0}", exeption_message);
                }
                else
                {
                    Debug.WriteLine("{0} EXCEPTION in {1} Fehler:{2}", DateTime.Now, "InlooxInfoList", ex);
                    errormsg = ex.Message;
                }

                Debug.WriteLine(String.Format("{0}  ###################   EXEPTION4   ############## in GetAttribute: {1}", DateTime.Now, errormsg));
            }

            //return true;

            _gettingDataNow = false;
            _gotData = true;

        }

        //public async System.Threading.Tasks.Task<bool> Start_ForWorker_GetFromServer_ProjectId_etc_FromDivision_Async()
        //{
        //    // Dim r As Boolean = Await BuildAndRefreshLicenseList()
        //    // Dim t As Boolean = Await Task.Run(AdressOf BuildAndRefreshLicenseList)

        //    return await Task.Run<bool>(() =>
        //    {
        //        return ForWorker_GetFromServer_ProjectId_etc_FromDivision();
        //    });
        //}


        public void Start_ForWorker_GetFromServer_ProjectId_etc_FromDivision()
        {
            _gettingDataNow = true;

            // BuildLicServerList()
            workerThread1 = new Thread(new ThreadStart(ForWorker_GetFromServer_ProjectId_etc));
            workerThread1.Start();
        }


        #endregion
    }

}