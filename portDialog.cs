using System.IO.Ports;
using System.Windows.Forms;

namespace UCNL_FWUpdate
{
    public partial class portDialog : Form
    {
        #region Properties

        public string Title
        {
            get { return this.Text; }
            set { this.Text = value; }
        }

        public string PortName
        {
            get
            {
                if (portNameCombo.SelectedIndex >= 0)
                    return portNameCombo.SelectedItem.ToString();
                else
                    return string.Empty;
            }
            set
            {
                int index = portNameCombo.Items.IndexOf(value);
                if (index >= 0)
                    portNameCombo.SelectedIndex = index;
            }
        }

        #endregion
        
        #region Constructor

        public portDialog()
        {
            InitializeComponent();

            portNameCombo.Items.Clear();
            portNameCombo.Items.AddRange(SerialPort.GetPortNames());
            if (portNameCombo.Items.Count > 0)
                portNameCombo.SelectedIndex = 0;
        }

        #endregion

        #region Methods


        #endregion
    }
}
