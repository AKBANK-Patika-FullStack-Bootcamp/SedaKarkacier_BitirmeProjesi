using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Model;
using Entities;

namespace Services
{
    public class MessageService
    {
        private DBContext _context = new DBContext();
        Logger logger = new Logger();

        public bool AddModel_M(Message _message)
        {
            try
            {
                _context.Message.Add(_message);
                _context.SaveChanges();
                return true;
            }
            catch (Exception exc)
            {
                logger.createLog("HATA " + exc.Message);
                return false;
            }
        }
        public bool DeleteModel_M(int SenderId, int RecieverId, int Id)
        {
            try
            {
                _context.Message.Remove(FindMessage(SenderId, RecieverId, Id));
                _context.SaveChanges();
                return true;
            }
            catch (Exception exc)
            {
                logger.createLog("HATA " + exc.Message);
                return false;
            }
        }

        public List<Message> GetMessages()
        {
            List<Message> message = new List<Message>();
            message = _context.Message.ToList();

            return message;
        }
        public Message FindMessage(int SenderId = 0, int RecieverId = 0, int Id = 0)
        {
            Message? message = new Message();
            if (SenderId > 0 && RecieverId > 0)
                message = _context.Message.FirstOrDefault(m => m.SenderId == SenderId && m.RecieverId == RecieverId);
            else if (Id > 0)
            {
                message = _context.Message.FirstOrDefault(m => m.Id == Id);
            }
            return message;
        }
    }
}
