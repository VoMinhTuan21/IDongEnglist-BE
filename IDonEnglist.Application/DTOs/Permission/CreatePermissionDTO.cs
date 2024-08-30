﻿namespace IDonEnglist.Application.DTOs.Permission
{
    public class CreatePermissionDTO : IPermissionDTO
    {
        public string Name { get; set; }
        public string? Code { get; set; }
        public string? Description { get; set; }
    }
}