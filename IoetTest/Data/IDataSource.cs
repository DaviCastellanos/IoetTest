using System;
using System.Collections.Generic;
using IoetTest.Models;

namespace IoetTest
{
    public interface IDataSource
    {
        List<Employee> GetEmployeesFromLocalFile(string fileName);
    }
}

