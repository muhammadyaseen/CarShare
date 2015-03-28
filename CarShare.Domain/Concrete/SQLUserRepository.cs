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
                    sqlConn.Close();

                    return null;
                }
            }

        }

        public int Save(User user)
        {
            sqlConn.Open();

            SqlCommand cmd = new SqlCommand("INSERT INTO [User]"
            + "         (Name, Email, Password, NIC, ContactNo, Status, Image, DriverLicense, Address, PersonalDesc)"
            + " VALUES (@name, @email, @pwd, @nic, @cno, @status, @img, @dl, @addr, @pdesc)", sqlConn);

            SqlParameter nameParam = new SqlParameter("name", user.Name);
            cmd.Parameters.Add(nameParam);

            SqlParameter emailParam = new SqlParameter("email", user.Email);
            cmd.Parameters.Add(emailParam);

            SqlParameter pwdParam = new SqlParameter("pwd", user.Password);
            cmd.Parameters.Add(pwdParam);

            SqlParameter nicParam = new SqlParameter("nic", user.NIC);
            cmd.Parameters.Add(nicParam);

            SqlParameter contactParam = new SqlParameter("cno", user.ContactNumber);
            cmd.Parameters.Add(contactParam);

            SqlParameter stParam = new SqlParameter("status", user.Status);
            cmd.Parameters.Add(stParam);

            SqlParameter imgParam = new SqlParameter("img", user.Image);
            cmd.Parameters.Add(imgParam);

            SqlParameter dlParam = new SqlParameter("dl", user.DriversLicense);
            cmd.Parameters.Add(dlParam);

            SqlParameter addrParam = new SqlParameter("addr", user.Address);
            cmd.Parameters.Add(addrParam);

            SqlParameter pdescParam = new SqlParameter("pdesc", user.PersonalDesc);
            cmd.Parameters.Add(pdescParam);

            try
            {
                return cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                return -1; //some error occurced
            }
            finally
            {
                sqlConn.Close();
            }

        }

        public User Authenticate(string email, string pwd)
        {
            sqlConn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM [User] WHERE Email = @email AND Password = @pwd", sqlConn);

            SqlParameter emailParam = new SqlParameter("email", email);

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
                    sqlConn.Close();

                    return null;
                }
            }
        }

        private User GetFromDatareader(SqlDataReader sqlReader)
        {
            
           User u =  new User(

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

           sqlConn.Close();

           return u;
        }
    }
}
