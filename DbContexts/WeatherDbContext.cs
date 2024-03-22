using System;
using DigiPayTest.Entities;
using Microsoft.EntityFrameworkCore;

namespace DigiPayTest.DbContexts
{
	public class WeatherDbContext: DbContext
    {
		public WeatherDbContext(DbContextOptions<WeatherDbContext> options)
            : base(options)
        {

        }
        public DbSet<WeatherTbl> WeatherTbl { get; set; } = null!;
        public DbSet<HourlyTbl> HourlyTbl { get; set; } = null!;
        public DbSet<HourlyUnitsTbl> HourlyUnitsTbl { get; set; } = null!;
        public DbSet<Temperature2mTbl> Temperature2mTbl { get; set; } = null!;
        public DbSet<TimeTbl> TimeTbl { get; set; } = null!;

    }
}

