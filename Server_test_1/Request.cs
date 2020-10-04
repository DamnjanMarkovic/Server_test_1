using System;
namespace Server_test_1
{
    public class Request
    {

        public String Type { get; set; }
        public String URL { get; set; }
        public String Host { get; set; }


        private Request(String type, String url, String host)
        {
        }

        public static Request GetRequest(String request)
        {
            if (String.IsNullOrEmpty(request))
                return null;

            String[] tokens = request.Split(' ');
            return new Request("", "", "");

        }
    }
}
