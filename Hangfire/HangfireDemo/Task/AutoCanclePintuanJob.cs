/***********************************************************************
 *            Project: CoreCms.Net                                     *
 *                Web: https://CoreCms.Net                             *
 *        ProjectName: 核心内容管理系统                                *
 *             Author: 大灰灰                                          *
 *              Email: JianWeie@163.com                                *
 *         CreateTime: 2020-08-25 1:25:29
 *        Description: 暂无
 ***********************************************************************/


using System;
using Newtonsoft.Json;

namespace HangfireDemo.Task
{
    /// <summary>
    /// 拼团自动取消到期团
    /// </summary>
    public class AutoCanclePinTuanJob
    {
        public async System.Threading.Tasks.Task Execute()
        {
            await System.Threading.Tasks.Task.Run(() =>
            {
                // Just loop.
                int ctr = 0;
                for (ctr = 0; ctr <= 1000000; ctr++)
                { }
                Console.WriteLine("Finished {0} loop iterations",
                                  ctr);
            });
        }
    }
}
