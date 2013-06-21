using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Marcel_GUI
{
    public partial class GUI : Form
    {
        public GUI()
        {
            InitializeComponent();
        }
        private int i = 0;
        String fileName;
        String fileExtension;
        String [] location;
        String ret;
        Directory [] directories;
        File [] files;
        String curPath;
        String fileName;
        Directory curdirectory;
        Boolean change = true;


        private void GUI_Load(object sender, EventArgs e)
        {
            System.Media.SoundPlayer player = new System.Media.SoundPlayer();
            player.SoundLocation = "song.wav";
            player.Play();
            player.PlayLooping();
            pictureBox1.Left = -700;
            pictureBox1.Image = Properties.Resources.I_am_your_god;
            
        }

        private void Inventory(Directory parent)
        {
            /*
            curPath = parent.GetCurrentDirectory();
            directories = parent.EnumerateDirectories(curPath);
            files = parent.EnumerateFiles(curPath);
            for(int i = 0; i < directories.Length; i++)
            {
                Inventory(directories[i]);
            }
            for(int i = 0; i < files.Length; i++)
            {
                String [] toSend = new String[3];
                String [] temp = ;


                write(toSend);
            }
             *      [0] name
             *      [1] extension
             *      [2] file path
            
            
            CurPath = parent.GetCurrentDirectory();
            parent.getAbsolutePath() + "\";
            sFileName = "";
            unsorted = parent.list();
            sorted = new String[unsorted.length];

            for (int i = 0; i < sorted.length; i++)
            {
                curFile = new File(CurPath + unsorted[i]);
                if (curFile.isDirectory())
                {
                    sorted[i] = "[dir ]" + unsorted[i];
                }
            }

            for (int i = 0; i < sorted.length; i++)
            {
                curFile = new File(CurPath + unsorted[i]);
                if (!curFile.isDirectory())
                {
                    sorted[i] = "[dir ]" + unsorted[i];
                }
            }

            for (int i = 0; i < sorted.length; i++)
            {
                curFile = new File(CurPath + sorted[i]);
                if (curFile.isDirectory())
                {
                    inventory(curFile);
                }
                else
                {
                    fileName = curFile.getAbsolutePath();
                    fileExtension = fileName.substring(fileName.length() - 4, fileName.length() - 1);
                    location = curFile.getAbsolutePath().split("/");
                    ret = fileName + "," + fileExtension + ",";
                    for (int j = 0; j < location.length; j++)
                    {
                        ret = ret + location[j] + ",";
                    }
                }
            }
            */
        }
        
        private void printInfo(String info)
        {

        }

        private void write(String [] toWrite)
        {
            //return true;
        }
        

        private void btnInventory_Click(object sender, EventArgs e)
        {
            if (txtInput.Text.Trim() != string.Empty)
            {
                
                //Inventory(inventory's found item shitz);
                MessageBox.Show("Create your own damn file, bitch.", "ERROR - DO YOUR OWN WORK", MessageBoxButtons.OK,
                    MessageBoxIcon.Stop);
            }
        }

        private void btnSelectOriginDirectory_Click(object sender, EventArgs e)
        {
            btnInventory.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (pictureBox1.Left <= 848)
            {
                pictureBox1.Left++;
            }
            else
            {
                if(change)
                {
                    pictureBox1.Image = Properties.Resources.I_am_your_god;
                    change = false;
                }
                else
                {
                    pictureBox1.Image = Properties.Resources.aargh1;
                    i++;
                }
                pictureBox1.Left = -300;
                
            }
        }
    }
}
