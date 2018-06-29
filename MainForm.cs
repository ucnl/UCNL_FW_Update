using RedNODEHost;
using System;
using System.IO;
using System.IO.Ports;
using System.Reflection;
using System.Windows.Forms;
using UCNLUI.Dialogs;

namespace UCNL_FWUpdate
{
    public partial class MainForm : Form
    {
        #region Invokers

        private void InvokeSetText(StatusStrip strip, ToolStripStatusLabel lbl, string text)
        {
            if (strip.InvokeRequired)
            {
                strip.Invoke((MethodInvoker)delegate { lbl.Text = text; });
            }
            else
            {
                lbl.Text = text;
            }
        }

        private void InvokeProgress(StatusStrip strip, ToolStripProgressBar pBar, int newValue)
        {
            if (strip.InvokeRequired)
            {
                strip.Invoke((MethodInvoker)delegate { pBar.Value = newValue; });
            }
            else
            {
                pBar.Value = newValue;
            }
        }
        
        #endregion

        #region Properties

        byte[] fwData;
        bool isfwDataLoaded = false;
        bool isPortReady = false;

        SerialPort port;
        PrecisionTimer timer;

        int fwDataSize;
        int fwDataBlocks;
        const int fwBlockSize = 256;
        uint fwTailSignature;
        int retryCnt = 0;

        int currentBlock = 0;

        #endregion               

        #region Constructor

        public MainForm()
        {
            InitializeComponent();
            port = new SerialPort("COM1", 115200, Parity.None, 8, StopBits.One);

            port.DataReceived += new SerialDataReceivedEventHandler(port_DataReceived);
            port.ErrorReceived += new SerialErrorReceivedEventHandler(port_ErrorReceived);

            timer = new PrecisionTimer();
            timer.Tick += new EventHandler(timer_Tick);
            timer.Period = 5000;
            timer.Started += new EventHandler(timer_Started);
            timer.Stopped += new EventHandler(timer_Stopped);

            CRC32x.crc32_init();
        }

        #endregion

        #region Methods

        private void CheckFWUploadBtnEnable()
        {
            updBtn.Enabled = (isfwDataLoaded && isPortReady);

            if (updBtn.Enabled)
                InvokeSetText(statusStrip, stateStatusLbl, "Ready");
            else
                InvokeSetText(statusStrip, stateStatusLbl, "Idle");
        }

        private void OnNextDataBlockRequest()
        {
            byte[] msg;

            if (currentBlock >= fwDataBlocks)
            {
                msg = BitConverter.GetBytes(fwTailSignature);
            }
            else
            {
                msg = new byte[fwBlockSize + 4];

                for (int i = 0; i < fwBlockSize; i++)
                {
                    msg[i] = fwData[currentBlock * fwBlockSize + i];
                }
                
                uint blockCRC = 0;
                blockCRC = CRC32x.crc32_stm32(blockCRC, msg, fwBlockSize);

                var bCRC = BitConverter.GetBytes(blockCRC);
                Array.Copy(bCRC, 0, msg, fwBlockSize, bCRC.Length);
                
                InvokeProgress(statusStrip, pBar, currentBlock);
                currentBlock++;
            }

            port.Write(msg, 0, msg.Length);
        }

        private void OnBadDataBlockIntegrityRequest()
        {
            byte[] msg;

            if (currentBlock >= fwDataBlocks)
            {
                msg = BitConverter.GetBytes(fwTailSignature);
            }
            else
            {
                msg = new byte[fwBlockSize + 4];

                for (int i = 0; i < fwBlockSize; i++)
                {
                    msg[i] = fwData[currentBlock * fwBlockSize + i];
                }

                uint blockCRC = 0;
                blockCRC = CRC32x.crc32_stm32(blockCRC, msg, fwBlockSize);

                var bCRC = BitConverter.GetBytes(blockCRC);
                Array.Copy(bCRC, 0, msg, fwBlockSize, bCRC.Length);
            }

            port.Write(msg, 0, msg.Length);
        }

        private void StartDataSend()
        {
            byte[] msg = new byte[5];
            var dSize = BitConverter.GetBytes(fwDataSize);
            msg[0] = 1;
            Array.Copy(dSize, 0, msg, 1, 4);

            port.Write(msg, 0, msg.Length);
            timer.Start();

            stateStatusLbl.Text = "Updating";
        }

        private void OnFWUpdateCompleted()
        {
            timer.Stop();
            currentBlock = 0;
            isPortReady = false;
            isfwDataLoaded = false;
            openBtn.Enabled = true;
            settingsBtn.Enabled = true;

            stateStatusLbl.Text = "Idle";

            CheckFWUploadBtnEnable();

            try
            {
                port.Close();
            }
            catch (Exception ex)
            {
            }

            MessageBox.Show("Firmware update completed successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information); 
        }

        private void OnFWUpdateError()
        {
            timer.Stop();
            currentBlock = 0;
            isPortReady = false;
            isfwDataLoaded = false;
            openBtn.Enabled = true;
            settingsBtn.Enabled = true;

            stateStatusLbl.Text = "Idle";

            CheckFWUploadBtnEnable();

            try
            {
                port.Close();
            }
            catch (Exception ex)
            {
            }

            MessageBox.Show("Firmware update failed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void OnFWUpdateDeviceTimeout()
        {
            timer.Stop();
            currentBlock = 0;
            isPortReady = false;
            isfwDataLoaded = false;
            openBtn.Enabled = true;
            settingsBtn.Enabled = true;

            stateStatusLbl.Text = "Idle";

            CheckFWUploadBtnEnable();

            try
            {
                port.Close();
            }
            catch (Exception ex)
            {
            }

            MessageBox.Show("Firmware update failed due to device timeout", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        #endregion

        #region UI handlers

        #region toolStrip

        private void openBtn_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog oDialog = new OpenFileDialog())
            {
                oDialog.Title = "Opening a firmware file...";
                oDialog.CheckFileExists = true;
                oDialog.CheckPathExists = true;
                oDialog.DefaultExt = "ucnlfw";
                oDialog.Filter = "UC&NL firmware files (*.ucnlfw)|*.ucnlfw";

                if (oDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    try
                    {
                        fwData = File.ReadAllBytes(oDialog.FileName);
                        isfwDataLoaded = true;

                        fwDataSize = fwData.Length - 4;
                        fwDataBlocks = fwDataSize / fwBlockSize;
                        fwTailSignature = BitConverter.ToUInt32(fwData, fwData.Length - 4);
                        currentBlock = 0;

                        CheckFWUploadBtnEnable();

                        pBar.Value = 0;
                        pBar.Maximum = fwDataBlocks;
                    }
                    catch (Exception ex)
                    {
                        isfwDataLoaded = false;
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void settingsBtn_Click(object sender, EventArgs e)
        {
            using (portDialog pDialog = new portDialog())
            {
                pDialog.Title = "Select a port";
                if (pDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    if (!string.IsNullOrEmpty(pDialog.PortName))
                    {

                        if (port.IsOpen)
                        {
                            try
                            {
                                port.Close();
                            }
                            catch (Exception ex)
                            {
                                /// TODO:
                            }
                        }

                        port.PortName = pDialog.PortName;
                        isPortReady = true;
                        CheckFWUploadBtnEnable();
                    }
                }
            }
        }

        private void infoBtn_Click(object sender, EventArgs e)
        {
            using (AboutBox aBox = new AboutBox())
            {
                aBox.Weblink = "http://www.unavlab.com/";
                aBox.ApplyAssembly(Assembly.GetExecutingAssembly());
                aBox.ShowDialog();
            }
        }

        #endregion

        #region updateBtn

        private void updBtn_Click(object sender, EventArgs e)
        {            
            try
            {
                port.Open();
                isPortReady = true;
            }
            catch (Exception ex)
            {
                isPortReady = false;
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (isPortReady)
            {
                StartDataSend();
                updBtn.Enabled = false;
                openBtn.Enabled = false;
                settingsBtn.Enabled = false;
            }
        }

        #endregion

        #endregion

        #region Handlers

        private void port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            var bytesToRead = port.BytesToRead;
            byte[] data = new byte[bytesToRead];
            port.Read(data, 0, bytesToRead);
            bool finished = false;
            for (int i = 0; (i < bytesToRead) && (!finished); i++)
            {
                if (data[i] == 0x31)
                {
                    // "1"
                    finished = true;
                    OnNextDataBlockRequest();
                }
                else if (data[i] == 0x32)
                {
                    // "2"
                    finished = true;
                    OnBadDataBlockIntegrityRequest();

                    if (++retryCnt > 3)
                    {
                        this.Invoke((MethodInvoker)delegate { OnFWUpdateDeviceTimeout(); });
                    }
                }
                else if (data[i] == 0x33)
                {
                    // "3"
                    finished = true;
                    this.Invoke((MethodInvoker)delegate { OnFWUpdateCompleted(); });                    
                }
                else if (data[i] == 0x34)
                {
                    finished = true;
                    this.Invoke((MethodInvoker)delegate { OnFWUpdateError(); });
                }

                if (finished) timer.Stop();
            }
        }

        private void port_ErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        {
            InvokeSetText(statusStrip, stateStatusLbl, e.EventType.ToString());
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            this.Invoke((MethodInvoker)delegate { OnFWUpdateDeviceTimeout(); });           
        }

        private void timer_Started(object sender, EventArgs e)
        {
            /// TODO:
        }

        private void timer_Stopped(object sender, EventArgs e)
        {
            /// TODO:
        }

        #endregion

    }
}
