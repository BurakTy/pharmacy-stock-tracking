using Business.Abstract;
using Core.Entities.Concrete;
using Core.Utilities.Results;
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
    public class SabitlerController : ControllerBase
    {

        private IStokBirimService _stokBirimService;
        private IStokRafService _stokRafService;
        private IPdfService _pdfService;

        public SabitlerController(IStokBirimService stokBirimService, IStokRafService stokRafService, IPdfService pdfService)
        {
            _stokBirimService = stokBirimService;
            _stokRafService = stokRafService;
            _pdfService = pdfService;

        }


        [HttpGet("pdf")]
        public IActionResult GetPdf(int id, int tur)
        {
            IDataResult<byte[]> result = new ErrorDataResult<byte[]>();
            switch (tur)
            {
                case 1: 
                    result = _pdfService.StokCikisPdf((short)id);
                    break;
                case 2:
                    result = _pdfService.FaturaPdf(id);
                    break;
                case 3:
                    result = _pdfService.BakanlikFormatFatura(id);
                    break;
            }
      
            if (result.Success)
            {
                System.Net.Mime.ContentDisposition cd = new System.Net.Mime.ContentDisposition
                {
                    FileName = Guid.NewGuid().ToString(),
                    Inline = false
                };
                Response.Headers.Add("Content-Disposition", cd.ToString());
               // return File(result.Data, "application/octet-stream");
                return File(result.Data, "application/pdf");
            }
            return BadRequest(result);
        }

        [HttpGet("getalldeporaf")]
        public IActionResult GetAllBirim(short fkDepo)
        {
            var result = _stokRafService.GetAll(fkDepo);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("addraf")]
        public IActionResult Add(StokRaf raf)
        {
            var result = _stokRafService.Add(raf);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getallbirim")]
        public IActionResult GeteAllBirim()
        {
            var result = _stokBirimService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("addbirim")]
        public IActionResult Add(StokBirim birim)
        {
            var result = _stokBirimService.Add(birim);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("updatebirim")]
        public IActionResult Update(StokBirim birim)
        {
            var result = _stokBirimService.Update(birim);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }


    }
}
