using System;
using System.IO;
using System.Text;
using System.Linq;
using System.Collections.Generic;

namespace zd2_Fedorov
{
    // Статический класс для загрузки и сохранения телефонной книги из/в CSV-файл.
    public static class PhoneBookLoader
    {
        // Загрузить контакты из CSV-файла в телефонную книгу.
        public static void Load(PhoneBook phoneBook, string fileName)
        {
            phoneBook.Clear();
            if (!File.Exists(fileName)) return;
            string[] lines = File.ReadAllLines(fileName, Encoding.UTF8);
            foreach (string line in lines)
            {
                string[] parts = line.Split(new char[] { ',', ';', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length >= 2)
                {
                    phoneBook.AddContact(parts[0].Trim(), parts[1].Trim());
                }
            }
        }
        // Сохранить контакты из телефонной книги в CSV-файл.
        public static void Save(PhoneBook phoneBook, string fileName)
        {
            List<Contact> contacts = phoneBook.GetAllContacts();
            string[] lines = contacts.Select(contact => contact.Name + "," + contact.Phone).ToArray();
            File.WriteAllLines(fileName, lines, Encoding.UTF8);
        }
    }
} 