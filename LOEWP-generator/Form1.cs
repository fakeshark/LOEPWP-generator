using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.IO;
using System.Windows.Forms;
using TikaOnDotNet.TextExtraction;

namespace LOEWP_generator
{
    public partial class frmLOEWP : Form
    {
        OpenFileDialog fileSelected;
        string lines;
        string output;
        string searchStringStart = "wpno=\"";
        string searchStringEnd = "\"";
        List<string> wpnoList = new List<string> { };

        public frmLOEWP()
        {
            InitializeComponent();
        }

        private void btnSelectTM_Click(object sender, EventArgs e)
        {
            rtbOutputPreview.Clear();
            //user browses to folder and selects file
            fileSelected = new OpenFileDialog();
            fileSelected.Filter = "XML Files (*.xml)|*.xml|Text Files (*.txt)|*.txt|SGML Files (*.sgml)|*.sgml";
            fileSelected.Multiselect = false;

            if (fileSelected.ShowDialog() == DialogResult.OK)
            {
                lines = File.ReadAllText(fileSelected.FileName);

                lblSelectedXmlTM.Text = "XML TM:   " + fileSelected.SafeFileName;
                //GenerateLOEWP();
                btnSelectPDF.Enabled = true;
            }
        }

        private void GenerateLOEWP()
        {
            if (lines != "" && lines != null)
            {
                char[] linesToChars = lines.ToCharArray();

                // make a list of all positions of all of the tag attribute string [ wpno=" ] (w/o the square brackets)
                List<int> positionList = GetPositions(lines, searchStringStart);
                string[] wpNumbers = new string[positionList.Count];

                foreach (int startPos in positionList)
                {
                    string thisWPNO = "";
                    for (int i = 0; i < 100; i++)
                    {
                        if (linesToChars[searchStringStart.Length + startPos + i].ToString() != searchStringEnd)
                        {
                            thisWPNO += linesToChars[searchStringStart.Length + startPos + i];
                        }
                        else
                        {
                            wpnoList.Add(thisWPNO);
                            break;
                        }
                    }
                }
            }

            output = "<loepwp>\r\n" +
                "\t<title>List of Effective Pages/Work Packages</title>\r\n" +
                "\t<note>\r\n" +
                "\t\t<trim.para>NOTE: Zero in the \"Change No.\" column indicates an original page or work package.</trim.para>\r\n" +
                "\t</note>\r\n" +
                "\t<issuechg>\r\n" +
                "\t\t<trim.para> Date of issue for the original manual is:</trim.para>\r\n" +
                "\t\t<issued>\r\n" +
                "\t\t\t<chgno> Original Draft </chgno>\r\n" +
                "\t\t\t<chgdate julian=\"0000000\"> XXXXX </chgdate>\r\n" +
                "\t\t</issued>\r\n" +
                "\t</issuechg>\r\n" +
                "\t<totalnumberof>\r\n" +
                "\t\t<text> THE TOTAL NUMBER OF PAGES FOR FRONT AND REAR  MATTER IS </text>\r\n" +
                "\t\t<totnum.frnt-rear-pages> XX </totnum.frnt-rear-pages>\r\n" +
                "\t\t<text> AND THE TOTAL NUMBER OF WORK PACKAGES IS </text>\r\n" +
                "\t\t<totnum.wps>" + wpnoList.Count + "</totnum.wps>\r\n" +
                "\t\t<text>, CONSISTING OF THE FOLLOWING:</text>\r\n" +
                "\t</totalnumberof>\r\n" +
                "\t<col.title> Page / WP No.</col.title>\r\n" +
                "\t<col.title> Change No.</col.title>\r\n";

            foreach (string wpnoItem in wpnoList)
            {
                output += "\t<chghistory modified=\"none\">\r\n" +
                    "\t\t<wpno wpref=\"" + wpnoItem + "\"/>\r\n" +
                    "\t\t<wppages>X Pages</wppages>\r\n" +
                    "\t\t<chgno>0</chgno>\r\n" +
                    "\t</chghistory>\r\n";
            }

            output += "</loepwp>\r\n";
            rtbOutputPreview.Text = output;
        }

        private void btnCopyToClipboard_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(rtbOutputPreview.Text);
            lblStatus.Text = "Preview text copied to clipboard.";
        }

        public List<int> GetPositions(string source, string searchString)
        {
            List<int> ret = new List<int>();
            int len = searchString.Length;
            int start = -len;
            while (true)
            {
                start = source.IndexOf(searchString, start + len);
                if (start == -1)
                {
                    break;
                }
                else
                {
                    ret.Add(start);
                }
            }
            return ret;
        }

        private void QuitApplication(object sender, EventArgs e)
        {
            if (MessageBox.Show("You are about to exit the application.\n\nProceed?", "Exit Application?", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                this.Close();
            }
        }

        private void btnSelectPDF_Click(object sender, EventArgs e)
        {
            //user browses to folder and selects file
            OpenFileDialog fileSelected = new OpenFileDialog();

            fileSelected.Filter = "PDF Files (*.pdf)|*.pdf";
            fileSelected.Multiselect = false;

            if (fileSelected.ShowDialog() == DialogResult.OK)
            {
                string filepath = fileSelected.FileName.ToString();
                TextExtractor te = new TextExtractor();
                string pdfText = te.Extract(filepath).ToString();
                rtbOutputPreview.Text = pdfText;
                // GenerateLOEWP();
            }

        }
    }
}
