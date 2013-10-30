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
        string[] usd_quotes;
        string[] output_quotes;

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

                output_quotes = new string[my_deals.Length];                
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

                    usd_quotes = new string[input_quote.Length / 2];

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

                        usd_quotes[j] = temp_row;
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
            DateTime date_quote;
            DateTime time_deal;

            string[] deal;
            string[] quote;
            string temp = "";           

            for (int i = 0; i < my_deals.Length; i++)
            {
                deal = my_deals[i].Split(new char[] { '\t' });
                date_deal = DateTime.Parse(deal[CONST_DATE]);
                time_deal = DateTime.Parse(deal[CONST_TIME], null, 
                                            System.Globalization.DateTimeStyles.NoCurrentDateDefault);

                int j = 0;

                //ПОИСК НЕОБХОДИМОЙ ДАТЫ В ИНДИКАТИВНЫХ КУРСАХ

                while (true)
                {
                    if (j < usd_quotes.Length)
                    {
                        quote = usd_quotes[j].Split(new char[] { '\t' });
                        date_quote = DateTime.Parse(quote[CONST_DATE]);
                    }
                    else
                    {
                        MessageBox.Show(string.Format("Не найдены индикативные курсы для сделки № {0}." +
                                                    "\nИли неправильный формат входных данных.", i + 1));
                        btnUSD.Enabled = false;
                        btnResult.Enabled = false;
                        return;
                    }

                    if (date_deal != date_quote)
                        j++;
                    else
                        break;
                }

                //ПОИСК НЕОХОДИМОГО ИНДИКАТИВНОГО КУРСА НА ВЫБРАННУЮ ДАТУ

                if (time_deal < new DateTime(1, 1, 1, 14, 0, 0))
                {
                    if (j == 0)//т.к. необходимо обратиться к предыдущей дате, то возможно j-1=-1 
                    {
                        MessageBox.Show("Добавте более ранние данные об индикативных курсах.");
                        break;
                    }
                    else
                    {
                        quote = usd_quotes[j - 1].Split(new char[] { '\t' });
                        temp = string.Format("{0}\t{1}", deal[CONST_DATE], quote[CONST_QUOTE_2]);

                    }
                }
                else if (time_deal > new DateTime(1, 1, 1, 18, 45, 0))
                {
                    temp = string.Format("{0}\t{1}", deal[CONST_DATE], quote[CONST_QUOTE_2]);
                }

                else
                {
                    temp = string.Format("{0}\t{1}", deal[CONST_DATE], quote[CONST_QUOTE_1]);                    
                }               

                output_quotes[i] = temp;
                  
            }

            using (SaveFileDialog save_file = new SaveFileDialog())
            {
                save_file.Filter = "Text files (*.txt) | *.txt";

                if (save_file.ShowDialog() == DialogResult.OK)
                {
                    File.WriteAllLines(save_file.FileName, output_quotes);
                    Close();
                }
                else { Close(); }
            }
        }
    }
}
