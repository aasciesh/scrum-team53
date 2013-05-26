using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ScrumMainApp.Models;
using System.Web.Script.Serialization;

namespace ScrumMainApp.Controllers
{
    public class BurnDownController : Controller
    {
        private ScrumDBEntities2 db = new ScrumDBEntities2();

        public ViewResult Index()
        {
            int projectId = Convert.ToInt32(Session["projectId"]);
            if (Session["projectId"] == null)
                RedirectToAction("Index", "Project");

            var firstSprint = db.Sprints.Where(m => m.projectId == projectId).First();
            ViewBag.firstSprintid = firstSprint.sprintId;
            ViewBag.sprintName = "Sprint : " + firstSprint.sprintNumber;
            return View();
        }


        public ActionResult chartForSprint(int id)
        {
            List<Data> chartData1 = new List<Data>() ;
            
            var sprint = db.Sprints.Single(i => i.sprintId == id);
            List<Task> taskforSprint = new List<Task>();

            var backlogItemlsit = db.BacklogItems.Where(b => b.sprintId == id).OrderBy(i => i.backlogId).ToList();
            var sprintTaskItems = db.Tasks.OrderBy(i => i.taskId).ToList();
            foreach (var item in sprintTaskItems)
            {
                if (backlogItemlsit.Any(b => b.backlogId == item.backlogId))
                    taskforSprint.Add(item);
            }

            int TaskWeight= 0;
            int TaskWeightAtBegining = 0;
            foreach (var item in taskforSprint)
                TaskWeight += Convert.ToInt32(item.timeEstimate);

            TaskWeightAtBegining = TaskWeight;

            ViewBag.startDate = MilliTimeStamp(sprint.startDate);
            ViewBag.endDate = MilliTimeStamp(sprint.endDate);
            ViewBag.totalEffort = TaskWeight;
            int j = 0;
            foreach (DateTime day in EachDay(sprint.startDate, sprint.endDate))
            {
                double timestamp = MilliTimeStamp(day);
                foreach (var item in taskforSprint)
                {
                    if (item.endDate == day)
                    {
                        TaskWeight -= Convert.ToInt32(item.timeEstimate);
                        Data dailydata = new Data { Timestamp= timestamp, TaskEffortLeft= TaskWeight};
                        chartData1.Add(dailydata);
                    }
                }
                    if(j==0){
                        Data dailydata = new Data { Timestamp = timestamp, TaskEffortLeft = TaskWeight };
                        chartData1.Add(dailydata);}
                    
                j++;
            }

            if (Request.IsAjaxRequest())
            {
                List<Data> idealScrumData= new List<Data>();
                Data idealStartPoint = new Data { Timestamp = MilliTimeStamp(sprint.startDate), TaskEffortLeft = TaskWeightAtBegining };
                idealScrumData.Add(idealStartPoint);
                Data idealEndPoint = new Data { Timestamp = MilliTimeStamp(sprint.endDate), TaskEffortLeft = 0};
                idealScrumData.Add(idealEndPoint);

                return Content(new JavaScriptSerializer().Serialize(idealScrumData) + "\n" + new JavaScriptSerializer().Serialize(chartData1));
            }
            
            
            ViewBag.dataList1 = chartData1;
            return PartialView();
        }

        
        public double MilliTimeStamp(DateTime TheDate)
        {
            DateTime d1 = new DateTime(1970, 1, 1);
            DateTime d2 = TheDate.ToUniversalTime();
            TimeSpan ts = new TimeSpan(d2.Ticks - d1.Ticks);


            return ts.TotalMilliseconds;
        }
        public IEnumerable<DateTime> EachDay(DateTime from, DateTime thru)
        {
            for (var day = from.Date; day.Date <= thru.Date; day = day.AddDays(1))
                yield return day;
        }

    }
    class Data
        {
            public double Timestamp {get; set; }
            public int TaskEffortLeft  {get; set; }
        }
}
