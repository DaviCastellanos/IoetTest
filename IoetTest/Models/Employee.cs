using System;
using System.Collections.Generic;

namespace IoetTest.Models
{
    public class Employee
    {
        public Employee(string name, List<Shift> shifts)
        {
            this.name = name;
            this.shifts = shifts;
        }

        private string name;
        private List<Shift> shifts;

        public string Name
        {
            get { return name; }
        }

        public List<Shift> Shifts
        {
            get { return shifts; }
        }

    }
}

