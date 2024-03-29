﻿using MySql.Data.MySqlClient;
using NazarTunes.Models.DataTemplates;
using System.Data;

namespace NazarTunes.Models.MySQLConnections
{
    public class AuthorizationLayerDb : AbsctractDb
    {
        public bool IfLoginExists(string login)
        {
            _cmd.CommandText = "function_check_if_login_exists";
            _cmd.CommandType = CommandType.StoredProcedure;
            _cmd.Parameters.Clear();
            _cmd.Parameters.Add("new_login", MySqlDbType.VarChar);
            _cmd.Parameters["new_login"].Value = login;
            var returnParameter = _cmd.Parameters.Add("@ReturnVal", MySqlDbType.Int32);
            returnParameter.Direction = ParameterDirection.ReturnValue;
            _db.Open();
            _cmd.ExecuteNonQuery();
            _db.Close();
            return (int)returnParameter.Value == 1;
        }

        private bool IfCredentialsCorrect(string login, string password)
        {
            _cmd.CommandText = "function_check_if_credentials_correct";
            _cmd.CommandType = CommandType.StoredProcedure;
            _cmd.Parameters.Clear();
            _cmd.Parameters.Add("new_login", MySqlDbType.VarChar);
            _cmd.Parameters["new_login"].Value = login;
            _cmd.Parameters.Add("new_pass", MySqlDbType.VarChar);
            _cmd.Parameters["new_pass"].Value = password;
            var returnParameter = _cmd.Parameters.Add("@ReturnVal", MySqlDbType.Int32);
            returnParameter.Direction = ParameterDirection.ReturnValue;
            _db.Open();
            _cmd.ExecuteNonQuery();
            _db.Close();
            return (int)returnParameter.Value == 1;
        }

        private bool IfUserIsDeleted(string login, string password)
        {
            _cmd.CommandText = "function_check_if_user_is_deleted";
            _cmd.CommandType = CommandType.StoredProcedure;
            _cmd.Parameters.Clear();
            _cmd.Parameters.Add("new_login", MySqlDbType.VarChar);
            _cmd.Parameters["new_login"].Value = login;
            _cmd.Parameters.Add("new_pass", MySqlDbType.VarChar);
            _cmd.Parameters["new_pass"].Value = password;
            var returnParameter = _cmd.Parameters.Add("@ReturnVal", MySqlDbType.Int32);
            returnParameter.Direction = ParameterDirection.ReturnValue;
            _db.Open();
            _cmd.ExecuteNonQuery();
            _db.Close();
            return (int)returnParameter.Value == 1;
        }

        public (bool correct_credentials, bool deleted_user, AbstractUser? user) Authorization(string login, string password)
        {
            if (IfCredentialsCorrect(login, password))
            {
                if (!IfUserIsDeleted(login, password))
                {
                    var role = GetRole(login, password);
                    if (role == "admin")
                    {
                        return (true, false, GetAdmin(login, password));
                    }
                    else
                    {
                        return (true, false, GetClient(login, password));
                    }
                }
                else return (true, true, null);
            }
            else return (false, true, null);
        }
        private Admin GetAdmin(string login, string password)
        {
            _cmd.CommandText = "procedure_get_admin";
            _cmd.CommandType = CommandType.StoredProcedure;
            _cmd.Parameters.Clear();
            _cmd.Parameters.Add("new_login", MySqlDbType.VarChar);
            _cmd.Parameters["new_login"].Value = login;
            _cmd.Parameters.Add("new_pass", MySqlDbType.VarChar);
            _cmd.Parameters["new_pass"].Value = password;
            var getId = _cmd.Parameters.Add("user_id", MySqlDbType.Int32);
            getId.Direction = ParameterDirection.Output;
            var getFirstName = _cmd.Parameters.Add("user_first_name", MySqlDbType.VarChar);
            getFirstName.Direction = ParameterDirection.Output;
            var getLastName = _cmd.Parameters.Add("user_last_name", MySqlDbType.VarChar);
            getLastName.Direction = ParameterDirection.Output;
            _db.Open();
            _cmd.ExecuteNonQuery();
            _db.Close();
            return new Admin { Id = (int)getId.Value, FirstName = (string)getFirstName.Value, LastName = (string)getLastName.Value };
        }

        private Client GetClient(string login, string password)
        {
            _cmd.CommandText = "procedure_get_client";
            _cmd.CommandType = CommandType.StoredProcedure;
            _cmd.Parameters.Clear();
            _cmd.Parameters.Add("new_login", MySqlDbType.VarChar);
            _cmd.Parameters["new_login"].Value = login;
            _cmd.Parameters.Add("new_pass", MySqlDbType.VarChar);
            _cmd.Parameters["new_pass"].Value = password;
            var getId = _cmd.Parameters.Add("user_id", MySqlDbType.Int32);
            getId.Direction = ParameterDirection.Output;
            var getFirstName = _cmd.Parameters.Add("user_first_name", MySqlDbType.VarChar);
            getFirstName.Direction = ParameterDirection.Output;
            var getLastName = _cmd.Parameters.Add("user_last_name", MySqlDbType.VarChar);
            getLastName.Direction = ParameterDirection.Output;
            var getPhone = _cmd.Parameters.Add("user_phone", MySqlDbType.VarChar);
            getPhone.Direction = ParameterDirection.Output;
            var getEmail = _cmd.Parameters.Add("user_email", MySqlDbType.VarChar);
            getEmail.Direction = ParameterDirection.Output;
            var getTotalAmount = _cmd.Parameters.Add("user_total_amount_spent", MySqlDbType.Double);
            getTotalAmount.Direction = ParameterDirection.Output;
            var getDiscount = _cmd.Parameters.Add("user_personal_discount", MySqlDbType.Int32);
            getDiscount.Direction = ParameterDirection.Output;
            var getIsSubscribed = _cmd.Parameters.Add("user_is_subscribed", MySqlDbType.Int32);
            getIsSubscribed.Direction = ParameterDirection.Output;
            _db.Open();
            _cmd.ExecuteNonQuery();
            _db.Close();
            return new Client
            {
                Id = (int)getId.Value,
                FirstName = (string)getFirstName.Value,
                LastName = (string)getLastName.Value,
                Phone = (string)getPhone.Value,
                Email = (string)getEmail.Value,
                TotalAmountSpent = (double)getTotalAmount.Value,
                PersonalDiscount = (int)getDiscount.Value,
                IsSubscribed = (int)getIsSubscribed.Value == 1
            };
        }

        private string GetRole(string login, string password)
        {
            _cmd.CommandText = "function_get_role";
            _cmd.CommandType = CommandType.StoredProcedure;
            _cmd.Parameters.Clear();
            _cmd.Parameters.Add("new_login", MySqlDbType.VarChar);
            _cmd.Parameters["new_login"].Value = login;
            _cmd.Parameters.Add("new_pass", MySqlDbType.VarChar);
            _cmd.Parameters["new_pass"].Value = password;
            var returnParameter = _cmd.Parameters.Add("@ReturnVal", MySqlDbType.VarChar);
            returnParameter.Direction = ParameterDirection.ReturnValue;
            _db.Open();
            _cmd.ExecuteNonQuery();
            _db.Close();
            return (string)returnParameter.Value;
        }

        public (Client? client, bool ifSucceed) CreateClient(string login, string password, string firstName, string lastName)
        {
            _cmd.CommandText = "procedure_create_user_with_no_check";
            _cmd.CommandType = CommandType.StoredProcedure;
            _cmd.Parameters.Clear();

            _cmd.Parameters.Add("new_login", MySqlDbType.VarChar);
            _cmd.Parameters["new_login"].Value = login;
            _cmd.Parameters.Add("new_pass", MySqlDbType.VarChar);
            _cmd.Parameters["new_pass"].Value = password;
            _cmd.Parameters.Add("new_role_id", MySqlDbType.Int32);
            _cmd.Parameters["new_role_id"].Value = 2;
            _cmd.Parameters.Add("new_first_name", MySqlDbType.VarChar);
            _cmd.Parameters["new_first_name"].Value = firstName;
            _cmd.Parameters.Add("new_last_name", MySqlDbType.VarChar);
            _cmd.Parameters["new_last_name"].Value = lastName;
            var getIfSucceed = _cmd.Parameters.Add("if_succeed", MySqlDbType.Int32);
            getIfSucceed.Direction = ParameterDirection.Output;

            _db.Open();
            _cmd.ExecuteNonQuery();
            _db.Close();

            if ((int)getIfSucceed.Value == 1)
            {
                return (GetClient(login, password), true);
            }
            return (null, false);
        }
    }
}
