using System;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.IO;
using System.Reflection.Emit;


namespace Password_hashing_app
{
    public partial class Form1 : Form
    {

        static string password;
        static string checkPass;
        static byte[] hashValue;
        static byte[] checkValue;
        static string path = @"hash.txt";
        static string secret = @"secret.txt";



        public Form1()
        {
            InitializeComponent();
            

        }

        private void Form1_Load(object sender, EventArgs e)
        {

            TextBox2.Visible = false;
            textBox3.Visible = false;
            button2.Visible = false;
            button3.Visible = false;
            label2.Visible = false;
            label3.Visible = false;

            if (!File.Exists(path))
            {

                label1.Text = "Enter your new password:";
            }
            else
            {
                label1.Text = "Enter your password:";
            }


           
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            label1.Text = "Enter your password:";
            if (!File.Exists(path))
            {
                
                password = textBox1.Text;
                textBox1.Clear();
                MessageBox.Show("The password has been saved!");

                Hashing();
            }
            else
            {
                checkPass = textBox1.Text;
                textBox1.Clear();
                checkHash();
            }

        }
          
        void Hashing()
        {
         
            byte[] byteWord = Encoding.UTF8.GetBytes(password);
            SHA256 hash = SHA256.Create();
            hashValue = hash.ComputeHash(byteWord);
            File.WriteAllBytes(path, hashValue);

        } void checkHash()
        {
            
            byte[] checkByte = Encoding.UTF8.GetBytes(checkPass);
            SHA256 checkSHA = SHA256.Create();
            checkValue = checkSHA.ComputeHash(checkByte);

            compareHash();


        }

         void compareHash()
        {

            byte[] oValue = File.ReadAllBytes(path);
            bool check = Enumerable.SequenceEqual(oValue, checkValue);
            if (check == true)
            {
               MessageBox.Show("The password is correct!");
                Encrypt();

            }
            else
            {
                MessageBox.Show("The password is incorrect! Please try again.");
               
            }


        }

        private void label1_Click(object sender, EventArgs e)
        {
        }
        private void box_TextChanged(object sender, EventArgs e)
        {
        }

        public void Encrypt()
        {
            textBox1.Visible = false;
            button1.Visible = false;
            label1.Visible = false;
            TextBox2.Visible = true;
            textBox3.Visible = true;
            button2.Visible = true;
            button3.Visible = true;
            label2.Visible = true;
            label3.Visible = true;
         

        }

        private void button2_Click(object sender, EventArgs e)
        {
        Encryption enc = new Encryption(TextBox2.Text);
            enc.Encrypt();
            if(File.Exists(secret))
            {
                MessageBox.Show("Text encrypted successfully!");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Encryption decn = new Encryption(TextBox2.Text);
            decn.Decrypt();
            textBox3.Text = decn.dePass;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
