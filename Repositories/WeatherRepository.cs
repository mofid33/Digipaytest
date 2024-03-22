using System;
using Microsoft.EntityFrameworkCore;
using System.Net;
using DigiPayTest.DbContexts;
using System.Xml.Linq;
using Newtonsoft.Json.Linq;
using DigiPayTest.Interfaces.Repositories;
using DigiPayTest.Entities;

namespace DigiPayTest.Repositories
{
    public class WeatherRepository : IWeather
    {
        private readonly WeatherDbContext _context;
        //private readonly IWeather _weatherRepository;

        public WeatherRepository(WeatherDbContext context)
        {

            _context = context ?? throw new ArgumentException(nameof(context));
        }

        public async Task<string> GetDataApi()
        {
            try
            {


                Random rnd = new Random();
                int num = rnd.Next(1000, 10000);
                HttpWebRequest request2 = (HttpWebRequest)WebRequest.Create("https://api.open-meteo.com/v1/forecast?latitude=52.52&longitude=13.41&hourly=temperature_2m");
                request2.ContentType = "application/json; charset=utf-8";
                request2.Method = "GET";
                request2.ServerCertificateValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

                byte[] bytes2;

                var httpResponse = request2.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    JObject joResponse = JObject.Parse(result);
                    bool insert =await InsertInto(joResponse);
                    if (insert == true)
                    {
                        return "true";

                    }
                    else
                    {
                        return null;

                    }

                }

                //_context.SaveChanges();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<bool> InsertInto(JObject joResponse)
        {
            try
            {
                WeatherTbl weather = new WeatherTbl();
                weather.Latitude = Convert.ToDouble(joResponse["latitude"]);
                weather.Longitude = Convert.ToDouble(joResponse["longitude"]);
                weather.Generationtimems = Convert.ToDecimal(joResponse["generationtime_ms"]);
                weather.Utcoffsetseconds = Convert.ToInt32(joResponse["utc_offset_seconds"]);
                weather.Timezone = joResponse["timezone"].ToString();
                weather.Timezoneabbreviation = joResponse["timezone_abbreviation"].ToString();
                weather.Elevation = Convert.ToInt32(joResponse["elevation"]);
                JObject hourlyunits = JObject.Parse(joResponse["hourly_units"].ToString());
                HourlyUnitsTbl _hourlyunits = new HourlyUnitsTbl();
                _hourlyunits.Temperature2m = hourlyunits["temperature_2m"].ToString();
                _hourlyunits.Time = hourlyunits["time"].ToString();
                await _context.HourlyUnitsTbl.AddAsync(_hourlyunits);
                await _context.SaveChangesAsync();
                HourlyTbl _hourly = new HourlyTbl();

                await _context.HourlyTbl.AddAsync(_hourly);
                await _context.SaveChangesAsync();
                weather.HourlyId = _hourly.HourlyId;
                weather.HourlyUnitsId = _hourlyunits.HourlyUnitsId;
                JObject hourly = JObject.Parse(joResponse["hourly"].ToString());
                await _context.WeatherTbl.AddAsync(weather);
                await _context.SaveChangesAsync();

                foreach (var item in hourly["time"])
                {
                    TimeTbl _time = new TimeTbl();
                    _time.HourlyId = _hourly.HourlyId;
                    _time.Time = Convert.ToDateTime(item);
                    await _context.TimeTbl.AddAsync(_time);

                }
                foreach (var item in hourly["temperature_2m"])
                {
                    Temperature2mTbl _time = new Temperature2mTbl();
                    _time.HourlyId = _hourly.HourlyId;
                    _time.Temperature2m = Convert.ToDecimal(item);
                    await _context.Temperature2mTbl.AddAsync(_time);

                }
                await _context.SaveChangesAsync();
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}

