using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using ColorFlower.Utilities;

namespace ColorFlower.Common
{
    public class SyncResultUtility
    {
        public static void Sync()
        {
            var isHaveNew = false;

            ColorFlowerXmlUtility.GenerateIssueResult();

            var currentIssueID = IssueEntity.IssueResults.Count == 0 ? 2016100 : IssueEntity.IssueResults.Last().IssueID;

            var actualResult = GetResultFromWeb();

            var actualMaxIsssueID = actualResult.Last().IssueID;

            for (int i = currentIssueID + 1; i <= actualMaxIsssueID; i++)
            {
                UtilityHelper.NewResultAdded(actualResult.Where((p) => p.IssueID == i).First());

                UtilityHelper.Initialize();

                var isCreateNew = ColorFlowerXmlUtility.IsCreateNew();
                var selectionSettings = ColorFlowerXmlUtility.GetSelectionSettings();

                if (isCreateNew)
                {
                    UtilityHelper.ClearAllPatterns();
                    var issueIndex = UtilityHelper.GetIssueIDInt(IssueEntity.IssueResults.Last().IssueIndex - 5);

                    var completeCount = 0;
                    for (int j = 1; j < 8; j++)
                    {
                        Trace.WriteLine(string.Format("Analysis Index = {0}, at {1}", j, DateTime.Now.ToLongTimeString()));
                        UtilityHelper.Analysis(issueIndex, j, selectionSettings.Threshold, selectionSettings.Start, selectionSettings.CharLength, selectionSettings.SelectionLength, ref completeCount);
                    }
                }
                UtilityHelper.Initialize();
                EmailUtility.Send();
                Trace.WriteLine(string.Format("Mail sent at {0}", DateTime.Now.ToLongTimeString()));
            }



            //if (actualMaxIsssueID > currentIssueID)
            //{
            //    var currentID = currentIssueID;
            //    while (currentID <= actualMaxIsssueID)
            //    {
            //        ++currentID;
            //        var newIssue = actualResult.Where((p) => p.IssueID == currentID);

            //        if (newIssue.Count() == 1)
            //        {
            //            UtilityHelper.NewResultAdded(newIssue.First());
            //            isHaveNew = true;
            //        }
            //    }
            //}

            //UtilityHelper.Initialize();

            //if (isHaveNew)
            //{
            //    var isCreateNew = ColorFlowerXmlUtility.IsCreateNew();

            //    if (isCreateNew)
            //    {
            //        UtilityHelper.ClearAllPatterns();
            //        var issueIndex = UtilityHelper.GetIssueIDInt(IssueEntity.IssueResults.Last().IssueIndex - 5);

            //        var completeCount = 0;
            //        for (int i = 1; i < 8; i++)
            //        {
            //            Trace.WriteLine(string.Format("Analysis Index = {0}, at {1}", i, DateTime.Now.ToLongTimeString()));
            //            UtilityHelper.Analysis(issueIndex, i, 80, 0, 15, 2, ref completeCount);
            //        }
            //    }
            //    UtilityHelper.Initialize();
            //    EmailUtility.Send();
            //    Trace.WriteLine(string.Format("Mail sent at {0}", DateTime.Now.ToLongTimeString()));
            //}
        }

        public static string HttpGet(string url, Encoding enc)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Timeout = 10000;//设置10秒超时
            request.Proxy = null;
            request.Method = "GET";
            request.ContentType = "application/x-www-from-urlencoded";
            WebResponse response = request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream(), enc);
            StringBuilder sb = new StringBuilder();
            sb.Append(reader.ReadToEnd());
            reader.Close();
            reader.Dispose();
            response.Close();
            return sb.ToString();
        }

        public static IList<IssueResult> GetResultFromWeb()
        {
            var res = new List<IssueResult>();
            try
            {
                var url = ColorFlowerXmlUtility.GetUri();
                string html = HttpGet(url, Encoding.UTF8);
                XmlDocument xml = new XmlDocument();
                xml.LoadXml(html);

                foreach (XmlNode node in xml.SelectNodes("xml/row"))
                {
                    var issueID = Convert.ToInt32(node.Attributes["expect"].Value);

                    //if (issueID > 2017086) continue;

                    var opencode = node.Attributes["opencode"].Value.ToString().Replace(",", "");
                    var OpenDate = DateTime.Parse(node.Attributes["opentime"].Value.ToString());
                    res.Add(new IssueResult()
                    {
                        IssueID = issueID,
                        OpenCode = opencode,
                        OpenDate = OpenDate
                    });
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
            }
            res.Reverse();
            return res;
        }
    }
}
