using FluentFTP;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Authentication;
using System.Windows.Forms;
using System.Xml.Linq;

namespace LSWFTPConnect {
    public partial class Form1 : Form {
        static List<string> files = new List<string>(); //file list
        static string folderName = "";
        static FtpClient client;

        public Form1() {
            InitializeComponent();
        }

        private void btnFiles_Click(object sender, EventArgs e) {
            try {

                ConnectFTP();
                foreach (FtpListItem item in client.GetListing("/")) {
                    Console.WriteLine(item);
                    if (item.Type == FtpFileSystemObjectType.File) {
                        files.Add(item.FullName.ToString());
                    }
                }

                foreach (string item in files) {
                    lbFiles.Items.Add(item);
                }
            } catch (Exception ex) {
                Console.WriteLine(ex.Message.ToString());
            }
        }
        void OnValidateCertificate(FtpClient control, FtpSslValidationEventArgs e) {
            // add logic to test if certificate is valid here
            e.Accept = true;
        }

        private void btnOutputDir_Click(object sender, EventArgs e) {
            // Show the FolderBrowserDialog.
            FolderBrowserDialog fbd = new FolderBrowserDialog() ;
            DialogResult result = fbd.ShowDialog();
            if (result == DialogResult.OK) {
                folderName = fbd.SelectedPath;
                lblOutput.Text = folderName;
            }
        }

        private void btnGo_Click(object sender, EventArgs e) {
            if(lbFiles.SelectedIndex == -1) {
                lblStatus.Text = "No File Selected";
                return;
            }

            if (!client.IsConnected) {
                ConnectFTP();
                lblStatus.Text = "Reconnecting..";
            }
            string selectedFile = lbFiles.Items[lbFiles.SelectedIndex].ToString();
            string fileName = Path.GetFileName(selectedFile);
            string outFile = lblOutput.Text + "\\" + fileName;

            string commPath = @"P:\RALFG\Common Files\Commissions & Insurance\Commission Statements\" +
                DateTime.Now.Year.ToString() + @"\LSW Combo\";
            lblStatus.Text = "Downloading " + fileName;
            client.DownloadFile(commPath + fileName, selectedFile);
            client.Disconnect();
            lblStatus.Text = "Done downloading file. Processesing";
            ProcessCommReport(commPath + fileName, lblOutput.Text + "\\" + 
                Path.GetFileNameWithoutExtension(selectedFile));
            lblStatus.Text = "Done Processesing";
        }

        private void ConnectFTP() {
            try {
                client = new FtpClient();
                client.Host = @"FTPS.NATIONALLIFE.COM";
                client.Credentials = new NetworkCredential(@"RALotterAgencyBuilders", @";f2uMuXV1DW@");
                client.EncryptionMode = FtpEncryptionMode.Explicit;
                client.SslProtocols = SslProtocols.Tls | SslProtocols.Tls11 | SslProtocols.Tls12;
                client.ValidateCertificate += new FtpSslValidation(OnValidateCertificate);
                client.Connect();
                lblStatus.Text = "Connected";
            } catch(Exception ex) {
                lblStatus.Text = ex.Message.ToString();
            }
        }

        protected virtual bool IsFileLocked(FileInfo file) {
            FileStream stream = null;

            try {
                stream = file.Open(FileMode.Open, FileAccess.ReadWrite, FileShare.None);
            }
            catch (IOException) {
                return true;
            }
            finally {
                if (stream != null)
                    stream.Close();
            }
            return false;
        }

        private void ProcessCommReport(string inFile, string outFile) {
            XDocument xdoc = XDocument.Load(inFile);
            XNamespace ns0 = xdoc.Root.GetNamespaceOfPrefix("ns0");
            var oLifes = from data in xdoc.Descendants()
                         where data.Name.LocalName == "OLifE"
                         select data;

            List<string> lifeLines = new List<string>();
            List<string> annLines = new List<string>();

            foreach (var data in oLifes) {
                var plan = data.Element(ns0 + "Holding").Element(ns0 + "Policy").Element(ns0 + "ProductCode").Value;
                var date = data.Element(ns0 + "Holding").Element(ns0 + "Policy").Element(ns0 + "IssueDate").Value;
                var polType = data.Element(ns0 + "Holding").Element(ns0 + "Policy").Element(ns0 + "ProductType").Value;

                var commDet = data.Element(ns0 + "FinancialStatement").Element(ns0 + "CommissionStatement")
                    .Element(ns0 + "CommissionDetail");

                var amt = commDet.Element(ns0 + "EarnedAmt").Value;
                var polNum = commDet.Attribute("HoldingID").Value;
                var owner = commDet.Attribute("OwnerPartyID").Value;
                //var date = commDet.Element(ns0 + "TransactionDate").Value;
                var commRate = commDet.Element(ns0 + "CommissionRate").Value;
                var split = commDet.Element(ns0 + "SplitPercent").Value;
                var prem = commDet.Element(ns0 + "PaymentBasisAmt").Value;
                var type = commDet.Element(ns0 + "CommissionType").Value;

                string line = polNum + ", " + owner + ", " + plan + ", " + date + ", " +
                    prem + ", " + commRate + ", " + split + ", ";

                if (type == "Renewal") {
                    line += ("0, " + amt.ToString());
                } else {
                    line += amt.ToString() + ", 0";
                }

                Console.WriteLine(line);
                if (polNum.StartsWith("LS")) {
                    lifeLines.Add(line);
                }
                else annLines.Add(line);
            }

            StreamWriter writer;
            if (annLines.Count > 0) {
                writer = new StreamWriter(outFile + "_AnnOut.csv");
                writer.WriteLine("Policy, Full Name, Plan, Issue Date, Premium, Rate %, Rate, Commission, Renewal");
                foreach (string item in annLines) {
                    writer.WriteLine(item);
                }
                writer.Close();
            }

            if (lifeLines.Count > 0) {
                writer = new StreamWriter(outFile + "_LifeOut.csv");
                writer.WriteLine("Policy, Full Name, Plan, Issue Date, Premium, Rate %, Rate, Commission, Renewal");
                foreach (string item in lifeLines) {
                    writer.WriteLine(item);
                }
                writer.Close();
            }
        }
    }
}
