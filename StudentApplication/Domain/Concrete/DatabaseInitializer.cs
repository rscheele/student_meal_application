using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Concrete
{
    // DropCreateDatabaseIfModelChanges VS DropCreateDatabaseAlways
    public class DatabaseInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<EFDBContextStudent>
    {
        protected override void Seed(EFDBContextStudent context)
        {
        }
    }
}
