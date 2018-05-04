using System;
using System.Text;
using System.Net;
using System.IO;
using System.Windows.Forms;

namespace Client
{
    class Client
    {
        public Client()
        { }

        public bool pathIsCorrect { get; set; } = false;
        public string serverPath { get; set; }
        public string Status { set; get; }
        private byte[] buffer { set; get; } = new byte[128];


        public void setServerPath(string path, RichTextBox txbx)
        {
            try
            {
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(path);
                request.Method = WebRequestMethods.Ftp.ListDirectory;
                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                if (response.StatusCode == (FtpStatusCode)150)
                {
                    Stream responseStream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(responseStream);
                    serverPath = path;
                    int size = 0;
                    while ((size = responseStream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        txbx.Text = txbx.Text + Encoding.UTF8.GetString(buffer) + "\n";
                    }
                    response.Close();

                    this.pathIsCorrect = true;
                    Status = "Success";
                }
                else
                {
                    serverPath = "";
                    Status = String.Format("Failed with {0} error", response.StatusCode);
                    this.pathIsCorrect = false;
                }
            }
            catch (Exception setPathExeption)
            {
                serverPath = "";
                Status = string.Format("{0}", setPathExeption.Message);
                this.pathIsCorrect = false;
            }
        }




        public void DownloadFile(string fileName, string dir)
        {
            try
            {
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(serverPath + fileName);
                request.Method = WebRequestMethods.Ftp.DownloadFile;

                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                if (response.StatusCode == (FtpStatusCode)150)
                {
                    Stream responseStream = response.GetResponseStream();
                    FileStream fileStream = new FileStream(dir, FileMode.Create);
                    int size = 0;
                    while ((size = responseStream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        fileStream.Write(buffer, 0, size);
                    }
                    fileStream.Close();
                    response.Close();
                    Status = string.Format("File downloaded with status {0}", response.StatusDescription);
                }
                else
                    Status = String.Format("Failed with {0} error", response.StatusCode);
            }
            catch (Exception downloadExeption)
            {
                Status = string.Format("{0}", downloadExeption.Message);
            }
        }



        public void UploadFile(string filePath, string dir)
        {
            try
            {
                FileInfo fi = new FileInfo(dir);
                filePath += fi.Name;
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(filePath);
                request.Method = WebRequestMethods.Ftp.UploadFile;

                FileStream fs = new FileStream(dir, FileMode.Open);
                byte[] fileContents = new byte[fs.Length];
                fs.Read(fileContents, 0, fileContents.Length);
                fs.Close();
                request.ContentLength = fileContents.Length;

                Stream requestStream = request.GetRequestStream();
                requestStream.Write(fileContents, 0, fileContents.Length);
                requestStream.Close();

                FtpWebResponse response = (FtpWebResponse)request.GetResponse();

                Status = string.Format("File uploaded with status {0}", response.StatusDescription);

                response.Close();
            }
            catch (Exception uploadExeption)
            {
                Status = string.Format("{0}", uploadExeption.Message);
            }
        }




        public void DeleteFile(string fileName)
        {
            try
            {
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(serverPath + fileName);
                request.Method = WebRequestMethods.Ftp.DeleteFile;

                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                Status = string.Format("File deleted with status {0}", response.StatusDescription);
            }
            catch (Exception deleteExeption)
            {
                Status = string.Format("{0}", deleteExeption.Message);
            }           
        }




        public void mkDir(string path)
        {
            try
            {
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(path);
                request.Method = WebRequestMethods.Ftp.MakeDirectory;

                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                Status = string.Format("Dir created with status {0}", response.StatusDescription);
            }
            catch (Exception mkDirExeption)
            {
                Status = string.Format("{0}", mkDirExeption.Message);
            }          
        }




        public void rmDir(string path)
        {
            try
            {
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(path);
                request.Method = WebRequestMethods.Ftp.RemoveDirectory;

                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                Status = string.Format("Dir created with status {0}", response.StatusDescription);
            }
            catch (Exception rmDirExeption)
            {
                Status = string.Format("{0}", rmDirExeption.Message);
            }
        }
    }
}
