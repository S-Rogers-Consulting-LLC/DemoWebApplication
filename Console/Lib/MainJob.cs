using System;
using log4net;
using Quartz;

namespace WebSite.Lib {
    public class MainJob : IJob {
        #region Members
        private static readonly ILog logger = LogManager.GetLogger(typeof(MainJob));
        #endregion

        public void Execute(IJobExecutionContext argIJobExecutionContext) {
            Console.WriteLine("The current time is: {0}", DateTime.Now);
        }
    }
}
