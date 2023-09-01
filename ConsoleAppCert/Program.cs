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
                if (name == "Q") break;

                string surname = InputCheckingName("nazwisko");
                if (surname == "Q") break;

                string ageString = InputCheckingAge("wiek");
                if (ageString == "Q") break;
                int age = int.Parse(ageString);
                Console.WriteLine("");
                Console.WriteLine("\tWybierz sposób wprowadzania ocen:");
                Console.WriteLine("\t\t1. Wprowadzanie do pamięci komputera");
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
            if (age > 9) s += "'1-6'";
            else s += "'ABCDE', gdzie 'A' - ocena najwyższa (6), 'F' - ocena najniższa (1)";
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
                if (inputUser == "Q") return "Q";

                if (!int.TryParse(inputUser, out int age) || (age < 6) || (age > 15))
                {
                    ConsoleMessageColor(ConsoleColor.DarkRed, $"\aPodano błędny wiek Ucznia (wiek w przedziale 6-15 lat), podaj jeszcze raz");
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
                if (inputUser == "q" || inputUser == "Q") return "Q";
                if (inputUser.Length < 3)
                {
                    ConsoleMessageColor(ConsoleColor.DarkRed, $"\aPodano za krótkie {text.Trim()} Ucznia  - wymagane minimum 3 znaki, podaj jeszcze raz");
                }
            }
            return inputUser[0].ToString().ToUpper() + inputUser[1..].ToLower();
        }

        private static void AddGradersToMemory(string name, string surname, int age)
        {
            var student = new StudentInMemory(name, surname, age);
            ConsoleHeadlineTextFinish(name, surname, age);
            ConsoleMessageColor(ConsoleColor.Magenta, "Oceny są wprowadzane do pamięci komutera");
            EnterGrade(student);
            DisplayStatistics(student);
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
            void StudentGradeAdded(object sender, EventArgs arg)
            {
                Console.WriteLine("Dodano nową ocenę");
            }
            student.GradeAdded += StudentGradeAdded;

            Console.WriteLine();
            Console.WriteLine("Wprowadź pierwszą ocenę (każdą zatwierdź enterem)");
            Console.WriteLine();

            while (true)
            {
                var input = Console.ReadLine().ToUpper().Trim();
                if (input == "Q")
                {
                    break;
                }
                try
                {
                    student.AddGrade(input);
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Błąd wprowadzania: {e.Message}");
                }
            }
        }

        private static void DisplayStatistics(IStudent student)
        {
            Console.WriteLine();
            ConsoleMessageColor(ConsoleColor.DarkBlue, $"Wyniki dla: {student.Name} {student.Surname} lat: {student.Age}");
            Console.WriteLine("oceny cząstkowe: ");
            var statistics = student.GetStatistics();
            if (statistics.Count > 0)
            {
                Console.WriteLine();
                ConsoleMessageColor(ConsoleColor.DarkBlue, $"Oceny: \tNajniższa: {statistics.Min:N2} \tNajwyższa: {statistics.Max:N2} \tŚrednia: {statistics.Average:N2} ({statistics.Sum}/{statistics.Count}), \tocena ogólna: {statistics.AverageLetter}");
            }
            else ConsoleMessageColor(ConsoleColor.DarkRed, "\a\tBrak wyników do wyświetlenia");
            Console.WriteLine();
            Console.WriteLine("zakończono wyświetlanie statystyk Ucznia, wciśnij dowolny klawisz, aby przejść do wprowadzania ocen kolejnego ucznia");
            Console.ReadKey();
        }
    }
}
