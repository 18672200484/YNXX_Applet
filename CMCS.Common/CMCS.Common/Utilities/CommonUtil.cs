using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.NetworkInformation;

namespace CMCS.Common.Utilities
{
    public static class CommonUtil
    {
        /// <summary>
        /// 测试Ip是否连通
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static bool PingReplyTest(string ip)
        {
            try
            {
                Ping pingSender = new Ping();
                PingReply reply = pingSender.Send(ip, 120);
                if (reply.Status == IPStatus.Success)
                    return true;
                return false;

            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
