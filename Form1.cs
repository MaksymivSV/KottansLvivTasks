using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KottansLvivTasks
{
    public partial class Form1 : Form
    {
        
        public string GetCreditCardVendor(string CreditNumb)
        {
            /*The bin list and bin range used on binlist by the major card schemes are: 
            - American Express	34, 37
            - Maestro	50, 56-69
            - Visa bin list: Card numbers start with a 4.
            - MasterCard bin list: Card numbers start with the numbers 51 through 55.
            - JCB bin list: Card numbers begin with 3528-3589. */
            string vendor = "Unknown";
            int CreditN = Int32.Parse(CreditNumb.Replace(" ", string.Empty).Substring(0, 6));
            //String.StartsWith()
            switch (CreditN / 100000)
            {
                case 3:
                    {
                        if ((CreditN / 100) >= 3528 && (CreditN / 100) <= 3589) vendor = "JBC";
                        else if ((CreditN / 10000) == 34 || (CreditN / 10000) == 37) vendor = "American Express";
                        break;
                    }
                case 4:
                    {
                        vendor = "Visa";
                        break;
                    }
                case 5:
                    {
                        if ((CreditN / 10000) >= 51 && (CreditN / 10000) <= 55) vendor = "MasterCard";
                        else vendor = "Maestro";
                        break;
                    }
                case 6:
                    {
                        vendor = "Maestro";
                        break;
                    }
                default: break;
            }
           // textBox1.Text = CreditN.ToString();
            return vendor;
        }

        public bool CompareRangeWithBin(int Digit, string vendor)
        {
            return false;
        }
        public bool CompareRangeWithBin(int start, int finish, string vendor)
        {
            return false;
        }

        public bool IsCreditCardNumberValid(string CreditNubm)
        {
            CreditNubm = CreditNubm.Replace(" ", string.Empty);
            int k = 0, sum = 0;
            for (int i = CreditNubm.Length - 1; i >= 0; i--)
            {
                k++;
                if (k % 2 == 0)
                {
                    int CurrentNumb = ((int)Char.GetNumericValue(CreditNubm[i]) * 2);
                    sum += (CurrentNumb > 9) ? (CurrentNumb - 9) : (CurrentNumb);
                }
                else
                {
                    sum += (int)Char.GetNumericValue(CreditNubm[i]);
                    
                }

            }
            return (sum%10 == 0);
        }

        public string GenerateNextCreditCardNumber(string CreditNumb)
        {
            Int64 NextNumb = Int64.Parse(CreditNumb);
            do
                {
                    NextNumb += 1;
                }
            while(!IsCreditCardNumberValid(NextNumb.ToString()));
            return NextNumb.ToString();
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //string test = "123456";
            //label1.Text = textBox1.Text.StartsWith("23").ToString();
            label2.Text = (IsCreditCardNumberValid(textBox1.Text))?"Valid":"Null";
            label1.Text = (GetCreditCardVendor(textBox1.Text));
            if (label1.Text != "Unknown") button2.Enabled = true; else button2.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = GenerateNextCreditCardNumber(textBox1.Text);
        }
    }
}
