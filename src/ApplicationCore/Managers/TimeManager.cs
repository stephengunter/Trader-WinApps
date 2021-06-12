using ApplicationCore.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Managers
{
    public interface ITimeManager
    {
        bool InTime { get; }
        DateTime BeginTime { get; }
        DateTime EndTime { get; }
    }


    public class TimeManager : ITimeManager
    {
        private readonly string begin;
        private readonly string end;

        private DateTime _beginTime;
        private DateTime _endTime;

        public TimeManager(string begin, string end)
        {
            this.begin = begin;

            this.end = end;
            var now = DateTime.Now;
            var beginTimes = begin.ToTimes();
            _beginTime = new DateTime(now.Year, now.Month, now.Day, beginTimes[0], beginTimes[1], beginTimes[2]);

            var endTimes = end.ToTimes();
            _endTime = new DateTime(now.Year, now.Month, now.Day, endTimes[0], endTimes[1], endTimes[2]);

            if (_endTime <= _beginTime) _endTime = _endTime.AddDays(1);
        }

        public DateTime BeginTime => _beginTime;
        public DateTime EndTime => _endTime;

        public bool InTime => DateTime.Now >= _beginTime && DateTime.Now <= _endTime;

        
    }

}
