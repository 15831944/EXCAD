using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EXCAD
{
    public partial class frmCommand : Form
    {
        public frmCommand()
        {
            InitializeComponent();
        }

        private void frmCommand_Load(object sender, EventArgs e)
        {

        }

        private void btnDrawCircle_Click(object sender, EventArgs e)
        {
            if (!AppManager.ApplicationsStarted)
            {
                MessageBox.Show("External applications is not started");
                return;
            }

            Excel excel = (Excel)AppManager.ExcelApp;
            AutoCAD autoCAD = (AutoCAD)AppManager.AutoCADApp;

            object cx = excel.GetRangeValue(txtCenterXCell.Text);
            object cy = excel.GetRangeValue(txtCenterYCell.Text);
            object r = excel.GetRangeValue(txtRadiusCell.Text);
            autoCAD.DrawCircle(Convert.ToDouble(cx), Convert.ToDouble(cy), Convert.ToDouble(r));
        }

        private void btnWriteObjectInfo_Click(object sender, EventArgs e)
        {
            if (!AppManager.ApplicationsStarted)
            {
                MessageBox.Show("External applications is not started");
                return;
            }

            Excel excel = (Excel)AppManager.ExcelApp;
            AutoCAD autoCAD = (AutoCAD)AppManager.AutoCADApp;

            var objectsProperties = autoCAD.SelectedObjectsProperties();
            int row = 1;
            bool alternativeColor = false;
            foreach (var objectProperties in objectsProperties)
            {
                foreach (var objectProperty in objectProperties)
                {
                    string propertyCellAddress = $"{txtObjectInfoSheet.Text}!C{row}";
                    string valueCellAddress = $"{txtObjectInfoSheet.Text}!D{row}";
                    excel.SetRangeValue(propertyCellAddress, objectProperty.Key);
                    excel.SetRangeValue(valueCellAddress, objectProperty.Value);
                    if (alternativeColor)
                    {
                        excel.SetRangeForeground(propertyCellAddress, 8388608);
                        excel.SetRangeForeground(valueCellAddress, 8388608);
                    }
                    row++;
                }
                alternativeColor = !alternativeColor;
            }
        }
    }
}
