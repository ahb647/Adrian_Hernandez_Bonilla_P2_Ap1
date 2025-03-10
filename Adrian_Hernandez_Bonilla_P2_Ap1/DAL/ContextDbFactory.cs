using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Adrian_Hernandez_Bonilla_P1_Ap1.Context
{
    public class ContextoFactory : IDesignTimeDbContextFactory<Contexto>
    {
        public Contexto CreateDbContext(string[] args)
        {
            
            var optionsBuilder = new DbContextOptionsBuilder<Contexto>();
            optionsBuilder.UseSqlServer("workstation id=RegistroDb.mssql.somee.com;packet size=4096;user id=ahb45_SQLLogin_1;pwd=59868mjt41;data source=RegistroDb.mssql.somee.com;persist security info=False;initial catalog=RegistroDb;TrustServerCertificate=True");

            return new Contexto(optionsBuilder.Options);
        }
    }
}