using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Microsoft.VisualBasic.ApplicationServices;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using static System.Windows.Forms.DataFormats;
using System.Collections;
using MySql.Data.MySqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Microsoft.VisualBasic;

namespace SystemOfTesting
{
    public partial class Test : Form
    {
        int question_count;
        int correct_answers;

        int correct_answers_number;
        string selected_response;

        string path;
        string test_name;
        string passport;

        System.IO.StreamReader Read;
        public Test(string path, string test_name, string passport)
        {
            InitializeComponent();
            this.path = path;
            this.test_name = test_name;
            this.passport = passport;
        }
        
        private void Test_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
        

        System.IO.StreamReader reader;

        void StartTesting()
        {
            try
            {
                
                reader = new StreamReader(path,Encoding.UTF8);
                
                Text = reader.ReadLine();
                
                question_count = 0;
                correct_answers = 0;
            }
            catch(Exception)
            {
                MessageBox.Show("Error");
            }

            Question();
        }

        void Question()
        {
            
            label1.Text = reader.ReadLine();

            checkBox1.Text = reader.ReadLine();
            checkBox2.Text = reader.ReadLine();
            checkBox3.Text = reader.ReadLine();
            checkBox4.Text = reader.ReadLine();
            checkBox5.Text = reader.ReadLine();

            correct_answers_number = int.Parse(reader.ReadLine());
            
            checkBox1.Checked = false;
            checkBox2.Checked = false;
            checkBox3.Checked = false;
            checkBox5.Checked = false;
            checkBox4.Checked = false;
            selected_response = "";
            button1.Enabled = false;

            question_count++;

            if (reader.EndOfStream == true) button1.Text = "Завершить";
            

        }

        void function(object sender, EventArgs e)
        {
            button1.Enabled = true;
            button1.Focus();
            System.Windows.Forms.CheckBox ch = (System.Windows.Forms.CheckBox)sender;
            var t = ch.Name;
            
            selected_response += int.Parse(t.Substring(8)).ToString();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Hide();
            using (Abiturient form = new Abiturient(passport)) form.ShowDialog();

        }

        private void button1_Click(object sender, EventArgs e)
        {

            char[] digits = selected_response.ToCharArray();
            Array.Sort(digits);
            selected_response = new string(digits);
            
            if (int.Parse(selected_response) == correct_answers_number) correct_answers++;
            
            if (button1.Text == "Начать тестирование сначала")
            {
                button1.Text = "Следующий";
                
                checkBox1.Visible = true;
                checkBox2.Visible = true;
                checkBox3.Visible = true;
                checkBox4.Visible = true;
                checkBox5.Visible = true;

                StartTesting();
                return;
            }
            if (button1.Text == "Завершить")
            {
                
                float result = ((correct_answers * 1.0f) / question_count) * 100;

                checkBox1.Visible = false;
                checkBox2.Visible = false;
                checkBox3.Visible = false;
                checkBox4.Visible = false;
                checkBox5.Visible = false;
                
                label1.Text = String.Format("Тестирование завершено.\n Правильных ответов: {0} из {1}.\n Ваш результат: {2:F2} %",
                    correct_answers,question_count, result);
                button1.Text = "Начать тестирование сначала";


                

                Database db = new Database();
                //
                //MySqlCommand command = new MySqlCommand("INSERT users @test_name VALUES @test_result;", db.getConnection());
                //MySqlCommand command = new MySqlCommand("UPDATE users SET @test_name = @test_result WHERE passport = @user_passport", db.getConnection());
                db.OpenConnection();
                //UPDATE users SET testname = 100 WHERE passport = AB3305720                


                string sqlCommandStatement = string.Format("UPDATE users SET {0} = @test_result WHERE passport = @user_passport", test_name);
                MySqlCommand command = new MySqlCommand(sqlCommandStatement, db.getConnection());


               // command.Parameters.AddWithValue("@test_name", test_name);
                command.Parameters.Add("@test_result", MySqlDbType.Float).Value = result;
                
                command.Parameters.Add("@user_passport", MySqlDbType.VarChar).Value = passport;


                if (command.ExecuteNonQuery() != 1) MessageBox.Show("Ошибка!");


                db.CloseConnection();

            }
            if (button1.Text == "Следующий") Question();
        }
            
        

        private void Test_Load(object sender, EventArgs e)
        {
            checkBox1.CheckedChanged += new EventHandler(function);
            checkBox2.CheckedChanged += new EventHandler(function);
            checkBox3.CheckedChanged += new EventHandler(function);
            checkBox4.CheckedChanged += new EventHandler(function);
            checkBox5.CheckedChanged += new EventHandler(function);

            StartTesting();
        }

       
        
    }
}
