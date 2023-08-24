namespace ConsoleAppCert.Test
{
    public class TypeTest
    {

        [Test]
        public void WhenTypeReferenceDifferentObject()
        {
            var studentTest1 = GetStudent("Arek", "Bett", 10);
            var studentTest2 = GetStudent("Arek", "Bett", 10);

            Assert.AreNotSame(studentTest1, studentTest2);
            Assert.False(studentTest1.Equals(studentTest2));
            Assert.False(Object.ReferenceEquals(studentTest1, studentTest2));
        }

        [Test]
        public void WhenTypeReferenceSameObject()
        {
            var studentTest1 = GetStudent("Arek", "Bett", 10);
            var studentTest2 = GetStudent("Arek", "Bett", 10);

            Assert.AreNotSame(studentTest1, studentTest2);
            Assert.False(studentTest1.Equals(studentTest2));
            Assert.False(Object.ReferenceEquals(studentTest1, studentTest2));
        }

        private StudentInMemory GetStudent(string name, string surname, int age)
        {
            return new StudentInMemory(name, surname, age);
        }
    }
}
