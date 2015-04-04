using CarShare.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarShare.Domain.Entities;
using System.Data.SqlClient;

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

        public bool Save(Car c)
        {
            return false;
        }

    }
}
