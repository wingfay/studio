using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Spire.Doc;
using Spire.Doc.Documents;
using Spire.Doc.Fields;

namespace SQL2Word
{
   //生成Table 模版文档
   public partial class FormSQL : Form
   {
      string strConnectionString = @"server=IP;database=DataBaseName;uid=sa;pwd=sa";
      public FormSQL()
      {
         InitializeComponent();
      }


      private void button1_Click(object sender, EventArgs e)
      {
         button1.Enabled = false;

         List<Field> fields = new List<Field>();

         string TableName = string.Empty;
         //处理OleDbConnection
         
         using (SqlConnection conn = new SqlConnection(strConnectionString))
         {
            conn.Open();
            //执行存储过程
            SqlCommand cmd = new SqlCommand(@"SELECT Name, CONVERT(int,RIGHT(Name,LEN(Name)-CHARINDEX('_t',Name)-1)) as s 
   FROM SysObjects
   Where XType = 'U'  AND CHARINDEX('_t', Name) > 0
   AND ISNUMERIC(RIGHT(Name, LEN(Name) - CHARINDEX('_t', Name) - 1)) = 1   ORDER BY s ", conn);
            cmd.CommandType = CommandType.Text;
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
               TableName = dr["Name"].ToString();
               textBox1.AppendText(TableName);
               textBox1.AppendText(Environment.NewLine);

                fields = new List<Field>();

               foreach (var field in GetFileds(strConnectionString, TableName))
               {



                  textBox1.AppendText($"{field.Name}:{field.Type}");
                  textBox1.AppendText(Environment.NewLine);

                  fields.Add(field);
               }

               

               break;
            }
         }

         button1.Enabled = true;

         Create(TableName, fields);
      }

      public struct Field
      {
         public string Name;
         public string Type;
         public string NullDesc;
         public string PKDesc;
      }

      public IEnumerable<Field> GetFileds(string connectionString, string tableName)
      {
         using (SqlConnection _Connection = new SqlConnection(connectionString))
         {
            _Connection.Open();

            string strSQL = $@"   SELECT  CASE WHEN col.colorder = 1 THEN obj.name
                  ELSE ''
             END AS 表名,
        col.colorder AS 序号 ,
        col.name AS 列名 ,
        ISNULL(ep.[value], '') AS 列说明 ,
        t.name AS 数据类型 ,
        cast(col.length/2 as varchar(10)) AS 长度 ,
        ISNULL(COLUMNPROPERTY(col.id, col.name, 'Scale'), 0) AS 小数位数 ,
        CASE WHEN COLUMNPROPERTY(col.id, col.name, 'IsIdentity') = 1 THEN '1'
             ELSE ''
        END AS 标识 ,
        CASE WHEN EXISTS ( SELECT   1
                           FROM     dbo.sysindexes si
                                    INNER JOIN dbo.sysindexkeys sik ON si.id = sik.id
                                                              AND si.indid = sik.indid
                                    INNER JOIN dbo.syscolumns sc ON sc.id = sik.id
                                                              AND sc.colid = sik.colid
                                    INNER JOIN dbo.sysobjects so ON so.name = si.name
                                                              AND so.xtype = 'PK'
                           WHERE    sc.id = col.id
                                    AND sc.colid = col.colid ) THEN '1'
             ELSE ''
        END AS 主键 ,
        CASE WHEN col.isnullable = 1 THEN '1'
             ELSE '0'
        END AS 允许空 ,
        ISNULL(comm.text, '') AS 默认值
FROM    dbo.syscolumns col
        LEFT  JOIN dbo.systypes t ON col.xtype = t.xusertype
        inner JOIN dbo.sysobjects obj ON col.id = obj.id
                                         AND obj.xtype = 'U'
                                         AND obj.status >= 0
        LEFT  JOIN dbo.syscomments comm ON col.cdefault = comm.id
        LEFT  JOIN sys.extended_properties ep ON col.id = ep.major_id
                                                      AND col.colid = ep.minor_id
                                                      AND ep.name = 'MS_Description'
        LEFT  JOIN sys.extended_properties epTwo ON obj.id = epTwo.major_id
                                                         AND epTwo.minor_id = 0
                                                         AND epTwo.name = 'MS_Description'
WHERE   obj.name = '{tableName}'--表名
ORDER BY col.colorder ;";
            SqlCommand cmd = new SqlCommand(strSQL, _Connection)
            {
               CommandType = CommandType.Text
            };
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
               Field field;

               field.Name = dr["列名"].ToString();
               field.Type = dr["数据类型"].ToString();

               if(field.Type.Contains("char"))
               {
                  field.Type += $"({dr["长度"].ToString()})";
               }

               if(dr["允许空"].ToString()=="1")
               {
                  field.NullDesc = "空";
               }
               else
               {
                  field.NullDesc = "非空";
               }

               if (dr["主键"].ToString() == "1")
               {
                  field.PKDesc = "PK";
               }
               else
               {
                  field.PKDesc = string.Empty;
               }

               yield return field;
            }
         }
      }

      private void UpdateCell(TableCell tableCell, string Message,bool isHeader=false,float fontSize = 9)
      {

         Paragraph para = tableCell.AddParagraph();
         
         if (isHeader)
         {
            tableCell.CellFormat.BackColor = Color.FromArgb(217, 217, 217);
            para.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Center;
         }
         TextRange TR = para.AppendText(Message);
         //Font name
         TR.CharacterFormat.FontName = "Microsoft YaHei";

         //Font size
         TR.CharacterFormat.FontSize = fontSize;

         ////Underline
         //TR.CharacterFormat.UnderlineStyle = UnderlineStyle.DotDot;

         ////Change text color
         //TR.CharacterFormat.TextColor = Color.Blue;
      }


      private void Create(string TableName,List<Field> fields,bool IsUseTableName2WordName=false)
      {
         Document document = new Document();
         Section section = document.AddSection();


         //创建表
         Table table = section.AddTable(true);


         table.ResetCells(2, 5);

         table.ApplyHorizontalMerge(0, 1, 4);

         TableRow rowHeader = table.Rows[0];
         rowHeader.IsHeader = true;

         UpdateCell(rowHeader.Cells[0], "表名", true);

         UpdateCell(rowHeader.Cells[1], TableName,fontSize:10);


         TableRow row = table.Rows[1];
         row.IsHeader = true;

         UpdateCell(row.Cells[0], "列名", true);
         UpdateCell(row.Cells[1], "数据类型", true);
         UpdateCell(row.Cells[2], "空/非空", true);
         UpdateCell(row.Cells[3], "约束条件", true);
         UpdateCell(row.Cells[4], "说明", true);



         foreach (var field in fields)
         {
            TableRow row1 = new TableRow(document);
            UpdateCell(row1.AddCell(true), field.Name);
            UpdateCell(row1.AddCell(true), field.Type);
            UpdateCell(row1.AddCell(true), field.NullDesc);
            UpdateCell(row1.AddCell(true), field.PKDesc);
            UpdateCell(row1.AddCell(true), string.Empty);
            table.Rows.Add(row1);

         }

         TableRow rowBottom = new TableRow(document);
         UpdateCell(rowBottom.AddCell(true), "补充说明",true);

         UpdateCell(rowBottom.AddCell(true), string.Empty);
         UpdateCell(rowBottom.AddCell(true), string.Empty);
         UpdateCell(rowBottom.AddCell(true), string.Empty);
         UpdateCell(rowBottom.AddCell(true), string.Empty);

         table.Rows.Add(rowBottom);

         table.ApplyHorizontalMerge(table.Rows.Count-1, 1, 4);


         if(IsUseTableName2WordName)
         {
            document.SaveToFile($"{TableName}.docx");

            System.Diagnostics.Process.Start($"{TableName}.docx");
         }
         else
         {
            document.SaveToFile("WordTable.docx");

            System.Diagnostics.Process.Start("WordTable.docx");
         }



      }

      private void btnTable2Word_Click(object sender, EventArgs e)
      {
         if(txtTableName.Text.Length<=0)
         {
            return;
         }

         btnTable2Word.Enabled = false;

         var fields = new List<Field>();

         foreach (var field in GetFileds(strConnectionString, txtTableName.Text.Trim()))
         {



            textBox1.AppendText($"{field.Name}:{field.Type}");
            textBox1.AppendText(Environment.NewLine);

            fields.Add(field);
         }

         btnTable2Word.Enabled = true;

         Create(txtTableName.Text.Trim(), fields,true);

      }
   }
}
