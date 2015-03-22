using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Sql;
using System.Threading.Tasks;
using CarShare.Domain.Abstract;
using CarShare.Domain.Entities;
using System.Data.SqlClient;
using System.Data;

namespace CarShare.Domain.Concrete
{
    public class SQLUserRepository : IUserRepository
    {
        SqlConnection sqlConn = new SqlConnection("Data Source=(Local);Initial Catalog=CarShare;Integrated Security=true");

        public User GetUserByID(int userID)
        {
            sqlConn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM [User] WHERE UserID = @id", sqlConn);

            SqlParameter idParam = new SqlParameter("id", SqlDbType.Int);
            idParam.Value = userID;

            cmd.Parameters.Add( idParam );

            using (SqlDataReader dr = cmd.ExecuteReader())
            {
                if (dr.Read())
                {
                    return GetFromDatareader( dr );
                }
                else
                {
                    return null;
                }
            }

        }

        public void Save(User user)
        {
            throw new NotImplementedException();
        }

        public User Authenticate(string email, string pwd)
        {
            sqlConn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM [User] WHERE Email = @email AND Password = @pwd", sqlConn);

            SqlParameter emailParam = new SqlParameter("id", email);

            cmd.Parameters.Add(emailParam);

            SqlParameter pwdParam = new SqlParameter("pwd", pwd);

            cmd.Parameters.Add(pwdParam);


            using (SqlDataReader dr = cmd.ExecuteReader())
            {
                if (dr.Read())
                {
                    return GetFromDatareader(dr);
                }
                else
                {
                    return null;
                }
            }
        }

        private User GetFromDatareader(SqlDataReader sqlReader)
        {
            return new User(

                int.Parse(sqlReader["UserID"].ToString()),
                sqlReader["Name"].ToString(),
                sqlReader["Email"].ToString(),
                sqlReader["Password"].ToString(),
                sqlReader["NIC"].ToString(),
                sqlReader["DriverLicense"].ToString(),
                sqlReader["Image"].ToString(),
                sqlReader["PersonalDesc"].ToString(),
                sqlReader["Address"].ToString(),
                sqlReader["Status"].ToString(),
                sqlReader["ContactNo"].ToString()
            );
        }
    }
}
