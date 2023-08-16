namespace ConsoleAppCert
{
    public class StudentInFile : StudentBase
    {
        internal List<double> grades = new List<double>();
        public override event GradeAddedDelegate GradeAdded;
        private const string fileName = "grades.txt";

        public StudentInFile(string name, string surname, int age) : base(name, surname, age)
        {
        }

        public override void AddGrade(double grade)
        {
            if (grade > 0 && grade <= 6)
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
            var fullFileName = $"{Surname}_{Name}_{Age}_{fileName}";
            if (File.Exists(fullFileName))
            {
                using (var reader = File.OpenText(fullFileName))
                {
                    var line = reader.ReadLine();
                    while (line != null)
                    {
                        line = reader.ReadLine();
                        if ((double.TryParse(line, out double result)) && (result > 0 && result <= 6))
                        {
                            grades.Add((double)result);
                        }
                    }
                }
            }
            var statistics = new Statistics();
            foreach (var grade in grades)
            {
                statistics.AddGrade(grade);
            }
            return statistics;
        }

        public override void PartialResults()
        {
            PartialResults(grades);
        }
        public static void StudentSaveInMemoryToTxt(List<double> grades, string fullFileName)
        {
            using (var writer = File.AppendText($"{fullFileName}"))
            {
                foreach (var item in grades)
                {
                    writer.WriteLine(item);
                }
            }
        }
    }
}
