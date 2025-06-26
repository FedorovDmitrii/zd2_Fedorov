using System;

namespace zd2_Fedorov
{
    // Класс, представляющий контакт телефонной книги.
    public class Contact
    {
        // Имя контакта.
        public string Name { get; set; }
        // Телефонный номер контакта.
        public string Phone { get; set; }
        // Конструктор по умолчанию.
        public Contact() { }
        // Конструктор с параметрами.
        public Contact(string name, string phone)
        {
            Name = name;
            Phone = phone;
        }
    }
} 