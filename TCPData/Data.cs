
using System.Collections;

namespace TCPData
{
    public static class Data
    {
        public static  List<Employee> GetEmployees()
        {
            List<Employee> employees = new List<Employee>()
            {
                new Employee() { Id = 4, FirstName = "Dough", LastName = "Jones", AnnualSalary = 40000.3m, IsManager = false, DepartmentId = 3 },
                new Employee() { Id = 2, FirstName = "Bob", LastName = "Stevens", AnnualSalary = 30000.3m, IsManager = false, DepartmentId = 1, },
                new Employee() { Id = 6, FirstName = "Dhanabal", LastName = "Ramakrishnan", AnnualSalary = 90000.3m, IsManager = false, DepartmentId = 3 },
                new Employee() { Id = 3, FirstName = "Sarah", LastName = "Jameson", AnnualSalary = 80000.3m, IsManager = true, DepartmentId = 2 },
                new Employee() { Id = 1, FirstName = "James", LastName = "Jones", AnnualSalary = 60000.3m, IsManager = true, DepartmentId = 1 },
                new Employee() { Id = 7, FirstName = "Vijay", LastName = "Manikandan", AnnualSalary = 90000.3m, IsManager = false, DepartmentId = 5 },
                new Employee() { Id = 7, FirstName = "Aravind", LastName = "Kumar", AnnualSalary = 90000.3m, IsManager = false, DepartmentId = 6 }
            };
            return employees;
        }

        public static List<Department> GetDepartments()
        {
            List<Department> departments = new List<Department>() {
                new Department() {  Id = 1, LongName = "Human Resources", ShortName = "HR"},
                new Department() {  Id = 2, LongName = "Finance", ShortName = "FN"},
                new Department() {  Id = 3, LongName = "Technology", ShortName = "TE"},
                new Department() {  Id = 4, LongName = "Customer Support", ShortName = "CUS"},
                new Department() {  Id = 5, LongName = "Marketting", ShortName = "MKT"},
            };
            return departments;
        }

        public static ArrayList GetHeterogeniousDataCollection()
        {
            ArrayList list = new ArrayList();
            list.Add(100);
            list.Add("Bob Jones");
            list.Add(200);
            list.Add(4000);
            list.Add(new Employee() { Id = 7, FirstName = "Vijay", LastName = "Manikandan", AnnualSalary = 90000.3m, IsManager = false, DepartmentId = 5 });
            list.Add(new Department() { Id = 5, LongName = "Marketting", ShortName = "MKT" });
            list.Add(new Department() { Id = 4, LongName = "Customer Support", ShortName = "CUS" });

            return list;
        }

        public static List<int> GetNumbers()
        {
            return new List<int>() { 3, 14, 23, 17, 28, 89 };
        }
    }
}
