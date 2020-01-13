
namespace CMCS.CarTransport.JxSampler.Core
{
    /// <summary>
    /// 硬件设备类
    /// </summary>
    public class Hardwarer
    {
        static ThinkCameraSDK.Core.ThinkCameraData rwer1 = new ThinkCameraSDK.Core.ThinkCameraData();
        /// <summary>
        /// 车号识别1
        /// </summary>
        public static ThinkCameraSDK.Core.ThinkCameraData Rwer1
        {
            get { return rwer1; }
        }
    }
}
