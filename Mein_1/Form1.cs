using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;
using MeinClass;

namespace Mein_1
{
    public partial class Form1 : Form
    {
        Random random = new Random();
        DataSet dataSet = new DataSet();



        public Form1()
        {
            InitializeComponent();
            SqlConnection MeinConnection = Mein.CreateConnection();
            MeinConnection.Open();

            Mein.CreateTable("Students", dataSet);
            Mein.CreateTable("Specials", dataSet);

            DataTable   Students = dataSet.Tables[0],
                        Specials = dataSet.Tables[1];

            dataGridView1.DataSource = Students;
            dataGridView2.DataSource = Specials;

            dataSet.Relations.Add("Specials_Students", Specials.Columns[0], Students.Columns[4]);
            Students.PrimaryKey = new DataColumn[] { Students.Columns[0] };
            Specials.PrimaryKey = new DataColumn[] { Specials.Columns[0] };
            ForeignKeyConstraint Key = new ForeignKeyConstraint(Specials.Columns[0], Students.Columns[4]);
            Key.DeleteRule = Rule.Cascade;







            #region Добавление записей
            //SqlCommand MeineStudent = new SqlCommand();

            //MeineStudent = new SqlCommand("DELETE FROM Students", MeinConnection);
            //MeineStudent.ExecuteNonQuery();
            //string[] data = new string[5]
            //{
            //"INSERT INTO Students (Id, Name, Surname, Phone, Id_Special) Values ('1','" + CreateName() + "','" + CreateName() + "','" + CreatePhone() + "','" + CreateSpecials() + "')",
            //"INSERT INTO Students (Id, Name, Surname, Phone, Id_Special) Values ('2','" + CreateName() + "','" + CreateName() + "','" + CreatePhone() + "','" + CreateSpecials() + "')",
            //"INSERT INTO Students (Id, Name, Surname, Phone, Id_Special) Values ('3','" + CreateName() + "','" + CreateName() + "','" + CreatePhone() + "','" + CreateSpecials() + "')",
            //"INSERT INTO Students (Id, Name, Surname, Phone, Id_Special) Values ('4','" + CreateName() + "','" + CreateName() + "','" + CreatePhone() + "','" + CreateSpecials() + "')",
            //"INSERT INTO Students (Id, Name, Surname, Phone, Id_Special) Values ('5','" + CreateName() + "','" + CreateName() + "','" + CreatePhone() + "','" + CreateSpecials() + "')"
            //};
            //foreach (var a in data)
            //{
            //    MeineStudent = new SqlCommand(a, MeinConnection);
            //    MeineStudent.ExecuteNonQuery();
            //}

            //MeineStudent = new SqlCommand("DELETE FROM Specials", MeinConnection);
            //MeineStudent.ExecuteNonQuery();
            //string[] dataa = new string[4]
            //{
            //"INSERT INTO Specials (Id, Depatment, Special) Values ('1','Programming','PO')",
            //"INSERT INTO Specials (Id, Depatment, Special) Values ('2','Technical','ATP')",
            //"INSERT INTO Specials (Id, Depatment, Special) Values ('3','Economy','BU')",
            //"INSERT INTO Specials (Id, Depatment, Special) Values ('4','Programming','PI')"
            //};
            //foreach (var a in dataa)
            //{
            //    MeineStudent = new SqlCommand(a, MeinConnection);
            //    MeineStudent.ExecuteNonQuery();
            //}
            #endregion


            //dataSet.WriteXmlSchema(@"C:\Users\right\Documents\Visual Studio 2017\Projects\Mein_1\XML\Schema.xml");
            //dataSet.WriteXml(@"C:\Users\right\Documents\Visual Studio 2017\Projects\Mein_1\XML\Data.xml");

        }



        #region Генерация записей
        public string CreateName()
        {
            string Name = "";
            char letter;
            for(int i = 0; i < 8; i++)
            {
                if (i==0)
                {
                    letter = (char)random.Next((int)'A', (int)'Z');
                }
                else
                {
                    letter = (char)random.Next((int)'a', (int)'z');
                }
                Name += letter;
            }
            return Name;
        }

        public string CreatePhone()
        {
            string Name = "";
            for (int i = 0; i < 8; i++)
            {
                Name += "" + random.Next(0,10);
            }
            return Name;
        }

        public string CreateSpecials()
        {
            return "" + random.Next(1, 5);
        }
        #endregion

        private void timer1_Tick(object sender, EventArgs e)
        {
            textBox1.Text = "" + CreateName();
            textBox1.Text = "" + CreatePhone();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int j = Convert.ToInt32(textBox1.Text);
            for (int i = 0; i < dataGridView2.RowCount; i++)
            {
                if (Convert.ToInt32(dataGridView2.Rows[i].Cells[0].Value) == j)
                {
                    dataGridView2.Rows.Remove(dataGridView2.Rows[i]);
                }
            }
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentCell.ColumnIndex == 4)
            {
                int Value = Convert.ToInt32(dataGridView1.CurrentCell.Value);
                if (Value < 1 || Value > 4)
                {
                    dataGridView1.Rows.Remove(dataGridView1.CurrentRow);
                }
            }
        }


    }
}
