using System;
namespace DigiPayTest.Interfaces.Repositories
{
	public interface IWeather
	{
        Task<string> GetDataApi();

    }
}

