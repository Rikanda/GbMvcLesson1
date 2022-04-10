using System;
using GbMvcLesson1.Model;
using GbMvcLesson1.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GbMvcLesson1.Controllers
{
	[Route("api/crud")]
	[ApiController]
	public class CrudController : ControllerBase
	{
		private readonly ValuesHolder _valuesHolder;

		public CrudController(ValuesHolder valuesHolder)
		{
			_valuesHolder = valuesHolder;
		}

		[HttpGet("read/list")]
		public IActionResult GetAllValues()
		{
			return Ok(_valuesHolder.GetAll());
		}

		[HttpGet("read")]
		public IActionResult GetValue([FromQuery] DateTime date)
		{
			var value = _valuesHolder.Get(date);

			if (value is null)
				return NotFound($"Значения для указанной даты не найдены");

			return Ok(value);
		}

		[HttpPost("add")]
		public IActionResult AddValue([FromBody] WeatherValue data)
		{
			if (data is null)
				return BadRequest("Отсутствуют данные для обработки");

			_valuesHolder.Add(data);
			return Ok();
		}

		[HttpPut("update")]
		public IActionResult UpdateValue([FromBody] WeatherValue data)
		{
			if (data is null)
				return BadRequest("Отсутствуют данные для обработки");

			if(!_valuesHolder.Update(data.Date, data.Temperature))
				return NotFound($"Значения для указанной даты не найдены");

			return Ok();
		}

		[HttpDelete("remove")]
		public IActionResult RemoveValue([FromQuery] DateTime date)
		{
			if (!_valuesHolder.Remove(date))
				return NotFound($"Значения для указанной даты не найдены");

			return Ok();
		}
	}
}
