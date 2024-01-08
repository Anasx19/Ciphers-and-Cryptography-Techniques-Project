using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace computer_security_project
{
    public partial class Form8 : Form
    {
        public int z, F1 = 0, F2 = 0;
        int i = 0;
        int ct;
        int[] S = new int[8];
        List<int> T = new List<int>();
        List<int> K = new List<int>();
        List<int> c = new List<int>();
        List<int> p = new List<int>();
        List<int> plain = new List<int>();
        List<int> CipherBinary = new List<int>();
        List<int> Ciphertext = new List<int>();
        int[,] MatBits = new int[8, 3] { { 0, 0, 0 }, { 0, 0, 1 }, { 0, 1, 0 }, { 0, 1, 1 }, { 1, 0, 0 }, { 1, 0, 1 }, { 1, 1, 0 }, { 1, 1, 1 } };

        public Form8()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Please Enter The Plaintext");
                F1 = 1;
                F2 = 1;
            }

            if (textBox2.Text == "")
            {
                MessageBox.Show("Please Enter The Key");
                F1 = 1;
                F2 = 1;
            }

            if (F2 == 0)
            {
                if (textBox1.Text.Length < 4 || textBox1.Text.Length > 4)
                {
                    MessageBox.Show("Plaintext must be 4 bits");
                    F1 = 1;
                }

                if (textBox2.Text.Length < 4 || textBox2.Text.Length > 4)
                {
                    MessageBox.Show("Key must be 4 bits");
                    F1 = 1;
                }
            }


            //// State Vector ////
            if (F1 == 0)
            {
                richTextBox1.AppendText("State Vector = S [");
                for (int i = 0; i < 8; i++)
                {
                    S[i] = i;
                    richTextBox1.AppendText(S[i].ToString() + " ");
                }
                richTextBox1.AppendText("]");


                //// Temporary Vector ////
                richTextBox1.AppendText(Environment.NewLine + "Temporary Vector = T [");
                for (int i = 0; i < textBox2.Text.Length * 2; i++)
                {
                    if (Char.IsNumber(textBox2.Text[z]))
                    {
                        T.Add(Int32.Parse(textBox2.Text[z].ToString()));
                    }

                    z++;

                    if (i == 3)
                    {
                        z = 0;
                    }
                    richTextBox1.AppendText(T[i].ToString() + " ");
                }
                richTextBox1.AppendText("]");


                //// Put Plaintext In List ////
                for (int i = 0; i < textBox1.Text.Length; i++)
                {
                    if (Char.IsNumber(textBox1.Text[i]))
                    {
                        plain.Add(Int32.Parse(textBox1.Text[i].ToString()));
                    }
                }


                //// Step 2: Initial Permutation////
                int j = 0;
                int Swap = 0;
                richTextBox1.AppendText(Environment.NewLine);
                for (int i = 0; i <= 7; i++)
                {
                    j = (j + S[i] + T[i]) % 8;
                    Swap = S[i];
                    S[i] = S[j];
                    S[j] = Swap;
                    richTextBox1.AppendText(Environment.NewLine + "S = [");
                    for (int k = 0; k <= 7; k++)
                    {
                        richTextBox1.AppendText(S[k].ToString() + " ");
                    }
                    richTextBox1.AppendText("]");
                }
                richTextBox1.AppendText(Environment.NewLine);


                //// Step3: Encryption ////
                int t = 0;
                j = 0;
                for (int z = 0; z < 4; z++)
                {
                    i = (i + 1) % 8;
                    j = (j + S[i]) % 8;
                    Swap = S[i];
                    S[i] = S[j];
                    S[j] = Swap;
                    richTextBox1.AppendText(Environment.NewLine + "S = [");
                    for (int k = 0; k <= 7; k++)
                    {
                        richTextBox1.AppendText(S[k].ToString() + " ");
                    }
                    richTextBox1.AppendText("]");
                    t = (S[i] + S[j]) % 8;
                    //K[z] = S[t];
                    K.Add(S[t]);
                    XOR(K[z], plain[z]);

                }


                //// Plaintext ////
                richTextBox1.AppendText(Environment.NewLine);
                richTextBox1.AppendText(Environment.NewLine + "P = [");
                for (i = 0; i < 4; i++)
                {
                    richTextBox1.AppendText(plain[i].ToString() + " ");

                }
                richTextBox1.AppendText("]");


                //// Key ////
                richTextBox1.AppendText(Environment.NewLine + "K = [");
                for (i = 0; i < 4; i++)
                {
                    richTextBox1.AppendText(K[i].ToString() + " ");

                }
                richTextBox1.AppendText("]");


                //// Ciphertext ////
                richTextBox1.AppendText(Environment.NewLine + "C = [");
                for (int i = 0; i < 4; i++)
                {
                    richTextBox1.AppendText(Ciphertext[i].ToString() + " ");

                }
                richTextBox1.AppendText("]");
            }
        }
        public void XOR(int K, int z)
        {
            c.Clear();
            p.Clear();
            CipherBinary.Clear();
            int x = 0;
            int y = 0;
            int w = 0;
            int v = 0;
            for (int i = 0; i < 3; i++)
            {
                x = MatBits[K, i];
                c.Add(x);
            }

            for (int i = 0; i < 3; i++)
            {
                y = MatBits[z, i];
                p.Add(y);
            }

            for (int i = 0; i < 3; i++)
            {
                if (c[i] == p[i])
                {
                    w = 0;
                    CipherBinary.Add(w);
                }
                else
                {
                    w = 1;
                    CipherBinary.Add(w);
                }
            }


            for (int r = 0; r <= 7; r++)
            {
                for (int col = 0; col < 1; col++)
                {
                    if (CipherBinary[col] == MatBits[r, col] && CipherBinary[col + 1] == MatBits[r, col + 1] && CipherBinary[col + 2] == MatBits[r, col + 2])
                    {
                        v = r;
                        Ciphertext.Add(v);
                        ct++;
                    }
                }
            }
        }
    }
}
