using System.Reflection;
using System.Windows.Forms;

namespace SourceCode.Tools.Management.Extensions
{
    internal static class TreeNodeExtensions
    {
        public static void NodeClick(this TreeNode treeNode, TreeView treeView)
        {
            treeNode.NodeClick(treeView, MouseButtons.Left);
        }

        public static void NodeClick(this TreeNode treeNode, TreeView treeView, MouseButtons mouseButtons)
        {
            var treeViewType = typeof(TreeView);
            var parameters = new object[1];
            parameters[0] = new TreeNodeMouseClickEventArgs(treeNode, mouseButtons, 1, 1, 1);

            var m = treeViewType.GetMethod("OnNodeMouseClick", BindingFlags.NonPublic | BindingFlags.Instance);
            m.Invoke(treeView, parameters);
        }
    }
}