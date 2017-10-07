namespace Seo.WindowPages
{
    public interface PageNavigation
    {
        /// <summary>
        /// 当导航到该页面时执行
        /// </summary>
        /// <returns>是否允许导航到此页</returns>
        bool NavigationIn();
        /// <summary>
        /// 当导航出该页面时执行
        /// </summary>
        /// <returns>是否允许导航出此页</returns>
        bool NavigationOut();
    }
}
