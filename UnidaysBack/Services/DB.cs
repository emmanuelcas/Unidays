using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using UnidaysBack.Models;
using UnidaysBack.Utils;

namespace UnidaysBack.Services
{
    public static class DB
    {
        public static string ConnectionString;



        public static async Task<int> Insert(CreatedUser value)
        {
            int code = 1;
            if (CheckExistingEmail(value.Email) == false)
            {
                try
                {
                    using (MySqlConnection conn = new MySqlConnection(ConnectionString))
                    {

                        string query = "insert into createdusers (email,password) values (@email,@password);";
                        var parameters = new IDataParameter[]
                        {
                        new MySqlParameter("@email", HashHelper.GetStringHash(value.Email)),
                        new MySqlParameter("@password", HashHelper.GetStringHash(value.Password)),
                        };
                        conn.Open();
                        using (MySqlCommand command = new MySqlCommand(query, conn))
                        {
                            if (parameters != null)
                            {
                                foreach (IDataParameter para in parameters)
                                {
                                    command.Parameters.Add(para);
                                }
                                await command.ExecuteNonQueryAsync();
                            }

                        }
                    }
                }
                catch (Exception ex)
                {
                    //message = ex.Message;
                }
            }
            else
                code = 2;

            return code;
        }

        private static bool CheckExistingEmail(string email)
        {
            try
            {

                string emailid = string.Empty;
                using (MySqlConnection conn = new MySqlConnection(ConnectionString))
                {
                    string query = "select id from createdusers where email = '"+ HashHelper.GetStringHash(email) + "'";
                    conn.Open();
                    MySqlCommand command = new MySqlCommand(query, conn);
                    emailid = Convert.ToString(command.ExecuteScalar());

                }

                if (emailid == "")
                    return false;
                else
                    return true;
            }


            catch (Exception ex)
            {

            }
            return true;

        }


        public static bool SignInValidation(SignInUser user)
        {
            try
            {

                string SignInUserID = string.Empty;
                using (MySqlConnection conn = new MySqlConnection(ConnectionString))
                {
                    string query = "select id from loginusers where (username = '" + HashHelper.GetStringHash(user.Username) + "' AND password = '" + HashHelper.GetStringHash(user.Password) + "')";
                    conn.Open();
                    MySqlCommand command = new MySqlCommand(query, conn);
                    SignInUserID = Convert.ToString(command.ExecuteScalar());

                }

                if (SignInUserID == "")
                    return false;
                else
                    return true;
            }


            catch (Exception ex)
            {

            }
            return true;

        }

    }
}
