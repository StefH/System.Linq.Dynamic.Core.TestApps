using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using NFluent;

namespace ConsoleApp1
{
    public class Employee
    {
        // [Key]
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }

        public ICollection<Paycheck> Paychecks { get; set; } = new List<Paycheck>();
    }

    public class Paycheck
    {
        // [Key]
        public int PaycheckId { get; set; }
        public decimal HourlyWage { get; set; }
        public int HoursWorked { get; set; }
        public DateTimeOffset DateCreated { get; set; }

        // [ForeignKey("EmployeeId")]
        public Employee Employee { get; set; }
        public int EmployeeId { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Arrange
            var employee1 = new Employee {Age = 66, EmployeeId = 1000, FirstName = "fn", LastName = "ln"};
            var employees = new List<Employee> { employee1 };

            var paychecks = new List<Paycheck>
            {
                new Paycheck
                {
                    EmployeeId = 1000,
                    Employee = employee1,
                    DateCreated = DateTimeOffset.Now,
                    HourlyWage = 10,
                    HoursWorked = 40,
                    PaycheckId = 5000
                },
                new Paycheck
                {
                    EmployeeId = 1000,
                    Employee = employee1,
                    DateCreated = DateTimeOffset.Now,
                    HourlyWage = 10,
                    HoursWorked = 20,
                    PaycheckId = 5001
                }
            };

            // Act
            var realQuery = employees.AsQueryable().GroupJoin(
                paychecks,
                employee => employee.EmployeeId,
                paycheck => paycheck.EmployeeId,
                (emp, paycheckList) => new { Name = emp.FirstName + " " + emp.LastName, NumberOfPaychecks = paycheckList.Count() });

            var dynamicQuery = employees.AsQueryable().GroupJoin(
                paychecks,
                "it.EmployeeId",
                "EmployeeId",
                "new(outer.FirstName + \" \" + outer.LastName as Name, inner.Count() as NumberOfPaychecks)");

            // Assert
            var realResult = realQuery.ToArray();
            Check.That(realResult).IsNotNull();

            var dynamicResult = dynamicQuery.ToDynamicArray();
            Check.That(dynamicResult).IsNotNull();
        }
    }
}
