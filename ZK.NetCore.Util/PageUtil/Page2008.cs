

namespace ZK.NetCore.Util.Page
{
    public class Page2008: IPage
    {
        public string Page(string strSQL, int iPageIndex, int iPageSize, string strOrderBy)
        {

            string strSQLPage = string.Format(@" SELECT * FROM(
               SELECT ROW_NUMBER() OVER (ORDER BY {0}) AS ROW,* FROM(
                 {1}
                ) TT
                ) TTT WHERE (TTT.ROW between {2} and {3}) ", strOrderBy, strSQL, (iPageSize * iPageIndex) + 1, iPageSize * iPageIndex + iPageSize);

            return strSQLPage;
        }
    }
}
