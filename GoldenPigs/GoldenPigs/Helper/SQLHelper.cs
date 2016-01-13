using System;
using System.Collections.Generic;
using System.Data;

using MySql.Data.MySqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldenPigs.Helper
{
    public class SQLHelper
    {
        public static MySqlCommand cmd = null;
        public static MySqlConnection conn = null;
        public static string connstr ="server=localhost;User Id=root;database=aicai;Password=root;Character Set=utf8;";
        public SQLHelper()
        { }
        #region 建立数据库连接对象
        /// <summary>
        /// 建立数据库连接
        /// </summary>
        /// <returns>返回一个数据库的连接MySqlConnection对象</returns>
        public static MySqlConnection init()
        {
            try
            {
                conn = new MySqlConnection(connstr);
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message.ToString());
            }
            return conn;
        }
        #endregion

        #region 设置MySqlCommand对象
        /// <summary>
        /// 设置MySqlCommand对象       
        /// </summary>
        /// <param name="cmd">MySqlCommand对象 </param>
        /// <param name="cmdText">命令文本</param>
        /// <param name="cmdType">命令类型</param>
        /// <param name="cmdParms">参数集合</param>
        private static void SetCommand(MySqlCommand cmd, string cmdText, CommandType cmdType, MySqlParameter[] cmdParms)
        {
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            cmd.CommandType = cmdType;
            if (cmdParms != null)
            {
                cmd.Parameters.AddRange(cmdParms);
            }
        }
        #endregion

        #region 执行相应的sql语句，返回相应的DataSet对象
        /// <summary>
        /// 执行相应的sql语句，返回相应的DataSet对象
        /// </summary>
        /// <param name="sqlstr">sql语句</param>
        /// <returns>返回相应的DataSet对象</returns>
        public static DataSet GetDataSet(string sqlstr)
        {
            DataSet set = new DataSet();
            try
            {
                init();
                MySqlDataAdapter adp = new MySqlDataAdapter(sqlstr, conn);
                adp.Fill(set);
                conn.Close();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message.ToString());
            }
            return set;
        }
        #endregion

        #region 执行相应的sql语句，返回相应的DataSet对象
        /// <summary>
        /// 执行相应的sql语句，返回相应的DataSet对象
        /// </summary>
        /// <param name="sqlstr">sql语句</param>
        /// <param name="tableName">表名</param>
        /// <returns>返回相应的DataSet对象</returns>
        public static DataSet GetDataSet(string sqlstr, string tableName)
        {
            DataSet set = new DataSet();
            try
            {
                init();
                MySqlDataAdapter adp = new MySqlDataAdapter(sqlstr, conn);
                adp.Fill(set, tableName);
                conn.Close();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message.ToString());
            }
            return set;
        }
        #endregion

        #region 执行不带参数sql语句，返回所影响的行数
        /// <summary>
        /// 执行不带参数sql语句，返回所影响的行数
        /// </summary>
        /// <param name="cmdstr">增，删，改sql语句</param>
        /// <returns>返回所影响的行数</returns>
        public static int ExecuteNonQuery(string cmdText)
        {
            int count;
            try
            {
                init();
                cmd = new MySqlCommand(cmdText, conn);
                count = cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
            return count;
        }
        #endregion

        #region 执行带参数sql语句或存储过程，返回所影响的行数
        /// <summary>
        /// 执行带参数sql语句或存储过程，返回所影响的行数
        /// </summary>
        /// <param name="cmdText">带参数的sql语句和存储过程名</param>
        /// <param name="cmdType">命令类型</param>
        /// <param name="cmdParms">参数集合</param>
        /// <returns>返回所影响的行数</returns>
        public static int ExecuteNonQuery(string cmdText, CommandType cmdType, MySqlParameter[] cmdParms)
        {
            int count;
            try
            {
                init();
                cmd = new MySqlCommand();
                SetCommand(cmd, cmdText, cmdType, cmdParms);
                count = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                conn.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
            return count;
        }
        #endregion

        #region 执行不带参数sql语句，返回一个从数据源读取数据的MySqlDataReader对象
        /// <summary>
        /// 执行不带参数sql语句，返回一个从数据源读取数据的MySqlDataReader对象
        /// </summary>
        /// <param name="cmdstr">相应的sql语句</param>
        /// <returns>返回一个从数据源读取数据的MySqlDataReader对象</returns>
        public static MySqlDataReader ExecuteReader(string cmdText)
        {
            MySqlDataReader reader;
            try
            {
                init();
                cmd = new MySqlCommand(cmdText, conn);
                reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
            return reader;
        }
        #endregion

        #region 执行带参数的sql语句或存储过程，返回一个从数据源读取数据的MySqlDataReader对象
        /// <summary>
        /// 执行带参数的sql语句或存储过程，返回一个从数据源读取数据的MySqlDataReader对象
        /// </summary>
        /// <param name="cmdText">sql语句或存储过程名</param>
        /// <param name="cmdType">命令类型</param>
        /// <param name="cmdParms">参数集合</param>
        /// <returns>返回一个从数据源读取数据的MySqlDataReader对象</returns>
        public static MySqlDataReader ExecuteReader(string cmdText, CommandType cmdType, MySqlParameter[] cmdParms)
        {
            MySqlDataReader reader;
            try
            {
                init();
                cmd = new MySqlCommand();
                SetCommand(cmd, cmdText, cmdType, cmdParms);
                reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
            return reader;
        }
        #endregion

        #region 执行不带参数sql语句，返回一个DataTable对象
        /// <summary>
        /// 执行不带参数sql语句，返回一个DataTable对象
        /// </summary>
        /// <param name="cmdText">相应的sql语句</param>
        /// <returns>返回一个DataTable对象</returns>
        public static DataTable GetDataTable(string cmdText)
        {

            MySqlDataReader reader;
            DataTable dt = new DataTable();
            try
            {
                init();
                cmd = new MySqlCommand(cmdText, conn);
                reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dt.Load(reader);
                reader.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
            return dt;
        }
        #endregion

        #region 执行带参数的sql语句或存储过程，返回一个DataTable对象
        /// <summary>
        /// 执行带参数的sql语句或存储过程，返回一个DataTable对象
        /// </summary>
        /// <param name="cmdText">sql语句或存储过程名</param>
        /// <param name="cmdType">命令类型</param>
        /// <param name="cmdParms">参数集合</param>
        /// <returns>返回一个DataTable对象</returns>
        public static DataTable GetDataTable(string cmdText, CommandType cmdType, MySqlParameter[] cmdParms)
        {
            MySqlDataReader reader;
            DataTable dt = new DataTable();
            try
            {
                init();
                cmd = new MySqlCommand();
                SetCommand(cmd, cmdText, cmdType, cmdParms);
                reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dt.Load(reader);
                reader.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
            return dt;
        }
        #endregion

        #region 执行不带参数sql语句,返回结果集首行首列的值object
        /// <summary>
        /// 执行不带参数sql语句,返回结果集首行首列的值object
        /// </summary>
        /// <param name="cmdstr">相应的sql语句</param>
        /// <returns>返回结果集首行首列的值object</returns>
        public static object ExecuteScalar(string cmdText)
        {
            object obj;
            try
            {
                init();
                cmd = new MySqlCommand(cmdText, conn);
                obj = cmd.ExecuteScalar();
                conn.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
            return obj;
        }
        #endregion

        #region 执行带参数sql语句或存储过程,返回结果集首行首列的值object
        /// <summary>
        /// 执行带参数sql语句或存储过程,返回结果集首行首列的值object
        /// </summary>
        /// <param name="cmdText">sql语句或存储过程名</param>
        /// <param name="cmdType">命令类型</param>
        /// <param name="cmdParms">返回结果集首行首列的值object</param>
        /// <returns></returns>
        public static object ExecuteScalar(string cmdText, CommandType cmdType, MySqlParameter[] cmdParms)
        {
            object obj;
            try
            {
                init();
                cmd = new MySqlCommand();
                SetCommand(cmd, cmdText, cmdType, cmdParms);
                obj = cmd.ExecuteScalar();
                conn.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
            return obj;
        }
        #endregion
    }
}
