﻿using System;

namespace RiseRestApi.Models
{
    public class RiseProgramBase
    {
        public string ProgramName { get; set; }
        public int AdminPersonId { get; set; }
        public int SchoolId { get; set; }
        public bool IsRemoved { get; set; }
    }
}
