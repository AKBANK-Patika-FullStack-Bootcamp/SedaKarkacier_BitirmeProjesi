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
    public class PaymentController : ControllerBase
    {
        List<Payment> paymentList = new List<Payment>();
        Result _result = new Result();
        PaymentService paymentService = new PaymentService();

        [HttpGet]
        public List<Payment> GetPayment()
        {
            return paymentService.GetPayments();
        }
        [HttpGet("/Payment/GetPaymentPaging")]
        public IActionResult GetPaymentPaging([FromQuery] OwnerParameters ownerParameters)
        {
            var owners = paymentService.GetPayments()

           .Skip(ownerParameters.PageNumber)
           .Take(ownerParameters.PageSize)
           .ToList();

            return Ok(owners);
        }

        [HttpPost]
        public Result Post(Payment payment)
        {
            Payment pmt = paymentService.FindPayment(payment.Type, payment.PayerId);
            //Yeni eleman listede var mı ? 
            bool PaymentCheck = (pmt != null) ? true : false;

            if (PaymentCheck == false)
            {
                //Listeye yeni eleman ekleniyor.
                if (paymentService.AddModel_P(payment) == true)
                {
                    _result.status = 1;
                    _result.Message = "Yeni ödeme listeye eklendi.";
                }
                else
                {
                    _result.status = 0;
                    _result.Message = "Hata, ödeme eklenemedi.";
                }
            }
            else
            {
                _result.status = 0;
                _result.Message = "Bu ödeme listede zaten var.";
            }

            return _result;
        }
        [HttpPut("{Id}")]
        public Result Update(int Id, Payment newValue)
        {

            //Kullanıcı güncelleme işlemi yapılır.
            Payment? _oldValue = paymentList.Find(o => o.Id == Id);
            if (_oldValue != null)
            {
                paymentList.Add(newValue);
                paymentList.Remove(_oldValue);

                _result.status = 1;
                _result.Message = "Ödeme bilgileri başarıyla güncellendi";
            }
            else
            {
                _result.status = 0;
                _result.Message = "Bu ödeme listede bulamadık.";
            }
            return _result;
        }
        [HttpDelete("{Id}")]
        public Result Delete(int PayerId,int Id)
        {
            if (paymentService.DeleteModel_P(PayerId, Id))
            {
                _result.status = 1;
                _result.Message = "Ödeme bilgileri silindi";
            }
            else
            {
                _result.status = 0;
                _result.Message = "Ödeme bilgileri zaten silinmiş.";
            }
            return _result;
        }
    }
}
