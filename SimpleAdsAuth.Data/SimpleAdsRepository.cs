using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace SimpleAdsAuth.Data
{
    public class SimpleAdsRepository
    {
        private string _connectionString;
        public SimpleAdsRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void AddUser (User user, string password)
        {
            using var connection = new SqlConnection(_connectionString);
            using var cmd = connection.CreateCommand();
            cmd.CommandText = "INSERT INTO Users (Name, Email, Password) VALUES (@name, @email, @password)";
            cmd.Parameters.AddWithValue("@Name", user.Name);
            cmd.Parameters.AddWithValue("@Email", user.Email);
            cmd.Parameters.AddWithValue("@Password", BCrypt.Net.BCrypt.HashPassword(password));
            connection.Open();
            cmd.ExecuteNonQuery();
        }

        public User Login(string email, string password)
        {
            var user = GetByEmail(email);
            if (user == null)
            {
                return null;
            }
            bool isValid = BCrypt.Net.BCrypt.Verify(password, user.Password);
            return isValid ? user : null;
        }

        public User GetByEmail(string email)
        {
            using var connection = new SqlConnection(_connectionString);
            using var cmd = connection.CreateCommand();
            cmd.CommandText = "SELECT TOP 1 * FROM Users WHERE Email = @email";
            cmd.Parameters.AddWithValue("@email", email);
            connection.Open();
            var reader = cmd.ExecuteReader();
            if (!reader.Read())
            {
                return null;
            }
          
            return new User
            {
                Id = (int)reader["Id"],
                Name = (string)reader["Name"],
                Email = (string)reader["Email"],
                Password = (string)reader["Password"]
            };
        }
        
        public void NewAd(Ad ad)
        {
            var connection = new SqlConnection(_connectionString);
            var cmd = connection.CreateCommand();
            cmd.CommandText = @"INSERT INTO AdsAuth (UserId, PhoneNumber, Details, DatePosted)
            VALUES (@UserId, @PhoneNumber, @Details, GETDATE())";
            
            cmd.Parameters.AddWithValue("@UserId", ad.UserId);
            cmd.Parameters.AddWithValue("@PhoneNumber", ad.PhoneNumber);
            cmd.Parameters.AddWithValue("@Details", ad.Details);
            connection.Open();
            cmd.ExecuteNonQuery();

        }
        public List<Ad> GetAllAds()
        {
            var connection = new SqlConnection(_connectionString);
            var cmd = connection.CreateCommand();
            cmd.CommandText = "SELECT a.*, u.Name " +
                "FROM AdsAuth A JOIN Users U On a.UserId = u.Id " +
                "ORDER BY DATEPOSTED DESC";
            connection.Open();
            var reader = cmd.ExecuteReader();
            var adList = new List<Ad>();
            while (reader.Read())
            {
                adList.Add(new Ad
                {
                   
                    UserId = (int)reader["UserId"],
                    PhoneNumber = (string)reader["PhoneNumber"],
                    Details = (string)reader["Details"],
                    DatePosted = (DateTime)reader["DatePosted"],
                    Name = (string)reader["Name"]
                });

            }

            return adList;
        }

        public List<Ad> GetAllAds(int userId)
        {
            var connection = new SqlConnection(_connectionString);
            var cmd = connection.CreateCommand();
            cmd.CommandText = "SELECT a.*, u.Name" +
                " FROM AdsAuth a Join Users U" +
                " ON a.UserId = u.Id" +
                " WHERE u.Id = @userId" +
                " ORDER BY DATEPOSTED DESC";
            cmd.Parameters.AddWithValue("@userId", userId);
            connection.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            var adList = new List<Ad>();
            while (reader.Read())
            {
                adList.Add(new Ad
                {
                    UserId = (int)reader["UserId"],
                    PhoneNumber = (string)reader["PhoneNumber"],
                    Details = (string)reader["Details"],
                    DatePosted = (DateTime)reader["DatePosted"],
                    Name = (string)reader["Name"]
                });

            }

            return adList;
        }

        public void DeleteAd(int id)
        {
            var connection = new SqlConnection(_connectionString);
            var cmd = connection.CreateCommand();
            cmd.CommandText = "DELETE FROM AdsAuth WHERE Id = @Id";
            cmd.Parameters.AddWithValue("@id", id);
            connection.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            connection.Close();
        }

    }
}
