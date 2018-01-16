using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Bot.Builder.FormFlow;

namespace MockBotTest.Models

{   
    public enum ReleasePrioList
    {
        Prio0,
        Prio1,
        Night,
        Lite
    }

    public enum FlagsOptions
    {
        Daily,
        Full,
        Part,
        Lite

    }

    [Serializable]

    public class ReleaseForm
    {

        public ReleasePrioList? Release;
        public int? NoOfBuilds;
        public DateTime? Releasedate;
        public int? NoOfDays;
        public List<FlagsOptions> ReleaseFlags;



        public static IForm<ReleaseForm> BuildForm()
        {
            return new FormBuilder<ReleaseForm>()
                .Message("Welcome to release function :)")
                .Build();
        }
    }
}