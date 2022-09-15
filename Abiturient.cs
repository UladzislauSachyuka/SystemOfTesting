using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Common;
using MySqlX.XDevAPI.Relational;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SystemOfTesting
{
    

    public partial class Abiturient : Form
    {
       
        string passport;

        public Abiturient(string passport)
        {
            InitializeComponent();
            this.passport = passport;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Hide();
            Login form = new Login();
            form.Show();

        }

        private void Abiturient_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string text = listBox1.GetItemText(listBox1.SelectedItem);
            
            Database db = new Database();

            MySqlCommand command = new MySqlCommand("SELECT Path FROM tests WHERE Name = @test_name", db.getConnection());
            command.Parameters.AddWithValue("@test_name", text);


            db.OpenConnection();

            MySqlDataReader read = command.ExecuteReader();
            
            if (read.Read()) {


                Hide();
                Test form = new Test(Convert.ToString(read.GetString(0)), text ,passport);
                form.Show();
                /* Hide();
                Test form = new Test(Convert.ToString(read.GetString(0)));
                form.Closed += (s, args) => Close();
                form.Show();*/
            }
            else
            {
                MessageBox.Show("Error");
            }
            db.CloseConnection();
            
        }

        private void Abiturient_Load(object sender, EventArgs e)
        {

            dataGridView1.DefaultCellStyle.Font = new Font("Arial", 10);
            dataGridView1.DefaultCellStyle.ForeColor = Color.Black;
            try
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {


            Database db = new Database();
            
            db.OpenConnection();

            MySqlCommand command2 = new MySqlCommand("SELECT * FROM users", db.getConnection());
            MySqlDataReader read2 = command2.ExecuteReader();

            DataTable dt = new DataTable();
            dt.Load(read2);

            int columns = dt.Columns.Count;
            
            MySqlCommand command = new MySqlCommand("SELECT * FROM users WHERE passport = @passport", db.getConnection());
            
            
            
            command.Parameters.AddWithValue("@passport",passport);

            MySqlDataReader read = command.ExecuteReader();

            read.Read();
            int column_counter = 4;
            DataColumnCollection columns2 = dt.Columns;
            
           
            DataTable table = new DataTable();
            table.Columns.Add("Название теста");
            table.Columns.Add("Результат");
            while (columns-- != 4)
            {
                if (!read.IsDBNull(column_counter))
                {
                    DataRow dr = table.NewRow();
                    dr["Название теста"] = dt.Columns[column_counter].ColumnName;
                    dr["Результат"] = read.GetString(column_counter);
                    table.Rows.Add(dr);

                }
                column_counter++;
            }
            dataGridView1.DataSource = table;
            db.CloseConnection();
        }
    }
}
