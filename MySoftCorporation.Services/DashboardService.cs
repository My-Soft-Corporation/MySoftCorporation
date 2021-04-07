using MySoft.Employee.Entities;
using MySoftCorporation.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySoftCorporation.Services
{
    public class DashboardService
    {
        public bool SavePicture(Picture picture)
        {
            MySoftCorporationDbContext mySoftCorporationDbContext = new MySoftCorporationDbContext();
            mySoftCorporationDbContext.Pictures.Add(picture);
            return  mySoftCorporationDbContext.SaveChanges() > 0;
        }
        public IEnumerable<Picture> GetPictures()
        {
            MySoftCorporationDbContext mySoftCorporationDbContext = new MySoftCorporationDbContext();
            return mySoftCorporationDbContext.Pictures.AsEnumerable();
        }
        public IEnumerable<Picture> GetPictures(List<int> pictureIDs)
        {
            MySoftCorporationDbContext mySoftCorporationDbContext = new MySoftCorporationDbContext();
            return pictureIDs.Select(x => mySoftCorporationDbContext.Pictures.Find(x)).ToList();
        }
    }
}
