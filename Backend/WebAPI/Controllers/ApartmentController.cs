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
    public class ApartmentController : ControllerBase
    {
        List<Apartment> apartmentList = new List<Apartment>();
        Result _result = new Result();
        ApartmentService apartmentService = new ApartmentService();

        [HttpGet]
        public List<Apartment> GetApartment()
        {
            return apartmentService.GetApartments();
        }
        [HttpGet("/Apartment/GetApartmentPaging")]
        public IActionResult GetApartmentPaging([FromQuery] OwnerParameters ownerParameters)
        {
            var owners = apartmentService.GetApartments()

           .Skip(ownerParameters.PageNumber)
           .Take(ownerParameters.PageSize)
           .ToList();

            return Ok(owners);
        }

        [HttpPost]
        public Result Post(Apartment apartment)
        {
            Apartment apr = apartmentService.FindApartment(apartment.Block, apartment.Type);
            //Yeni eleman listede var mı ? 
            bool apartmentCheck = (apr != null) ? true : false;

            if (apartmentCheck == false)
            {
                //Listeye yeni eleman ekleniyor.
                if (apartmentService.AddModel_(apartment) == true)
                {
                    _result.status = 1;
                    _result.Message = "Yeni daire listeye eklendi.";
                }
                else
                {
                    _result.status = 0;
                    _result.Message = "Hata, daire eklenemedi.";
                }
            }
            else
            {
                _result.status = 0;
                _result.Message = "Bu daire listede zaten var.";
            }

            return _result;
        }
        [HttpPut("{Id}")]
        public Result Update(int Id, Apartment newValue)
        {

            //Kullanıcı güncelleme işlemi yapılır.
            Apartment? _oldValue = apartmentList.Find(o => o.Id == Id);
            if (_oldValue != null)
            {
                apartmentList.Add(newValue);
                apartmentList.Remove(_oldValue);

                _result.status = 1;
                _result.Message = "Daire bilgileri başarıyla güncellendi";
            }
            else
            {
                _result.status = 0;
                _result.Message = "Bu daireyi listede bulamadık.";
            }
            return _result;
        }
        [HttpDelete("{Id}")]
        public Result Delete(int Id)
        {
            if (apartmentService.DeleteModel_(Id))
            {
                _result.status = 1;
                _result.Message = "Daire bilgileri silindi";
            }
            else
            {
                _result.status = 0;
                _result.Message = "Daire bilgileri zaten silinmiş.";
            }
            return _result;
        }
    }
}
