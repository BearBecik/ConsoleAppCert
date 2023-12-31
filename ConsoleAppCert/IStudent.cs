﻿using static ConsoleAppCert.StudentBase;

namespace ConsoleAppCert
{
    public interface IStudent
    {
        string Name { get; }
        string Surname { get; }
        int Age { get; }

        event GradeAddedDelegate GradeAdded;

        void AddGrade(double grade);

        void AddGrade(string grade);

        Statistics GetStatistics();

        void WriteGrades();
    }
}