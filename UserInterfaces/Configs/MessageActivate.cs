using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UI.Configs
{
    public class MessageActivate
    {
        public static DialogResult DialogQuestion(string msg)
        {
            return MessageBox.Show(msg, "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
        }
    }
}
