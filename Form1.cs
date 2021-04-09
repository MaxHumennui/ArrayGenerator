using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TSPP
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        void printBoard(DataGridView dgv, int[,] board)
        {
            var columns = board.GetUpperBound(1) + 1; //Number of columns
            var rows = board.GetUpperBound(0) + 1;    //Number of rows

            //Add columns (name, text)
            try
            {
                for (int c = 0; c < columns; c++)
                {

                    dgv.Columns.Add($"{c + 1}", $"Ꭓ{c + 1}");
                    dgv.Columns[c].Width = 30;
                    dgv.Columns[c].SortMode = DataGridViewColumnSortMode.NotSortable;

                }
            }
            catch
            {
                MessageBox.Show("Сумма значень стовбців не повинна перевищувати 65535.");
            }
           

            //Add rows  
            for (int r = 0; r < rows; r++)
            {
                try
                {
                    //Slice 2d array and get the row
                    var row = Enumerable.Range(0, columns).Select(c => (object)board[r, c]).ToArray();
                    //Add the row
                    dgv.Rows.Add(row);
                }
                catch
                {
                    MessageBox.Show("ERROR: Неможливо добавити рядки поки нема стовбців. Спочатку необхідно добавити стовбці.");
                }
            }
        }

        private void Generate_Click(object sender, EventArgs e)
        {
            int n = Convert.ToInt32(Rows.Text);

            int m = Convert.ToInt32(Columns.Text);

            int[,] arr = new int[n, m];

            Random rnd = new Random();

            for (int i = 0; i < n; i ++)
            {
                for (int j = 0; j < m; j++)
                {
                    arr[i, j] = Convert.ToByte(rnd.Next(0, 100)); // random array value
                }
            }

            tabl.Rows.Clear();
            tabl.Columns.Clear();

            printBoard(tabl, arr);

            int marg;

            if (m > 3 && m <= 25)
            {
                marg = 30 * m + 17;
                tabl.Width = marg;

                marg = (800 - marg) / 2;
                tabl.Left = marg;
            }
            if(m <= 3 && m >= 0)
            {
                marg = 30 * m;
                tabl.Width = marg;

                marg = (800 - marg) / 2;
                tabl.Left = marg;
            }
            if (m >= 25)
            {
                marg = 30 * 25 + 17;
                tabl.Width = marg;

                marg = (800 - marg) / 2;
                tabl.Left = marg;
            }
        }

        private void ResultButton_Click(object sender, EventArgs e)
        {
            int[,] data = new int[tabl.RowCount, tabl.ColumnCount];
            try
            {
                for (int i = 0; i < tabl.RowCount; i++)
                {
                    for (int j = 0; j < tabl.ColumnCount; j++)
                    {
                        data[i, j] = Convert.ToInt32(tabl[j, i].Value);
                    }
                }
            }
            catch
            {
                MessageBox.Show("ERROR: Невірний формат введеня");
            }


            int temp;

            int[] results = new int[tabl.RowCount];

            for (int k = 0; k < tabl.RowCount; k++)
            {
                for (int i = 0; i < tabl.ColumnCount - 1; i++)
                {
                    for (int j = i + 1; j < tabl.ColumnCount; j++)
                    {
                        if (data[k,i] > data[k,j])
                        {
                            temp = data[k,i];
                            data[k,i] = data[k,j];
                            data[k,j] = temp;
                        }
                    }
                }
            }

            for (int i = 0; i < tabl.RowCount; i ++)
            {
                results[i] = data[i, tabl.ColumnCount-1] - data[i, 0];
            }

            Form2 newForm = new Form2(results);
            newForm.Show();
        }

        private void Rows_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;

            if (!Char.IsDigit(ch) && ch != 8)
            {
                e.Handled = true;
            }
        }

        private void tabl_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;

            if (!Char.IsDigit(ch) && ch != 8 && ch != '-')
            {
                e.Handled = true;
            }
        }
    }
}
