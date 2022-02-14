using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Model;
using Entities;


namespace Services
{
    public class ApartmentService
    {
        private DBContext _context = new DBContext();
        Logger logger = new Logger();
        public bool AddModel_(Apartment _apartment)
        {
            try
            {
                _context.Apartment.Add(_apartment);
                _context.SaveChanges();
                return true;
            }
            catch (Exception exc)
            {
                logger.createLog("HATA " + exc.Message);
                return false;
            }
        }
        public bool DeleteModel_(int Id)
        {
            try
            {
                _context.Apartment.Remove(FindApartment("", "", Id));
                _context.SaveChanges();
                return true;
            }
            catch (Exception exc)
            {
                logger.createLog("HATA " + exc.Message);
                return false;
            }
        }

        public List<Apartment> GetApartments()
        {
            List<Apartment> apartment = new List<Apartment>();
            apartment = _context.Apartment.ToList();

            return apartment;
        }
        public Apartment FindApartment(string Block = "", string Type = "", int Id = 0)
        {
            Apartment? apartment = new Apartment();
            if (!string.IsNullOrEmpty(Block) && !string.IsNullOrEmpty(Type))
                apartment = _context.Apartment.FirstOrDefault(m => m.Block == Block && m.Type == Type);
            else if (Id > 0)
            {
                apartment = _context.Apartment.FirstOrDefault(m => m.Id == Id);
            }
            return apartment;
        }
    }
}
