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

        string[] dataGrifFild = new string[20];

        private void change_Load(object sender, EventArgs e)
        {
            try
            {

                string myConnectionString = "Database=" + Program.bd + ";Data Source=" + Program.host + ";User Id=" + Program.user + ";Password=" + Program.pass;
                MySqlConnection myConnection = new MySqlConnection(myConnectionString);
                myConnection.Open();

                string sql = "SHOW COLUMNS FROM `project`.`cars`";
                MySqlCommand command = new MySqlCommand(sql, myConnection);
                MySqlDataReader reader = command.ExecuteReader();

                
                int kol = 0;
                while (reader.Read())
                {
                    dataGrifFild[kol] = reader[0].ToString();
                    kol++;
                }
                myConnection.Close();

                myConnectionString = "Database=" + Program.bd + ";Data Source=" + Program.host + ";User Id=" + Program.user + ";Password=" + Program.pass;
                myConnection = new MySqlConnection(myConnectionString);
                myConnection.Open();

                sql = "SELECT * FROM `cars`";
                MySqlCommand com = new MySqlCommand(sql, myConnection);
                reader = com.ExecuteReader();

                for (int i = 0; i < kol; i++)
                    dataGridView1.Columns.Add(dataGrifFild[i], dataGrifFild[i]);

                List<string[]> data = new List<string[]>();

                while (reader.Read())
                {
                    data.Add(new string[kol]);

                    for (int i = 0; i < kol; i++)
                        data[data.Count - 1][i] = reader[i].ToString();
                }
                
                reader.Close();
                myConnection.Close();
                foreach (string[] s in data)
                    dataGridView1.Rows.Add(s);
            }
            catch (Exception ex)
            { MessageBox.Show("Ошибка!" + ex); }
        }

        int row;
        int column;

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            row = e.RowIndex;
            column = e.ColumnIndex;
        }
        private void Button1_Click(object sender, EventArgs e)
        {
            try 
            {
                //MessageBox.Show(dataGrifFild[column]);
                //MessageBox.Show(dataGridView1[0, row].Value.ToString());
                //MessageBox.Show(dataGridView1[column, row].Value.ToString());

                string myConnectionString = "Database=" + Program.bd + ";Data Source=" + Program.host + ";User Id=" + Program.user + ";Password=" + Program.pass;
                MySqlConnection myConnection = new MySqlConnection(myConnectionString);
                myConnection.Open();

                string sql = "UPDATE `project`.`cars` SET `" + dataGrifFild[column] 
                    + "` = '" + dataGridView1[column, row].Value 
                    + "' WHERE `cars`.`id` = " + dataGridView1[0, row].Value;
                MySqlCommand com = new MySqlCommand(sql, myConnection);

                com.ExecuteNonQuery();
                MessageBox.Show("Данные обновленны! ");

                myConnection.Close();
            }
            catch (Exception ex)
            { MessageBox.Show("Ошибка!" + ex); }
        }
    }
}
