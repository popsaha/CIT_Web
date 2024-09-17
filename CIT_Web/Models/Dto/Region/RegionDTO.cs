﻿using System.ComponentModel.DataAnnotations;

namespace CIT_Web.Models.Dto.Region
{
    public class RegionDTO
    {
        public int RegionID { get; set; }
        [Required]
        [MaxLength(30)]
        public string? RegionName { get; set; }
        [Required]
        public string? DataSource { get; set; }
    }
}
