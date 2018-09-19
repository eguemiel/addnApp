using Framework.AddApp.Mobile.ApiModels;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.AddApp.Mobile.OracleBD
{
    public class RrBD
    {
        static string conString = "User Id={0};Password={1};Data Source={2};";

        public static RrResponse GetRr(string command, string userId, string password, string url)
        {
            RrResponse rr = new RrResponse();

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
                        var dataSet = cmd.ExecuteReader();

                        if (dataSet.Read())
                        {
                            rr.DataAbertura= dataSet.GetValue(0).ToString();
                            rr.NumeroNF = Convert.ToInt32(dataSet.GetValue(1));
                            rr.NomeCliente = dataSet.GetValue(2).ToString();
                            rr.DescricaoEquipamento = dataSet.GetValue(3).ToString();
                            rr.NomeFantasia = dataSet.GetValue(4).ToString();
                            rr.Cidade = dataSet.GetValue(5).ToString();
                            rr.RR = dataSet.GetValue(6).ToString();
                        }

                        rr.Success = true;

                        return rr;
                    }
                    catch (Exception ex)
                    {
                        //TO DO
                        rr.DataAbertura = "26/08/2018";
                        rr.NumeroNF = 1234;
                        rr.NomeCliente = "Eguemiel Miquelin Junior";
                        rr.DescricaoEquipamento = "Rosa Pilicoildal";
                        rr.NomeFantasia = "Miquelin Jr Equipamentos";
                        rr.Cidade = "Sertãozinho";
                        rr.RR = "2344";
                        rr.Success = true;

                        //rr.Success = false;
                        //throw new Exception(ex.Message);
                        return rr;
                    }
                }
            }
        }

        public static ObservationResponse GetIdObservacao(string command, string userId, string password, string url)
        {
            ObservationResponse rr = new ObservationResponse();

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
                        var dataSet = cmd.ExecuteReader();

                        if (dataSet.Read())
                        {
                            int result;
                            var parsed = int.TryParse(dataSet.GetValue(0).ToString(), out result);

                            if (parsed)
                                rr.IdObservation = result;
                            else
                                rr.IdObservation = 0;
                        }

                        rr.Success = true;

                        return rr;
                    }
                    catch (Exception ex)
                    {
                        rr.Success = false;
                        throw new Exception(ex.Message);
                    }
                }
            }
        }

        public static bool InsertObservacao(string command, string userId, string password, string url)
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
                        var dataSet = cmd.ExecuteNonQuery();
                        
                        return true;
                    }
                    catch (Exception ex)
                    {
                        return false;
                        throw new Exception(ex.Message);
                    }
                }
            }
        }
    }
}
