using DAL.Model;
using Entities;
using Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class MessageController : ControllerBase
    {
        List<Message> messageList = new List<Message>();
        Result _result = new Result();
        MessageService messageService = new MessageService();

        [HttpGet]
        public List<Message> GetMessage()
        {
            return messageService.GetMessages();
        }

        [HttpGet("/Message/GetMessagePaging")]
        public IActionResult GetMessagePaging([FromQuery] OwnerParameters ownerParameters)
        {
            var owners = messageService.GetMessages()

           .Skip(ownerParameters.PageNumber)
           .Take(ownerParameters.PageSize)
           .ToList();

            return Ok(owners);
        }

        [HttpPost]
        public Result Post(Message message)
        {
            Message msg = messageService.FindMessage(message.SenderId, message.RecieverId);
            //Yeni mesaj listede var mı ? 
            bool messageCheck = (msg != null) ? true : false;

            if (messageCheck == false)
            {
                //Listeye yeni mesaj ekleniyor.
                if (messageService.AddModel_M(message) == true)
                {
                    _result.status = 1;
                    _result.Message = "Yeni mesaj listeye eklendi.";
                }
                else
                {
                    _result.status = 0;
                    _result.Message = "Hata, mesaj eklenemedi.";
                }
            }
            else
            {
                _result.status = 0;
                _result.Message = "Bu mesaj listede zaten var.";
            }

            return _result;
        }
        [HttpPut("{Id}")]
        public Result Update(int Id, Message newValue)
        {

            //Mesaj güncelleme işlemi yapılır.
            Message? _oldValue = messageList.Find(o => o.Id == Id);
            if (_oldValue != null)
            {
                messageList.Add(newValue);
                messageList.Remove(_oldValue);

                _result.status = 1;
                _result.Message = "Mesaj bilgileri başarıyla güncellendi";
            }
            else
            {
                _result.status = 0;
                _result.Message = "Bu mesajı listede bulamadık.";
            }
            return _result;
        }
        [HttpDelete("{Id}")]
        public Result Delete(int SenderId, int RecieverId, int Id)
        {
            if (messageService.DeleteModel_M(SenderId, RecieverId, Id))
            {
                _result.status = 1;
                _result.Message = "Mesaj bilgileri silindi";
            }
            else
            {
                _result.status = 0;
                _result.Message = "Mesaj bilgileri zaten silinmiş.";
            }
            return _result;
        }
    }
}

