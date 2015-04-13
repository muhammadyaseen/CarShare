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
		                                    //+ "CI.Image, "
                                            + " U.Name, U.Email, U.ContactNo, U.Address, U.UserID, "
		                                    + " Md.Model, Mk.Make"
                                            + " FROM Car C"
                                            //+ " JOIN CarImage CI ON CI.CarID = C.CarID"
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

        public DetailsView ConstructStitchedResult(SqlDataReader dr, bool keepOpen = false, bool getImages = true )
        {

            Car c = GetCarFromReader(dr);

            User u = GetUserFromReader(dr);

            if ( ! keepOpen ) sqlConn.Close();

            if ( getImages )  c.CarImageList = GetCarImagesFor(c.CarID);

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

            if ( sqlConn.State == ConnectionState.Closed) sqlConn.Open();

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

        public IEnumerable<Make> GetAllMakes()
        {
            List<Make> list = new List<Make>();

            sqlConn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM Make", sqlConn);

            using (SqlDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    list.Add(new Make(int.Parse(dr["MakeID"].ToString()), dr["Make"].ToString()));
                }
            }

            sqlConn.Close();

            return list;
        }

        public IEnumerable<Model> GetAllModels()
        {
            List<Model> list = new List<Model>();

            sqlConn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM Model", sqlConn);

            using (SqlDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    list.Add(new Model(int.Parse(dr["ModelID"].ToString()), dr["Model"].ToString()));
                }
            }

            sqlConn.Close();

            return list;
        }


        public IEnumerable<DetailsView> GetSearchResults(string kword, int model, int make, string location)
        {
            sqlConn.Open();

            List<DetailsView> results = new List<DetailsView>();

            string condition = " ";
            
            condition += make != 0 ? " Mk.MakeID = @mk AND ": "";

            condition += model != 0 ? " Md.ModelID = @md AND ": "";

            condition += location != null ? " C.Location = @loc AND " : "";

            condition += " 1=1 ";

            SqlCommand cmd = new SqlCommand("Select DISTINCT C.RegNo, C.MaxCapacity, C.CarID, C.Title, C.Location, C.Description,"
                                            //+ " CI.Image, "
                                            + " U.Name, U.Email, U.ContactNo, U.Address, U.UserID, "
                                            + " Md.Model, Mk.Make"
                                            + " FROM Car C"
                                            //+ " JOIN CarImage CI ON CI.CarID = C.CarID"
                                            + " JOIN [User] U ON C.OwnerID = U.UserID"
                                            + " JOIN Make Mk ON Mk.MakeID = C.MakeID"
                                            + " JOIN Model Md ON Md.ModelID = C.ModelID"
                                            + " WHERE " + condition , sqlConn);

            if (make != 0)
            {
                SqlParameter makeParam = new SqlParameter("mk", SqlDbType.Int);
                makeParam.Value = make;
                cmd.Parameters.Add(makeParam);
            }

            if (model != 0)
            {
                SqlParameter modeParam = new SqlParameter("md", SqlDbType.Int);
                modeParam.Value = model;
                cmd.Parameters.Add(modeParam);
            }

            if (location != null)
            {
                SqlParameter locParam = new SqlParameter("loc", SqlDbType.Int);
                locParam.Value = make;
                cmd.Parameters.Add(locParam);
            }

            using (SqlDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    results.Add( ConstructStitchedResult(dr, true, false) );
                }
            }

            //all the data from car,user has been fetched.. and reader has been closed ..we should now fetch CarImages

            foreach (DetailsView d in results)
            {
                d.Car.CarImageList = GetCarImagesFor(d.Car.CarID);
            }

            sqlConn.Close();

            return results;

            
        }

        public Car GetCarFromReader(SqlDataReader dr)
        {
            Car c = new Car();

            c.CarID = int.Parse(dr["CarID"].ToString());
            c.CarDesc = dr["Description"].ToString();
            c.Make = dr["Make"].ToString();
            c.Model = dr["Model"].ToString();
            c.Location = dr["Location"].ToString();
            c.RegNo = dr["RegNo"].ToString();
            c.MaxCapacity = int.Parse(dr["MaxCapacity"].ToString());
            c.Title = dr["Title"].ToString();

            return c;
        }

        public User GetUserFromReader(SqlDataReader dr)
        {
            User u = new User();

            u.Name = dr["Name"].ToString();
            u.Address = dr["Address"].ToString();
            u.ContactNumber = dr["ContactNo"].ToString();
            u.Email = dr["Email"].ToString();
            u.UserID = int.Parse(dr["UserID"].ToString());

            return u;

        }
    }
}
