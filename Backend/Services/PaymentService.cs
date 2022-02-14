using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Model;
using Entities;

namespace Services
{
    public class PaymentService
    {
        private DBContext _context = new DBContext();
        Logger logger = new Logger();
        public bool AddModel_P(Payment _payment)
        {
            try
            {
                _context.Payment.Add(_payment);
                _context.SaveChanges();
                return true;
            }
            catch (Exception exc)
            {
                logger.createLog("HATA " + exc.Message);
                return false;
            }
        }
        public bool DeleteModel_P(int PayerId, int Id)
        {
            try
            {
                _context.Payment.Remove(FindPayment("", PayerId, Id));
                _context.SaveChanges();
                return true;
            }
            catch (Exception exc)
            {
                logger.createLog("HATA " + exc.Message);
                return false;
            }
        }

        public List<Payment> GetPayments()
        {
            List<Payment> payment = new List<Payment>();
            payment = _context.Payment.ToList();

            return payment;
        }
        public Payment FindPayment(string Type = "", int PayerId = 0, int Id = 0)
        {
            Payment? payment = new Payment();
            if (!string.IsNullOrEmpty(Type) && PayerId > 0)
                payment = _context.Payment.FirstOrDefault(m => m.Type == Type && m.PayerId == PayerId);
            else if (Id > 0)
            {
                payment = _context.Payment.FirstOrDefault(m => m.Id == Id);
            }
            return payment;
        }
    }
}
