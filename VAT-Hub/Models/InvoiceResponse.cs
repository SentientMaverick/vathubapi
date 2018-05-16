namespace VAT_Hub.Controllers
{
    public class InvoiceResponse
    {
        public string Bill_Number { get; internal set; }
        public string folderRSN { get; internal set; }
        public string peopleRSN { get; internal set; }
        public string status { get; internal set; }
        public string statusCode { get; internal set; }
    }
}