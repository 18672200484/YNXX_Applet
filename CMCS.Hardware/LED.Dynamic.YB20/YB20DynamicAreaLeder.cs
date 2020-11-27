using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//
using System.Runtime.InteropServices;

namespace LED.Dynamic.YB20
{
	/// <summary>
	/// 上海仰邦 LED 19版DLL LedDynamicArea 动态区域
	/// </summary>
	public class YB20DynamicAreaLeder
	{
		#region MyRegion
		//创建节目
		public static void Creat_Program_6()
		{
			bxdualsdk.EQprogramHeader_G6 header;
			header.FileType = 0x00;
			header.ProgramID = 0;
			header.ProgramStyle = 0x00;
			header.ProgramPriority = 0x00;
			header.ProgramPlayTimes = 1;
			header.ProgramTimeSpan = 0;
			header.SpecialFlag = 0;
			header.CommExtendParaLen = 0x00;
			header.ScheduNum = 0;
			header.LoopValue = 0;
			header.Intergrate = 0x00;
			header.TimeAttributeNum = 0x00;
			header.TimeAttribute0Offset = 0x0000;
			header.ProgramWeek = 0xff;
			header.ProgramLifeSpan_sy = 0xffff;
			header.ProgramLifeSpan_sm = 0x03;
			header.ProgramLifeSpan_sd = 0x14;
			header.ProgramLifeSpan_ey = 0xffff;
			header.ProgramLifeSpan_em = 0x03;
			header.ProgramLifeSpan_ed = 0x14;
			header.PlayPeriodGrpNum = 0;
			int err = bxdualsdk.bxDual_program_addProgram_G6(ref header);
			//Console.WriteLine("program_addProgram:" + err);
		}
		//添加时段
		public static void Creat_ProgramaddPlayPeriod_6()
		{
			bxdualsdk.EQprogrampTime_G56 Time;
			Time.StartHour = 0x17;
			Time.StartMinute = 0x29;
			Time.StartSecond = 0x00;
			Time.EndHour = 0x17;
			Time.EndMinute = 0x30;
			Time.EndSecond = 0x00;

			bxdualsdk.EQprogramppGrp_G56 headerGrp;
			headerGrp.playTimeGrpNum = 1;
			headerGrp.timeGrp0 = Time;
			headerGrp.timeGrp1 = Time;
			headerGrp.timeGrp2 = Time;
			headerGrp.timeGrp3 = Time;
			headerGrp.timeGrp4 = Time;
			headerGrp.timeGrp5 = Time;
			headerGrp.timeGrp6 = Time;
			headerGrp.timeGrp7 = Time;
			int err = bxdualsdk.bxDual_program_addPlayPeriodGrp_G6(ref headerGrp);
			//Console.WriteLine("program_addPlayPeriodGrp:" + err);
		}
		//节目添加边框
		public static void ProgramAddFrame_6()
		{
			bxdualsdk.EQscreenframeHeader_G6 sfheader;
			sfheader.FrameDispStype = 0x01;    //边框显示方式
			sfheader.FrameDispSpeed = 0x10;    //边框显示速度
			sfheader.FrameMoveStep = 0x01;     //边框移动步长
			sfheader.FrameUnitLength = 2;   //边框组元长度
			sfheader.FrameUnitWidth = 2;    //边框组元宽度
			sfheader.FrameDirectDispBit = 0;//上下左右边框显示标志位，目前只支持6QX-M卡 
			byte[] img = Encoding.Default.GetBytes("\\黄10.png");
			//bxdualsdk.bxDual_program_addFrame_G6(ref sfheader, img);
		}
		//创建区域
		public static void Creat_Area_6(byte AreaType, ushort x, ushort y, ushort w, ushort h, ushort areaID)
		{
			bxdualsdk.EQareaHeader_G6 aheader;
			aheader.AreaType = AreaType;
			aheader.AreaX = x;
			aheader.AreaY = y;
			aheader.AreaWidth = w;
			aheader.AreaHeight = h;
			aheader.BackGroundFlag = 0x00;
			aheader.Transparency = 101;
			aheader.AreaEqual = 0x00;
			bxdualsdk.EQSound_6G stSoundData = new bxdualsdk.EQSound_6G();
			stSoundData.SoundFlag = 0;
			stSoundData.SoundVolum = 0;
			stSoundData.SoundSpeed = 0;
			stSoundData.SoundDataMode = 0;
			stSoundData.SoundReplayTimes = 0;
			stSoundData.SoundReplayDelay = 0;
			stSoundData.SoundReservedParaLen = 0;
			stSoundData.Soundnumdeal = 0;
			stSoundData.Soundlanguages = 0;
			stSoundData.Soundwordstyle = 0;
			stSoundData.SoundDataLen = 0;
			byte[] t = new byte[1];
			t[0] = 0;
			stSoundData.SoundData = IntPtr.Zero;
			aheader.stSoundData = stSoundData;
			int err = bxdualsdk.bxDual_program_addArea_G6(areaID, ref aheader);  //添加图文区域
																				 //Console.WriteLine("program_AddArea:" + err);
		}
		//区域添加边框
		public static void AreaAddFrame_6(ushort areaID)
		{
			bxdualsdk.EQscreenframeHeader_G6 sfheader;
			sfheader.FrameDispStype = 0x01;    //边框显示方式0x00 –闪烁 0x01 –顺时针转动 0x02 –逆时针转动 0x03 –闪烁加顺时针转动 0x04 –闪烁加逆时针转动 0x05 –红绿交替闪烁 0x06 –红绿交替转动 0x07 –静止打出
			sfheader.FrameDispSpeed = 0x10;    //边框显示速度
			sfheader.FrameMoveStep = 0x01;     //边框移动步长，单位为点，此参 数范围为 1~16 
			sfheader.FrameUnitLength = 2;   //边框组元长度
			sfheader.FrameUnitWidth = 2;    //边框组元宽度
			sfheader.FrameDirectDispBit = 0;//上下左右边框显示标志位，目前只支持6QX-M卡 
			byte[] img = Encoding.Default.GetBytes("\\黄10.png");
			bxdualsdk.bxDual_program_picturesAreaAddFrame_G6(areaID, ref sfheader, img);
		}
		//添加内容
		public static void Creat_AddStr_6(ushort areaID, string txt, string fontname)
		{
			byte[] str = Encoding.GetEncoding("GBK").GetBytes(txt);
			byte[] font = Encoding.GetEncoding("GBK").GetBytes(fontname);
			//string str = "Hello,LED\n789";
			bxdualsdk.EQpageHeader_G6 pheader;
			pheader.PageStyle = 0x00;
			pheader.DisplayMode = 0x01;//移动模式
			pheader.ClearMode = 0x01;
			pheader.Speed = 15;//速度
			pheader.StayTime = 0;//停留时间
			pheader.RepeatTime = 1;
			pheader.ValidLen = 0;
			pheader.CartoonFrameRate = 0x00;
			pheader.BackNotValidFlag = 0x00;
			pheader.arrMode = bxdualsdk.E_arrMode.eMULTILINE;
			pheader.fontSize = 12;
			pheader.color = (uint)0x01;
			pheader.fontBold = 0;
			pheader.fontItalic = 0;
			pheader.tdirection = bxdualsdk.E_txtDirection.pNORMAL;
			pheader.txtSpace = 0;
			pheader.Valign = 2;//横向对齐方式（0系统自适应、1左对齐、2居中、3右对齐）
			pheader.Halign = 1;//纵向对齐方式（0系统自适应、1上对齐、2居中、3下对齐）
			IntPtr cc = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(bxdualsdk.EQpageHeader_G6)));
			Marshal.StructureToPtr(pheader, cc, false);
			int err = bxdualsdk.bxDual_program_picturesAreaAddTxt_G6(areaID, str, font, ref pheader);
			//int err = bxdualsdk.bxDual_program_fontPath_picturesAreaAddTxt_G6(areaID, str, font, cc);
			//Console.WriteLine("program_picturesAreaAddTxt:" + err);
		}
		//添加图片
		public static void Creat_Addimg_6(ushort areaID, ushort picID, string txt)
		{
			bxdualsdk.EQpageHeader_G6 pheader;
			pheader.PageStyle = 0x00;
			pheader.DisplayMode = 0x02;
			pheader.ClearMode = 0x01;
			pheader.Speed = 15;
			pheader.StayTime = 200;
			pheader.RepeatTime = 1;
			pheader.ValidLen = 0;
			pheader.CartoonFrameRate = 0x00;
			pheader.BackNotValidFlag = 0x00;
			pheader.arrMode = bxdualsdk.E_arrMode.eSINGLELINE;
			pheader.fontSize = 10;
			pheader.color = (uint)0x01;
			pheader.fontBold = 0;
			pheader.fontItalic = 0;
			pheader.tdirection = bxdualsdk.E_txtDirection.pNORMAL;
			pheader.txtSpace = 0;
			pheader.Valign = 2;
			pheader.Halign = 2;
			byte[] img = Encoding.Default.GetBytes(txt);
			IntPtr cc = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(bxdualsdk.EQpageHeader_G6)));
			Marshal.StructureToPtr(pheader, cc, false);
			int err = bxdualsdk.bxDual_program_pictureAreaAddPic_G6(areaID, picID, ref pheader, img);
			//Console.WriteLine("program_pictureAreaAddPic_G6:" + err);
		}
		//发送 节目
		public static void Net_SengProgram_6(byte[] ipAdder, ushort port)
		{
			int err = 0;
			//byte[] arrProgram = new byte[100];//[Marshal.SizeOf(typeof(bxdualsdk.EQprogram))];
			//bxdualsdk.EQprogram_G6 program;
			//err = bxdualsdk.program_IntegrateProgramFile_G6(arrProgram);
			//Console.WriteLine("program_IntegrateProgramFile:" + err);
			//IntPtr dec = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(bxdualsdk.EQprogram_G6)));
			//Marshal.Copy(arrProgram, Marshal.SizeOf(typeof(bxdualsdk.EQprogram_G6)) * 0, dec, Marshal.SizeOf(typeof(bxdualsdk.EQprogram_G6)));
			//program = (bxdualsdk.EQprogram_G6)Marshal.PtrToStructure(dec, typeof(bxdualsdk.EQprogram_G6));
			//Marshal.FreeHGlobal(dec);
			bxdualsdk.EQprogram_G6 program = new bxdualsdk.EQprogram_G6();
			program.fileName = Encoding.GetEncoding("GBK").GetBytes("P000");
			program.fileType = 0;
			program.fileLen = 0;
			program.fileAddre = IntPtr.Zero;
			program.dfileName = Encoding.GetEncoding("GBK").GetBytes("D000");
			program.dfileType = 0;
			program.dfileLen = 0;
			program.dfileAddre = IntPtr.Zero;
			err = bxdualsdk.bxDual_program_IntegrateProgramFile_G6(ref program);
			err = bxdualsdk.bxDual_program_deleteProgram_G6();
			//Console.WriteLine("program_deleteProgram:" + err);

			err = bxdualsdk.bxDual_cmd_ofsStartFileTransf(ipAdder, port);
			//Console.WriteLine("cmd_ofsStartFileTransf:" + err);

			err = bxdualsdk.bxDual_cmd_ofsWriteFile(ipAdder, port, program.dfileName, program.dfileType, program.dfileLen, 1, program.dfileAddre);
			//Console.WriteLine("cmd_ofsWriteFile:" + err);
			err = bxdualsdk.bxDual_cmd_ofsWriteFile(ipAdder, port, program.fileName, program.fileType, program.fileLen, 1, program.fileAddre);
			//Console.WriteLine("cmd_ofsWriteFile:" + err);
			err = bxdualsdk.bxDual_cmd_ofsEndFileTransf(ipAdder, port);
			//Console.WriteLine("cmd_ofsEndFileTransf:" + err);
			err = bxdualsdk.bxDual_program_freeBuffer_G6(ref program);
		}
		public static void Com_SengProgram_6(byte[] com)
		{
			int err = 0;
			bxdualsdk.EQprogram_G6 program = new bxdualsdk.EQprogram_G6();
			err = bxdualsdk.bxDual_program_IntegrateProgramFile_G6(ref program);
			//Console.WriteLine("program_IntegrateProgramFile:" + err);
			err = bxdualsdk.bxDual_program_deleteProgram_G6();
			//Console.WriteLine("program_deleteProgram:" + err);

			err = bxdualsdk.bxDual_cmd_uart_ofsStartFileTransf(com, 2);
			//Console.WriteLine("cmd_uart_ofsStartFileTransf:" + err);

			err = bxdualsdk.bxDual_cmd_uart_ofsWriteFile(com, 2, program.dfileName, program.dfileType, program.dfileLen, 1, program.dfileAddre);
			//Console.WriteLine("cmd_uart_ofsWriteFile:" + err);
			err = bxdualsdk.bxDual_cmd_uart_ofsWriteFile(com, 2, program.fileName, program.fileType, program.fileLen, 1, program.fileAddre);
			//Console.WriteLine("cmd_uart_ofsWriteFile:" + err);
			err = bxdualsdk.bxDual_cmd_uart_ofsEndFileTransf(com, 2);
			//Console.WriteLine("cmd_uart_ofsEndFileTransf:" + err);
			err = bxdualsdk.bxDual_program_freeBuffer_G6(ref program);
		}
		#endregion
		//控制卡端口
		public static ushort port = 5005;
		public static byte[] LEDip;
		/// <summary>
		/// 打开LED设备
		/// </summary>
		/// <param name="ip"></param>
		/// <param name="areaID"></param>
		/// <returns></returns>
		public static bool OpenLED(string ip, byte areaID)
		{
			LEDip = Encoding.GetEncoding("GBK").GetBytes(ip);
			try
			{
				//初始化动态库
				int err = bxdualsdk.bxDual_InitSdk();
				bxdualsdk.Ping_data data = new bxdualsdk.Ping_data();
				err = bxdualsdk.bxDual_cmd_tcpPing(LEDip, port, ref data);
				//设置屏幕参数相关  发送节目必要接口，发送动态区可忽略
				err = bxdualsdk.bxDual_program_setScreenParams_G56(bxdualsdk.E_ScreenColor_G56.eSCREEN_COLOR_SINGLE, data.ControllerType, bxdualsdk.E_DoubleColorPixel_G56.eDOUBLE_COLOR_PIXTYPE_1);

				return err == 0;
			}
			catch
			{
				return false;
			}
		}

		/// <summary>
		/// 更新LED信息
		/// </summary>
		/// <param name="str"></param>
		public static bool UpdateLED(string str)
		{
			bxdualsdk.Ping_data data = new bxdualsdk.Ping_data();
			int err = bxdualsdk.bxDual_cmd_tcpPing(LEDip, port, ref data);
			//设置屏幕参数相关  发送节目必要接口，发送动态区可忽略
			err = bxdualsdk.bxDual_program_setScreenParams_G56(bxdualsdk.E_ScreenColor_G56.eSCREEN_COLOR_SINGLE, data.ControllerType, bxdualsdk.E_DoubleColorPixel_G56.eDOUBLE_COLOR_PIXTYPE_1);
			//创建节目
			Creat_Program_6();
			//创建图文区
			Creat_Area_6(0, 0, 0, 96, 64, 1);
			//添加显示文本
			Creat_AddStr_6(1, str, "宋体");
			//发送节目显示
			Net_SengProgram_6(LEDip, port);
			err = bxdualsdk.bxDual_cmd_check_time(LEDip, port);
			return err == 0;
		}

		/// <summary>
		/// 关闭LED
		/// </summary>
		public static void CloseLED()
		{
			try
			{
				//释放动态库
				bxdualsdk.bxDual_ReleaseSdk();
			}
			catch
			{
			}
		}
	}
}
