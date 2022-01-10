using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace WinFormProject
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string[] lines = File.ReadAllLines("../../../properties.txt");
            foreach (string line in lines)
            {
                string[] parts = line.Split(new string[] { "," }, StringSplitOptions.None);
                Program.bd = parts[0];
                Program.host = parts[1];
                Program.user = parts[2];
                Program.pass = parts[3];
            }

            try
            {
                string myConnectionString = "Database=" + Program.bd + ";Data Source=" + Program.host + ";User Id=" + Program.user + ";Password=" + Program.pass;
                MySqlConnection myConnection = new MySqlConnection(myConnectionString);
                myConnection.Open();
                MessageBox.Show("Подключение прошло успешно!");
                Form Form2 = new Form2();
                Form2.Show();
                this.Hide();
                myConnection.Close();
            }
            catch
            {
                MessageBox.Show("Ошибка!!!!!");
            }
            /*
            Console.WriteLine("Подключение прошло успешно!");

            // Выполнение комманд не требующих вывода информации

            string sql = "UPDATE `project`.`cars` SET `colour` = 'Красный' WHERE `cars`.`id` = 20;";
            MySqlCommand com = new MySqlCommand(sql, myConnection);


             Считывание
            string sql = "SELECT * FROM `project`.`cars`";
            MySqlCommand com = new MySqlCommand(sql, myConnection);
            MySqlDataReader reader = com.ExecuteReader();
            while (reader.Read())
            Console.WriteLine(reader[4].ToString());
             

            int ok = com.ExecuteNonQuery();
            if (ok > 0) Console.WriteLine("Данные обновленны!");
            else Console.WriteLine("При изменение данных произошла ошибка!");
            */
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
