namespace ConsoleAppCert.Test
{
    public class StudentTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void WhenStudentAgeGreater9CollectFourGrades_ShouldCorrectResult()
        {
            var studentTest = new StudentInMemory("Arkadiusz", "Bett", 10);
            studentTest.AddGrade("-4");
            studentTest.AddGrade("5+");
            studentTest.AddGrade("6-");
            studentTest.AddGrade(4.5);
            studentTest.AddGrade(+4);           //trakotwane jako double a nie string!!!

            var result = studentTest.GetStatistics();

            Assert.That((result.Min, result.Max, Math.Round(result.Average, 2)), Is.EqualTo((3.75, 5.75, 4.70)));
        }

        [Test]
        public void WhenStudentAgeLetter10GreaterCollectFourGrades_ShouldCorrectResult2()
        {
            var studentTest = new StudentInMemory("Arkadiusz", "Bett", 8);
            studentTest.AddGrade("A");
            studentTest.AddGrade("A-");
            studentTest.AddGrade("B+");
            studentTest.AddGrade("B");
            studentTest.AddGrade("B");

            var result = studentTest.GetStatistics();

            Assert.That((result.Min, result.Max, Math.Round(result.Average, 2)), Is.EqualTo((5, 6, 5.45)));
        }
    }
}