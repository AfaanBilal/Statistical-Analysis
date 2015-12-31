/*
* Statistical Analysis
* (c) 2013-2016, Afaan Bilal
* www.amxinfinity.tk
*/

using System;
using System.Windows.Forms;

namespace Statistical_Analysis
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        double[] Xi, Fi, XiFi, XiDev, FiDev, CF, XiMDev, FiMDev;
        double sum, sum2, sum3, mean, N, mdm, median, mdmedian;
        bool even;
        string[] tmp1, tmp2;

        char[] trc = { ' ', '\n' };

        // Initialize and allocate the vars
        void InitVariables()
        {
            tmp1 = rtbXI.Text.Trim(trc).Split('\n');
            tmp2 = rtbFI.Text.Trim(trc).Split('\n');

            Xi = new double[tmp1.Length];
            Fi = new double[tmp2.Length];
            XiFi = new double[tmp2.Length];
            XiDev = new double[tmp2.Length];
            FiDev = new double[tmp2.Length];
            XiMDev = new double[tmp2.Length];
            FiMDev = new double[tmp2.Length];
            CF = new double[tmp2.Length];
            sum = sum2 = sum3 = N = 0;
        }

        // Put the data-entries into an array
        void SetXi()
        {
            for (int i = 0; i < tmp1.Length; i++)
            {
                Xi[i] = Convert.ToDouble(tmp1[i]);
            }
        }

        // Put the frequency-entries into an array
        void SetFi()
        {
            for (int i = 0; i < tmp2.Length; i++)
            {
                Fi[i] = Convert.ToDouble(tmp2[i]);
            }
        }

        // Calculate and set the Cumulative Frequencies
        void SetCF()
        {
            CF[0] = Fi[0];

            for (int i = 1; i < tmp2.Length; i++)
            {
                CF[i] = CF[i - 1] + Fi[i];
            }

            string tmps = "";
            for (int i = 0; i < CF.Length; i++)
            {
                tmps += CF[i].ToString() + '\n';
            }
            rtbCF.Text = tmps;
        }

        // Set the data-entry * Frequency column
        void SetXiFi()
        {
            for (int i = 0; i < Xi.Length; i++)
            {
                XiFi[i] = Xi[i] * Fi[i];
            }

            string tmps = "";
            for (int i = 0; i < XiFi.Length; i++)
            {
                tmps += Math.Round(XiFi[i], 3).ToString() + '\n';
            }
            rtbFIXI.Text = tmps;
        }

        // Set the data-entry * Frequency column sum
        void SetXiFiSum()
        {
            for (int i = 0; i < Xi.Length; i++)
            {
                sum += XiFi[i];
            }
        }

        // Set total frequency (total number of data-entries)
        void SetN()
        {
            for (int i = 0; i < Fi.Length; i++)
            {
                N += Fi[i];
            }
        }

        // Set the Mean
        void SetMean()
        {
            mean = sum / N;

            tbMean.Text = Math.Round(mean, 3).ToString();
        }

        // Set the data-entry deviation column
        void SetXiDev()
        {
            for (int i = 0; i < Xi.Length; i++)
            {
                XiDev[i] = Math.Abs(Xi[i] - mean);
            }

            string tmps = "";
            for (int i = 0; i < XiFi.Length; i++)
            {
                tmps += Math.Round(XiDev[i], 3).ToString() + '\n';
            }
            rtbDEV.Text = tmps;
        }

        // Set the frequency deviation column
        void SetFiDev()
        {
            for (int i = 0; i < XiDev.Length; i++)
            {
                FiDev[i] = Fi[i] * XiDev[i];
            }

            string tmps = "";
            for (int i = 0; i < FiDev.Length; i++)
            {
                tmps += Math.Round(FiDev[i], 3).ToString() + '\n';
            }
            rtbFIDEV.Text = tmps;
        }

        // Set the frequency deviation sum
        void SetFiDevSum()
        {
            for (int i = 0; i < FiDev.Length; i++)
            {
                sum2 += FiDev[i];
            }
        }

        // Set the Mean Deviation About Mean
        void SetMeanDeviationAboutMean()
        {
            mdm = sum2 / N;

            tbMDM.Text = Math.Round(mdm, 3).ToString();
        }

        // Set the data-entry median deviation
        void SetXiMedianDeviation()
        {
            for (int i = 0; i < Xi.Length; i++)
            {
                XiMDev[i] = Math.Abs(Xi[i] - median);
            }

            string tmps = "";
            for (int i = 0; i < XiMDev.Length; i++)
            {
                tmps += Math.Round(XiMDev[i], 3).ToString() + '\n';
            }
            rtbDevMedian.Text = tmps;
        }

        // Set the frequency median deviation
        void SetFiMedianDeviation()
        {
            for (int i = 0; i < FiMDev.Length; i++)
            {
                FiMDev[i] = Fi[i] * XiMDev[i];
            }

            string tmps = "";
            for (int i = 0; i < FiMDev.Length; i++)
            {
                tmps += Math.Round(FiMDev[i], 3).ToString() + '\n';
            }
            rtbFIDevMedian.Text = tmps;
        }

        // Set the sum of frequency deviation (Median)
        void SetFiMDevSum()
        {
            for (int i = 0; i < FiMDev.Length; i++)
            {
                sum3 += FiMDev[i];
            }
        }

        // Set the mean deviation about median
        void SetMeanDeviationAboutMedian()
        {
            mdmedian = sum3 / N;

            tbMDMedian.Text = Math.Round(mdmedian, 3).ToString();
        }

        // Do all stuff
        void CalculateAndPrint()
        {
            InitVariables();

            SetXi();

            SetFi();

            SetCF();

            SetXiFi();

            SetXiFiSum();

            SetN();

            SetMean();

            SetXiDev();

            SetFiDev();

            SetFiDevSum();

            SetMeanDeviationAboutMean();

            CalculateMedian();

            SetXiMedianDeviation();

            SetFiMedianDeviation();

            SetFiMDevSum();

            SetMeanDeviationAboutMedian();
        }

        void CalculateMedian()
        {
            if (N % 2 != 0)
                even = false;
            else
                even = true;

            if (even)
            {
                int nb2 = (int)(N / 2);
                int nb2p1 = nb2 + 1;
                double nb2t = 0, nb2p1t = 0;

                for (int i = 0; i < CF.Length; i++)
                {
                    if ((int)CF[i] == nb2)
                    {
                        nb2t = Xi[i];
                        break;
                    }

                    if (i > 0)
                    {
                        if (nb2 < CF[i] && nb2 > CF[i - 1])
                        {
                            nb2t = Xi[i];
                            break;
                        }
                    }
                }

                for (int i = 0; i < CF.Length; i++)
                {
                    if ((int)CF[i] == nb2p1)
                    {
                        nb2p1t = Xi[i];
                        break;
                    }

                    if (i > 0)
                    {
                        if (nb2p1 < CF[i] && nb2p1 > CF[i - 1])
                        {
                            nb2p1t = Xi[i];
                            break;
                        }
                    }
                }

                median = (nb2t + nb2p1t) / 2;
            }
            else
            {
                int cfv = 0, cfiv = 0;
                double tmp1s = (N + 1) / 2;
                int tmp1 = (int)tmp1s;

                for (int i = 0; i < CF.Length; i++)
                {
                    if ((int)CF[i] == tmp1)
                    {
                        cfv = (int)CF[i];
                        cfiv = i;
                        break;
                    }

                    if (i > 0)
                    {
                        if (tmp1s < CF[i] && tmp1 > CF[i - 1])
                        {
                            cfv = (int)CF[i];
                            cfiv = i;
                            break;
                        }
                    }
                }

                median = Xi[cfiv];
            }

            tbMedian.Text = median.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (rtbXI.Text.Trim(trc) == "" || rtbFI.Text.Trim(trc) == "" || rtbXI.Text.Trim(trc).Split('\n').Length != rtbFI.Text.Trim(trc).Split('\n').Length)
            {
                MessageBox.Show(" Instructions:\n\n One entry on each line. Please fill in the data items in ascending\n order and their respective frequencies properly.\n\n If the data ungrouped, please write 1 in the\n frequency box for each item.", "Statistical Analysis", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            CalculateAndPrint();

            rtbFIXI.Enabled = true;
            rtbDEV.Enabled = true;
            rtbFIDEV.Enabled = true;
            rtbCF.Enabled = true;
            rtbDevMedian.Enabled = true;
            rtbFIDevMedian.Enabled = true;
            tbMDM.Enabled = true;
            tbMean.Enabled = true;
            tbMedian.Enabled = true;
            tbMDMedian.Enabled = true;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            rtbCF.Enabled = false;
            rtbFIXI.Enabled = false;
            rtbDEV.Enabled = false;
            rtbFIDEV.Enabled = false;
            rtbDevMedian.Enabled = false;
            rtbFIDevMedian.Enabled = false;
            tbMDM.Enabled = false;
            tbMean.Enabled = false;
            tbMedian.Enabled = false;
            tbMDMedian.Enabled = false;

            lblCopyrights.Text = lblCopyrights.Text.Replace("2013", DateTime.Now.Year.ToString());     
        }

        private void btAbt_Click(object sender, EventArgs e)
        {
            new About().ShowDialog();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            MessageBox.Show(" Instructions:\n\n One entry on each line. Please fill in the data items in ascending\n order and their respective frequencies properly.\n\n If the data ungrouped, please write 1 in the\n frequency box for each item.", "Statistical Analysis", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            button2_Click_1(null, null);
        }

    }
}
