using System;
using System.Windows.Forms;

namespace SourceCode.Tools.Management
{
    public partial class PropertiesForm : Form
    {
        internal PropertiesForm(object value, string formText)
        {
            InitializeComponent();
            propertyGrid.SelectedObject = value;
            this.Text = formText;
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}