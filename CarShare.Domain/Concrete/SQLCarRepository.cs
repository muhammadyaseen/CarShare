using CarShare.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarShare.Domain.Entities;
using System.Data.SqlClient;
using System.Data;
using CarShare.Domain.ViewEntities;

namespace CarShare.Domain.Concrete
{
    public class SQLCarRepository : ICarRepository
    {
        SqlConnection sqlConn = new SqlConnection("Data Source=(Local);Initial Catalog=CarShare;Integrated Security=true");


        public Car GetCarByID(int carID)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Car> GetCarsOwnedBy(int userID)
        {
            throw new NotImplementedException();
        }

        public DetailsView GetCarAndAssociatedDetails(int carID)
        {
            sqlConn.Open();

            SqlCommand cmd = new SqlCommand("Select	C.RegNo, C.MaxCapacity, C.CarID, C.Title, C.Location, C.Description,"
		                                    + "CI.Image, U.Name,"
                                            + "U.Email, U.ContactNo, U.Address,"
		                                    + "Md.Model, Mk.Make"
                                            + " FROM Car C"
                                            + " JOIN CarImage CI ON CI.CarID = C.CarID"
                                            + " JOIN [User] U ON C.OwnerID = U.UserID"
                                            + " JOIN Make Mk ON Mk.MakeID = C.MakeID"
                                            + " JOIN Model Md ON Md.ModelID = C.ModelID"
                                            + " WHERE C.CarID = @carid", sqlConn);

            SqlParameter idParam = new SqlParameter("carid", SqlDbType.Int);
            idParam.Value = carID;

            cmd.Parameters.Add(idParam);

            using (SqlDataReader dr = cmd.ExecuteReader())
            {
                if (dr.Read())
                {
                    return ConstructStitchedResult(dr);
                }
                else
                {
                    sqlConn.Close();

                    return null;
                }
            }
        }

        public DetailsView ConstructStitchedResult(SqlDataReader dr)
        {
            //todo : refactor

            Car c = new Car();

            c.CarID = int.Parse(dr["CarID"].ToString());
            c.CarDesc = dr["Description"].ToString();
            c.Make = dr["Make"].ToString();
            c.Model = dr["Model"].ToString();
            c.Location = dr["Location"].ToString();
            c.RegNo = dr["RegNo"].ToString();
            c.MaxCapacity = int.Parse(dr["MaxCapacity"].ToString());
            c.Title = dr["Title"].ToString();

            User u = new User();

            u.Name = dr["Name"].ToString();
            u.Address = dr["Address"].ToString();
            u.ContactNumber = dr["ContactNo"].ToString();
            u.Email = dr["Email"].ToString();

            sqlConn.Close();

            c.CarImageList = GetCarImagesFor(c.CarID);

            return new DetailsView { Car = c, User = u };

        }

        public bool Save(Car c)
        {
            return false;
        }

        public List<CarImage> GetCarImagesFor(int carID)
        {
            List<CarImage> list = new List<CarImage>();
            
            //todo : implement sql code

            sqlConn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM CarImage WHERE CarID = @carid", sqlConn);

            SqlParameter idParam = new SqlParameter("carid", SqlDbType.Int);
            idParam.Value = carID;

            cmd.Parameters.Add(idParam);


            using (SqlDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    list.Add(new CarImage( int.Parse(dr["CarID"].ToString()), int.Parse(dr["CarImageID"].ToString()), dr["Image"].ToString()));
                }
            }

            return list;
        }

    }
}
