using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StokKartController : ControllerBase
    {
        private readonly IStokKartService _stokKartService;

        public StokKartController(IStokKartService stokKartService)
        {
            _stokKartService = stokKartService;
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _stokKartService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("getbykod")]
        public IActionResult GetByKod(Guid id)
        {
            var result = _stokKartService.GetByKod(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getbybarkod")]
        public IActionResult GetByBarkod(string barkod)
        {
            var result = _stokKartService.GetAllByBarkod(barkod);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getbyname")]
        public IActionResult GetByName(string name)
        {
            var result = _stokKartService.GetAllByName(name);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("add")]
        public IActionResult Add(StokKart stokKart)
        {
            var result = _stokKartService.Add(stokKart);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }


        [HttpPost("update")]
        public IActionResult Eupdate(StokKart stokKart)
        {
            var result = _stokKartService.Update(stokKart);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
