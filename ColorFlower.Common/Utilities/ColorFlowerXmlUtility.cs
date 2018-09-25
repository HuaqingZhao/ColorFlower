using ColorFlower.Common;
using ColorFlower.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using ColorFlower.Common.Entities;

namespace ColorFlower.Utilities
{
    public class ColorFlowerXmlUtility
    {
        private static XmlDocument Doc;
        private static string path;

        static ColorFlowerXmlUtility()
        {
            if (System.Diagnostics.Process.GetCurrentProcess().ProcessName.ToUpper() == "devenv".ToUpper()) return;

            path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ColorFlower.xml");
            Doc = new XmlDocument();
            Doc.Load(path);
        }

        #region Issue result

        public static void GenerateIssueResult()
        {
            IssueEntity.IssueResults = GetIssueResult();
        }

        public static bool IsExists(int dispalyNumber, string result)
        {
            var res = false;

            var xPath = string.Format("root/Issues/Issue[@issueID='{0}']", dispalyNumber);

            var node = Doc.SelectSingleNode(xPath);

            if (node != null)
            {
                if (node.InnerText.Replace(",", string.Empty).Trim().Equals(result))
                {
                    res = true;
                }
                else
                {
                    DeleteIssueNode(xPath);
                }
            }

            return res;
        }

        private static IList<IssueResult> GetIssueResult()
        {
            var res = new List<IssueResult>();

            var items = Doc.SelectSingleNode("root/Issues");

            foreach (XmlNode node in items.ChildNodes)
            {
                if (node.Attributes["index"] == null) continue;

                var index = node.Attributes["index"].Value.ToString().ToInt32();

                var opendate = DateTime.Parse(node.Attributes["opendate"].Value.ToString());

                var opencode = node.InnerText.ToString();

                var val = new List<int>();

                foreach (var item in node.InnerText)
                {
                    val.Add(item.ToString().ToInt32());
                }

                res.Add(new IssueResult() { IssueID = index, OpenCode = opencode, OpenDate = opendate });
            }

            var resOrder = res.OrderBy((p) => { return p.IssueID; });

            var issueResults = new List<IssueResult>();
            var ssueIndex = 0;
            foreach (var item in resOrder)
            {
                item.IssueIndex = ++ssueIndex;
                issueResults.Add(item);
            }
            return issueResults;
        }

        public static bool AddIssueNode(string xPath, IssueResult issueResult)
        {
            var res = true;

            var xtPath = string.Format("root/Issues/Issue[@index='{0}']", issueResult.IssueID - 1);

            var previoseNode = Doc.SelectSingleNode(xtPath);
            var newNode = Doc.CreateElement("Issue");
            newNode.SetAttribute("index", issueResult.IssueID.ToString());
            newNode.SetAttribute("opendate", issueResult.OpenDate.ToString("yyyy-MM-dd"));
            newNode.InnerText = issueResult.OpenCode;

            var rootNode = Doc.SelectSingleNode(xPath);

            if (previoseNode != null)
            {
                rootNode.InsertAfter(newNode, previoseNode);
            }
            else
            {
                if (issueResult.IssueID > IssueEntity.IssueResults.Last().IssueID)
                    rootNode.AppendChild(newNode);
                else
                    rootNode.InsertBefore(newNode, rootNode.FirstChild);
            }

            Doc.Save(path);

            return res;
        }

        public static void UpdateIssueNode(string xPath, string val)
        {
            var node = Doc.SelectSingleNode(xPath);

            node.InnerText = val;

            Doc.Save(path);
        }

        public static void DeleteIssueNode(string xPath)
        {
            var node = Doc.SelectSingleNode(xPath);

            if (node == null) return;

            var rootPath = xPath.Substring(0, xPath.LastIndexOf("/"));

            var rootNode = Doc.SelectSingleNode(rootPath);

            rootNode.RemoveChild(node);

            Doc.Save(path);
        }
        #endregion

        #region Output
        public static bool AddOutputNode(string xPath, int issue, int index, string val)
        {
            var res = true;

            var sb = new StringBuilder();
            sb.AppendLine(val.TrimEnd());
            sb.AppendLine(" --------------- " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " --------------- ");
            val = sb.ToString();

            var comPath = string.Format("{0}/Output{1}", xPath, issue);

            var node = Doc.SelectSingleNode(comPath);

            if (node == null)
            {
                var nnode = Doc.CreateElement(string.Format("Output{0}", issue));
                var rootNode = Doc.SelectSingleNode(xPath);

                rootNode.AppendChild(nnode);

                Doc.Save(path);
            }

            comPath = string.Format("{0}/Output{1}/Item[@index='{2}']", xPath, issue, index);

            var n = Doc.SelectSingleNode(comPath);
            if (n == null)
            {
                var newNode = Doc.CreateElement("Item");
                newNode.SetAttribute("index", index.ToString());
                newNode.InnerText = val;

                var rNode = Doc.SelectSingleNode(string.Format("{0}/Output{1}", xPath, issue));

                rNode.AppendChild(newNode);
            }
            else
            {
                n.InnerText = val;
            }

            Doc.Save(path);

            return res;
        }

        public static string GetOutput(string xPath)
        {
            var node = Doc.SelectSingleNode(xPath);

            if (node == null) return string.Empty;

            return Doc.SelectSingleNode(xPath).InnerText.Replace("        ", string.Empty);
        }
        #endregion

        #region Patterns
        public static void GeneratePatterns()
        {
            IssueEntity.CalcPatterns = new List<CalcPattern>();

            for (int i = 1; i < 8; i++)
            {
                var xPath = string.Format("root/Patterns/Item{0}/Item", i);
                var nodes = GetNodesByXPath(xPath);

                var dic = new Dictionary<string, string>();
                foreach (XmlNode item in nodes)
                {
                    var name = item.Attributes["name"].Value;

                    var pattern = item.InnerText.Replace(" ", string.Empty).Trim();

                    dic.Add(name, pattern);
                }

                var calcPattern = new CalcPattern();
                calcPattern.IssueIndex = i;
                calcPattern.Patterns = dic;
                IssueEntity.CalcPatterns.Add(calcPattern);
            }
        }

        public static string GetTextByXPath(string xPath)
        {
            var res = string.Empty;

            res = Doc.SelectSingleNode(xPath).InnerText;

            return res;
        }

        public static IDictionary<string, string> GetPatterns(string xPath)
        {
            var res = new Dictionary<string, string>();

            var items = Doc.SelectSingleNode(xPath);

            foreach (XmlNode item in items.ChildNodes)
            {
                if (item.Attributes["name"] == null) continue;

                var name = item.Attributes["name"].Value;

                var tem = item.InnerText.Replace(" ", string.Empty);

                if (tem.StartsWith("\r\n"))
                    tem = tem.Substring(2);

                var val = tem;

                if (!res.ContainsKey(name))
                    res.Add(name, val);
            }

            return res;
        }

        public static bool SaveDefaultPattern(int issueIndex, string patternName)
        {
            var res = false;

            try
            {
                var xPath = string.Format("root/Patterns/DefaultPatterns/Pattern[@index='{0}']", issueIndex);

                var node = Doc.SelectSingleNode(xPath);

                if (node != null)
                {
                    node.InnerText = patternName;

                    Doc.Save(path);

                    res = true;
                }
            }
            catch (Exception ex)
            {

            }
            return res;
        }

        public static IDictionary<string, string> GetDefaultPatterns()
        {
            var res = new Dictionary<string, string>();

            var xPath = string.Format("root/Patterns/DefaultPatterns");

            var items = Doc.SelectSingleNode(xPath);

            foreach (XmlNode item in items.ChildNodes)
            {
                if (item.Attributes["index"] == null) continue;

                var index = item.Attributes["index"].Value;

                var val = item.InnerText;

                if (!res.ContainsKey(index))
                    res.Add(index, val);
            }

            return res;
        }

        public static XmlNodeList GetNodesByXPath(string xPath)
        {
            return Doc.SelectNodes(xPath);
        }

        public static bool IsExists(string xPath)
        {
            var res = false;

            var node = Doc.SelectSingleNode(xPath);

            if (node != null)
                res = true;

            return res;
        }

        public static bool AddPatternNode(string xPath, string name, string val)
        {
            var res = true;

            var comPath = string.Format("{0}/Item[@name='{1}']", xPath, name);

            var node = Doc.SelectSingleNode(comPath);

            if (node != null) return false;

            var newNode = Doc.CreateElement("Item");
            newNode.SetAttribute("name", name);
            newNode.InnerText = val;

            var rootNode = Doc.SelectSingleNode(xPath);

            rootNode.AppendChild(newNode);

            Doc.Save(path);

            return res;
        }

        public static void UpdatePatternNode(string xPath, string val)
        {
            var node = Doc.SelectSingleNode(xPath);

            node.InnerText = val;

            Doc.Save(path);
        }

        public static void DeletePatternNode(string xPath)
        {
            var node = Doc.SelectSingleNode(xPath);

            var rootPath = xPath.Substring(0, xPath.LastIndexOf("/"));

            var rootNode = Doc.SelectSingleNode(rootPath);

            rootNode.RemoveChild(node);

            Doc.Save(path);
        }

        public static void DeletePatternChildNode(string xPath)
        {
            Doc.SelectSingleNode(xPath).RemoveAll();

            //var node = Doc.SelectSingleNode(xPath);

            //foreach (XmlNode item in node.ChildNodes)
            //{
            //    node.RemoveChild(item);
            //}

            Doc.Save(path);
        }

        public static void DeleteAllPatternNode(string xPath)
        {
            var nodes = Doc.SelectNodes(xPath);

            var rootPath = xPath.Substring(0, xPath.LastIndexOf("/"));

            var rootNode = Doc.SelectSingleNode(rootPath);

            foreach (XmlNode item in nodes)
            {
                rootNode.RemoveChild(item);
            }

            Doc.Save(path);
        }
        #endregion

        #region Summary

        public static IList<string> GetSummaryPersonNames()
        {
            var res = new List<string>();

            var node = Doc.SelectSingleNode("root/Summary");

            if (node == null) return res;

            foreach (XmlNode item in node.ChildNodes)
            {
                res.Add(item.Attributes["name"].Value);
            }

            return res;
        }

        public static Summary GetSummary(string personName, int issueNumber)
        {
            var res = new Summary();

            var comPath = string.Format("root/Summary/Person[@name='{0}']/Item[@issueNumber='{1}']", personName, issueNumber);

            var node = Doc.SelectSingleNode(comPath);

            if (node != null)
            {
                try
                {
                    res.IssueNumber = node.Attributes["issueNumber"].Value.ToInt32();
                    res.Pay = node.Attributes["pay"].Value.ToInt32();
                    res.Income = node.Attributes["income"].Value.ToInt32();
                    res.Balance = node.Attributes["balance"].Value.ToInt32();
                    res.Count = node.Attributes["count"].Value.ToInt32();
                    res.Output = node.InnerText;
                }
                catch (Exception ex)
                { }
            }

            return res;
        }

        public static bool SaveSummaryNode(Summary summary)
        {
            var res = true;

            var comPath = string.Format("root/Summary/Person[@name='{0}']/Item[@issueNumber='{1}']", summary.PersonName, summary.IssueNumber);

            var node = Doc.SelectSingleNode(comPath);

            if (node == null)
            {
                var newNode = Doc.CreateElement("Item");
                newNode.SetAttribute("issueNumber", summary.IssueNumber.ToString());
                newNode.SetAttribute("pay", summary.Pay.ToString());
                newNode.SetAttribute("income", summary.Income.ToString());
                newNode.SetAttribute("balance", summary.Balance.ToString());
                newNode.SetAttribute("count", summary.Count.ToString());
                newNode.InnerText = summary.Output;

                var rootNode = Doc.SelectSingleNode(string.Format("root/Summary/Person[@name='{0}']", summary.PersonName));

                rootNode.AppendChild(newNode);
            }
            else
            {
                node.Attributes["issueNumber"].Value = summary.IssueNumber.ToString();
                node.Attributes["pay"].Value = summary.Pay.ToString();
                node.Attributes["income"].Value = summary.Income.ToString();
                node.Attributes["balance"].Value = summary.Balance.ToString();
                node.Attributes["count"].Value = summary.Count.ToString();
                node.InnerText = summary.Output;
            }

            Doc.Save(path);

            return res;
        }
        #endregion

        #region Predictions

        public static IList<Prediction> GetAllPredictions()
        {
            var predictions = new List<Prediction>();

            var nodes = Doc.SelectNodes("root/Predictions/Item");

            var prediction = new Prediction();
            if (nodes != null)
            {
                foreach (XmlNode item in nodes)
                {
                    prediction = new Prediction();

                    prediction.IssueIndex = item.Attributes["index"].Value.ToString().ToInt32();
                    prediction.PredictionResult = item.Attributes["predictionResult"].Value.ToString();

                    predictions.Add(prediction);
                }
            }
            return predictions;
        }

        public static bool SetPredictionResult(int issueIndex, string predictionResult)
        {
            var success = false;
            
            var node = Doc.SelectSingleNode(string.Format("root/Predictions/Item[@index='{0}']", issueIndex));

            if (node == null)
            {
                node =  CreatePrediction(issueIndex);
            }

            if (node != null)
            {
                node.Attributes["predictionResult"].Value = predictionResult;
            }

            Doc.Save(path);

            return success;
        }

        private static XmlNode CreatePrediction(int issueindex)
        {
            var rootNode = Doc.SelectSingleNode("root/Predictions");

            var newNode = Doc.CreateElement("Item");
            newNode.SetAttribute("index", issueindex.ToString());
            newNode.SetAttribute("predictionResult", string.Empty);

            rootNode.AppendChild(newNode);

            return newNode;
        }

        #endregion

        public static IList<EmailDetails> GetEmailAddresses()
        {
            var res = new List<EmailDetails>();

            var nodes = Doc.SelectNodes("root/EmailAddress/Address");

            foreach (XmlNode item in nodes)
            {
                if (!string.IsNullOrEmpty(item.InnerText))
                {
                    var filter = item.Attributes["filter"].Value;
                    res.Add(new EmailDetails() { Filter = filter, Address = item.InnerText });
                }
            }

            return res;
        }

        public static string GetUri()
        {
            var res = string.Empty;

            var node = Doc.SelectSingleNode("root/settings/key[@name='Uri']");

            if (node != null)
            {
                res = node.Attributes["value"].Value.ToString();
            }

            return res;
        }

        public static bool IsCreateNew()
        {
            var node = Doc.SelectSingleNode("root/settings/key[@name='IsCreateNew']");

            if (node != null)
            {
                var res = node.Attributes["value"].Value.ToString();

                if (res.Equals("True"))
                {
                    return true;
                }
            }

            return false;
        }

        public static SelectionSettings GetSelectionSettings()
        {
            var res = new SelectionSettings();
            var node = Doc.SelectSingleNode("root/settings/key[@name='Threshold']");

            if (node != null)
            {
               res.Threshold = node.Attributes["value"].Value.ToString().ToInt32();
            }

            node = Doc.SelectSingleNode("root/settings/key[@name='Start']");

            if (node != null)
            {
                res.Start = node.Attributes["value"].Value.ToString().ToInt32();
            }

            node = Doc.SelectSingleNode("root/settings/key[@name='CharLength']");

            if (node != null)
            {
                res.CharLength = node.Attributes["value"].Value.ToString().ToInt32();
            }

            node = Doc.SelectSingleNode("root/settings/key[@name='SelectionLength']");

            if (node != null)
            {
                res.SelectionLength = node.Attributes["value"].Value.ToString().ToInt32();
            }

            return res;
        }
    }
}
