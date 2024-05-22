using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using EasyModbus;

namespace ModbusTCP_POC
{
    public partial class Form1 : Form
    {
        ModbusServer ModServer;
        public Form1()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (btnStart.Text == "START")
            {
                ModServer = new ModbusServer();
                ModServer.Listen();
                timer1.Enabled = true;
                lblStatus.Text = "Status : Started";
                btnStart.Text = "STOP";
            } else if (btnStart.Text == "STOP")
            {
                ModServer.StopListening();
                ModServer = null;
                lblStatus.Text = "Status : Stopped";
                 btnStart.Text = "START";
            }
        }

        private void btnSet_Click(object sender, EventArgs e)
        {
            int iadr = int.Parse(txtRegAdr.Text);

            if (cbxRegType.Text == "Holding Register")
            {
                try
                {
                    short ival = short.Parse(txtRegVal.Text);
                    ModbusServer.HoldingRegisters regs = ModServer.holdingRegisters;
                    regs[iadr] = ival;
                } catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            } else if (cbxRegType.Text == "Input Register")
            {
                short ival = short.Parse(txtRegVal.Text);
                ModbusServer.InputRegisters regs = ModServer.inputRegisters;
                regs[iadr] = ival;
            } else if (cbxRegType.Text == "Digital Input")
            {
                bool ival = bool.Parse(txtRegVal.Text);
                ModbusServer.DiscreteInputs regs = ModServer.discreteInputs;
                regs[iadr] = ival;
            } else if (cbxRegType.Text == "Coil Output")
            {
                short ival = short.Parse(txtRegVal.Text);
                ModbusServer.Coils regs = ModServer.coils;
                regs[iadr] = true;
            }
        }

        private void btnGet_Click(object sender, EventArgs e)
        {
            //ModbusServer.HoldingRegisters regs = ModServer.holdingRegisters;
            //lblGetReg.Text = regs[int.Parse(txtGetReg.Text)].ToString();

            //timer1.Enabled = true;
            ModbusServer.HoldingRegisters regs = ModServer.holdingRegisters;
            int i = 0;
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            bool currentVal = ModServer.coils[30];
            ModServer.coils[30] = !currentVal;
        }
    }
}
