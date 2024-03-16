using TCPData;
using TCPExtensions;
using System.Linq;
namespace ThePretendCompanyApplication
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Employee> employees = Data.GetEmployees();

            //List<Employee> managers = employees.Filter<Employee>(emp => emp.AnnualSalary < 50000);
            //foreach (Employee employee in managers)
            //{
            //    Console.WriteLine($"Id: {employee.Id} , FirstName: {employee.FirstName}, LastName: {employee.LastName}, Annual Salary: {employee.AnnualSalary}, IsManager: {employee.IsManager}, DepartmentId: {employee.DepartmentId}");
            //}

            List<Department> departments = Data.GetDepartments();
            //List<Department> findAndHrDept = departments.Where(dept => dept.ShortName == "HR" || dept.ShortName == "FN").ToList();
            //foreach (Department department in findAndHrDept)
            //{
            //    Console.WriteLine($"Id: { department.Id } , ShortName: { department.ShortName }, LongName: { department.LongName }");
            //}

            var resultList = from emp in employees
                             join dept in departments
                             on emp.DepartmentId equals dept.Id
                             select new
                             {
                                 FirstName = emp.FirstName,
                                 LastName = emp.LastName,
                                 AnnualSalary = emp.AnnualSalary,
                                 Manager = emp.IsManager,
                                 Department = dept.LongName,
                             };

            foreach (var result in resultList )
            {
                Console.WriteLine($"FirstName: {result.FirstName} LastName: {result.LastName} AnnualSalary: {result.AnnualSalary}, Manager: {result.Manager}, Department: {result.Department}" );
            }

            var averageSalary = resultList.Average(a => a.AnnualSalary);
            var highestSalary = resultList.Max(a => a.AnnualSalary);
            var lowestSalary = resultList.Min(a => a.AnnualSalary);


            Console.WriteLine($"Average Salary: {averageSalary}");
            Console.WriteLine($"Highest Salary: {highestSalary}");
            Console.WriteLine($"Lowest Salary: {lowestSalary}");
        }
    }
}
