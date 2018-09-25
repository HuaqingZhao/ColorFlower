using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ColorFlower
{
    public static class FuncExtension
    {
        public static int ToInt32(this object o)
        {
            return Convert.ToInt32(o);
        }

        public static int ToInt32(this ComboBox c)
        {
            var res = -1;

            Int32.TryParse(c.SelectedItem.ToString(), out res);

            return res;
        }

        public static int ToInt32(this string o)
        {
            return Convert.ToInt32(o);
        }

        public static string[] SplitTo(this string val, string str)
        {
            return val.Split(new string[] { str }, StringSplitOptions.None);

        }

        public static bool IsDesignMode(this UserControl ctrl)
        {
            return System.Diagnostics.Process.GetCurrentProcess().ProcessName.ToUpper() == "devenv".ToUpper();
        }

        public static bool IsDesignMode(this Form ctrl)
        {
            return System.Diagnostics.Process.GetCurrentProcess().ProcessName.ToUpper() == "devenv".ToUpper();
        }

        public static void FireEvent(this Action action)
        {
            if (action != null)
                action();
        }
    }
}
