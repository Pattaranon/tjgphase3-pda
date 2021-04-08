using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Data;
using System.Data.SqlServerCe;
using System.Data.SqlTypes;
using CallServices.CallingService;
using SPACE.TJG.Barcode.UI.Execute;

namespace SPACE.TJG.Barcode.UI.Connections
{
    public partial class Connection
    {
        protected CallService dbManager;

        public Connection()
        {
            string appPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly()
           .GetName().CodeBase);//\Program Files\UserInterface
            string connectionString = string.Format("Data Source=\\My Documents\\TJG.sdf;Password=pass;Persist Security Info=True", appPath);

            dbManager = new CallService(connectionString);
            try
            {
                dbManager.Open();
            }
            catch (SqlCeException)
            {
                //Executing.Instance.Insert_Log("PDA_CONNSQLCEException", sqlex.Message.ToString(), "Class_Connection", "Method_Connection()");
                throw;
            }
            catch (Exception)
            {
                //Executing.Instance.Insert_Log("PDA_CONNException", ex.Message.ToString(), "Class_Connection", "Method_Connection()");
                throw;
            }
        }
    }
}