using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlServerCe;
using System.Data.SqlTypes;
using EntitiesTemp.GIDelivery;
using EntitiesTemp.GRProduction;
using EntitiesTemp.GIRefIOEmpty;
using EntitiesTemp.Customer;
using EntitiesTemp.DistributionRet;
using System.Globalization;
using ServiceFunction.Function;
using EntitiesTemp.Warehouse;
using EntitiesTemp.OtherScan;
using EntitiesTemp.Factory;
using EntitiesTemp.Delivery;
using EntitiesTemp.SAP;

namespace UserInterfaces.Execute
{
    public class Executing : Connections.Connection, IDisposable
    {
        #region I. Instance
        public static Executing Instance
        {
            get
            {
                Executing instance = new Executing();
                return instance;
            }
        }
        #endregion

        #region 0. getDateTime
        //Get DateTime
        public DateTime GetDateServer()
        {
            try
            {
                DateTime dtpDateTime = DateTime.Now;

                string sql = "select GETDATE() AS getDateTime";

                SqlCeCommand command = new SqlCeCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = sql.ToString();
                dbManager.Open();

                DataTable dtDateTime = (DataTable)dbManager.ExecuteToDataTable(command);

                return (DateTime)dtDateTime.Rows[0]["getDateTime"];
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region B. Binding

        public DataTable getFactory()
        {
            try
            {
                DataTable dt_get = new DataTable();

                string sql = "SELECT * " +
                             "FROM t_factory ";

                SqlCeCommand command = new SqlCeCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = sql.ToString();
                dbManager.Open();
                dt_get = dbManager.ExecuteToDataTable(command);

                return dt_get;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region 1. GI Ref. Delivery

        //Check Serial Number
        public bool checkGIDeliverySerialDuplicate(string _serial_number)
        {
            try
            {
                DataTable dt_chk_serial = new DataTable();
                string sql = "SELECT * " +
                             "FROM GI_Delivery " +
                             "WHERE (serial_number='" + _serial_number + "') ";

                SqlCeCommand command = new SqlCeCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = sql.ToString();
                dbManager.Open();
                dt_chk_serial = dbManager.ExecuteToDataTable(command);

                if (dt_chk_serial.Rows.Count > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool IsMatchDeliveryNoAndSerialNumber(string deliveryNo, string serialNumber)
        {
            try
            {
                DataTable dt_chk_serial = new DataTable();
                string sql = "SELECT * " +
                             "FROM SAP_Import " +
                             "where Delivery_No = '" + deliveryNo + "' and Serial_Number =  '" + serialNumber + "' ";

                SqlCeCommand command = new SqlCeCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = sql.ToString();
                dbManager.Open();
                dt_chk_serial = dbManager.ExecuteToDataTable(command);

                if (dt_chk_serial.Rows.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool SaveGIDelivery(GIDeliveryEntity _ent)
        {
            bool _result = false;
            try
            {

                StringBuilder sql = new StringBuilder();
                sql.Append("INSERT INTO GI_Delivery (serial_number, do_no, create_date) ");
                sql.Append(string.Format("VALUES ('{0}','{1}','{2}') ", _ent.serial_number, _ent.do_no, _ent.create_date));

                SqlCeCommand command = new SqlCeCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = sql.ToString();

                dbManager.Open();
                if (dbManager.ExecuteNonQuery(command) > 0)
                    _result = true;
            }
            catch (SqlCeException)
            {
                _result = false;
                throw;
            }
            finally
            {
                if (dbManager != null)
                {
                    dbManager.Dispose();
                }
            }
            return _result;
        }
        public DataTable getGIDeliveryForDel()
        {
            try
            {
                DataTable dt_get = new DataTable();

                string sql = "SELECT * " +
                             "FROM GI_Delivery " +
                             "ORDER BY create_date DESC";

                SqlCeCommand command = new SqlCeCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = sql.ToString();
                dbManager.Open();
                dt_get = dbManager.ExecuteToDataTable(command);

                return dt_get;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public string CountGIALL()
        {
            string value = string.Empty;
            try
            {
                DataTable dt_get = new DataTable();
                string _count = string.Empty;

                string sql = "SELECT COUNT(*) AS count_gi_row " +
                             "FROM GI_Delivery ";

                SqlCeCommand command = new SqlCeCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = sql.ToString();
                dbManager.Open();
                dt_get = dbManager.ExecuteToDataTable(command);

                if (dt_get.Rows.Count > 0)
                    value = Convert.ToString(dt_get.Rows[0]["count_gi_row"]);

                return value;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public string CountGIByDO(string _do_no)
        {
            string value = string.Empty;
            try
            {
                DataTable dt_get = new DataTable();
                string _count = string.Empty;

                string sql = "SELECT COUNT(*) AS count_gi_by_do_no " +
                             "FROM GI_Delivery " +
                             "WHERE (do_no='" + _do_no + "') ";

                SqlCeCommand command = new SqlCeCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = sql.ToString();
                dbManager.Open();
                dt_get = dbManager.ExecuteToDataTable(command);

                if (dt_get.Rows.Count > 0)
                    value = Convert.ToString(dt_get.Rows[0]["count_gi_by_do_no"]);
                else
                    value = "0";
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (this.dbManager != null)
                {
                    this.dbManager.Dispose();
                }
            }

            return value;
        }
        public DataTable getInvoiceDO(string _do_no)
        {
            DataTable result;
            try
            {
                DataTable dataTable = new DataTable();
                string text = "SELECT create_date, do_no, serial_number FROM GI_Delivery WHERE (do_no='" + _do_no + "') ";
                SqlCeCommand sqlCeCommand = new SqlCeCommand();
                sqlCeCommand.CommandType = CommandType.Text;
                sqlCeCommand.CommandText = text.ToString();
                this.dbManager.Open();
                dataTable = this.dbManager.ExecuteToDataTable(sqlCeCommand);
                this.dbManager.Close();
                result = dataTable;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (this.dbManager != null)
                {
                    this.dbManager.Dispose();
                }
            }
            return result;
        }

        #endregion

        #region 2. GR From Production

        public bool checkGRProductionSerialDuplicate(string _serial_number)
        {
            try
            {
                DataTable dt_chk_serial = new DataTable();
                string sql = "SELECT * " +
                             "FROM GR_Production " +
                             "WHERE (serial_number='" + _serial_number + "') ";

                SqlCeCommand command = new SqlCeCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = sql.ToString();
                dbManager.Open();
                dt_chk_serial = dbManager.ExecuteToDataTable(command);

                if (dt_chk_serial.Rows.Count > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool SaveGRProduction(GRProductionEntity _ent)
        {
            bool _result = false;
            try
            {

                StringBuilder sql = new StringBuilder();
                sql.Append("INSERT INTO GR_Production (serial_number, pre_order, batch, create_date) ");
                sql.Append(string.Format("VALUES ('{0}','{1}','{2}','{3}') ", _ent.serial_number, _ent.pre_order, _ent.batch, _ent.create_date));

                SqlCeCommand command = new SqlCeCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = sql.ToString();

                dbManager.Open();
                if (dbManager.ExecuteNonQuery(command) > 0)
                    _result = true;
            }
            catch (SqlCeException)
            {
                _result = false;
                throw;
            }
            finally
            {
                if (dbManager != null)
                {
                    dbManager.Dispose();
                }
            }
            return _result;
        }
        public string CountGRProductionALL()
        {
            string value = string.Empty;
            try
            {
                DataTable dt_get = new DataTable();
                string _count = string.Empty;

                string sql = "SELECT COUNT(*) AS count_gr_row " +
                             "FROM GR_Production ";

                SqlCeCommand command = new SqlCeCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = sql.ToString();
                dbManager.Open();
                dt_get = dbManager.ExecuteToDataTable(command);

                if (dt_get.Rows.Count > 0)
                    value = Convert.ToString(dt_get.Rows[0]["count_gr_row"]);

                return value;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public string CountByPreOrderGRProduction(string _pre_order)
        {
            string value = string.Empty;
            try
            {
                DataTable dt_get = new DataTable();
                string _count = string.Empty;

                string sql = "SELECT COUNT(*) AS count_by_pre_order " +
                             "FROM GR_Production " +
                             "WHERE (pre_order = '" + _pre_order + "') ";

                SqlCeCommand command = new SqlCeCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = sql.ToString();
                dbManager.Open();
                dt_get = dbManager.ExecuteToDataTable(command);

                if (dt_get.Rows.Count > 0)
                    value = Convert.ToString(dt_get.Rows[0]["count_by_pre_order"]);

                return value;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region 3. Distribution Ret

        public bool checkCustCode(string _cust_code, string _cust_id, ref string _error_code, ref string _cust_name)
        {
            bool result;
            try
            {
                bool flag;
                DataTable dataTable = new DataTable();
                DataTable dataTable2 = new DataTable();
                DataTable dtCustomer = new DataTable();
                string sqlCus = "SELECT * FROM customer ";
                SqlCeCommand sqlCeCommandCustomer = new SqlCeCommand();
                sqlCeCommandCustomer.CommandType = CommandType.Text;
                sqlCeCommandCustomer.CommandText = sqlCus.ToString();
                this.dbManager.Open();
                dtCustomer = this.dbManager.ExecuteToDataTable(sqlCeCommandCustomer);
                this.dbManager.Close();
                if (!dtCustomer.IsNullOrNoRows())
                {
                    #region มีข้อมูล ลูกค้าในระบบแล้ว

                    string sql = "SELECT * FROM customer WHERE (SUBSTRING(cust_id, 1, 3)  = '" + _cust_code + "') ";
                    SqlCeCommand sqlCeCommand = new SqlCeCommand();
                    sqlCeCommand.CommandType = CommandType.Text;
                    sqlCeCommand.CommandText = sql.ToString();
                    this.dbManager.Open();
                    dataTable = this.dbManager.ExecuteToDataTable(sqlCeCommand);
                    this.dbManager.Close();

                    if (!dataTable.IsNullOrNoRows())
                    {
                        #region Fotmat customer ถูกต้อง

                        string sql2 = "SELECT * FROM customer WHERE (cust_id  = '" + _cust_id + "') ";
                        SqlCeCommand sqlCeCommand2 = new SqlCeCommand();
                        sqlCeCommand2.CommandType = CommandType.Text;
                        sqlCeCommand2.CommandText = sql2.ToString();
                        this.dbManager.Open();
                        dataTable2 = this.dbManager.ExecuteToDataTable(sqlCeCommand2);
                        if (!dataTable2.IsNullOrNoRows())
                        {
                            flag = true;
                            _cust_name = dataTable2.Rows[0]["cust_name"].ToString().Trim();
                        }
                        else
                        {
                            flag = false;
                            _error_code = "002";
                        }

                        #endregion
                    }
                    else
                    {
                        #region Fotmat customer ไม่ถูกต้อง

                        flag = false;
                        _error_code = "001";

                        #endregion
                    }

                    #endregion
                }
                else
                {
                    #region ไม่มีข้อมูลลูกค้าในระบบ

                    flag = false;
                    _error_code = "003";

                    #endregion
                }
                
                result = flag;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (this.dbManager != null)
                {
                    this.dbManager.Dispose();
                }
            }
            return result;
        }
        public bool checkCustomerCode(string _cust_code, ref string _error_code, ref string _cust_name)
        {
            bool result;
            try
            {
                bool flag;
                DataTable dataTable = new DataTable();
                DataTable dtCustomer = new DataTable();
                string sqlCus = "SELECT * FROM customer ";
                SqlCeCommand sqlCeCommandCustomer = new SqlCeCommand();
                sqlCeCommandCustomer.CommandType = CommandType.Text;
                sqlCeCommandCustomer.CommandText = sqlCus.ToString();
                this.dbManager.Open();
                dtCustomer = this.dbManager.ExecuteToDataTable(sqlCeCommandCustomer);
                this.dbManager.Close();
                if (!dtCustomer.IsNullOrNoRows())
                {
                    #region มีข้อมูล ลูกค้าในระบบแล้ว

                    string sql = "SELECT * FROM customer WHERE cust_id = '" + _cust_code + "'";
                    SqlCeCommand sqlCeCommand = new SqlCeCommand();
                    sqlCeCommand.CommandType = CommandType.Text;
                    sqlCeCommand.CommandText = sql.ToString();
                    this.dbManager.Open();
                    dataTable = this.dbManager.ExecuteToDataTable(sqlCeCommand);
                    this.dbManager.Close();

                    if (!dataTable.IsNullOrNoRows())
                    {
                        #region มีลูกค้าในระบบ

                        flag = true;
                        _cust_name = dataTable.Rows[0]["cust_name"].ToString().Trim();

                        #endregion
                    }
                    else
                    {
                        #region ไม่มีรหัสลูกค้านี้ในระบบ

                        flag = false;
                        _error_code = "002";

                        #endregion
                    }

                    #endregion
                }
                else
                {
                    #region ไม่มีข้อมูลลูกค้าในระบบ

                    flag = false;
                    _error_code = "003";

                    #endregion
                }

                result = flag;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (this.dbManager != null)
                {
                    this.dbManager.Dispose();
                }
            }
            return result;
        }
        public string CountDistributionRetALL()
        {
            string text = string.Empty;
            string result;
            try
            {
                DataTable dataTable = new DataTable();
                string empty = string.Empty;
                string sql = "SELECT COUNT(*) AS count_dis_ret FROM Distribution_Ret ";
                SqlCeCommand sqlCeCommand = new SqlCeCommand();
                sqlCeCommand.CommandType = CommandType.Text;
                sqlCeCommand.CommandText = sql.ToString();
                this.dbManager.Open();
                dataTable = this.dbManager.ExecuteToDataTable(sqlCeCommand);
                this.dbManager.Close();
                if (dataTable.Rows.Count > 0)
                    text = Convert.ToString(dataTable.Rows[0]["count_dis_ret"]);
                else
                    text = "0";

                result = text;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (this.dbManager != null)
                {
                    this.dbManager.Dispose();
                }
            }
            return result;
        }
        public DataTable getDetailCustomer(string _cust_code, string _doc_number, string _vehicle)
        {
            DataTable result;
            try
            {
                DataTable dataTable = new DataTable();
                string sql = "SELECT create_date, empty_or_full, cust_id, doc_no, serial_number, vehicle FROM Distribution_Ret WHERE (cust_id ='" + _cust_code + "') AND (doc_no ='" + _doc_number + "') AND (vehicle ='" + _vehicle + "') ";
                SqlCeCommand sqlCeCommand = new SqlCeCommand();
                sqlCeCommand.CommandType = CommandType.Text;
                sqlCeCommand.CommandText = sql.ToString();
                this.dbManager.Open();
                dataTable = this.dbManager.ExecuteToDataTable(sqlCeCommand);
                this.dbManager.Close();
                result = dataTable;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (this.dbManager != null)
                {
                    this.dbManager.Dispose();
                }
            }
            return result;
        }
        public bool checkDistributionRetSerialDuplicate(string _serial_number)
        {
            bool result;
            try
            {
                DataTable dataTable = new DataTable();
                string sql = "SELECT * FROM Distribution_Ret WHERE (serial_number='" + _serial_number + "') ";
                SqlCeCommand sqlCeCommand = new SqlCeCommand();
                sqlCeCommand.CommandType = CommandType.Text;
                sqlCeCommand.CommandText = sql.ToString();
                this.dbManager.Open();
                dataTable = this.dbManager.ExecuteToDataTable(sqlCeCommand);
                this.dbManager.Close();

                if (dataTable.Rows.Count > 0)
                    result = true;
                else
                    result = false;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (this.dbManager != null)
                {
                    this.dbManager.Dispose();
                }
            }
            return result;
        }
        public bool SaveDistributionRet(DistributionRetEntity _ent)
        {
            bool result = false;
            try
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append("INSERT INTO Distribution_Ret (serial_number, cust_id, doc_no, vehicle, empty_or_full, create_date) ");
                stringBuilder.Append(string.Format("VALUES ('{0}','{1}','{2}','{3}','{4}','{5}') ", _ent.serial_number, _ent.cust_id, _ent.doc_no, _ent.vehicle, _ent.empty_or_full, _ent.create_date));
                SqlCeCommand sqlCeCommand = new SqlCeCommand();
                sqlCeCommand.CommandType = CommandType.Text;
                sqlCeCommand.CommandText = stringBuilder.ToString();
                this.dbManager.Open();

                if (this.dbManager.ExecuteNonQuery(sqlCeCommand) > 0)
                    result = true;

                this.dbManager.Close();
            }
            catch (SqlCeException)
            {
                result = false;
                throw;
            }
            finally
            {
                if (this.dbManager != null)
                {
                    this.dbManager.Dispose();
                }
            }
            return result;
        }
        public string DistributionRetByCustomer(string _cust_id)
        {
            string text = string.Empty;
            string result;
            try
            {
                DataTable dataTable = new DataTable();
                string empty = string.Empty;
                string sql = "SELECT COUNT(*) AS count_dis_ret FROM Distribution_Ret WHERE (cust_id = '" + _cust_id + "') ";
                SqlCeCommand sqlCeCommand = new SqlCeCommand();
                sqlCeCommand.CommandType = CommandType.Text;
                sqlCeCommand.CommandText = sql.ToString();
                this.dbManager.Open();
                dataTable = this.dbManager.ExecuteToDataTable(sqlCeCommand);
                this.dbManager.Close();
                if (dataTable.Rows.Count > 0)
                {
                    text = Convert.ToString(dataTable.Rows[0]["count_dis_ret"]);
                }

                result = text;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (this.dbManager != null)
                {
                    this.dbManager.Dispose();
                }
            }
            return result;
        }
        public DataTable getDistributionRetForDel()
        {
            DataTable result;
            try
            {
                DataTable dataTable = new DataTable();
                string sql = "SELECT * FROM Distribution_Ret ORDER BY create_date DESC";
                SqlCeCommand sqlCeCommand = new SqlCeCommand();
                sqlCeCommand.CommandType = CommandType.Text;
                sqlCeCommand.CommandText = sql.ToString();
                this.dbManager.Open();
                dataTable = this.dbManager.ExecuteToDataTable(sqlCeCommand);
                this.dbManager.Close();
                result = dataTable;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (this.dbManager != null)
                {
                    this.dbManager.Dispose();
                }
            }
            return result;
        }
        public bool DeleteGIDelivery(string _do_no, string _serial_number)
        {
            bool result;
            try
            {
                string sql = string.Concat(new string[]
		{
			"DELETE FROM GI_Delivery WHERE (do_no = '",
			_do_no,
			"') AND (serial_number = '",
			_serial_number,
			"')"
		});
                SqlCeCommand sqlCeCommand = new SqlCeCommand();
                sqlCeCommand.CommandType = CommandType.Text;
                sqlCeCommand.CommandText = sql.ToString();
                this.dbManager.Open();
                this.dbManager.ExecuteNonQuery(sqlCeCommand);
                this.dbManager.Close();
                result = true;
            }
            catch (Exception)
            {
                result = false;
            }
            finally
            {
                if (this.dbManager != null)
                {
                    this.dbManager.Dispose();
                }
            }
            return result;
        }
        public bool DeleteDistributionRet(string _cust_id, string _doc_no, string _vehicle, string _empty_or_full, string _serial_number)
        {
            bool result;
            try
            {
                string sql = string.Concat(new string[]
		{
			"DELETE FROM Distribution_Ret WHERE (cust_id = '",
			_cust_id,
			"') AND (doc_no = '",
			_doc_no,
			"') AND (vehicle = '",
			_vehicle,
			"') AND (empty_or_full = '",
			_empty_or_full,
			"') AND (serial_number = '",
			_serial_number,
			"')"
		});
                SqlCeCommand sqlCeCommand = new SqlCeCommand();
                sqlCeCommand.CommandType = CommandType.Text;
                sqlCeCommand.CommandText = sql.ToString();
                this.dbManager.Open();
                this.dbManager.ExecuteNonQuery(sqlCeCommand);
                this.dbManager.Close();
                result = true;
            }
            catch (Exception)
            {
                result = false;
            }
            finally
            {
                if (this.dbManager != null)
                {
                    this.dbManager.Dispose();
                }
            }
            return result;
        }
        public bool DeleteDistributionRet()
        {
            bool result;
            try
            {
                string text = "DELETE FROM Distribution_Ret ";
                SqlCeCommand sqlCeCommand = new SqlCeCommand();
                sqlCeCommand.CommandType = CommandType.Text;
                sqlCeCommand.CommandText = text.ToString();
                this.dbManager.Open();
                this.dbManager.ExecuteNonQuery(sqlCeCommand);
                this.dbManager.Close();
                result = true;
            }
            catch (Exception)
            {
                result = false;
            }
            finally
            {
                if (this.dbManager != null)
                {
                    this.dbManager.Dispose();
                }
            }
            return result;
        }
        public bool DeleteDistributionSale()
        {
            bool result;
            try
            {
                string text = "DELETE FROM GI_Delivery ";
                SqlCeCommand sqlCeCommand = new SqlCeCommand();
                sqlCeCommand.CommandType = CommandType.Text;
                sqlCeCommand.CommandText = text.ToString();
                this.dbManager.Open();
                this.dbManager.ExecuteNonQuery(sqlCeCommand);
                this.dbManager.Close();
                result = true;
            }
            catch (Exception)
            {
                result = false;
            }
            finally
            {
                if (this.dbManager != null)
                {
                    this.dbManager.Dispose();
                }
            }
            return result;
        }
        public string CountRowDeleteDistributionRetByCustCode(string _cust_code)
        {
            string text = string.Empty;
            string result;
            try
            {
                DataTable dataTable = new DataTable();
                string empty = string.Empty;
                string text2 = "SELECT COUNT(*) AS count_dis_by_cust_code FROM Distribution_Ret WHERE (cust_id = '" + _cust_code + "') ";
                SqlCeCommand sqlCeCommand = new SqlCeCommand();
                sqlCeCommand.CommandType = CommandType.Text;
                sqlCeCommand.CommandText = text2.ToString();
                this.dbManager.Open();
                dataTable = this.dbManager.ExecuteToDataTable(sqlCeCommand);
                this.dbManager.Close();
                if (dataTable.Rows.Count > 0)
                    text = Convert.ToString(dataTable.Rows[0]["count_dis_by_cust_code"]);
                else
                    text = "0";
                result = text;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (this.dbManager != null)
                {
                    this.dbManager.Dispose();
                }
            }
            return result;
        }
        public bool DeleteDistributionRetByCustCode(string _cust_code)
        {
            bool result;
            try
            {
                string text = "DELETE FROM Distribution_Ret WHERE (cust_id = '" + _cust_code + "') ";
                SqlCeCommand sqlCeCommand = new SqlCeCommand();
                sqlCeCommand.CommandType = CommandType.Text;
                sqlCeCommand.CommandText = text.ToString();
                this.dbManager.Open();
                this.dbManager.ExecuteNonQuery(sqlCeCommand);
                this.dbManager.Close();
                result = true;
            }
            catch (Exception)
            {
                result = false;
            }
            finally
            {
                if (this.dbManager != null)
                {
                    this.dbManager.Dispose();
                }
            }
            return result;
        }
        public string CountRowDeleteGIByDO(string _do_no)
        {
            string text = string.Empty;
            string result;
            try
            {
                DataTable dataTable = new DataTable();
                string empty = string.Empty;
                string sql = "SELECT COUNT(*) AS count_gi_by_do_no FROM GI_Delivery WHERE (do_no='" + _do_no + "') ";
                SqlCeCommand sqlCeCommand = new SqlCeCommand();
                sqlCeCommand.CommandType = CommandType.Text;
                sqlCeCommand.CommandText = sql.ToString();
                this.dbManager.Open();
                dataTable = this.dbManager.ExecuteToDataTable(sqlCeCommand);
                this.dbManager.Close();

                if (dataTable.Rows.Count > 0)
                    text = Convert.ToString(dataTable.Rows[0]["count_gi_by_do_no"]);
                else
                    text = "0";

                result = text;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (this.dbManager != null)
                {
                    this.dbManager.Dispose();
                }
            }
            return result;
        }
        public bool DeleteGIDeliveryByDO(string _do_no)
        {
            bool result;
            try
            {
                string text = "DELETE FROM GI_Delivery WHERE (do_no = '" + _do_no + "') ";
                SqlCeCommand sqlCeCommand = new SqlCeCommand();
                sqlCeCommand.CommandType = CommandType.Text;
                sqlCeCommand.CommandText = text.ToString();
                this.dbManager.Open();
                this.dbManager.ExecuteNonQuery(sqlCeCommand);
                this.dbManager.Close();
                result = true;
            }
            catch (Exception)
            {
                result = false;
            }
            finally
            {
                if (this.dbManager != null)
                {
                    this.dbManager.Dispose();
                }
            }
            return result;
        }

        #endregion

        #region 4. GI Ref. IO Empty

        public bool checkGIRefIOEmptySerialDuplicate(string _serial_number)
        {
            try
            {
                DataTable dt_chk_serial = new DataTable();
                string sql = "SELECT * " +
                             "FROM GI_Ref_IO_Empty " +
                             "WHERE (serial_number='" + _serial_number + "') ";

                SqlCeCommand command = new SqlCeCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = sql.ToString();
                dbManager.Open();
                dt_chk_serial = dbManager.ExecuteToDataTable(command);

                if (dt_chk_serial.Rows.Count > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public string CountGIRefIOALL()
        {
            string value = string.Empty;
            try
            {
                DataTable dt_get = new DataTable();
                string _count = string.Empty;

                string sql = "SELECT COUNT(*) AS count_gi_io_empty " +
                             "FROM GI_Ref_IO_Empty ";

                SqlCeCommand command = new SqlCeCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = sql.ToString();
                dbManager.Open();
                dt_get = dbManager.ExecuteToDataTable(command);

                if (dt_get.Rows.Count > 0)
                    value = Convert.ToString(dt_get.Rows[0]["count_gi_io_empty"]);

                return value;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public string CountGIEmptyByDO(string _do_no)
        {
            string text = string.Empty;
            string result;
            try
            {
                DataTable dataTable = new DataTable();
                string empty = string.Empty;
                string text2 = "SELECT COUNT(*) AS count_gi_by_do_no FROM GI_Ref_IO_Empty WHERE (do_no = '" + _do_no + "') ";
                SqlCeCommand sqlCeCommand = new SqlCeCommand();
                sqlCeCommand.CommandType = CommandType.Text;
                sqlCeCommand.CommandText = text2.ToString();
                this.dbManager.Open();
                dataTable = this.dbManager.ExecuteToDataTable(sqlCeCommand);
                this.dbManager.Close();
                if (dataTable.Rows.Count > 0)
                {
                    text = Convert.ToString(dataTable.Rows[0]["count_gi_by_do_no"]);
                }
                result = text;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (this.dbManager != null)
                {
                    this.dbManager.Dispose();
                }
            }
            return result;
        }
        public bool SaveGIRefIOEmpty(GIRefIOEmptyEntity _ent)
        {
            bool _result = false;
            try
            {

                StringBuilder sql = new StringBuilder();
                sql.Append("INSERT INTO GI_Ref_IO_Empty (serial_number, do_no, create_date) ");
                sql.Append(string.Format("VALUES ('{0}','{1}','{2}') ", _ent.serial_number, _ent.do_no, _ent.create_date));

                SqlCeCommand command = new SqlCeCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = sql.ToString();

                dbManager.Open();
                if (dbManager.ExecuteNonQuery(command) > 0)
                    _result = true;
            }
            catch (SqlCeException)
            {
                _result = false;
                throw;
            }
            finally
            {
                if (dbManager != null)
                {
                    dbManager.Dispose();
                }
            }
            return _result;
        }

        #endregion

        #region 5. Delete All Data

        public bool DeleteAllInSqlCeOnPDA()
        {
            StringBuilder stringBuilder = new StringBuilder();
            StringBuilder stringBuilder2 = new StringBuilder();
            StringBuilder stringBuilder3 = new StringBuilder();
            StringBuilder stringBuilder4 = new StringBuilder();
            SqlCeCommand sqlCeCommand = new SqlCeCommand();
            SqlCeCommand sqlCeCommand2 = new SqlCeCommand();
            SqlCeCommand sqlCeCommand3 = new SqlCeCommand();
            SqlCeCommand sqlCeCommand4 = new SqlCeCommand();
            bool result;
            try
            {
                this.dbManager.Open();
                this.dbManager.BeginTransaction();
                stringBuilder = new StringBuilder();
                stringBuilder.Append(string.Format("DELETE FROM Distribution_Ret ", new object[0]));
                sqlCeCommand = new SqlCeCommand();
                sqlCeCommand.CommandType = CommandType.Text;
                sqlCeCommand.CommandText = stringBuilder.ToString();
                bool flag;
                if (this.dbManager.ExecuteNonQuery(sqlCeCommand) >= 0)
                {
                    stringBuilder2.Append(string.Format("DELETE FROM GI_Delivery ", new object[0]));
                    sqlCeCommand2 = new SqlCeCommand();
                    sqlCeCommand2.CommandType = CommandType.Text;
                    sqlCeCommand2.CommandText = stringBuilder2.ToString();
                    if (this.dbManager.ExecuteNonQuery(sqlCeCommand2) >= 0)
                    {
                        stringBuilder3.Append(string.Format("DELETE FROM GI_Ref_IO_Empty ", new object[0]));
                        sqlCeCommand3 = new SqlCeCommand();
                        sqlCeCommand3.CommandType = CommandType.Text;
                        sqlCeCommand3.CommandText = stringBuilder3.ToString();
                        if (this.dbManager.ExecuteNonQuery(sqlCeCommand3) >= 0)
                        {
                            stringBuilder4.Append(string.Format("DELETE FROM GR_Production ", new object[0]));
                            sqlCeCommand4 = new SqlCeCommand();
                            sqlCeCommand4.CommandType = CommandType.Text;
                            sqlCeCommand4.CommandText = stringBuilder4.ToString();
                            if (this.dbManager.ExecuteNonQuery(sqlCeCommand4) >= 0)
                            {
                                this.dbManager.CommitTransaction();
                                flag = true;
                                this.dbManager.Close();
                            }
                            else
                            {
                                flag = false;
                            }
                        }
                        else
                        {
                            flag = false;
                        }
                    }
                    else
                    {
                        flag = false;
                    }
                }
                else
                {
                    flag = false;
                }
                result = flag;
            }
            catch (Exception)
            {
                this.dbManager.RollBackTransaction();
                throw;
            }
            finally
            {
                if (this.dbManager != null)
                {
                    this.dbManager.Dispose();
                }
            }
            return result;
        }
        public bool DoDeleteAllInSqlCeOnPDA()
        {
            StringBuilder stringBuilder = new StringBuilder();
            StringBuilder stringBuilder2 = new StringBuilder();
            StringBuilder stringBuilder3 = new StringBuilder();
            SqlCeCommand sqlCeCommand = new SqlCeCommand();
            SqlCeCommand sqlCeCommand2 = new SqlCeCommand();
            SqlCeCommand sqlCeCommand3 = new SqlCeCommand();
            bool result;
            try
            {
                this.dbManager.Open();
                this.dbManager.BeginTransaction();
                stringBuilder = new StringBuilder();
                stringBuilder.Append(string.Format("DELETE FROM warehouse ", new object[0]));
                sqlCeCommand = new SqlCeCommand();
                sqlCeCommand.CommandType = CommandType.Text;
                sqlCeCommand.CommandText = stringBuilder.ToString();
                bool flag;
                if (this.dbManager.ExecuteNonQuery(sqlCeCommand) >= 0)
                {
                    stringBuilder2.Append(string.Format("DELETE FROM delivery ", new object[0]));
                    sqlCeCommand2 = new SqlCeCommand();
                    sqlCeCommand2.CommandType = CommandType.Text;
                    sqlCeCommand2.CommandText = stringBuilder2.ToString();
                    if (this.dbManager.ExecuteNonQuery(sqlCeCommand2) >= 0)
                    {
                        stringBuilder3.Append(string.Format("DELETE FROM otherscans ", new object[0]));
                        sqlCeCommand3 = new SqlCeCommand();
                        sqlCeCommand3.CommandType = CommandType.Text;
                        sqlCeCommand3.CommandText = stringBuilder3.ToString();
                        if (this.dbManager.ExecuteNonQuery(sqlCeCommand3) >= 0)
                        {
                            this.dbManager.CommitTransaction();
                            flag = true;
                            this.dbManager.Close();
                        }
                        else
                        {
                            flag = false;
                        }
                    }
                    else
                    {
                        flag = false;
                    }
                }
                else
                {
                    flag = false;
                }
                result = flag;
            }
            catch (Exception)
            {
                this.dbManager.RollBackTransaction();
                throw;
            }
            finally
            {
                if (this.dbManager != null)
                {
                    this.dbManager.Dispose();
                }
            }
            return result;
        }
        public bool DeleteGIRefIOEmpty()
        {
            bool result;
            try
            {
                string text = "DELETE FROM GI_Ref_IO_Empty ";
                SqlCeCommand sqlCeCommand = new SqlCeCommand();
                sqlCeCommand.CommandType = CommandType.Text;
                sqlCeCommand.CommandText = text.ToString();
                this.dbManager.Open();
                this.dbManager.ExecuteNonQuery(sqlCeCommand);
                this.dbManager.Close();
                result = true;
            }
            catch (Exception)
            {
                result = false;
            }
            finally
            {
                if (this.dbManager != null)
                {
                    this.dbManager.Dispose();
                }
            }
            return result;
        }
        public bool DeleteGRFromProduction()
        {
            bool result;
            try
            {
                string text = "DELETE FROM GR_Production ";
                SqlCeCommand sqlCeCommand = new SqlCeCommand();
                sqlCeCommand.CommandType = CommandType.Text;
                sqlCeCommand.CommandText = text.ToString();
                this.dbManager.Open();
                this.dbManager.ExecuteNonQuery(sqlCeCommand);
                this.dbManager.Close();
                result = true;
            }
            catch (Exception)
            {
                result = false;
            }
            finally
            {
                if (this.dbManager != null)
                {
                    this.dbManager.Dispose();
                }
            }
            return result;
        }

        #endregion

        #region 6. Download Customer

        public bool getImportCustomer()
        {
            bool result;
            try
            {
                DataTable dataTable = new DataTable();
                string sql = "SELECT * FROM customer ";
                SqlCeCommand sqlCeCommand = new SqlCeCommand();
                sqlCeCommand.CommandType = CommandType.Text;
                sqlCeCommand.CommandText = sql.ToString();
                this.dbManager.Open();
                dataTable = this.dbManager.ExecuteToDataTable(sqlCeCommand);
                this.dbManager.Close();

                if (dataTable.Rows.Count > 0)
                    result = true;
                else
                    result = false;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (this.dbManager != null)
                {
                    this.dbManager.Dispose();
                }
            }
            return result;
        }
        public bool DeleteCustomerInSqlCeOnPDA()
        {
            bool result;
            try
            {
                string sql = "DELETE FROM customer ";
                SqlCeCommand sqlCeCommand = new SqlCeCommand();
                sqlCeCommand.CommandType = CommandType.Text;
                sqlCeCommand.CommandText = sql.ToString();
                this.dbManager.Open();
                bool flag;
                if (this.dbManager.ExecuteNonQuery(sqlCeCommand) > 0)
                {
                    flag = true;
                    this.dbManager.Close();
                }
                else
                {
                    flag = false;
                }
                result = flag;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (this.dbManager != null)
                {
                    this.dbManager.Dispose();
                }
            }
            return result;
        }
        public bool SaveImportCustomer(List<CustomerEntity> _list)
        {
            StringBuilder sql = new StringBuilder();
            SqlCeCommand command = new SqlCeCommand();
            bool _result = false;
            try
            {
                foreach (var _ent in _list)
                {
                    string _cust_name = _ent.cust_name.Replace("'", "''");
                    sql = new StringBuilder();
                    sql.Append("INSERT INTO customer (cust_id, cust_name) ");
                    sql.Append(string.Format("VALUES ('{0}','{1}') ", _ent.cust_id, _cust_name));

                    command = new SqlCeCommand();
                    command.CommandType = CommandType.Text;
                    command.CommandText = sql.ToString();

                    dbManager.Open();
                    if (dbManager.ExecuteNonQuery(command) > 0)
                        _result = true;
                    else
                        _result = false;
                }

                this.dbManager.Close();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (this.dbManager != null)
                {
                    this.dbManager.Dispose();
                }
            }
            return _result;
        }

        #endregion

        #region 7. Download SAP

        public bool getImportSAP()
        {
            bool result;
            try
            {
                DataTable dataTable = new DataTable();
                string sql = "SELECT * FROM SAP_Import ";
                SqlCeCommand sqlCeCommand = new SqlCeCommand();
                sqlCeCommand.CommandType = CommandType.Text;
                sqlCeCommand.CommandText = sql.ToString();
                this.dbManager.Open();
                dataTable = this.dbManager.ExecuteToDataTable(sqlCeCommand);
                this.dbManager.Close();

                if (dataTable.Rows.Count > 0)
                    result = true;
                else
                    result = false;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (this.dbManager != null)
                {
                    this.dbManager.Dispose();
                }
            }
            return result;
        }
        public bool DeleteSAPInSqlCeOnPDA()
        {
            bool result;
            try
            {
                string sql = "DELETE FROM SAP_Import ";
                SqlCeCommand sqlCeCommand = new SqlCeCommand();
                sqlCeCommand.CommandType = CommandType.Text;
                sqlCeCommand.CommandText = sql.ToString();
                this.dbManager.Open();
                bool flag;
                if (this.dbManager.ExecuteNonQuery(sqlCeCommand) > 0)
                {
                    flag = true;
                    this.dbManager.Close();
                }
                else
                {
                    flag = false;
                }
                result = flag;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (this.dbManager != null)
                {
                    this.dbManager.Dispose();
                }
            }
            return result;
        }
        public bool SaveImportSAP(List<SAPEntity> _list)
        {
            StringBuilder sql = new StringBuilder();
            SqlCeCommand command = new SqlCeCommand();
            bool _result = false;
            try
            {
                foreach (var _ent in _list)
                {
                    string _cust_name = _ent.Customer_Name.Replace("'", "''");
                    string _Material = _ent.Material.Replace("'", "''");
                    string _Serial_Number = _ent.Serial_Number.Replace("'", "''");
                    string _Batch = _ent.Batch.Replace("'", "''");
                    string _Material_Description = _ent.Material_Description.Replace("'", "''");
                    string _Plant_Name = _ent.Plant_Name.Replace("'", "''");
                    string _Customer_Purchase_Order_Number = _ent.Customer_Purchase_Order_Number.Replace("'", "''");

                    sql = new StringBuilder();
                    sql.Append("INSERT INTO SAP_Import (Posting_Date, Transaction_Type, Delivery_No, Invoice_No, Customer, Customer_Name, Material, Material_Description, Quantity, Serial_Number, Batch, Plant, Plant_Name, Car_ID, Customer_Purchase_Order_Number, Create_Date) ");
                    sql.Append(string.Format("VALUES ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}', getdate()) ", _ent.Posting_Date, _ent.Transaction_Type, _ent.Delivery_No, _ent.Invoice_No, _ent.Customer, _cust_name, _Material, _Material_Description, _ent.Quantity, _Serial_Number, _Batch, _ent.Plant, _Plant_Name, _ent.Car_ID, _Customer_Purchase_Order_Number));

                    command = new SqlCeCommand();
                    command.CommandType = CommandType.Text;
                    command.CommandText = sql.ToString();

                    dbManager.Open();
                    if (dbManager.ExecuteNonQuery(command) > 0)
                        _result = true;
                    else
                        _result = false;
                }

                this.dbManager.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (this.dbManager != null)
                {
                    this.dbManager.Dispose();
                }
            }
            return _result;
        }

        #endregion

        #region 7. Upload Data

        public DataTable getDataDistributionRet()
        {
            try
            {
                DataTable dt_get = new DataTable();

                string sql = "SELECT * " +
                             "FROM Distribution_Ret ";

                SqlCeCommand command = new SqlCeCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = sql.ToString();
                dbManager.Open();
                dt_get = dbManager.ExecuteToDataTable(command);

                return dt_get;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataTable getDataGIRefIOEmpty()
        {
            try
            {
                DataTable dt_get = new DataTable();

                string sql = "SELECT * " +
                             "FROM GI_Ref_IO_Empty ";

                SqlCeCommand command = new SqlCeCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = sql.ToString();
                dbManager.Open();
                dt_get = dbManager.ExecuteToDataTable(command);

                return dt_get;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataTable getDataGIDelivery()
        {
            try
            {
                DataTable dt_get = new DataTable();

                string sql = "SELECT * " +
                             "FROM GI_Delivery ";

                SqlCeCommand command = new SqlCeCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = sql.ToString();
                dbManager.Open();
                dt_get = dbManager.ExecuteToDataTable(command);

                return dt_get;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataTable getDataGRProduction()
        {
            try
            {
                DataTable dt_get = new DataTable();

                string sql = "SELECT * " +
                             "FROM GR_Production ";

                SqlCeCommand command = new SqlCeCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = sql.ToString();
                dbManager.Open();
                dt_get = dbManager.ExecuteToDataTable(command);

                return dt_get;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool DeleteDistributionRetAfterUpload()
        {
            bool _result = false;
            try
            {
                string text = "DELETE FROM Distribution_Ret ";
                SqlCeCommand sqlCeCommand = new SqlCeCommand();
                sqlCeCommand.CommandType = CommandType.Text;
                sqlCeCommand.CommandText = text.ToString();
                this.dbManager.Open();
                this.dbManager.ExecuteNonQuery(sqlCeCommand);
                this.dbManager.Close();
                _result = true;
            }
            catch (SqlCeException)
            {
                _result = false;
                throw;
            }
            finally
            {
                if (dbManager != null)
                {
                    dbManager.Dispose();
                }
            }
            return _result;
        }
        public bool DeleteGIRefIOEmptyAfterUpload()
        {
            bool _result = false;
            try
            {
                string text = "DELETE FROM GI_Ref_IO_Empty ";
                SqlCeCommand sqlCeCommand = new SqlCeCommand();
                sqlCeCommand.CommandType = CommandType.Text;
                sqlCeCommand.CommandText = text.ToString();
                this.dbManager.Open();
                this.dbManager.ExecuteNonQuery(sqlCeCommand);
                this.dbManager.Close();
                _result = true;
            }
            catch (SqlCeException)
            {
                _result = false;
                throw;
            }
            finally
            {
                if (dbManager != null)
                {
                    dbManager.Dispose();
                }
            }
            return _result;
        }
        public bool DeleteGIDeliveryAfterUpload()
        {
            bool _result = false;
            try
            {
                string text = "DELETE FROM GI_Delivery ";
                SqlCeCommand sqlCeCommand = new SqlCeCommand();
                sqlCeCommand.CommandType = CommandType.Text;
                sqlCeCommand.CommandText = text.ToString();
                this.dbManager.Open();
                this.dbManager.ExecuteNonQuery(sqlCeCommand);
                this.dbManager.Close();
                _result = true;
            }
            catch (SqlCeException)
            {
                _result = false;
                throw;
            }
            finally
            {
                if (dbManager != null)
                {
                    dbManager.Dispose();
                }
            }
            return _result;
        }
        public bool DeleteGRProductionAfterUpload()
        {
            bool _result = false;
            try
            {
                string text = "DELETE FROM GR_Production ";
                SqlCeCommand sqlCeCommand = new SqlCeCommand();
                sqlCeCommand.CommandType = CommandType.Text;
                sqlCeCommand.CommandText = text.ToString();
                this.dbManager.Open();
                this.dbManager.ExecuteNonQuery(sqlCeCommand);
                this.dbManager.Close();
                _result = true;
            }
            catch (SqlCeException)
            {
                _result = false;
                throw;
            }
            finally
            {
                if (dbManager != null)
                {
                    dbManager.Dispose();
                }
            }
            return _result;
        }

        #endregion

        #region 8. Insert Log

        public bool Insert_Log(string error_code, string error_message, string page_form, string method_name)
        {
            bool _result = false;
            try
            {

                StringBuilder sql = new StringBuilder();
                sql.Append("INSERT INTO Action_Log (error_code, error_message, page_form, method_name, create_date)  ");
                sql.Append(string.Format("VALUES ('{0}','{1}','{2}', '{3}', getdate()) ", error_code, error_message.Replace("'", ""), page_form, method_name));

                SqlCeCommand command = new SqlCeCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = sql.ToString();

                dbManager.Open();
                dbManager.ExecuteNonQuery(command);
                _result = true;
            }
            catch (SqlCeException)
            {
                _result = false;
            }
            finally
            {
                if (dbManager != null)
                {
                    dbManager.Dispose();
                }
            }
            return _result;
        }

        #endregion

        #region 9. Activate Licence

        public string CheckExpireSoftware()
        {
            string connti = "FALSE";
            try
            {
                DateTimeFormatInfo usDtfi = new CultureInfo("en-US", false).DateTimeFormat;

                DataTable dt_licence = new DataTable();

                string sql = "SELECT TOP(1) expire_date " +
                             "FROM t_setup ";

                SqlCeCommand command = new SqlCeCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = sql.ToString();
                dbManager.Open();
                dt_licence = dbManager.ExecuteToDataTable(command);

                if (!dt_licence.IsNullOrNoRows())
                {
                    #region Has data

                    string _expire_date = dt_licence.Rows[0][0].ToString().Trim();
                    if (string.IsNullOrEmpty(_expire_date))
                    {
                        connti = "FALSE";
                    }
                    else if (!string.IsNullOrEmpty(_expire_date))
                    {
                        string _decry_expire_date = Convert.ToString(Convert.ToInt64(_expire_date) - 39335472);
                        if (_decry_expire_date.Trim() == "30000101")
                        {
                            connti = "LIFETIME";
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(_decry_expire_date))
                            {
                                string _crrent_date = Convert.ToDateTime(GetDateServer()).ToString("yyyyMMdd", usDtfi);

                                if (Convert.ToInt64(_decry_expire_date) < Convert.ToInt64(_crrent_date))
                                    connti = "EXPIRE"; //connti = "FALSE";
                                else if (Convert.ToInt64(_decry_expire_date) > Convert.ToInt64(_crrent_date))
                                    connti = "TRUE";
                                else
                                    connti = "FALSE";
                            }
                            else
                            {
                                connti = "FALSE";
                            }
                        }
                    }

                    #endregion
                }
                else
                {
                    #region Not has data

                    connti = "FALSE";

                    #endregion
                }
            }
            catch (Exception)
            {
                connti = "FALSE";
            }

            return connti;
        }
        public bool ActivateLicence(string _licence_key)
        {
            bool result = false;
            try
            {
                string text = "UPDATE t_setup SET expire_date = '" + _licence_key.Trim() + "', update_date = GETDATE() ";
                SqlCeCommand sqlCeCommand = new SqlCeCommand();
                sqlCeCommand.CommandType = CommandType.Text;
                sqlCeCommand.CommandText = text.ToString();
                this.dbManager.Open();
                this.dbManager.ExecuteNonQuery(sqlCeCommand);
                this.dbManager.Close();
                result = true;
            }
            catch (Exception)
            {
                result = false;
            }

            return result;
        }
        public string ValidationLicence(string _licence_keys)
        {
            string Result = string.Empty;
            try
            {
                DateTimeFormatInfo usDtfi = new CultureInfo("en-US", false).DateTimeFormat;
                string _licence_key = Convert.ToString((Convert.ToInt64(_licence_keys) - 39335472));
                string _crrent_date = Convert.ToDateTime(GetDateServer()).ToString("yyyyMMdd", usDtfi);
                if (!string.IsNullOrEmpty(_licence_key))
                {
                    if (_licence_key.Trim() == "30000101")
                    {
                        if (ActivateLicence(_licence_keys))
                        {
                            Result = "TRUE";
                        }
                    }
                    else if (Convert.ToInt32(_licence_key) < Convert.ToInt32(_crrent_date))
                    {
                        Result = "TRUE_EXPIRED";
                    }
                    else if (_licence_key.IndexOf("2016") >= 0)
                    {
                        if (ActivateLicence(_licence_keys))
                        {
                            DateTime dTime = DateTime.ParseExact(_licence_key, "yyyyMMdd", null);
                            string display_date = dTime.ToLongDateString();
                            Result = "TRUE_EXPRIE|" + display_date;
                        }
                    }
                    else
                    {
                        Result = "FALSE";
                    }
                }
            }
            catch (Exception)
            {
                Result = "FALSE";
            }

            return Result;
        }

        #endregion

        #region 10. Delete by function

        public bool DeleteWarehouse()
        {
            bool result;
            try
            {
                string text = "DELETE FROM warehouse ";
                SqlCeCommand sqlCeCommand = new SqlCeCommand();
                sqlCeCommand.CommandType = CommandType.Text;
                sqlCeCommand.CommandText = text.ToString();
                this.dbManager.Open();
                this.dbManager.ExecuteNonQuery(sqlCeCommand);
                this.dbManager.Close();
                result = true;
            }
            catch (Exception)
            {
                result = false;
            }
            finally
            {
                if (this.dbManager != null)
                {
                    this.dbManager.Dispose();
                }
            }
            return result;
        }
        public bool DeleteDelivery()
        {
            bool result;
            try
            {
                string text = "DELETE FROM delivery ";
                SqlCeCommand sqlCeCommand = new SqlCeCommand();
                sqlCeCommand.CommandType = CommandType.Text;
                sqlCeCommand.CommandText = text.ToString();
                this.dbManager.Open();
                this.dbManager.ExecuteNonQuery(sqlCeCommand);
                this.dbManager.Close();
                result = true;
            }
            catch (Exception)
            {
                result = false;
            }
            finally
            {
                if (this.dbManager != null)
                {
                    this.dbManager.Dispose();
                }
            }
            return result;
        }
        public bool DeleteOtherScans()
        {
            bool result;
            try
            {
                string text = "DELETE FROM otherscans ";
                SqlCeCommand sqlCeCommand = new SqlCeCommand();
                sqlCeCommand.CommandType = CommandType.Text;
                sqlCeCommand.CommandText = text.ToString();
                this.dbManager.Open();
                this.dbManager.ExecuteNonQuery(sqlCeCommand);
                this.dbManager.Close();
                result = true;
            }
            catch (Exception)
            {
                result = false;
            }
            finally
            {
                if (this.dbManager != null)
                {
                    this.dbManager.Dispose();
                }
            }
            return result;
        }

        #endregion

        #region 11. Warehouse

        public string CountWarehouseALL()
        {
            string text = string.Empty;
            string result;
            try
            {
                DataTable dataTable = new DataTable();
                string empty = string.Empty;
                string sql = "SELECT COUNT(*) AS count_warehouse FROM warehouse ";
                SqlCeCommand sqlCeCommand = new SqlCeCommand();
                sqlCeCommand.CommandType = CommandType.Text;
                sqlCeCommand.CommandText = sql.ToString();
                this.dbManager.Open();
                dataTable = this.dbManager.ExecuteToDataTable(sqlCeCommand);
                this.dbManager.Close();
                if (dataTable.Rows.Count > 0)
                    text = Convert.ToString(dataTable.Rows[0]["count_warehouse"]);
                else
                    text = "0";

                result = text;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (this.dbManager != null)
                {
                    this.dbManager.Dispose();
                }
            }
            return result;
        }
        public DataTable getSaleOrderNo(string _sale_orderno)
        {
            DataTable result;
            try
            {
                DataTable dataTable = new DataTable();
                string text = "SELECT create_date, sell_order_no, cylinder_sn FROM warehouse WHERE (sell_order_no='" + _sale_orderno + "') ";
                SqlCeCommand sqlCeCommand = new SqlCeCommand();
                sqlCeCommand.CommandType = CommandType.Text;
                sqlCeCommand.CommandText = text.ToString();
                this.dbManager.Open();
                dataTable = this.dbManager.ExecuteToDataTable(sqlCeCommand);
                this.dbManager.Close();
                result = dataTable;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (this.dbManager != null)
                {
                    this.dbManager.Dispose();
                }
            }
            return result;
        }
        public DataTable getWarehouseForDel()
        {
            DataTable result;
            try
            {
                DataTable dataTable = new DataTable();
                string sql = "select wh.*, cu.cust_name from warehouse wh inner join customer cu on wh.cust_id = cu.cust_id ORDER BY wh.create_date DESC ";
                SqlCeCommand sqlCeCommand = new SqlCeCommand();
                sqlCeCommand.CommandType = CommandType.Text;
                sqlCeCommand.CommandText = sql.ToString();
                this.dbManager.Open();
                dataTable = this.dbManager.ExecuteToDataTable(sqlCeCommand);
                this.dbManager.Close();
                result = dataTable;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (this.dbManager != null)
                {
                    this.dbManager.Dispose();
                }
            }
            return result;
        }
        public bool checkCylinderDuplicate(string _serial_number)
        {
            try
            {
                DataTable dt_chk_serial = new DataTable();
                string sql = "SELECT * " +
                             "FROM warehouse " +
                             "WHERE (cylinder_sn='" + _serial_number + "') ";

                SqlCeCommand command = new SqlCeCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = sql.ToString();
                dbManager.Open();
                dt_chk_serial = dbManager.ExecuteToDataTable(command);

                if (dt_chk_serial.Rows.Count > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool DoSaveWarehouse(WarehouseEntity _ent)
        {
            bool _result = false;
            try
            {

                StringBuilder sql = new StringBuilder();
                sql.Append("INSERT INTO warehouse (sell_order_no, cust_id, item_code, cylinder_sn, create_by, create_date) ");
                sql.Append(string.Format("VALUES ('{0}','{1}','{2}','{3}','{4}','{5}') ", _ent.sell_order_no, _ent.cust_id, _ent.item_code, _ent.cylinder_sn, _ent.create_by, _ent.create_date));

                SqlCeCommand command = new SqlCeCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = sql.ToString();

                dbManager.Open();
                if (dbManager.ExecuteNonQuery(command) > 0)
                    _result = true;
            }
            catch (SqlCeException)
            {
                _result = false;
                throw;
            }
            finally
            {
                if (dbManager != null)
                {
                    dbManager.Dispose();
                }
            }
            return _result;
        }
        public bool DoDeleteDelivery(string _sell_order_no, string _cust_id, string _item_code, string _serial_number)
        {
            bool result;
            try
            {
                string sql = string.Concat(new string[]
		{
			"DELETE FROM warehouse WHERE (sell_order_no = '",
			_sell_order_no,
			"') AND (cust_id = '",
			_cust_id,
			"') AND (item_code = '",
			_item_code,
			"') AND (cylinder_sn = '",
			_serial_number,
			"')"
		});
                SqlCeCommand sqlCeCommand = new SqlCeCommand();
                sqlCeCommand.CommandType = CommandType.Text;
                sqlCeCommand.CommandText = sql.ToString();
                this.dbManager.Open();
                this.dbManager.ExecuteNonQuery(sqlCeCommand);
                this.dbManager.Close();
                result = true;
            }
            catch (Exception ex)
            {
                Executing.Instance.Insert_Log("ERRDELETE_SC1_2", ex.Message.ToString(), "Executing", "Method DoDeleteDelivery");
                result = false;
            }
            finally
            {
                if (this.dbManager != null)
                {
                    this.dbManager.Dispose();
                }
            }
            return result;
        }
        public string CountBySellOrderNo(string _sell_order_no)
        {
            string value = string.Empty;
            try
            {
                DataTable dt_get = new DataTable();
                string _count = string.Empty;

                string sql = "SELECT COUNT(*) AS count_by_pre_order " +
                             "FROM warehouse " +
                             "WHERE (sell_order_no = '" + _sell_order_no + "') ";

                SqlCeCommand command = new SqlCeCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = sql.ToString();
                dbManager.Open();
                dt_get = dbManager.ExecuteToDataTable(command);

                if (dt_get.Rows.Count > 0)
                    value = Convert.ToString(dt_get.Rows[0]["count_by_pre_order"]);

                return value;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public string CountAllSellOrderNo()
        {
            string value = string.Empty;
            try
            {
                DataTable dt_get = new DataTable();
                string _count = string.Empty;

                string sql = "select count(*) as sell_order_no from(select distinct sell_order_no from warehouse) as t_sell_order ";

                SqlCeCommand command = new SqlCeCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = sql.ToString();
                dbManager.Open();
                dt_get = dbManager.ExecuteToDataTable(command);

                if (!dt_get.IsNullOrNoRows())
                    value = Convert.ToString(dt_get.Rows[0]["sell_order_no"]);

                return value;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region 12. Delivery

        public string CountDeliveryALL()
        {
            string text = string.Empty;
            string result;
            try
            {
                DataTable dataTable = new DataTable();
                string empty = string.Empty;
                string sql = "SELECT COUNT(*) AS count_delivery FROM delivery ";
                SqlCeCommand sqlCeCommand = new SqlCeCommand();
                sqlCeCommand.CommandType = CommandType.Text;
                sqlCeCommand.CommandText = sql.ToString();
                this.dbManager.Open();
                dataTable = this.dbManager.ExecuteToDataTable(sqlCeCommand);
                this.dbManager.Close();
                if (dataTable.Rows.Count > 0)
                    text = Convert.ToString(dataTable.Rows[0]["count_delivery"]);
                else
                    text = "0";

                result = text;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (this.dbManager != null)
                {
                    this.dbManager.Dispose();
                }
            }
            return result;
        }
        public DataTable GetCustomer(string _cust_code, string factory_code, string _vehicle)
        {
            DataTable result;
            try
            {
                DataTable dataTable = new DataTable();
                string sql = "SELECT create_date, empty_or_full, cust_id, factory_code, cylinder_sn, vehicle FROM delivery WHERE (cust_id ='" + _cust_code + "') AND (vehicle ='" + _vehicle + "') AND (factory_code ='" + factory_code + "') ";
                SqlCeCommand sqlCeCommand = new SqlCeCommand();
                sqlCeCommand.CommandType = CommandType.Text;
                sqlCeCommand.CommandText = sql.ToString();
                this.dbManager.Open();
                dataTable = this.dbManager.ExecuteToDataTable(sqlCeCommand);
                this.dbManager.Close();
                result = dataTable;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (this.dbManager != null)
                {
                    this.dbManager.Dispose();
                }
            }
            return result;
        }
        public bool checkCylinderDeliveryDuplicate(string _cylinder_sn)
        {
            bool result;
            try
            {
                DataTable dataTable = new DataTable();
                string sql = "SELECT * FROM delivery WHERE (cylinder_sn='" + _cylinder_sn + "') ";
                SqlCeCommand sqlCeCommand = new SqlCeCommand();
                sqlCeCommand.CommandType = CommandType.Text;
                sqlCeCommand.CommandText = sql.ToString();
                this.dbManager.Open();
                dataTable = this.dbManager.ExecuteToDataTable(sqlCeCommand);
                this.dbManager.Close();

                if (dataTable.Rows.Count > 0)
                    result = true;
                else
                    result = false;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (this.dbManager != null)
                {
                    this.dbManager.Dispose();
                }
            }
            return result;
        }
        public bool SaveDelivery(DeliveryEntity _ent)
        {
            bool result = false;
            try
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append("INSERT INTO delivery (factory_code, cust_id, vehicle, item_code, empty_or_full, cylinder_sn, create_by, create_date) ");
                stringBuilder.Append(string.Format("VALUES ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}') ", _ent.factory, _ent.cust_id, _ent.vehicle, _ent.item_code, _ent.empty_or_full, _ent.cylinder_sn, _ent.create_by, _ent.create_date));
                SqlCeCommand sqlCeCommand = new SqlCeCommand();
                sqlCeCommand.CommandType = CommandType.Text;
                sqlCeCommand.CommandText = stringBuilder.ToString();
                this.dbManager.Open();

                if (this.dbManager.ExecuteNonQuery(sqlCeCommand) > 0)
                    result = true;

                this.dbManager.Close();
            }
            catch (SqlCeException)
            {
                result = false;
                throw;
            }
            finally
            {
                if (this.dbManager != null)
                {
                    this.dbManager.Dispose();
                }
            }
            return result;
        }
        public DataTable getDeliveryForDel()
        {
            DataTable result;
            try
            {
                DataTable dataTable = new DataTable();
                string sql = "select de.*, cu.cust_name from delivery de inner join customer cu on de.cust_id = cu.cust_id ORDER BY de.create_date DESC ";
                SqlCeCommand sqlCeCommand = new SqlCeCommand();
                sqlCeCommand.CommandType = CommandType.Text;
                sqlCeCommand.CommandText = sql.ToString();
                this.dbManager.Open();
                dataTable = this.dbManager.ExecuteToDataTable(sqlCeCommand);
                this.dbManager.Close();
                result = dataTable;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (this.dbManager != null)
                {
                    this.dbManager.Dispose();
                }
            }
            return result;
        }
        public string DeliveryByCustomer(string _cust_id)
        {
            string text = string.Empty;
            string result;
            try
            {
                DataTable dataTable = new DataTable();
                string empty = string.Empty;
                string sql = "SELECT COUNT(*) AS count_dis_ret FROM delivery WHERE (cust_id = '" + _cust_id + "') ";
                SqlCeCommand sqlCeCommand = new SqlCeCommand();
                sqlCeCommand.CommandType = CommandType.Text;
                sqlCeCommand.CommandText = sql.ToString();
                this.dbManager.Open();
                dataTable = this.dbManager.ExecuteToDataTable(sqlCeCommand);
                this.dbManager.Close();
                if (dataTable.Rows.Count > 0)
                {
                    text = Convert.ToString(dataTable.Rows[0]["count_dis_ret"]);
                }

                result = text;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (this.dbManager != null)
                {
                    this.dbManager.Dispose();
                }
            }
            return result;
        }
        public bool DeleteDelivery(string _factory, string _cust_id, string _item_code, string _empty_or_full, string _serial_number)
        {
            bool result;
            try
            {
                string sql = string.Concat(new string[]
		{
			"DELETE FROM delivery WHERE (factory_code = '",
			_factory,
			"') AND (cust_id = '",
			_cust_id,
			"') AND (item_code = '",
			_item_code,
			"') AND (empty_or_full = '",
			_empty_or_full,
			"') AND (cylinder_sn = '",
			_serial_number,
			"')"
		});
                SqlCeCommand sqlCeCommand = new SqlCeCommand();
                sqlCeCommand.CommandType = CommandType.Text;
                sqlCeCommand.CommandText = sql.ToString();
                this.dbManager.Open();
                this.dbManager.ExecuteNonQuery(sqlCeCommand);
                this.dbManager.Close();
                result = true;
            }
            catch (Exception ex)
            {
                Executing.Instance.Insert_Log("ERRDELETE_SC2_2", ex.Message.ToString(), "Executing", "Method DeleteDelivery");
                result = false;
            }
            finally
            {
                if (this.dbManager != null)
                {
                    this.dbManager.Dispose();
                }
            }
            return result;
        }

        #endregion

        #region 13. Other Scan

        public string CountByLotNumber(string _lot_number)
        {
            string value = string.Empty;
            try
            {
                DataTable dt_get = new DataTable();
                string _count = string.Empty;

                string sql = "SELECT COUNT(*) AS count_by_lotNumber " +
                             "FROM otherscans " +
                             "WHERE (notes = '" + _lot_number + "') ";

                SqlCeCommand command = new SqlCeCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = sql.ToString();
                dbManager.Open();
                dt_get = dbManager.ExecuteToDataTable(command);

                if (dt_get.Rows.Count > 0)
                    value = Convert.ToString(dt_get.Rows[0]["count_by_lotNumber"]);

                return value;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public string CountAllLotNumber()
        {
            string value = string.Empty;
            try
            {
                DataTable dt_get = new DataTable();
                string _count = string.Empty;

                string sql = "select count(*) as lot_number from(select distinct notes from otherscans) as t_otherscans ";

                SqlCeCommand command = new SqlCeCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = sql.ToString();
                dbManager.Open();
                dt_get = dbManager.ExecuteToDataTable(command);

                if (!dt_get.IsNullOrNoRows())
                    value = Convert.ToString(dt_get.Rows[0]["lot_number"]);

                return value;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataTable getLotBarcode(string _lot_number)
        {
            DataTable result;
            try
            {
                DataTable dataTable = new DataTable();
                string text = "SELECT create_date, notes, scan_number FROM otherscans WHERE (notes='" + _lot_number + "') ";
                SqlCeCommand sqlCeCommand = new SqlCeCommand();
                sqlCeCommand.CommandType = CommandType.Text;
                sqlCeCommand.CommandText = text.ToString();
                this.dbManager.Open();
                dataTable = this.dbManager.ExecuteToDataTable(sqlCeCommand);
                this.dbManager.Close();
                result = dataTable;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (this.dbManager != null)
                {
                    this.dbManager.Dispose();
                }
            }
            return result;
        }
        public bool checkScanNumberDuplicate(string _scan_number)
        {
            try
            {
                DataTable dt_chk_serial = new DataTable();
                string sql = "SELECT * " +
                             "FROM otherscans " +
                             "WHERE (scan_number='" + _scan_number + "') ";

                SqlCeCommand command = new SqlCeCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = sql.ToString();
                dbManager.Open();
                dt_chk_serial = dbManager.ExecuteToDataTable(command);

                if (dt_chk_serial.Rows.Count > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool DoSaveOtherScan(OtherScanEntity _ent)
        {
            bool _result = false;
            try
            {

                StringBuilder sql = new StringBuilder();
                sql.Append("INSERT INTO otherscans (scan_number, notes, item_code, create_by, create_date) ");
                sql.Append(string.Format("VALUES ('{0}','{1}','{2}','{3}','{4}') ", _ent.scan_number, _ent.notes, _ent.item_code, _ent.create_by, _ent.create_date));

                SqlCeCommand command = new SqlCeCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = sql.ToString();

                dbManager.Open();
                if (dbManager.ExecuteNonQuery(command) > 0)
                    _result = true;
            }
            catch (SqlCeException)
            {
                _result = false;
                throw;
            }
            finally
            {
                if (dbManager != null)
                {
                    dbManager.Dispose();
                }
            }
            return _result;
        }

        #endregion

        #region 14. View Data

        // Warehouse
        public DataTable getWarehouse()
        {
            DataTable result;
            try
            {
                DataTable dataTable = new DataTable();
                string sql = "SELECT * FROM warehouse ORDER BY create_date DESC";
                SqlCeCommand sqlCeCommand = new SqlCeCommand();
                sqlCeCommand.CommandType = CommandType.Text;
                sqlCeCommand.CommandText = sql.ToString();
                this.dbManager.Open();
                dataTable = this.dbManager.ExecuteToDataTable(sqlCeCommand);
                this.dbManager.Close();
                result = dataTable;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (this.dbManager != null)
                {
                    this.dbManager.Dispose();
                }
            }
            return result;
        }
        public DataTable getWarehouseByCondition(string _condition_type, string _condition_value)
        {
            DataTable result;
            try
            {
                DataTable dataTable = new DataTable();
                if (_condition_type.Trim() == "CYLINDER")
                {
                    string sql = "SELECT * FROM warehouse WHERE cylinder_sn LIKE '%" + _condition_value + "%' ORDER BY create_date DESC ";
                    SqlCeCommand sqlCeCommand = new SqlCeCommand();
                    sqlCeCommand.CommandType = CommandType.Text;
                    sqlCeCommand.CommandText = sql.ToString();
                    this.dbManager.Open();
                    dataTable = this.dbManager.ExecuteToDataTable(sqlCeCommand);
                    this.dbManager.Close();
                }
                else if(_condition_type.Trim() == "ITEM_CODE")
                {
                    string sql = "SELECT * FROM warehouse WHERE item_code LIKE '%" + _condition_value + "%' ORDER BY create_date DESC ";
                    SqlCeCommand sqlCeCommand = new SqlCeCommand();
                    sqlCeCommand.CommandType = CommandType.Text;
                    sqlCeCommand.CommandText = sql.ToString();
                    this.dbManager.Open();
                    dataTable = this.dbManager.ExecuteToDataTable(sqlCeCommand);
                    this.dbManager.Close();
                }

                result = dataTable;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (this.dbManager != null)
                {
                    this.dbManager.Dispose();
                }
            }
            return result;
        }
        // Delivery
        public DataTable getDelivery()
        {
            DataTable result;
            try
            {
                DataTable dataTable = new DataTable();
                string sql = "SELECT * FROM delivery ORDER BY create_date DESC";
                SqlCeCommand sqlCeCommand = new SqlCeCommand();
                sqlCeCommand.CommandType = CommandType.Text;
                sqlCeCommand.CommandText = sql.ToString();
                this.dbManager.Open();
                dataTable = this.dbManager.ExecuteToDataTable(sqlCeCommand);
                this.dbManager.Close();
                result = dataTable;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (this.dbManager != null)
                {
                    this.dbManager.Dispose();
                }
            }
            return result;
        }
        public DataTable getDeliveryByCondition(string _condition_type, string _condition_value)
        {
            DataTable result;
            try
            {
                DataTable dataTable = new DataTable();
                if (_condition_type.Trim() == "CYLINDER")
                {
                    string sql = "SELECT * FROM delivery WHERE cylinder_sn LIKE '%" + _condition_value + "%' ORDER BY create_date DESC ";
                    SqlCeCommand sqlCeCommand = new SqlCeCommand();
                    sqlCeCommand.CommandType = CommandType.Text;
                    sqlCeCommand.CommandText = sql.ToString();
                    this.dbManager.Open();
                    dataTable = this.dbManager.ExecuteToDataTable(sqlCeCommand);
                    this.dbManager.Close();
                }
                else if (_condition_type.Trim() == "ITEM_CODE")
                {
                    string sql = "SELECT * FROM delivery WHERE item_code LIKE '%" + _condition_value + "%' ORDER BY create_date DESC ";
                    SqlCeCommand sqlCeCommand = new SqlCeCommand();
                    sqlCeCommand.CommandType = CommandType.Text;
                    sqlCeCommand.CommandText = sql.ToString();
                    this.dbManager.Open();
                    dataTable = this.dbManager.ExecuteToDataTable(sqlCeCommand);
                    this.dbManager.Close();
                }

                result = dataTable;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (this.dbManager != null)
                {
                    this.dbManager.Dispose();
                }
            }
            return result;
        }
        // Other Scan
        public DataTable getOtherScan()
        {
            DataTable result;
            try
            {
                DataTable dataTable = new DataTable();
                string sql = "SELECT * FROM otherscans ORDER BY create_date DESC";
                SqlCeCommand sqlCeCommand = new SqlCeCommand();
                sqlCeCommand.CommandType = CommandType.Text;
                sqlCeCommand.CommandText = sql.ToString();
                this.dbManager.Open();
                dataTable = this.dbManager.ExecuteToDataTable(sqlCeCommand);
                this.dbManager.Close();
                result = dataTable;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (this.dbManager != null)
                {
                    this.dbManager.Dispose();
                }
            }
            return result;
        }
        
        #endregion

        #region 15. Get Password by Screen

        public string getPassword(string _screen_type)
        {
            string text = string.Empty;
            string result;
            try
            {
                DataTable dataTable = new DataTable();
                string empty = string.Empty;
                string text2 = "select password from t_password WHERE (screen_type = '" + _screen_type + "') ";
                SqlCeCommand sqlCeCommand = new SqlCeCommand();
                sqlCeCommand.CommandType = CommandType.Text;
                sqlCeCommand.CommandText = text2.ToString();
                this.dbManager.Open();
                dataTable = this.dbManager.ExecuteToDataTable(sqlCeCommand);
                this.dbManager.Close();
                if (!dataTable.IsNullOrNoRows())
                    text = Convert.ToString(dataTable.Rows[0]["password"]);
                else
                    text = "0";

                result = text;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (this.dbManager != null)
                {
                    this.dbManager.Dispose();
                }
            }
            return result;
        }

        #endregion

        #region 16. Is Sign Software

        public bool IsSignIn()
        {
            bool result = false;
            try
            {
                string text = "update t_password set [password] = 'Y' where screen_type = 'IS_USED' ";
                SqlCeCommand sqlCeCommand = new SqlCeCommand();
                sqlCeCommand.CommandType = CommandType.Text;
                sqlCeCommand.CommandText = text.ToString();
                this.dbManager.Open();
                this.dbManager.ExecuteNonQuery(sqlCeCommand);
                this.dbManager.Close();
                result = true;
            }
            catch (Exception)
            {
                result = false;
            }

            return result;
        }
        public bool IsSignOut()
        {
            bool result = false;
            try
            {
                string text = "update t_password set [password] = 'N' where screen_type = 'IS_USED' ";
                SqlCeCommand sqlCeCommand = new SqlCeCommand();
                sqlCeCommand.CommandType = CommandType.Text;
                sqlCeCommand.CommandText = text.ToString();
                this.dbManager.Open();
                this.dbManager.ExecuteNonQuery(sqlCeCommand);
                this.dbManager.Close();
                result = true;
            }
            catch (Exception)
            {
                result = false;
            }

            return result;
        }

        #endregion

        #region 17. Get Config Sign On

        public string getConfigSignOn(string _screen_type)
        {
            string text = string.Empty;
            string result;
            try
            {
                DataTable dataTable = new DataTable();
                string empty = string.Empty;
                string text2 = "select password from t_password WHERE (screen_type = '" + _screen_type + "') ";
                SqlCeCommand sqlCeCommand = new SqlCeCommand();
                sqlCeCommand.CommandType = CommandType.Text;
                sqlCeCommand.CommandText = text2.ToString();
                this.dbManager.Open();
                dataTable = this.dbManager.ExecuteToDataTable(sqlCeCommand);
                this.dbManager.Close();
                if (!dataTable.IsNullOrNoRows())
                    text = Convert.ToString(dataTable.Rows[0]["password"]);
                else
                    text = "0";

                result = text;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (this.dbManager != null)
                {
                    this.dbManager.Dispose();
                }
            }
            return result;
        }

        #endregion

        #region 18. Check exits table

        public bool checkTableExits(string tableName)
        {
            bool value = false;
            try
            {
                DataTable dt_get = new DataTable();
                string _count = string.Empty;

                string sql = "SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = '" + tableName + "' ";

                SqlCeCommand command = new SqlCeCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = sql.ToString();
                dbManager.Open();
                dt_get = dbManager.ExecuteToDataTable(command);

                if (!dt_get.IsNullOrNoRows())
                {
                    value = true;
                }
                else
                {
                    value = false;
                }

                return value;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region 19. Create Table

        public void CreateTableSAPImport()
        {
            try
            {

                StringBuilder sql = new StringBuilder();
                sql.Append(string.Format(@"CREATE TABLE SAP_Import 
(Posting_Date nvarchar(20) NULL,
Transaction_Type nvarchar(150) NULL,
Delivery_No nvarchar(100) NULL,
Invoice_No nvarchar(150) NULL,
Customer nvarchar(100) NULL,
Customer_Name nvarchar(4000) NULL,
Material nvarchar(100) NULL,
Material_Description nvarchar(4000) NULL,
Quantity nvarchar(10) NULL,
Serial_Number nvarchar(150) NULL,
Batch nvarchar(150) NULL,
Plant nvarchar(80) NULL,
Plant_Name nvarchar(4000) NULL,
Car_ID nvarchar(150) NULL,
Customer_Purchase_Order_Number nvarchar(350) NULL,
Create_Date datetime NULL) "));

                SqlCeCommand command = new SqlCeCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = sql.ToString();

                dbManager.Open();
                dbManager.ExecuteNonQuery(command);
            }
            catch (SqlCeException)
            {
            }
            finally
            {
                if (dbManager != null)
                {
                    dbManager.Dispose();
                }
            }
        }

        #endregion

        #region 20. Check exits table

        public bool checkPasswordExits(string screenType)
        {
            bool value = false;
            try
            {
                DataTable dt_get = new DataTable();
                string _count = string.Empty;

                string sql = "select [password] from t_password where screen_type = '" + screenType + "' ";

                SqlCeCommand command = new SqlCeCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = sql.ToString();
                dbManager.Open();
                dt_get = dbManager.ExecuteToDataTable(command);

                if (!dt_get.IsNullOrNoRows())
                {
                    value = true;
                }
                else
                {
                    value = false;
                }

                return value;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region 21. Insert Password Download SAP

        public bool Insert_Password(string id, string screenType, string password)
        {
            bool _result = false;
            try
            {

                StringBuilder sql = new StringBuilder();
                sql.Append("INSERT INTO t_password (id,screen_type,[password],create_date,update_date)  ");
                sql.Append(string.Format("VALUES ('{0}','{1}','{2}', getdate(), getdate()) ", id, screenType, password));

                SqlCeCommand command = new SqlCeCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = sql.ToString();

                dbManager.Open();
                dbManager.ExecuteNonQuery(command);
                _result = true;
            }
            catch (SqlCeException)
            {
                _result = false;
            }
            finally
            {
                if (dbManager != null)
                {
                    dbManager.Dispose();
                }
            }
            return _result;
        }

        #endregion

        #region 22. Get Id Password

        public string getMaxIdPassword()
        {
            string text = string.Empty;
            string result;
            try
            {
                DataTable dataTable = new DataTable();
                string empty = string.Empty;
                string text2 = "select top(1) id from t_password order by id desc; ";
                SqlCeCommand sqlCeCommand = new SqlCeCommand();
                sqlCeCommand.CommandType = CommandType.Text;
                sqlCeCommand.CommandText = text2.ToString();
                this.dbManager.Open();
                dataTable = this.dbManager.ExecuteToDataTable(sqlCeCommand);
                this.dbManager.Close();
                if (!dataTable.IsNullOrNoRows())
                {
                    text = Convert.ToString(dataTable.Rows[0]["id"]);
                }
                else
                {
                    text = string.Empty;
                }

                result = text;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (this.dbManager != null)
                {
                    this.dbManager.Dispose();
                }
            }
            return result;
        }

        #endregion

        #region G. IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}