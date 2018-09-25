using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using ColorFlower.Common;
using ColorFlower.Utilities;
using System.Threading;

namespace Bloom
{
    class Program
    {
        static void Main(string[] args)
        {
            var first = true;
            while (true)
            {
                if (first || DateTime.Now.Hour == 21)
                {
                    var rootPath = @"F:\CF";

                    var entryDic = new Dictionary<string, string>();
                    foreach (var item in Directory.GetDirectories(rootPath))
                    {
                        entryDic.Add(Path.GetFileNameWithoutExtension(item), Path.Combine(item, "ColorFlower.xml"));
                    }

                    var currentIssueID = SyncResultUtility.GetResultFromWeb().LastOrDefault().IssueID + 1;
                    var predictions = new Dictionary<string, string>();

                    var output = new StringBuilder();
                    output.Append("<div><table border='1'><tr><th>型</th><th>预测</th></tr>");
                    foreach (KeyValuePair<string, string> item in entryDic)
                    {
                        var doc = new XmlDocument();
                        doc.Load(item.Value);

                        var node = doc.SelectSingleNode(string.Format("//Predictions/Item[@index='{0}']", currentIssueID));
                        output.Append(string.Format("<tr><td>{0}</td><td>{1}</td></tr>", item.Key, node.Attributes["predictionResult"].Value.ToString().Substring(0, 10)));
                        predictions.Add(item.Key, node.Attributes["predictionResult"].Value.ToString().Substring(0, 10));
                    }

                    output.Append("</table></div>");
                    EmailUtility.SendBloom(output.ToString(), currentIssueID);
                    first = false;
                }
                else
                    Thread.Sleep(1000 * 60 * 60);
            }
        }
    }
}
