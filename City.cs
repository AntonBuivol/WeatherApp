﻿using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp
{
    [Table("Cities")]
    public class City
    {
        [PrimaryKey, AutoIncrement, Column("id")]
        public int Id { get; set; }

        [Unique, Column("cityName")]
        public string CityName { get; set; }
    }
}
