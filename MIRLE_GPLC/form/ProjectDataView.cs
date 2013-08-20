﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MIRLE_GPLC.form.marker;
using MIRLE_GPLC.Model;
using Modbus.TCP;
using System.Threading;
using System.Net.Sockets;

namespace MIRLE_GPLC.form
{
    internal partial class ProjectDataView : UserControl
    {
        private List<ProjectMarker> markers = new List<ProjectMarker>();
        private int _shownMarker = 0;
        private PLC _lastSelectedPLC;
        private Master MBmaster;

        private int ShownMarker
        {
            get { return _shownMarker; }
            set
            {
                if (value < 0)
                    value = markers.Count - 1;
                else if (value >= markers.Count)
                    value = 0;

                _shownMarker = value;
                lastSelectedPLC = null;

                labelTitle.Text = markers[_shownMarker].ProjectData.name;
                labelText.Text = markers[_shownMarker].ProjectData.addr;

                labelCounter.Text = (_shownMarker + 1).ToString() + " of " + markers.Count;

                Refresh();
            }
        }
        private PLC lastSelectedPLC
        {
            get { return _lastSelectedPLC; }
            set
            {
                /*if (client != null && client.IsConnected)
                {
                    client.Disconnect();
                    client = null;
                }*/
                if (MBmaster != null && MBmaster.connected)
                {
                    MBmaster.disconnect();
                    MBmaster = null;
                }
                _lastSelectedPLC = value;
                refreshItemList(_lastSelectedPLC);
            }
        }


        public ProjectDataView()
        {
            InitializeComponent();
        }
        public void init(List<ProjectMarker> markers)
        {
            if (markers.Count == 0)
                return;

            // Assign the list of markers
            this.markers = markers;

            // Sort the list of markers if you want to
            // Exclude this if you dont care about order
            this.markers.Sort(
                delegate(ProjectMarker m1, ProjectMarker m2)
                {
                    return m1.ProjectData.id.CompareTo(m2.ProjectData.id);
                }
            );

            // Show the first marker
            buttonBack.Enabled = buttonNext.Enabled = (markers.Count != 1);
            ShownMarker = 0;

            this.Show();
        }

        #region -- Refresh List Method --

        public override void Refresh()
        {
            base.Refresh();
            projectCreateControl1.Hide();
            dataFieldInputControl1.Hide();
            this.listView_data.Show();
            this.listView_plc.Show();
            refreshPLCList();
        }
        private void refreshPLCList()
        {
            ProjectData p = markers[_shownMarker].ProjectData;
            p.reload();

            listView_plc.Items.Clear();
            listView_data.Items.Clear();
            foreach (PLC plc in p.plcs)
            {
                ListViewItem item = new ListViewItem(plc.alias);
                item.SubItems.Add(plc.netid.ToString());
                item.SubItems.Add(plc.ip);
                item.SubItems.Add(plc.port.ToString());
                listView_plc.Items.Add(item);
            }
            refreshItemList(lastSelectedPLC);
        }
        private void refreshItemList(PLC plc)
        {
            listView_data.Items.Clear();
            if (plc == null)
            {
                return;
            }
            plc.reload();
            foreach (Record r in plc.dataFields)
            {
                ListViewItem item = new ListViewItem(r.alias);
                item.SubItems.Add("????");
                listView_data.Items.Add(item);
            }
            // modbus worker
            ThreadPool.QueueUserWorkItem(new WaitCallback(o => modbusTCPWorker(plc)));

        }

        #endregion

        #region -- Event Handler --

        private void buttonBack_Click(object sender, EventArgs e)
        {
            ShownMarker--;
        }
        private void buttonNext_Click(object sender, EventArgs e)
        {
            ShownMarker++;
        }

        private void listView_plc_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView_plc.SelectedIndices.Count > 0)
            {
                lastSelectedPLC = markers[_shownMarker].ProjectData.plcs[listView_plc.SelectedIndices[0]];
            }
        }
        private void listView_plc_DoubleClick(object sender, EventArgs e)
        {
            int index = listView_plc.SelectedIndices.Count > 0 ? listView_plc.SelectedIndices[0] : -1;
            PLCEditControl(index);
        }
        private void listView_data_DoubleClick(object sender, EventArgs e)
        {
            int index = listView_data.SelectedIndices.Count > 0 ? listView_data.SelectedIndices[0] : -1;
            DataFieldControl(index);
        }

        private void listView_plc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete && lastSelectedPLC != null)
            {
                ModelUtil.deletePLC(lastSelectedPLC.id);
                refreshPLCList();
            }
        }
        private void listView_data_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete && lastSelectedPLC != null)
            {
                if (listView_data.SelectedIndices.Count > 0)
                {
                    ModelUtil.deleteItem(lastSelectedPLC.dataFields[listView_data.SelectedIndices[0]].id);
                    refreshItemList(lastSelectedPLC);
                }
            }
        }

        #endregion

        #region -- mouse double click event for non-item area --

        private void listView_plc_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Clicks == 2)
            {
                listView_plc_DoubleClick(sender, e);
            }
        }
        private void listView_data_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Clicks == 2)
            {
                listView_data_DoubleClick(sender, e);
            }
        }
        private void listView_plc_MouseUp(object sender, MouseEventArgs e)
        {
            if(listView_plc.Visible && listView_plc.SelectedIndices.Count <= 0)
            {
                lastSelectedPLC = null;
            }
        }

        #endregion

        #region -- input control --
        
        private void PLCEditControl(int index)
        {
            ProjectData project = markers[_shownMarker].ProjectData;

            this.listView_data.Hide();
            this.listView_plc.Hide();
            if (index >= 0)
            {
                projectCreateControl1.init(project, project.plcs[index]);
            }
            else
            {
                projectCreateControl1.init(project);
            }
        }
        private void DataFieldControl(int index)
        {
            if (lastSelectedPLC == null)
            {
                return;
            }

            this.listView_data.Hide();
            this.listView_plc.Hide();
            if (index >= 0)
            {
                dataFieldInputControl1.init(lastSelectedPLC.id, lastSelectedPLC.dataFields[index]);
            }
            else
            {
                dataFieldInputControl1.init(lastSelectedPLC.id);
            }
        }

        #endregion

        #region -- Modbus TCP Worker --

        /*private void modbusTCPWorker(PLC plc)
        {
            if (plc == null)
            {
                return;
            }

            // initialize modbus TCP/IP
            ModbusClientAdpater adpater = new ModbusClientAdpater();
            // config ip and port
            TcpModbusConnectConfig config = new TcpModbusConnectConfig() { IpAddress = plc.ip, Port = plc.port };
            // config modbus connection type
            client = adpater.CreateModbusClient(EnumModbusFraming.TCP);
            // connecting message "Connecting to [IP]:[PORT]"
            string str = string.Format("Connecting to {0}:{1}", config.IpAddress, config.Port);

            // modbus TCP connect
            try
            {
                do
                {
                    // invoke ui thread to change tooltip
                    //Invoke(new DataFieldHandler(RefreshDataField), new Object[] { str });
                    SpinWait.SpinUntil(() => false, 1000);
                } while (!client.Connect(config));
            }
            catch (SocketException)
            {
                // invoke ui thread to change tooltip
                //Invoke(new DataFieldHandler(RefreshDataField), new Object[] { "Modbus TCP/IP connect fail" });
            }

            // refresh dataGrid periodically
            while (client != null && client.IsConnected)
            {
                try
                {
                    int i = 0;
                    foreach (Record r in plc.dataFields)
                    {
                        //ThreadPool.QueueUserWorkItem(new WaitCallback(o => readData((ushort)r.addr, (ushort)r.length, index)));
                        readData((ushort)r.addr, (ushort)r.length, i++);
                    }
                    // spin wait
                    SpinWait.SpinUntil(() => false, 1000);
                }
                catch (ModbusException)
                {
                }
            }
        }*/
        
        private void modbusTCPWorker(PLC plc)
        {
            if (plc == null)
            {
                return;
            }

            // initialize modbus TCP/IP
            MBmaster = new Master("192.168.7.27", 502);
            // connecting message "Connecting to [IP]:[PORT]"
            //string str = string.Format("Connecting to {0}:{1}", plc.ip, plc.port);

            // refresh dataGrid periodically
            while (MBmaster != null && MBmaster.connected)
            {
                try
                {
                    int i = 0;
                    foreach (Record r in plc.dataFields)
                    {
                        readData(Convert.ToByte(r.id), Convert.ToUInt16(r.addr), Convert.ToUInt16(r.length), i++);
                    }
                    // spin wait
                    SpinWait.SpinUntil(() => false, 1000);
                }
                catch (Exception)
                {
                }
            }
        }

        private void readData(byte id, ushort addr, ushort length, int index)
        {
            try
            {
                // modbus read
                long val = modbusRead(id, addr, length);
                Invoke(new DataFieldHandler(RefreshDataField), new Object[] { index, val.ToString() });
            }
            catch (Exception)
            {
                //Invoke(new DataFieldHandler(RefreshDataField), new Object[] { index, "????" });
            }
        }

        private long modbusRead(byte id, ushort addr, ushort length)
        {
            // get the most significant digit
            int pow = (int) Math.Pow(10, Math.Floor(Math.Log10(addr)));
            int ldigit = (int)Math.Floor((double)addr / pow);
            addr = (pow > 1) ? (ushort)(addr % pow) : addr;
            byte[] data = new byte[length*2];
            // four operation type
            switch (ldigit)
            {
                case 1:
                    MBmaster.ReadCoils(1, id, addr, length, ref data);
                    break;
                case 2:
                    MBmaster.ReadDiscreteInputs(2, id, addr, length, ref data);
                    break;
                case 3:
                    MBmaster.ReadInputRegister(3, id, addr, length, ref data);
                    break;
                case 4:
                    MBmaster.ReadHoldingRegister(4, id, addr, length, ref data);
                    break;
                default:
                    MBmaster.ReadHoldingRegister(4, id, addr, length, ref data);
                    break;
            }
            return (data[0] << 8) + data[1];
        }

        #endregion

        // status delegate
        delegate void DataFieldHandler(int i, string str);
        private void RefreshDataField(int i, string str)
        {
            if (listView_data.Items.Count > i)
            {
                listView_data.Items[i].SubItems[1].Text = str;
            }
        }

    }
}
