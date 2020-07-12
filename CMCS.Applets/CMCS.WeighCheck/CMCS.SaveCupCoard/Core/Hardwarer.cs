
namespace CMCS.SaveCupCoard.Core
{
	/// <summary>
	/// 硬件设备类
	/// </summary>
	public class Hardwarer
	{
		static CupCoard.DJ.CupCoard_DJ iocer = new CupCoard.DJ.CupCoard_DJ();
		/// <summary>
		/// 存样柜
		/// </summary>
		public static CupCoard.DJ.CupCoard_DJ Iocer
		{
			get { return iocer; }
		}

	}
}
