namespace ConsoleAppCert
{
    public class StudentInFile : StudentBase
    {
        public override event GradeAddedDelegate GradeAdded;

        private const string fileName = "grades.txt";

        public StudentInFile(string name, string surname, int age) : base(name, surname, age)
        {
        }

        public override void AddGrade(double grade)
        {
            if (grade > 0 && grade <= 6.50)
            {
                var fullFileName = $"{Surname}_{Name}_{Age}_{fileName}";
                using (var writer = File.AppendText($"{fullFileName}"))
                {
                    writer.WriteLine(grade);
                    if (GradeAdded != null)
                    {
                        GradeAdded(this, new EventArgs());
                    }
                }
            }
            else
            {
                throw new Exception("\aWprowadzono ocenę z poza dopuszczalnego zakresu");
            }
        }

        public override Statistics GetStatistics()
        {
            var statistics = new Statistics();
            var fullFileName = $"{Surname}_{Name}_{Age}_{fileName}";
            if (File.Exists(fullFileName))
            {
                using (var reader = File.OpenText(fullFileName))
                {
                    var line = reader.ReadLine();
                    while (line != null)
                    {
                        if (double.TryParse(line, out double grade))
                        {
                            statistics.AddGrade(grade);
                        }
                        line = reader.ReadLine();
                    }
                }
            }
            return statistics;
        }

        public override void WriteGrades()
        {
            var fullFileName = $"{Surname}_{Name}_{Age}_{fileName}";
            if (File.Exists(fullFileName))
            {
                using (var reader = File.OpenText(fullFileName))
                {
                    var line = reader.ReadLine();
                    while (line != null)
                    {
                        if (double.TryParse(line, out double grade))
                        {
                            Console.Write($"{grade:N2}");
                            line = reader.ReadLine();
                            if (line != null) Console.Write(", ");
                        }
                    }
                }
            }
            Console.WriteLine();
        }
    }
}