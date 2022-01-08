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
    public class StokHastaController : ControllerBase
    {
        IStokHastaService _stokHastaService;

        public StokHastaController(IStokHastaService stokHastaService)
        {
            _stokHastaService = stokHastaService;
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _stokHastaService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("getbyid")]
        public IActionResult GetByKod(int id)
        {
            var result = _stokHastaService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }


        [HttpGet("getbyname")]
        public IActionResult GetByName(string name)
        {
            var result = _stokHastaService.GetAllByName(name);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("add")]
        public IActionResult Add(StokHasta stokHasta)
        {
            var result = _stokHastaService.Add(stokHasta);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }


        [HttpPost("update")]
        public IActionResult Eupdate(StokHasta stokHasta)
        {
            var result = _stokHastaService.Update(stokHasta);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
