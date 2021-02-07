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
         //不允许添加行
         dataGridView1.AllowUserToAddRows = false;
         //只允许选中单行
         dataGridView1.MultiSelect = false;
         //整行选中
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

         var list = CheckStart(Viewpath,"*.cshtml");

         list = list.Union(CheckStart(jsPath, "*.js").ToList());

         this.dataGridView1.DataSource = list.OrderByDescending(file => file.CheckBoxCount + file.LabelCount + file.ACount+ file.IMGCount + file.DIVCount+file.ButtonCount).ToList();

      }

      private IEnumerable<FileData> CheckStart(string strPath,string filter)
      {
         var files = Directory.GetFiles(strPath, filter, SearchOption.AllDirectories);

         List<FileData> filelist = new List<FileData>();

         bool existComment = false;


         foreach (var item in files)
         {
            var filedata = new FileData { FileName = item, CheckBoxCount = 0 };

            if (item.IndexOf("\\Manager\\") >= 0 || (filter.Contains("js") && item.ToLower().IndexOf("manager")>=0))
            {
               continue;
            }

            filedata.FileName = filedata.FileName.Replace(strPath, "");

            filedata.CheckBoxLineDesc = string.Empty;

            var lines = File.ReadAllLines(item);

          

            for (int i = 0; i < lines.Count(); i++)
            {
               string content = lines[i];

               int line = i + 1;


               if (string.IsNullOrEmpty(content))
               {
                  continue;
               }

               #region  check comment
               
               if ((content.Contains("@*") && content.Contains("*@")) || (filter.Contains("js") && content.Contains("//")))
               {
                  continue;
               }
               else if (content.Contains("@*"))
               {
                  existComment = true;

                  continue;
               }

               if (existComment)
               {

                  if (content.Contains("*@"))
                  {
                     existComment = false;


                  }
                  continue;
               }

               #endregion




               if (content.IndexOf("<") < 0)
               {
                  continue;
               }

               //var tagHtml = string.Empty;
               //if (content.IndexOf(">") < 0)
               //{
               //   tagHtml = content;

               //   int j = i + 1;

               //   for (; j < lines.Length; j++)
               //   {
               //      if (lines[j].ToLower().IndexOf(">") >= 0)
               //      {
               //         break;
               //      }

               //      tagHtml += lines[j].Trim();

                    
               //   }

               //   tagHtml += lines[j].Substring(0, lines[j].ToLower().IndexOf(">")).Trim();
               //}

               

              

               if (content.ToLower().IndexOf("<label ") >= 0 && content.ToLower().IndexOf("@*") < 0)
               {
                  if (content.ToLower().IndexOf("for=") < 0 && content.ToLower().IndexOf("for =") < 0)
                  {
                     filedata.LabelCount++;

                     if (string.IsNullOrEmpty(filedata.LabelLineDesc) == false)
                     {
                        filedata.LabelLineDesc += "  ";
                     }

                     filedata.LabelLineDesc += string.Format("{0}", line);
                  }


               }


               if (content.ToLower().IndexOf("<a ") >= 0)
               {
                  if ((content.ToLower().IndexOf("href=") < 0 && content.ToLower().IndexOf("href =") < 0
                     && content.ToLower().IndexOf("#=href#") <= 0 && content.ToLower().IndexOf("#= href#") <= 0)
                     || (content.ToLower().IndexOf("href='#'") > 0 || content.ToLower().IndexOf("href=\"#\"") > 0 || content.ToLower().IndexOf("href =\"#\"") > 0 || content.ToLower().IndexOf("href='\\\\#'") > 0))
                  {
                     filedata.ACount++;

                     if (string.IsNullOrEmpty(filedata.ALineDesc) == false)
                     {
                        filedata.ALineDesc += "  ";
                     }

                     filedata.ALineDesc += string.Format("{0}", line);
                  }

                  if ((content.ToLower().IndexOf("onmouseover") > 0 && content.ToLower().IndexOf("onfocus") < 0)
                        || (content.ToLower().IndexOf("onmouseover") < 0 && content.ToLower().IndexOf("onfocus") > 0)
                        || (content.ToLower().IndexOf("onmouseout") < 0 && content.ToLower().IndexOf("onblur") > 0)
                        || (content.ToLower().IndexOf("onmouseout") > 0 && content.ToLower().IndexOf("onblur") < 0)
                        )
                  {
                     filedata.ACount++;

                     if (string.IsNullOrEmpty(filedata.ALineDesc) == false)
                     {
                        filedata.ALineDesc += "  ";
                     }

                     filedata.ALineDesc += string.Format("{0}", line);
                  }




               }

               if (content.ToLower().IndexOf("<img ") >= 0)
               {
                  if (content.ToLower().IndexOf("alt=") < 0)
                  {
                     filedata.IMGCount++;

                     if (string.IsNullOrEmpty(filedata.IMGLineDesc) == false)
                     {
                        filedata.IMGLineDesc += "  ";
                     }

                     filedata.IMGLineDesc += string.Format("{0}", line);
                  }
               }


               if (content.ToLower().IndexOf("<div ") >= 0)
               {
                  int index = content.ToLower().IndexOf("<div ");
                  var substr = content.Substring(index);

                  bool jump = false;
                  if (substr.IndexOf(">") > 0)
                  {
                     substr = substr.Substring(0, substr.IndexOf(">"));
                  }
                  else
                  {
                     int j = i + 1;


                     for (; j < lines.Length; j++)
                     {
                        if (lines[j].ToLower().IndexOf(">") >= 0)
                        {
                           jump = true;
                           break;
                        }

                        substr += lines[j].Trim();


                     }

                     substr += lines[j].Substring(0, lines[j].ToLower().IndexOf(">")).Trim();

                  }

                  if (substr.ToLower().IndexOf("onclick") > 0 || substr.ToLower().IndexOf("dblclick") > 0 || substr.ToLower().IndexOf("mouseover") > 0)
                  {
                     filedata.DIVCount++;

                     if (string.IsNullOrEmpty(filedata.DIVLineDesc) == false)
                     {
                        filedata.DIVLineDesc += "  ";
                     }

                     filedata.DIVLineDesc += string.Format("{0}", line);
                  }

                  if (jump)
                  {
                     continue;
                  }
               }


               if (content.ToLower().IndexOf("<input ") >= 0)
               {
                  int index = content.ToLower().IndexOf("<input ");
                  var substr = content.Substring(index);

                  bool jump = false;
                  if (substr.IndexOf(">") > 0)
                  {
                     substr = substr.Substring(0, substr.IndexOf(">"));
                  }
                  else
                  {
                     int j = i + 1;


                     for (; j < lines.Length; j++)
                     {
                        if (lines[j].ToLower().IndexOf("</input>") >= 0 || lines[j].ToLower().IndexOf("/>") >= 0 || lines[j].ToLower().IndexOf(">") >= 0)
                        {
                           jump = true;
                           break;
                        }

                        substr += lines[j].Trim();


                     }

                     if(lines[j].ToLower().IndexOf("</input>") >= 0)
                     {
                        substr += lines[j].Substring(0, lines[j].ToLower().IndexOf("</input>")).Trim();
                     }
                     else if(lines[j].ToLower().IndexOf("/>") >= 0)
                     {
                        substr += lines[j].Substring(0, lines[j].ToLower().IndexOf("/>")).Trim();
                     }
                     else
                     {
                        substr += lines[j].Substring(0, lines[j].ToLower().IndexOf(">")).Trim();
                     }
                    

                     i = j;
                  }

                  if ((substr.ToLower().IndexOf(@"type=""checkbox""") >= 0 || substr.ToLower().IndexOf("checkbox") >= 0)
                 && (substr.ToLower().IndexOf(@"title=") <= 0 && substr.ToLower().IndexOf(@"title =") <= 0))
                  {


                     filedata.CheckBoxCount++;

                     if (string.IsNullOrEmpty(filedata.CheckBoxLineDesc) == false)
                     {
                        filedata.CheckBoxLineDesc += "  ";
                     }

                     filedata.CheckBoxLineDesc += string.Format("{0}", line);



                  }
                  else if (substr.ToLower().IndexOf("button") > 0  && (substr.ToLower().IndexOf("value=")<0 && substr.ToLower().IndexOf("value =") < 0))
                  {
                     if (substr.ToLower().IndexOf("aria-label") < 0)
                     {
                        filedata.ButtonCount++;

                        if (string.IsNullOrEmpty(filedata.ButtonLineDesc) == false)
                        {
                           filedata.ButtonLineDesc += "  ";
                        }

                        filedata.ButtonLineDesc += string.Format("{0}", line);
                     }
                  }

                  if (jump)
                  {
                     continue;
                  }
               }



               if (content.ToLower().IndexOf("<button ") >= 0)
               {
                  int index = content.ToLower().IndexOf("<button ");
                  var substr = content.Substring(index);


                  if (content.IndexOf(">") > 0)
                  {
                     substr = substr.Substring(0, substr.IndexOf(">")+1);

                     if (substr.Contains("/>"))
                     {
                        if (substr.ToLower().IndexOf("aria-label") < 0)
                        {
                           filedata.ButtonCount++;

                           if (string.IsNullOrEmpty(filedata.ButtonLineDesc) == false)
                           {
                              filedata.ButtonLineDesc += "  ";
                           }

                           filedata.ButtonLineDesc += string.Format("{0}", line);
                        }
                     }
                     else
                     {
                        bool jump = false;
                        var buttonInnerHTML = string.Empty;

                        if (content.ToLower().Contains("</button>"))
                        {
                           buttonInnerHTML = content.Substring(content.IndexOf(">")+1, content.ToLower().IndexOf("</button>") - content.IndexOf(">") - 1).Trim();
                        }
                        else
                        {
                           int j = i + 1;

                           

                           for (; j < lines.Length; j++)
                           {
                              if (lines[j].ToLower().IndexOf("</button>") >= 0)
                              {
                                 jump = true;
                                 break;
                              }

                              buttonInnerHTML += lines[j].Trim();


                           }

                           buttonInnerHTML += lines[j].Substring(0, lines[j].ToLower().IndexOf("</button>")).Trim();

                           i = j;
                        }

                        if (buttonInnerHTML.Length < 0)
                        {
                           filedata.ButtonCount++;

                           if (string.IsNullOrEmpty(filedata.ButtonLineDesc) == false)
                           {
                              filedata.ButtonLineDesc += "  ";
                           }

                           filedata.ButtonLineDesc += string.Format("{0}", line);
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
                                 filedata.ButtonCount++;

                                 if (string.IsNullOrEmpty(filedata.ButtonLineDesc) == false)
                                 {
                                    filedata.ButtonLineDesc += "  ";
                                 }

                                 filedata.ButtonLineDesc += string.Format("{0}", line);
                              }
                           }
                        }

                        if (jump)
                        {
                           continue;
                        }
                           

                          


                     }
                    
                   
                  }


               }


            }



      


            filelist.Add(filedata);


         }


         return filelist;



         



      }




      private void CheckLable()
      {

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
