namespace Library_Management_System
{
    public class Librarian : Customer
    {
        private string _carlendar;
        public Librarian(string IDs, string Name, string Age, string Sex, string PhoneNumber, string Status, string Calendar) : base(IDs, Name, Age, Sex, PhoneNumber, Status)
        {
            _carlendar = Calendar;
        }

        public string getCalendar()
        {
            return _carlendar;
        }
    }
}