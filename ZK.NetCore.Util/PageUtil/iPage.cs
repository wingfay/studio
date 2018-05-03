

namespace ZK.NetCore.Util.Page
{
    public interface IPage
    {
        string Page(string strSQL, int iPageIndex, int iPageSize, string strOrderBy);
    }
}
