using System;
using System.Collections.Generic;
using System.Linq;

namespace zd2_Fedorov
{
    // Класс для управления телефонной книгой.
    public class PhoneBook
    {
        private List<Contact> contacts = new List<Contact>();
        // Получить все контакты.
        public List<Contact> GetAllContacts()
        {
            return contacts.OrderBy(contact => contact.Name).ToList();
        }
        // Поиск контактов по имени (частичное совпадение, регистронезависимо).
        public List<Contact> FindByName(string name)
        {
            return contacts.Where(contact => contact.Name != null && contact.Name.IndexOf(name, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
        }
        // Добавить контакт.
        public void AddContact(Contact contact)
        {
            contacts.Add(contact);
        }
        // Добавить контакт по имени и телефону (перегрузка).
        public void AddContact(string name, string phone)
        {
            contacts.Add(new Contact(name, phone));
        }
        // Удалить контакт по объекту.
        public void RemoveContact(Contact contact)
        {
            contacts.Remove(contact);
        }
        // Удалить контакт по имени и телефону (перегрузка).
        public void RemoveContact(string name, string phone)
        {
            Contact toRemove = contacts.FirstOrDefault(contact => contact.Name == name && contact.Phone == phone);
            if (toRemove != null)
                contacts.Remove(toRemove);
        }
        // Очистить все контакты.
        public void Clear()
        {
            contacts.Clear();
        }
    }
} 