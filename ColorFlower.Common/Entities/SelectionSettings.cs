using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorFlower.Common.Entities
{
    public class SelectionSettings
    {
        public int Threshold { get; set; }
        public int Start { get; set; }
        public int CharLength { get; set; }
        public int SelectionLength { get; set; }
    }
}
