using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Tools.Management.Extensions
{
    /// <summary>
    /// Extension methods for TreeView
    /// </summary>
    internal static class TreeViewExtensions
    {
        public static Tuple<string, List<string>> GetTreeViewTreeState(this TreeView treeView)
        {
            var treeViewState = new Tuple<string, List<string>>(treeView?.SelectedNode?.FullPath, new List<string>());

            if (treeView == null ||
                treeView.Nodes.Count == 0)
            {
                return treeViewState;
            }

            treeViewState.Item2.AddRange(treeView.Nodes.GetVisibleNodes());
            return treeViewState;
        }

        public static void LoadVisibleExpandedTreeState(this TreeView treeView, List<string> savedTreeState, string selectedNodeFullPath)
        {
            if (treeView == null ||
                savedTreeState == null ||
                savedTreeState.Count == 0)
            {
                return;
            }

            foreach (var nodeFullPath in savedTreeState)
            {
                var treeNode = treeView.GetNodeWithPath(nodeFullPath);

                if (treeNode != null)
                    treeNode.Expand();
            }

            var selectedNode = treeView.GetNodeWithPath(selectedNodeFullPath);

            if (selectedNode != null)
            {
                treeView.SelectedNode = selectedNode;
            }
        }

        private static TreeNode GetNodeWithPath(this TreeView treeView, string nodeFullPath)
        {
            if (string.IsNullOrEmpty(nodeFullPath))
            {
                return null;
            }

            string[] splitFullPath = nodeFullPath.Split(new string[] { treeView.PathSeparator }, StringSplitOptions.RemoveEmptyEntries);

            TreeNodeCollection nodeCol = treeView.Nodes;
            TreeNode treeNode = null;

            foreach (string path in splitFullPath)
            {
                TreeNode currentNode = nodeCol.GetNodeWithPath(path);
                if (currentNode == null)
                {
                    break;
                }
                treeNode = currentNode;
                nodeCol = treeNode.Nodes;

                treeNode.NodeClick(treeView);
            }

            return treeNode;
        }
    }
}