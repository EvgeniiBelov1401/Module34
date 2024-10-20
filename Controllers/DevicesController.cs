﻿using AutoMapper;
using HomeApi.Configuration;
using HomeApi.Contracts.Devices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace HomeApi.Controllers
{
    public class DevicesController : ControllerBase
    {
        private IOptions<HomeOptions> _options;
        private IMapper _mapper;

        public DevicesController(IOptions<HomeOptions> options, IMapper mapper)
        {
            _options = options;
            _mapper = mapper;
        }
        [HttpGet]
        [Route("")]
        public IActionResult Get()
        {
            return StatusCode(200, "Устройства отсутствуют");
        }


        [HttpPost]
        [Route("Add")]
        public IActionResult Add(
           [FromBody] // Атрибут, указывающий, откуда брать значение объекта
           AddDeviceRequest request // Объект запроса
         )
        {
            if (request.CurrentVolts < 120)
            {
                //return StatusCode(403, $"Устройства с напряжением меньше 120 вольт не поддерживаются!");
                ModelState.AddModelError("currentVolts", "Устройства с напряжением меньше 120 вольт не поддерживаются!");
                return BadRequest(ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return StatusCode(200, $"Устройство {request.Name} добавлено!");
        }
    }
}
