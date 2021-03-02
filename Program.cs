using System;
using System.Collections.Generic;
using System.Linq;

namespace Przychodnia
{
    public class Patient
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public long Pesel { get; set; }
        public override string ToString()
        {
            return "Imię: " + Name + " Nazwisko: " + Surname + " Pesel: " + Pesel;
        }
    }
    public class Doctor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Title { get; set; }
        public string Specialization { get; set; }
        public override string ToString()
        {
            return "ID: " + Id + " Imię: " + Name + " Nazwisko: " + Surname + " Tytuł: " + Title + " Specjalizacja: " + Specialization;
        }
    }
    public class Visit
    {
        public string Date { get; set; }
        public string Time { get; set; }
        public override string ToString()
        {
            return "Data wizyty: " + Date + " Godzina wizyty: " + Time;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            string haslo = "";
            while (haslo != "hasloprzychodnia")
            {
                Console.WriteLine("Podaj haslo: ");
                haslo = Console.ReadLine();
            }
            Console.WriteLine("Haslo poprawne");
            int choice = 0;
            List<Doctor> doctors = new List<Doctor>();
            List<Patient> patients = new List<Patient>();
            List<Patient> listFiltered = new List<Patient>();
            List<Visit> visits = new List<Visit>();
            List<Object> visitsWithPatientsAndDoctors = new List<Object>();
            menu();
            void menu()
            {
                Console.WriteLine("Witaj w systemie obsługi przychodni. Wybierz jedną z poniższych opcji:");
                Console.WriteLine("1. Dodawanie pacjentów");
                Console.WriteLine("2. Lista pacjentów");
                Console.WriteLine("3. Dodawanie wizyt do pacjenta");
                Console.WriteLine("4. Wyszukiwanie pacjentów");// po peselu, z wizytą i imieniem i nazwiskiem lekarza
                Console.WriteLine("5. Lista wizyt"); // z peselami
                Console.WriteLine("6. Dodawanie lekarzy");
                Console.WriteLine("7. Lista lekarzy");
                Console.WriteLine("8. Usuwanie lekarzy");
                Console.WriteLine("9. Wyjście z programu");
                choice = Int32.Parse(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        addingPatients();
                        break;
                    case 2:
                        patientsList();
                        break;
                    case 3:
                        addingVisitsToPatients();
                        break;
                    case 4:
                        searchingPatients();
                        break;
                    case 5:
                        visitsList();
                        break;
                    case 6:
                        addingDoctors();
                        break;
                    case 7:
                        doctorsList();
                        break;
                    case 8:
                        deletingDoctors();
                        break;
                    case 9:
                        exit();
                        break;
                    default:
                        Console.WriteLine("Nie ma takiej opcji!");
                        menu();
                        break;
                }
            }
            void addingPatients()
            {
                Console.WriteLine("Podaj imię pacjenta: ");
                string name = Console.ReadLine();
                Console.WriteLine("Podaj nazwisko pacjenta: ");
                string surname = Console.ReadLine();
                Console.WriteLine("Podaj pesel pacjenta: ");
                bool badFormat;
                do
                {
                    badFormat = false;
                    try
                    {
                        long pesel = Int64.Parse(Console.ReadLine());
                        string peselLength = (pesel.ToString());
                        if (peselLength.Length == 11)
                        {
                            patients.Add(new Patient() { Name = name, Surname = surname, Pesel = pesel });
                        }
                        else
                        {
                            Console.WriteLine("Za malo / duzo cyfr!");
                            badFormat = true;
                        }
                    }
                    catch (FormatException e)
                    {
                        Console.WriteLine("To nie liczba");
                        badFormat = true;
                    }
                }
                while (badFormat);

                Console.WriteLine("1. Dodaj pacjenta");
                Console.WriteLine("2. Wyjdź do menu");
                int choice2 = int.Parse(Console.ReadLine());
                switch (choice2)
                {
                    case 1:
                        Console.Clear();
                        addingPatients();
                        break;
                    default:
                        Console.Clear();
                        menu();
                        break;

                }
            }
            void patientsList()
            {
                if (patients.Count == 0)
                {
                    Console.WriteLine("Brak pacjentów!");
                    menu();
                }
                else
                {
                    foreach (Patient aPatient in patients)
                    {
                        Console.WriteLine("{0,20}\n", aPatient);
                    }
                    menu();
                }
            }
            void addingVisitsToPatients()
            {
                Console.WriteLine("Wpisz pesel pacjenta którego chcesz umówić na wizytę: ");
                long inputPesel = Int64.Parse(Console.ReadLine());
                Console.WriteLine("Wpisz ID lekarza do którego chcesz umówić wizyte: ");
                int inputId = int.Parse(Console.ReadLine());
                Console.WriteLine("Podaj date wizyty dd-mm-rrrr: ");
                string date = Console.ReadLine();
                Console.WriteLine("Podaj godzinę wizyty w formacie hh:mm: ");
                string time = Console.ReadLine();
                visits.Add(new Visit() { Date = date, Time = time });
                List<Patient> listFilteredPatients = patients.Where(x => x.Pesel == inputPesel).ToList();
                List<Doctor> listFilteredDoctors = doctors.Where(x => x.Id == inputId).ToList();
                visitsWithPatientsAndDoctors = listFilteredPatients.Cast<object>().Concat(visits).Concat(listFilteredDoctors).ToList();
                Console.WriteLine();
                foreach (var x in listFilteredPatients)
                {
                    Console.WriteLine("{0,20}", x);
                }
                foreach (var x in visits)
                {
                    Console.WriteLine("{0,20}", x);
                }
                foreach (var x in listFilteredDoctors)
                {
                    Console.WriteLine("{0,20}", x);
                }
                Console.WriteLine("Dodano wizyte!");
                Console.WriteLine();
                menu();
            }
            void searchingPatients()
            {
                Console.Clear();
                Console.WriteLine("Podaj pesel pacjenta: ");
                long inputPesel = int.Parse(Console.ReadLine());
                listFiltered = patients.Where(x => x.Pesel == inputPesel).ToList();
                if (listFiltered.Count == 0)
                {
                    Console.WriteLine("Brak takiego pacjenta");
                    menu();
                }
                else
                {
                    foreach (Patient person in listFiltered)
                    {
                        Console.WriteLine("{0,20}\n", person);
                    }
                    menu();
                }
            }
            void visitsList()
            {
                if (visitsWithPatientsAndDoctors.Count == 0)
                {
                    Console.WriteLine("Brak wizyt");
                    menu();
                }
                else
                {
                    Console.Clear();
                    foreach (object visit in visitsWithPatientsAndDoctors)
                    {
                        Console.WriteLine("{0,20}\n", visit);
                    }
                    menu();
                }

            }
            void addingDoctors() // dodawanie lekarzy 
            {
                Console.WriteLine("Podaj ID lekarza: ");
                int id = int.Parse(Console.ReadLine());
                Console.WriteLine("Podaj imie lekarza: ");
                string name = Console.ReadLine();
                Console.WriteLine("Podaj nazwisko lekarza: ");
                string surname = Console.ReadLine();
                Console.WriteLine("Podaj tytuł lekarza: ");
                string title = Console.ReadLine();
                Console.WriteLine("Podaj specjalizacje lekarza: ");
                string specialization = Console.ReadLine();
                doctors.Add(new Doctor() { Id = id, Name = name, Surname = surname, Title = title, Specialization = specialization });
                Console.WriteLine("1. Dodaj lekarza");
                Console.WriteLine("2. Wyjdź do menu");
                int choice2 = int.Parse(Console.ReadLine());
                switch (choice2)
                {
                    case 1:
                        Console.Clear();
                        addingDoctors();
                        break;
                    default:
                        Console.Clear();
                        menu();
                        break;

                }

            }
            void doctorsList()
            {
                if (doctors.Count == 0)
                {
                    Console.WriteLine("Brak lekarzy!");
                    menu();
                }
                else
                {
                    foreach (Doctor aDoctor in doctors)
                    {
                        Console.WriteLine("{0,20}\n", aDoctor);
                    }
                    menu();
                }
            }
            void deletingDoctors()
            {
                Console.WriteLine("Podaj ID lekarza którego chcesz usunąć");
                int idToDelete = int.Parse(Console.ReadLine());
                doctors.RemoveAll(d => d.Id == idToDelete);
                Console.WriteLine("Lekarz usunięty!");
                menu();
            }
            void exit()
            {
                Environment.Exit(0);
            }

        }
    }
}
