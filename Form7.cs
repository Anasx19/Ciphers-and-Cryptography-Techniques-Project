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
    public partial class Form7 : Form
    {
        public List<int> factors = new List<int>();
        public List<int> pk = new List<int>();
        public int primeflag = 0, primeflag2 = 0, p, q, n, e = 0, k = 0, msg;
        float phi = 0;
        double d, c;
        decimal dec, enc;
        Random rr = new Random();

        public Form7()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (primeflag == 0 && primeflag2 == 0 && !String.IsNullOrEmpty(textBox3.Text))
            {
                //Decryption
                dec = power((int)d, (int)enc, n);
                richTextBox1.AppendText(Environment.NewLine + "Decrypted text = " + dec);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            clearAll();
            checkPrimes();

            if (primeflag == 0 && primeflag2 == 0 && !String.IsNullOrEmpty(textBox3.Text))
            {
                //Generating public key
                n = p * q;
                //printDivisors(n, factors);

                rr = new Random();

                this.e = 7;
                //this.e = checkFactors(this.e, factors);

                phi = (p - 1) * (q - 1);
                while (this.e > 1 && this.e < phi)
                {
                    if (gcd(this.e, (int)phi) == 1)
                        break;
                    else
                        this.e++;
                }

                for (int i = 0; i < 2; i++)
                {
                    if (i == 0)
                        pk.Add(this.e);
                    else
                        pk.Add(n);
                }

                for (int i = 0; i < pk.Count; i++)
                {
                    if (i == 0)
                        richTextBox1.AppendText("Public Key= e: " + pk[i].ToString());
                    else
                        richTextBox1.AppendText(" n: " + pk[i].ToString());
                }

                //Generating private key
                d = InverseModulo(this.e, (int)phi);
                richTextBox1.AppendText(Environment.NewLine + "Private Key= d: " + d + " n: " + n);

                //Encryption
                msg = Convert.ToInt32(textBox3.Text);
                enc = power(this.e, msg, n);
                richTextBox1.AppendText(Environment.NewLine + "Encrypted text = " + enc);


            }
            else
                MessageBox.Show("A field is missing");

        }

        private void button2_Click(object sender, EventArgs e)
        {
            clearAll();
            textBox1.Clear(); textBox2.Clear(); textBox3.Clear(); richTextBox1.Clear();

        }
        public void checkPrimes()
        {
            p = Convert.ToInt32(textBox1.Text);
            q = Convert.ToInt32(textBox2.Text);
            int n, i, m = 0;

            n = p;
            m = n / 2;

            for (i = 2; i <= m; i++)
            {
                if (n % i == 0)
                {
                    primeflag = 1;
                    break;
                }
            }

            n = 0;
            n = q;
            m = n / 2;

            for (i = 2; i <= m; i++)
            {
                if (n % i == 0)
                {
                    primeflag2 = 1;
                    break;
                }
            }

            if (primeflag == 1 && primeflag2 == 0)
            {
                MessageBox.Show(p + " is not a prime number");
            }

            if (primeflag2 == 1 && primeflag == 0)
            {
                MessageBox.Show(q + " is not a prime number");
            }
            if (primeflag == 1 && primeflag2 == 1)
            {
                MessageBox.Show("Both " + p + " and " + q + " are not prime numbers");
            }
        }

        public decimal power(int e, int msg, int n)
        {
            List<int> k = new List<int>();
            List<int> mod = new List<int>();
            List<int> tempo = new List<int>();
            k.Clear();
            mod.Clear();
            double x = 1, v = 1, ans2 = 1;

            int ans;
            int h = 0;
            int j = 0;
            for (int i = 1; i <= e; i *= 2)
            {
                if (i < 16)
                {
                    k.Add(i);
                }
                else
                {
                    j = i / 2;
                    while (true)
                    {
                        if (j >= 16)
                        {
                            j /= 2;
                        }
                        else
                            break;
                    }

                    for (int z = j; z <= i; z += j)
                    {


                        if (j <= i)
                        {
                            k.Add(j);
                        }
                    }
                }
                h += i;
            }
            for (int i = 0; i < k.Count; i++)
            {
                //MessageBox.Show("My powers:" + k[i]);
                v = Math.Pow(msg, k[i]);
                v = v % n;
                x *= v;
                mod.Add((int)v);
                //tempo.Add((int)v);
            }

            if (h == e)
            {
                x = x % n;
                return (int)x;
            }

            if (h > e)
            {
                for (int i = 0; i < k.Count; i++)
                {
                    int temp = h;
                    temp -= k[i];
                    if (temp == e)
                    {
                        mod.RemoveAt(i);
                        k.RemoveAt(i);
                        break;
                    }
                }
            }

            for (int i = 0; i < mod.Count; i++)
            {
                ans = mod[i];
                //MessageBox.Show("Power: "+k[i]+ " Answer: "+ans);
                ans2 *= ans;
                //MessageBox.Show(" ------------: " + ans2);
            }

            x = ans2 % n;
            return (decimal)x;

        }

        public int gcd(int a, int h)
        {
            int temp;
            while (true)
            {
                temp = a % h;
                if (temp == 0)
                    return h;
                a = h;
                h = temp;
            }
        }

        static void printDivisors(int n, List<int> f)
        {
            for (int i = 1; i <= n; i++)
            {
                if (n % i == 0)
                {
                    f.Add(i);
                }
            }
        }

        public int checkFactors(int n, List<int> f)
        {
            rr = new Random();
            n = rr.Next();
            for (int i = 0; i < f.Count; i++)
            {
                if (n == f[i])
                {
                    n = rr.Next();
                    i = 0;
                }
            }
            return n;
        }

        void clearAll()
        {
            factors.Clear();
            pk.Clear();
            primeflag = 0; primeflag2 = 0; p = 0; q = 0; n = 0; e = 0; k = 0; msg = 0; phi = 0; d = 0; c = 0;

        }

        public int InverseModulo(int a, int n)
        {

            int i = n, v = 0, d = 1;
            int t, x;
            while (a > 0)
            {
                t = i / a;
                x = a;
                a = i % x;
                i = x;
                x = d;
                d = v - t * x;
                v = x;
            }
            v %= n;

            if (v < 0)
                v += n;
            
            return v;

        }
    }
}
