

namespace ZK.NetCore.Util.Page
{
    public class Page2012:IPage
    {
        public string Page(string strSQL, int iPageIndex, int iPageSize, string strOrderBy)
        {

            string strSQLPage = string.Format(@"
               SELECT * FROM(
                 {1}
                ) TT
                 WHERE ORDER BY  {0} OFFSET {2} ROWS FETCH NEXT {3} ROWS ONLY;", strOrderBy, strSQL, (iPageSize * iPageIndex) + 1, iPageSize * iPageIndex + iPageSize);

            return strSQLPage;
        }
    }
}
