using System.Windows.Forms;
using Tools.Management.Properties;
using SourceCode.Workflow.Management;

namespace Tools.Management.Factories
{
    internal static class TreeNodeCreator
    {
        public static TreeNode Create(ProcessFolder processFolder)
        {
            var treeNode = new TreeNode();

            treeNode.Tag = processFolder;
            treeNode.Text = processFolder.FolderName;
            treeNode.Name = processFolder.FolderName;
            treeNode.ImageKey = Constants.ImageName.ProcessFolder;
            treeNode.SelectedImageKey = Constants.ImageName.ProcessFolder;

            return treeNode;
        }

        public static TreeNode Create(ProcessSet procSet)
        {
            var treeNode = new TreeNode();

            treeNode.Tag = procSet;
            treeNode.Text = $"{procSet.Name} [{procSet.ProcSetID}]";
            treeNode.Name = procSet.FullName;
            treeNode.ImageKey = Constants.ImageName.ProcSet;
            treeNode.SelectedImageKey = Constants.ImageName.ProcSet;

            return treeNode;
        }

        public static TreeNode Create(Process process)
        {
            var treeNode = new TreeNode();

            var processText = process.VersionNumber.ToString();
            if (process.DefaultVersion)
            {
                processText = $"{processText} ({ Resources.Default})";
            }

            treeNode.Tag = process;
            treeNode.Text = processText;
            treeNode.Name = process.VersionNumber.ToString();
            treeNode.ImageKey = Constants.ImageName.Process;
            treeNode.SelectedImageKey = Constants.ImageName.Process;

            return treeNode;
        }

        public static TreeNode Create(ProcessInstance processInstance)
        {
            var treeNode = new TreeNode();

            treeNode.Tag = processInstance;
            treeNode.Text = $"[{processInstance.ID}] {processInstance.Folio}";
            treeNode.Name = processInstance.ID.ToString();
            treeNode.ImageKey = Constants.ImageName.ProcessInstance;
            treeNode.SelectedImageKey = Constants.ImageName.ProcessInstance;

            return treeNode;
        }

        public static TreeNode CreateVersionsFolder()
        {
            var versionsTreeNode = new TreeNode();

            versionsTreeNode.Text = Resources.Versions;
            versionsTreeNode.Name = Resources.Versions;

            return versionsTreeNode;
        }
    }
}