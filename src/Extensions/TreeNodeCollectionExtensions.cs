using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Tools.Management.Extensions
{
    /// <summary>
    /// Extension methods for TreeNodeCollection
    /// </summary>
    internal static class TreeNodeCollectionExtensions
    {
        public static TreeNode GetNodeWithPath(this TreeNodeCollection nodeCol, string nodePath)
        {
            TreeNode currentNode = nodeCol.Cast<TreeNode>().FirstOrDefault(i => i.Text == nodePath);
            return currentNode;
        }

        public static IList<string> GetVisibleNodes(this TreeNodeCollection treeNodeCollection)
        {
            var nodeStates = new List<string>();

            if (treeNodeCollection == null)
            {
                return nodeStates;
            }

            foreach (TreeNode treeNode in treeNodeCollection)
            {
                if (treeNode.IsExpanded)
                {
                    nodeStates.Add(treeNode.FullPath);

                    if (treeNode.Nodes.Count > 0)
                    {
                        nodeStates.AddRange(treeNode.Nodes.GetVisibleNodes());
                    }
                }
            }

            return nodeStates;
        }
    }
}