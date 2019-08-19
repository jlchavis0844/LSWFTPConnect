using FluentFTP;
using Microsoft.Office.Interop.Excel;
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
        static List<FtpListItem> files = new List<FtpListItem>(); //file list
        static string folderName = ""; 
        static FtpClient client;
        List<Object[]> lifeLines;
        List<Object[]> annLines;
        static PasswordRepository passRep = new PasswordRepository();
        static Microsoft.Office.Interop.Excel.Application oXL;
        static _Workbook oWB;
        static _Worksheet oSheet;
        static Range oRng;
        static object misvalue = System.Reflection.Missing.Value;
        static bool TrimbleRichard = false;


        public Form1() {
            InitializeComponent();

            if(passRep.GetPassword() != null && String.IsNullOrEmpty(passRep.GetPassword())) {
                tbPassword.Text = passRep.GetPassword();
            }

            if(passRep.GetPassword() == null || String.IsNullOrEmpty(passRep.GetPassword())) {
                btnFiles.Enabled = false;
                btnGo.Enabled = false;
            }
        }

        private void btnFiles_Click(object sender, EventArgs e) {
            try {
                lblStatus.Text = "Connecting...";
                ConnectFTP();
                lblStatus.Text = "Connected! Getting listings...";
                lblStatus.Refresh();
                foreach (FtpListItem item in client.GetListing("/")) {
                    Console.WriteLine(item);
                    if (item.Type == FtpFileSystemObjectType.File && item.FullName.Contains(@"/AGY")) {
                        files.Add(item);
                    }
                }

                files.Sort(delegate (FtpListItem lhs, FtpListItem rhs) {
                    if (lhs.Modified == null && rhs.Modified == null) return 0;
                    else if (lhs.Modified == null) return -1;
                    else if (rhs.Modified == null) return 1;
                    else return lhs.Modified.CompareTo(rhs.Modified)*-1;
                });

                foreach (FtpListItem item in files) {
                    lbFiles.Items.Add(item.FullName.ToString());
                }

                lblStatus.Text = "Files Loaded, please choose one to process";
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
            fbd.Dispose();
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
                client.Host = tbServer.Text;
                Console.WriteLine(passRep.GetPassword());
                client.Credentials = new NetworkCredential(tbUserName.Text, passRep.GetPassword());
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
                if(file != null)
                    stream = file.Open(FileMode.Open, FileAccess.ReadWrite, FileShare.None);
            } catch (IOException) {
                return false;
            } finally {
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

            lifeLines = new List<Object[]>();
            annLines = new List<Object[]>();
            Boolean skip = false;

            foreach (var data in oLifes) {//parent of the commission data is OLifE (1 to 1)
                skip = false;
                var parties = from party in data.Descendants()
                              where party.Name.LocalName == "Party"
                              select party;

                foreach(var party in parties) {//specific patch for recurring entry
                    if (party.Element(ns0+"FullName").Value == "CLARK LOGAN DAVID") {
                        skip = true;
                        break;
                    }
                }
                if (skip)
                    continue;
                //check for plan of 'Non PlanCode'
                string plan = data.Element(ns0 + "Holding").Element(ns0 + "Policy").Element(ns0 + "PlanName").Value.ToString();
                if (plan == "Non PlanCode") {
                    skip = true;
                    continue;
                }

                DateTime date = Convert.ToDateTime(data.Element(ns0 + "Holding").Element(ns0 + "Policy").Element(ns0 + "IssueDate").Value);
                DateTime sDate = Convert.ToDateTime(data.Element(ns0 + "FinancialStatement").Element(ns0 + "StatementDate").Value);
                var commDet = data.Element(ns0 + "FinancialStatement").Element(ns0 + "CommissionStatement")
                    .Element(ns0 + "CommissionDetail");

                double amt = Convert.ToDouble(commDet.Element(ns0 + "EarnedAmt").Value);
                string polNum = commDet.Attribute("HoldingID").Value.ToString();
                string owner = commDet.Attribute("OwnerPartyID").Value.ToString().Replace("\'","`");
                double commRate = Convert.ToDouble(commDet.Element(ns0 + "CommissionRate").Value);
                double split = Convert.ToDouble(commDet.Element(ns0 + "SplitPercent").Value);
                double prem = Convert.ToDouble(commDet.Element(ns0 + "PaymentBasisAmt").Value);
                var type = commDet.Element(ns0 + "CommissionType").Value.ToString();

                if(owner == "Trimble Richard" && amt == 4) {
                    MessageBox.Show("Possible Erroneous Entry for Timble Richard, $4.00 comm\n"+
                        "You may need to remove this line to balance the payment", 
                        "Trimble Richard Error");
                    TrimbleRichard = true;
                }
                //string line = polNum + ", " + owner + ", " + plan + ", " + date + ", " +
                //    prem + ", " + commRate + ", " + split + ", ";

                double comm = 0.0;
                double ren = 0.0;

                if (type == "Renewal") {
                    ren = amt;
                } else comm = amt;
                //                   0      1     2     3      4      5        6     7     8
                Object[] oArr = { polNum, owner, plan, date, prem, commRate, split, comm, ren };
                if (polNum.StartsWith("LS",StringComparison.CurrentCulture)) {
                    lifeLines.Add(oArr);
                } else annLines.Add(oArr);
            }
            double TR_Total = annLines.Sum(aLine => (Convert.ToDouble(aLine[7]) + Convert.ToDouble(aLine[8])));
            if (TrimbleRichard) {
                var result = MessageBox.Show("Total with suspect line:\t" + TR_Total + "\n" +
                    "Total Without suspect line:\t" + (TR_Total - 4), TR_Total + " vs " + (TR_Total - 4) + "\n"+
                    "Would you like to delete the suspect line?",
                    MessageBoxButtons.YesNo);
            }

            writeToExcel(annLines, outFile + "_Ann_out.xls");
            writeToExcel(lifeLines, outFile + "_LIFE_out.xls");
        }

        private void btnStore_Click(object sender, EventArgs e) {
            passRep.SavePassword(tbPassword.Text);
            btnGo.Enabled = true;
            btnFiles.Enabled = true;
        }

        private void btnLocalFile_Click(object sender, EventArgs e) {
            // Show the FolderBrowserDialog.
            OpenFileDialog ofd = new OpenFileDialog();
            string commPath = @"P:\RALFG\Common Files\Commissions & Insurance\Commission Statements\" +
                DateTime.Now.Year.ToString() + @"\LSW Combo\";
            ofd.InitialDirectory = commPath;
            ofd.Filter = "XML Files|*.xml";
            DialogResult result = ofd.ShowDialog();
            
            if (result == DialogResult.OK) {
                string path = ofd.FileName;
                string outFileLocal = lblOutput.Text + "\\" + Path.GetFileNameWithoutExtension(path);
                //lblOutput.Text = path;
                ProcessCommReport(path, outFileLocal);
                lblStatus.Text = "Done Processesing";

            }
            ofd.Dispose();
        }

        public static void writeToExcel(List<Object[]> lines, string outfile) {
            if (lines == null || String.IsNullOrEmpty(outfile))
                return;

            try {
                //Start Excel and get Application object.
                oXL = new Microsoft.Office.Interop.Excel.Application();
                oXL.Visible = false;
                oXL.UserControl = false;
                oXL.DisplayAlerts = false;

                //Get a new workbook.
                oWB = oXL.Workbooks.Add("");
                oSheet = oWB.ActiveSheet;

                //Add table headers going cell by cell.
                oSheet.Cells[1, 1] = "Policy";
                oSheet.Cells[1, 2] = "Fullname";
                oSheet.Cells[1, 3] = "Plan";
                oSheet.Cells[1, 4] = "Issue Date";
                oSheet.Cells[1, 5] = "Premium";
                oSheet.Cells[1, 6] = "Rate %";
                oSheet.Cells[1, 7] = "Rate";
                oSheet.Cells[1, 8] = "Commission";
                oSheet.Cells[1, 9] = "Renewal";
                Range rg = (Microsoft.Office.Interop.Excel.Range)oSheet.Cells[1, 4];
                rg.EntireColumn.NumberFormat = "MM/DD/YYYY";

                //Format A1:D1 as bold, vertical alignment = center.
                oSheet.get_Range("A1", "I1").Font.Bold = true;
                oSheet.get_Range("A1", "I1").VerticalAlignment =
                    Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                for (int i = 0; i < lines.Count; i++) {
                    oSheet.get_Range("A" + (i + 2), "I" + (i + 2)).Value2 = lines[i];
                }
                oRng = oSheet.get_Range("A1", "I1");
                oRng.EntireColumn.AutoFit();
                oXL.Visible = false;
                oXL.UserControl = false;

                //outFile = GetSavePath();
                oWB.SaveAs(outfile,
                    56, //Seems to work better than default excel 16
                    Type.Missing,
                    Type.Missing,
                    false,
                    false,
                    XlSaveAsAccessMode.xlNoChange,
                    Type.Missing,
                    Type.Missing,
                    Type.Missing,
                    Type.Missing,
                    Type.Missing);

                //System.Diagnostics.Process.Start(outFile);
            }
            catch (Exception ex) {
                MessageBox.Show("Error: " + ex.Message, "Error");
            }
            finally {
                if (oWB != null)
                    oWB.Close();
                if (File.Exists(outfile))
                    System.Diagnostics.Process.Start(outfile);
            }
        }
    }
}
