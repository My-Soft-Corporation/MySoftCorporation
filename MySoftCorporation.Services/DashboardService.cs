using MySoft.Employee.Entities;
using MySoftCorporation.Data.Entities;
using System.Collections.Generic;
using System.Linq;

namespace MySoftCorporation.Services
{
    public class DashboardService
    {
        private readonly MySoftCorporationDbContext _context;
        public DashboardService()
        {
            _context = new MySoftCorporationDbContext();
        }
        
        public bool SavePicture(Picture picture)
        {
            _context.Pictures.Add(picture);
            return _context.SaveChanges() > 0;
        }
        public IEnumerable<Picture> GetPictures()
        {
            return _context.Pictures.AsEnumerable();
        }
        public IEnumerable<Picture> GetPictures(List<int> pictureIDs)
        {
            return pictureIDs.Select(x => _context.Pictures.Find(x)).ToList();
        }
    }
}
