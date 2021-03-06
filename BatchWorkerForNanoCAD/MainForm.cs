﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BatchWorker
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }
        static List<string> allPath;// все пути      
        private void btnSelectFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbdSelectFolder = new FolderBrowserDialog();
            if (fbdSelectFolder.ShowDialog() == DialogResult.OK)
            {
                string folderPath = fbdSelectFolder.SelectedPath;
                PathManyFiles PtAllSt = new PathManyFiles(folderPath);
                PtAllSt.sendAllPathFromClass += new EventHandler<UserEventArgsPathAll>(other_sendAllPathesFromClass);
                PtAllSt.DoEventSendAllPathesFromClass();    
            }
        }
        void other_sendAllPathesFromClass(object sender, UserEventArgsPathAll e)
        {
            bool res = e.setAllPathes;
            if (res == true)
            {
                btnDGNdelete.Enabled = true;
                btnSPDSobjDestroy.Enabled = true;
                allPath = e.allPathes;
            }
            else
            {
                return;
            }          
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            DgnDelete Del = new DgnDelete();
            Del.DgnDeleting(allPath);
        }

        private void btnSPDSobjDestroy_Click(object sender, EventArgs e)
        {
            SPDSdestroyer destroyer = new SPDSdestroyer();
            bool isSaveDWG = cbSaveDwg.Checked;
            destroyer.DestroySPDSobjects(allPath,isSaveDWG);

        }

    }
}
