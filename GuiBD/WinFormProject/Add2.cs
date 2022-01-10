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
    public partial class Add2 : Form
    {
        public Add2()
        {
            InitializeComponent();
        }

        string[] dataGrifFild = new string[20];
        int kol = 0;

        private void Add2_Load(object sender, EventArgs e)
        {
            string myConnectionString = "Database=" + Program.bd + ";Data Source=" + Program.host + ";User Id=" + Program.user + ";Password=" + Program.pass;
            MySqlConnection myConnection = new MySqlConnection(myConnectionString);
            myConnection.Open();

            string sql = "SHOW COLUMNS FROM `project`.`cars`";
            MySqlCommand command = new MySqlCommand(sql, myConnection);
            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                dataGrifFild[kol] = reader[0].ToString();
                kol++;
            }
            for (int i = 0; i < kol; i++)
                dataGridView1.Columns.Add(dataGrifFild[i], dataGrifFild[i]);
            myConnection.Close();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                string myConnectionString = "Database=" + Program.bd + ";Data Source=" + Program.host + ";User Id=" + Program.user + ";Password=" + Program.pass;
                MySqlConnection myConnection = new MySqlConnection(myConnectionString);
                myConnection.Open();

                string sql = "INSERT INTO `cars` (" ;
                for (int i = 0; i < kol; i++)
                {
                    if (i == 0)
                        sql = sql + "`" + dataGrifFild[i] + "`";
                    else 
                        sql = sql + ",`" + dataGrifFild[i] + "`";
                }
                sql += ") VALUES (";

                DataGridViewRow row;

                for (int c = 0; c < dataGridView1.Rows.Count - 1; c++)
                {
                    row = dataGridView1.Rows[c];
                    for (int i = 0; i < kol; i++)
                    {
                        if (c == 0 && i == 0 && row.Cells[i].Value != null)
                            sql = sql + "'" + row.Cells[i].Value.ToString() + "'";
                        else if (row.Cells[i].Value != null)
                            sql = sql + " ,'" + row.Cells[i].Value.ToString() + "'";
                        else
                            sql = sql + ", ''";
                    }
                }
                sql += ");";
                MessageBox.Show(sql);

                MySqlCommand com = new MySqlCommand(sql, myConnection);
                com.ExecuteNonQuery();
                MessageBox.Show("Данные добавлены!");

                myConnection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка! " + ex);
            }
        }

        
    }
}
