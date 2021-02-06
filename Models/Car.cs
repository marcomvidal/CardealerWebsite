using System;

namespace Cars.Models
{
    public class Car
    {
        public Guid Id { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public DateTime AssemblyDate { get; set; }
    }
}