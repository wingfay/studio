using System;
using System.Windows.Forms;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;
using HtmlDocument = HtmlAgilityPack.HtmlDocument;

namespace WinForm_Sudio
{
   public partial class FormWCAGCheck : Form
   {
      public FormWCAGCheck()
      {
         InitializeComponent();
         txtFolder.Text = "D:\\Projects\\Webopac2015\\Webopac2015\\";

         dataGridView1.ReadOnly = false;
         dataGridView1.AllowUserToAddRows = false;
         dataGridView1.MultiSelect = false;
         dataGridView1.SelectionMode = DataGridViewSelectionMode.CellSelect;


      }

      private void btnCheck_Click(object sender, EventArgs e)
      {
         if (string.IsNullOrEmpty(txtFolder.Text))
         {
            DialogResult result = folderBrowserDialog1.ShowDialog(this);

            if(result == DialogResult.OK)
            {
               txtFolder.Text = folderBrowserDialog1.SelectedPath;

            }
         }

         var Viewpath = txtFolder.Text + "Views";

         var jsPath = txtFolder.Text + "Scripts\\opac";

         var list = CheckStart(Viewpath, "*.cshtml");

         list = list.Union(CheckStart(jsPath, "*.js").ToList());

         var csPath = txtFolder.Text + "App_Code";


         list = list.Union(CheckStart(csPath, "*.cs").ToList());


        // var testFile = "D:\\Projects\\Webopac2015\\Webopac2015\\App_Code\\Controllers\\ItemDetailController.cs";

         //CheckSingleFile("D:\\Projects\\Webopac2015\\Webopac2015\\App_Code\\Controllers\\", testFile, "*.cs");


         this.dataGridView1.DataSource = list.OrderByDescending(file => file.CheckBoxCount + file.LabelCount + file.ACount+ file.IMGCount + file.DIVCount+file.ButtonCount).ToList();

      }

      private FileData CheckSingleFile(string strPath, string file,string filter)
      {
         bool existComment = false;

         var filedata = new FileData { FileName = file, CheckBoxCount = 0 };

         if (file.IndexOf("\\Manager\\") >= 0 || file.ToLower().IndexOf("manager") >= 0)
         {
            return null;
         }

         filedata.FileName = filedata.FileName.Replace(strPath, "");

         filedata.CheckBoxLineDesc = string.Empty;

         var lines = File.ReadAllLines(file);

         if (lines.Count() <= 0)
         {
            return null;
         }

         string content = string.Empty;

         int index = 0;

         string tagHtml = string.Empty;
         do
         {



            if (string.IsNullOrEmpty(content))
            {
               content = lines[index];
            }

            int line = index + 1;

            if (string.IsNullOrEmpty(content))
            {
               index++;
               continue;
            }

            #region  check comment


            if ((filter.Equals("*.js") || filter.Equals("*.cs")))
            {
               if (content.Contains("//"))
               {
                  var indexComment = content.IndexOf("//");


                  var sub = content.Substring(0, content.IndexOf("//"));

                  if (isVaildComment(sub))
                  {
                     if (string.IsNullOrWhiteSpace(sub))
                     {
                        index++;
                        content = string.Empty;
                        continue;
                     }
                     else
                     {
                        content = sub;
                     }
                  }

               }


            }
            else
            {
               if (content.Contains("@*") && content.Contains("*@"))
               {
                  index++;
                  content = string.Empty;
                  continue;
               }
               else if (content.Contains("@*"))
               {
                  existComment = true;
                  index++;
                  content = string.Empty;
                  continue;
               }
            }

            if (existComment)
            {

               if (content.Contains("*@"))
               {
                  existComment = false;


               }
               index++;
               content = string.Empty;
               continue;
            }

            #endregion

            if (content.IndexOf("<") < 0)
            {
               index++;
               content = string.Empty;
               continue;
            }

            if (content.IndexOf(">") < 0)
            {
               tagHtml = content;

               int j = index + 1;

               for (; j < lines.Length; j++)
               {
                  if (lines[j].ToLower().IndexOf(">") >= 0)
                  {
                     break;
                  }

                  tagHtml += lines[j].Trim();


               }

               if (j < lines.Count())
               {
                  tagHtml += lines[j].Substring(0, lines[j].IndexOf(">")).Trim();
                  content = lines[j].Substring(lines[j].IndexOf(">") + 1);
               }

               index = j;
            }
            else
            {

               if (content.IndexOf(">") < content.IndexOf("<"))
               {
                  content = string.Empty;
                  ++index;
                  continue;
               }

               tagHtml = content.Substring(content.IndexOf("<"), content.IndexOf(">") - content.IndexOf("<") + 1);

               content = content.Substring(content.IndexOf(">") + 1);
            }





            bool jump = ParseHTML(tagHtml, lines, index + 1, ref index, content, filedata);


            if (string.IsNullOrEmpty(content))
            {
               if (index + 1 < lines.Count())
               {
                  content = lines[++index];
               }
               else
               {
                  break;
               }

            }

            if (index >= lines.Count())
            {
               break;
            }

         } while (lines.Count() > index);


         return filedata;

      }

      private IEnumerable<FileData> CheckStart(string strPath,string filter)
      {
         var files = Directory.GetFiles(strPath, filter, SearchOption.AllDirectories);

         List<FileData> filelist = new List<FileData>();



         foreach (var item in files)
         {

            FileData fileData = CheckSingleFile(strPath, item, filter);


            if(fileData !=null && (fileData.CheckBoxCount + fileData.LabelCount + fileData.ACount + fileData.IMGCount + fileData.DIVCount + fileData.ButtonCount) > 0)
            {
               filelist.Add(fileData);
            }

         }

         return filelist;
      }


      private bool ParseHTML(string tagHTML,  string[] lines,int line,ref int index,string content,  FileData fileData)
      {
         bool jump = false;

         if (tagHTML.ToLower().IndexOf("<label ") >= 0)
         {
            if (tagHTML.ToLower().IndexOf("for=") < 0 && tagHTML.ToLower().IndexOf("for =") < 0)
            {
               fileData.LabelCount++;

               if (string.IsNullOrEmpty(fileData.LabelLineDesc) == false)
               {
                  fileData.LabelLineDesc += "  ";
               }

               fileData.LabelLineDesc += string.Format("{0}", line);
            }


         }
         else if (tagHTML.ToLower().IndexOf("<a ") >= 0)
         {
            if ((tagHTML.ToLower().IndexOf("href=") < 0 && tagHTML.ToLower().IndexOf("href =") < 0
               && tagHTML.ToLower().IndexOf("#=href#") <= 0 && tagHTML.ToLower().IndexOf("#= href#") <= 0)
               || (tagHTML.ToLower().IndexOf("href='#'") > 0 || tagHTML.ToLower().IndexOf("href=\"#\"") > 0 
               || tagHTML.ToLower().IndexOf("href =\"#\"") > 0 || tagHTML.ToLower().IndexOf("href='\\\\#'") > 0
               || tagHTML.ToLower().IndexOf("href=\\\"#\\\"") > 0))
            {
               fileData.ACount++;

               if (string.IsNullOrEmpty(fileData.ALineDesc) == false)
               {
                  fileData.ALineDesc += "  ";
               }

               fileData.ALineDesc += string.Format("{0}", line);
            }

            if ((tagHTML.ToLower().IndexOf("onmouseover") > 0 && tagHTML.ToLower().IndexOf("onfocus") < 0)
                  || (tagHTML.ToLower().IndexOf("onmouseover") < 0 && tagHTML.ToLower().IndexOf("onfocus") > 0)
                  || (tagHTML.ToLower().IndexOf("onmouseout") < 0 && tagHTML.ToLower().IndexOf("onblur") > 0)
                  || (tagHTML.ToLower().IndexOf("onmouseout") > 0 && tagHTML.ToLower().IndexOf("onblur") < 0)
                  )
            {
               fileData.ACount++;

               if (string.IsNullOrEmpty(fileData.ALineDesc) == false)
               {
                  fileData.ALineDesc += "  ";
               }

               fileData.ALineDesc += string.Format("{0}", line);
            }




         }
         else if (tagHTML.ToLower().IndexOf("<img ") >= 0)
         {
            if (tagHTML.ToLower().IndexOf("alt=") < 0)
            {
               fileData.IMGCount++;

               if (string.IsNullOrEmpty(fileData.IMGLineDesc) == false)
               {
                  fileData.IMGLineDesc += "  ";
               }

               fileData.IMGLineDesc += string.Format("{0}", line);
            }
         }
         else if (tagHTML.ToLower().IndexOf("<div ") >= 0)
         {

            if (tagHTML.ToLower().IndexOf("onclick") > 0 || tagHTML.ToLower().IndexOf("dblclick") > 0 || tagHTML.ToLower().IndexOf("mouseover") > 0)
            {
               fileData.DIVCount++;

               if (string.IsNullOrEmpty(fileData.DIVLineDesc) == false)
               {
                  fileData.DIVLineDesc += "  ";
               }

               fileData.DIVLineDesc += string.Format("{0}", line);
            }

         }
         else if (tagHTML.ToLower().IndexOf("<input ") >= 0)
         {
           

            if ((tagHTML.ToLower().IndexOf(@"type=""checkbox""") >= 0 || tagHTML.ToLower().IndexOf("checkbox") >= 0)
           && (tagHTML.ToLower().IndexOf(@"title=") <= 0 && tagHTML.ToLower().IndexOf(@"title =") <= 0))
            {


               fileData.CheckBoxCount++;

               if (string.IsNullOrEmpty(fileData.CheckBoxLineDesc) == false)
               {
                  fileData.CheckBoxLineDesc += "  ";
               }

               fileData.CheckBoxLineDesc += string.Format("{0}", line);



            }
            else if (tagHTML.ToLower().IndexOf("button") > 0 && (tagHTML.ToLower().IndexOf("value=") < 0 && tagHTML.ToLower().IndexOf("value =") < 0))
            {
               if (tagHTML.ToLower().IndexOf("aria-label") < 0)
               {
                  fileData.ButtonCount++;

                  if (string.IsNullOrEmpty(fileData.ButtonLineDesc) == false)
                  {
                     fileData.ButtonLineDesc += "  ";
                  }

                  fileData.ButtonLineDesc += string.Format("{0}", line);
               }
            }

         }
         else if (tagHTML.ToLower().IndexOf("<button ") >= 0)
         {
            if (tagHTML.Contains("/>"))
            {
               if (tagHTML.ToLower().IndexOf("aria-label") < 0)
               {
                  fileData.ButtonCount++;

                  if (string.IsNullOrEmpty(fileData.ButtonLineDesc) == false)
                  {
                     fileData.ButtonLineDesc += "  ";
                  }

                  fileData.ButtonLineDesc += string.Format("{0}", line);
               }
            }
            else
            {
               var buttonInnerHTML = string.Empty;

               if (content.ToLower().Contains("</button>"))
               {
                  buttonInnerHTML = content.Substring(0, content.ToLower().IndexOf("</button>")).Trim();

                  content = content.Substring(content.ToLower().IndexOf("</button>")+9);
               }
               else
               {
                  int j = index + 1;



                  for (; j < lines.Length; j++)
                  {
                     if (lines[j].ToLower().IndexOf("</button>") >= 0)
                     {
                        jump = true;
                        break;
                     }

                     buttonInnerHTML += lines[j].Trim();


                  }

                  if (j < lines.Count())
                  {
                     buttonInnerHTML += lines[j].Substring(0, lines[j].ToLower().IndexOf("</button>")).Trim();

                     content = lines[j].Substring(lines[j].ToLower().IndexOf("</button>") + 9);
                  }

 
                  index = j;

                 
               }

               if (buttonInnerHTML.Length < 0)
               {
                  fileData.ButtonCount++;

                  if (string.IsNullOrEmpty(fileData.ButtonLineDesc) == false)
                  {
                     fileData.ButtonLineDesc += "  ";
                  }

                  fileData.ButtonLineDesc += string.Format("{0}", line);
               }
               else
               {

                  HtmlDocument doc = new HtmlDocument();

                  doc.LoadHtml(buttonInnerHTML);

                  if (doc.DocumentNode.InnerText.Trim().Length <= 0)
                  {
                     if (buttonInnerHTML.IndexOf("<") > 0
                    && (buttonInnerHTML.IndexOf("alt=") < 0 && buttonInnerHTML.IndexOf("alt =") < 0 && buttonInnerHTML.IndexOf("title=") < 0 && buttonInnerHTML.IndexOf("title =") < 0))
                     {
                        fileData.ButtonCount++;

                        if (string.IsNullOrEmpty(fileData.ButtonLineDesc) == false)
                        {
                           fileData.ButtonLineDesc += "  ";
                        }

                        fileData.ButtonLineDesc += string.Format("{0}", line);
                     }
                  }
               }

              




            }

            


         }



         return jump;
      }



      private bool isVaildComment(string str)
      {
         if(str.Contains("\"") ==false && str.Contains("'")== false)
         {
            return true;
         }

         char doubleMarks = '"';

         char singleMarks = '\'';

         int countDoubleMarks = 0;

         int countSingleMarks = 0;

         foreach (char item in str)
         {
            if (item == doubleMarks)
            {
               ++countDoubleMarks;
            }

            if (item == singleMarks)
            {
               ++countSingleMarks;
            }
         }
         if(countDoubleMarks>0 && countDoubleMarks % 2 > 0)
         {
            return false;
         }

         if (countSingleMarks > 0 && countSingleMarks % 2 > 0)
         {
            return false;
         }


         return true;
      }
      

   }

   public class FileData
   {
      public string FileName { get; set; }

      public int CheckBoxCount { get; set; }

      public string CheckBoxLineDesc { get; set; }

      public int LabelCount { get; set; }

      public string LabelLineDesc { get; set; }

      public int ACount { get; set; }

      public string ALineDesc { get; set; }

      public int IMGCount { get; set; }

      public string IMGLineDesc { get; set; }


      public int DIVCount { get; set; }

      public string DIVLineDesc { get; set; }

      public int ButtonCount { get; set; }

      public string ButtonLineDesc { get; set; }






   }
}
