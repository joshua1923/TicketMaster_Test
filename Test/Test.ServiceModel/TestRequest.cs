using System.Collections.Generic;
using ServiceStack;
using Test.ServiceModel.Models;

namespace Test.ServiceModel
{
    [Route("/test/{range}")]
    public class TestRequest : IReturn<TestResponse>
    {
        public int[] Range { get; set; }
    }

    public class TestResponse
    {
        public string Result { get; set; }
        public Summary Summary { get; set; }
    }
}