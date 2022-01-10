using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace WinFormProject
{
    public partial class Get : Form
    {
        int y = 0;
        int k = 0;
        CheckBox[] chBox = new CheckBox[20];
        TextBox[] TxBox = new TextBox[20];
        public Get()
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

                while (reader.Read())
                {
                    if(reader[0].ToString() != "id")
                    {
                        chBox[y] = new CheckBox();
                        chBox[y].AutoSize = true;
                        chBox[y].Location = new Point(80, 120 + 30 * (y + 1));
                        chBox[y].Text = reader[0].ToString();
                        this.Controls.Add(chBox[y]);
                        TxBox[y] = new TextBox();
                        TxBox[y].AutoSize = true;
                        TxBox[y].Location = new Point(180, 120 + 30 * (y + 1));
                        this.Controls.Add(TxBox[y]);
                        y++;
                    }
                    
                }
                k = y;
                reader.Close();
                myConnection.Close();
            }
            catch (Exception ex)
            { MessageBox.Show("Ошибка!" + ex); }

            try
            {
                string bd = "project";
                string host = "localhost";
                string user = "root";
                string pass = "";

                string myConnectionString = "Database=" + bd + ";Data Source=" + host + ";User Id=" + user + ";Password=" + pass;
                MySqlConnection myConnection = new MySqlConnection(myConnectionString);
                myConnection.Open();

                string sql = "SHOW COLUMNS FROM `project`.`owner`";
                MySqlCommand command = new MySqlCommand(sql, myConnection);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    if(reader[0].ToString() != "id")
                    {
                        chBox[y] = new CheckBox();
                        chBox[y].AutoSize = true;
                        chBox[y].Location = new Point(80, 150 + 30 * (y + 1));
                        chBox[y].Text = reader[0].ToString();
                        this.Controls.Add(chBox[y]);
                        TxBox[y] = new TextBox();
                        TxBox[y].AutoSize = true;
                        TxBox[y].Location = new Point(180, 150 + 30 * (y + 1));
                        this.Controls.Add(TxBox[y]);
                        y++;
                    }

                }
                reader.Close();
                myConnection.Close();

                string strUrl = "http://localhost/NoImage.jpg";

                WebRequest webreq = WebRequest.Create(strUrl);
                WebResponse webres = webreq.GetResponse();
                Stream stream = webres.GetResponseStream();

                Image image = Image.FromStream(stream);
                stream.Close();

                pictureBox1.Image = image;
            }
            catch (Exception ex)
            { MessageBox.Show("Ошибка!" + ex); }

            for (int i = 0; i < y; i++)
                if (chBox[i].Text == "id")
                { 
                    chBox[i].Visible = false;
                }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            string bd = "project";
            string host = "localhost";
            string user = "root";
            string pass = "";

            dataGridView1.Rows.Clear();
            for (int i = 0; i < dataGridView1.Columns.Count; i++)
                dataGridView1.Columns.RemoveAt(0);
            try
            {
                string myConnectionString = "Database=" + bd + ";Data Source=" + host + ";User Id=" + user + ";Password=" + pass;
                MySqlConnection myConnection = new MySqlConnection(myConnectionString);
                myConnection.Open();

                string sql = "SELECT `cars`.`id`";
                int kol = 1;
                bool where = false;
                bool firstTable = false;
                bool secondTable = false;
                string[] dataGrifFild = new string[20];

                for (int i = 0; i < k; i++)
                    if (chBox[i].Checked == true)
                    {
                        firstTable = true;
                        if (TxBox[i].Text != "")
                            where = true;
                        if (kol > 0) sql = sql + ", `cars`.`" + chBox[i].Text + "` ";
                        else sql = sql + " `cars`.`" + chBox[i].Text + "` ";
                        dataGrifFild[kol] = chBox[i].Text;
                        kol++;
                    }

                for (int i = k; i < y; i++)
                    if (chBox[i].Checked == true)
                    {
                        secondTable = true;
                        if (TxBox[i].Text != "")
                            where = true;
                        if (kol > 0) sql = sql + ", `owner`.`" + chBox[i].Text + "` ";
                        else sql = sql + " `owner`.`" + chBox[i].Text + "` ";
                        dataGrifFild[kol] = chBox[i].Text;
                        kol++;
                    }
                sql += " FROM ";
                if (firstTable) sql = sql + "`project`.`cars`";
                if (secondTable && firstTable) sql = sql + " ,  ";
                if (secondTable) sql = sql + "`project`.`owner` ";

                if (secondTable && firstTable)
                    sql = sql + "WHERE `cars`.`owner` = `owner`.`id`";

                if (where)
                {
                    int k_where = 0;
                    sql = sql + " WHERE ";
                    for (int i = 0; i < y; i++)
                        if (chBox[i].Checked == true)
                        {
                            if (TxBox[i].Text != "")
                                if (k_where > 0) sql = sql + " and `" + chBox[i].Text + "` = '" + TxBox[i].Text + "'  ";
                                else { sql = sql + "`" + chBox[i].Text + "` = '" + TxBox[i].Text + "'  "; k_where++; }
                        }
                }

                /*
                
                SELECT `cars`.`model` , `cars`.`colour` , `owner`.`name` , `owner`.`surname` , `cars`.`god`
                FROM `project`.`cars` , `project`.`owner`
                WHERE `cars`.`owner` = `owner`.`id`
                
                 */

                MessageBox.Show(sql);

                MySqlCommand com = new MySqlCommand(sql, myConnection);
                MySqlDataReader reader = com.ExecuteReader();

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
            catch(Exception ex)
            {
                MessageBox.Show("Ошибка!" + ex);
            }


        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                int indexCurrent = dataGridView1.CurrentCell.RowIndex;
                string s = dataGridView1[0, indexCurrent].Value.ToString();

                string sql = "SELECT `pic` FROM `project`.`cars` WHERE `id`='";
                sql = sql + s + "'";

                string myConnectionString = "Database=" + Program.bd + ";Data Source=" + Program.host + ";User Id=" + Program.user + ";Password=" + Program.pass;
                MySqlConnection myConnection = new MySqlConnection(myConnectionString);
                myConnection.Open();

                MySqlCommand com = new MySqlCommand(sql, myConnection);
                MySqlDataReader reader = com.ExecuteReader();
                
                string strUrl = "http://localhost/NoImage.jpg";

                if (reader.Read())
                    strUrl = "http://localhost/" + reader[0].ToString();

                WebRequest webreq = WebRequest.Create(strUrl);
                WebResponse webres = webreq.GetResponse();
                Stream stream = webres.GetResponseStream();
                Image image;

                image = Image.FromStream(stream);

                pictureBox1.Image = image;
                stream.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка!" + ex);
            }
        }
    }
}
