using LinkDev.Talabat.Core.Domain.Entities.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Domain.Specifications.EmployeeSpecification
{
    public class EmployeeWithDepartmentSpecification :BaseSpecificatins<Employee ,int>
    {
        public EmployeeWithDepartmentSpecification(int? DepartmentId):base
            (
            
            E=>E.DepartmentId == DepartmentId
            )
        {
            Includes.Add(E => E.department!);
        }

        public EmployeeWithDepartmentSpecification(int id) : base(id) 
        {
            Includes.Add(E => E.department!);

        }
    }
}
