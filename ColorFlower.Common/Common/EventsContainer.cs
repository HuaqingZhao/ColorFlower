using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorFlower
{
    public static class EventsContainer
    {
        public static Action<int> IssueChanged;
        public static Action IssueResultChanged;
        public static Action PatternSelectionChanged;
        public static Action PatternsChanged;
    }
}
