using TCPData;

namespace LINQExample_3
{
    public class Program
    {
        public static void Main(string[] args)
        {
            List<Employee> employees = Data.GetEmployees();
            List<Department> departments = Data.GetDepartments();
            //SequenceEqualExample(employees);
            //ConcatExample(employees);
            //AggregateExample(employees);
            //AverageExample(employees);
            //CountExample(employees);
            //SumMinMaxExample(employees);
            //DefaultIfEmptyExample(employees);
            //EmptyExample();
            //RangeExample();
            //RepeatExample();
            //DistinctExample();
            //ExceptExample();
            //InterSectExample();
            //UnionExample();
            //SkipExample(employees);
            //SkipWhileExample(employees);
            //TakeExample(employees);
            //TakeWhileExample(employees);
            //ToDictionaryExample(employees);
            //LetExample(employees);
            //IntoExample(employees);
            //SelectExample();
            //SelectManyExample();
        }

        private static void SelectManyExample()
        {
            // SelectMany is like a flatmap
            List<Department> departmentWithEmployees = Data.GetDepartmentsWithEmployees();
            var result = departmentWithEmployees.SelectMany(d => d.Employees);
            foreach (Employee employee in result)
            {
                Console.WriteLine($"Id: {employee.Id},FirstName: {employee.FirstName}, LastName: {employee.LastName}, AnnualSalary: {employee.AnnualSalary}, IsManager: {employee.IsManager}");
            }
        }
        private static void SelectExample()
        {
            List<Department> departmentWithEmployees = Data.GetDepartmentsWithEmployees();
            var result = departmentWithEmployees.Select(d => d.Employees);
            foreach (var dept in result)
            {
                if (dept != null)
                {
                    foreach (Employee employee in dept)
                    {
                        Console.WriteLine($"Id: {employee.Id},FirstName: {employee.FirstName}, LastName: {employee.LastName}, AnnualSalary: {employee.AnnualSalary}, IsManager: {employee.IsManager}");
                    }
                }
            }
        }

        private static void IntoExample(List<Employee> employees)
        {
            var results = from emp in employees
                          where emp.AnnualSalary > 50000
                          select emp
                                      into HighEarners
                          where HighEarners.IsManager == true
                          select new
                          {
                              FullName = HighEarners.FirstName + " " + HighEarners.LastName,
                              AnnualSalary = HighEarners.AnnualSalary
                          };
            foreach (var result in results)
            {
                Console.WriteLine($"FullName: {result.FullName}, AnnualSalaryPlusBonus: {result.AnnualSalary}");
            }
        }

        private static void LetExample(List<Employee> employees)
        {
            // let allows you to use the values of the let later in the select or where condition
            var results = from emp in employees
                          let Initials = emp.FirstName.Substring(0, 1).ToUpper() + emp.LastName.Substring(0, 1).ToUpper()
                          let AnnualSalaryPlusBonus = (emp.IsManager) ? (emp.AnnualSalary + (0.004m * emp.AnnualSalary)) : (emp.AnnualSalary + (0.002m * emp.AnnualSalary))
                          where Initials == "JS" || Initials == "SJ" && AnnualSalaryPlusBonus > 50000
                          select new
                          {
                              Initials = Initials,
                              FullName = emp.FirstName + " " + emp.LastName,
                              AnnualSalaryPlusBonus = AnnualSalaryPlusBonus
                          };

            foreach (var result in results)
            {
                Console.WriteLine($"Initials: {result.Initials}, FullName: {result.FullName}, AnnualSalaryPlusBonus: {result.AnnualSalaryPlusBonus}");
            }
        }

        private static void ToDictionaryExample(List<Employee> employees)
        {
            //To Dictionary is similar to COllectors.ToMap in Java
            Dictionary<int, Employee> dictionary = (from emp in employees
                                                    where emp.AnnualSalary > 5000
                                                    select emp).ToDictionary<Employee, int>(emp => emp.Id);
            foreach (int empId in dictionary.Keys)
            {
                Console.WriteLine($"EmpId: {empId}, Employee: {dictionary.GetValueOrDefault(empId, null)}");
            }
        }

        private static void SkipWhileExample(List<Employee> employees)
        {
            // Skip till you reach the condition.
            var employeeSalaryList = employees.OrderByDescending(emp => emp.AnnualSalary).SkipWhile(e => e.AnnualSalary > 50000);
            foreach (Employee employee in employeeSalaryList)
            {
                Console.WriteLine($"Id: {employee.Id},FirstName: {employee.FirstName}, LastName: {employee.LastName}, AnnualSalary: {employee.AnnualSalary}, IsManager: {employee.IsManager}");
            }
        }

        private static void TakeWhileExample(List<Employee> employees)
        {
            // Skip till you reach the condition.
            var employeeSalaryList = employees.OrderBy(emp => emp.AnnualSalary).TakeWhile(e => e.AnnualSalary < 50000);
            foreach (Employee employee in employeeSalaryList)
            {
                Console.WriteLine($"Id: {employee.Id},FirstName: {employee.FirstName}, LastName: {employee.LastName}, AnnualSalary: {employee.AnnualSalary}, IsManager: {employee.IsManager}");
            }
        }

        private static void SkipExample(List<Employee> employees)
        {
            var result = employees.Skip(2);
            foreach (Employee employee in result)
            {
                Console.WriteLine($"Id: {employee.Id},FirstName: {employee.FirstName}, LastName: {employee.LastName}, AnnualSalary: {employee.AnnualSalary}, IsManager: {employee.IsManager}");
            }
        }

        private static void TakeExample(List<Employee> employees)
        {
            var result = employees.Take(2);
            foreach (Employee employee in result)
            {
                Console.WriteLine($"Id: {employee.Id},FirstName: {employee.FirstName}, LastName: {employee.LastName}, AnnualSalary: {employee.AnnualSalary}, IsManager: {employee.IsManager}");
            }
        }

        private static void ExceptExample()
        {
            IEnumerable<int> first = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            IEnumerable<int> second = new List<int> { 1, 7, 9 };
            // This also have a comparer argument.
            var numbersThatAreNotInSecond = first.Except(second);
            foreach (var number in numbersThatAreNotInSecond)
            {
                Console.WriteLine(number);
            }

            List<Employee> employees = Data.GetEmployees();
            List<Employee> secondEmployees = new List<Employee> { employees[0] };
            var filteredList = employees.Except(secondEmployees, new EmployeeComparer());
            Console.WriteLine($"Size of Employees: {employees.Count}, Size of Filtered Employees: {filteredList.ToList().Count}");
            foreach(Employee employee in filteredList)
            {
                Console.WriteLine($"Id: {employee.Id},FirstName: {employee.FirstName}, LastName: {employee.LastName}, AnnualSalary: {employee.AnnualSalary}, IsManager: {employee.IsManager}");
            }

        }

        private static void UnionExample()
        {
            IEnumerable<int> first = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            IEnumerable<int> second = new List<int> { 1, 7, 9 };
            // This also have a comparer argument.
            var numbersThatAreNotInSecond = first.Union(second);
            foreach (var number in numbersThatAreNotInSecond)
            {
                Console.WriteLine(number);
            }

            List<Employee> employees = Data.GetEmployees();
            List<Employee> secondEmployees = new List<Employee> { employees[0] };
            var filteredList = employees.Union(secondEmployees, new EmployeeComparer());
            Console.WriteLine($"Size of Employees: {employees.Count}, Size of Filtered Employees: {filteredList.ToList().Count}");
            foreach (Employee employee in filteredList)
            {
                Console.WriteLine($"Id: {employee.Id},FirstName: {employee.FirstName}, LastName: {employee.LastName}, AnnualSalary: {employee.AnnualSalary}, IsManager: {employee.IsManager}");
            }

        }

        private static void InterSectExample()
        {
            IEnumerable<int> first = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            IEnumerable<int> second = new List<int> { 1, 7, 9 };
            // This also have a comparer argument.
            var numbersThatAreNotInSecond = first.Intersect(second);
            foreach (var number in numbersThatAreNotInSecond)
            {
                Console.WriteLine(number);
            }

            List<Employee> employees = Data.GetEmployees();
            List<Employee> secondEmployees = new List<Employee> { employees[0] };
            var filteredList = employees.Intersect(secondEmployees, new EmployeeComparer());
            Console.WriteLine($"Size of Employees: {employees.Count}, Size of Filtered Employees: {filteredList.ToList().Count}");
            foreach (Employee employee in filteredList)
            {
                Console.WriteLine($"Id: {employee.Id},FirstName: {employee.FirstName}, LastName: {employee.LastName}, AnnualSalary: {employee.AnnualSalary}, IsManager: {employee.IsManager}");
            }

        }

        private static void DistinctExample()
        {
            List<int> numberList = new List<int> { 1, 2, 3, 4, 1, 2, 3, 4, 5, 7, 1, 6, 7 };
            List<int> distinctList = numberList.Distinct().ToList();
            foreach (int num in distinctList)
            {
                Console.WriteLine(num);
            }
        }

        private static void RepeatExample()
        {
            var strCollection = Enumerable.Repeat<string>("Placeholder", 10);
            foreach (string i in strCollection)
            {
                Console.WriteLine(i);
            }
        }

        private static void RangeExample()
        {
            // Range Syntax is, Range(<startinggNumber>, <NumberOfIterationsOrIncrements>)
            var intCollection = Enumerable.Range(25, 20);
            foreach (int i in intCollection)
            {
                Console.WriteLine(i);
            }
        }

        private static void EmptyExample()
        {
            List<Employee> emptyEmployeeList = Enumerable.Empty<Employee>().ToList();
            emptyEmployeeList.Add(new Employee { Id = 10, FirstName = "Mukesh", LastName = "Pandian", AnnualSalary = 60000.3m, IsManager = true, DepartmentId = 1 });
            foreach (Employee employee in emptyEmployeeList)
            {
                Console.WriteLine($"Id: {employee.Id},FirstName: {employee.FirstName}, LastName: {employee.LastName}, AnnualSalary: {employee.AnnualSalary}, IsManager: {employee.IsManager}");
            }
        }

        private static void DefaultIfEmptyExample(List<Employee> employees)
        {
            List<int> intList = new List<int>();
            // This returns a new singleton list with default value of the contained type.
            var newLis = intList.DefaultIfEmpty();
            Console.WriteLine(newLis.ElementAt(0));
            // This returns a new singleton list with default value mentioned int the lambda.
            var newList = employees.Where(emp => emp.DepartmentId == 199).DefaultIfEmpty(new Employee { Id = 0 });
            Console.WriteLine(newList.ElementAt(0));
        }

        private static void SumMinMaxExample(List<Employee> employees)
        {
            decimal techEmployeesTotalSalaryBudget = employees.Where(emp => emp.DepartmentId == 3).Sum(emp => emp.AnnualSalary);
            decimal maximumTechEmployeeSalary = employees.Where(emp => emp.DepartmentId == 3).Max(emp => emp.AnnualSalary);
            decimal minimumTechEmployeeSalary = employees.Where(emp => emp.DepartmentId == 3).Min(emp => emp.AnnualSalary);
            Console.WriteLine($"The total employee salary budget for Technology Department is {techEmployeesTotalSalaryBudget} with Salary Ranging from {minimumTechEmployeeSalary} to {maximumTechEmployeeSalary}");
        }

        private static void CountExample(List<Employee> employees)
        {
            var technologEmployeesCount = employees.Count(emp => emp.DepartmentId == 3);
            Console.WriteLine($"The Number of employees in Technology Department: {technologEmployeesCount}");
        }

        private static void AverageExample(List<Employee> employees)
        {
            decimal average = employees.Average(emp => emp.AnnualSalary);
            Console.WriteLine($"The Averge annual salary for the employee is {average}");
        }

        private static void AggregateExample(List<Employee> employees)
        {
            decimal totalAnnualSalary = employees
                            // This is to perform BonusCalculation and sum
                            .Aggregate<Employee, decimal>(0, (totalAnnualSalary, emp) =>
                            {
                                var bonusPercent = emp.IsManager ? 0.004m : 0.002m;
                                totalAnnualSalary += (emp.AnnualSalary + (emp.AnnualSalary * bonusPercent));
                                return totalAnnualSalary;
                            });
            string eachEmployeeData = employees.Aggregate<Employee, string, string>("Employee Annual Salaries(including bonus) ", (s, emp) =>
            {
                var bonusPercent = emp.IsManager ? 0.004m : 0.002m;
                s += $"{emp.FirstName} {emp.LastName} - {emp.AnnualSalary + (emp.AnnualSalary * bonusPercent)}, ";
                return s;
            }, s =>  s.Substring(0, s.Length -2));
            Console.WriteLine($"Total Annual salary of all employees (Including bonus): {totalAnnualSalary}");
            Console.WriteLine($"{eachEmployeeData}");
        }

        private static void ConcatExample(List<Employee> employees)
        {
            var sequence1 = new List<int>() { 1, 2, 3, 4, 5, 6 };
            var sequence2 = new List<int>() { 7, 8, 9, 10, 11, 12 };
            var concatNumbers = sequence1.Concat(sequence2);
            foreach (int i in concatNumbers)
            {
                Console.WriteLine(i);
            }
            var combinedEmployess = employees.Concat(Data.GetSecondEmployees()).OrderBy(emp => emp.Id);
            foreach (Employee employee in combinedEmployess)
            {
                Console.WriteLine($"Id: {employee.Id},FirstName: {employee.FirstName}, LastName: {employee.LastName}, AnnualSalary: {employee.AnnualSalary}, IsManager: {employee.IsManager}");
            }
        }

        private static void SequenceEqualExample(List<Employee> employees)
        {
            var sequence1 = new List<int>() { 1, 2, 3, 4, 5, 6 };
            var sequence2 = new List<int>() { 1, 2, 3, 4, 5, 6 };
            var isBothSequenceSame = sequence1.SequenceEqual(sequence2);
            Console.WriteLine($"Is Sequence Equal {isBothSequenceSame}");
            var employeeListCompare = Data.GetEmployees();
            var employeeSequenceSame = employeeListCompare.SequenceEqual(employees, new EmployeeComparer());
            Console.WriteLine($"Are Employee Sequence Equal {employeeSequenceSame}");
        }
    }
}
