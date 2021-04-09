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
    public partial class Form2 : Form
    {
        public Form2(int[] res)
        {
            InitializeComponent();

            for (int i = 0; i < res.Length; i++)
            {
                label1.Text += "Рядок " + (i+1).ToString() +" = "+ res[i].ToString() + "\n";
            }
        }
    }
}
