namespace ConsoleAppCert
{
    public class StudentInMemory : StudentBase
    {
        internal List<double> grades = new List<double>();

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
            StudentInFile.StudentSaveInMemoryToTxt(grades, $"{Surname}_{Name}_{Age}_grades.txt");
        }
        public override void PartialResults()
        {
            PartialResults(grades);
        }
    }
}

