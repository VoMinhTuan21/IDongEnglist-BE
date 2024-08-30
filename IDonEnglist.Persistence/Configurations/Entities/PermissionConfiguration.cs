using IDonEnglist.Application.Constants;
using IDonEnglist.Application.Utils;
using IDonEnglist.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IDonEnglist.Persistence.Configurations.Entities
{
    public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.HasData(
                new Permission
                {
                    Id = 5,
                    Name = "Category",
                    Code = "category"
                },
                new Permission
                {
                    Id = 1,
                    Name = PermissionTypes.CreateCategory,
                    Code = SlugGenerator.GenerateSlug(PermissionTypes.CreateCategory),
                    ParentId = 5
                },
                new Permission
                {
                    Id = 2,
                    Name = PermissionTypes.UpdateCategory,
                    Code = SlugGenerator.GenerateSlug(PermissionTypes.UpdateCategory),
                    ParentId = 5,
                },
                new Permission
                {
                    Id = 3,
                    Name = PermissionTypes.DeleteCategory,
                    Code = SlugGenerator.GenerateSlug(PermissionTypes.DeleteCategory),
                    ParentId = 5
                },
                new Permission
                {
                    Id = 4,
                    Name = PermissionTypes.ReadCategory,
                    Code = SlugGenerator.GenerateSlug(PermissionTypes.ReadCategory),
                    ParentId = 5
                }
            );
        }
    }
}
