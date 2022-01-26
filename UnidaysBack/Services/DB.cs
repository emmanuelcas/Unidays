
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Threading.Tasks;
using UnidaysBack.Models;
using UnidaysBack.Utils;

namespace UnidaysBack.Services
{
    public static class DB
    {
        public static string ConnectionString;



        public static async Task Insert(CreatedUser value)
        {
            if (!IsEmailAlreadyPersisted(value.Email))
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
                    using (var command = new MySqlCommand(query, conn))
                    {
                        if (parameters != null)
                        {
                            foreach (var parameter in parameters)
                            {
                                command.Parameters.Add(parameter);
                            }
                            await command.ExecuteNonQueryAsync();
                        }
                    }
                }
            }
            else
            {
                throw new Exception("User with that email already exists");
            }
                
        }



        private static bool IsEmailAlreadyPersisted(string email)
        {
            string emailId = string.Empty;
            using (var conn = new MySqlConnection(ConnectionString))
            {
                string query = "select id from createdusers where email = '" + HashHelper.GetStringHash(email) + "'";
                conn.Open();
                MySqlCommand command = new MySqlCommand(query, conn);
                emailId = Convert.ToString(command.ExecuteScalar());

            }

            return !(emailId == "");
        }


        public static void SignInValidation(SignInUser user)
        {
            string signInUserID = string.Empty;
            using (var conn = new MySqlConnection(ConnectionString))
            {
                string query = "select id from loginusers where (username = '" + HashHelper.GetStringHash(user.Username) + "' AND password = '" + HashHelper.GetStringHash(user.Password) + "')";
                conn.Open();
                var command = new MySqlCommand(query, conn);
                signInUserID = Convert.ToString(command.ExecuteScalar());

            }

            if(signInUserID == "")
                throw new Exception("Invalid User and/or Password");

        }

    }
}
