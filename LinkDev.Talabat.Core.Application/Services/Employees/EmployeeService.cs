using AutoMapper;
using LinkDev.Talabat.Core.Application.Abstraction.Employee;
using LinkDev.Talabat.Core.Application.Abstraction.Employee.Models;
using LinkDev.Talabat.Core.Domain.Contracts;
using LinkDev.Talabat.Core.Domain.Entities.Employees;
using LinkDev.Talabat.Core.Domain.Specifications.EmployeeSpecification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Application.Services.Employees
{
    internal class EmployeeService(IUniteOfWork uniteOfWork , IMapper mapper) : IEmployeeService
    {
        public async Task<EmployeeToReturnDto> GetEmployeeAsync(int id)
        {
            var spec = new EmployeeWithDepartmentSpecification(id);
            var Employee = await uniteOfWork.GetRepoitery<Employee,int>().GetWithSpecAsync(spec);

            var EmployeetoReturn = mapper.Map<EmployeeToReturnDto>(Employee);

            return EmployeetoReturn;

        }

        public async Task<IEnumerable<EmployeeToReturnDto>> GetEmployeesAsync()
        {
            var spec = new EmployeeWithDepartmentSpecification(5);
            
            var Employees = await uniteOfWork.GetRepoitery<Employee,int>().GetWithSpecAllAsync(spec);

            var EmployeetoReturn = mapper.Map<IEnumerable<EmployeeToReturnDto>>(Employees);
            return EmployeetoReturn;
        }
    }
}
