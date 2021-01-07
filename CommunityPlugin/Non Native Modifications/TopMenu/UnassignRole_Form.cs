using CommunityPlugin.Objects.Models;
using EllieMae.Encompass.Automation;
using EllieMae.Encompass.BusinessObjects.Loans;
using EllieMae.Encompass.Collections;
using EllieMae.Encompass.Query;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CommunityPlugin.Non_Native_Modifications.TopMenu
{
    public partial class UnassignRole_Form : Form
    {

        private PasteLoanNumbers_Dialog loanNumbersDataDialog;

        public UnassignRole_Form()
        {
            InitializeComponent();
        }

        private void UnassignRole_Form_Load(object sender, EventArgs e)
        {
            this.LoadSettings();
        }

        private void LoadSettings()
        {
            this.LoadRoles();

            loanNumbersDataDialog = new PasteLoanNumbers_Dialog();
            flowLayoutPanel.Controls.Add(loanNumbersDataDialog);
        }

        void LoadRoles()
        {
            rolesDD.Items.Add("");
            List<Role> roles = new List<Role>();
            foreach (Role r in EncompassApplication.Session.Loans.Roles)
            {
                roles.Add(r);
            }
            roles = roles.OrderBy(x => x.Name).ToList();
            rolesDD.DisplayMember = "Name";
            rolesDD.DataSource = roles;
        }

        private void buttonStartUnassign_ClickAsync(object sender, EventArgs e)
        {
            if (!this.ValidateContents())
                return;


            DialogResult dialogResult = MessageBox.Show("Unassigning this role canNOT be undone. Are you sure you wish to continue?", "Are you sure?", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.No)
            {
                return;
            }

            this.UnassignRoleAndSaveResultsAsync();
    

        }

        private async Task UnassignRoleAndSaveResultsAsync()
        {

            var loanNumbersToUnassign = loanNumbersDataDialog.GetLoanNumbersFromDataGrid();
            var roleSelected = (Role)rolesDD.SelectedItem;

            statusLabel.Visible = true;
            var progress = new Progress<string>(s => statusLabel.Text = s);

            buttonStartUnassign.Enabled = false;


            try
            {
                List<LoanActionResult> results = await Task.Factory.StartNew(() => UnassignRole(progress, roleSelected, loanNumbersToUnassign),
                           TaskCreationOptions.LongRunning);

                this.SaveResults(progress, results);

            }
            catch (Exception ex)
            {
                statusLabel.Text = $"Error Hit: {ex.ToString()}";
            }

        }


        private void SaveResults(IProgress<string> progress, List<LoanActionResult> unassignResults)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("LoanNumber, Result, WasSuccessful,ErrorMessage");

            foreach (var result in unassignResults)
            {
                string errorMsg = string.IsNullOrEmpty(result.ErrorMessage) ? "" : result.ErrorMessage.Replace(",", ". ");
                sb.AppendLine($"{result.LoanNumber}, {result.Result}, {result.WasSuccessful}, {errorMsg}");
            }

            bool exists = System.IO.Directory.Exists(@"c:\temp\");

            if (!exists)
                System.IO.Directory.CreateDirectory(@"c:\temp\");

            string name = string.Format("C:\\temp\\UnassignResults-{0:MM-dd-yyyy_hh-mm-tt}.CSV", DateTime.Now);
            using (System.IO.StreamWriter sw = new System.IO.StreamWriter(name))
            {
                sw.Write(sb.ToString());
            }

            progress.Report($"Complete! Results saved to: '{name}'.");

        }

        private List<LoanActionResult> UnassignRole(IProgress<string> progress, Role roleSelected, List<string> loanNumbersToUnassign)
        {
            List<LoanActionResult> response = new List<LoanActionResult>();

            int count = 0;
            foreach (var loanNumber in loanNumbersToUnassign)
            {
                count++;
                string loanProgress = $"{count}/{loanNumbersToUnassign.Count}. {loanNumber}.";

                progress.Report($"{loanProgress} Searching for loan..");
                var loanResult = new LoanActionResult() { WasSuccessful = true };
                try
                {
                    StringFieldCriterion loanNumberCri = new StringFieldCriterion();
                    loanNumberCri.FieldName = "Loan.LoanNumber";
                    loanNumberCri.Value = loanNumber;
                    loanNumberCri.MatchType = StringFieldMatchType.Exact;
                    loanNumberCri.Include = true;

                    QueryCriterion fullQuery = loanNumberCri;

                    LoanIdentityList ids = EncompassApplication.Session.Loans.Query(fullQuery);

                    if (ids.Count == 0) // if no loans found
                    {
                        progress.Report($"{loanProgress} Cannot find loan.");

                        loanResult.Result = $"Cannot find loan"; ;
                        response.Add(loanResult);

                        // logger.Info((rowIndex - 1) + "/" + (rowCount - 1) + ". Cannot find loan number \"" + loanNumberCri + "\". Going to next.");
                        continue; // skip the remainder of this iteration
                    }

                    //logger.Info((rowIndex - 1) + "/" + (rowCount - 1) + ". Opening loan guid: " + ids[0].Guid);

                    progress.Report($"{loanProgress} Opening loan..");

                    Loan currentLoan = EncompassApplication.Session.Loans.Open(ids[0].Guid);
                    loanResult.LoanNumber = currentLoan.LoanNumber;

                    if (currentLoan.GetCurrentLocks().Count > 0)
                    {
                        string msg = $"Loan opened by {currentLoan.GetCurrentLock().LockedBy}";
                        progress.Report($"{loanProgress} {msg}");

                        loanResult.Result = msg;
                        response.Add(loanResult);

                        //loanSummaryCell.Value = $"Loan opened by {currentLoan.GetCurrentLock().LockedBy}";
                        currentLoan.Close();
                        continue;
                    }

                    currentLoan.Lock();

                    bool needToSaveLoan = false;
                    string userName = "";
                    foreach (LoanAssociate loanAssociate in currentLoan.Associates)
                    {
                        if (loanAssociate.WorkflowRole == roleSelected)
                        {
                            if (loanAssociate.User != null)
                            {
                                userName = loanAssociate.User.FullName;
                                loanAssociate.Unassign();
                                needToSaveLoan = true;
                            }
                        }
                    }

                    if (needToSaveLoan)
                    {
                        string msg = $"'{roleSelected.Name}' role successfully unassigned";
                        progress.Report($"{loanProgress} {msg}. Saving...");

                        currentLoan.Commit();
                        loanResult.Result = msg;

                    }
                    else
                    {
                        string msg = $"'{roleSelected.Name}' role not found on loan";
                        progress.Report($"{loanProgress} {msg}.");

                        loanResult.Result = msg;
                    }

                    currentLoan.Close();
                }
                catch (Exception ex)
                {
                    loanResult.WasSuccessful = false;

                    progress.Report($"{loanProgress} ERROR HIT {ex.ToString()}.");

                    loanResult.ErrorMessage = ex.ToString();
                }

                response.Add(loanResult);
            }

            return response;
        }

        private bool ValidateContents()
        {
            var loanNumbers = loanNumbersDataDialog.GetLoanNumbersFromDataGrid();
            if (loanNumbers.Any() == false)
            {
                MessageBox.Show("No Loan numbers are entered!");
                return false;
            }

            if (rolesDD.SelectedItem == null)
            {
                MessageBox.Show("No Role is selected");
                return false;
            }

            if (string.IsNullOrEmpty(rolesDD.Text))
            {
                MessageBox.Show("No Role is selected");
                return false;
            }

            return true;
        }
    }
}
