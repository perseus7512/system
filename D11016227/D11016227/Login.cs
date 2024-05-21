using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace D11016227
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        string user_name;
        int times = 0;
        string rating;
        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection cnn = new SqlConnection("data source=192.192.140.111;initial catalog=D11016227;user id=sa;password=2022Takming");
            cnn.Open();
            string select = "SELECT * FROM 員工資料表 WHERE 員工代號 = @employeeId AND 密碼 = @password";
            SqlCommand cmd = new SqlCommand(select, cnn);
            cmd.Parameters.AddWithValue("@employeeId", textBox1.Text);
            cmd.Parameters.AddWithValue("@password", textBox2.Text);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                user_name = dr["員工姓名"].ToString();
                rating = dr["權限"].ToString();
            }
            if (user_name == null)
            {
                times++;
                if (times == 3)
                {
                    MessageBox.Show("你登入失敗三次了!請確認您的帳號密碼。");
                    this.Dispose();
                }
                else
                {
                    MessageBox.Show("登入失敗!");
                }
            }
            else
            {
                
                MessageBox.Show("歡迎" + " " + user_name + " !! ");
                switch (rating)
                {
                    case "1":
                        Operations op = new Operations();
                        op.Show();
                        break;
                    case "5":
                        index ind = new index();
                        ind.Show();
                        break;
                }
                textBox1.Text = "";
                textBox2.Text = "";
                this.Hide();
            dr.Close();
            cmd.Dispose();
            cnn.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                button1_Click(sender, e);
        }
    }
}
