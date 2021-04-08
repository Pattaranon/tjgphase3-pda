using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace ServiceFunction.Cursor
{
    public static class UICursor
    {
        public static void CursorWait()
        {
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
        }

        public static void CursorStop()
        {
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
        }
    }
}
