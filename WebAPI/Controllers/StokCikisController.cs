using Business.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StokCikisController : ControllerBase
    {

        IStokCikisService _stokCikisService;
        //IStokCikisDetayService _stokCikisDetayService;

        public StokCikisController(IStokCikisService stokCikisService, IStokCikisDetayService stokCikisDetayService)
        {
            _stokCikisService = stokCikisService;
            //_stokCikisDetayService = stokCikisDetayService;
        }

        [HttpGet("getallcikis")]
        public IActionResult GetAll()
        {
            var result = _stokCikisService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getcikisbyislemno")] // Şuan kulanılmıyor
        public IActionResult GetByIslemNo(int islemNo)
        {
            var result = _stokCikisService.GetByIslemNo(islemNo);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getallcikisbyislemtip")] // Şuan kulanılmıyor
        public IActionResult GetAllByIslemTip(short id)
        {
            var result = _stokCikisService.GetAllByIslemTip(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getallcikisbytarih")]
        public IActionResult GetAllByTarih(DateTime min, DateTime max,short? fkDepo)
        {
            fkDepo = fkDepo != 0 ? fkDepo : null;
            var result = _stokCikisService.GetAllByTarih(min, max, fkDepo);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        
        [HttpGet("getonaylanacakbytarih")]
        public IActionResult GetOnaylanacak(DateTime min, DateTime max,short istekDepo)
        {
            var result = _stokCikisService.GetOnaylanacak(min, max, istekDepo);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getallbyfkdepo")]// isteyen true , istek yapan false
        public IActionResult GetByFkDepo(short fkDepo, bool isteyen)
        {
            var result = _stokCikisService.GetByFkDepo(fkDepo, isteyen);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getallbylogin")] // Şuan kulanılmıyor
        public IActionResult GetByFkLogin(int fkLogin)
        {
            var result = _stokCikisService.GetByFkLogin(fkLogin);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getallbyonaylayan")]// Şuan kulanılmıyor
        public IActionResult GetByFkOnaylayan(int fkOnaylayan)
        {
            var result = _stokCikisService.GetByFkOnaylayan(fkOnaylayan);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
       
        [HttpGet("getcikisdetay")]
        public IActionResult GetCikisDetay(int islemNo)
        {
            var result = _stokCikisService.GetCikisDetay(islemNo);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPost("addstokcikis")]
        public IActionResult AddStokCikis(AddStokCikisDto addStokCikis)
        {
            var result = _stokCikisService.Add(addStokCikis.stokCikis, addStokCikis.stokCikisDetay);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest();
        } 
        [HttpPost("onayilaccikis")]
        public IActionResult IlacCikisOnayla(OnayStokIlacCikisDto addStokCikis)
        {
            var result = _stokCikisService.IlacCikisOnayla(addStokCikis.stokCikis, addStokCikis.stokIlacCikis);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest();
        }

        [HttpPost("onaystokcikis")]
        public IActionResult OnayStokCikis(AddStokCikisDto addStokCikis)
        {
            StokCikis stokCikis = _stokCikisService.GetByIslemNo(addStokCikis.stokCikis.IslemNo).Data;
            stokCikis.Fk_OnayLogin = addStokCikis.stokCikis.Fk_OnayLogin;
            if (stokCikis != null)
            {
                var result = _stokCikisService.Onayla(stokCikis, addStokCikis.stokCikisDetay);
                if (result.Success)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
           
            return BadRequest("Hata");
        }


    }
}
