using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using CommunityPlugin.Objects;
using CommunityPlugin.Objects.Helpers;
using CommunityPlugin.Objects.Interface;
using CommunityPlugin.Objects.Models;
using CommunityPlugin.Objects.Models.WCM.DocumentMapper;
using CommunityPlugin.Objects.Models.WCM.FieldExtraction;
using EllieMae.Encompass.Automation;
using Newtonsoft.Json;
using WyndhamLib.Authentication;

namespace CommunityPlugin.Standard_Plugins
{
    public class DataExtractionLoanAllDocsPlugin : Plugin, ILogin, ILoanOpened
    {
        private IEnumerable<IClassifiedDocument> _fieldExtractedDocs;
        private List<Document> _dataExtractionDocumentMaps;
        private static readonly WcmSettings WcmConfig = CDOHelper.CDO.CommunitySettings.WcmSettings;
        private Task getDocumentMapsTask;

        public override bool Authorized()
        {
            return PluginAccess.CheckAccess(nameof(DataExtractionLoanAllDocsPlugin));
        }


        public override void Login(object sender, EventArgs e)
        {
            getDocumentMapsTask = new TaskFactory().StartNew(() =>
            {
                _dataExtractionDocumentMaps =
                    WcmHelpers.GetMapperDocuments(WcmConfig.DocumentMapperAiExternalSourceId, WcmConfig);
            });
        }

        public override void LoanOpened(object sender, EventArgs e)
        {
            getDocumentMapsTask.Wait();
            // get all field extraction for loans
            LoadExtractedDataFieldsForm();
        }

        private void LoadExtractedDataFieldsForm()
        {
            new TaskFactory().StartNew(() =>
            {
                GetLoanExtractedDataFieldsAsync();

            }).ContinueWith((x) =>
            {
                var th = new Thread(() =>
                {
                    var form = new DataExtractionLoanAllDocsForm(_fieldExtractedDocs.ToList(), _dataExtractionDocumentMaps);
                    form.FormClosing += (s, e) => Application.ExitThread();
                    form.Show();
                    Application.Run();
                });
                th.SetApartmentState(ApartmentState.STA);
                th.Start();
            });
        }

        private async Task GetLoanExtractedDataFieldsAsync()
        {
            try
            {
                var url = WcmConfig.GetFieldExtractionDataForLoanUrl + $"?loanGuid={EncompassApplication.CurrentLoan.Guid}";
                HttpResponseMessage httpResponse = WyndhamClientManager.GetAuthHttpClient().GetResponseMessage(url);
                var rawJson = await httpResponse.Content.ReadAsStringAsync();
                if (httpResponse.IsSuccessStatusCode)
                {
                    _fieldExtractedDocs = JsonConvert.DeserializeObject<List<ClassifiedDocument>>(rawJson);
                }
                else
                {
                    throw new Exception($"Major error getting Loan Field Extraction Data. " +
                                        "Please restart Encompass and if the error persists submit a help desk ticket. " +
                                        $"{Environment.NewLine}{Environment.NewLine}Status Code: '{httpResponse.StatusCode}'");
                }

            }
            catch (Exception e)
            {
                MessageBox.Show($"Error: {e.ToString()}");
                throw;
            }


        }
    }
}
