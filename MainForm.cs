using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using UCNLDrivers;
using UCNLNMEA;
using UCNLUI.Dialogs;

namespace UCNL_FW_Update
{
    public partial class MainForm : Form
    {
        #region Properties

        byte[] fw_bytes;
        byte[] fw_tableOffsets;
        int fw_bytes_rPos = 0;
        int packetSize = 256;
        uint packetCrc = 0;
        byte[] packet;
        int packetIdx = 0;
        int totalPackets = 0;

        PrecisionTimer timer;
        NMEASerialPort nmeaPort;

        string PortName = string.Empty;

        BaudRate InitialBaudrate = BaudRate.baudRate9600;

        bool IsRedNAVRedNODE
        {
            get
            {
                return isRedNAVRedNODE.Checked;
            }
        }
        
        bool isExtraPauseBeforeFWUPDATE = false;

        int repeats = 0;
        int maxRepeats = 5;

        UCNL_FW_ACTION action = UCNL_FW_ACTION.IDLE;

        CRC crc = new CRC();
        

        #endregion

        #region Constructor

        public MainForm()
        {
            InitializeComponent();

            timer = new PrecisionTimer();
            timer.Period = 6000;
            timer.Tick += new EventHandler(timer_Tick);

            initialPortSpeedCbx.SelectedIndex = 0;
            portNameCbx.Items.AddRange(SerialPort.GetPortNames());

            #region NMEA

            NMEAParser.AddManufacturerToProprietarySentencesBase(ManufacturerCodes.UCN);
            NMEAParser.AddProprietarySentenceDescription(ManufacturerCodes.UCN, "L", "x,x,c--c");

            #endregion            

            if (portNameCbx.Items.Count > 0)
            {
                portNameCbx.SelectedIndex = 0;
                SetCtrlState(true);
            }
            else
            {
                SetCtrlState(false);
            }
            
        }

        #endregion        

        #region Methods

        private void SetCtrlState(bool state)
        {
            portNameCbx.Enabled = state;
            initialPortSpeedCbx.Enabled = state;
            isRedNAVRedNODE.Enabled = state;
            isEmptyDeviceChb.Enabled = state;
            firmwarePathTxb.Enabled = state;
            loadBtn.Enabled = state;
            copyToClipboardLobBoxBtn.Enabled = state;
            exportLogBtn.Enabled = state;
        }

        private void ProcessException(Exception ex, bool isMsgBox)
        {
            InvokeWriteLogString(string.Format("Error: {0}\r\n", ex.Message));
            if (isMsgBox)
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }        

        private void InvokeWriteLogString(string text)
        {
            if (LogBox.InvokeRequired)
                LogBox.Invoke((MethodInvoker)delegate { LogBox.AppendText(text); });
            else
                LogBox.AppendText(text);
        }

        private void InvokeWriteStateLbl(string text)
        {
            if (mainStatusStrip.InvokeRequired)
                mainStatusStrip.Invoke((MethodInvoker)delegate { stateLbl.Text = text; });
            else
                stateLbl.Text = text;
        }

        private void InvokeSwitchProgressBarMode(ProgressBarStyle newStyle)
        {
            if (mainStatusStrip.InvokeRequired)
                mainStatusStrip.Invoke((MethodInvoker)delegate { progressBar.Style = newStyle; });
            else
                progressBar.Style = newStyle;
        }

        private void InvokeSetProgressBarValue(int newValue)
        {
            if (mainStatusStrip.InvokeRequired)
                mainStatusStrip.Invoke((MethodInvoker)delegate { progressBar.Value = newValue; });
            else
                progressBar.Value = newValue;

        }

        private void InvokeCtrlEnabled(Control ctrl, bool state)
        {
            if (ctrl.InvokeRequired)
                ctrl.Invoke((MethodInvoker)delegate { ctrl.Enabled = state; });
            else
                ctrl.Enabled = state;

        }


        #region Actions

        private bool RequestDevInfo()
        {
            InvokeWriteStateLbl("Querying");
            InvokeWriteLogString("Querying device info...\r\n");
            InvokeSwitchProgressBarMode(ProgressBarStyle.Marquee);
            bool result = false;
            var cmdStr = NMEAParser.BuildProprietarySentence(ManufacturerCodes.UCN, "L", new object[] { (int)UCNL_SRV_CMD.DEV_INFO_GET, 0, "" });
            try
            {
                nmeaPort.SendData(cmdStr);
                result = true;
                timer.Start();
                InvokeWriteLogString(string.Format(">> {0}", cmdStr));
            }
            catch (Exception ex)
            {
                ProcessException(ex, false);
            }

            return result;
        }

        private bool RequestFWUpdate(bool isMedium)
        {
            InvokeWriteStateLbl("Querying");
            InvokeSwitchProgressBarMode(ProgressBarStyle.Marquee);
            InvokeWriteLogString("Querying firmware update...\r\n");
            bool result = false;
            var cmdStr = NMEAParser.BuildProprietarySentence(ManufacturerCodes.UCN, "L", new object[] { (int)UCNL_SRV_CMD.FW_UPDATE_INVOKE, Convert.ToInt32(isMedium), "" });
            try
            {
                nmeaPort.SendData(cmdStr);
                timer.Start();
                InvokeWriteLogString(string.Format(">> {0}", cmdStr));
                result = true;
            }
            catch (Exception ex)
            {
                ProcessException(ex, false);
            }

            return result;                                   
        }

        private bool RequestTransferSize()
        {
            InvokeWriteStateLbl("Querying");
            InvokeSwitchProgressBarMode(ProgressBarStyle.Marquee);
            InvokeWriteLogString("Preparing...\r\n");
            bool result = false;

            

            var sizeBytes = BitConverter.GetBytes(fw_bytes.Length);
            byte[] bytes = new byte[5];
            bytes[0] = 0x31;
            bytes[1] = sizeBytes[0];
            bytes[2] = sizeBytes[1];
            bytes[3] = sizeBytes[2];
            bytes[4] = sizeBytes[3];            
            


            try
            {                
                nmeaPort.SendRaw(bytes);

                Thread.Sleep(500);
                result = true;                
                timer.Start();
            }
            catch (Exception ex)
            {
                ProcessException(ex, false);
            }

            return result;     
        }

        private bool ReopenPort()
        {
            bool result = false;
            InvokeWriteStateLbl("Reopening port");
            InvokeSwitchProgressBarMode(ProgressBarStyle.Marquee);
            InvokeWriteLogString("Reopening port...\r\n");
            try
            {
                nmeaPort.Close();
                nmeaPort.PortBaudRate = BaudRate.baudRate115200;
                nmeaPort.IsRawModeOnly = true;
                Thread.Sleep(1000);
                nmeaPort.Open();
                Thread.Sleep(2000);

                action = UCNL_FW_ACTION.FW_UPDATE_INIT;
                result = RequestTransferSize();
            }
            catch (Exception ex)
            {
                ProcessException(ex, false);
            }

            return result;
        }

        private bool RequestTransferBlock()
        {
            InvokeWriteStateLbl("Updating");
            InvokeWriteLogString(string.Format("ME: Sending data packet #{0}/{1}...\r\n", packetIdx, totalPackets));
            bool result = false;

            try
            {
                nmeaPort.SendRaw(packet);
                result = true;
                timer.Start();
            }
            catch (Exception ex)
            {
                ProcessException(ex, false);
            }

            return result;     
        }

        private bool RequestTransferLastBlock(byte[] offsets)
        {
            InvokeWriteStateLbl("Updating");
            InvokeWriteLogString(string.Format("ME: Sending last data packet #{0}/{1}...\r\n", packetIdx, totalPackets));
            bool result = false;

            try
            {
                nmeaPort.SendRaw(packet);
                Thread.Sleep(500);
                //nmeaPort.SendRaw(offsets);
                nmeaPort.SendRaw(new byte[] { 1, 2, 3, 4 });
                nmeaPort.SendRaw(new byte[] { 5, 6, 7, 8 });
                result = true;
                timer.Start();
            }
            catch (Exception ex)
            {
                ProcessException(ex, false);
            }

            return result;
        }

        private void UpdateFinished(string description)
        {
            action = UCNL_FW_ACTION.IDLE;
            InvokeWriteStateLbl("Idle");
            InvokeSwitchProgressBarMode(ProgressBarStyle.Continuous);
            InvokeSetProgressBarValue(0);
                
            timer.Stop();

            InvokeWriteLogString(string.Format("Firmware update finished: {0}\r\n", description));

            if (this.InvokeRequired)
                this.Invoke((MethodInvoker)delegate { SetCtrlState(true); });
            else
                SetCtrlState(true);

            InvokeCtrlEnabled(startUploadingBtn, true);
        }


        #endregion

        #endregion

        #region Handlers

        private void loadBtn_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog oDialog = new OpenFileDialog())
            {
                oDialog.Title = "Select a UC&NL firmware file....";
                oDialog.Filter = "UC&NL firmware (*.ucnlfw)|*.ucnlfw";
                oDialog.CheckFileExists = true;
                oDialog.CheckPathExists = true;

                if (oDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    try
                    {
                        var t_bytes = File.ReadAllBytes(oDialog.FileName);

                        if (t_bytes.Length > 4)
                        {
                            fw_tableOffsets = new byte[4];
                            Array.Copy(t_bytes, t_bytes.Length - 4, fw_tableOffsets, 0, 4);
                            fw_bytes = new byte[t_bytes.Length - 4];
                            Array.Copy(t_bytes, fw_bytes, fw_bytes.Length);

                            totalPackets = fw_bytes.Length / packetSize;
                        
                            InvokeWriteLogString(string.Format("Loaded file: {0}\r\n", Path.GetFileName(oDialog.FileName)));

                            firmwarePathTxb.Text = oDialog.FileName;
                            progressBar.Minimum = 0;
                            progressBar.Maximum = totalPackets;
                            progressBar.Value = 0;
                            startUploadingBtn.Enabled = true;                            
                        }
                        else
                        {
                            MessageBox.Show("File too small", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        ProcessException(ex, true);
                    }
                }
            }
        }

        private void infoBtn_Click(object sender, EventArgs e)
        {
            using (AboutBox aDialog = new AboutBox())
            {
                aDialog.ApplyAssembly(Assembly.GetExecutingAssembly());
                aDialog.Weblink = "http://www.unavlab.com/";
                aDialog.ShowDialog();
            }
        }

        private void startUploadingBtn_Click(object sender, EventArgs e)
        {
            SetCtrlState(false);
            startUploadingBtn.Enabled = false;

            if ((nmeaPort != null) && (nmeaPort.IsOpen))
            {
                try
                {
                    nmeaPort.Close();
                }
                catch (Exception ex)
                {
                    ProcessException(ex, false);
                }
            }

            nmeaPort = new NMEASerialPort(new SerialPortSettings(PortName, InitialBaudrate, Parity.None, DataBits.dataBits8, StopBits.One, Handshake.None));
            nmeaPort.NewNMEAMessage += new EventHandler<NewNMEAMessageEventArgs>(nmeaPort_NewMessage);
            nmeaPort.PortError += new EventHandler<SerialErrorReceivedEventArgs>(nmeaPort_PortError);
            nmeaPort.RawDataReceived += new EventHandler<RawDataReceivedEventArgs>(nmeaPort_RawData);
            
            try
            {
                nmeaPort.Open();
            }
            catch (Exception ex)
            {
                ProcessException(ex, true);
                SetCtrlState(true);
                startUploadingBtn.Enabled = true;
            }

            if (!startUploadingBtn.Enabled)
            {
                packetIdx = 0;
                repeats = 0;                

                if (isEmptyDeviceChb.Checked)
                {
                    action = UCNL_FW_ACTION.FW_UPDATE_INIT;
                    nmeaPort.IsRawModeOnly = true;
                    RequestTransferSize();
                }
                else
                {
                    action = UCNL_FW_ACTION.REQUEST_DEVICE_INFO;
                    nmeaPort.IsRawModeOnly = false;
                    RequestDevInfo();
                }
            }
        }

        private void copyToClipboardLobBoxBtn_Click(object sender, EventArgs e)
        {
            LogBox.Copy();
        }

        private void exportLogBtn_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sDialog = new SaveFileDialog())
            {
                sDialog.Title = "Select a file to save log...";
                sDialog.DefaultExt = "txt";
                sDialog.Filter = "Plain text (*.txt)|*.txt";

                if (sDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    try
                    {
                        File.WriteAllText(sDialog.FileName, LogBox.Text);
                    }
                    catch (Exception ex)
                    {
                        ProcessException(ex, true);
                    }
                }
            }
        }

        private void LogBox_TextChanged(object sender, EventArgs e)
        {
            LogBox.ScrollToCaret();
        }

        private void portNameCbx_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (portNameCbx.SelectedItem != null)
                PortName = portNameCbx.SelectedItem.ToString();
            else
                PortName = string.Empty;
        }

        private void initialPortSpeedCbx_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitialBaudrate = (BaudRate)Enum.Parse(typeof(BaudRate), initialPortSpeedCbx.SelectedItem.ToString());
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (repeats++ < maxRepeats)
            {
                switch (action)
                {
                    case UCNL_FW_ACTION.REQUEST_DEVICE_INFO:
                    {
                        RequestDevInfo();
                        break;
                    }
                    case UCNL_FW_ACTION.INVOKE_FW_REQ:
                    {
                        RequestFWUpdate(IsRedNAVRedNODE);
                        break;
                    }
                    case UCNL_FW_ACTION.PORT_REOPENING:
                    {
                        ReopenPort();
                        break;
                    }
                    case UCNL_FW_ACTION.FW_UPDATE_INIT:
                    {
                        RequestTransferSize();
                        break;
                    }
                    case UCNL_FW_ACTION.FW_BLOCK:
                    {
                        RequestTransferBlock();
                        break;
                    }
                }                
            }
            else
            {
                UpdateFinished("FAIL: device timeout");
            }
        }

        private void isMBoxBeforeUpdateCxb_CheckedChanged(object sender, EventArgs e)
        {
            isExtraPauseBeforeFWUPDATE = isMBoxBeforeUpdateCxb.Checked;
        }
       

        private void nmeaPort_NewMessage(object sender, NewNMEAMessageEventArgs e)
        {
            NMEAProprietarySentence nmeaMsg = new NMEAProprietarySentence();
            bool isParsed = false;

            InvokeWriteLogString(string.Format("<< {0}", e.Message));

            #region try parse

            try
            {
                var tempMsg = NMEAParser.Parse(e.Message);

                if (tempMsg is NMEAProprietarySentence)
                {
                    nmeaMsg = (tempMsg as NMEAProprietarySentence);
                    isParsed = true;
                }
                else
                {
                    InvokeWriteLogString(string.Format("Skipping sentence {0}", (tempMsg as NMEAStandartSentence).SentenceID));
                }
            }
            catch (Exception ex)
            {
                ProcessException(ex, false);
            }

            #endregion

            if (isParsed)
            {
                timer.Stop();
                if (nmeaMsg.Manufacturer == ManufacturerCodes.UCN)
                {
                    if (nmeaMsg.SentenceIDString == "L")
                    {
                        repeats = 0;
                        UCNL_SRV_CMD cmdID = (UCNL_SRV_CMD)(int)nmeaMsg.parameters[0];

                        switch (cmdID)
                        {
                            case UCNL_SRV_CMD.DEV_INFO_VAL:
                            {
                                // $PUCNL,x,x,c--c 
                                InvokeWriteLogString(Utils.ParseDevInfoStr((string)nmeaMsg.parameters[2]));
                                action = UCNL_FW_ACTION.INVOKE_FW_REQ;

                                if (isExtraPauseBeforeFWUPDATE)
                                    MessageBox.Show("Turn off S_MODE in your device and press OK", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);


                                RequestFWUpdate(IsRedNAVRedNODE);                                
                                break;
                            }
                            case UCNL_SRV_CMD.ACK:
                            {
                                // $PUCNL,x,x,c--c
                                InvokeWriteLogString("DEVICE: Firmware update mode ON\r\n");
                                nmeaPort.IsRawModeOnly = true;                               

                                if (InitialBaudrate != BaudRate.baudRate115200)
                                {
                                    action = UCNL_FW_ACTION.PORT_REOPENING;
                                    ReopenPort();                                    
                                }
                                else
                                {
                                    action = UCNL_FW_ACTION.FW_UPDATE_INIT;
                                    RequestTransferSize();
                                }

                                break;
                            }
                        }
                    }
                }
                else
                {
                    InvokeWriteLogString(string.Format("Skipping sentence {0}", nmeaMsg.SentenceIDString));
                }
            }                            
        }

        private void nmeaPort_PortError(object sender, SerialErrorReceivedEventArgs e)
        {
            InvokeWriteLogString(string.Format("Error {0} in {1}\r\n", e.EventType.ToString(), PortName));
        }

        private void nmeaPort_RawData(object sender, RawDataReceivedEventArgs e)
        {
            if (nmeaPort.IsRawModeOnly)
            {
                timer.Stop();
                
                int result = e.Data[0];

                if (result == 49)
                {
                    repeats = 0;
                    List<byte> tpacket = new List<byte>();

                    for (int n = 0; (n < packetSize) && (fw_bytes_rPos < fw_bytes.Length); n++)
                        tpacket.Add(fw_bytes[fw_bytes_rPos++]);

                    packetCrc = crc.CRC32(0, tpacket.ToArray());
                    tpacket.AddRange(BitConverter.GetBytes(packetCrc));
                                        
                    action = UCNL_FW_ACTION.FW_BLOCK;
                    InvokeWriteLogString("DEVICE: Next packet\r\n");

                    if (fw_bytes_rPos >= fw_bytes.Length)
                        tpacket.AddRange(fw_tableOffsets);

                    packet = tpacket.ToArray();
                    RequestTransferBlock();          

                    if (packetIdx == 0)
                    {
                        InvokeSwitchProgressBarMode(ProgressBarStyle.Continuous);
                    }

                    InvokeSetProgressBarValue(packetIdx);

                    if (packetIdx < totalPackets)
                        packetIdx++;
                }
                else if (result == 50)
                {
                    action = UCNL_FW_ACTION.FW_BLOCK;
                    RequestTransferBlock();
                    InvokeWriteLogString("DEVICE: Bad packet\r\n");
                }
                else if (result == 51)
                {
                    UpdateFinished("SUCCESS");                        
                }
                else if (result == 52)
                {
                    // 4
                    UpdateFinished("FAIL: device timeout (4)");
                }
                else if (result == 53)
                {
                    // 5
                    UpdateFinished("FAIL: Firmware integrity error. Try again with \"Device is empty option\"");
                }
                else
                {
                    UpdateFinished(string.Format("FAIL: ({0})", result));
                }
            }
        }

        #endregion                                                        
    }
}
