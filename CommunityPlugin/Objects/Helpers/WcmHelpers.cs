using CommunityPlugin.Objects.Models;
using CommunityPlugin.Objects.Models.WCM.DocumentImporter;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WyndhamLib.Authentication;

namespace CommunityPlugin.Objects.Helpers
{
    public static class WcmHelpers
    {
        internal static async Task<List<ExternalSource>> GetDocumentImporterExternalSourcesAsync(WcmSettings wcmSettings)
        {

            string url = wcmSettings.GetDocumentImporterSourcesUrl;
            HttpResponseMessage httpResponse = WyndhamClientManager.GetAuthHttpClient().GetResponseMessage(url);
            var rawJson = await httpResponse.Content.ReadAsStringAsync();
            if (httpResponse.IsSuccessStatusCode)
            {
                var response = JsonConvert.DeserializeObject<List<ExternalSource>>(rawJson);
                return response;
            }
            else
            {
               throw new Exception($"Major error getting Document Importer Settings. " +
                            "Please restart Encompass and if the error persists submit a help desk ticket. " +
                            $"{Environment.NewLine}{Environment.NewLine}Status Code: '{httpResponse.StatusCode}'");
            }

        }
   
    public static DataGridViewRow GetCurrentGridRow(DataGridView dgv, out string errorMsg)
        {
            errorMsg = null;

            var selectedCell = dgv.CurrentCell;
            if (selectedCell == null)
            {
                errorMsg = "No Row is selected, please try again.";
                return null;
            }

            DataGridViewRow row = dgv.Rows[dgv.CurrentCell.RowIndex];
            if (row.IsNewRow)
            {
                errorMsg = "No Row is selected, please try again.";
                return null;
            }

            return row;
        }
    }
}
