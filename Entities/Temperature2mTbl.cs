using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigiPayTest.Entities;

public partial class Temperature2mTbl
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Temperature2mId { get; set; }

    public decimal Temperature2m { get; set; }

    public int HourlyId { get; set; }

    public virtual HourlyTbl Hourly { get; set; } = null!;
}
