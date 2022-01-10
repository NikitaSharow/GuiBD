using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace WinFormProject
{
    public partial class change : Form
    {
        public change()
        {
            InitializeComponent();
        }

        private void Get_Load(object sender, EventArgs e)
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

                string sql = "SHOW COLUMNS FROM `project`.`cars`";
                MySqlCommand command = new MySqlCommand(sql, myConnection);
                MySqlDataReader reader = command.ExecuteReader();

                int y = 0;
                CheckBox[] chBox = new CheckBox[10];
                while (reader.Read())
                {
                    y++;
                    chBox[y] = new CheckBox
                    {
                        AutoSize = true,
                        Location = new Point(100, 200 + y * 25),
                        Text = reader[0].ToString()
                    };
                    this.Controls.Add(chBox[y]);
                }
                reader.Close();
                myConnection.Close();
            }
            catch { }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            string bd = "project";
            string host = "localhost";
            string user = "root";
            string pass = "";

            string myConnectionString = "Database=" + bd + ";Data Source=" + host + ";User Id=" + user + ";Password=" + pass;
            MySqlConnection myConnection = new MySqlConnection(myConnectionString);
            myConnection.Open();

            string sql = "UPDATE `project`.`cars` SET `" + comboBox1.Text +"` = '" + textBox1.Text + "' WHERE `cars`.`id` =" + textBox2.Text + ";";
            MySqlCommand com = new MySqlCommand(sql, myConnection);

            int ok = com.ExecuteNonQuery();
            if (ok > 0) MessageBox.Show("Данные обновленны!");
            else MessageBox.Show("При изменение данных произошла ошибка!");

            myConnection.Close();
        }
    }
}
