using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Threading;
using System.Threading.Tasks;

namespace winTasks
{
    public partial class Form1 : Form
    {
        private TaskScheduler _scheduler;

        public Form1()
        {
            InitializeComponent();

            _scheduler = TaskScheduler.FromCurrentSynchronizationContext();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new Task(() =>
            {
                Task[] tareas = {
                Task.Factory.StartNew(() => IncreaseValue(textBox1, progressBar1, 150, 1), CancellationToken.None, TaskCreationOptions.LongRunning, _scheduler),
                Task.Factory.StartNew(() => IncreaseValue(textBox2, progressBar2, 180, 1), CancellationToken.None, TaskCreationOptions.LongRunning, _scheduler),
                Task.Factory.StartNew(() => IncreaseValue(textBox3, progressBar3, 50, 1), CancellationToken.None, TaskCreationOptions.LongRunning, _scheduler),
                Task.Factory.StartNew(() => IncreaseValue(textBox4, progressBar4, 200, 1), CancellationToken.None, TaskCreationOptions.LongRunning, _scheduler)
                };
                Task.WaitAll(tareas);

            }).Start();

            MessageBox.Show("Se han terminado todas las tareas");
        }

        private void IncreaseValue(TextBox t, ProgressBar p, int r, int c)
        {
            p.Maximum = r;
            for (int i = 0; i < r; i++)
            {
                int result = i * c;

                Thread.Sleep(10);

                UpdateView(t, p, result);
            }
        }

        private void UpdateView(TextBox t, ProgressBar p, int result)
        {
            p.Value = result;
            t.Text += "Count: " + result + Environment.NewLine;
            t.SelectionStart = t.Text.Length;
            t.ScrollToCaret();
            Application.DoEvents();
        }

    }
}
