using Microsoft.AspNetCore.SignalR;

namespace SignalRSample.Hubs
{
    public class ViewerCounterHub : Hub
    {
        public Dictionary<string,int> GetRaceStatus()
        {
            return ViewerCounterDictionaryInMem.ViewerCounterDictionary;
        }
    }
}
