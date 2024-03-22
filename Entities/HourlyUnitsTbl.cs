using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigiPayTest.Entities;

public partial class HourlyUnitsTbl
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int HourlyUnitsId { get; set; }

    public string Time { get; set; } = null!;

    public string Temperature2m { get; set; } = null!;

    public virtual ICollection<WeatherTbl> WeatherTbls { get; set; } = new List<WeatherTbl>();
}
