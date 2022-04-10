using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using GbMvcLesson1.Model;

namespace GbMvcLesson1.Services
{
	/// <summary>
	/// Хранилище данных о температурных показателх
	/// </summary>
	public class ValuesHolder
	{
		private ConcurrentDictionary<DateTime, WeatherValue> data;

		public ValuesHolder()
		{
			data = new ConcurrentDictionary<DateTime, WeatherValue>();
		}

		/// <summary>
		/// Получить коллекцую всех данных из хранилища
		/// </summary>
		/// <returns></returns>
		public ICollection<WeatherValue> GetAll() => data.Values;

		public WeatherValue Get(DateTime date) =>
			data.TryGetValue(date, out var value) ? value : null;

		/// <summary>
		/// Добавить данные в хранилище
		/// </summary>
		/// <param name="value"></param>
		/// <exception cref="ArgumentNullException"></exception>
		public void Add(WeatherValue value)
		{
			if (value == null) throw new ArgumentNullException(nameof(value));

			data.TryAdd(value.Date, value);
		}
		
		/// <summary>
		/// Обновит данные в хранилище
		/// </summary>
		/// <param name="date">Для какой даты обновить данные о температуре</param>
		/// <param name="newValue">Новое значение температуры</param>
		/// <returns>false - если знаение в указанное время не найдено</returns>
		public bool Update(DateTime date, float newValue)
		{
			if (!data.TryGetValue(date, out var item))
				return false;

			item.Temperature = newValue;
			return true;
		}

		/// <summary>
		/// Удалить значение для указанного времени
		/// </summary>
		/// <param name="date"></param>
		/// <returns></returns>
		public bool Remove(DateTime date)
		{
			return data.TryRemove(date, out _);
		}
	}
}
