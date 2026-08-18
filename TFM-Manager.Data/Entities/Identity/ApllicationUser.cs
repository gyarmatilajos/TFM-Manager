using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace TFM_Manager.Data.Entities.Identity
{
    public class ApllicationUser : IdentityUser<int>, IEntityTypeConfiguration<ApllicationUser>
    {
        public void Configure(EntityTypeBuilder<ApllicationUser> builder)
        {
            throw new NotImplementedException();
        }
    }
}
