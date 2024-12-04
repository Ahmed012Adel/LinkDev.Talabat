using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Infrastructrure.Persistence.Common
{
    [AttributeUsage(AttributeTargets.Class)]
    public class DbContextTypeAttribute : Attribute
    {
        public Type DbcontextType { get; }

        public DbContextTypeAttribute(Type dbcontextType)
        {
            DbcontextType = dbcontextType;
        }
    }
}
