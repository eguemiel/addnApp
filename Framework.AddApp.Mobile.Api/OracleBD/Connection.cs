using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Framework.AddApp.Mobile.OracleBD
{
    public static class Connection
    {
        static string conString = "User Id={0};Password={1};Data Source={2};";

        public static object SelectCommand(string command, string userId, string password, string url)
        {
            using (OracleConnection con = new OracleConnection(string.Format(conString, userId, password, url)))
            {
                using (OracleCommand cmd = con.CreateCommand())
                {
                    try
                    {
                        con.Open();
                        cmd.BindByName = true;

                        //Use the command to display employee names from 
                        // the EMPLOYEES table
                        cmd.CommandText = command;

                        //// Assign id to the department number 50 
                        //OracleParameter user = new OracleParameter("nome", usuario);
                        //OracleParameter pass = new OracleParameter("senha", senha);
                        //cmd.Parameters.Add(user);
                        //cmd.Parameters.Add(pass);

                        //Execute the command and use DataReader to display the data
                        var dataSet = cmd.ExecuteScalar();

                        return dataSet;

                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                }
            }
        }
    }
}
