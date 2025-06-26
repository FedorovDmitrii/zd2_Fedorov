using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using zd2_Fedorov;

namespace zd2_Fedorov
{
    public partial class Form1: Form
    {
        private PhoneBook phoneBook = new PhoneBook();
        private string fileName = "contacts.csv";
        private bool searchVisible = false;
        public Form1()
        {
            InitializeComponent();
            // Загрузка контактов при запуске
            PhoneBookLoader.Load(phoneBook, fileName);
            ShowAllContacts();
            // По умолчанию скрываем поиск
            labelSearch.Visible = false;
            textBoxSearch.Visible = false;
        }
        // Отобразить все контакты в списке.
        private void ShowAllContacts()
        {
            listBoxContacts.Items.Clear();
            List<Contact> contacts = phoneBook.GetAllContacts();
            foreach (Contact contact in contacts)
            {
                listBoxContacts.Items.Add(contact.Name + " | " + contact.Phone);
            }
        }
        // Обработчик меню "Все контакты"
        private void MenuAllContacts_Click(object sender, EventArgs e)
        {
            ShowAllContacts();
        }
        // Обработчик меню "Поиск"
        private void MenuSearch_Click(object sender, EventArgs e)
        {
            searchVisible = !searchVisible;
            labelSearch.Visible = searchVisible;
            textBoxSearch.Visible = searchVisible;
            if (!searchVisible)
            {
                textBoxSearch.Text = string.Empty;
                ShowAllContacts();
            }
        }
        // Поиск контактов по имени
        private void SearchContacts()
        {
            string search = textBoxSearch.Text.Trim();
            listBoxContacts.Items.Clear();
            List<Contact> found = phoneBook.FindByName(search);
            foreach (Contact contact in found)
            {
                listBoxContacts.Items.Add(contact.Name + " | " + contact.Phone);
            }
        }
        // Реакция на изменение текста поиска
        private void TextBoxSearch_TextChanged(object sender, EventArgs e)
        {
            SearchContacts();
        }
        // Обработчик меню "Добавить"
        private void MenuAdd_Click(object sender, EventArgs e)
        {
            AddContact();
        }
        // Добавление контакта
        private void AddContact()
        {
            string name = textBoxName.Text.Trim();
            string phone = textBoxPhone.Text.Trim();
            // Проверка: поля не пустые
            if (name.Length == 0 || phone.Length == 0)
            {
                MessageBox.Show("Введите фамилию, имя и телефон.");
                return;
            }
            // Проверка: имя не содержит цифр
            if (name.Any(char.IsDigit))
            {
                MessageBox.Show("Имя и фамилия не должны содержать цифры.");
                return;
            }
            // Проверка: телефон содержит только допустимые символы
            foreach (char ch in phone)
            {
                if (!char.IsDigit(ch) && ch != ' ' && ch != '-' && ch != '(' && ch != ')')
                {
                    MessageBox.Show("Телефон может содержать только цифры, пробелы, скобки и дефисы.");
                    return;
                }
            }
            // Проверка: длина телефона
            if (phone.Count(char.IsDigit) < 7)
            {
                MessageBox.Show("Телефон должен содержать минимум 7 цифр.");
                return;
            }
            phoneBook.AddContact(name, phone);
            ShowAllContacts();
        }
        // Обработчик меню "Удалить"
        private void MenuDelete_Click(object sender, EventArgs e)
        {
            DeleteSelectedContact();
        }
        // Удаление выбранного контакта
        private void DeleteSelectedContact()
        {
            int index = listBoxContacts.SelectedIndex;
            if (index < 0)
            {
                MessageBox.Show("Выберите контакт для удаления.");
                return;
            }
            string item = listBoxContacts.Items[index].ToString();
            string[] parts = item.Split('|');
            if (parts.Length == 2)
            {
                string name = parts[0].Trim();
                string phone = parts[1].Trim();
                phoneBook.RemoveContact(name, phone);
                ShowAllContacts();
            }
        }
        // Обработчик меню "Сохранить"
        private void MenuSave_Click(object sender, EventArgs e)
        {
            PhoneBookLoader.Save(phoneBook, fileName);
            MessageBox.Show("Сохранено.");
        }
        // Обработчик меню "Выход"
        private void MenuExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
