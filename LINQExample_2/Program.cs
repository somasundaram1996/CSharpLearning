using System.Collections;
using TCPData;

namespace LINQExample_2
{
    public class Program
    {
        static void Main(string[] args)
        {
            List<Employee> employees = Data.GetEmployees();
            List<Department> departments = Data.GetDepartments();
            //OrderByExample(employees, departments);
            //OrderByExampleDesc(employees, departments);
            //OrderByAndThenByExample(employees, departments);
            //OrderByAndThenByDescExample(employees, departments);
            //QueryExamleOfOrderByAndThenByDesc(employees, departments);
            //QueryExamleOfOrderByAndThenBy(employees, departments);
            //GroupByOperatorQuerySyntax(employees);
            //GroupByAndLookupOperatorMethodSyntax(employees);
            //AllOrIsAnyOperatorExample(employees);
            //ContainsAndComparerExample(employees);
            //OfTypeExample();
            //ElementAtExample(employees);
            //ElementAtOrDefaultExampleWithNullCollease(employees);
            //LastFirstAndDefaultOperators();
            //SingleSingleOrDefaultExample(employees);

        }

        private static void SingleSingleOrDefaultExample(List<Employee> employees)
        {
           // this operation will throw InvalidOperatonException since there is more than one Employee in the List
           //         var employee = employees.Single();
           // This will return a value as there is only one employee that matches the condition
           //var employee = employees.Single(employee => employee.Id == 1);
           // Console.WriteLine($"Id: {employee.Id},FirstName: {employee.FirstName}, LastName: {employee.LastName}, AnnualSalary: {employee.AnnualSalary}, IsManager: {employee.IsManager}");
           // this operation will throw InvalidOperatonException since there is more than one Employee in the List that satisfies the condition
           //         var employee = employees.Single(employee => employee.AnnualSalary >= 20000);
           // this operation will throw InvalidOperatonException since there is more than one Employee in the List that satisfies the condition
           //         var employee = employees.SingleOrDefault(employee => employee.AnnualSalary >= 20000);
           // This method will return the employee if employee is present or else the default v alue
           //         var employee = employees.SingleOrDefault(employee => employee.AnnualSalary >= 110000);
           // employee ??= new Employee() { Id = 0, FirstName = "Default", LastName = "Default", AnnualSalary = 0, IsManager = false, DepartmentId = 0 };
           // Console.WriteLine($"Id: {employee.Id},FirstName: {employee.FirstName}, LastName: {employee.LastName}, AnnualSalary: {employee.AnnualSalary}, IsManager: {employee.IsManager}");
        }

        private static void LastFirstAndDefaultOperators()
        {
            List<int> numbers = Data.GetNumbers();
            Console.WriteLine($"First Number: {numbers.First()}");
            Console.WriteLine($"First Event Number: {numbers.First(num => num % 2 == 0)}");
            Console.WriteLine($"0 Or the First Number Greater than 100: {numbers.FirstOrDefault(num => num > 100)}");
            Console.WriteLine($"0 Or the First Number Greater than 20: {numbers.FirstOrDefault(num => num > 20)}");
            Console.WriteLine($"Last Number: {numbers.Last()}");
            Console.WriteLine($"Last Event Number: {numbers.Last(num => num % 2 == 0)}");
            Console.WriteLine($"0 Or the Last Number Greater than 100: {numbers.LastOrDefault(num => num > 100)}");
            Console.WriteLine($"0 Or the Last Number Greater than 20: {numbers.LastOrDefault(num => num > 20)}");
        }

        private static void ElementAtOrDefaultExampleWithNullCollease(List<Employee> employees)
        {
            var employee = employees.ElementAtOrDefault(100);
            employee ??= new Employee() { Id = 0, FirstName = "Default", LastName = "Default", AnnualSalary = 0, IsManager = false, DepartmentId = 0 };
            Console.WriteLine($"Id: {employee.Id},FirstName: {employee.FirstName}, LastName: {employee.LastName}, AnnualSalary: {employee.AnnualSalary}, IsManager: {employee.IsManager}");
        }

        private static void ElementAtExample(List<Employee> employees)
        {
            // this method will throw ArgumentOutOfRangeExeption if index is out of bound
            var employee = employees.ElementAt(2);
            Console.WriteLine($"Id: {employee.Id},FirstName: {employee.FirstName}, LastName: {employee.LastName}, AnnualSalary: {employee.AnnualSalary}, IsManager: {employee.IsManager}");
        }

        private static void OfTypeExample()
        {
            var mixedCollection = Data.GetHeterogeniousDataCollection();
            var filteredStrings = from s in mixedCollection.OfType<string>()
                                  select s;
            var filteredNumber = from s in mixedCollection.OfType<int>()
                                 select s;
            var filteredEmployee = from s in mixedCollection.OfType<Employee>()
                                   select s;
            var filteredDepartment = from s in mixedCollection.OfType<Department>()
                                     select s;
            foreach (var item in filteredDepartment)
            {
                Console.WriteLine(item);
            }
        }

        private static void ContainsAndComparerExample(List<Employee> employees)
        {
            var searchEmployee = new Employee() { Id = 4, FirstName = "Dough", LastName = "Jones", AnnualSalary = 40000.3m, IsManager = false, DepartmentId = 3 };
            bool containsEmployee = employees.Contains(searchEmployee, new EmployeeComparer());
            if (containsEmployee)
            {
                Console.WriteLine($"The Employee {searchEmployee.FirstName + " " + searchEmployee.LastName} present in the employees list.");
            }
            else
            {
                Console.WriteLine($"The Employee {searchEmployee.FirstName + " " + searchEmployee.LastName} is not present in the employees list.");
            }
        }

        private static void AllOrIsAnyOperatorExample(List<Employee> employees)
        {
            var annualSalaryCompare = 40000;
            bool isTrueAll = employees.All(emp => emp.AnnualSalary > annualSalaryCompare);
            if (isTrueAll)
            {
                Console.WriteLine($"All employees have a salary of more than {annualSalaryCompare}");
            }
            else
            {
                Console.WriteLine($"Not All employees have a salary of more than {annualSalaryCompare}");
            }
            bool isAny = employees.Any(emp => emp.AnnualSalary > annualSalaryCompare);
            if (isAny)
            {
                Console.WriteLine($"Atleast one employee has a salary of more than {annualSalaryCompare}");
            }
            else
            {
                Console.WriteLine($"No employees have a salary of more than {annualSalaryCompare}");
            }
        }

        private static void GroupByAndLookupOperatorMethodSyntax(List<Employee> employees)
        {
            //GroupBy is deferred type of execution. To have Immedetiate execution use ToLookup
            var results = employees.OrderBy(emp => emp.DepartmentId).GroupBy(emp => emp.DepartmentId);
            foreach (var group in results)
            {
                Console.WriteLine($"Department Id: {group.Key}");
                foreach (Employee employee in group)
                {
                    Console.WriteLine($"Id: {employee.Id},FirstName: {employee.FirstName}, LastName: {employee.LastName}, AnnualSalary: {employee.AnnualSalary}, IsManager: {employee.IsManager}");
                }
            }
        }

        private static void GroupByOperatorQuerySyntax(List<Employee> employees)
        {
            var results = from emp in employees
                          orderby emp.DepartmentId
                          group emp by emp.DepartmentId;
            foreach (var group in results)
            {
                Console.WriteLine($"Department Id: {group.Key}");
                foreach (Employee employee in group)
                {
                    Console.WriteLine($"Id: {employee.Id},FirstName: {employee.FirstName}, LastName: {employee.LastName}, AnnualSalary: {employee.AnnualSalary}, IsManager: {employee.IsManager}");
                }
            }
        }

        private static void QueryExamleOfOrderByAndThenByDesc(List<Employee> employees, List<Department> departments)
        {
            var results = from employee in employees
                          join department in departments
                          on employee.DepartmentId equals department.Id
                          orderby employee.DepartmentId, employee.AnnualSalary descending
                          select new
                          {
                              Id = employee.Id,
                              FirstName = employee.FirstName,
                              LastName = employee.LastName,
                              AnnualSalary = employee.AnnualSalary,
                              DepartmentId = department.Id,
                              DepartmentName = department.LongName
                          };
            foreach (var result in results)
            {
                Console.WriteLine($"Id: {result.Id},FirstName: {result.FirstName}, LastName: {result.LastName}, AnnualSalary: {result.AnnualSalary}, DepartmentId: {result.DepartmentId}, DepartmentName: {result.DepartmentName}");
            }
        }

        private static void QueryExamleOfOrderByAndThenBy(List<Employee> employees, List<Department> departments)
        {
            var results = from employee in employees
                          join department in departments
                          on employee.DepartmentId equals department.Id
                          orderby employee.DepartmentId, employee.AnnualSalary
                          select new
                          {
                              Id = employee.Id,
                              FirstName = employee.FirstName,
                              LastName = employee.LastName,
                              AnnualSalary = employee.AnnualSalary,
                              DepartmentId = department.Id,
                              DepartmentName = department.LongName
                          };
            foreach (var result in results)
            {
                Console.WriteLine($"Id: {result.Id},FirstName: {result.FirstName}, LastName: {result.LastName}, AnnualSalary: {result.AnnualSalary}, DepartmentId: {result.DepartmentId}, DepartmentName: {result.DepartmentName}");
            }
        }

        private static void OrderByExample(List<Employee> employees, List<Department> departments)
        {
            var results = employees.Join(departments, employee => employee.DepartmentId, department => department.Id, (employee, department) =>
                        new
                        {
                            Id = employee.Id,
                            FirstName = employee.FirstName,
                            LastName = employee.LastName,
                            AnnualSalary = employee.AnnualSalary,
                            DepartmentId = department.Id,
                            DepartmentName = department.LongName
                        }).OrderBy(result => result.DepartmentId);
            foreach (var result in results)
            {
                Console.WriteLine($"Id: {result.Id},FirstName: {result.FirstName}, LastName: {result.LastName}, AnnualSalary: {result.AnnualSalary}, DepartmentId: {result.DepartmentId}, DepartmentName: {result.DepartmentName}");
            }
        }

        private static void OrderByExampleDesc(List<Employee> employees, List<Department> departments)
        {
            var results = employees.Join(departments, employee => employee.DepartmentId, department => department.Id, (employee, department) =>
                        new
                        {
                            Id = employee.Id,
                            FirstName = employee.FirstName,
                            LastName = employee.LastName,
                            AnnualSalary = employee.AnnualSalary,
                            DepartmentId = department.Id,
                            DepartmentName = department.LongName
                        }).OrderByDescending(result => result.DepartmentId);
            foreach (var result in results)
            {
                Console.WriteLine($"Id: {result.Id},FirstName: {result.FirstName}, LastName: {result.LastName}, AnnualSalary: {result.AnnualSalary}, DepartmentId: {result.DepartmentId}, DepartmentName: {result.DepartmentName}");
            }
        }

        private static void OrderByAndThenByExample(List<Employee> employees, List<Department> departments)
        {
            // Orders the Employees  first by DepartmentId and then by the AnnualSalary within that group
            var results = employees.Join(departments, employee => employee.DepartmentId, department => department.Id, (employee, department) =>
                        new
                        {
                            Id = employee.Id,
                            FirstName = employee.FirstName,
                            LastName = employee.LastName,
                            AnnualSalary = employee.AnnualSalary,
                            DepartmentId = department.Id,
                            DepartmentName = department.LongName
                        }).OrderBy(result => result.DepartmentId).ThenBy(result => result.AnnualSalary);
            foreach (var result in results)
            {
                Console.WriteLine($"Id: {result.Id},FirstName: {result.FirstName}, LastName: {result.LastName}, AnnualSalary: {result.AnnualSalary}, DepartmentId: {result.DepartmentId}, DepartmentName: {result.DepartmentName}");
            }
        }

        private static void OrderByAndThenByDescExample(List<Employee> employees, List<Department> departments)
        {
            // Orders the Employees  first by DepartmentId and then by the AnnualSalary
            var results = employees.Join(departments, employee => employee.DepartmentId, department => department.Id, (employee, department) =>
                        new
                        {
                            Id = employee.Id,
                            FirstName = employee.FirstName,
                            LastName = employee.LastName,
                            AnnualSalary = employee.AnnualSalary,
                            DepartmentId = department.Id,
                            DepartmentName = department.LongName
                        }).OrderBy(result => result.DepartmentId).ThenByDescending(result => result.AnnualSalary);
            foreach (var result in results)
            {
                Console.WriteLine($"Id: {result.Id},FirstName: {result.FirstName}, LastName: {result.LastName}, AnnualSalary: {result.AnnualSalary}, DepartmentId: {result.DepartmentId}, DepartmentName: {result.DepartmentName}");
            }
        }
    }
}
