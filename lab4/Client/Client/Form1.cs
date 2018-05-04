using System;
using System.Windows.Forms;
using System.Threading;

namespace Client
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }

        private Client NewClient = new Client();
        private string fileDirectoryForDownload { set; get; }
        private string fileDirectoryForUpload { set; get; }       

        private string setDirectoryForDownload()
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                return saveFileDialog1.FileName;
            else
                return "";
        }

        private string setDirectoryForUpload()
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
                return openFileDialog1.FileName;
            else
                return "";
        }


        //ПОТОЧНЫЕ ФУНКЦИИ
        private void SetDir_Click_Handle()
        {
            textBox_CurDir.Text = "No path";
            if (textBox_Input.Text != "")
            {
                richTextBox_Output.Text = "";
                NewClient.setServerPath(textBox_Input.Text, richTextBox_Output);
                textBox_CurDir.Text = NewClient.serverPath;
                label_Status.Text = NewClient.Status;
            }
            else
                label_Status.Text = "Input directory";
        }

        private void Download_Click_Handle()
        {
            NewClient.DownloadFile(textBox_Input.Text, fileDirectoryForDownload);
            label_Status.Text = NewClient.Status;
        }

        private void Upload_Click_Handle()
        {
            NewClient.UploadFile(textBox_Input.Text, fileDirectoryForUpload);
            label_Status.Text = NewClient.Status;
        }

        private void Delete_Click_Handle()
        {
            if (NewClient.pathIsCorrect)
            {
                if ((textBox_Input.Text != ""))
                {
                    NewClient.DeleteFile(textBox_Input.Text);
                    label_Status.Text = NewClient.Status;
                }
                else
                    label_Status.Text = "Input filepath";
            }
            else
                label_Status.Text = "Set directory";
        }

        private void mkDir_Click_Handle()
        {
            if (NewClient.pathIsCorrect)
            {
                if ((textBox_Input.Text != ""))
                {
                    NewClient.mkDir(textBox_Input.Text);
                    label_Status.Text = NewClient.Status;
                }
                else
                    label_Status.Text = "Input filepath";
            }
            else
                label_Status.Text = "Set directory";
        }

        private void rmDir_Click_Handle()
        {
            if (NewClient.pathIsCorrect)
            {
                if ((textBox_Input.Text != ""))
                {
                    NewClient.rmDir(textBox_Input.Text);
                    label_Status.Text = NewClient.Status;
                }
                else
                    label_Status.Text = "Input filepath";
            }
            else
                label_Status.Text = "Set directory";
        }



        //ОБРАБОТКА
        private void Button_SetDir_Click(object sender, EventArgs e)
        {
            Thread newThread = new Thread(new ThreadStart(SetDir_Click_Handle));
            newThread.Start();
        }

        private void button_Download_Click(object sender, EventArgs e)
        {
            if (NewClient.pathIsCorrect)
            {
                if ((textBox_Input.Text != "") && ((fileDirectoryForDownload = setDirectoryForDownload()) != ""))
                {
                    Thread newThread = new Thread(new ThreadStart(Download_Click_Handle));
                    newThread.Start();
                }
                else
                    label_Status.Text = "Input filename";
            }
            else
                label_Status.Text = "Set directory";
        }

        private void button_Upload_Click(object sender, EventArgs e)
        {
            if (NewClient.pathIsCorrect)
            {
                if ((textBox_Input.Text != "") && ((fileDirectoryForUpload = setDirectoryForUpload()) != ""))
                {
                    Thread newThread = new Thread(new ThreadStart(Upload_Click_Handle));
                    newThread.Start();
                }
                else
                    label_Status.Text = "Input filepath";
            }
            else
                label_Status.Text = "Set directory";
        }

        private void button_Delete_Click(object sender, EventArgs e)
        {
            Thread newThread = new Thread(new ThreadStart(Delete_Click_Handle));
            newThread.Start();
        }

        private void button_mkDir_Click(object sender, EventArgs e)
        {
            Thread newThread = new Thread(new ThreadStart(mkDir_Click_Handle));
            newThread.Start();
        }

        private void button_rmDir_Click(object sender, EventArgs e)
        {
            Thread newThread = new Thread(new ThreadStart(rmDir_Click_Handle));
            newThread.Start();
        }
    }
}
