﻿namespace MapperApp.Models.DTOs.Incoming
{
    public class CreateDriverDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int DriverNumber { get; set; }
        public int WorldChampionships { get; set; }
    }
}