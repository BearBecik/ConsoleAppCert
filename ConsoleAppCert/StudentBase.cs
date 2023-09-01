namespace ConsoleAppCert
{
    public abstract class StudentBase : IStudent
    {
        public delegate void GradeAddedDelegate(object sender, EventArgs args);

        public abstract event GradeAddedDelegate GradeAdded;

        public StudentBase(string name, string surname, int age)
        {
            Name = name;
            Surname = surname;
            Age = age;
        }

        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }

        public abstract void AddGrade(double grade);

        public void AddGrade(string grade)
        {
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

            //if (double.TryParse(grade, out double result) && result >= double.MinValue && result <= double.MaxValue && Age > 9)
            if (double.TryParse(grade, out double result) && result >= 1 && result <= 6 && Age > 9)
            {
                switch (result)
                {
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                    case 6:
                        AddGrade(result + addSmallGrade);
                        break;

                    default:
                        throw new Exception("\adopuszczalne oceny: 1, 2, 3, 4, 5 i 6 ze znakiem '+' lub '-'");
                }
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
                        throw new Exception("\adopuszczalne oceny: A, B, C, D, E i F ze znakiem '+' lub '-'");
                }
            }
            else
            {
                throw new Exception("\aocena z poza dopuszczalnego zakresu");
            }
        }

        public abstract Statistics GetStatistics();
    }
}