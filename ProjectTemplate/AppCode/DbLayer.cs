using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ProjectTemplate.AppCode
{
    public class DbLayer
    {
        public DataTable GetData(string query)
        {
            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Department"].ConnectionString);
            try
            {
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.CommandType = CommandType.Text;
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                con.Open();
                da.Fill(dt);
                con.Close();
            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                };
            };
            return dt;
        }

        public DataTable GetData(string query, SqlParameter[] param)
        {
            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Department"].ConnectionString);
            try
            {
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.CommandType = CommandType.Text;
                for (int i = 0; i < param.Length; i++)
                {
                    cmd.Parameters.Add(param[i]);
                }
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                con.Open();
                da.Fill(dt);
                con.Close();
            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                };
            };
            return dt;
        }
    }
}