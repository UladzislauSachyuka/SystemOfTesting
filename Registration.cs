using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.AxHost;

namespace SystemOfTesting
{
    public partial class Registration : Form
    {
        public Registration()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            if(textBox1.Text == "" || textBox3.Text == "" || textBox2.Text == "")
            {
                MessageBox.Show("Поля для заполнения ФИО не должны быть пусты");
                return;
            }
            if (textBox1.Text.Any(char.IsDigit) || textBox2.Text.Any(char.IsDigit) || textBox3.Text.Any(char.IsDigit))
            {
                MessageBox.Show("Поля для заполнения ФИО не должны содержать цифры");
                return;
            }
            
            DateTime date;
            if (!DateTime.TryParse(textBox4.Text, out date) || date.Year > 2022 || date.Year < 1910)
            {
                MessageBox.Show("Введите корректную дату");
                return;
            }
            if (textBox5.Text.Length < 2)
            {
                MessageBox.Show("Серия паспорта должна содержать 2 буквы");
                return;
            }

            if (textBox6.Text.Length < 7 || !int.TryParse(textBox6.Text, out _))
            {
                MessageBox.Show("Номер паспорта должен содержать 7 цифр");
                return;
            }
            if (textBox7.Text.Length < 4)
            {
                MessageBox.Show("Пароль должен содержать от 4 до 15 символов");
                return;
            }
            if(textBox7.Text != textBox8.Text)
            {
                MessageBox.Show("Пароли не совпадают");
                return;
            }

            String fio = textBox1.Text + textBox2.Text + textBox3.Text;
            
            

            String id = textBox5.Text + textBox6.Text;
            String password = textBox7.Text;

            Database db = new Database();
            DataTable table = new DataTable();

            MySqlCommand command = new MySqlCommand("INSERT users(fio, birthday, passport, password) VALUES (@fio_val, @birthday_val, @passport_val, @password_val)", db.getConnection());

            command.Parameters.Add("@fio_val", MySqlDbType.VarChar).Value = fio;
            command.Parameters.Add("@birthday_val", MySqlDbType.Date).Value = date;
            command.Parameters.Add("@passport_val", MySqlDbType.VarChar).Value = id;
            command.Parameters.Add("@password_val", MySqlDbType.VarChar).Value = password;

            db.OpenConnection();

            if (command.ExecuteNonQuery() == 1) 
                MessageBox.Show("Аккаунт успешно создан!");
            else
                MessageBox.Show("Ошибка!");


            db.CloseConnection();
        }

        private void label10_Click(object sender, EventArgs e)
        {
            Hide();
            Login form = new Login();
            form.Show();
        }

        private void Registration_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        
    }
}
