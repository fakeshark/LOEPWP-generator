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
        string finalOutput;
        string searchStringStart = "wpno=\"";
        string searchStringEnd = "\"";
        List<string> wpnoList = new List<string> { };
        string pdfAllText = string.Empty;
        string dateDDMonthYYYY = "XXXXXX";
        string JulianDateString = "XXXXXX";
        string[] PdfLines = new string[] { };
        string[] Month = new string[] { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };

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

            //todo: get alphbet lookup to not get the roman numerals

            #region Warning Summary pages

            string frontMatterABC = "a";
            string[] alphabet = new string[] { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };

            List<string> alphabetCheck = new List<string>();
            for (int i = 0; i < PdfLines.Length; i++)
            {
                if (PdfLines[i].Length == 1 || PdfLines[i].Length == 7)
                {
                    if (PdfLines[i].Length == 7)
                    {
                        if (PdfLines[i].Substring(1, 6) == "/blank")
                        {
                            alphabetCheck.Add(PdfLines[i]);
                        }
                    }
                    else
                    {
                        alphabetCheck.Add(PdfLines[i]);
                    }
                }
            }
            string[] alphabetCheckArr = alphabetCheck.ToArray();
            alphabetCheck.Clear();

            for (int i = 0; i < alphabetCheckArr.Length; i++)
            {
                if (alphabetCheckArr[i] == alphabet[i] || alphabetCheckArr[i] == alphabet[i] + "/blank")
                {
                    alphabetCheck.Add(alphabetCheckArr[i]);
                }
                else
                {
                    break;
                }
            }

            string[] fmABC = alphabetCheck.ToArray();
            string frontMatterABCblank = string.Empty;

            if (fmABC[fmABC.Length - 1].Length == 7)
            {
                char[] letter = fmABC[fmABC.Length - 1].ToCharArray();
                fmABC[fmABC.Length - 1] = letter[0].ToString();
                frontMatterABCblank = "\t<chghistory modified=\"none\">\r\n" +
                                                            "\t\t<title>Blank</title>\r\n" +
                                                            "\t\t<chgno>0</chgno>\r\n" +
                                                            "\t</chghistory>\r\n";
            }

            frontMatterABC = fmABC[0] + "-" + fmABC[fmABC.Length - 1];
            #endregion

            #region TOC and "How to use this manual" pages
            string[] romanNumerals = new string[] { "iii", "iv", "v", "vi", "vii", "viii", "ix", "x", "xi", "xii", "xiii", "xiv", "xv", "xvi", "xvii", "xviii", "xix", "xx", "xxi", "xxii", "xxiii", "xxiv", "xxv", "xxvi", "xxvii", "xxviii", "xxix", "xxx", "xxxi", "xxxii", "xxxiii", "xxxiv", "xxxv", "xxxvi", "xxxvii", "xxxviii", "xxxix", "xl", "xli", "xlii", "xliii", "xliv", "xlv", "xlvi", "xlvii", "xlviii", "xlix", "l", "li", "lii", "liii", "liv", "lv", "lvi", "lvii", "lviii", "lix", "lx", "lxi", "lxii", "lxiii", "lxiv", "lxv", "lxvi", "lxvii", "lxviii", "lxix", "lxx", "lxxi", "lxxii", "lxxiii", "lxxiv", "lxxv", "lxxvi", "lxxvii", "lxxviii", "lxxix", "lxxx", "lxxxi", "lxxxii", "lxxxiii", "lxxxiv", "lxxxv", "lxxxvi", "lxxxvii", "lxxxviii", "lxxxix", "xc", "xci", "xcii", "xciii", "xciv", "xcv", "xcvi", "xcvii", "xcviii", "xcix",
  "iii/blank", "iv/blank", "v/blank", "vi/blank", "vii/blank", "viii/blank", "ix/blank", "x/blank", "xi/blank", "xii/blank", "xiii/blank", "xiv/blank", "xv/blank", "xvi/blank", "xvii/blank", "xviii/blank", "xix/blank", "xx/blank", "xxi/blank", "xxii/blank", "xxiii/blank", "xxiv/blank", "xxv/blank", "xxvi/blank", "xxvii/blank", "xxviii/blank", "xxix/blank", "xxx/blank", "xxxi/blank", "xxxii/blank", "xxxiii/blank", "xxxiv/blank", "xxxv/blank", "xxxvi/blank", "xxxvii/blank", "xxxviii/blank", "xxxix/blank", "xl/blank", "xli/blank", "xlii/blank", "xliii/blank", "xliv/blank", "xlv/blank", "xlvi/blank", "xlvii/blank", "xlviii/blank", "xlix/blank", "l/blank", "li/blank", "lii/blank", "liii/blank", "liv/blank", "lv/blank", "lvi/blank", "lvii/blank", "lviii/blank", "lix/blank", "lx/blank", "lxi/blank", "lxii/blank", "lxiii/blank", "lxiv/blank", "lxv/blank", "lxvi/blank", "lxvii/blank", "lxviii/blank", "lxix/blank", "lxx/blank", "lxxi/blank", "lxxii/blank", "lxxiii/blank", "lxxiv/blank", "lxxv/blank", "lxxvi/blank", "lxxvii/blank", "lxxviii/blank", "lxxix/blank", "lxxx/blank", "lxxxi/blank", "lxxxii/blank", "lxxxiii/blank", "lxxxiv/blank", "lxxxv/blank", "lxxxvi/blank", "lxxxvii/blank", "lxxxviii/blank", "lxxxix/blank", "xc/blank", "xci/blank", "xcii/blank", "xciii/blank", "xciv/blank", "xcv/blank", "xcvi/blank", "xcvii/blank", "xcviii/blank", "xcix/blank" };
            List<string> tempRoman = new List<string>();
            for (int i = 0; i < PdfLines.Length; i++)
            {
                if (PdfLines[i].Length <= 14)
                {
                    if (romanNumerals.Contains(PdfLines[i]))
                    {
                        tempRoman.Add(PdfLines[i]);
                    }
                }
            }
            string[] onlyRomanNums = tempRoman.ToArray();
            tempRoman.Clear();
            int offset = 0;
            for (int i = 0; i < onlyRomanNums.Length; i++)
            {
                if (onlyRomanNums[i] == romanNumerals[i + offset] || onlyRomanNums[i] == romanNumerals[i + offset] + "/blank")
                {
                    if (onlyRomanNums[i] == romanNumerals[i + offset] + "/blank")
                    {
                        tempRoman.Add(onlyRomanNums[i]);
                        offset++;
                    }
                    else
                    {
                        tempRoman.Add(onlyRomanNums[i]);
                    }
                }
                else
                {
                    break;
                }
            }
            string[] tempRomanArr = tempRoman.ToArray();
            string tocHowToUse = "i-" + tempRomanArr[tempRomanArr.Length - 1];
            #endregion

            #region Chapter Pages And Work Packages
            List<string> ChapterAndWPs = new List<string>();
            List<string> IndexPgs = new List<string>();

            Regex ChapterTitlePageRegex = new Regex(@"[A-T]{7}\s\d+");
            Regex WorkPackageRegex = new Regex(@"\d{4,4}[–]\d{1,2}|[/][a-z]{5,5}");
            Regex IndexRegex = new Regex(@"[I - I]{ 1 }[d - x]{ 4,4}[–]\d+");
            string rearIndex = string.Empty;
            Match ChapterMatch;
            Match WPMatch;
            Match IndexMatch;
            string lastWP = string.Empty;
            int counter = 1;

            for (int i = 0; i < PdfLines.Length; i++)
            {
                //get Chapter title page
                if (PdfLines[i].Length == 9 || PdfLines[i].Length == 10)
                {
                    ChapterMatch = ChapterTitlePageRegex.Match(PdfLines[i]);
                    if (ChapterMatch.Success && PdfLines[i] != "e/blank")
                    {
                        ChapterAndWPs.Add(PdfLines[i]);
                    }
                }
                //get Work Package page Number
                if (PdfLines[i].Length == 6 || PdfLines[i].Length == 7 || PdfLines[i].Length == 12 || PdfLines[i].Length == 13)
                {
                    //ChapterAndWPs.Add(PdfLines[i]);
                    WPMatch = WorkPackageRegex.Match(PdfLines[i]);
                    if (WPMatch.Success && PdfLines[i] != "e/blank")
                    {
                        ChapterAndWPs.Add(PdfLines[i]);
                    }
                }
                //get Index pages
                 if (PdfLines[i].Length > 6 && PdfLines[i].Substring(0, 6) == "Index–")
                {
                    string test = PdfLines[i];
                    IndexPgs.Add(PdfLines[i]);
                }
            }

            string lastItem = string.Empty;
            List<string> Output = new List<string>();
            counter = 1;

            foreach (string item in ChapterAndWPs)
            {
                //handle Chapter Title Pages
                if (item[0] == 'C')
                {
                    char[] ChapterString = item.ToCharArray();
                    string TitleCaseChapter = string.Empty;
                    for (int i = 0; i < ChapterString.Length; i++)
                    {
                        if (i != 0)
                        {
                            TitleCaseChapter += ChapterString[i].ToString().ToLower();
                        }
                        else
                        {
                            TitleCaseChapter += ChapterString[i];
                        }
                    }
                    Output.Add(TitleCaseChapter + " Title Page");
                    Output.Add("Blank");
                }
                //handle wp numbers
                if (item[4] == '–')
                {
                    int currentWPNum = Convert.ToInt32(item.Substring(0, 4));

                    if (counter == 1)
                    {
                        lastItem = item;
                        counter++;
                    }
                    else
                    {
                        if (GetWPNum(item) > GetWPNum(lastItem))
                        {
                            Output.Add(AddItemTo(lastItem));
                            lastItem = item;
                        }
                        else
                        {
                            if (ChapterAndWPs.IndexOf(item) == ChapterAndWPs.Count - 1)
                            {
                                Output.Add(AddItemTo(item));
                            }
                            else
                            {
                                lastItem = item;
                            }
                        }
                    }
                }
            }
            foreach (string item in IndexPgs)
            {
                    rearIndex = item;
            }

            #endregion

            #region Build XML Output
            string[] ChaptersWPs = Output.ToArray();
            string[] WPrefsAndTitles = wpnoList.ToArray();

            int totalPagesFrontRear = 0;
            int totalNumberWPs = 0;
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
            "\t\t\t<totnum.frnt-rear-pages>" + totalPagesFrontRear + "</totnum.frnt-rear-pages>\r\n" +
            "\t\t\t<text> AND THE TOTAL NUMBER OF WORK PACKAGES IS </text>\r\n" +
            "\t\t\t<totnum.wps>" + totalNumberWPs + "</totnum.wps>\r\n" +
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
            "\t\t<title>" + frontMatterABC + "</title>\r\n" +
            "\t\t<totnum.pages></totnum.pages>\r\n" +
            "\t\t<chgno>0</chgno>\r\n" +
            "\t</chghistory>\r\n" + frontMatterABCblank +
            "\t<chghistory modified=\"none\">\r\n" +
            "\t\t<title>" + tocHowToUse + "</title>\r\n" +
            "\t\t<totnum.pages></totnum.pages>\r\n" +
            "\t\t<chgno>0</chgno>\r\n" +
            "\t</chghistory>\r\n";
            int wpCounter = 0;
            string newEntry = string.Empty;

            for (int i = 0; i < ChaptersWPs.Length; i++)
            {
                if (ChaptersWPs[i][0] != 'C' && ChaptersWPs[i][0] != 'B')
                {
                    newEntry = "\t<!-- work package #: " + ChaptersWPs[i].Substring(0, 4) + " -->\r\n" +
                    "\t<chghistory modified=\"none\">\r\n" +
                    "\t\t<wpno wpref=\"" + WPrefsAndTitles[wpCounter] + "\"/>\r\n" +
                    "\t\t<wppages>" + ChaptersWPs[i].Substring(5, ChaptersWPs[i].Length - 5) + " Pages</wppages>\r\n" +
                    "\t\t<chgno>0</chgno>\r\n" +
                    "\t</chghistory>\r\n";

                    finalOutput += newEntry;
                    wpCounter++;
                }
                else
                {
                    newEntry = "\t<chghistory modified=\"none\">\r\n" +
                    "\t\t<title>" + ChaptersWPs[i] + "</title>\r\n" +
                    "\t\t<totnum.pages></totnum.pages>\r\n" +
                    "\t\t<chgno>0</chgno>\r\n" +
                    "\t</chghistory>\r\n";
                    finalOutput += newEntry;
                }
            }
            if (rearIndex.Length > 0)
            {
                rearIndex = "Index–1 - " + rearIndex;
                finalOutput += "\t<chghistory modified=\"none\">\r\n" +
                    "\t\t<title>" + rearIndex + "</title>\r\n" +
                    "\t\t<totnum.pages></totnum.pages>\r\n" +
                    "\t\t<chgno>0</chgno>\r\n" +
                    "\t</chghistory>\r\n";
            }

            finalOutput += "\t</loepwp>\r\n";

            rtbOutputPreview.Text = finalOutput;
            #endregion

        }

        private string AddItemTo(string item)
        {
            string formattedItem = string.Empty;
            string wpno = item.Substring(0, 4);
            if (item.Length == 6)
            {
                formattedItem += wpno + "," + item.Substring(5, 1);
            }
            if (item.Length == 7)
            {
                formattedItem += wpno + "," + item.Substring(5, 2);
            }
            if (item.Length == 12)
            {
                formattedItem += wpno + "," + (Convert.ToInt32(item.Substring(5, 1)) + 1).ToString();
            }
            if (item.Length == 13)
            {
                formattedItem += wpno + "," + (Convert.ToInt32(item.Substring(5, 2)) + 1).ToString();
            }
            return formattedItem;
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
                PdfLines = PdfTextParseArray(pdfText);
                for (int i = 0; i < PdfLines.Length; i++)
                {
                    pdfAllText += PdfLines[i] + "\n";
                }

                GenerateLOEWP();

                //try
                //{
                //    GenerateLOEWP();
                //}
                //catch (Exception ex)
                //{
                //    MessageBox.Show("There seems to be a problem...\n\nPlease check the source files for errors.\n\n\n" + ex.Message, "Warning!");
                //}
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

            dateDDMonthYYYY = day + " " + Month[Convert.ToInt32(month) - 1] + ", " + year;
        }
    }
}
