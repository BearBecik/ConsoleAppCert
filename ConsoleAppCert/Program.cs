using System.Xml.Linq;

namespace ConsoleAppCert
{
    public class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                ConsoleHeadlineText();
                Console.WriteLine("Dane podać dane Ucznia:");

                string name = InputCheckingName("imię     ");
                if (name == "Q") { break; }

                string surname = InputCheckingName("nazwisko");
                if (surname == "Q") { break; }

                string ageString = InputCheckingAge("wiek");
                if (ageString == "Q") { break; }
                int age = int.Parse(ageString);
                Console.WriteLine("");
                Console.WriteLine("\tWybierz sposób wprowadzania ocen:");
                Console.WriteLine("\t\t1. Wprowadzanie do pamięci komputera (z możliwością zapisu do pliku po zakończeniu wprowadzania)");
                Console.WriteLine("\t\t2. wprowadzanie/dopisywanie ocen do pliku 'txt'");
                Console.WriteLine("\t\tQ - zakończenie wprowadzania");
                Console.WriteLine("");
                Console.Write("Wybierz opcję: 1, 2 lub Q: ");

                bool Finish = false;
                while (!Finish)
                {
                    var userDecision = Console.ReadLine().ToUpper().ToUpper();
                    switch (userDecision)
                    {
                        case "1":
                            AddGradersToMemory(name, surname, age);
                            Finish = true;

                            break;
                        case "2":
                            AddGradersToFile(name, surname, age);
                            Finish = true;
                            break;
                        case "Q":
                            Finish = true;
                            break;
                        default:
                            Console.Write("\aBłędny wybór, wybierz ponownie opcję: 1, 2 lub Q:  ");
                            continue;
                    }
                }
            }
        }

        private static void ConsoleHeadlineText()
        {
            Console.ResetColor();
            Console.Clear();
            Console.WriteLine("\t\t Witamy w programie oceniania uczniów Szkoły Podstawowej");
            Console.WriteLine("\t\t**********************************************************");
            Console.WriteLine();
            Console.WriteLine("Dla uczniów klas I-III dopuszczane są oceny ABCDEF, dla uczniów klas IV-VIII oceny 1-6 wraz z '+' i '-'");
            Console.WriteLine();
            Console.WriteLine("użycie 'q' powoduje zakończenie wprowadzania");
            Console.WriteLine();
        }
        private static void ConsoleHeadlineTextFinish(string name, string surname, int age)
        {
            ConsoleHeadlineText();
            string s = $"Dla {name} {surname} lat {age} wprowadzaj oceny z zakresu: ";
            if (age > 9) { s += "'1-6'"; }
            else { s += "'ABCDEF'"; }
            Console.SetCursorPosition((Console.WindowWidth - s.Length) / 2, Console.CursorTop);
            ConsoleMessageColor(ConsoleColor.Green, s);
            Console.WriteLine();
        }

        public static void ConsoleMessageColor(ConsoleColor color, string text)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ResetColor();
        }
        public static string InputCheckingAge(string text)
        {
            Console.Write($"{text} \t\t");
            string inputUser = "";
            while (true)
            {
                inputUser = Console.ReadLine().Trim().ToUpper();
                if (inputUser == "Q") { return "Q"; }

                if (!int.TryParse(inputUser, out int age) || (age < 6) || (age > 15))
                {
                    ConsoleMessageColor(ConsoleColor.DarkRed, $"\aPodano błędny wiek Ucznia, podaj jeszcze raz");
                }
                else break;
            }
            return inputUser;
        }

        public static string InputCheckingName(string text)
        {
            Console.Write($"{text} \t");
            string inputUser = "";
            while (inputUser.Length < 3)
            {
                inputUser = Console.ReadLine().Trim();
                if (inputUser == "q" || inputUser == "Q") { return "Q"; }
                if (inputUser.Length < 3)
                {
                    ConsoleMessageColor(ConsoleColor.DarkRed, $"\aPodano za krótkie {text}, podaj jeszcze raz");
                }
            }
            return inputUser[0].ToString().ToUpper() + inputUser[1..].ToLower();
        }

        private static void AddGradersToMemory(string name, string surname, int age)
        {
            var student = new StudentInMemory(name, surname, age);
            ConsoleHeadlineTextFinish(name, surname, age);
            ConsoleMessageColor(ConsoleColor.Magenta, "Oceny są wprowadzane do pamięci komutera (możliwość zapisu po zakończeniu wprowadzania)");
            EnterGrade(student);
            DisplayStatistics(student);
            ConsoleMessageColor(ConsoleColor.DarkRed, "\aCzy zapisać wprowadzone oceny do pliku 'txt'? T - tak");
            string inputUser = Console.ReadLine().ToUpper().Trim();
            if (inputUser == "T")
            {
                student.StudentSaveInMemoryToTxt();
            }
        }

        private static void AddGradersToFile(string name, string surname, int age)
        {
            var student = new StudentInFile(name, surname, age);
            ConsoleHeadlineTextFinish(name, surname, age);
            ConsoleMessageColor(ConsoleColor.Magenta, "Oceny będą zapisywane w pliku 'txt'");
            EnterGrade(student);
            DisplayStatistics(student);
        }

        private static void EnterGrade(IStudent student)
        {
            student.GradeAdded += StudentGradeAdded;
            void StudentGradeAdded(object sender, EventArgs arg)
            {
                Console.WriteLine("Dodano nową ocenę");
            }
            Console.WriteLine();
            Console.WriteLine("Wprowadź pierwszą ocenę (każdą zatwierdź enterem, 'q-quit' koniec wprowadzania ocen)");
            Console.WriteLine();

            while (true)
            {
                var input = Console.ReadLine().ToUpper().Trim();
                if (input == "Q" || input == "Q")
                {
                    break;
                }
                try
                {
                    student.AddGrade(input);
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Exception: {e.Message}");
                }
            }
        }

        private static void DisplayStatistics(IStudent student)
        {
            var statistics = student.GetStatistics();
            Console.WriteLine();
            ConsoleMessageColor(ConsoleColor.DarkBlue, $"Wyniki dla: {student.Name} {student.Surname} lat: {student.Age}");
            student.PartialResults();
            if (statistics.Count > 0)
            {
                ConsoleMessageColor(ConsoleColor.DarkBlue, $"\tMin: {statistics.Min:N2} \tMax: {statistics.Max:N2} \tŚrednia: {statistics.Average:N2} ({statistics.Sum}/{statistics.Count}), \tOgólna ocena: {statistics.AverageLetter}");
            }
            else { ConsoleMessageColor(ConsoleColor.DarkRed, "\a\tBrak wyników do wyświetlenia"); }
            Console.WriteLine();
            Console.WriteLine("zakończono wyświetlanie statystyk Ucznia, wciśnij dowolny klawisz, aby przejść do wprowadzania kolejnego ucznia");
            Console.ReadKey();
        }
    }
}
