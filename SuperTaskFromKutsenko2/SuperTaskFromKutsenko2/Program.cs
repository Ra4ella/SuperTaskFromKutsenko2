using System;
using System.IO;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using static SuperTaskFromKutsenko2.Program;

namespace SuperTaskFromKutsenko2
{
    public class Program
    {
        public class Library
        {
            [XmlElement("Book")]
            public List<Book> books { get; set; } = new List<Book> //Лист для книг
            {
                new Book { Title = "1984", Author = "George Orwell", Genre = "Dystopian", Year = 1949, Id = 1 },
                new Book { Title = "To Kill a Mockingbird", Author = "Harper Lee", Genre = "Fiction", Year = 1960, Id = 2 },
                new Book { Title = "Pride and Prejudice", Author = "Jane Austen", Genre = "Romance", Year = 1813, Id = 3 }
            };
            [XmlElement("User")]
            public List<User> users { get; set; } = new List<User> { }; //Лист для пользователей
            public string fileName = "log.txt"; // Имя файла для логирования действий

            public void SaveData() // Функция для сериализации данных в Xml файл
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Library));
                using (StreamWriter write = new StreamWriter("file.xml"))
                {
                    serializer.Serialize(write, this);
                }
            }

            public void AddBook(Book book) // Функция для добавления книг
            {
                if (books.Any(b => b.Id == book.Id)) 
                {
                    Console.WriteLine("Book with that id is already");
                    return;
                }
                books.Add(book);
                Console.WriteLine("Book added");
                using (StreamWriter write = new StreamWriter(fileName, true)) // Запись действий в текстовый документ
                {
                    write.WriteLine("Book added");
                    write.WriteLine($"Id: {book.Id}");
                }
            }
            public void RemoveBook(int id) // Функция для удаления книг
            {
                var book = books.FirstOrDefault(b => b.Id == id);
                if (book == null)
                {
                    Console.WriteLine("Book with that id isn't found");
                    return;
                }
                books.Remove(book);
                Console.WriteLine("Book removed");
                using (StreamWriter write = new StreamWriter(fileName, true)) // Запись действий в текстовый документ
                {
                    write.WriteLine("Book removed");
                    write.WriteLine($"Id: {book.Id}");
                }
            }
            public void EditBook(int id, string title, string author, string genre, int year) // Функция для изменения книг
            {
                Book book = books.FirstOrDefault(b => b.Id == id);
                if (book == null)
                {
                    Console.WriteLine("Book with that id isn't found");
                    return;
                }
                book.Title = title;
                book.Author = author;
                book.Genre = genre;
                book.Year = year;
                Console.WriteLine("Book edited");
                using (StreamWriter write = new StreamWriter(fileName, true)) // Запись действий в текстовый документ
                {
                    write.WriteLine("Book edited");
                    write.WriteLine($"Id: {book.Id}");
                }
            }
            public void ShowBooks() // Функция для вывода всех книг в библиотеке
            {
                foreach (var book in books)
                {
                    Console.WriteLine();
                    Console.WriteLine($"Title: {book.Title}");
                    Console.WriteLine($"Author: {book.Author}");
                    Console.WriteLine($"Genre: {book.Genre}");
                    Console.WriteLine($"Year: {book.Year}");
                    Console.WriteLine($"Id: {book.Id}");
                    Console.WriteLine();
                }
            }
            public void ShowBooksFromTitle(string title) // Функция для вывода книг по названию
            {
                Book book = books.FirstOrDefault(t => t.Title == title);
                if (book == null)
                {
                    Console.WriteLine("Book with that title isn't found");
                    return;
                }
                Console.WriteLine();
                Console.WriteLine($"Title: {book.Title}");
                Console.WriteLine($"Author: {book.Author}");
                Console.WriteLine($"Genre: {book.Genre}");
                Console.WriteLine($"Year: {book.Year}");
                Console.WriteLine($"Id: {book.Id}");
                Console.WriteLine();
            }
            public void ShowBooksFromAuthor(string author) // Функция для вывода книг по автору
            {
                Book book = books.FirstOrDefault(a => a.Author == author);
                if (book == null)
                {
                    Console.WriteLine("Book with that author isn't found");
                    return;
                }
                Console.WriteLine();
                Console.WriteLine($"Title: {book.Title}");
                Console.WriteLine($"Author: {book.Author}");
                Console.WriteLine($"Genre: {book.Genre}");
                Console.WriteLine($"Year: {book.Year}");
                Console.WriteLine($"Id: {book.Id}");
                Console.WriteLine();
            }

            public void Register(User user) // Функция для добавления пользователя
            {
                if (users.Any(u => u.Id == user.Id))
                {
                    Console.WriteLine("User with that id is already");
                    return;
                }
                users.Add(user);
                Console.WriteLine("User registered");
                using (StreamWriter write = new StreamWriter(fileName, true)) // Запись действий в текстовый документ
                {
                    write.WriteLine("User registered");
                    write.WriteLine($"Id: {user.Id}");
                }
            }

            public void RemoveUser(int id) // Функция для удаления пользователя
            {
                User user = users.FirstOrDefault(u => u.Id == id);
                if (user == null)
                {
                    Console.WriteLine("User with that id isn't found");
                    return;
                }
                users.Remove(user);
                Console.WriteLine("User removed");
                using (StreamWriter write = new StreamWriter(fileName, true)) // Запись действий в текстовый документ
                {
                    write.WriteLine("User removed");
                    write.WriteLine($"Id: {user.Id}");
                }
            }

            public void ShowUser() // Функция для вывода всех пользователей
            {
                foreach (var user in users)
                {
                    Console.WriteLine();
                    Console.WriteLine($"Id: {user.Id}");
                    Console.WriteLine($"Name: {user.Name}");
                    Console.WriteLine();
                }
            }
            public void RentBook(int idBook, int idUser) // Функция для взятия книги в аренду
            {
                Book book = books.FirstOrDefault(b => b.Id == idBook);
                User user = users.FirstOrDefault(u => u.Id == idUser);
                if (user == null)
                {
                    Console.WriteLine("User with that id isn't found");
                    return;
                }
                if (book == null)
                {
                    Console.WriteLine("Book with that id isn't found");
                    return;
                }
                if (book.Status == "Taken")
                {
                    Console.WriteLine("Book is already taken");
                    return;
                }
                book.Status = "Taken";
                user.Books.Add(book);
                Console.WriteLine("Book taken");
                using (StreamWriter write = new StreamWriter(fileName, true)) // Запись действий в текстовый документ
                {
                    write.WriteLine("Book taken");
                    write.WriteLine($"Id: {book.Id}");
                }
            }

            public void GiveBook(int idBook, int idUser) // Функция для возврата книги
            {
                User user = users.FirstOrDefault(u => u.Id == idUser);
                Book book = user.Books.FirstOrDefault(b => b.Id == idBook);
                if (user == null)
                {
                    Console.WriteLine("User with that id isn't found");
                    return;
                }
                if (book == null)
                {
                    Console.WriteLine("Book with that id isn't found");
                    return;
                }
                book.Status = "Free";
                user.Books.Remove(book);
                Console.WriteLine("Book given");
                using (StreamWriter write = new StreamWriter(fileName, true)) // Запись действий в текстовый документ
                {
                    write.WriteLine("Book given");
                    write.WriteLine($"Id: {book.Id}");
                }
            }

            public void ShowRentBook(int idUser) // Функция для вывода всех книг в аренду у пользователя
            {
                User user = users.FirstOrDefault(u => u.Id == idUser);
                if (user.Books.Count == 0)
                {
                    Console.WriteLine("No books taken");
                    return;
                }
                foreach (var book in user.Books)
                {
                    Console.WriteLine();
                    Console.WriteLine($"Title: {book.Title}");
                    Console.WriteLine($"Author: {book.Author}");
                    Console.WriteLine($"Genre: {book.Genre}");
                    Console.WriteLine($"Year: {book.Year}");
                    Console.WriteLine($"Id: {book.Id}");
                    Console.WriteLine();
                }
            }

        }
        public class Book // Клас для книг
        {
            public string Title { get; set; }
            public string Author { get; set; }
            public string Genre { get; set; }
            public int Year { get; set; }
            public int Id { get; set; }
            public string Status = "Free";

        }
        public class User // Клас для пользователей
        {
            public string Name { get; set; }
            public int Id { get; set; }
            [XmlArray("Books")]
            [XmlArrayItem("Book")]
            public List<Book> Books { get; set; } = new List<Book>(); // Массив для книг которые в аренде
        }
        static void Main(string[] args)
        {
            Library library = new Library();
            File.WriteAllText("log.txt",string.Empty); // Перезапуск файла при начале программы для обновления логов

            while (true) // Меню
            {
                Console.WriteLine("1. Book");
                Console.WriteLine("2. User");
                Console.WriteLine("3. Rent a book");
                Console.WriteLine("4. Exit");
                Console.Write("Enter your choice: ");
                int choice1 = Convert.ToInt32(Console.ReadLine());
                if (choice1 > 0 && choice1 < 5)
                {
                    if (choice1 == 1)
                    {
                        Console.WriteLine("1. Add book");
                        Console.WriteLine("2. Remove book");
                        Console.WriteLine("3. Edit book");
                        Console.WriteLine("4. Show books");
                        Console.WriteLine("5. Search book");
                        Console.Write("Enter your choice: ");
                        int choice2 = Convert.ToInt32(Console.ReadLine());
                        if (choice2 > 0 && choice2 < 6)
                        {
                            if (choice2 == 1)
                            {
                                Console.Write("Enter title: ");
                                string title = Console.ReadLine();
                                Console.Write("Enter author: ");
                                string author = Console.ReadLine();
                                Console.Write("Enter genre: ");
                                string genre = Console.ReadLine();
                                Console.Write("Enter year: ");
                                int year = Convert.ToInt32(Console.ReadLine());
                                if (year < 0 && year > 2025)
                                {
                                    Console.Write("Enter year again: ");
                                    year = Convert.ToInt32(Console.ReadLine());
                                }
                                Console.Write("Enter id: ");
                                int id = Convert.ToInt32(Console.ReadLine());
                                Book book = new Book() { Title = title, Author = author, Genre = genre, Year = year, Id = id };
                                library.AddBook(book);
                            }
                            if (choice2 == 2)
                            {
                                Console.Write("Enter id: ");
                                int id = Convert.ToInt32(Console.ReadLine());
                                library.RemoveBook(id);
                            }
                            if (choice2 == 3)
                            {
                                Console.Write("Enter id for edit: ");
                                int id = Convert.ToInt32(Console.ReadLine());
                                Console.Write("Enter title: ");
                                string title = Console.ReadLine();
                                Console.Write("Enter author: ");
                                string author = Console.ReadLine();
                                Console.Write("Enter genre: ");
                                string genre = Console.ReadLine();
                                Console.Write("Enter year: ");
                                int year = Convert.ToInt32(Console.ReadLine());
                                if (year < 0 && year > 2025)
                                {
                                    Console.Write("Enter year again: ");
                                    year = Convert.ToInt32(Console.ReadLine());
                                }
                                library.EditBook(id, title, author, genre, year);
                            }
                            if (choice2 == 4)
                            {
                                library.ShowBooks();
                            }
                            if (choice2 == 5)
                            {
                                Console.WriteLine("1. Title");
                                Console.WriteLine("2. Author");
                                Console.Write("Enter your choice: ");
                                int choice3 = Convert.ToInt32(Console.ReadLine());
                                if (choice3 > 0 && choice3 < 3)
                                {
                                    if (choice3 == 1)
                                    {
                                        Console.Write("Enter title: ");
                                        string title = Console.ReadLine();
                                        library.ShowBooksFromTitle(title);
                                    }
                                    if (choice3 == 2)
                                    {
                                        Console.Write("Enter author: ");
                                        string author = Console.ReadLine();
                                        library.ShowBooksFromTitle(author);
                                    }
                                }
                                else
                                {
                                    Console.Write("Enter your choice again: ");
                                    choice3 = Convert.ToInt32(Console.ReadLine());
                                }
                            }
                        }
                        else
                        {
                            Console.Write("Enter your choice again: ");
                            choice2 = Convert.ToInt32(Console.ReadLine());
                        }
                    }
                    if (choice1 == 2)
                    {
                        Console.WriteLine("1. Register new user");
                        Console.WriteLine("2. Remove user");
                        Console.WriteLine("3. Show user");
                        Console.Write("Enter your choice: ");
                        int choice4 = Convert.ToInt32(Console.ReadLine());
                        if (choice4 > 0 && choice4 < 4)
                        {
                            if (choice4 == 1)
                            {
                                Console.Write("Enter name: ");
                                string name = Console.ReadLine();
                                Console.Write("Enter id: ");
                                int id = Convert.ToInt32(Console.ReadLine());
                                User user = new User() { Id = id, Name = name };
                                library.Register(user);
                            }
                            if (choice4 == 2)
                            {
                                Console.Write("Enter id for remove: ");
                                int id = Convert.ToInt32(Console.ReadLine());
                                library.RemoveUser(id);
                            }
                            if (choice4 == 3) 
                            {
                                library.ShowUser();
                            }
                        }
                        else
                        {
                            Console.Write("Enter your choice again: ");
                            choice4 = Convert.ToInt32(Console.ReadLine());
                        }
                    }
                    if (choice1 == 3)
                    {
                        Console.WriteLine("1. Rent book");
                        Console.WriteLine("2. Give book");
                        Console.WriteLine("3. Show user's books list");
                        Console.Write("Enter your choice: ");
                        int choice5 = Convert.ToInt32(Console.ReadLine());
                        if (choice5 > 0 && choice5 < 4)
                        {
                            if (choice5 == 1)
                            {
                                Console.Write("Enter user's id: ");
                                int idUser = Convert.ToInt32(Console.ReadLine());
                                Console.Write("Enter book's id: ");
                                int idBook = Convert.ToInt32(Console.ReadLine());
                                library.RentBook(idBook, idUser);
                            }
                            if (choice5 == 2)
                            {
                                Console.Write("Enter user's id: ");
                                int idUser = Convert.ToInt32(Console.ReadLine());
                                Console.Write("Enter book's id: ");
                                int idBook = Convert.ToInt32(Console.ReadLine());
                                library.GiveBook(idBook, idUser);
                            }
                            if (choice5 == 3) 
                            {
                                Console.Write("Enter user's id: ");
                                int idUser = Convert.ToInt32(Console.ReadLine());
                                library.ShowRentBook(idUser);
                            }
                        }
                        else
                        {
                            Console.Write("Enter your choice again: ");
                            choice5 = Convert.ToInt32(Console.ReadLine());
                        }
                    }
                    if (choice1 == 4)
                    {
                        library.SaveData();
                        break;
                    }
                }
                else
                {
                    Console.Write("Enter your choice again: ");
                    choice1 = Convert.ToInt32(Console.ReadLine());
                }
            }
        }
    }
}
