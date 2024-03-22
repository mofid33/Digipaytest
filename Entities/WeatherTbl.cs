using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigiPayTest.Entities;

public partial class WeatherTbl
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int WeatherId { get; set; }

    public double Latitude { get; set; }

    public double Longitude { get; set; }

    public int Utcoffsetseconds { get; set; }

    public string Timezone { get; set; } = null!;

    public string Timezoneabbreviation { get; set; } = null!;

    public float Elevation { get; set; }

    public int HourlyUnitsId { get; set; }

    public int HourlyId { get; set; }

    public decimal? Generationtimems { get; set; }

    public virtual HourlyTbl Hourly { get; set; } = null!;

    public virtual HourlyUnitsTbl HourlyUnits { get; set; } = null!;
}
