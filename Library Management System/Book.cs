using System;

namespace Library_Management_System
{
    public class Book
    {
        private string Name;
        private string Author;
        private string Category;
        private string Date;
        
        public Book(string Name, string Author, string Category, string Date)
        {
            this.Name = Name;
            this.Author = Author;
            this.Category = Category;
            this.Date = Date;
        }

        public string getName()
        {
            return this.Name;
        }
        public string getAuthor()
        {
            return this.Author;
        }
        public string getCategory()
        {
            return this.Category;
        }
        public string getDate()
        {
            return this.Date;
        }
    }
}