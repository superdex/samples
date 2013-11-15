using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ScannerPOC.Models;

namespace ScannerPOC.Controllers
{
    public class TransmissionController : Controller
    {
        private Entities db = new Entities();

        //
        // GET: /Transmission/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Transmission/Nearby/5
        // return transmissions after (2 hrs) and a little before (30 min?) this one
        public ActionResult Nearby(Transmission thistrans)
        {
            thistrans = db.Transmissions.Find(thistrans.Id);
            DateTime justbefore = thistrans.TimeStamp.AddMinutes(-30);
            DateTime justafter = thistrans.TimeStamp.AddHours(2);
            List<Transmission> nearby =
                db.Transmissions.AsQueryable().Where(
                    x => (x.TimeStamp>=justbefore)&&(x.TimeStamp<=justafter)&&(x.ChannelName == thistrans.ChannelName) && (x.ChannelSubName == thistrans.ChannelSubName)).OrderBy(x=>x.TimeStamp).ToList();
            return new JsonResult(){Data=nearby, JsonRequestBehavior = JsonRequestBehavior.AllowGet};
        }

    }
}
