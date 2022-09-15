using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.DataFormats;

namespace SystemOfTesting
{
    public partial class TestCreator : Form
    {
        string text;
        string file_name;
        public TestCreator()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(text == null) text = file_name + "\n";
            if (!checkBox1.Checked && !checkBox2.Checked && !checkBox3.Checked && !checkBox4.Checked && !checkBox5.Checked)
            {
                MessageBox.Show("Должен быть хотя бы один правильный ответ!");
            }
            else if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "" 
                || textBox6.Text == "") 
            {
                MessageBox.Show("Поле не может быть пустым!");
            }
            else 
            {
                text += textBox1.Text + '\n' + textBox6.Text + '\n'
                    + textBox5.Text + '\n' + textBox4.Text + '\n' + textBox3.Text + '\n'
                    + textBox2.Text + '\n';

                if (checkBox1.Checked)
                    text += '1';
                if (checkBox2.Checked)
                    text += '2';
                if (checkBox3.Checked)
                    text += '3';
                if (checkBox4.Checked)
                    text += '4';
                if (checkBox5.Checked)
                    text += '5';
                text += '\n';

                checkBox1.Checked = false;
                checkBox2.Checked = false;
                checkBox3.Checked = false;
                checkBox4.Checked = false;
                checkBox5.Checked = false;

                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                textBox4.Clear();
                textBox5.Clear();
                textBox6.Clear();



            }


            
        }

        private void TestCreator_Load(object sender, EventArgs e)
        {

            label1.Hide();
            button1.Hide();
            textBox1.Hide();
            panel2.Hide();
            textBox2.Hide();
            textBox3.Hide();
            textBox4.Hide();
            textBox5.Hide();
            textBox6.Hide();
            panel7.Hide();
            panel3.Hide();
            panel8.Hide();
            panel12.Hide();
            panel16.Hide();
            panel20.Show();
            label3.Hide();
            button3.Hide();
            checkBox1.Hide();
            checkBox2.Hide();
            checkBox3.Hide();
            checkBox4.Hide();
            checkBox5.Hide();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(textBox7.Text == "")
            {
                MessageBox.Show("Название теста не должно быть пустым");
                return;
            }
            file_name = textBox7.Text;
            if (file_name != "" && !File.Exists(Directory.GetCurrentDirectory() + file_name + ".txt"))
            {
                //File.Create(file_name + ".txt");
                File.Create(Directory.GetCurrentDirectory() + file_name + ".txt");
                label1.Show();
                button1.Show();
                textBox1.Show();
                textBox2.Show();
                panel2.Show();
                panel20.Hide();
                textBox3.Show();
                textBox4.Show();
                textBox5.Show();
                textBox6.Show();
                panel7.Show();
                panel3.Show();
                panel8.Show();
                panel12.Show();
                panel16.Show();

                label3.Show();
                button3.Show();
                checkBox1.Show();
                checkBox2.Show();
                checkBox3.Show();
                checkBox4.Show();
                checkBox5.Show();

                textBox7.Hide();
                label2.Hide();
                button2.Hide();
            }
            else
            {
                MessageBox.Show("Введите название теста!");
            }
            

            
        }

        private void TestCreator_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (file_name != "" && text != "") { }


            string path = Directory.GetParent(System.Reflection.Assembly.GetExecutingAssembly().Location).FullName;
            string fileName = Path.Combine(path, file_name + ".txt");


            using (FileStream fs = File.Create(fileName))
            {
                Byte[] info = new UTF8Encoding(true).GetBytes(text);

                fs.Write(info, 0, info.Length);
            }

            Database db = new Database();

            MySqlCommand command = new MySqlCommand("INSERT tests(Name, Path) VALUES (@test_name, @test_path)", db.getConnection());

            command.Parameters.Add("@test_name", MySqlDbType.VarChar).Value = file_name;
            command.Parameters.Add("@test_path", MySqlDbType.VarChar).Value = fileName;
            
            db.OpenConnection();

            if (command.ExecuteNonQuery() == 1)
                MessageBox.Show("Тест успешно добавлен!");
            else
                MessageBox.Show("Ошибка!");

            MySqlCommand command2 = new MySqlCommand("ALTER TABLE users ADD " + file_name +  " Float", db.getConnection());

            command2.ExecuteNonQuery();



            db.CloseConnection();

            
            Hide();
            Admin form = new Admin();
            form.Show();

        }

    }
}
