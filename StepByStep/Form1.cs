using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StepByStep
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            database.Create_db();
            ara();
            textBox3.Visible = false;
            button4.Enabled =false;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Text="yavuz.akan@gmail.com";
        }

        private void button1_Click(object sender, EventArgs e)
        {
 



            using (var fbd = new OpenFileDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.FileName))
                {

                    textBox1.Text = fbd.FileName.ToString();

                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string data1 = textBox1.Text;
            string data2 = textBox2.Text;
            string data3 = "0";
            string id = textBox3.Text;
            string control = button2.Text;



            if (data1 != ""  && data2 !="" && data3 !="")
            {
                if (control=="Save")
                {
                    database.add(data1, data2, data3);
                    textBox1.Text="";
                    textBox2.Text="";

                    ara();
                }
                if (control=="Update")
                {

                    database.update(data1, data2, data3, id);
                    textBox1.Text="";
                    textBox2.Text="";
                    textBox3.Text="0";
                   
                    ara();

                }

            }
            else
            {
                MessageBox.Show("Error");
            }





        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Sure", "Delete All ?", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                try
                {


                    database.sil();
                    ara();


                }
                catch
                {

                }
            }
            else if (dialogResult == DialogResult.No)
            {

                //do something else
            }
        }


        public void ara()
        {
            string ara = textBox1.Text;
            dataGridView1.Rows.Clear();
            string path = "texttomp3.db";
            string cs = @"URI=file:"+Application.StartupPath+"\\texttomp3.db";
            var con = new SQLiteConnection(cs);
            SQLiteDataReader dr;
            con.Open();

            //string stm = "select * FROM data ORDER BY id ASC  ";
            //SELECT * FROM (SELECT * FROM graphs WHERE sid=2 ORDER BY id DESC LIMIT 10) g ORDER BY g.id
            string stm = "select * FROM data ";
            var cmd = new SQLiteCommand(stm, con);
            dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                dataGridView1.Rows.Insert(0, dr.GetValue(0).ToString(), dr.GetValue(1).ToString(), dr.GetValue(2).ToString(), dr.GetValue(3).ToString());

            }

            con.Close();

            this.dataGridView1.AllowUserToAddRows = false;


            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            //dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.ReadOnly = true;

            // dataGridView1.Columns[0].Visible = false;






        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow dataGridViewRow = dataGridView1.Rows[e.RowIndex];


                textBox1.Text = dataGridViewRow.Cells["Column1"].Value.ToString();
                textBox2.Text = dataGridViewRow.Cells["Column2"].Value.ToString();
                textBox3.Text = dataGridViewRow.Cells["Column0"].Value.ToString();


            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (textBox3.Text=="0")
            {
                button2.Text="Save";
                button4.Enabled =false;

            }
            else
            {
                button2.Text="Update";
                button4.Enabled =true;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Sure", "Delete this ?", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                try
                {

                    string id = textBox3.Text;
                    database.delete(id);
                    ara();
                    textBox1.Text="";
                    textBox2.Text="";
                    textBox3.Text="0";

                }
                catch
                {

                }
            }
            else if (dialogResult == DialogResult.No)
            {

                //do something else
            }
        }
    }
}
