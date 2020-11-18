using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using CMCS.CarTransport.DAO;
using CMCS.CarTransport.JxSampler.Core;
using CMCS.CarTransport.JxSampler.Enums;
using CMCS.CarTransport.JxSampler.Frms.Sys;
using CMCS.Common;
using CMCS.Common.DAO;
using CMCS.Common.Entities;
using CMCS.Common.Entities.CarTransport;
using CMCS.Common.Entities.Fuel;
using CMCS.Common.Entities.Inf;
using CMCS.Common.Entities.Sys;
using CMCS.Common.Enums;
using CMCS.Common.Utilities;
using DevComponents.DotNetBar;
using LED.YB19;

namespace CMCS.CarTransport.JxSampler.Frms
{
	public partial class FrmJxSampler : DevComponents.DotNetBar.Metro.MetroForm
	{
		/// <summary>
		/// 窗体唯一标识符
		/// </summary>
		public static string UniqueKey = "FrmCarSampler";

		public FrmJxSampler()
		{
			InitializeComponent();
		}

		#region Vars

		CarTransportDAO carTransportDAO = CarTransportDAO.GetInstance();
		JxSamplerDAO jxSamplerDAO = JxSamplerDAO.GetInstance();
		CommonDAO commonDAO = CommonDAO.GetInstance();

		/// <summary>
		/// 语音播报
		/// </summary>
		VoiceSpeaker voiceSpeaker = new VoiceSpeaker();

		bool autoHandMode = true;
		/// <summary>
		/// 手动模式=true  手动模式=false
		/// </summary>
		public bool AutoHandMode
		{
			get { return autoHandMode; }
			set
			{
				autoHandMode = value;

				btnSendSamplingPlan.Visible = !value;
				btnSelectAutotruck.Visible = !value;
				btnReset.Visible = !value;
			}
		}

		public static PassCarQueuer passCarQueuer = new PassCarQueuer();

		ImperfectCar currentImperfectCar;
		/// <summary>
		/// 识别或选择的车辆凭证
		/// </summary>
		public ImperfectCar CurrentImperfectCar
		{
			get { return currentImperfectCar; }
			set
			{
				currentImperfectCar = value;

				if (value != null)
					panCurrentCarNumber.Text = value.Voucher;
				else
					panCurrentCarNumber.Text = "等待车辆";
			}
		}

		eFlowFlag currentFlowFlag = eFlowFlag.等待车辆;
		/// <summary>
		/// 当前业务流程标识
		/// </summary>
		public eFlowFlag CurrentFlowFlag
		{
			get { return currentFlowFlag; }
			set
			{
				currentFlowFlag = value;

				lblFlowFlag.Text = value.ToString();
			}
		}

		private CmcsBuyFuelTransport currentBuyFuelTransport;
		/// <summary>
		/// 当前运输记录
		/// </summary>
		public CmcsBuyFuelTransport CurrentBuyFuelTransport
		{
			get { return currentBuyFuelTransport; }
			set
			{
				currentBuyFuelTransport = value;

				if (value != null)
				{
					commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.当前运输记录Id.ToString(), value.Id);

					txtSupplierName.Text = value.SupplierName;
					txtMineName.Text = value.MineName;
					txtTicketWeight.Text = value.TicketWeight.ToString();
					txtTransportCompanyName.Text = value.TransportCompanyName;
					txtFuelKindName.Text = value.FuelKindName;
				}
				else
				{
					commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.当前运输记录Id.ToString(), string.Empty);

					txtSupplierName.ResetText();
					txtMineName.ResetText();
					txtTransportCompanyName.ResetText();
					txtFuelKindName.ResetText();
					txtTicketWeight.ResetText();
				}
			}
		}

		CmcsAutotruck currentAutotruck;
		/// <summary>
		/// 当前车
		/// </summary>
		public CmcsAutotruck CurrentAutotruck
		{
			get { return currentAutotruck; }
			set
			{
				currentAutotruck = value;

				if (value != null)
				{
					commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.当前车Id.ToString(), value.Id);
					commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.当前车号.ToString(), value.CarNumber);

					txtCarNumber.Text = value.CarNumber;
					panCurrentCarNumber.Text = value.CarNumber;

					dbi_CarriageLength.Value = value.CarriageLength;
					dbi_CarriageWidth.Value = value.CarriageWidth;
					dbi_CarriageBottomToFloor.Value = value.CarriageBottomToFloor;
					dbi_CarriageTotalLength.Value = value.CarriageTotalLength;
					dbi_CarriageHeight.Value = value.CarriageHeight;
					dbi_LeftObstacle1.Value = value.LeftObstacle1;
					dbi_LeftObstacle2.Value = value.LeftObstacle2;
					dbi_LeftObstacle3.Value = value.LeftObstacle3;
					dbi_LeftObstacle4.Value = value.LeftObstacle4;
					dbi_LeftObstacle5.Value = value.LeftObstacle5;
					dbi_LeftObstacle6.Value = value.LeftObstacle6;
				}
				else
				{
					commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.当前车Id.ToString(), string.Empty);
					commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.当前车号.ToString(), string.Empty);

					dbi_CarriageLength.Value = 0;
					dbi_CarriageWidth.Value = 0;
					dbi_CarriageBottomToFloor.Value = 0;
					dbi_CarriageTotalLength.Value = 0;
					dbi_CarriageHeight.Value = 0;
					dbi_LeftObstacle1.Value = 0;
					dbi_LeftObstacle2.Value = 0;
					dbi_LeftObstacle3.Value = 0;
					dbi_LeftObstacle4.Value = 0;
					dbi_LeftObstacle5.Value = 0;
					dbi_LeftObstacle6.Value = 0;

					txtCarNumber.ResetText();
					panCurrentCarNumber.ResetText();
				}
			}
		}

		private InfQCJXCYSampleCMD currentSampleCMD;
		/// <summary>
		/// 当前采样命令
		/// </summary>
		public InfQCJXCYSampleCMD CurrentSampleCMD
		{
			get { return currentSampleCMD; }
			set { currentSampleCMD = value; }
		}

		private eEquInfSamplerSystemStatus samplerSystemStatus;
		/// <summary>
		/// 采样机系统状态
		/// </summary>
		public eEquInfSamplerSystemStatus SamplerSystemStatus
		{
			get { return samplerSystemStatus; }
			set
			{
				samplerSystemStatus = value;

				if (value == eEquInfSamplerSystemStatus.就绪待机)
					slightSamplerStatus.LightColor = EquipmentStatusColors.BeReady;
				else if (value == eEquInfSamplerSystemStatus.正在运行 || value == eEquInfSamplerSystemStatus.正在卸样)
					slightSamplerStatus.LightColor = EquipmentStatusColors.Working;
				else if (value == eEquInfSamplerSystemStatus.发生故障)
					slightSamplerStatus.LightColor = EquipmentStatusColors.Breakdown;
				else if (value == eEquInfSamplerSystemStatus.设备停止)
					slightSamplerStatus.LightColor = EquipmentStatusColors.Forbidden;
			}
		}

		/// <summary>
		/// 采样机设备编码
		/// </summary>
		public string SamplerMachineCode;
		/// <summary>
		/// 采样机设备名称
		/// </summary>
		public string SamplerMachineName;

		#endregion

		/// <summary>
		/// 窗体初始化
		/// </summary>
		private void InitForm()
		{
			FrmDebugConsole.GetInstance();

			// 采样机设备编码
			this.SamplerMachineCode = commonDAO.GetAppletConfigString("采样机设备编码");
			this.SamplerMachineName = commonDAO.GetMachineNameByCode(this.SamplerMachineCode);

			// 默认自动
			sbtnChangeAutoHandMode.Value = true;
		}

		private void FrmCarSampler_Load(object sender, EventArgs e)
		{
			timer2_Tick(null, null);
		}

		private void FrmCarSampler_Shown(object sender, EventArgs e)
		{
			InitHardware();

			InitForm();
		}

		private void FrmCarSampler_FormClosing(object sender, FormClosingEventArgs e)
		{
			// 卸载设备
			UnloadHardware();
		}

		#region 设备相关

		#region 车号识别

		void Rwer1_OnScanError(Exception ex)
		{
			Log4Neter.Error("车号识别1", ex);
		}

		void Rwer1_OnStatusChange(bool status)
		{
			// 接收设备状态 
			InvokeEx(() =>
			{
				slightRwer1.LightColor = (status ? Color.Green : Color.Red);

				commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.车号识别1_连接状态.ToString(), status ? "1" : "0");
			});
		}

		public void Rwer1_OnReadSuccess(string carnumber)
		{
			InvokeEx(() =>
			{
				if (carnumber != "无车牌" && this.CurrentFlowFlag == eFlowFlag.等待车辆)
				{
					passCarQueuer.Enqueue(carnumber);
					this.CurrentFlowFlag = eFlowFlag.验证车辆;
					timer1_Tick(null, null);
					//UpdateLedShow(carnumber);
					Log4Neter.Info(string.Format("车号识别1识别到车号：{0}", carnumber));
				}
			});
		}
		#endregion

		#region LED控制卡
		YB19DynamicAreaLeder led = new YB19DynamicAreaLeder();
		/// <summary>
		/// LED1更新标识
		/// </summary>
		bool LED1m_bSendBusy = false;

		private bool _LED1ConnectStatus;
		/// <summary>
		/// LED1连接状态
		/// </summary>
		public bool LED1ConnectStatus
		{
			get
			{
				return _LED1ConnectStatus;
			}

			set
			{
				_LED1ConnectStatus = value;

				slightLED1.LightColor = (_LED1ConnectStatus ? Color.Green : Color.Red);

				commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.LED屏1_连接状态.ToString(), value ? "1" : "0");
			}
		}

		/// <summary>
		/// LED1上一次显示内容
		/// </summary>
		string LED1PrevLedFileContent = string.Empty;

		/// <summary>
		/// 更新LED动态区域
		/// </summary>
		/// <param name="value1">第一行内容</param>
		/// <param name="value2">第二行内容</param>
		private void UpdateLedShow(string value1 = "", string value2 = "")
		{
			FrmDebugConsole.GetInstance().Output("更新LED1:|" + value1 + "|" + value2 + "|");

			if (!this.LED1ConnectStatus) return;
			if (this.LED1PrevLedFileContent == value1 + value2) return;

			if (LED1m_bSendBusy == false)
			{
				LED1m_bSendBusy = true;

				bool nResult = led.UpdateLED(value1 + " " + value2);
				if (!nResult) Log4Neter.Error("更新LED动态区域", new Exception("更新LED动态区域"));

				LED1m_bSendBusy = false;
			}

			this.LED1PrevLedFileContent = value1 + value2;
		}

		#endregion

		#region 设备初始化与卸载

		/// <summary>
		/// 初始化外接设备
		/// </summary>
		private void InitHardware()
		{
			try
			{
				bool success = false;

				// 车号识别1
				Hardwarer.Rwer1.OnActionReadSuccess = Rwer1_OnReadSuccess;
				Hardwarer.Rwer1.OnActionScanError = Rwer1_OnScanError;
				Hardwarer.Rwer1.OnActionStatusChange = Rwer1_OnStatusChange;
				success = Hardwarer.Rwer1.ConnectCamera(commonDAO.GetAppletConfigString("车号识别1_IP地址"), IntPtr.Zero);
				if (!success)
				{
					MessageBoxEx.Show("车号识别1连接失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				}
				else
				{
					InvokeEx(() =>
					{
						Hardwarer.Rwer1.StartPreview(picVideo1.Handle);
					});
				}

				#region LED控制卡

				string led1SocketIP = commonDAO.GetAppletConfigString("LED显示屏1_IP地址");
				if (!string.IsNullOrEmpty(led1SocketIP))
				{
					if (CommonUtil.PingReplyTest(led1SocketIP))
					{
						if (led.OpenLED(led1SocketIP, 0))
						{
							// 初始化成功
							this.LED1ConnectStatus = true;
							UpdateLedShow("等待车辆");
						}
						else
						{
							this.LED1ConnectStatus = false;
							Log4Neter.Error("初始化LED1控制卡", new Exception("通讯失败"));
							MessageBoxEx.Show("LED1控制卡连接失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
						}
					}
					else
					{
						this.LED1ConnectStatus = false;
						Log4Neter.Error("初始化LED1控制卡，网络连接失败", new Exception("网络异常"));
						MessageBoxEx.Show("LED1控制卡网络连接失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					}
				}

				#endregion

				//语音设置
				//voiceSpeaker.SetVoice(commonDAO.GetAppletConfigInt32("语速"), commonDAO.GetAppletConfigInt32("音量"), commonDAO.GetAppletConfigString("语音包"));

				timer1.Enabled = true;
			}
			catch (Exception ex)
			{
				Log4Neter.Error("设备初始化", ex);
			}
		}

		/// <summary>
		/// 卸载设备
		/// </summary>
		private void UnloadHardware()
		{
			// 注意此段代码
			Application.DoEvents();

			try
			{
				commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.当前车Id.ToString(), string.Empty);
				commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.当前车号.ToString(), string.Empty);
			}
			catch { }
			try
			{
				Hardwarer.Rwer1.Close();
			}
			catch { }
			try
			{
				YB19DynamicAreaLeder.CloseLED();
			}
			catch { }
		}

		#endregion

		#endregion

		#region 公共业务

		/// <summary>
		/// 读卡、车号识别任务
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void timer1_Tick(object sender, EventArgs e)
		{
			timer1.Stop();
			timer1.Interval = 2000;

			try
			{
				switch (this.CurrentFlowFlag)
				{
					case eFlowFlag.等待车辆:
						#region

						//if (this.InductorCoil1)
						//{
						//    // 当读卡区域地感有信号，触发读卡或者车号识别

						//    List<string> tags = Hardwarer.Rwer1.ScanTags();
						//    if (tags.Count > 0) passCarQueuer.Enqueue(tags[0]);
						//}

						//if (passCarQueuer.Count > 0) this.CurrentFlowFlag = eFlowFlag.验证车辆;

						#endregion
						break;

					case eFlowFlag.验证车辆:
						#region

						// 队列中无车时，等待车辆
						if (passCarQueuer.Count == 0)
						{
							this.CurrentFlowFlag = eFlowFlag.等待车辆;
							break;
						}

						this.CurrentImperfectCar = passCarQueuer.Dequeue();
						panCurrentCarNumber.Text = this.CurrentImperfectCar.Voucher;

						// 方式一：根据识别的车牌号查找车辆信息
						this.CurrentAutotruck = carTransportDAO.GetAutotruckByCarNumber(this.CurrentImperfectCar.Voucher);

						if (this.CurrentAutotruck != null)
						{
							UpdateLedShow(this.CurrentAutotruck.CarNumber + "读卡成功");
							this.voiceSpeaker.Speak(this.CurrentAutotruck.CarNumber + " 读卡成功", 1, false);

							if (this.CurrentAutotruck.IsUse == 1)
							{
								//if (this.CurrentAutotruck.CarriageLength > 0 && this.CurrentAutotruck.CarriageWidth > 0 && this.CurrentAutotruck.CarriageBottomToFloor > 0)
								//{
								// 未完成运输记录
								CmcsUnFinishTransport unFinishTransport = carTransportDAO.GetUnFinishTransportByAutotruckId(this.CurrentAutotruck.Id, eCarType.入厂煤.ToString());
								if (unFinishTransport != null)
								{
									this.CurrentBuyFuelTransport = carTransportDAO.GetBuyFuelTransportById(unFinishTransport.TransportId);
									if (this.CurrentBuyFuelTransport != null)
									{
										CmcsNoSampler noSampler = commonDAO.SelfDber.Entity<CmcsNoSampler>("where MineId=:MineId and StartTime<=:CreateDate and EndTime>=:CreateDate order by CreateDate desc", new { MineId = this.CurrentBuyFuelTransport.MineId, CreateDate = DateTime.Now });
										if (noSampler != null)
										{
											UpdateLedShow(this.CurrentAutotruck.CarNumber, "无需采样");
											this.voiceSpeaker.Speak(this.CurrentAutotruck.CarNumber + " 无需采样 直接离开", 1, false);
											this.CurrentFlowFlag = eFlowFlag.等待离开;
											break;
										}
										//if (this.CurrentBuyFuelTransport.InFactoryTime.Date < DateTime.Now.Date)
										//{
										this.CurrentBuyFuelTransport.InFactoryTime = DateTime.Now;
										carTransportDAO.GCQCInFactoryBatchByBuyFuelTransport(this.CurrentBuyFuelTransport);
										commonDAO.SelfDber.Update(this.CurrentBuyFuelTransport);
										//}
										// 判断路线设置
										string nextPlace;
										if (carTransportDAO.CheckNextTruckInFactoryWay(this.CurrentAutotruck.CarType, this.CurrentBuyFuelTransport.StepName, "采样", CommonAppConfig.GetInstance().AppIdentifier, out nextPlace))
										{
											btnSendSamplingPlan.Enabled = true;

											this.CurrentFlowFlag = eFlowFlag.发送计划;
											timer1.Interval = 200;
											UpdateLedShow(this.CurrentAutotruck.CarNumber, "等待驶入");
											this.voiceSpeaker.Speak(this.CurrentAutotruck.CarNumber + " 等待驶入", 1, false);
										}
										else
										{
											UpdateLedShow("路线错误", "禁止通过");
											this.voiceSpeaker.Speak("路线错误 禁止通过 " + (!string.IsNullOrEmpty(nextPlace) ? "请前往" + nextPlace : ""), 1, false);

											timer1.Interval = 20000;
										}
									}
									else
									{
										commonDAO.SelfDber.Delete<CmcsUnFinishTransport>(unFinishTransport.Id);
									}
								}
								else
								{
									this.UpdateLedShow(this.CurrentAutotruck.CarNumber, "未排队");
									this.voiceSpeaker.Speak("车牌号 " + this.CurrentAutotruck.CarNumber + " 未找到排队记录", 1, false);
									timer1.Interval = 20000;
								}
								//}
								//else
								//{
								//	this.UpdateLedShow(this.CurrentAutotruck.CarNumber, "车厢未测量");
								//	this.voiceSpeaker.Speak("车牌号 " + this.CurrentAutotruck.CarNumber + " 车厢未测量", 1, false);

								//	timer1.Interval = 20000;
								//}
							}
							else
							{
								UpdateLedShow(this.CurrentAutotruck.CarNumber, "已停用");
								this.voiceSpeaker.Speak("车牌号 " + this.CurrentAutotruck.CarNumber + " 已停用，禁止通过", 1, false);

								timer1.Interval = 20000;
							}
						}
						else
						{
							UpdateLedShow(this.CurrentImperfectCar.Voucher, "未登记");

							// 方式一：车号识别
							this.voiceSpeaker.Speak("车牌号 " + this.CurrentImperfectCar.Voucher + " 未登记，禁止通过", 1, false);

							timer1.Interval = 20000;
						}

						#endregion
						break;

					case eFlowFlag.发送计划:
						#region

						if (this.SamplerSystemStatus == eEquInfSamplerSystemStatus.就绪待机)
						{
							CmcsRCSampling sampling = carTransportDAO.GetRCSamplingById(this.CurrentBuyFuelTransport.SamplingId);
							if (sampling != null)
							{
								txtSampleCode.Text = sampling.SampleCode;
								if (this.CurrentAutotruck.CarriageLength == 0)
								{
									this.CurrentAutotruck.CarriageTotalLength = 16000;
									this.CurrentAutotruck.CarriageLength = 13000;
									this.CurrentAutotruck.CarriageWidth = 2400;
									this.CurrentAutotruck.CarriageHeight = 1800;
									this.CurrentAutotruck.CarriageBottomToFloor = 1500;
								}

								this.CurrentSampleCMD = new InfQCJXCYSampleCMD()
								{
									MachineCode = this.SamplerMachineCode,
									CarNumber = this.CurrentBuyFuelTransport.CarNumber,
									InFactoryBatchId = this.CurrentBuyFuelTransport.InFactoryBatchId,
									SampleCode = sampling.SampleCode,
									Mt = 0,
									// 根据预报
									TicketWeight = 0,
									// 根据预报
									CarCount = 0,
									// 采样点数根据相关逻辑计算
									PointCount = Convert.ToInt32(dbtxtSampleCount.Value),
									CarriageTotalLength = this.CurrentAutotruck.CarriageTotalLength,
									CarriageLength = this.CurrentAutotruck.CarriageLength,
									CarriageWidth = this.CurrentAutotruck.CarriageWidth,
									CarriageHeight = this.CurrentAutotruck.CarriageHeight,
									CarriageBottomToFloor = this.CurrentAutotruck.CarriageBottomToFloor,
									Obstacle1 = this.CurrentAutotruck.LeftObstacle1.ToString(),
									Obstacle2 = this.CurrentAutotruck.LeftObstacle2.ToString(),
									Obstacle3 = this.CurrentAutotruck.LeftObstacle3.ToString(),
									Obstacle4 = this.CurrentAutotruck.LeftObstacle4.ToString(),
									Obstacle5 = this.CurrentAutotruck.LeftObstacle5.ToString(),
									Obstacle6 = this.CurrentAutotruck.LeftObstacle6.ToString(),
									ResultCode = eEquInfCmdResultCode.默认.ToString(),
									DataFlag = 0
								};

								// 发送采样计划
								if (commonDAO.SelfDber.Insert<InfQCJXCYSampleCMD>(CurrentSampleCMD) > 0)
								{
									this.CurrentFlowFlag = eFlowFlag.等待采样;
									this.UpdateLedShow("等待采样");
									this.voiceSpeaker.Speak("等待采样", 1, false);
								}
							}
							else
							{
								this.UpdateLedShow("未找到采样单信息");
								this.voiceSpeaker.Speak("未找到采样单信息，请联系管理员", 1, false);

								timer1.Interval = 5000;
							}
						}
						else
						{
							this.UpdateLedShow("采样机未就绪");
							this.voiceSpeaker.Speak("采样机未就绪", 1, false);

							timer1.Interval = 5000;
						}

						#endregion
						break;

					case eFlowFlag.等待采样:
						#region

						// 判断采样是否完成
						InfQCJXCYSampleCMD qCJXCYSampleCMD = commonDAO.SelfDber.Get<InfQCJXCYSampleCMD>(this.CurrentSampleCMD.Id);
						if (qCJXCYSampleCMD.ResultCode == eEquInfCmdResultCode.成功.ToString())
						{
							if (jxSamplerDAO.SaveBuyFuelTransport(this.CurrentBuyFuelTransport.Id, DateTime.Now, CommonAppConfig.GetInstance().AppIdentifier))
							{
								this.UpdateLedShow("采样完毕", " 请离开");
								this.voiceSpeaker.Speak("采样完毕，请离开", 1, false);

								this.CurrentFlowFlag = eFlowFlag.等待离开;
							}
						}

						// 降低灵敏度
						timer1.Interval = 4000;

						#endregion
						break;

					case eFlowFlag.等待离开:

						ResetBuyFuel();

						// 降低灵敏度
						timer1.Interval = 4000;

						break;
				}

				RefreshEquStatus();
			}
			catch (Exception ex)
			{
				Log4Neter.Error("timer1_Tick", ex);
			}
			finally
			{
				timer1.Start();
			}

			timer1.Start();
		}

		private void timer2_Tick(object sender, EventArgs e)
		{
			//timer2.Stop();
			// 三分钟执行一次
			//timer2.Interval = 180000;

			try
			{
				// 入厂煤
				LoadTodayUnFinishBuyFuelTransport();
				LoadTodayFinishBuyFuelTransport();
			}
			catch (Exception ex)
			{
				Log4Neter.Error("timer2_Tick", ex);
			}
			finally
			{
				//timer2.Start();
			}
		}

		/// <summary>
		/// 切换手动/自动模式
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void sbtnChangeAutoHandMode_ValueChanged(object sender, EventArgs e)
		{
			this.AutoHandMode = sbtnChangeAutoHandMode.Value;
		}

		#endregion

		#region 入厂煤业务

		/// <summary>
		/// 保存入厂煤运输记录
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnSendSamplingPlan_Click(object sender, EventArgs e)
		{
			if (SendSamplingPlan()) MessageBoxEx.Show("发送失败", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
		}

		/// <summary>
		/// 保存入厂煤运输记录
		/// </summary>
		/// <returns></returns>
		bool SendSamplingPlan()
		{
			if (this.CurrentBuyFuelTransport == null)
			{
				MessageBoxEx.Show("请选择车辆", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return false;
			}

			this.CurrentFlowFlag = eFlowFlag.发送计划;

			return false;
		}

		/// <summary>
		/// 重置入厂煤运输记录
		/// </summary>
		void ResetBuyFuel()
		{
			this.CurrentFlowFlag = eFlowFlag.等待车辆;

			this.CurrentAutotruck = null;
			this.CurrentBuyFuelTransport = null;

			txtSampleCode.ResetText();

			btnSendSamplingPlan.Enabled = false;

			UpdateLedShow("  等待车辆");

			// 最后重置
			this.CurrentImperfectCar = null;
			timer2_Tick(null, null);
		}

		/// <summary>
		/// 重置
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnReset_Click(object sender, EventArgs e)
		{
			ResetBuyFuel();
		}

		/// <summary>
		/// 获取未完成的入厂煤记录
		/// </summary>
		void LoadTodayUnFinishBuyFuelTransport()
		{
			superGridControl1.PrimaryGrid.DataSource = jxSamplerDAO.GetUnFinishBuyFuelTransport();
		}

		/// <summary>
		/// 获取指定日期已完成的入厂煤记录
		/// </summary>
		void LoadTodayFinishBuyFuelTransport()
		{
			superGridControl2.PrimaryGrid.DataSource = jxSamplerDAO.GetFinishedBuyFuelTransport(DateTime.Now.Date, DateTime.Now.Date.AddDays(1));
		}

		#endregion

		#region 基础信息

		/// <summary>
		/// 选择车辆
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnSelectAutotruck_Click(object sender, EventArgs e)
		{
			FrmUnFinishTransport_Select frm = new FrmUnFinishTransport_Select("where CarType='" + eCarType.入厂煤.ToString() + "' order by CreateDate desc");
			if (frm.ShowDialog() == DialogResult.OK)
			{
				passCarQueuer.Enqueue(frm.Output.CarNumber);
				this.CurrentFlowFlag = eFlowFlag.验证车辆;
			}
		}

		#endregion

		#region 其他

		private void superGridControl_BeginEdit(object sender, DevComponents.DotNetBar.SuperGrid.GridEditEventArgs e)
		{
			// 取消进入编辑
			e.Cancel = true;
		}

		/// <summary>
		/// 设置行号
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void superGridControl_GetRowHeaderText(object sender, DevComponents.DotNetBar.SuperGrid.GridGetRowHeaderTextEventArgs e)
		{
			e.Text = (e.GridRow.RowIndex + 1).ToString();
		}

		/// <summary>
		/// Invoke封装
		/// </summary>
		/// <param name="action"></param>
		public void InvokeEx(Action action)
		{
			if (this.IsDisposed || !this.IsHandleCreated) return;

			this.Invoke(action);
		}

		/// <summary>
		/// 更新采样机状态
		/// </summary>
		private void RefreshEquStatus()
		{
			string systemStatus = commonDAO.GetSignalDataValue(this.SamplerMachineCode, eSignalDataName.系统.ToString());
			eEquInfSamplerSystemStatus result;
			if (Enum.TryParse(systemStatus, out result)) SamplerSystemStatus = result;
		}

		#endregion
	}
}
