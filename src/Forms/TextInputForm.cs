using System.Windows.Forms;

namespace Tools.Management.Forms
{
    public partial class TextInputForm : Form
    {
        public TextInputForm(string value)
        {
            InitializeComponent();

            TextRichTextBox.Text = value;
        }

        private void okButton_Click(object sender, System.EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}