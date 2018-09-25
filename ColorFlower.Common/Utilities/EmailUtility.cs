using ColorFlower.Common;
using System;
using System.Net.Mail;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using ColorFlower.Common.Entities;

namespace ColorFlower.Utilities
{
    public class EmailUtility
    {

        private static string TableFormat = "<table border='1'><tr><th>模型名</th><th>号码</th><th>历史</th></tr>{0}</table>";
        private static string ContentRowFormat = "<tr><td>{0}</td><td>{1}</td><td>{2}</td></tr>";
        private static string HightlightContentRowFormat = "<tr><td><b>{0}</b></td><td><b>{1}</b></td><td><b>{2}</b></td></tr>";
        private static string NumberRowFormat = "<br>{0}<br/>";
        private static string OutputFormat = "<div><b>备注</b>{0}</div>";
        private static string IndexFormat = "<div><br/><h1>第{0}位</h1></div>";

        public static void Send()
        {
            var addresses = ColorFlowerXmlUtility.GetEmailAddresses();

            foreach (var item in addresses)
            {
                var mail = new MailMessage();
                var SmtpServer = new SmtpClient("smtp.163.com");
                mail.From = new MailAddress(item.Address, "Tony");
                mail.To.Add(item.Address);

                mail.Subject = string.Format("第{0}期", IssueEntity.IssueResults.Last().IssueID + 1);
                mail.IsBodyHtml = true;
                mail.Body = GenerateContent(item.Filter);

                //SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential(item.Address, "tony200692");
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);

                SmtpServer.Dispose();
            }
        }

        public static void SendBloom(string msg, int issueID)
        {
            var addresses = ColorFlowerXmlUtility.GetEmailAddresses();

            foreach (var item in addresses)
            {
                var mail = new MailMessage();
                var SmtpServer = new SmtpClient("smtp.163.com");
                mail.From = new MailAddress(item.Address, "Tony");
                mail.To.Add(item.Address);

                mail.Subject = string.Format("第{0}期", issueID);
                mail.IsBodyHtml = true;
                mail.Body = msg;

                //SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential(item.Address, "tony200692");
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);

                SmtpServer.Dispose();
            }
        }

        public static void SendResponse(string msg)
        {
            var addresses = ColorFlowerXmlUtility.GetEmailAddresses();

            foreach (var item in addresses)
            {
                var mail = new MailMessage();
                var SmtpServer = new SmtpClient("smtp.163.com");
                mail.From = new MailAddress(item.Address, "Tony");
                mail.To.Add(item.Address);

                mail.Subject = msg;
                mail.IsBodyHtml = true;
                mail.Body = string.Empty;

                //SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential(item.Address, "tony200692");
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);

                SmtpServer.Dispose();
            }
        }

        private static string GenerateContent(string filter)
        {
            var patterns = IssueEntity.CalcPatterns;

            var issueNumber = IssueEntity.IssueResults.Last().IssueID;
            var issueResult = IssueEntity.IssueResults.Last().OpenCode;
            var issueDate = IssueEntity.IssueResults.Last().OpenDate;
            var currentNumber = issueNumber + 1;
            var selectionSettings = ColorFlowerXmlUtility.GetSelectionSettings();

            var content = new StringBuilder();
            content.Append(string.Format("<h1>第{0}期 {1}-{2}-{3}</h1><br/>", currentNumber, selectionSettings.Threshold, selectionSettings.CharLength, selectionSettings.SelectionLength));
            content.Append(string.Format("<b>“=”代表对，“@”代表错</b><br/>", currentNumber));
            content.Append(string.Format("<b>第{0}期({1})结果：{2}</b><br/>", issueNumber, issueDate.ToShortDateString(), issueResult));

            content.Append(string.Format("<b>###+++###</b><br/>"));

            //content.Append(string.Format("<b>###***###</b><br/>"));

            var autoGene = new StringBuilder();

            autoGene.Append("<br/><table border='1'><tr><th>位数</th><th>个独</th><th>预测</th></tr>");

            var sb = new StringBuilder();
            foreach (var cp in patterns)
            {
                var outPutItems = new List<OutPutItem>();

                var sbTables = new StringBuilder();

                var sbContent = new StringBuilder();

                var ps = new List<string>();
                foreach (var item in cp.Patterns)
                {
                    ps.Add(item.Key);
                }

                ps.Sort();

                var caclStrings = string.Empty;

                content.Append(string.Format(IndexFormat, cp.IssueIndex));

                foreach (var item in ps)
                {
                    // ignore not care templates
                    if (!filter.Contains("*") && !filter.Contains(item.Substring(0, 1))) continue;

                    var cr = UtilityHelper.CalcAll(currentNumber, cp.IssueIndex, item);

                    var contentRowFormat = ContentRowFormat;
                    if (CheckIsDefaultPattern(cp.IssueIndex, item))
                        contentRowFormat = HightlightContentRowFormat;

                    var history = UtilityHelper.GenerateHistory(currentNumber, cp.IssueIndex, item);
                    sbContent.Append(string.Format(contentRowFormat, item, cr.CaclString, history));

                    outPutItems.Add(new OutPutItem() { PatternName = item, CalcNumber = cr.CaclString, History = history });

                    if (!item.StartsWith("W"))
                        caclStrings += cr.CaclString.Replace("[-1]", string.Empty);
                }

                sbTables.Append(string.Format(TableFormat, sbContent.ToString()));

                var num = new StringBuilder();
                for (int i = 0; i < 10; i++)
                {
                    num.Append(string.Format("{0}:{1}   ", i, new Regex(string.Format("[{0}]", i.ToString())).Matches(caclStrings).Count));
                }

                //var final = CalcFinalNumberOnePattern(outPutItems) + CalcFinalNumberNumberCount(outPutItems);

                content.Append(sbTables.ToString());

                content.Append(string.Format(NumberRowFormat, num.ToString()));

                var finalNumbers = string.Empty;
                var cfnnc = CalcFinalNumberNumberCount(outPutItems, out finalNumbers);

                sb.Append(finalNumbers + "-");

                autoGene.Append(string.Format("<tr><td>第{0}位</td><td>{1}</td><td>{2}</td></tr>", cp.IssueIndex, cfnnc, finalNumbers));

                if (filter.Contains("NOTE"))
                    content.Append(string.Format(OutputFormat, ColorFlowerXmlUtility.GetOutput(string.Format("root/Outputs/Output{0}/Item[@index='{1}']", currentNumber, cp.IssueIndex)).Replace("\r\n", "<br/>")));
            }
            var res = autoGene.Append("</table>").ToString();
            content.Replace("###+++###", res);
            //content.Replace("###***###", GenerateProdictionHistory());

            content.Append("</br></br></br>");

            var predictions = sb.ToString();
            ColorFlowerXmlUtility.SetPredictionResult(currentNumber, predictions.Remove(predictions.Length-2));

            return content.ToString().Replace("[-1] ", string.Empty);
        }

        /// <summary>
        /// The minimun wrong pattern, take only one pattern result 
        /// </summary>
        /// <param name="outPutItems"></param>
        /// <returns></returns>
        private static string CalcFinalNumberOnePattern(IList<OutPutItem> outPutItems)
        {
            var historyStringToCalc = new List<string>();
            var rightExp = new Regex("=");
            foreach (var item in outPutItems)
            {
                var h = item.History;
                historyStringToCalc.Add(string.Format("{0},{1}", rightExp.Matches(h).Count, item.CalcNumber.Replace("[-1]", string.Empty)));
            }

            historyStringToCalc.Sort();

            historyStringToCalc.Reverse();

            var count = historyStringToCalc.Count;

            var start = (int)(count * 0.2);
            var end = (int)(count * 0.5);

            var dic = new Dictionary<int, int>();

            for (int i = 0; i < 10; i++)
            {
                dic.Add(i, 0);
            }

            for (int i = start; i < end; i++)
            {
                var str = historyStringToCalc[i].SplitTo(",")[1].Replace("[-1]", string.Empty).Replace(" ", string.Empty);

                for (int j = 0; j < str.Length; j++)
                {
                    dic[str[j].ToString().ToInt32()]++;
                }
            }

            var newdic = dic.OrderByDescending(p => { return p.Value; });

            var sb = new StringBuilder();
            foreach (KeyValuePair<int,int> item in newdic)
            {
                sb.Append(item.Key);
            }

            return sb.ToString();
        }

        /// <summary>
        /// The minimum wrong patttern result list, take result by number count.
        /// the first 4 number which count is maximun 
        /// </summary>
        /// <param name="outPutItems"></param>
        /// <returns></returns>
        private static string CalcFinalNumberNumberCount(IList<OutPutItem> outPutItems, out string finalNumbers)
        {
            var finalNumbersSB = new StringBuilder();
            var final = new StringBuilder();

            var dic = new Dictionary<int, int>();

            for (int i = 0; i < 10; i++)
            {
                dic.Add(i, 0);
            }

            var rightExp = new Regex("=");
            foreach (var item in outPutItems)
            {
                if (rightExp.Matches(item.History).Count > 10)
                {
                    var calcNumber = item.CalcNumber.Replace("[-1]", string.Empty).Replace(" ", string.Empty);

                    for (int i = 0; i < calcNumber.Length; i++)
                    {
                        var intNumber = calcNumber.Substring(i, 1).ToInt32();

                        dic[intNumber]++;
                    }
                }
            }

            var newdic = dic.OrderByDescending(p => { return p.Value; });

            foreach (var item in newdic)
            {
                final.Append(string.Format("{0}:{1} ", item.Value, item.Key));
                finalNumbersSB.Append(item.Key);
            }

            finalNumbers = finalNumbersSB.ToString();

            return final.ToString();
        }

        private static bool CheckIsDefaultPattern(int issueIndex, string patternName)
        {
            var res = false;

            var dps = ColorFlowerXmlUtility.GetDefaultPatterns();
            foreach (KeyValuePair<string, string> item in dps)
            {
                if (item.Key.Equals(issueIndex.ToString()) && item.Value.ToString().Equals(patternName))
                    res = true;
            }
            return res;
        }

        private static string GenerateProdictionHistory()
        {
            var rs = new StringBuilder();

            rs.Append("<br/><table border='1'><tr><th>期数</th><th>结果</th><th>预测</th><th>9数图形</th><th>8数图形</th><th>7数图形</th><th>6数图形</th><th>5数图形</th><th>4数图形</th><th>3数图形</th></tr>");

            var predictions = ColorFlowerXmlUtility.GetAllPredictions();

            var ps = predictions.OrderByDescending(p => p.IssueIndex);

            foreach (var item in ps)
            {
                if (item.IssueIndex == IssueEntity.IssueResults.Last().IssueID + 1) continue;

                rs.Append(string.Format("<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td><td>{5}</td><td>{6}</td><td>{7}</td><td>{8}</td><td>{9}</td></tr>",
                    item.IssueIndex, item.ActuallyResult, item.PredictionResultDispaly, item.MatchedFormat9, item.MatchedFormat8, item.MatchedFormat7, item.MatchedFormat6, item.MatchedFormat5, item.MatchedFormat4, item.MatchedFormat3));
            }

            rs.Append("</table>");
            return rs.ToString();
        }
    }
}
