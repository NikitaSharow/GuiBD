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
    public partial class Del : Form
    {
        public Del()
        {
            InitializeComponent();
        }

        string[] dataGrifFild = new string[20];

        private void Del_Load(object sender, EventArgs e)
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
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            row = dataGridView1.CurrentCell.RowIndex;
            //MessageBox.Show(row.ToString());
            //MessageBox.Show(dataGridView1[0, row].Value.ToString());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try 
            {

                string myConnectionString = "Database=" + Program.bd + ";Data Source=" + Program.host + ";User Id=" + Program.user + ";Password=" + Program.pass;
                MySqlConnection myConnection = new MySqlConnection(myConnectionString);
                myConnection.Open();

                string sql = "DELETE FROM `project`.`cars` WHERE `cars`.`id` = " 
                    + dataGridView1[0, row].Value.ToString();
                MessageBox.Show(sql);

                MySqlCommand command = new MySqlCommand(sql, myConnection);
                var result = MessageBox.Show("Вы уверены?", "", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                { MessageBox.Show("Данные удалены!"); command.ExecuteNonQuery();}

                
            }
            catch (Exception ex)
            { MessageBox.Show("Ошибка!" + ex); }

        }
    }
}
