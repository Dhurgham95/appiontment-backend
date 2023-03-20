namespace AppiontmentBackEnd.Helpers
{
    public class StatusNameMap
    {
        public IDictionary<string, string> MappStatusName = new Dictionary<string, string>()
            {
            {"InReview","بانتظار الموافقة"},
            {"Approved","تمت الموافقة"},
            {"Rejected","مرفوض"},
         

            };
    }
}

