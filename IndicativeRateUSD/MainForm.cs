using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace IndicativeRateUSD
{
    public partial class MainForm : Form
    {
        string[] my_deals;
        string[] usd_quote;
        string[] output_quote;

        public MainForm()
        {
            InitializeComponent();
        }        

        private void btnDeals_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog open_file = new OpenFileDialog())
            {
                open_file.Filter = "Text files (*.txt) | *.txt";

                if (open_file.ShowDialog() == DialogResult.OK)
                {
                    my_deals = File.ReadAllLines(open_file.FileName);
                    btnUSD.Enabled = true;
                }
                else { return; }

                output_quote = new string[my_deals.Length];                
            }
        }

        private void btnUSD_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog open_file = new OpenFileDialog())
            {
                open_file.Filter = "Text files (*.txt) | *.txt";

                if (open_file.ShowDialog() == DialogResult.OK)
                {
                    string[] input_quote = File.ReadAllLines(open_file.FileName);

                    usd_quote = new string[input_quote.Length / 2];

                    const int CONST_DATE = 1;
                    const int CONST_TIME = 2;
                    const int CONST_QUOTE = 3;
                    
                    string[] row_1;
                    string[] row_2;
                    string temp_row;

                    for (int i = input_quote.Length - 1, j = 0; i > 0; i = i - 2, j++)
                    {
                        row_1 = input_quote[i].Split(new char[] { '\t', ' ' });
                        row_2 = input_quote[i - 1].Split(new char[] { '\t', ' ' });

                        if (row_1[CONST_DATE] == row_2[CONST_DATE] && 
                            row_1[CONST_TIME] != row_2[CONST_TIME])
                        {
                            temp_row = string.Format("{0}\t{1}\t{2}", row_1[CONST_DATE], 
                                                                        row_1[CONST_QUOTE], 
                                                                        row_2[CONST_QUOTE]);                            
                        }
                        else
                        {
                            MessageBox.Show(string.Format("Ошибка в индикативных курсах в строках {0} и {1}!!!", i - 1, i));
                            break;
                        }

                        usd_quote[j] = temp_row;
                    }

                    btnResult.Enabled = true;

                }
                else { return; }
            }
        }

        private void btnResult_Click(object sender, EventArgs e)
        {
            const int CONST_DATE = 0;
            const int CONST_TIME = 1;
            const int CONST_QUOTE_1 = 1;
            const int CONST_QUOTE_2 = 2;

            DateTime date_deal;
            DateTime date_usd;
            DateTime time_deal;

            string[] temp_deal;
            string[] temp_usd;
            string temp_str = "";

            bool flag;

            for (int i = 0; i < my_deals.Length; i++)
            {
                flag = true;

                temp_deal = my_deals[i].Split(new char[] { '\t' });
                date_deal = DateTime.Parse(temp_deal[CONST_DATE]);

                for (int j = 0; j < usd_quote.Length; j++)
                {
                    if (flag)
                    {
                        temp_usd = usd_quote[j].Split(new char[] { '\t' });
                        date_usd = DateTime.Parse(temp_usd[CONST_DATE]);

                        if (date_deal == date_usd)
                        {
                            time_deal = DateTime.Parse(temp_deal[CONST_TIME], null, System.Globalization.DateTimeStyles.NoCurrentDateDefault);

                            if (time_deal < new DateTime(1, 1, 1, 14, 0, 0))
                            {
                                if (j == 0)
                                {
                                    MessageBox.Show("Добавте более ранние данные об индикативных курсах.");
                                    break;
                                }
                                else
                                {
                                    temp_usd = usd_quote[j - 1].Split(new char[] { '\t' });
                                    temp_str = string.Format("{0}\t{1}", temp_deal[CONST_DATE], temp_usd[CONST_QUOTE_2]);
                                    flag = false;//прервать внутренний цикл

                                }
                            }
                            else if (time_deal > new DateTime(1, 1, 1, 18, 45, 0))
                            {
                                temp_str = string.Format("{0}\t{1}", temp_deal[CONST_DATE], temp_usd[CONST_QUOTE_2]);
                                flag = false;//прервать внутренний цикл
                            }

                            else
                            {
                                temp_str = string.Format("{0}\t{1}", temp_deal[CONST_DATE], temp_usd[CONST_QUOTE_1]);
                                flag = false;//прервать внутренний цикл
                            }

                        }
                        
                    }
                    
                }

                if (flag)//если во внутреннем цикле флаг так и остался true значит нужные данные не найдены
                {
                    MessageBox.Show(string.Format("Не найдены индикативные курсы для сделки № {0}." +
                                                    "\nИли неправильный формат входных данных.", i + 1));
                    btnUSD.Enabled = false;
                    btnResult.Enabled = false;
                    return;
                }

                output_quote[i] = temp_str;
            }

            using (SaveFileDialog save_file = new SaveFileDialog())
            {
                save_file.Filter = "Text files (*.txt) | *.txt";

                if (save_file.ShowDialog() == DialogResult.OK)
                {
                    File.WriteAllLines(save_file.FileName, output_quote);
                    Close();
                }
                else { Close(); }
            }
        }
    }
}
