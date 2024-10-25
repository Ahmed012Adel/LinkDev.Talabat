using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Infrastructrure.Persistence.Common
{
    [AttributeUsage(AttributeTargets.Class)]
    public class DbContextTypeAttributr : Attribute
    {
        public Type DbcontextType { get; }

        public DbContextTypeAttributr(Type dbcontextType)
        {
            DbcontextType = dbcontextType;
        }
    }
}
