﻿namespace IDonEnglist.Application.DTOs.TestPart
{
    public class CreateTestPartDTO : ITestPartDTO
    {
        public string Name { get; set; }
        public int Order { get; set; }
        public int Duration { get; set; }
        public int Questions { get; set; }
    }
}
