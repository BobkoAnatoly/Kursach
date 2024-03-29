﻿using System.Collections.Generic;

namespace ProfileClassLibrary.BusClasses
{
    public class StopTime
    {
        private string _indexTime;
        public string IndexTime { get => _indexTime; }

        private List<Times> _stopTimeList;
        public List<Times> StopTimeList { get => _stopTimeList; }

        public StopTime(string indexTime, List<Times> stopTimeList)
        {
            _stopTimeList = stopTimeList;
            _indexTime = indexTime;
        }
    }
}
