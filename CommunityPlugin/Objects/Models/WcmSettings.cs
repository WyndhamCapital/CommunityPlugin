﻿using System.Collections.Generic;
using CommunityPlugin.Objects.Models.WCM.PricingAlertAudit;

namespace CommunityPlugin.Objects.Models
{
    public class WcmSettings
    {
        public string GetDocumentImporterSourcesUrl { get; set; }
        public string GetDocumentMapperExternalSourcesUrl { get; set; }

        public string GetDocumentMapperDocumentsUrl { get; set; }

        public string UpdateDocumentMapperDocumentUrl { get; set; }

        public string GetAllPortalDocumentsUri { get; set; }

        public string GetDocumentFromBlendUri { get; set; }

        public string UpdateDocExportStatusBlendUri { get; set; }

        public List<PricingAlertField> PricingAlertAuditFields { get; set; }


    }
}