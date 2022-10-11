namespace Library_Management_System
{
    public class Customer
    {
        private string IDs;
        private string Name;
        private string Age;
        private string Sex;
        private string PhoneNumber;
        private string Status;

        public Customer(string IDs, string Name, string Age, string Sex, string PhoneNumber, string Status)
        {
            this.IDs = IDs;
            this.Name = Name;
            this.Age = Age;
            this.Sex = Sex;
            this.PhoneNumber = PhoneNumber;
            this.Status = Status;
        }

        public string getID()
        {
            return IDs;
        }

        public string getName()
        {
            return Name;
        }

        public string getAge()
        {
            return Age;
        }

        public string getSex()
        {
            return Sex;
        }

        public string getPhoneNumber()
        {
            return PhoneNumber;
        }

        public string getStatus()
        {
            return Status;
        }

    }
}