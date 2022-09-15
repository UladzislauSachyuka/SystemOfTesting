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
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.AxHost;
using Application = System.Windows.Forms.Application;

namespace SystemOfTesting
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }




        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Hide();
            Registration form = new Registration();
            form.Show();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "admin")
            {
                Hide();
                Admin form = new Admin();
                form.Show();
                return;
            }

            button1.ForeColor = Color.FromArgb(1, 10, 47);
            if (textBox3.Text.Length < 7 || !int.TryParse(textBox3.Text, out _))
            {
                MessageBox.Show("Номер паспорта должен содержать 7 цифр");
                return;
            }
            if (textBox1.Text.Length < 2)
            {
                MessageBox.Show("Серия паспорта должна содержать 2 буквы");
                return;
            }

            string id = textBox1.Text + textBox3.Text;

            if(textBox2.Text.Length < 4)
            {
                MessageBox.Show("Пароль должен содержать от 4 до 15 символов");
                return;
            }  
            string password = textBox2.Text;

            

            Database db = new Database();
            DataTable table = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();

            MySqlCommand command = new MySqlCommand("SELECT passport, password FROM users WHERE passport = @user_id AND password = @user_password", db.getConnection());

            command.Parameters.Add("@user_id", MySqlDbType.VarChar).Value = id;
            command.Parameters.Add("@user_password", MySqlDbType.VarChar).Value = password;

            adapter.SelectCommand = command;
            adapter.Fill(table);

            if (table.Rows.Count > 0)
            {
                Hide();
                Abiturient form = new Abiturient(id);
                form.Show();

            }
            else
                MessageBox.Show("Пользователь не найден");

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }
        private void Login_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            textBox2.PasswordChar = '\0';
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            textBox2.PasswordChar = '●';
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            textBox1.Clear();

            //panel3.BackColor = Color.FromArgb(78,184,206);
            textBox1.ForeColor = Color.FromArgb(78, 184, 206);

            //panel4.BackColor = Color.WhiteSmoke;
            textBox2.ForeColor = Color.WhiteSmoke;

            //panel5.BackColor = Color.WhiteSmoke;
            textBox3.ForeColor = Color.WhiteSmoke;
        }

        private void textBox3_Click(object sender, EventArgs e)
        {
            textBox3.Clear();

           
           textBox3.ForeColor = Color.FromArgb(78, 184, 206);

           
            textBox1.ForeColor = Color.WhiteSmoke;

           
            textBox2.ForeColor = Color.WhiteSmoke;
        }

        private void textBox2_Click(object sender, EventArgs e)
        {
            textBox2.Clear();
            textBox2.PasswordChar = '●';

           
            textBox2.ForeColor = Color.FromArgb(78, 184, 206);

           
            textBox1.ForeColor = Color.WhiteSmoke;
            
           
            textBox3.ForeColor = Color.WhiteSmoke;
        }

        private void button1_MouseDown(object sender, MouseEventArgs e)
        {
            button1.ForeColor = Color.FromArgb(1, 10, 47);
        }
    }
}
