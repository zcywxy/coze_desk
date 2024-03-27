using CefSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Coze
{
    public class CustomContextMenuHandler : IContextMenuHandler
    {
        private const int RefreshPageCommandId = 26502; // 自定义命令，使用唯一的ID
        private const int RefreshLayOutCommandId = 26503; // 自定义命令，使用唯一的ID
        private const int CopyCommandId = 26504; // 自定义命令，使用唯一的ID
        private const int CopyUrlCommandId = 26505; // 自定义命令，使用唯一的ID

        public void OnBeforeContextMenu(IWebBrowser browserControl, IBrowser browser, IFrame frame, IContextMenuParams parameters, IMenuModel model)
        {
            // 清除默认菜单
            model.Clear();

            // 如果可以复制文本，则添加复制菜单项
            if (!string.IsNullOrWhiteSpace(parameters.SelectionText))
            {
                model.AddItem((CefMenuCommand)CopyCommandId, "复制");
            }
            // 添加刷新页面菜单项
            model.AddItem((CefMenuCommand)RefreshPageCommandId, "刷新页面");
            model.AddItem((CefMenuCommand)RefreshLayOutCommandId, "修改布局");
            model.AddItem((CefMenuCommand)CopyUrlCommandId, "复制当前页url");

            // 其他菜单项...
        }

        public bool OnContextMenuCommand(IWebBrowser browserControl, IBrowser browser, IFrame frame, IContextMenuParams parameters, CefMenuCommand commandId, CefEventFlags eventFlags)
        {
            switch ((int)commandId)
            {
                case RefreshPageCommandId:
                    // 执行刷新页面操作
                    browser.Reload();
                    return true;
                case RefreshLayOutCommandId:
                    RefreshLayOut(browser);
                    return true;
                case CopyCommandId:
                    Copy(frame);
                    return true;
                case CopyUrlCommandId:
                    CopyUrl(frame);
                    return true;
            }
            return false;
        }

        public void Copy(IFrame frame)
        {
            frame.Copy();
        }


        public void CopyUrl(IFrame frame)
        {
            string url=frame.Url;
            Clipboard.SetText(url);
            MessageBox.Show("复制成功");
        }


        private static void RefreshLayOut(IBrowser browser)
        {
            string script = @"setInterval(function() {
    let element = document.querySelector('.sidesheet-container');
    if(element) {
		element.style.gridTemplateColumns='0fr 6fr 20fr';
        element.style.marginLeft = '200px'; // 设置左边距为20像素
        element.style.marginRight = '200px'; // 设置右边距为20像素
        clearInterval(element);  // 元素找到后清除定期检查
    }
    }, 1000);  // 每1000毫秒（即1秒）检查一次";
            browser.EvaluateScriptAsync(script);
        }

        public void OnContextMenuDismissed(IWebBrowser browserControl, IBrowser browser, IFrame frame)
        {
            // 当上下文菜单被关闭时触发
        }

        public bool RunContextMenu(IWebBrowser browserControl, IBrowser browser, IFrame frame, IContextMenuParams parameters, IMenuModel model, IRunContextMenuCallback callback)
        {
            // 返回false以显示上下文菜单
            return false;
        }
    }

}
