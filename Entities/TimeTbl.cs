using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigiPayTest.Entities;

public partial class TimeTbl
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int TimeId { get; set; }

    public DateTime Time { get; set; }

    public int HourlyId { get; set; }

    public virtual HourlyTbl Hourly { get; set; } = null!;
}
