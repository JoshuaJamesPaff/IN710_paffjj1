﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;



namespace metronome
{
    public partial class Form1 : Form
    {
        private Metronome mainMetronome;
        private Beeper mainBeeper;
        private Counter mainCounter;
        private TimeDisplay mainTimeDisplay;

        //thread object
        private Thread metronomeThread;


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            mainMetronome = new Metronome(1000, this);
            mainBeeper = new Beeper(mainMetronome, "blip1.wav");
            mainCounter = new Counter(mainMetronome, numericUpDown1);
            mainTimeDisplay = new TimeDisplay(mainMetronome, listBox1);    
        }

        //button calls mainMetronome.start in new thread
        private void button1_Click(object sender, EventArgs e)
        {
           int currInterval = Convert.ToInt16(textBox1.Text);
           mainMetronome.Interval = currInterval;

            //starts thread
           metronomeThread = new Thread(mainMetronome.start);
           metronomeThread.Start();
        }

        public void clearBuffer()
        {
            Application.DoEvents();
        }

        //button aborts metronome thread
        private void button2_Click(object sender, EventArgs e)
        {
            metronomeThread.Abort();
        }

    }
}
