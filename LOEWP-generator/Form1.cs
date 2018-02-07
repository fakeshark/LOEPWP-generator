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
        List<string> wpnoList = new List<string> { };
        string[] PdfLines = new string[] { };


        string finalOutput;
        string searchStringStart = "wpno=\"";
        string searchStringEnd = "\"";
        string dateDDMonthYYYY = "XXXXXX";
        string JulianDateString = "XXXXXX";
        string[] Month = new string[] { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
        string filepath;

        public frmLOEWP()
        {
            InitializeComponent();
        }

        private void GenerateLOEPWP()
        {
            // do a prompt to set the Date of issue if not already set
            // use  List<string> wpnoList and string[] PdfLines for the generate code
            string[] alphabet = new string[] { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", "a/blank", "b/blank", "c/blank", "d/blank", "e/blank", "f/blank", "g/blank", "h/blank", "i/blank", "j/blank", "k/blank", "l/blank", "m/blank", "n/blank", "o/blank", "p/blank", "q/blank", "r/blank", "s/blank", "t/blank", "u/blank", "v/blank", "w/blank", "x/blank", "y/blank", "z/blank" };
            string[] romanNumerals = new string[] { "i", "iii", "iv", "v", "vi", "vii", "viii", "ix", "x", "xi", "xii", "xiii", "xiv", "xv", "xvi", "xvii", "xviii", "xix", "xx", "xxi", "xxii", "xxiii", "xxiv", "xxv", "xxvi", "xxvii", "xxviii", "xxix", "xxx", "xxxi", "xxxii", "xxxiii", "xxxiv", "xxxv", "xxxvi", "xxxvii", "xxxviii", "xxxix", "xl", "xli", "xlii", "xliii", "xliv", "xlv", "xlvi", "xlvii", "xlviii", "xlix", "l", "li", "lii", "liii", "liv", "lv", "lvi", "lvii", "lviii", "lix", "lx", "lxi", "lxii", "lxiii", "lxiv", "lxv", "lxvi", "lxvii", "lxviii", "lxix", "lxx", "lxxi", "lxxii", "lxxiii", "lxxiv", "lxxv", "lxxvi", "lxxvii", "lxxviii", "lxxix", "lxxx", "lxxxi", "lxxxii", "lxxxiii", "lxxxiv", "lxxxv", "lxxxvi", "lxxxvii", "lxxxviii", "lxxxix", "xc", "xci", "xcii", "xciii", "xciv", "xcv", "xcvi", "xcvii", "xcviii", "xcix",
  "iii/blank", "iv/blank", "v/blank", "vi/blank", "vii/blank", "viii/blank", "ix/blank", "x/blank", "xi/blank", "xii/blank", "xiii/blank", "xiv/blank", "xv/blank", "xvi/blank", "xvii/blank", "xviii/blank", "xix/blank", "xx/blank", "xxi/blank", "xxii/blank", "xxiii/blank", "xxiv/blank", "xxv/blank", "xxvi/blank", "xxvii/blank", "xxviii/blank", "xxix/blank", "xxx/blank", "xxxi/blank", "xxxii/blank", "xxxiii/blank", "xxxiv/blank", "xxxv/blank", "xxxvi/blank", "xxxvii/blank", "xxxviii/blank", "xxxix/blank", "xl/blank", "xli/blank", "xlii/blank", "xliii/blank", "xliv/blank", "xlv/blank", "xlvi/blank", "xlvii/blank", "xlviii/blank", "xlix/blank", "l/blank", "li/blank", "lii/blank", "liii/blank", "liv/blank", "lv/blank", "lvi/blank", "lvii/blank", "lviii/blank", "lix/blank", "lx/blank", "lxi/blank", "lxii/blank", "lxiii/blank", "lxiv/blank", "lxv/blank", "lxvi/blank", "lxvii/blank", "lxviii/blank", "lxix/blank", "lxx/blank", "lxxi/blank", "lxxii/blank", "lxxiii/blank", "lxxiv/blank", "lxxv/blank", "lxxvi/blank", "lxxvii/blank", "lxxviii/blank", "lxxix/blank", "lxxx/blank", "lxxxi/blank", "lxxxii/blank", "lxxxiii/blank", "lxxxiv/blank", "lxxxv/blank", "lxxxvi/blank", "lxxxvii/blank", "lxxxviii/blank", "lxxxix/blank", "xc/blank", "xci/blank", "xcii/blank", "xciii/blank", "xciv/blank", "xcv/blank", "xcvi/blank", "xcvii/blank", "xcviii/blank", "xcix/blank" };
            string[] chapters = new string[] { "CHAPTER 1", "CHAPTER 2", "CHAPTER 3", "CHAPTER 4", "CHAPTER 5", "CHAPTER 6", "CHAPTER 7", "CHAPTER 8", "CHAPTER 9", "CHAPTER 10", "CHAPTER 11" };
            string[] indexPages = new string[] { "Index–1", "Index–2", "Index–3", "Index–4", "Index–5", "Index–6", "Index–7", "Index–8", "Index–9", "Index–10", "Index–11", "Index–12", "Index–13", "Index–14", "Index–15", "Index–16", "Index–17", "Index–18", "Index–19", "Index–20", "Index–21", "Index–22", "Index–23", "Index–24", "Index–25", "Index–26", "Index–27", "Index–28", "Index–29", "Index–30", "Index–1/blank", "Index–3/blank", "Index–5/blank", "Index–7/blank", "Index–9/blank", "Index–11/blank", "Index–13/blank", "Index–15/blank", "Index–17/blank", "Index–19/blank", "Index–21/blank", "Index–23/blank", "Index–25/blank", "Index–27/blank", "Index–29/blank" };
            List<string> PpfPagesFiltered = new List<string>();
            Regex WorkPackageRegex = new Regex(@"\d{4,4}[–]\d{1,2}|[/][a-n]{5,5}");
            Regex IndexPageRegex = new Regex(@"[I - I]{ 1 }[d - x]{ 4,4}[–]\d+");
            Match WorkPackageMatch;
            Match IndexPageMatch;

            for (int i = 0; i < PdfLines.Length; i++)
            {
                if (PdfLines[i].Length <= 14)
                {
                    WorkPackageMatch = WorkPackageRegex.Match(PdfLines[i]);
                    IndexPageMatch = IndexPageRegex.Match(PdfLines[i]);
                    if (alphabet.Contains(PdfLines[i]) || romanNumerals.Contains(PdfLines[i]) || chapters.Contains(PdfLines[i]) || (WorkPackageMatch.Success && (PdfLines[i].Length == 6 || PdfLines[i].Length == 7 || PdfLines[i].Length == 12 || PdfLines[i].Length == 13)) || indexPages.Contains(PdfLines[i]))
                    {
                        if (PdfLines[i].Length >= 7)
                        {
                            string PdfLineNoBlank = PdfLines[i];
                            if (PdfLineNoBlank[PdfLineNoBlank.Length - 1] == 'k')
                            {
                                PdfLineNoBlank = PdfLineNoBlank.Substring(0, PdfLineNoBlank.Length - 6);
                                PpfPagesFiltered.Add(PdfLineNoBlank);
                            }
                            else
                            {
                                PpfPagesFiltered.Add(PdfLines[i]);
                            }
                        }
                        else
                        {
                            PpfPagesFiltered.Add(PdfLines[i]);
                        }
                    }
                }
            }

            string[] filteredPages = PpfPagesFiltered.ToArray();
            List<string> finalPageList = new List<string>();

            //get the Warning Summary pages
            string warningSummaryPages = string.Empty;

            for (int i = 0; i < filteredPages.Length; i++)
            {
                if (i == 0)
                {
                    warningSummaryPages = filteredPages[i];
                }
                else
                {
                    if (filteredPages[i].Length == 1 && filteredPages[i] == alphabet[i])
                    {
                        warningSummaryPages = filteredPages[i];
                    }
                    else
                    {
                        break;
                    }
                }
            }

            string tocPages = string.Empty;

            //get the table of contents pages
            int rnCounter = 0;
            for (int i = 0; i < filteredPages.Length; i++)
            {
                if (filteredPages[i] == "i" && filteredPages[i + 1] == "iii")
                {
                    tocPages = filteredPages[i];
                    rnCounter++;
                }
                else if (rnCounter > 0)
                {
                    if (filteredPages[i] == romanNumerals[rnCounter])
                    {
                        tocPages = filteredPages[i];
                        rnCounter++;
                    }
                    else
                    {
                        break;
                    }
                }
            }

            // get the workpackages and chapter title pages
            for (int i = 0; i < filteredPages.Length; i++)
            {
                WorkPackageMatch = WorkPackageRegex.Match(filteredPages[i]);
                if (WorkPackageMatch.Success || chapters.Contains(filteredPages[i]))
                {
                    finalPageList.Add(filteredPages[i]);
                }
            }

            string[] OutputList = finalPageList.ToArray();
            finalPageList.Clear();
            string lastItem = string.Empty;

            for (int i = 0; i < OutputList.Length; i++)
            {
                if (i == 0)
                {
                    lastItem = OutputList[i];
                }
                else
                {
                    if (GetFirstFour(OutputList[i]) != GetFirstFour(lastItem))
                    {
                        finalPageList.Add(lastItem);
                        lastItem = OutputList[i];
                    }
                    else if (i == OutputList.Length - 1)
                    {
                        finalPageList.Add(OutputList[i]);
                    }
                    else
                    {
                        lastItem = OutputList[i];
                    }
                }
            }

            //get the rear index pages
            string indxPages = string.Empty;
            for (int i = 0; i < filteredPages.Length; i++)
            {
                if (indexPages.Contains(filteredPages[i]))
                {
                    indxPages = filteredPages[i];
                }
            }

            BuildXMLOutput(warningSummaryPages, tocPages, finalPageList, indxPages);

        }

        private void BuildXMLOutput(string warningSummaryPages, string tocPages, List<string> finalPageList, string indxPages)
        {
            // add cover page & front matter xml code to the output string
            string finalOutput = "\t<loepwp>\r\n" +
           "\t\t<title>List of Effective Pages/Work Packages</title>\r\n" +
           "\t\t<note>\r\n" +
           "\t\t\t<trim.para>NOTE: Zero in the \"Change No.\" column indicates an original page or work package.</trim.para>\r\n" +
           "\t\t</note>\r\n" +
           "\t\t<issuechg>\r\n" +
           "\t\t\t<trim.para>Date of issue for the original manual is:</trim.para>\r\n" +
           "\t\t\t<issued>\r\n" +
           "\t\t\t\t<chgno>Original Draft</chgno>\r\n" +
           "\t\t\t\t<chgdate julian=\"" + JulianDateString + "\">" + dateDDMonthYYYY + "</chgdate>\r\n" +
           "\t\t\t</issued>\r\n" +
           "\t\t</issuechg>\r\n" +
           "\t\t<totalnumberof>\r\n" +
           "\t\t\t<text>THE TOTAL NUMBER OF PAGES FOR FRONT AND REAR MATTER IS </text>\r\n" +
           "\t\t\t<totnum.frnt-rear-pages>XXXXXX</totnum.frnt-rear-pages>\r\n" +
           "\t\t\t<text> AND THE TOTAL NUMBER OF WORK PACKAGES IS </text>\r\n" +
           "\t\t\t<totnum.wps>" + wpnoList.Count.ToString() + "</totnum.wps>\r\n" +
           "\t\t\t<text>, CONSISTING OF THE FOLLOWING:</text>\r\n" +
           "\t\t</totalnumberof>\r\n" +
           "\t\t<col.title>Page / WP No.</col.title>\r\n" +
           "\t\t<col.title>Change No.</col.title>\r\n" +
           "\t <chghistory modified=\"none\">\r\n" +
           "\t\t<title>Front Cover</title>\r\n" +
           "\t\t<totnum.pages></totnum.pages>\r\n" +
           "\t\t<chgno>0</chgno>\r\n" +
           "\t</chghistory>\r\n" +
           "\t<chghistory modified=\"none\">\r\n" +
           "\t\t<pageno>a</pageno>\r\n" +
           "\t\t<pageno>" + warningSummaryPages + "</pageno>\r\n" +
           "\t\t<chgno>0</chgno>\r\n" +
           "\t</chghistory>\r\n" +
           "\t<chghistory modified=\"none\">\r\n" +
           "\t\t<pageno>i</pageno>\r\n" +
           "\t\t<pageno>" + tocPages + "</pageno>\r\n" +
           "\t\t<chgno>0</chgno>\r\n" +
           "\t</chghistory>\r\n";

            // add chapter title pages & work package xml code to the output string
            int wpCounter = -1;
            foreach (string item in finalPageList)
            {
                // Chapter title pages and following blank page
                if (GetFirstFour(item).ToString() == "CHAP")
                {
                    string chapterNumber = item.Remove(0, 7).Insert(0, "Chapter");

                    finalOutput += "\t<chghistory modified=\"none\">\r\n" +
                    "\t\t<title>" + chapterNumber + " Title Page</title>\r\n" +
                    "\t\t<totnum.pages></totnum.pages>\r\n" +
                    "\t\t<chgno>0</chgno>\r\n" +
                    "\t</chghistory>\r\n" +
                    "\t<chghistory modified=\"none\">\r\n" +
                    "\t\t<title>Blank</title>\r\n" +
                    "\t\t<totnum.pages></totnum.pages>\r\n" +
                    "\t\t<chgno>0</chgno>\r\n" +
                    "\t</chghistory>\r\n";
                }
                else
                {
                    wpCounter++;

                    if (ckbAddWpComment.Checked == true)
                    {
                        finalOutput += "\r\n<!--   WP#: " + GetFirstFour(item) + "  -->\r\n";
                    }

                    finalOutput += "\t<chghistory modified=\"none\">\r\n" +
                    "\t\t<wpno wpref=\"" + wpnoList[wpCounter] + "\"/>\r\n" +
                    "\t\t<wppages>" + GetPageNumber(item) + " pgs</wppages>\r\n" +
                    "\t\t<chgno>0</chgno>\r\n" +
                    "\t</chghistory>\r\n";
                }
            }

            // add index pages & rear matter xml code to the output string
            finalOutput += "\t<chghistory modified=\"none\">\r\n" +
            "\t\t<pageno>Index–1</pageno>\r\n" +
            "\t\t<pageno>" + indxPages + "</pageno>\r\n" +
            "\t\t<chgno>0</chgno>\r\n" +
            "\t</chghistory>\r\n" +
             "\t<chghistory modified=\"none\">\r\n" +
            "\t\t<title>Inside back cover</title>\r\n" +
            "\t\t<totnum.pages></totnum.pages>\r\n" +
            "\t\t<chgno>0</chgno>\r\n" +
            "\t</chghistory>\r\n" +
            "\t<chghistory modified=\"none\">\r\n" +
            "\t\t<title>Back cover</title>\r\n" +
            "\t\t<totnum.pages></totnum.pages>\r\n" +
            "\t\t<chgno>0</chgno>\r\n" +
            "\t</chghistory>\r\n";

            rtbOutputPreview.Text = finalOutput;
            btnCopyToClipboard.Enabled = true;
            btnExportCode.Enabled = true;
        }

        private string GetPageNumber(string item)
        {
            int pgNum = Convert.ToInt32(item.Substring(5, item.Length - 5));
            // if pgNum is odd add 1 to it to make it even and account for blank page
            if (pgNum % 2 != 0)
            {
                pgNum++;
            }
            return pgNum.ToString();
        }

        private string GetFirstFour(string source)
        {
            string firstFour = string.Empty;
            if (source.Length >= 4)
            {
                firstFour = source.Substring(0, 4);
            }
            return firstFour;
        }

        private int GetWPNum(string item)
        {
            string WPNumText = item.Substring(0, 4);
            int WPNum = Convert.ToInt32(WPNumText);
            return WPNum;
        }

        private void btnCopyToClipboard_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(rtbOutputPreview.Text);
            lblStatus.Text = "Preview text copied to clipboard.";
        }

        private void btnExportCode_Click(object sender, EventArgs e)
        {
            var sfd = new SaveFileDialog();

            sfd.Filter = "txt files (*.txt)|*.txt";
            sfd.FileName = "LOEPWP-Output.txt";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                finalOutput = rtbOutputPreview.Text;
                File.WriteAllText(sfd.FileName, finalOutput);
                MessageBox.Show("Your file has been saved.\n\nFile Location:\t" + sfd.FileName);
            }
        }

        private string[] PdfTextParseArray(string pdfText)
        {
            List<string> tempPdfLines = new List<string>();
            String[] pdfLines = new string[] { };
            pdfLines = pdfText.Split(new char[] { '\r', '\t' });

            for (int i = 0; i < pdfLines.Length; i++)
            {
                pdfLines[i] = Regex.Replace(pdfLines[i], @"\r\n?|\n", "");
                if (pdfLines[i] != "")
                {
                    tempPdfLines.Add(pdfLines[i]);
                }
            }
            pdfLines = tempPdfLines.ToArray();
            return pdfLines;
        }

        private void ResetForm()
        {
            rtbOutputPreview.Clear();
            lblSelectedXmlTM.Text = "XML TM:   none selected";
            lblSelectedPdfTM.Text = "PDF TM:   none selected";
            lblStatus.Text = "";
            lblJulianDate.Text = "Julian Date: ...";
            btnSelectPDF.Enabled = false;
            btnExportCode.Enabled = false;
            btnCopyToClipboard.Enabled = false;
            btnGenerateCode.Enabled = false;
        }

        private void QuitApplication(object sender, EventArgs e)
        {
            if (MessageBox.Show("You are about to exit the application.\n\nProceed?", "Exit Application?", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                this.Close();
            }
        }

        private void dtpDateOfIssue_ValueChanged(object sender, EventArgs e)
        {
            DateTime date = dtpDateOfIssue.Value;
            double julianDate = date.ToOADate() + 2415018.5;
            int formattedJulian = (int)Math.Floor(julianDate);
            lblJulianDate.Text = "Julian Date: " + formattedJulian.ToString();
            JulianDateString = formattedJulian.ToString();
            string dateText = dtpDateOfIssue.Value.ToShortDateString();

            //getting Date into '31 January, 2018' format
            string FormattedDateText = string.Empty;
            char[] dateCharArr = dateText.ToCharArray();
            string day = string.Empty;
            string month = string.Empty;
            string year = string.Empty;
            string temp = string.Empty;

            for (int i = 0; i < dateCharArr.Length; i++)
            {
                if (dateCharArr[i] == '/')
                {
                    //set variable
                    if (month == "")
                    {
                        month = temp;
                        temp = "";
                    }
                    else
                    {
                        day = temp;
                        temp = "";
                    }

                }
                else if (i != dateCharArr.Length - 1)
                {
                    //add to variable
                    temp += dateCharArr[i].ToString();
                }
                else
                {
                    temp += dateCharArr[i].ToString();
                    year = temp;
                }
            }

            dateDDMonthYYYY = day + " " + Month[Convert.ToInt32(month) - 1] + " " + year;
        }

        private void btnSelectPDF_Click(object sender, EventArgs e)
        {
            ImportPdfFile();
        }

        private void btnSelectTM_Click(object sender, EventArgs e)
        {
            ResetForm();
            ImportXmlFile();
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

        private void btnGenerateCode_Click(object sender, EventArgs e)
        {
            GenerateLOEPWP();
        }

        private void ImportXmlFile()
        {
            try
            {
                //user browses to folder and selects file
                OpenFileDialog xmlfileSelected = new OpenFileDialog();

                xmlfileSelected.Filter = "XML Files (*.xml)|*.xml|Text Files (*.txt)|*.txt|SGML Files (*.sgml)|*.sgml";
                xmlfileSelected.Multiselect = false;

                if (xmlfileSelected.ShowDialog() == DialogResult.OK)
                {
                    string lines = File.ReadAllText(xmlfileSelected.FileName);

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
                    lblSelectedXmlTM.Text = "XML TM:   " + xmlfileSelected.SafeFileName;
                    btnSelectPDF.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error has occured while trying to import the document.\n\nMake sure file is not already being accessed by another application.\n\n\n" + ex.ToString(), "Error!");
            }
        }

        private void ImportPdfFile()
        {
            try
            {
                //user browses to folder and selects file
                OpenFileDialog pdffileSelected = new OpenFileDialog();
                pdffileSelected.Filter = "PDF Files (*.pdf)|*.pdf";
                pdffileSelected.Multiselect = false;

                if (pdffileSelected.ShowDialog() == DialogResult.OK)
                {
                    string pdfAllText = string.Empty;
                    filepath = pdffileSelected.FileName.ToString();

                    TextExtractor te = new TextExtractor();
                    string pdfText = te.Extract(filepath).ToString();

                    PdfLines = PdfTextParseArray(pdfText);

                    lblSelectedPdfTM.Text = "PDF TM:   " + pdffileSelected.SafeFileName;
                    btnGenerateCode.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error has occured while trying to import the document.\n\nMake sure file is not already being accessed by another application.\n\n\n" + ex.ToString(), "Error!");
            }
        }

        private void btnResetForm_Click(object sender, EventArgs e)
        {
            ResetForm();
        }
    }
}
