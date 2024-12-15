using Lab2SchoolDBs.Data;
using Lab2SchoolDBs.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design;
using System.Linq.Expressions;

namespace Lab2SchoolDBs
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Skapar meny för användare
            Console.WriteLine("Välje ett alternativ!");
            Console.WriteLine("1. Visa all personal");
            Console.WriteLine("2. Visa personal inom en viss roll");
            Console.WriteLine("3. Hämta alla elever. Inklusive sortering");
            Console.WriteLine("4. Hämta alla elever i en viss klass\n");

            string choice = Console.ReadLine();
            //Switch sats med alla funktioner som skapats
            switch (choice) 
            {
                case "1": AllWorkers();
                    break;
                case "2": WorkersOption();
                    break;
                case "3": ShowAllStudents();
                    break;
                case "4": StudentsFromClass();
                    break;
            }
        }
        static void AllWorkers()
        {
            using(var context = new SchoolDBContext())
            {
                //Hämtar ut allt i personal list
                var personal = context.Personals.ToList();

                Console.WriteLine("-----All personal------");

                foreach(var p in personal)
                {
                    Console.WriteLine($"Namn: {p.FirstName} {p.LastName}");
                    Console.WriteLine($"Roll: {p.Roll}");
                    Console.WriteLine($"Personal ID: {p.PersonalId}");
                    Console.WriteLine();
                }
            }
        }

        static void WorkersOption()
        {
            Console.Write("Ange en roll (tex Lärare, Rektor, Admin): ");
            string roll = Console.ReadLine();
            using (var context = new SchoolDBContext())
            {
                //Jämför användarens input av roll med rollerna som finns listade 
                var personalList = context.Personals
                    .Where(p => p.Roll.ToLower() == roll.ToLower())
                    .ToList();

                if (personalList.Any())
                {
                    Console.WriteLine($"----Personal med roll: {roll}------");

                    foreach(var p in personalList)
                    {
                        Console.WriteLine($"Personal ID: {p.PersonalId}");
                        Console.WriteLine($"Namn: {p.FirstName} {p.LastName}");
                        Console.WriteLine($"Roll: {p.Roll}");
                        Console.WriteLine();
                    }
                }
                else 
                { Console.WriteLine($"Finns ingen person med rollen {roll} som du angav!"); }
            } 
        }

        static void ShowAllStudents()
        {
            // Frågar användaren hur namnen ska sorteras: 
            // efter för- eller efternamn och 
            //  i stigande eller fallande ordning
            Console.Write("Vill du se namnen sorterat på för eller efternamn? (Förnamn/Efternamn): ");
            string nameChoice = Console.ReadLine().Trim().ToLower();

            Console.WriteLine("Vill du se namnen sorterade i fallande eller stigande ordning? (Fallande/stigande)");
            string sortChoice = Console.ReadLine().Trim().ToLower();

            using(var context = new SchoolDBContext())
            {
                // Hämtar en query för alla studenter från databasen
                IQueryable<Student> studentQuery = context.Students;
               
                // Kontrollera användarens val för att sortera efter förnamn eller efternamn
                if (nameChoice == "förnamn")
                {                    
                     studentQuery = sortChoice == "fallande"
                    ? studentQuery.OrderByDescending(s => s.FirstName) // Fallande ordning
                     : studentQuery.OrderBy(s => s.FirstName); // Stigande ordning
                }
                else if(nameChoice == "efternamn")
                {
                    studentQuery = sortChoice == "fallande"
                    ? studentQuery.OrderByDescending(s => s.LastName)
                    : studentQuery.OrderBy(s => s.LastName);
                }
                else
                {
                    // Om användaren inte matar in 'förnamn' eller 'efternamn' visas ett felmeddelande
                    Console.WriteLine("Felaktig input! Du måste ange (förnamn eller efternamn)!");
                    return;
                }
                // Utför frågan och omvandlar resultatet till en lista
                var studentList = studentQuery.ToList();

                // Kontrollera om listan innehåller några studenter
                if (studentList.Any())
                {
                    // Om det finns skrivs deras information ut
                    Console.WriteLine("----- Lista över studenter--------");
                    foreach(var s in studentList)
                    {
                        Console.WriteLine($"Namn: {s.FirstName} {s.LastName}, Kurs: {s.Kurs}");
                        Console.WriteLine();
                    }
                }
                else { Console.WriteLine("Inga studenter hittades"); }
            }
        }

        static void StudentsFromClass()
        {
            using(var context = new SchoolDBContext())
            {
                // Hämtar alla studenter från databasen och konverterar resultatet till en lista
                var showStudents = context.Students.ToList();

                // Skriver in lista med alla studenter ochd reas information
                foreach(var s in showStudents)
                {
                    Console.WriteLine($"Namn: {s.FirstName} {s.LastName}");
                    Console.WriteLine($"Personnummer: {s.PersonNummer}");
                    Console.WriteLine($"Kurs: {s.Kurs}");
                    Console.WriteLine();
                }
                //Användaren matar in kurs
                Console.Write("Ange den kurs du vill se studenterna i: ");
                string choice = Console.ReadLine();

                // Filtrerar studenter baserat på kursen som användaren angav
                var studentList = context.Students
                    .Where(s => s.Kurs.ToLower() == choice.ToLower())
                    .ToList();

                // Kontrollera om det finns studenter i den angivna kursen
                if (studentList.Any())
                {
                    Console.WriteLine($"\nStudenter i klassen {choice}");
                    foreach(var s in studentList)
                    {
                        Console.WriteLine($"Namn: {s.FirstName} {s.LastName}");
                        Console.WriteLine($"Personnummer: {s.PersonNummer}");
                        Console.WriteLine($"Kurs: {s.Kurs}");
                        Console.WriteLine();
                    }
                }
                else { Console.WriteLine($"Finns inga studenter i klassen {choice}");  }          
            }
        }
    }
}
