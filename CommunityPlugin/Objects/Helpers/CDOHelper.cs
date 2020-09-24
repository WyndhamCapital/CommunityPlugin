using CommunityPlugin.Objects.Models;
using EllieMae.Encompass.Automation;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;

namespace CommunityPlugin.Objects.Helpers
{
    public class CDOHelper
    {
        private const string Name = "CommunitySettings.json";
        private static CDO File;

        public static CDO CDO => File ?? DownloadCDO();

        private static CDO DownloadCDO()
        {
            File = JsonConvert.DeserializeObject<CDO>(Encoding.UTF8.GetString(EncompassApplication.Session.DataExchange.GetCustomDataObject(Name).Data));
            return File;
        }

        public static void UpdateCDO(CDO CDO)
        {
            File = CDO;
        }

        public static void UploadCDO()
        {
            EncompassApplication.Session.DataExchange.SaveCustomDataObject(Name, new EllieMae.Encompass.BusinessObjects.DataObject(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject((object)File))));
        }

        public static void SaveObjectToJsonCDO(string cdoName, Object obj)
        {
            string output = JsonConvert.SerializeObject(obj, Newtonsoft.Json.Formatting.Indented);
            byte[] bytes = Encoding.UTF8.GetBytes(output);
            EllieMae.Encompass.BusinessObjects.DataObject dataObj = new EllieMae.Encompass.BusinessObjects.DataObject(bytes);
            EncompassApplication.Session.DataExchange.SaveCustomDataObject(cdoName, dataObj);
        }


        public static T GetCustomDataObjectValue<T>(string cdoName)
        {
            T obj = default(T);
            try
            {
                var cdo = EncompassApplication.Session.DataExchange.GetCustomDataObject(cdoName);
                if (cdo != null)
                {
                    using (StreamReader sr = new StreamReader(new MemoryStream(cdo.Data)))
                    {
                        obj = JsonConvert.DeserializeObject<T>(sr.ReadToEnd());
                        return obj;
                    }
                }

                return obj;
            }
            catch (Exception)
            { }

            return obj;
        }

    }
}
