using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace second.Data
{
    public class ApplicatioDbContext: IdentityDbContext
    {
        public ApplicatioDbContext(DbContextOptions<ApplicatioDbContext> opt): base(opt)
        { }
    }
}
