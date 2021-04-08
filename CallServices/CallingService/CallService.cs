using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlServerCe;

namespace CallServices.CallingService
{
    public class CallService : IDisposable
    {
        #region Fields
        private SqlCeConnection sqlCEConnection;
        private SqlCeTransaction sqlCETransaction;
        #endregion

        #region Constructors
        public CallService()
        {
            SqlCeCommand ce = new SqlCeCommand();
        }
        public CallService(string connectionString)
        {
            sqlCEConnection = new SqlCeConnection(connectionString);
        }        
        #endregion

        #region Properties

        public string ConnectionString 
        {
            get
            {
                return sqlCEConnection.ConnectionString;
            }
            set
            {
                sqlCEConnection.ConnectionString = value;
            }
        }
        public SqlCeConnection Connection
        {
            get
            {
                return sqlCEConnection;
            }
            set
            {
                sqlCEConnection = value;
            }
        
        }
        public static System.Drawing.Color Color;

        #endregion
                              
        #region Methods
        
        public void Open()
        {
            try
            {
                sqlCETransaction = null;
                if (sqlCEConnection.State == ConnectionState.Closed)
                {
                    sqlCEConnection.Open();
                }
            }
            catch (Exception)
            {                
                throw;
            }
        }

        public void BeginTransaction()
        {
            try
            {
                sqlCETransaction = sqlCEConnection.BeginTransaction();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void BeginTransaction(IsolationLevel isolationLevel)
        {
            try
            {
                sqlCETransaction = sqlCEConnection.BeginTransaction(isolationLevel);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void CommitTransaction()
        {
            try
            {
                sqlCETransaction.Commit();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void RollBackTransaction()
        {
            try
            {
                sqlCETransaction.Rollback();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void PreparingTransaction(SqlCeCommand command)
        {
            if (sqlCETransaction != null)
            {
                command.Transaction = sqlCETransaction;
            }
        }
      
        public int ExecuteNonQuery(SqlCeCommand command)
        {
            try
            {
                PreparingTransaction(command);
                command.Connection = sqlCEConnection;
                return command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public SqlCeDataReader ExecuteReader(SqlCeCommand command)
        {
            try
            {
                PreparingTransaction(command);
                command.Connection = sqlCEConnection;
                return command.ExecuteReader();
            }
            catch (Exception)
            {                
                throw;
            }
        }
        public object ExecuteScalar(SqlCeCommand command)
        {
            try
            {
                PreparingTransaction(command);
                command.Connection = sqlCEConnection;
                return command;
            }
            catch (Exception)
            {                
                throw;
            }
        }
        public DataTable ExecuteToDataTable(SqlCeCommand command)
        {
            try
            {
                PreparingTransaction(command);
                command.Connection = sqlCEConnection;
                SqlCeDataReader dr = command.ExecuteReader();
                DataTable dt=new DataTable();
                dt.Load(dr);
                return dt;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet ExecuteToDataSet(SqlCeCommand command)
        {
            try
            {
                PreparingTransaction(command);
                command.Connection = sqlCEConnection;
                SqlCeDataAdapter da = new SqlCeDataAdapter(command);
                DataSet ds = new DataSet();
                da.Fill(ds);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Close()
        {
            if (sqlCEConnection != null) 
            {
                sqlCEConnection.Close();
            }
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}