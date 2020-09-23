using CommunityPlugin.Objects.Interface;
using CommunityPlugin.Objects.Models;
using EllieMae.EMLite.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CommunityPlugin.Non_Native_Modifications.TopMenu
{
    public partial class AnalysisTool_Form : Form
    {
        List<AnalysisBase> AnalysisClasses = new List<AnalysisBase>();
        AnalysisBase CurrentAnalysis;
        public AnalysisTool_Form()
        {
            InitializeComponent();
            InterfaceHelper i = new InterfaceHelper();
            AnalysisClasses.AddRange(i.GetAll(typeof(AnalysisBase)).Select(x=> Activator.CreateInstance(x)).Cast<AnalysisBase>().ToList());
            cmbFilter.Items.AddRange(AnalysisClasses.Select(x=>x.GetType().Name).ToArray());
            Tracing.Debug = true;
        }

        public void LoadAnalysisToolCache()
        {

            pnlStatus.Visible = true;

            Thread thread = new Thread(new ThreadStart(WorkThreadFunction));
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            while (thread.IsAlive)
            {
                Thread.Sleep(500);
                Application.DoEvents();
            }

            pnlStatus.Visible = false;

        }

        private void WorkThreadFunction()
        {
            foreach (AnalysisBase baseClass in AnalysisClasses)
                baseClass.LoadCache();
        }

 
        private void btnSearch_Click(object sender, EventArgs e)
        {
            if(CurrentAnalysis != null)
            {
                UpdateGrid(CurrentAnalysis.Search(txtSearch.Text));
            }
        }

        private void UpdateGrid(AnalysisResult Analysis)
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = Analysis.Result;
            dataGridView1.AutoGenerateColumns = true;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void cmbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            AnalysisBase current = AnalysisClasses.Where(x => x.GetType().Name.Equals(cmbFilter.Text)).FirstOrDefault();
            if (current == null)
                return;

            CurrentAnalysis = current;
        }


    }
}
