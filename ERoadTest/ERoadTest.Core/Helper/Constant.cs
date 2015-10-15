using System;

namespace ERoadTest
{
    public class Constant
    {
        public const int ZoomLevel = 15;
        public const int TimeInterval = 20000;

        public const string GoogleApiKey = "AIzaSyDm2gKMODWhfzGtEVAtR5jAhmay7rbs-CQ";
        public const string GoogleServerkey = "AIzaSyDWq9fFIh3IxuqGhBLLHNoG6btEYuKO9gI";

        public const string GoogleDirectionUrl = "https://maps.googleapis.com/maps/api/directions/json?origin={0}&destination={1}&key=" + GoogleServerkey + "";
        public const string GoogleTimeZone = "https://maps.googleapis.com/maps/api/timezone/json?location={0}&timestamp={1}&sensor=false";
        public const string GeoCodingUrl = "https://maps.googleapis.com/maps/api/geocode/json?{0}&key=" + GoogleServerkey + "";
    }
}