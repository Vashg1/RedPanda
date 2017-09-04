using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Threading;
using System.Collections;
using System.IO;

namespace HappyCustomer
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
            Hashtable hashtable = new Hashtable();

            foreach (string line in Program.lines)
            {
                Program.CustomerQue.Enqueue(line);
            }

            backgroundWorker1.DoWork += backgroundWorker1_DoWork;
            backgroundWorker1.ProgressChanged += backgroundWorker1_ProgressChanged;
            backgroundWorker1.RunWorkerCompleted += backgroundWorker1_RunWorkerCompleted;  //Tell the user how the process went
            backgroundWorker1.WorkerReportsProgress = true;
            backgroundWorker1.WorkerSupportsCancellation = true; //Allow for the process to be cancelled

        }

        public class SaveCustomers
        {
            private static Queue<string> GetStoreFeedBackQueue()
            {
                Queue<string> StoreFeedBackQueue = new Queue<string>();
                return StoreFeedBackQueue;
            }


            public void SaveCustomerDataToQueue()
            {
                AddToHashFile(Program.CustomerQue.Dequeue().ToString());
            }

            public void AddToHashFile(string line)
            {
                string[] rows = line.Split(',');
                DateTime date = DateTime.Parse(rows[1]);

                string htkey = rows[0] + ',' + date.ToString("dd MMMM yy");

                if (Program.hashtable.ContainsKey(htkey))
                {
                    Program.hashtable[htkey] = Convert.ToInt32(Program.hashtable[htkey]) + 1;
                }
                else
                {

                    Program.hashtable.Add(htkey, 1);
                }
            }
        }

        private void StartUp_Click(object sender, EventArgs e)
        {
            backgroundWorker1.RunWorkerAsync(); // start the background process

        }

        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            SaveCustomers SaveCustomers = new SaveCustomers();
            for (int i = 0; i < Program.lines.Count(); i++)
            {
                Thread.Sleep(100);
                SaveCustomers.SaveCustomerDataToQueue();
           ///     backgroundWorker1.ReportProgress(i);

                //Check if there is a request to cancel the process
                if (backgroundWorker1.CancellationPending)
                {
                    e.Cancel = true;
                    backgroundWorker1.ReportProgress(0);
                    return;
                }
            }
            //If the process exits the loop, ensure that progress is set to 100%
            //Remember in the loop we set i < 100 so in theory the process will complete at 99%
            backgroundWorker1.ReportProgress(100);
        }

        private void backgroundWorker1_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                lblStatus.Text = "Process was cancelled";
            }
            else if (e.Error != null)
            {
                lblStatus.Text = "There was an error running the process. The thread aborted";
            }
            else
            {
                lblStatus.Text = "Stores has been uploaded was completed";
                ManagersReport();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Check if background worker is doing anything and send a cancellation if it is
            if (backgroundWorker1.IsBusy)
            {
                backgroundWorker1.CancelAsync();
            }

        }

        public void ManagersReport()
        {

            DateTime thisDay = DateTime.Today;
            String date = thisDay.ToString("MM_dd_yy");
            String FileName = @"c:\Output_" + date + ".csv";
            string happyday = GetHappiestDay();
            if (!File.Exists(FileName))
            {
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(FileName, true))
                {
                    file.WriteLine("The happiest day is {0} ", happyday);
                    file.WriteLine("List of happy stores below with number of happy hits");  //vashg//remove these
                    foreach (String key in Program.hashtable.Keys)
                    {
                        string NewVal = Program.hashtable[key].ToString();
                        file.Write(key + ",");
                        file.WriteLine(NewVal);
                    }
                }
            }
        }

        public String GetHappiestDay()
        {

            int MaxValue = 0;
            String MaxKey = String.Empty;
            Hashtable happyhash = new Hashtable();
           
            foreach (String key in Program.hashtable.Keys)
            {
              string[] rows = key.Split(',');
              string htkey = rows[1];
              if (happyhash.ContainsKey(htkey))
              {
                  happyhash[htkey] = Convert.ToInt32(happyhash[htkey]) + Convert.ToInt32(Program.hashtable[key]);
              }
              else
              {

                  happyhash.Add(htkey, Convert.ToInt32(Program.hashtable[key]));
              }

              if (MaxValue < Convert.ToInt32(happyhash[htkey]))
              {
                  MaxValue = Convert.ToInt32(happyhash[htkey]);
                  MaxKey = htkey;
              }
            }


            return (MaxKey + " with number of happy hits " + MaxValue);
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }


    }

}

 



