using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using IoetTest.Models;
using Microsoft.Extensions.Hosting;

namespace IoetTest
{
    public class TimeManager : IHostedService
    {
        private readonly Task completedTask = Task.CompletedTask;

        private IDataSource dataSource;

        private List<Employee> Employees;

        public TimeManager(IDataSource dataSource)
        {
            this.dataSource = dataSource;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Employees = dataSource.GetEmployeesFromLocalFile("EmployeeData.txt");

            foreach (Employee employee in Employees)
            {
                ValidateEmployee(employee);
                Console.WriteLine($"The amount to pay {employee.Name} is {GetTotalAmountToPay(employee.Shifts)} USD.");
            }

            return completedTask;
        }

        private string GetTotalAmountToPay(List<Shift> shifts)
        {
            int result = 0;

            foreach (Shift shift in shifts)
            {
                ValidateShift(shift);
                int pendingTime = 0;

                if (shift.InitialTime <= 900)
                {
                    int dawnHours = 0;

                    if (shift.FinalTime >= 900)
                    {
                        dawnHours = (int)Math.Round((900 - shift.InitialTime) / 100.0);
                        pendingTime = 901;
                    }
                    else
                        dawnHours = (int)Math.Round((shift.FinalTime - shift.InitialTime) / 100.0);

                    if (shift.Day <= Day.Friday)
                        result += dawnHours * WeekDayFee.Dawn;
                    else
                        result += dawnHours * WeekendDayFee.Dawn;
                }

                if ((shift.InitialTime >= 900 && shift.InitialTime <= 1800) || pendingTime > 0)
                {
                    int dayHours = 0;

                    int currentTime = pendingTime > shift.InitialTime ? pendingTime : shift.InitialTime;

                    if (shift.FinalTime >= 1800)
                    {
                        dayHours = (int)Math.Round((1800 - currentTime) / 100.0);
                        pendingTime = 1801;
                    }
                    else
                    {
                        dayHours = (int)Math.Round((shift.FinalTime - currentTime) / 100.0);
                        pendingTime = 0;
                    }

                    if (shift.Day <= Day.Friday)
                        result += dayHours * WeekDayFee.Day;
                    else
                        result += dayHours * WeekendDayFee.Day;
                }

                if (shift.InitialTime >= 1800 || pendingTime > 0)
                {
                    int nightHours = 0;

                    int currentTime = pendingTime > shift.InitialTime ? pendingTime : shift.InitialTime;

                    nightHours = (int)Math.Round((shift.FinalTime - currentTime) / 100.0);

                    if (shift.Day <= Day.Friday)
                        result += nightHours * WeekDayFee.Night;
                    else
                        result += nightHours * WeekendDayFee.Night;
                }
            }

            return result.ToString();
        }

        private void ValidateShift(Shift shift)
        {
            if(shift.InitialTime < 0 || shift.InitialTime > 2400)
                throw new ArgumentException($"Initial time {shift.InitialTime} is not valid.");

            if(shift.FinalTime < 0 || shift.FinalTime > 2400)
                throw new ArgumentException($"Final time {shift.FinalTime} is not valid.");

        }

        private void ValidateEmployee(Employee employee)
        {
            if (employee.Name == null)
                throw new ArgumentException($"Employee name can't be null");

            if (employee.Shifts == null)
                throw new ArgumentException($"Employee shifts can't be null");
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return completedTask;
        }
    }
}

