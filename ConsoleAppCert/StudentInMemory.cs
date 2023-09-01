namespace ConsoleAppCert
{
    public class StudentInMemory : StudentBase
    {
        public override event GradeAddedDelegate GradeAdded;
        private List<double> grades = new();

        public StudentInMemory(string name, string surname, int age) : base(name, surname, age)
        {
        }

        public override void AddGrade(double grade)
        {
            if (grade > 0 && grade <= 6.50)
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
            int i = 0;
            foreach (var grade in grades)
            {
                statistics.AddGrade(grade);
                if (i == grades.Count - 1) { Console.Write($"{grade:N2}"); }
                else Console.Write($"{grade:N2}, ");
                i++;
            }
            return statistics;
        }
    }
}

