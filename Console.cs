using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace HttpWarmUp.Uwp
{
    public class Console : TextBox
    {
        public void Write(string text, params object[] args)
        {
            this.Text += string.Format(text, args);
        }

        public void WriteLine(string text, params object[] args)
        {
            this.Text += string.Format(text, args) + "\r";
        }
    }
}
