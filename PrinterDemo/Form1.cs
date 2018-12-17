using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;


namespace PrinterDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            // init serial com port at here
            // Todo: set the arduino COM port at here. Check at device manager
            SerialPort mySerialPort = new SerialPort("COM5");

            mySerialPort.BaudRate = 9600;
            mySerialPort.Parity = Parity.None;
            mySerialPort.StopBits = StopBits.One;
            mySerialPort.DataBits = 8;
            mySerialPort.Handshake = Handshake.None;
            mySerialPort.RtsEnable = true;
            mySerialPort.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);
            mySerialPort.Open();
        }

        private void btn_print_Click(object sender, EventArgs e)
        {
            TSCLIB_DLL.openport("TSC TDP-225");                                                 //Open specified printer driver
            TSCLIB_DLL.setup("25", "220", "1", "8", "1", "6", "0");                           //Setup the media size and sensor type info
            TSCLIB_DLL.clearbuffer();                                                           //Clear image buffer
            //TSCLIB_DLL.barcode("150", "600", "128", "100", "1", "270", "2", "2", "Barcode Test"); //Drawing barcode
            //TSCLIB_DLL.printerfont("100", "350", "3", "270", "1", "1", "Print Font Test");        //Drawing printer font
            //TSCLIB_DLL.windowsfont(100, 400, 24, 90, 0, 0, "ARIAL", "Windows Arial Font Test");  //Draw windows font
            //TSCLIB_DLL.downloadpcx("UL.PCX", "UL.PCX");                                         //Download PCX file into printer
            TSCLIB_DLL.sendcommand("QRCODE 30,30,M,6,0,M2,\"BRADY123\"");                                //Drawing PCX graphic
            TSCLIB_DLL.printlabel("1", "1");                                                    //Print labels
            TSCLIB_DLL.closeport();                                                      //Close specified printer driver 
        }
   
        private static void DataReceivedHandler(
                            object sender,
                            SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            string indata = sp.ReadExisting();

            // If received "Detected!" from arduino Port
            if (indata.Contains("D"))
            {
                //MessageBox.Show("Is Dot Net Perls awesome?", "Important Question", MessageBoxButtons.YesNo);
                // Setup printer to print something
                TSCLIB_DLL.openport("TSC TDP-225");                                           //Open specified printer driver
                TSCLIB_DLL.setup("25", "220", "1", "8", "1", "6", "0");                            //Setup the media size and sensor type info
                TSCLIB_DLL.clearbuffer();                                                           //Clear image buffer
                //TSCLIB_DLL.barcode("100", "100", "128", "100", "1", "270", "2", "2", "Barcode Test"); //Drawing barcode
                //TSCLIB_DLL.printerfont("100", "250", "3", "0", "1", "1", "Print Font Test");        //Drawing printer font
                //TSCLIB_DLL.windowsfont(100, 300, 24, 270, 0, 0, "ARIAL", "Windows Arial Font Test");  //Draw windows font
                //TSCLIB_DLL.downloadpcx("UL.PCX", "UL.PCX");                                         //Download PCX file into printer
                TSCLIB_DLL.sendcommand("QRCODE 30,30,M,6,0,M2,\"BRADY123\"");
                //TSCLIB_DLL.sendcommand("PUTPCX 100,400,\"UL.PCX\"");
                //Drawing PCX graphic
                TSCLIB_DLL.printlabel("1", "1");                                                    //Print labels
                TSCLIB_DLL.closeport();
            }
        }
    }
}
