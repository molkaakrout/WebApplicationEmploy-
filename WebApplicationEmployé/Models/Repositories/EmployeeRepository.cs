namespace WebApplicationEmployé.Models.Repositories
{
    public class EmployeeRepository : IRepository<Employee>
    {
        List<Employee> lemployees;

        public EmployeeRepository()
        {
            lemployees = new List<Employee>()
            {
                new Employee
                {
                    Id = 1,
                    Name = "Sofien ben ali",
                    Departement = "comptabilité",
                    Salary = 1000
                },

                new Employee
                {
                    Id = 2,
                    Name = "Mourad triki",
                    Departement = "RH",
                    Salary = 1500
                },

                new Employee
                {
                    Id = 3,
                    Name = "ali ben mohamed",
                    Departement = "informatique",
                    Salary = 1700
                },

                new Employee
                {
                    Id = 4,
                    Name = "tarak aribi",
                    Departement = "informatique",
                    Salary = 1100
                }
            };
        }

        // Ajouter un nouvel employé
        // L'Id est généré automatiquement
        public void Add(Employee e)
        {
            e.Id = lemployees.Any()
                ? lemployees.Max(x => x.Id) + 1
                : 1;

            lemployees.Add(e);
        }

        // Chercher un employé par son Id
        public Employee FindByID(int id)
        {
            var emp = lemployees.FirstOrDefault(a => a.Id == id);
            return emp;
        }

        // Supprimer un employé
        public void Delete(int id)
        {
            var emp = FindByID(id);

            if (emp != null)
            {
                lemployees.Remove(emp);
            }
        }

        // Retourner tous les employés
        public IList<Employee> GetAll()
        {
            return lemployees;
        }

        // Modifier un employé
        public void Update(int id, Employee newemployee)
        {
            var emp = FindByID(id);

            if (emp != null)
            {
                emp.Name = newemployee.Name;
                emp.Departement = newemployee.Departement;
                emp.Salary = newemployee.Salary;
            }
        }

        // Recherche d'un employé
        public List<Employee> Search(string term)
        {
            if (!string.IsNullOrEmpty(term))
            {
                return lemployees
                    .Where(a => a.Name.Contains(term))
                    .ToList();
            }
            else
            {
                return lemployees;
            }
        }

        // Statistiques : salaire moyen
        public double SalaryAverage()
        {
            return lemployees.Average(x => x.Salary);
        }

        // Statistiques : salaire maximum
        public double MaxSalary()
        {
            return lemployees.Max(x => x.Salary);
        }

        // Statistiques : nombre d'employés du département RH
        public int HrEmployeesCount()
        {
            return lemployees
                .Where(x => x.Departement == "RH")
                .Count();
        }
    }
}