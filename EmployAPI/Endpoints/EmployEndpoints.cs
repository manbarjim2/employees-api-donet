using EmployAPI.Data;
using EmployAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmployAPI.Endpoints
{
    public static class EmployEndpoints
    {
        //Extension method
        public static RouteGroupBuilder MapEmployEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("employees").WithParameterValidation();

            //GET /employees
            group.MapGet("/", async (EmployContext employContext) => await employContext.Employees.Include("Department").ToListAsync());

            //GET /employees/{id}
            group.MapGet("/{id}", async (EmployContext employContext, int id) => {

                Employee? employee = await employContext.Employees.Include("Department").FirstOrDefaultAsync(e => e.Id == id);

                return employee is not null ? Results.Ok(employee) : Results.NotFound();

            });

            //POST /employees
            group.MapPost("/", async (EmployContext employContext, Employee newEmployee) => {
                newEmployee.Department = await employContext.Departments.FirstOrDefaultAsync(d => d.Id == newEmployee.DepartmentId);
                employContext.Employees.Add(newEmployee);
                await employContext.SaveChangesAsync();
                return Results.Created($"/employees/{newEmployee.Id}", newEmployee);
            });

            //PUT /employees/{id}
            group.MapPut("/{id}", async (EmployContext employContext, int id, Employee updatedEmployee) => 
            {
                Employee? employee = await employContext.Employees.FindAsync(id);

                if (employee is null)
                {
                    return Results.NotFound();
                }

                if (updatedEmployee.Name is not null) { employee.Name = updatedEmployee.Name; }
                if (updatedEmployee.Salary != 0) { employee.Salary = updatedEmployee.Salary; }
                if (updatedEmployee.BirthDate != default) { employee.BirthDate = updatedEmployee.BirthDate; }
                if (updatedEmployee.DepartmentId != 0) { employee.DepartmentId = updatedEmployee.DepartmentId; }

                employContext.Employees.Update(employee);
                await employContext.SaveChangesAsync();
                return Results.NoContent();

            });

            //DELETE /employees/{id}
            group.MapDelete("/{id}", async (EmployContext employContext, int id) => 
            {
                Employee? employee = await employContext.Employees.FindAsync(id);

                if (employee is null)
                {
                    return Results.NotFound();
                }

                employContext.Employees.Remove(employee);
                await employContext.SaveChangesAsync();

                return Results.NoContent();
            });

            return group;
        }
    }
}
