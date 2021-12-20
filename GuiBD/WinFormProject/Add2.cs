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

        private void Button1_Click(object sender, EventArgs e)
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

                String s; String s2;// String s3; String s4;
                DataGridViewRow row;
                MessageBox.Show(dataGridView1.Rows.Count.ToString());
                for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                {
                    row = dataGridView1.Rows[i];
                    s = row.Cells[0].Value.ToString();
                    s2 = row.Cells[3].Value.ToString();
                    string sql = "INSERT INTO `cars` (`num`,`model`) VALUES (" + s + "," + s2 + ");";
                    MySqlCommand com = new MySqlCommand(sql, myConnection);
                    //MessageBox.Show(s);
                    com.ExecuteNonQuery();
                }

                myConnection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка! " + ex);
            }
        }

        private void Add2_Load(object sender, EventArgs e)
        {

        }
    }
}
