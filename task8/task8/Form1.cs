﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace task8
{
    public partial class Form1 : Form
    {
        private List<Abstractions.Toy> _toys = new List<Abstractions.Toy>();
        private Abstractions.IToyFactory _factory;
        private Abstractions.IToyFactory Factory
        {
            get { return _factory; }
            set { _factory = value; }
        }


        public Form1()
        {
            InitializeComponent();
            Factory = new Entities.BallFactory();
            mainPanel.Width = Width;
        }

        private void createTimer_Tick(object sender, EventArgs e)
        {
            var ball = Factory.CreateNew();
            _toys.Add((Entities.Ball)ball);
            ball.Left = -ball.Width;
            mainPanel.Controls.Add(ball);
        }

        private void conveyorTimer_Tick(object sender, EventArgs e)
        {
            var maxPosition = 0;
            foreach (var ball in _toys)
            {
                ball.MoveToy();
                if (ball.Left > maxPosition)
                    maxPosition = ball.Left;
            }

            if (maxPosition > 1000)
            {
                var oldestBall = _toys[0];
                mainPanel.Controls.Remove(oldestBall);
                _toys.Remove(oldestBall);
            }
        }
    }
}
