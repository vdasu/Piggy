using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Piggy
{
    [Serializable]
    public class Views
    {
        private Dictionary<string, int> viewCountDict;
        private int maxViewCount;

        public string MaxViewedRestaurant { get; set; }

        public Views()
        {
            this.maxViewCount = 0;
            this.MaxViewedRestaurant = String.Empty;
            this.viewCountDict = new Dictionary<string, int>();
        }

        public void UpdateMostViewed(string visitedRestaurant)
        {
            // to update the MaxViewCount and MaxViewedRestaurant
            // returns new most viewed restaurant name and counter

            // visitedRestaurant should be a restaurantId + ; + restaurantName

            int previousViewCount;
            if (this.viewCountDict.ContainsKey(visitedRestaurant))
            {
                previousViewCount = this.viewCountDict[visitedRestaurant];
            } 
            else
            {
                previousViewCount = 0;
            }
            int newViewCount = previousViewCount + 1;
            if (this.maxViewCount == 0 || this.maxViewCount < newViewCount)
            {
                this.maxViewCount = newViewCount;                
                this.MaxViewedRestaurant = visitedRestaurant;
            }
            this.viewCountDict[visitedRestaurant] = newViewCount;
        }


    }
}