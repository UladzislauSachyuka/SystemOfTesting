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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace SystemOfTesting
{
    public partial class Admin : Form
    {
        public Admin()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            Database db = new Database();
            
            MySqlCommand command = new MySqlCommand("SELECT * FROM users", db.getConnection());

           
            db.OpenConnection();

            MySqlDataReader read = command.ExecuteReader();
            
            DataTable dt = new DataTable();
            dt.Load(read);
            
            dataGridView1.DataSource = dt;

            db.CloseConnection();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Hide();
            Login form = new Login();
            form.Show();
        }

        private void Admin_FormClosed(object sender, FormClosedEventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            Hide();
            TestCreator form = new TestCreator();
            form.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Database db = new Database();

            db.OpenConnection();

            string test_name = listBox1.GetItemText(listBox1.SelectedItem);
           
            //DELETE FROM Customers WHERE CustomerName='Alfreds Futterkiste';
            MySqlCommand command2 = new MySqlCommand("DELETE FROM tests WHERE Name = @test_name", db.getConnection());
            command2.Parameters.AddWithValue("@test_name", test_name);


            if (command2.ExecuteNonQuery() == 1)
                MessageBox.Show("Тест успешно удален!");
            else
                MessageBox.Show("Ошибка!");

            listBox1.Items.Clear();
            MySqlCommand command = new MySqlCommand("SELECT Name FROM tests", db.getConnection());

            MySqlDataReader read = command.ExecuteReader();

            while (read.Read())
            {

                listBox1.Items.Add(Convert.ToString(read["Name"]));
            }

            db.CloseConnection();
        }

        private void Admin_Load(object sender, EventArgs e)
        {
            Database db = new Database();

            MySqlCommand command = new MySqlCommand("SELECT Name FROM tests", db.getConnection());


            db.OpenConnection();

            MySqlDataReader read = command.ExecuteReader();

            while (read.Read())
            {

                listBox1.Items.Add(Convert.ToString(read["Name"]));
            }

            db.CloseConnection();
        }
    }
}
