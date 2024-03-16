using TCPData;
using TCPExtensions;
namespace LINQExample_1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Employee> employees = Data.GetEmployees();
            List<Department> departments = Data.GetDepartments();
            //This is a Method Syntax Imlementation
            //MethodChainingMethod(employees);

            // This is a Query Syntax for same purpose
            //QuerySyntaxMethod(employees);
            //DeferredExecutionWithExtensionMethodExample(employees);

            // To Do Immediate Execution we run a .ToList() so that it is executed immedietely.
            //ImmedeateExecution(employees);

            //InnerJoinExampleByMethodChaining(employees, departments);

            //InnerJoinQueryExample(employees, departments);

            //GroupJoinByMethodSyntax(employees, departments);

            //GroupJoinByQuerySyntax(employees, departments);

        }

        private static void GroupJoinByQuerySyntax(List<Employee> employees, List<Department> departments)
        {
            var results = from department in departments
                          join emp in employees
                          on department.Id equals emp.DepartmentId
                          into employeeGroup
                          select new
                          {
                              Employees = employeeGroup,
                              Department = department.LongName
                          };
            foreach (var result in results)
            {
                Console.WriteLine($"Department Name: {result.Department}");
                foreach (var employee in result.Employees)
                {
                    Console.WriteLine($"\tFullName: {employee.FirstName + " " + employee.LastName}, AnnualSalary: {employee.AnnualSalary}");
                }
            }
        }

        private static void GroupJoinByMethodSyntax(List<Employee> employees, List<Department> departments)
        {
            // Equivalent to Collectors.GroupingBy() in java. This is a left outer join.
            var results = departments.GroupJoin(employees, department => department.Id, employee => employee.DepartmentId, (department, employeeGroup) => new
            {
                Employees = employeeGroup,
                Department = department.LongName
            });
            foreach (var result in results)
            {
                Console.WriteLine($"Department Name: {result.Department}");
                foreach (var employee in result.Employees)
                {
                    Console.WriteLine($"\tFullName: {employee.FirstName + " " + employee.LastName}, AnnualSalary: {employee.AnnualSalary}");
                }
            }
        }

        private static void InnerJoinQueryExample(List<Employee> employees, List<Department> departments)
        {
            var results = from department in departments
                          join employee in employees
                          on department.Id equals employee.DepartmentId
                          select new
                          {
                              FullName = employee.FirstName + " " + employee.LastName,
                              AnnualSalary = employee.AnnualSalary,
                              Department = department.LongName
                          };

            foreach (var result in results)
            {
                Console.WriteLine($"FullName: {result.FullName}, AnnualSalary: {result.AnnualSalary}, Department: {result.Department}");
            }
        }

        private static void InnerJoinExampleByMethodChaining(List<Employee> employees, List<Department> departments)
        {
            var results = departments.Join(employees, department => department.Id, employee => employee.DepartmentId, (department, employee) => new
            {
                FullName = employee.FirstName + " " + employee.LastName,
                AnnualSalary = employee.AnnualSalary,
                Department = department.LongName
            });

            foreach (var result in results)
            {
                Console.WriteLine($"FullName: {result.FullName}, AnnualSalary: {result.AnnualSalary}, Department: {result.Department}");
            }
        }

        private static void MethodChainingMethod(List<Employee> employees)
        {
            // Select is like Map in Streams in Java but can return IEnumerable of Anonymous type too. IEnumerable is like Streams in Java
            var results = employees.Select(e => new
            {
                FullName = e.FirstName + " " + e.LastName,
                AnnualSalary = e.AnnualSalary,
            }).Where(e => e.AnnualSalary > 50000);
            foreach (var result in results)
            {
                Console.WriteLine($"FullName: {result.FullName}, AnnualSalary: {result.AnnualSalary}");
            }
        }

        private static void QuerySyntaxMethod(List<Employee> employees)
        {

            var results = from emp in employees
                          where emp.AnnualSalary > 50000
                          select new
                          {
                              FullName = emp.FirstName + " " + emp.LastName,
                              AnnualSalary = emp.AnnualSalary
                          };

            employees.Add(new Employee()
            {
                Id = 5,
                FirstName = "Vicky",
                LastName = "Muthusamy",
                AnnualSalary = 90000.5m,
                IsManager = false,
                DepartmentId = 3
            });
            //Deferred Execution i.e the above query is executed only when the value is requested and that is why we can see the newly added employee after the query code.
            foreach (var result in results)
            {
                Console.WriteLine($"FullName: {result.FullName}, AnnualSalary: {result.AnnualSalary}");
            }
        }

        private static void DeferredExecutionWithExtensionMethodExample(List<Employee> employees)
        {
            var results = from emp in employees.GetHighSalariedEmployee()
                          select new
                          {
                              FullName = emp.FirstName + " " + emp.LastName,
                              AnnualSalary = emp.AnnualSalary
                          };
            employees.Add(new Employee()
            {
                Id = 5,
                FirstName = "Vicky",
                LastName = "Muthusamy",
                AnnualSalary = 90000.5m,
                IsManager = false,
                DepartmentId = 3
            });
            foreach (var result in results)
            {
                Console.WriteLine($"FullName: {result.FullName}, AnnualSalary: {result.AnnualSalary}");
            }
        }

        private static void ImmedeateExecution(List<Employee> employees)
        {
            var results = (from emp in employees.GetHighSalariedEmployee()
                           select new
                           {
                               FullName = emp.FirstName + " " + emp.LastName,
                               AnnualSalary = emp.AnnualSalary
                           }).ToList();
            employees.Add(new Employee()
            {
                Id = 5,
                FirstName = "Vicky",
                LastName = "Muthusamy",
                AnnualSalary = 90000.5m,
                IsManager = false,
                DepartmentId = 3
            });
            foreach (var result in results)
            {
                Console.WriteLine($"FullName: {result.FullName}, AnnualSalary: {result.AnnualSalary}");
            }
        }
    }
}
