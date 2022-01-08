using Business.Abstract;
using Entities.Concrete;
using Entities.DTOs;
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
    public class StokEnvanterController : ControllerBase
    {
        private readonly IStokEnvanterService  _stokEnvanterService;
        private readonly IStokFaturaService _stokFaturaService;

        public StokEnvanterController(IStokEnvanterService stokEnvanterService, IStokFaturaService stokFaturaService)
        {
            _stokEnvanterService = stokEnvanterService;
            _stokFaturaService = stokFaturaService;
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _stokEnvanterService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("add")]
        public IActionResult Add(AddStokEnvanterDto stokEnvanter)
        {
            var result = _stokEnvanterService.Add(stokEnvanter);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getstokbyname")]
        public IActionResult GetAllStokByName(string name,short? fkDepo=null)
        {
            var result = _stokEnvanterService.GetAllStokByName(name,fkDepo);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }


        [HttpGet("getstokbybarkod")]
        public IActionResult GetAllStokByBarkod(string barkod, short? fkDepo = null)
        {
            var result = _stokEnvanterService.GetAllStokByBarkod(barkod);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        

        [HttpGet("getstokbyskt")]
        public IActionResult GetAllBySonKullanma(DateTime min, DateTime max, short? fkDepo = null)
        {
            var result = _stokEnvanterService.GetAllBySonKullanma(min,max,fkDepo);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }


        [HttpPost("update")]
        public IActionResult Update(StokEnvanter stokEnvanter)
        {
            var result = _stokEnvanterService.OnylUpdate(stokEnvanter);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getfaturabytarih")]
        public IActionResult GetAllFaturaByTarih(DateTime min, DateTime max, short? fkDepo = null)
        {
            var result = _stokFaturaService.GetAllByTarih(min, max, fkDepo);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
