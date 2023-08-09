namespace ConsoleAppCert
{
    public class StudentInMemory : StudentBase
    {
        private List<double> grades = new List<double>();

        public override event GradeAddedDelegate GradeAdded;

        public StudentInMemory(string name, string surname, int age) : base(name, surname, age)
        {
        }

        public override void AddGrade(double grade)
        {
            if (grade > 0 && grade <= 6)
            {
                grades.Add(grade);
                if (GradeAdded != null)
                {
                    GradeAdded(this, new EventArgs());
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
            foreach (var grade in grades)
            {
                statistics.AddGrade(grade);
            }
            return statistics;
        }

        public void StudentSaveInMemoryToTxt()
        {
            string fileName = "grades.txt";
            var fullFileName = $"{Surname}_{Name}_{Age}_{fileName}";

            using (var writer = File.AppendText($"{fullFileName}"))
            {
                foreach (var item in grades)
                {
                    writer.WriteLine(item);
                }
            }
        }

        public override void PartialResults()
        {
            Console.WriteLine("oceny cząstkowe: ");
            foreach (var item in grades)
            {
                Console.Write($"{item:N2}, ");
            }
            Console.WriteLine();
        }
    }
}

