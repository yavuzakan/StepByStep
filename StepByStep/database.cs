using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StepByStep
{
    class database
    {
        public static void Create_db()
        {
            string path = "texttomp3.db";
            string cs = @"URI=file:"+Application.StartupPath+"\\texttomp3.db";

            if (!System.IO.File.Exists(path))
            {
                SQLiteConnection.CreateFile(path);
                using (var sqlite = new SQLiteConnection(@"Data Source="+ path))
                {
                    sqlite.Open();
                    string sql = "CREATE TABLE data (id INTEGER, data1 TEXT,  data2 TEXT, data3 TEXT , PRIMARY KEY(id AUTOINCREMENT))";
                    SQLiteCommand command = new SQLiteCommand(sql, sqlite);
                    command.ExecuteNonQuery();



                }

            }

        }

        public static void add(string data1, string data2, string data3)
        {
            try
            {
                string path = "texttomp3.db";
                string cs = @"URI=file:"+Application.StartupPath+"\\texttomp3.db";




                var con = new SQLiteConnection(cs);
                con.Open();
                var cmd = new SQLiteCommand(con);




                //"server=localhost;username=root;password=;database=follow";
                //string sql2 = "CREATE TABLE passwords (id INTEGER, info TEXT , username TEXT, password TEXT,  PRIMARY KEY(id AUTOINCREMENT))";
                cmd.CommandText = "INSERT INTO data(data1,data2,data3) VALUES(@data1,@data2,@data3)";

                cmd.Parameters.AddWithValue("@data1", data1);
                cmd.Parameters.AddWithValue("@data2", data2);
                cmd.Parameters.AddWithValue("@data3", data3);



                cmd.ExecuteNonQuery();

                System.IO.Directory.CreateDirectory(@data3);
            }
            catch (Exception e)
            {


            }




        }

        public static void sil()
        {

            string path = "texttomp3.db";
            string cs = @"URI=file:"+Application.StartupPath+"\\texttomp3.db";

            var con = new SQLiteConnection(cs);
            SQLiteDataReader dr;
            con.Open();

            //string stm = "select * FROM data ORDER BY id ASC  ";
            //SELECT * FROM (SELECT * FROM graphs WHERE sid=2 ORDER BY id DESC LIMIT 10) g ORDER BY g.id
            string stm = "delete from data";
            var cmd = new SQLiteCommand(stm, con);
            dr = cmd.ExecuteReader();

            stm = "delete from sqlite_sequence where name='data'";
            cmd = new SQLiteCommand(stm, con);
            dr = cmd.ExecuteReader();

            con.Close();



        }
        public static void delete(string id)
        {
            //DELETE FROM COMPANY WHERE ID = 7

            try
            {
                string path = "texttomp3.db";
                string cs = @"URI=file:"+Application.StartupPath+"\\texttomp3.db";



                DateTime bugun = DateTime.Now;


                var con = new SQLiteConnection(cs);
                con.Open();
                var cmd = new SQLiteCommand(con);



                string sql = "delete from data where id ="+id;

                cmd.CommandText = sql;
                cmd.Prepare();
                cmd.ExecuteNonQuery();
                MessageBox.Show("Ok.");

            }
            catch (Exception e)
            {


            }



        }
        public static void update(string data1, string data2, string data3, string id)
        {
            try
            {
                string path = "texttomp3.db";
                string cs = @"URI=file:"+Application.StartupPath+"\\texttomp3.db";



                DateTime bugun = DateTime.Now;


                var con = new SQLiteConnection(cs);
                con.Open();
                var cmd = new SQLiteCommand(con);



                string sql = "UPDATE data set data1='"+data1+"' ,data2='"+data2+"'   where id ="+id;

                cmd.CommandText = sql;
                cmd.Prepare();
                cmd.ExecuteNonQuery();
                MessageBox.Show("Ok.");




            }
            catch (Exception e)
            {


            }




        }


    }
   
}
