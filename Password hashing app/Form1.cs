using System;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.IO;

namespace Password_hashing_app
{
    public partial class Form1 : Form
    {


        static string password;
        static string checkPass;
        static byte[] hashValue;
        static byte[] checkValue;
        static string path = @"hash.txt";



        public Form1()
        {
            InitializeComponent();
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
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
        static void Hashing()
        {
         
            byte[] byteWord = Encoding.UTF8.GetBytes(password);
            SHA256 hash = SHA256.Create();
            hashValue = hash.ComputeHash(byteWord);
            File.WriteAllBytes(path, hashValue);

        }static void checkHash()
        {
            
            byte[] checkByte = Encoding.UTF8.GetBytes(checkPass);
            SHA256 checkSHA = SHA256.Create();
            checkValue = checkSHA.ComputeHash(checkByte);

            compareHash();


        }

        static void compareHash()
        {
            byte[] oValue = File.ReadAllBytes(path);
            bool check = Enumerable.SequenceEqual(oValue, checkValue);
            if (check == true)
            {
                MessageBox.Show("The password is correct!");
            }
            else
            {
                MessageBox.Show("The password is incorrect! Please try again.");
               
            }


        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }
    }
}
