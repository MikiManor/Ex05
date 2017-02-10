﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Ex05_Othelo
{
    public partial class MainMenu : Form
    {
        public MainMenu()
        {
            InitializeComponent();
        }

        private void buttonChangeBoardSize_Click(object sender, EventArgs e)
        {
            OtheloBoard.BoardSize += 2;
            buttonChangeBoardSize.Text = string.Format("Board & Size: {0}x{0}(Click to increase)",OtheloBoard.BoardSize);
        }

        private void buttonPlayVsPc_Click(object sender, EventArgs e)
        {
            Hide();
            OtheloUI.StartPlay(OtheloBoard.BoardSize, 2);
        }

        private void buttonPlayVsHuman_Click(object sender, EventArgs e)
        {
            Hide();
            OtheloUI.StartPlay(OtheloBoard.BoardSize, 1);
        }
    }
}