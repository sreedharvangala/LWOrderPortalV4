namespace Finsoft.Handlers
{
    
    
    public partial class BlobFactoryConfig : BlobFactory
    {
        
        public static void Initialize()
        {
            // register blob handlers
            RegisterHandler("CampaignPdffile", "\"dbo\".\"Campaign\"", "\"Pdffile\"", new string[] {
                        "\"CampaignNo\""}, "Campaign Pdffile", "Campaign", "Pdffile");
            RegisterHandler("PromotionImage", "\"dbo\".\"Promotion\"", "\"Image\"", new string[] {
                        "\"PromotionID\""}, "Promotion Attach PDF File", "Promotion", "Image");
            RegisterHandler("SiteContentData", "\"dbo\".\"SiteContent\"", "\"Data\"", new string[] {
                        "\"SiteContentID\""}, "Site Content Data", "SiteContent", "Data");
        }
    }
}
