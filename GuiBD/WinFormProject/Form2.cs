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
                string myConnectionString = "Database=" + Program.bd + ";Data Source=" + Program.host + ";User Id=" + Program.user + ";Password=" + Program.pass;
                MySqlConnection myConnection = new MySqlConnection(myConnectionString);
                myConnection.Open();

                string sql = "SELECT `root` FROM `project`.`usersdata` WHERE `login` = '"+ textBox1.Text + "' AND `password` ='"+ textBox2.Text + "' ";
                MySqlCommand command = new MySqlCommand(sql, myConnection);
                MySqlDataReader reader = command.ExecuteReader();
                
                if (reader.Read())
                {
                    MessageBox.Show("Успешно ");
                    if (reader[0].ToString() == "admin")
                    { 
                        MessageBox.Show("Вам доступны права админа"); 
                        Admin = true;
                        button7.Visible = true;
                        textBox4.Visible = true;
                        textBox3.Visible = true;
                        comboBox1.Visible = true;
                        label3.Visible = true;
                        label4.Visible = true;
                        label5.Visible = true;
                    }
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
            { Form Del = new Del(); Del.Show(); }
            else
                MessageBox.Show("Недостаточно прав");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                string myConnectionString = "Database=" + Program.bd + ";Data Source=" + Program.host + ";User Id=" + Program.user + ";Password=" + Program.pass;
                MySqlConnection myConnection = new MySqlConnection(myConnectionString);
                myConnection.Open();

                string sql = "INSERT INTO `usersdata` (`login`,`password`,`root`) VALUES ('" 
                    + textBox4.Text + "','" + textBox3.Text + "','" + comboBox1.Text + "');";
            
                MessageBox.Show(sql);

                MySqlCommand com = new MySqlCommand(sql, myConnection);
                com.ExecuteNonQuery();
                MessageBox.Show("Пользователь добавлен!");

                myConnection.Close();
            }
            catch (Exception ex)
            { MessageBox.Show("Ошибка!" + ex); }
        }
    }
}
