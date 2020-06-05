using System;
using Covid19;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Covid19
{
    public class CovidDataHelper
    {
        public Area ChainFilter(Area global_area, params string[] filters)
        {
            Area to_filter = global_area;
            foreach (string s in filters)
            {
                bool HaveIt = false;
                foreach (Area a in to_filter.areas)
                {
                    if (a.displayName.ToLower().Contains(s.ToLower()))
                    {
                        HaveIt = true;
                        to_filter = a;
                    }
                }

                if (HaveIt == false)
                {
                    throw new Exception("Unable to find area '" + s + "'.");
                }
            }

            return to_filter;
        }
    
        public async Task<Area> GetGlobalDataAsync()
        {
            HttpClient hc = new HttpClient();
            HttpResponseMessage hrm = await hc.GetAsync("https://bing.com/covid/local/newyork_unitedstates");
            string web = await hrm.Content.ReadAsStringAsync();

            int loc1 = 0;
            int loc2 = 0;

            loc1 = web.IndexOf("var data");
            loc1 = web.IndexOf("=", loc1 + 1);
            loc2 = web.IndexOf(";</script>", loc1 + 1);
            string data = web.Substring(loc1 + 1, loc2 - loc1 - 1);

            Area a = JsonConvert.DeserializeObject<Area>(data);

            return a;
        }
    
        public Area[] DistanceSort(Area[] areas, float my_lat, float my_lon)
        {
            List<Area> PickFrom = new List<Area>();
            foreach (Area a in areas)
            {
                PickFrom.Add(a);
            }


            //Filter
            List<Area> Sorted = new List<Area>();
            do
            {
                Area winner = PickFrom[0];
                foreach (Area a in PickFrom)
                {
                    float ClosestDistance = winner.DistanceFromPoint(my_lat, my_lon);
                    float ThisDistance = a.DistanceFromPoint(my_lat, my_lon);
                    if (ThisDistance < ClosestDistance)
                    {
                        winner = a;
                    }
                }
                Sorted.Add(winner);
                PickFrom.Remove(winner);
            } while (PickFrom.Count > 0);

            return Sorted.ToArray();
        }
    
    }
}