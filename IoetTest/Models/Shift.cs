namespace IoetTest.Models
{
    public class Shift
    {
        public Shift(int initialTime, int finalTime, Day day)
        {
            this.initialTime = initialTime;
            this.finalTime = finalTime;
            this.day = day;
        }

        private int initialTime;
        private int finalTime;
        private Day day;

        public int InitialTime
        {
            get { return initialTime; }
        }

        public int FinalTime
        {
            get { return finalTime; }
        }

        public Day Day
        {
            get { return day; }
        }
    }
}