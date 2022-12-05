using System.Security.Principal;

namespace AmazonFood.Web
{
    public static class SD
    {
        public static string ProductAPIBase { get; set; }

        public enum ApiType
        {

            GET,
            POST, 
            PUT,
            DELETE

        }


    }
}
