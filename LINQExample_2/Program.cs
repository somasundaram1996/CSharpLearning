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
