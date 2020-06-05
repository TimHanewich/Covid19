using System;

namespace Covid19
{
    public class Area
    {
        public string id {get; set;}
        public string displayName {get; set;}
        public Area[] areas {get; set;}
        public int? totalConfirmed {get; set;}
        public int? totalDeaths {get; set;}
        public int? totalRecovered {get; set;}
        public int? totalRecoveredDelta {get; set;}
        public int? totalDeathsDelta {get; set;}
        public int? totalConfirmedDelta {get; set;}
        public DateTime lastUpdated {get; set;}
        public float Lat {get; set;}
        public float Long {get; set;}
        public string parentId {get; set;}

        public float DistanceFromPoint(float lati, float longi)
        {
            float xdiff = longi - Long;
            float ydiff = lati - Lat;
            float xp = xdiff * xdiff;
            float yp = ydiff * ydiff;
            float dist = Convert.ToSingle(Math.Sqrt(xp + yp));
            return dist;
        }
    }
}
