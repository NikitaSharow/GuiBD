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

namespace WinFormProject
{
    public partial class Form2 : Form
    {
        bool Admin = false;

        public Form2()
        {
            InitializeComponent();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form Form1 = new Form1();
            Form1.Show();
            this.Close();
        }

        private void Form2_FormClosed_1(object sender, FormClosedEventArgs e)
        {
            Form Form1 = Application.OpenForms[0];
            Form1.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form get = new Get();
            get.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (Admin)
            { Form change = new change(); change.Show(); }
            else
                MessageBox.Show("Недостаточно прав");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (Admin)
            { Form Add2 = new Add2(); Add2.Show(); }
            else
                MessageBox.Show("Недостаточно прав");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                string bd = "project";
                string host = "localhost";
                string user = "root";
                string pass = "";

                string myConnectionString = "Database=" + bd + ";Data Source=" + host + ";User Id=" + user + ";Password=" + pass;
                MySqlConnection myConnection = new MySqlConnection(myConnectionString);
                myConnection.Open();

                string sql = "SELECT `root` FROM `project`.`usersdata` WHERE `login` = '"+ textBox1.Text + "' AND `password` ='"+ textBox2.Text + "' ";
                MySqlCommand command = new MySqlCommand(sql, myConnection);
                MySqlDataReader reader = command.ExecuteReader();
                
                if (reader.Read())
                {
                    MessageBox.Show("Успешно ");
                    if (reader[0].ToString() == "admin")
                    { MessageBox.Show("Вам доступны права админа"); Admin = true; }
                }
                else
                    MessageBox.Show("Неверный логин или пароль");
            }

            catch (Exception ex)
            { MessageBox.Show("Ошибка!" + ex); }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (Admin)
            {}
            else
                MessageBox.Show("Недостаточно прав");
        }
    }
}
