using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigiPayTest.Entities;

public partial class HourlyTbl
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int HourlyId { get; set; }

    public virtual ICollection<Temperature2mTbl> Temperature2mTbls { get; set; } = new List<Temperature2mTbl>();

    public virtual ICollection<TimeTbl> TimeTbls { get; set; } = new List<TimeTbl>();

    public virtual ICollection<WeatherTbl> WeatherTbls { get; set; } = new List<WeatherTbl>();
}
