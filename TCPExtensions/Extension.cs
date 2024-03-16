using System;
using System.Collections.Generic;
using TCPData;

namespace TCPExtensions
{
    public static class Extension
    {
        public static List<T> Filter<T>(this List<T> records, Predicate<T> filterCondition)
        {
            List<T> filteredList = new List<T>();
            foreach (T record in records)
            {
                if (filterCondition(record))
                {
                    filteredList.Add(record);
                }
            }
            return filteredList;
        }

        public static IEnumerable<Employee> GetHighSalariedEmployee(this IEnumerable<Employee> employees) {
            foreach (Employee employee in employees)
            {
                Console.WriteLine($"Accessing Employee: {employee.FirstName + " " + employee.LastName}");
                if (employee.AnnualSalary > 50000)
                {
                    // Yield is used to return stream like functionality i.e value is returned one by one.
                    yield return employee;
                }
            }
        }
    }
}
