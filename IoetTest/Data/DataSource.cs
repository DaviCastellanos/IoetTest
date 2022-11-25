using System;
using System.Collections.Generic;
using System.IO;
using IoetTest.Models;

namespace IoetTest
{
    public class DataSource : IDataSource
    {
        public List<Employee> GetEmployeesFromLocalFile(string fileName)
        {
            List<Employee> employees = new List<Employee>();

            var path = Path.Combine(Directory.GetCurrentDirectory(), fileName);

            string data = File.ReadAllText(path);

            string[] employeesData = data.Split('|');

            foreach (string employee in employeesData)
            {
                string[] first = employee.Split("=");

                string employeeName = first[0];

                string[] daysAndTimes = first[1].Split(",");

                List<Shift> shifts = new List<Shift>();

                foreach (string dayAndTime in daysAndTimes)
                {
                    string day = dayAndTime.Substring(0, 2);
                    string[] time = dayAndTime.Substring(2).Replace(":", "").Split("-");
                    int initialTime;
                    int finishTime;

                    if (!int.TryParse(time[0], out initialTime))
                    {
                        throw new Exception($"Initial time has wrong format on day {day}");
                    }

                    if (!int.TryParse(time[1], out finishTime))
                    {
                        throw new Exception($"Finish time has wrong format on day {day}");
                    }

                    //Console.WriteLine($"{day}, initialTime {initialTime}, finishTime {finishTime}");
                    Shift shift = new Shift(initialTime, finishTime, SetDay(day));

                    shifts.Add(shift);
                }


                employees.Add(new Employee(employeeName,shifts));
            }

            return employees;
        }

        private Day SetDay(string day)
        {
            switch(day)
            {
                default:
                    throw new ArgumentException($"{day} is not valid");

                case "MO":
                    return Day.Monday;

                case "TU":
                    return Day.Tuesday;

                case "WE":
                    return Day.Wednesday;

                case "TH":
                    return Day.Thursday;

                case "FR":
                    return Day.Friday;

                case "SA":
                    return Day.Saturday;

                case "SU":
                    return Day.Sunday;

            }
        }
    }
}

