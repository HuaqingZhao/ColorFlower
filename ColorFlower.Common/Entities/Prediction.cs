using System;
using System.Text;
using System.Linq;

namespace ColorFlower.Common.Entities
{
    /// <summary>
    /// Prediction list of histroy
    /// </summary>
    public class Prediction
    {
        /// <summary>
        /// Issue index
        /// </summary>
        public int IssueIndex { get; set; }

        /// <summary>
        /// prediction result
        ///     e.g 123-123-123-123-123-123-123
        /// </summary>
        public string PredictionResult { get; set; }

        public string PredictionResultDispaly
        {
            get
            {
                var res = new StringBuilder();

                var temp = PredictionResult.SplitTo("-");

                foreach (var item in temp)
                {
                    res.Append(item.Substring(0, 8) + "-");
                }

                var s = res.ToString();
                s = s.Substring(0, s.Length -1);
                return s;
            }
        }

        /// <summary>
        /// Actually result
        ///     e.g 1234567
        /// </summary>
        public string ActuallyResult
        {
            get
            {
                if (IssueEntity.IssueResults.LastOrDefault().IssueID < IssueIndex) return string.Empty;
                return IssueEntity.IssueResults.Where(p => p.IssueID == IssueIndex).FirstOrDefault().OpenCode;
            }
        }
        /// <summary>
        /// Match format for review
        /// </summary>
        public string MatchedFormat3
        {
            get
            {
                return GetFormatResult(3);
            }
        }
        /// <summary>
        /// Match format for review
        /// </summary>
        public string MatchedFormat4
        {
            get
            {
                return GetFormatResult(4);
            }
        }
        /// <summary>
        /// Match format for review
        /// </summary>
        public string MatchedFormat5
        {
            get
            {
                return GetFormatResult(5);
            }
        }
        /// <summary>
        /// Match format for review
        /// </summary>
        public string MatchedFormat6
        {
            get
            {
                return GetFormatResult(6);
            }
        }
        /// <summary>
        /// Match format for review
        /// </summary>
        public string MatchedFormat7
        {
            get
            {
                return GetFormatResult(7);
            }
        }
        /// <summary>
        /// Match format for review
        /// </summary>
        public string MatchedFormat8
        {
            get
            {
                return GetFormatResult(8);
            }
        }

        /// <summary>
        /// Match format for review
        /// </summary>
        public string MatchedFormat9
        {
            get
            {
                return GetFormatResult(9);
            }
        }

        private string GetFormatResult(int count)
        {
            var sb = new StringBuilder();

            if (IssueIndex > 0 && !string.IsNullOrEmpty(PredictionResult) && !string.IsNullOrEmpty(ActuallyResult))
            {
                var actualR = ActuallyResult.ToCharArray();
                var predictionR = PredictionResult.SplitTo("-");

                for (int i = 0; i < 7; i++)
                {
                    sb.Append(predictionR[i].Substring(0, count).Contains(actualR[i].ToString()) ? "=" : "@");
                }
            }
            return sb.ToString();
        }
    }
}
