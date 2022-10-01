using System;

namespace Library_Management_System
{
    public class Book
    {
        private string _Name;
        private string _Author;
        private string _Category;
        private int _Amount;
        private string _Date;
        
        public Book(string Name, string Author, string Category, int Amount, string Date)
        {
            _Name = Name;
            _Author = Author;
            _Category = Category;
            _Amount = Amount;
            _Date = Date;
        }

        public string getName()
        {
            return _Name;
        }
        
        public string getAuthor()
        {
            return _Author;
        }
        
        public string getCategory()
        {
            return _Category;
        }
        
        public string getDate()
        {
            return _Date;
        }

        public int getAmount()
        {
            return _Amount;
        }
    }
}