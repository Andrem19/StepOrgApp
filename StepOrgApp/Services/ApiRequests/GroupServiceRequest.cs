using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StepOrgApp.Services.ApiRequests
{
    public  class GroupServiceRequest
    {
        private HttpClient _httpClient { get; set; }
        public GroupServiceRequest(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        //public async Task<List<object>> GetAllMyGroups()
        //{
            
        //}
        //public async Task<object> CreateGroup()
        //{

        //}
    }
}
