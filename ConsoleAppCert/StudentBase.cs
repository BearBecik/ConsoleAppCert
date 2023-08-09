namespace ConsoleAppCert
{
    public abstract class StudentBase : Person, IStudent
    {
        public delegate void GradeAddedDelegate(object sender, EventArgs args);
        public abstract event GradeAddedDelegate GradeAdded;

        public override string Name { get; set; }
        public override string Surname { get; set; }
        public override int Age { get; set; }

        public StudentBase(string name, string surname, int age) : base(name, surname, age)
        {
        }

        public abstract void AddGrade(double grade);

        public void AddGrade(string grade)
        {
            if (grade == "Q" || grade == "Z") { return; }

            double addSmallGrade = 0;
            if (grade.Length == 2)
            {
                if (grade.Contains("+"))
                {
                    grade = grade.Replace("+", "");
                    addSmallGrade = 0.50;
                }
                else if (grade.Contains("-"))
                {
                    grade = grade.Replace("-", "");
                    addSmallGrade = -0.25;
                }
            }

            if (double.TryParse(grade, out double result) && result >= double.MinValue && result <= double.MaxValue && Age > 9)
            {
                AddGrade((double)(result + addSmallGrade));
            }
            else if (Age <= 9)
            {
                switch (grade)
                {
                    case "A":
                        AddGrade(6 + addSmallGrade);
                        break;
                    case "B":
                        AddGrade(5 + addSmallGrade);
                        break;
                    case "C":
                        AddGrade(4 + addSmallGrade);
                        break;
                    case "D":
                        AddGrade(3 + addSmallGrade);
                        break;
                    case "E":
                        AddGrade(2 + addSmallGrade);
                        break;
                    case "F":
                        AddGrade(1 + addSmallGrade);
                        break;
                    default:
                        throw new Exception("\aPodano błędną ocenę, dopuszczalne wartości?: A-E");
                        break;
                }
            }
            else
            {
                throw new Exception("\aWprowadzona ocenę jest z poza dopuszczalnego zakresu");
            }
        }
        public abstract Statistics GetStatistics();
        public abstract void PartialResults();
    }
}
