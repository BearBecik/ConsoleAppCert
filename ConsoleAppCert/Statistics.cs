namespace ConsoleAppCert
{
    public class Statistics
    {
        public double Min { get; private set; }
        public double Max { get; private set; }
        public double Sum { get; private set; }
        public double Count { get; private set; }

        public double Average
        {
            get
            {
                if (Count != 0)
                {
                    return (double)Math.Round((Sum / Count), 2);
                }
                else
                {
                    return 0;
                }
            }
        }

        public char AverageLetter
        {
            get
            {
                switch (Average)
                {
                    case var average when average > 5.5:
                        return 'A';
                    case var average when average > 4.5:
                        return 'B';
                    case var average when average > 3.5:
                        return 'C';
                    case var average when average > 2.5:
                        return 'D';
                    case var average when average > 1.5:
                        return 'E';
                    default:
                        return 'F';
                }
            }
        }

        public Statistics()
        {
            Count = 0;
            Sum = 0;
            Max = double.MinValue;
            Min = double.MaxValue;

        }
        public void AddGrade(double grade)
        {
            Count++;
            Sum += grade;
            Min = Math.Min(Min, grade);
            Max = Math.Max(Max, grade);
        }
    }
}
