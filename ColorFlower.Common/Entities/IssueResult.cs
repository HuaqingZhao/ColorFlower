using System;
using System.Collections.Generic;

namespace ColorFlower.Common
{
    public class IssueResult
    {
        public int IssueID { get; set; }

        public int IssueIndex { get; set; }

        public string OpenCode { get; set; }

        private IList<int> openCodeInt;

        public IList<int> OpenCodeInt
        {
            get
            {
                if (openCodeInt != null) return openCodeInt;

                openCodeInt = new List<int>();
                for (int i = 0; i < 7; i++)
                {
                    openCodeInt.Add(Convert.ToInt32(OpenCode.Substring(i, 1)));
                }
                return openCodeInt;
            }
        }

        public DateTime OpenDate { get; set; }
    }
}
