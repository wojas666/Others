using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimetableSchedule.Abstract;

namespace TimetableSchedule
{
    class Program
    {
        IEmployesRepository employesRepository;
        IHolidaysRepository holidaysRepository;
        IDutyRepository dutyRepository;
        DutyGenerator generator;

        DateTime generateTo;
        
        public Program()
        {
            employesRepository = new EmployesRepository();
            holidaysRepository = new HolidaysRepository();
            dutyRepository = new DutyRepository();
            generator = new DutyGenerator();
        }

        static void Main(string[] args)
        {
            int select = -1;
            Program program = new Program();
            program.CreateExampleEmployesData();
            program.CreateExampleHolidaysData();
            
            while (select != 4)
            {
                select = DisplayMenu();
                switch (select)
                {
                    case 1:
                        program.GenerateDutyMenu();
                        break;
                    case 2:
                        program.AssignLeave();
                        break;
                    case 3:
                        program.DisplayDuty();
                        break;
                    case 4:
                        return;

                }
            }
        }

        private void DisplayDuty()
        {
            if (generateTo != null)
            {
                foreach (Duty duty in dutyRepository.DutyRepository)
                {
                    Console.WriteLine("Data: " + duty.Date + " Pracownik: " + employesRepository.EmployesRepository.Where(e => e.Employee_ID == duty.EmployeeOnDuty_ID).FirstOrDefault().FirstName);
                }

                Console.ReadLine();
            }
        }

        private static int DisplayMenu()
        {
            Console.WriteLine("1. Generuj Grafik.");
            Console.WriteLine("2. Przypisz Urlop.");
            Console.WriteLine("3. Wyświetl Grafik.");
            Console.WriteLine("4. Zakończ Program.");
            return Convert.ToInt32(Console.ReadLine());
        }
        private void AssignLeave()
        {
            Console.WriteLine("Wybierz pracownika któremu chcesz przypisać urlop: ");
            int index = -1;

            foreach(Employee e in employesRepository.EmployesRepository)
            {
                Console.WriteLine(e.Employee_ID + ". " + " Imię: " + e.FirstName + " Nazwisko: " + e.LastName);
            }

            index = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Podaj Miesiąc od którego chcesz przypisać urlop: ");
            int mounthFrom = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Podaj Dzień od którego chcesz przypisać urlop: ");
            int dayFrom = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Podaj Miesiąc do którego chcesz przypisać urlop: ");
            int mounthTo = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Podaj Dzień do którego chcesz przypisać urlop: ");
            int dayTo = Convert.ToInt32(Console.ReadLine());

            var employeToSetVacation = employesRepository.EmployesRepository
                .Where(e => e.Employee_ID == index)
                .FirstOrDefault();

            employeToSetVacation.Vacation = new Vacation
            {
                From = new DateTime(DateTime.Now.Year, mounthFrom, dayFrom),
                To = new DateTime(DateTime.Now.Year, mounthTo, dayTo)
            };

            if(generateTo != null)
            {
                generator.RecalculateWorksDays(employeToSetVacation.Vacation.From, dutyRepository, employesRepository);
                generator.GenerateDuty(employeToSetVacation.Vacation.From, generateTo, dutyRepository, employesRepository, holidaysRepository);
            }
        }
        private void GenerateDutyMenu()
        {
            Console.WriteLine("Podaj rok do którego grafik ma zostać wygenerowany: ");
            int year = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Podaj miesiąc do którego grafik ma zostać wygenerowany: ");
            int mounth = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Podaj dzień do którego grafik ma zostać wygenerowany: ");
            int day = Convert.ToInt32(Console.ReadLine());

            generateTo = new DateTime(year, mounth, day);
            generator.GenerateDuty(DateTime.Now, generateTo, dutyRepository, employesRepository, holidaysRepository);
        }

        private void CreateExampleEmployesData()
        {
            employesRepository.Insert(
                new Employee
                {
                    FirstName = "Jakub",
                    LastName = "Nowak",
                    Age = 27,
                    Vacation = new Vacation
                    {
                        From = new DateTime(1997, 1,1),
                        To = new DateTime(1997, 1, 1)
                    },
                    WorkDays = 0
                }
             );

            employesRepository.Insert(
                new Employee
                {
                    FirstName = "Kamil",
                    LastName = "Nowicki",
                    Age = 33,
                    Vacation = new Vacation
                    {
                        From = new DateTime(1997, 1, 1),
                        To = new DateTime(1997, 1, 1)
                    },
                    WorkDays = 0
                }
             );

            employesRepository.Insert(
                new Employee
                {
                    FirstName = "Adam",
                    LastName = "Kowalski",
                    Age = 25,
                    Vacation = new Vacation
                    {
                        From = new DateTime(1997, 1, 1),
                        To = new DateTime(1997, 1, 1)
                    },
                    WorkDays = 0
                }
             );

            employesRepository.Insert(
                new Employee
                {
                    FirstName = "Marcin",
                    LastName = "Filipiuk",
                    Age = 41,
                    Vacation = new Vacation
                    {
                        From = new DateTime(1997, 1, 1),
                        To = new DateTime(1997, 1, 1)
                    },
                    WorkDays = 0
                });
        }
        private void CreateExampleHolidaysData()
        {
            holidaysRepository.Insert(
                new Holiday
                {
                    Date = new DateTime(2017, 6, 15),
                    Name = "Boże Ciało"
                }
            );

            holidaysRepository.Insert(
                new Holiday
                {
                    Date = new DateTime(2017, 5, 30),
                    Name = "Wymyślone święto I"
                }
            );

            holidaysRepository.Insert(
                new Holiday
                {
                    Date = new DateTime(2017, 6, 2),
                    Name = "Święto ustalone przez firmę"
                }
            );

            holidaysRepository.Insert(
                new Holiday
                {
                    Date = new DateTime(2017, 7, 3),
                    Name = "Wymyślone święto II"
                }
            );
        }
    }
}
