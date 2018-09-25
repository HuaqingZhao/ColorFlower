using ColorFlower.Common;
using ColorFlower.Entities;
using ColorFlower.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Diagnostics;

namespace ColorFlower
{
    public class UtilityHelper
    {
        private static object objLock = new object();

        static UtilityHelper()
        {
            EventsContainer.IssueResultChanged += Initialize;
            EventsContainer.PatternsChanged += Initialize;
        }

        public static void Load()
        {
            Initialize();
        }

        public static void Initialize()
        {
            ColorFlowerXmlUtility.GenerateIssueResult();
            ColorFlowerXmlUtility.GeneratePatterns();

            CacheIssueID();
            CacheIssueIndex();
        }

        public static int Analysis(int issueNo, string calcPattern)
        {
            var temp = calcPattern.SplitTo(";");

            var res = 0;

            for (int i = 0; i < temp.Length; i++)
            {
                res += GetItem(issueNo, temp[i]);
            }

            return (int)(res % 10);
        }

        public static int GetItem(int issueNo, string val)
        {
            var temp = val.Split(new string[] { "," }, StringSplitOptions.None);

            var currentIssueID = GetIssueID(GetIssueIndex(issueNo) - Convert.ToInt32(temp[0]));

            if (string.IsNullOrEmpty(currentIssueID))
                return 0;

            return GetItem(Convert.ToInt32(GetIssueID(GetIssueIndex(issueNo) - Convert.ToInt32(temp[0]))), Convert.ToInt32(temp[1]));
        }

        public static int GetItem(int issueNo, int issueIndex)
        {
            return GetResult(issueNo, issueIndex);
        }

        public static IList<string> AnalysisAll(int issueNo, int issueIndex, int percentThreshold, int first)
        {
            var res = new List<string>();

            Log("AnalysisAll - start");

            //x,x
            for (int i = 1; i < 10; i++)
            {
                for (int k = 1; k < 8; k++)
                {
                    Calc(issueNo, issueIndex, string.Format("{0},{1}", i, k), res, percentThreshold, first);
                }
            }

            //x,x;x,x
            for (int q = 1; q < 10; q++)
            {
                for (int w = 1; w < 8; w++)
                {
                    for (int i = 1; i < 10; i++)
                    {
                        for (int k = 1; k < 8; k++)
                        {
                            if (i < q) continue;

                            if (i == q && w < k) continue;

                            Calc(issueNo, issueIndex, string.Format("{0},{1};{2},{3}", q, w, i, k), res, percentThreshold, first);
                        }
                    }

                }
            }

            //x,x;x,x;x,x
            for (int a = 1; a < 5; a++)
            {
                for (int b = 1; b < 8; b++)
                {
                    for (int q = 1; q < 10; q++)
                    {
                        for (int w = 1; w < 8; w++)
                        {
                            if (q < a) continue;

                            if (a == q && b < w) continue;

                            for (int i = 1; i < 10; i++)
                            {
                                for (int k = 1; k < 8; k++)
                                {
                                    if (i < q) continue;

                                    if (i == q && w < k) continue;

                                    Calc(issueNo, issueIndex, string.Format("{0},{1};{2},{3};{4},{5}", a, b, q, w, i, k), res, percentThreshold, first);
                                }
                            }
                        }
                    }
                }
            }
            Log("AnalysisAll - end");
            return res;
        }

        public static void Calc(int key, int index, string pattern, IList<string> calcResultPercent, int percentThreshold, int first)
        {
            var patterns = pattern.SplitTo(Environment.NewLine);

            var val = 0;
            var count = 0;

            var issueIndex = UtilityHelper.GetIssueIndex(key);
            for (int k = issueIndex; k > 2; k--)
            {
                var issueID = UtilityHelper.GetIssueIDInt(k);
                var res = new List<int>();

                for (int i = 0; i < patterns.Length; i++)
                {
                    if (string.IsNullOrEmpty(patterns[i])) continue;

                    try
                    {
                        var r = Analysis(issueID, patterns[i]);

                        if (r < 0) break;

                        if (!res.Contains(r))
                            res.Add(r);
                    }
                    catch (ArgumentOutOfRangeException ex)
                    {
                        return;
                    }
                }

                res.Sort();

                var ss = new List<int>();
                for (int i = 0; i < 10; i++)
                {
                    if (!res.Contains(i))
                        ss.Add(i);
                }

                val += ss.Contains(GetItem(issueID, index)) ? 1 : 0;
                count++;

                if (first == issueID) break;
            }

            if (count > 0)
            {
                var percent = Math.Round((decimal)((double)val / (double)(count)), 2) * 100;
                if (percent >= percentThreshold)
                    calcResultPercent.Add(pattern);
            }
        }

        public static CalcResultItem CalcAll(int issueNumber, int issueIndex, string patternName)
        {
            if (string.IsNullOrEmpty(patternName)) return new CalcResultItem();

            IDictionary<string, string> ps = GetPattern(issueIndex);

            var patterns = new Dictionary<string, string>();

            foreach (KeyValuePair<string, string> item in ps)
            {
                if (patternName.Equals("Wrong"))
                {
                    if (item.Key.StartsWith("W"))
                    {
                        patterns.Add(item.Key, item.Value);
                    }
                }
                else
                {
                    if (item.Key.Equals(patternName))
                    {
                        patterns.Add(item.Key, item.Value);
                    }
                }
            }

            return CalcAll(issueNumber, issueIndex, patterns);
        }

        public static string GenerateHistory(int issueNumber, int issueIndex, string patternName)
        {
            var res = string.Empty;

            var sb = new StringBuilder();

            if (string.IsNullOrEmpty(patternName)) return res;

            IDictionary<string, string> ps = GetPattern(issueIndex);

            var patterns = new Dictionary<string, string>();

            foreach (KeyValuePair<string, string> item in ps)
            {
                if (item.Key.Equals(patternName))
                {
                    res = Calc(issueIndex, item.Value.ToString());
                    break;
                }
            }

            return res.Length >= 20 ? res.Substring(0, 20) : res;
        }

        public static bool AnalysisCurrent(int issueIndex, string ps)
        {
            var res = false;

            var reg = new Regex("@");

            res = (reg.Matches(Calc(issueIndex, ps).Substring(0, 15)).Count <= 2);

            return res;
        }

        public static bool AnalysisCurrent(int issueIndex, string ps, int start, int charLength, int selectionLength)
        {
            var res = false;

            var reg = new Regex("@");
            var str = Calc(issueIndex, ps).Substring(start, charLength);

            if (selectionLength <= charLength / 2)
            {
                res = reg.Matches(str).Count <= selectionLength;
            }
            else
            {
                res = reg.Matches(str).Count >= selectionLength;
            }

            return res;
        }

        public static bool AnalysisCurrentWrong(int issueIndex, string ps)
        {
            var res = false;

            var reg = new Regex("@");

            res = reg.Matches(Calc(issueIndex, ps).Substring(0, 15)).Count >= 13;

            return res;
        }

        public static string Calc(int issueIndex, string ps)
        {
            var res1 = string.Empty;
            var s = new StringBuilder();

            try
            {
                var patterns = ps.SplitTo(Environment.NewLine);

                //for (int i = 0; i < patterns.Length; i++)
                //{
                //    var p = new Regex(@"\d");
                //    var ms = p.Matches(patterns[i]);

                //    if ((ms.Count % 2) != 0)
                //    {
                //        return res1;
                //    }
                //}

                var key = IssueEntity.IssueResults.Last().IssueIndex;
                var minius = key - IssueEntity.IssueResults.Count;

                for (int k = key; k > minius; k--)
                {
                    var displayID = Convert.ToInt32(UtilityHelper.GetIssueID(k));
                    var res = new List<int>();

                    for (int i = 0; i < patterns.Length; i++)
                    {
                        if (string.IsNullOrEmpty(patterns[i])) continue;

                        try
                        {
                            var r = UtilityHelper.Analysis(displayID, patterns[i]);

                            if (r < 0)
                                throw new Exception();

                            if (!res.Contains(r))
                                res.Add(r);
                        }
                        catch (ArgumentOutOfRangeException ex)
                        {
                            return res1;
                        }
                    }

                    res.Sort();

                    var ss = new List<int>();
                    for (int i = 0; i < 10; i++)
                    {
                        if (!res.Contains(i))
                        {
                            ss.Add(i);
                        }
                    }

                    s.Append(ss.Contains(GetItem(displayID, issueIndex)) ? "=" : "@");
                    res1 = s.ToString();
                }
            }
            catch (Exception ex)
            {
                res1 = null;

            }

            return res1;
        }

        public static CalcResultItem CalcAll(int issueNumber, int issueIndex, IDictionary<string, string> ps)
        {
            var res = new CalcResultItem();

            try
            {
                if (ps == null) return res;

                var sb = new StringBuilder();
                var o = new List<int>();

                foreach (KeyValuePair<string, string> item in ps)
                {
                    var output = new List<int>();

                    if (string.IsNullOrEmpty(item.Value)) continue;

                    var patterns = item.Value.SplitTo(Environment.NewLine);

                    for (int i = 0; i < patterns.Length; i++)
                    {
                        if (string.IsNullOrEmpty(patterns[i])) continue;

                        try
                        {
                            var r = UtilityHelper.Analysis(issueNumber, patterns[i]);

                            if (r < 0 && r > 9) return res;

                            if (!output.Contains(r))
                            {
                                output.Add(r);
                            }
                        }
                        catch (Exception ex)
                        {

                        }
                    }

                    for (int k = 0; k < 10; k++)
                    {
                        if (!output.Contains(k))
                        {
                            if (!o.Contains(k))
                                o.Add(k);
                        }
                    }
                }

                var result = GetItem(issueNumber, issueIndex).ToString();

                o.Sort();

                sb.Append(string.Format("[{0}]   ", result));

                foreach (var item in o)
                {
                    sb.Append(item + " ");
                }

                res.CaclString = sb == null ? string.Empty : sb.ToString();
                res.IsMatch = sb.ToString().Substring(2).Contains(result);
                res.CalcCount = o.Count == 0 ? 10 : o.Count;
            }
            catch (Exception ex)
            {
            }

            return res;
        }

        public static IDictionary<string, string> GetPattern(int issueIndex)
        {
            return IssueEntity.CalcPatterns.Where(p => p.IssueIndex == issueIndex).FirstOrDefault().Patterns;
        }

        public static int GetResult(int issueNo, int issueIndex)
        {
            var res = -1;
            var en = IssueEntity.IssueResults.Where(p => p.IssueID == issueNo).FirstOrDefault();

            if (en != null)
            {
                res = en.OpenCodeInt[issueIndex - 1];
            }
            return res;
        }

        public static string GetIssueResult(string displayNo)
        {
            return IssueEntity.IssueResults.Where(p => p.IssueID.ToString().Equals(displayNo)).FirstOrDefault().OpenCode;
        }

        private static IDictionary<int, string> issueIDs = new Dictionary<int, string>();

        public static string GetIssueID(int issueIndex)
        {
            return issueIDs.ContainsKey(issueIndex) ? issueIDs[issueIndex] : string.Empty;
        }

        private static void CacheIssueID()
        {
            issueIDs = new Dictionary<int, string>();

            foreach (var item in IssueEntity.IssueResults)
            {
                issueIDs.Add(item.IssueIndex, item.IssueID.ToString());
            }

            issueIDs.Add(IssueEntity.IssueResults.Last().IssueIndex + 1, (IssueEntity.IssueResults.Last().IssueID + 1).ToString());
        }

        public static int GetIssueIDInt(int issueIndex)
        {
            return Convert.ToInt32(GetIssueID(issueIndex));
        }

        private static IDictionary<int, int> issueIndeies;

        public static int GetIssueIndex(int issueID)
        {
            return issueIndeies.ContainsKey(issueID) ? issueIndeies[issueID] : -1;
        }

        private static void CacheIssueIndex()
        {
            issueIndeies = new Dictionary<int, int>();
            foreach (var item in IssueEntity.IssueResults)
            {
                issueIndeies.Add(item.IssueID, item.IssueIndex);
            }

            issueIndeies.Add(IssueEntity.IssueResults.Last().IssueID + 1, IssueEntity.IssueResults.Last().IssueIndex + 1);
        }

        public static void ClearResult()
        {
            if (IssueEntity.IssueResults.Count > 0)
            {
                var last = IssueEntity.IssueResults.Last().IssueID + 1;

                var count = IssueEntity.IssueResults.Last().IssueID - 35;

                if (count > 0)
                {
                    for (int i = count; i > 0; i--)
                    {
                        var xpath = string.Format("root/Issues/Issue[@index='{0}']", i);

                        ColorFlowerXmlUtility.DeleteIssueNode(xpath);
                    }
                }

                if (EventsContainer.IssueResultChanged != null)
                {
                    UtilityHelper.Initialize();
                    EventsContainer.IssueResultChanged();
                }
            }
        }

        public static AnalysisItem CalcAnalysisItem(int key, string ps)
        {
            var ai = new AnalysisItem();

            try
            {
                var patterns = ps.SplitTo(Environment.NewLine);

                var res = new List<int>();

                for (int i = 0; i < patterns.Length; i++)
                {
                    if (string.IsNullOrEmpty(patterns[i])) continue;

                    try
                    {
                        var r = UtilityHelper.Analysis(key, patterns[i]);

                        if (r < 0)
                            throw new Exception();

                        if (!res.Contains(r))
                            res.Add(r);
                    }
                    catch (ArgumentOutOfRangeException ex)
                    {
                        return ai;
                    }
                }

                res.Sort();

                ai.Item0 = !res.Contains(0) ? "T" : "F";
                ai.Item1 = !res.Contains(1) ? "T" : "F";
                ai.Item2 = !res.Contains(2) ? "T" : "F";
                ai.Item3 = !res.Contains(3) ? "T" : "F";
                ai.Item4 = !res.Contains(4) ? "T" : "F";
                ai.Item5 = !res.Contains(5) ? "T" : "F";
                ai.Item6 = !res.Contains(6) ? "T" : "F";
                ai.Item7 = !res.Contains(7) ? "T" : "F";
                ai.Item8 = !res.Contains(8) ? "T" : "F";
                ai.Item9 = !res.Contains(9) ? "T" : "F";
            }
            catch (Exception ex)
            {

            }

            return ai;
        }

        public static List<AnalysisItem> GenerateAnalysisItems(int issueNumber, int issueIndex)
        {
            var res = new List<AnalysisItem>();

            var patterns = UtilityHelper.GetPattern(issueIndex);

            foreach (KeyValuePair<string, string> item in patterns)
            {
                res.Add(UtilityHelper.CalcAnalysisItem(issueNumber, item.Value));
            }
            return res;
        }

        public static IList<CalcResult> GeneratorCalcResult(IDictionary<int, string> SelectedPatternNames)
        {
            var calcResults = new List<CalcResult>();

            var maxIssueNumber = IssueEntity.IssueResults.Last().IssueIndex + 1;

            var count = maxIssueNumber - Consts.TakeCount;

            for (int i = maxIssueNumber; i > count; i--)
            {
                var issueid = UtilityHelper.GetIssueID(i);

                if (string.IsNullOrEmpty(issueid)) break;

                var issueID = issueid.ToInt32();
                var calcResult = new CalcResult();

                var resultMatches = new Dictionary<int, bool>();

                calcResult.IssueNumber = issueID;
                calcResult.DisplayNumber = issueID.ToString();

                var i1 = UtilityHelper.CalcAll(issueID, 1, SelectedPatternNames[1]);
                calcResult.Index1 = i1.CaclString;
                resultMatches.Add(1, i1.IsMatch);

                var i2 = UtilityHelper.CalcAll(issueID, 2, SelectedPatternNames[2]);
                calcResult.Index2 = i2.CaclString;
                resultMatches.Add(2, i2.IsMatch);

                var i3 = UtilityHelper.CalcAll(issueID, 3, SelectedPatternNames[3]);
                calcResult.Index3 = i3.CaclString;
                resultMatches.Add(3, i3.IsMatch);

                var i4 = UtilityHelper.CalcAll(issueID, 4, SelectedPatternNames[4]);
                calcResult.Index4 = i4.CaclString;
                resultMatches.Add(4, i4.IsMatch);

                var i5 = UtilityHelper.CalcAll(issueID, 5, SelectedPatternNames[5]);
                calcResult.Index5 = i5.CaclString;
                resultMatches.Add(5, i5.IsMatch);

                var i6 = UtilityHelper.CalcAll(issueID, 6, SelectedPatternNames[6]);
                calcResult.Index6 = i6.CaclString;
                resultMatches.Add(6, i6.IsMatch);

                var i7 = UtilityHelper.CalcAll(issueID, 7, SelectedPatternNames[7]);
                calcResult.Index7 = i7.CaclString;
                resultMatches.Add(7, i7.IsMatch);

                calcResult.ResultMatches = resultMatches;

                var itemcount = string.Format("{0}  {1}  {2}",
                    i1.CalcCount * i4.CalcCount,
                    i1.CalcCount * i2.CalcCount * i3.CalcCount * i4.CalcCount,
                    i1.CalcCount * i2.CalcCount * i3.CalcCount * i4.CalcCount * i5.CalcCount * i6.CalcCount * i7.CalcCount * 2);

                calcResult.Count = itemcount;

                calcResults.Add(calcResult);
            }

            return calcResults;
        }

        public static IList<ResultDetails> DetailsCalc(int issueNumber, int issueIndex, string patternsText)
        {
            var rds = new List<ResultDetails>();
            var s = new StringBuilder();

            try
            {
                var patterns = patternsText.SplitTo(Environment.NewLine);

                for (int i = 0; i < patterns.Length; i++)
                {
                    var p = new Regex(@"\d");
                    var ms = p.Matches(patterns[i]);

                    if ((ms.Count % 2) != 0)
                    {
                        return rds;
                    }
                }

                var index = UtilityHelper.GetIssueIndex(issueNumber);

                // the newly one which does not open.
                if (issueNumber == IssueEntity.IssueResults.Last().IssueID + 1)
                {
                    index = IssueEntity.IssueResults.Last().IssueIndex + 1;
                }

                for (int k = index; k > 0; k--)
                {
                    var rd = new ResultDetails();
                    var res = new List<int>();

                    var number = 0;

                    if (k == IssueEntity.IssueResults.Last().IssueIndex + 1)
                    {
                        number = IssueEntity.IssueResults.Last().IssueID + 1;
                    }
                    else
                    {
                        number = Convert.ToInt32(UtilityHelper.GetIssueID(k));
                    }

                    for (int i = 0; i < patterns.Length; i++)
                    {
                        if (string.IsNullOrEmpty(patterns[i])) continue;

                        try
                        {
                            var r = UtilityHelper.Analysis(number, patterns[i]);

                            if (r < 0)
                                throw new Exception();

                            if (!res.Contains(r))
                                res.Add(r);
                        }
                        catch (ArgumentOutOfRangeException ex)
                        {
                            rds = new List<ResultDetails>();
                            return rds;
                        }
                    }

                    res.Sort();

                    var includeNumbers = new StringBuilder();
                    var excludeNumbers = new StringBuilder();

                    var ss = new List<int>();
                    for (int i = 0; i < 10; i++)
                    {
                        if (!res.Contains(i))
                        {
                            ss.Add(i);
                            includeNumbers.Append(i + " ");
                        }
                    }

                    foreach (var item in res)
                    {
                        excludeNumbers.Append(item + " ");
                    }

                    rd.IssueNumber = UtilityHelper.GetIssueIDInt(k);
                    rd.IssueIndex = issueIndex;
                    rd.DisplayNumber = UtilityHelper.GetIssueID(k);
                    rd.ResultNumber = GetItem(UtilityHelper.GetIssueIDInt(k), issueIndex);
                    rd.IncludeNumbers = includeNumbers.ToString();
                    rd.ExcludeNumbers = excludeNumbers.ToString();
                    var isMatch = ss.Contains(GetItem(UtilityHelper.GetIssueIDInt(k), issueIndex)) ? "对" : "错";
                    rd.IsMatch = isMatch;

                    rds.Add(rd);
                }
            }
            catch (Exception ex)
            {

            }

            return rds;
        }

        public static void AutoAnalysis()
        {
            CalcAllRight();

            CalcAllWrong();

            ClearAllPatterns();

            UtilityHelper.Initialize();
            EventsContainer.PatternsChanged.FireEvent();
            EmailUtility.Send();
        }

        private static void AnalysisCompleted(Task t)
        {
            ClearAllPatterns();

            UtilityHelper.Initialize();
            EventsContainer.PatternsChanged.FireEvent();
            EmailUtility.Send();
        }

        private static void CalcAllRight()
        {
            var first = IssueEntity.IssueResults.First().IssueID;
            var sourceIssueNumber = IssueEntity.IssueResults.Last().IssueID - 10;


            for (int index = 1; index < 8; index++)
            {
                var analysisOutput = UtilityHelper.AnalysisAll(sourceIssueNumber, index, 95, first);

                var matches = CalcMatches(index, analysisOutput);

                for (int i = 0; i < matches.Count; i++)
                {
                    var xPath = string.Format("root/Patterns/Item{0}", index);
                    var patternName = string.Format("P{0,00000}", i);
                    ColorFlowerXmlUtility.AddPatternNode(xPath, patternName, matches[i].Trim());
                }
            }
        }

        public static void Analysis(int issueNumber, int issueIndex, int threshold, int start, int charLength, int selectionLength, ref int completeCount)
        {
            var first = IssueEntity.IssueResults.First().IssueID;

            var analysisOutput = UtilityHelper.AnalysisAll(issueNumber, issueIndex, threshold, first);

            var matches = CalcMatches(issueIndex, analysisOutput, start, charLength, selectionLength);

            for (int i = 0; i < matches.Count; i++)
            {
                var xPath = string.Format("root/Patterns/Item{0}", issueIndex);
                var patternName = string.Format("P{0,00000}", i);
                ColorFlowerXmlUtility.AddPatternNode(xPath, patternName, matches[i].Trim());
            }

            completeCount++;
        }

        private static void CalcAllWrong()
        {
            var first = IssueEntity.IssueResults.First().IssueID;

            for (int i = 8; i < 16; i++)
            {
                var sourceIssueNumber = IssueEntity.IssueResults.Last().IssueID - i;

                for (int index = 1; index <= 7; index++)
                {
                    if (index > 7) return;

                    var analysisOutput = UtilityHelper.AnalysisAll(sourceIssueNumber, index, 90, first);

                    var matches = CalcMatchesWrong(index, analysisOutput);

                    for (int k = 0; k < matches.Count; k++)
                    {
                        var xPath = string.Format("root/Patterns/Item{0}", index);
                        var patternName = string.Format("W{0,00000}", k);
                        ColorFlowerXmlUtility.AddPatternNode(xPath, patternName, matches[k].Trim());
                    }
                }
            }
        }

        public static void ClearAllPatterns()
        {
            UtilityHelper.Initialize();

            for (int i = 1; i < 8; i++)
            {
                var xPath = string.Format("root/Patterns/Item{0}", i);

                ColorFlowerXmlUtility.DeletePatternChildNode(xPath);
            }
        }

        public static int ClearPatterns(int issueIndex)
        {
            var count = 0;
            var uselessPatterns = new List<string>();

            var ps = UtilityHelper.GetPattern(issueIndex);

            foreach (KeyValuePair<string, string> item in ps)
            {
                var match = AnalysisCurrent(issueIndex, item.Value);

                if (!match)
                {
                    if (!uselessPatterns.Contains(item.Key))
                        uselessPatterns.Add(item.Key);
                }
            }

            foreach (var item in uselessPatterns)
            {
                var xPath = string.Format("root/Patterns/Item{0}/Item[@name='{1}']", issueIndex, item);

                ColorFlowerXmlUtility.DeletePatternNode(xPath);
                count++;
            }

            return count;
        }

        public static void Reinitialize()
        {
            UtilityHelper.Initialize();
            EventsContainer.PatternsChanged.FireEvent();
        }

        private static List<string> CalcMatches(int issueIndex, IList<string> analysisOutput)
        {
            var matches = new List<string>();

            var k = 8;

            for (int i = 0; i < analysisOutput.Count; )
            {
                var sb1 = new StringBuilder();

                for (int l = 0; l < k; l++)
                {
                    sb1.AppendLine(analysisOutput[i + l]);
                }

                if (UtilityHelper.AnalysisCurrent(issueIndex, sb1.ToString()))
                {
                    matches.Add(sb1.ToString());
                    i += k;
                }
                else
                    i++;

                if (i + k >= analysisOutput.Count) break;
            }

            return matches;
        }

        private static List<string> CalcMatches(int issueIndex, IList<string> analysisOutput, int start, int charLength, int selectionLength)
        {
            Log("CalcMatches - start");

            var matches = new List<string>();

            for (int k = 8; k > 7; k--)
            {
                for (int i = 0; i < analysisOutput.Count; )
                {
                    var sb1 = new StringBuilder();

                    for (int l = 0; l < k; l++)
                    {
                        sb1.AppendLine(analysisOutput[i + l]);
                    }

                    if (UtilityHelper.AnalysisCurrent(issueIndex, sb1.ToString(), start, charLength, selectionLength))
                    {
                        matches.Add(sb1.ToString());
                        i += k;
                    }
                    else
                        i++;

                    if (i + k >= analysisOutput.Count) break;
                }
            }
            Log("CalcMatches - end");
            return matches;
        }

        private static List<string> CalcMatchesWrong(int issueIndex, IList<string> analysisOutput)
        {
            var matches = new List<string>();

            for (int k = 8; k > 5; k--)
            {
                for (int i = 0; i < analysisOutput.Count; )
                {
                    var sb1 = new StringBuilder();

                    for (int l = 0; l < k; l++)
                    {
                        sb1.AppendLine(analysisOutput[i + l]);
                    }

                    if (UtilityHelper.AnalysisCurrentWrong(issueIndex, sb1.ToString()))
                    {
                        matches.Add(sb1.ToString());
                        i += k;
                    }
                    else
                        i++;

                    if (i + k >= analysisOutput.Count) break;
                }
            }
            return matches;
        }

        public static IDictionary<int, int> GenerateNumber(int issueNumber, int issueIndex)
        {
            IDictionary<int, int> dic = new Dictionary<int, int>();

            for (int i = 0; i < 10; i++)
            {
                dic.Add(i, 0);
            }

            try
            {
                var ps = UtilityHelper.GetPattern(issueIndex);

                foreach (KeyValuePair<string, string> item in ps)
                {
                    if (string.IsNullOrEmpty(item.Value)) continue;

                    // ignore W template
                    if (item.Key.StartsWith("W")) continue;

                    var patterns = item.Value.SplitTo(Environment.NewLine);

                    var l = new List<int>();
                    for (int i = 0; i < patterns.Length; i++)
                    {
                        if (string.IsNullOrEmpty(patterns[i])) continue;

                        try
                        {
                            var r = UtilityHelper.Analysis(issueNumber, patterns[i]);

                            if (r < 0 && r > 9) return dic;

                            l.Add(r);
                        }
                        catch (ArgumentOutOfRangeException ex)
                        {
                            return dic;
                        }
                    }

                    for (int i = 0; i < 10; i++)
                    {
                        if (!l.Contains(i))
                        {
                            int s = 1 + dic[i];

                            foreach (KeyValuePair<int, int> item1 in dic)
                            {
                                if (item1.Key == i)
                                {
                                    dic.Remove(i);

                                    dic.Add(i, s);

                                    break;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return dic;
        }

        public static void NewResultAdded(IssueResult issueResult)
        {
            var xPath = string.Format("root/Issues");

            ColorFlowerXmlUtility.AddIssueNode(xPath, issueResult);
        }

        public static void Log(string log)
        {
            Trace.WriteLine(string.Format("{0} - {1}", DateTime.Now.ToLongTimeString(), log));
        }
    }
}
