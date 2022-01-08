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
    public class StokDepoController : ControllerBase
    {
        IStokDepoService _stokDepoService;

        public StokDepoController(IStokDepoService stokDepoService)
        {
            _stokDepoService = stokDepoService;
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _stokDepoService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getalltalepaktif")]
        public IActionResult GetAllTalepAktif()
        {
            var result = _stokDepoService.GetAllTalepAktif();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getbykod")]
        public IActionResult GetByKod(int depoKod)
        {
            var result = _stokDepoService.GetByKod(depoKod);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

       

        [HttpGet("getbyname")]
        public IActionResult GetByName(string name)
        {
            var result = _stokDepoService.GetAllByName(name);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("add")]
        public IActionResult Add(StokDepo stokDepo)
        {
            var result = _stokDepoService.Add(stokDepo);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }


        [HttpPost("update")]
        public IActionResult Eupdate(StokDepo stokDepo)
        {
            var result = _stokDepoService.Update(stokDepo);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
