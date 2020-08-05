using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
//
using CMCS.Common.Entities;
using CMCS.Common.Entities.BaseInfo;
using CMCS.Common.Entities.CarTransport;
using CMCS.Common.Entities.Fuel;
using CMCS.Common.Entities.iEAA;
using CMCS.Common.Entities.Inf;
using CMCS.Common.Entities.Sys;
using CMCS.Common.Enums;
using CMCS.Common.Utilities;
using CMCS.DapperDber.Dbs.OracleDb;
using CMCS.DapperDber.Util;

namespace CMCS.Common.DAO
{
	/// <summary>
	/// 公共业务
	/// </summary>
	public class CommonDAO
	{
		private static CommonDAO instance;

		public static CommonDAO GetInstance()
		{
			if (instance == null)
			{
				instance = new CommonDAO();
			}

			return instance;
		}

		private CommonDAO()
		{ }

		public OracleDapperDber SelfDber
		{
			get { return Dbers.GetInstance().SelfDber; }
		}

		#region 参数管理

		/// <summary>
		/// 根据名称参数
		/// </summary>
		/// <param name="parameterName">参数名称</param>
		/// <returns></returns>
		public string GetParameterByName(string parameterName)
		{
			Parameter parameter = SelfDber.Entity<Parameter>("where Name=:Name", new { Name = parameterName });
			if (parameter != null) return parameter.Value;
			else return string.Empty;
		}

		#endregion

		#region 编码管理

		/// <summary>
		/// 根据名称获取编码
		/// </summary>
		/// <param name="kindName">编码类别</param>
		/// <returns></returns>
		public List<CodeContent> GetCodeContentByKind(string kindName)
		{
			List<CodeContent> res = new List<CodeContent>();

			CodeKind codeKind = SelfDber.Entity<CodeKind>("where Kind=:Kind", new { Kind = kindName });
			if (codeKind != null) res = SelfDber.Entities<CodeContent>("where KindId=:KindId order by CodeOrder asc", new { KindId = codeKind.Id });

			return res;
		}

		#endregion

		#region 权限判断

		/// <summary>
		/// 判断用户是否有权限
		/// </summary>
		/// <param name="userAccount"></param>
		/// <param name="resourceCode">模块功能编码</param>
		/// <returns></returns>
		public bool HasResourcePowerByResCode(string userAccount, string resourceCode)
		{
			if (string.IsNullOrEmpty(userAccount) || string.IsNullOrEmpty(resourceCode)) return false;
			User user = SelfDber.Query<User>("select t.* from sysamtbuser t inner join sysamtbparty_role a on t.partyid=a.partyid inner join sysamtbpartyrole b on b.id=a.roleid where (b.RoleCode=:RoleCode or b.RoleCode='0000') and t.UserAccount=:UserAccount", new { RoleCode = resourceCode, UserAccount = userAccount }).FirstOrDefault();
			if (user != null) return true;
			return false;
		}

		#endregion

		#region 用户登录

		/// <summary>
		/// 登录验证
		/// </summary>
		/// <param name="roleCode">角色编码</param>
		/// <param name="userAccount"></param>
		/// <param name="password"></param>
		/// <returns></returns>
		public User Login(string roleCode, string userAccount, string password)
		{
			return SelfDber.Query<User>("select t.* from sysamtbuser t inner join sysamtbparty_role a on t.partyid=a.partyid inner join sysamtbpartyrole b on b.id=a.roleid where (b.RoleCode=:RoleCode or b.RoleCode='0000') and t.UserAccount=:UserAccount and t.MDPassword=:MDPassword ", new { RoleCode = roleCode, UserAccount = userAccount, MDPassword = password }).FirstOrDefault();
		}

		/// <summary>
		/// 获取某角色下所有的用户
		/// </summary>
		/// <param name="roleCode">角色编码</param>
		/// <returns></returns>
		public List<User> GetAllSystemUser(string roleCode)
		{
			return SelfDber.Query<User>("select t.* from sysamtbuser t inner join sysamtbparty_role a on t.partyid=a.partyid inner join sysamtbpartyrole b on b.id=a.roleid where b.RoleCode=:RoleCode or b.RoleCode='0000'", new { RoleCode = roleCode }).ToList();
		}

		/// <summary>
		/// 获取管理员
		/// </summary>
		/// <returns></returns>
		public User GetAdminUser()
		{
			return SelfDber.Entity<User>("where UserAccount=:UserAccount", new { UserAccount = GlobalVars.AdminAccount });
		}

		#endregion

		#region 程序配置

		/// <summary>
		/// 获取程序配置
		/// </summary>
		/// <param name="appIdentifier">程序唯一标识</param>
		/// <param name="configName">配置名称</param>
		/// <returns></returns>
		public string GetAppletConfigString(string appIdentifier, string configName)
		{
			CmcsAppletConfig appletConfig = SelfDber.Entity<CmcsAppletConfig>("where AppIdentifier=:AppIdentifier and ConfigName=:ConfigName", new { AppIdentifier = appIdentifier, ConfigName = configName });
			if (appletConfig != null) return appletConfig.ConfigValue;

			return string.Empty;
		}

		/// <summary>
		/// 设置程序配置
		/// </summary>
		/// <param name="appIdentifier">程序唯一标识</param>
		/// <param name="configName">配置名称</param>
		/// <param name="configValue">值</param>
		/// <returns></returns>
		public bool SetAppletConfig(string appIdentifier, string configName, string configValue)
		{
			CmcsAppletConfig appletConfig = SelfDber.Entity<CmcsAppletConfig>("where AppIdentifier=:AppIdentifier and ConfigName=:ConfigName", new { AppIdentifier = appIdentifier, ConfigName = configName });
			if (appletConfig != null)
			{
				appletConfig.ConfigValue = configValue;
				return SelfDber.Update(appletConfig) > 0;
			}
			else
			{
				return SelfDber.Insert(new CmcsAppletConfig()
				{
					AppIdentifier = appIdentifier,
					ConfigName = configName,
					ConfigValue = configValue
				}) > 0;
			}
		}

		/// <summary>
		/// 获取程序配置
		/// </summary> 
		/// <param name="configName">配置名称</param>
		/// <returns></returns>
		public string GetAppletConfigString(string configName)
		{
			return GetAppletConfigString(CommonAppConfig.GetInstance().AppIdentifier, configName);
		}

		/// <summary>
		/// 获取程序配置
		/// </summary>
		/// <param name="configName">配置名称</param>
		/// <returns></returns>
		public bool GetAppletConfigBoolen(string configName)
		{
			try
			{
				return Convert.ToBoolean(GetAppletConfigString(configName));
			}
			catch { return false; }
		}

		/// <summary>
		/// 获取程序配置
		/// </summary>
		/// <param name="configName">配置名称</param>
		/// <returns></returns>
		public int GetAppletConfigInt32(string configName)
		{
			try
			{
				return Convert.ToInt32(GetAppletConfigString(configName));
			}
			catch { return 0; }
		}

		/// <summary>
		/// 获取程序配置
		/// </summary>
		/// <param name="configName">配置名称</param>
		/// <returns></returns>
		public DateTime GetAppletConfigDateTime(string appIdentifier, string configName)
		{
			try
			{
				return Convert.ToDateTime(GetAppletConfigString(configName));
			}
			catch { return DateTime.MinValue; }
		}

		/// <summary>
		/// 获取程序配置
		/// </summary>
		/// <param name="configName">配置名称</param>
		/// <returns></returns>
		public double GetAppletConfigDouble(string configName)
		{
			try
			{
				return Convert.ToDouble(GetAppletConfigString(configName));
			}
			catch { return 0; }
		}

		/// <summary>
		/// 设置程序配置
		/// </summary>
		/// <param name="configName"></param>
		/// <param name="configValue"></param>
		/// <returns></returns>
		public bool SetAppletConfig(string configName, string configValue)
		{
			return SetAppletConfig(CommonAppConfig.GetInstance().AppIdentifier, configName, configValue);
		}

		/// <summary>
		/// 获取公共程序配置
		/// </summary> 
		/// <param name="configName">配置名称</param>
		/// <returns></returns>
		public string GetCommonAppletConfigString(string configName)
		{
			return GetAppletConfigString(GlobalVars.CommonAppletConfigName, configName);
		}

		/// <summary>
		/// 获取公共程序配置
		/// </summary>
		/// <param name="configName">配置名称</param>
		/// <returns></returns>
		public bool GetCommonAppletConfigBoolen(string configName)
		{
			try
			{
				return Convert.ToBoolean(GetCommonAppletConfigString(configName));
			}
			catch { return false; }
		}

		/// <summary>
		/// 获取公共程序配置
		/// </summary>
		/// <param name="configName">配置名称</param>
		/// <returns></returns>
		public int GetCommonAppletConfigInt32(string configName)
		{
			try
			{
				return Convert.ToInt32(GetCommonAppletConfigString(configName));
			}
			catch { return 0; }
		}

		/// <summary>
		/// 获取公共程序配置
		/// </summary>
		/// <param name="configName">配置名称</param>
		/// <returns></returns>
		public DateTime GetCommonAppletConfigDateTime(string appIdentifier, string configName)
		{
			try
			{
				return Convert.ToDateTime(GetCommonAppletConfigString(configName));
			}
			catch { return DateTime.MinValue; }
		}

		/// <summary>
		/// 获取公共程序配置
		/// </summary>
		/// <param name="configName">配置名称</param>
		/// <returns></returns>
		public double GetCommonAppletConfigDouble(string configName)
		{
			try
			{
				return Convert.ToDouble(GetCommonAppletConfigString(configName));
			}
			catch { return 0; }
		}

		/// <summary>
		/// 设置公共程序配置
		/// </summary>
		/// <param name="configName"></param>
		/// <param name="configValue"></param>
		/// <returns></returns>
		public bool SetCommonAppletConfig(string configName, string configValue)
		{
			return SetAppletConfig(GlobalVars.CommonAppletConfigName, configName, configValue);
		}

		#endregion

		#region 运行日志

		/// <summary>
		/// 保存程序运行日志
		/// </summary>
		/// <param name="appletLogLevel">日志等级</param>
		/// <param name="title">日志标题</param>
		/// <param name="content">日志内容</param>
		/// <returns></returns>
		public bool SaveAppletLog(eAppletLogLevel appletLogLevel, string title, string content, string userName)
		{
			return SelfDber.Insert(new CmcsAppletLog()
			{
				AppIdentifier = CommonAppConfig.GetInstance().AppIdentifier,
				Title = title,
				LogLevel = appletLogLevel.ToString(),
				Content = content,
				CreateUser = userName,
				OperUser = userName
			}) > 0;
		}

		#endregion

		#region 实时信号

		/// <summary>
		/// 获取实时信号
		/// </summary> 
		/// <param name="signalPrefix">信号前缀</param>
		/// <param name="signalName">信号名</param>
		/// <returns></returns>
		public string GetSignalDataValue(string signalPrefix, string signalName)
		{
			CmcsSignalData cmcsSignalData = SelfDber.Entity<CmcsSignalData>("where SignalPrefix=:SignalPrefix and SignalName=:SignalName order by UpdateTime desc", new { SignalPrefix = signalPrefix, SignalName = signalName });
			if (cmcsSignalData != null) return cmcsSignalData.SignalValue;

			return string.Empty;
		}

		/// <summary>
		/// 获取实时信号
		/// </summary> 
		/// <param name="signalPrefix">信号前缀</param>
		/// <param name="signalName">信号名</param>
		/// <returns></returns>
		public double GetSignalDataValueDouble(string signalPrefix, string signalName)
		{
			double res = 0;
			Double.TryParse(GetSignalDataValue(signalPrefix, signalName), out res);

			return res;
		}

		/// <summary>
		/// 获取实时信号
		/// </summary> 
		/// <param name="signalPrefix">信号前缀</param>
		/// <param name="signalName">信号名</param>
		/// <returns></returns>
		public int GetSignalDataValueInt32(string signalPrefix, string signalName)
		{
			int res = 0;
			Int32.TryParse(GetSignalDataValue(signalPrefix, signalName), out res);

			return res;
		}

		/// <summary>
		/// 获取实时信号
		/// </summary> 
		/// <param name="signalPrefix">信号前缀</param>
		/// <param name="signalName">信号名</param>
		/// <returns></returns>
		public bool GetSignalDataValueBoolean(string signalPrefix, string signalName)
		{
			Boolean res = false;
			Boolean.TryParse(GetSignalDataValue(signalPrefix, signalName), out res);

			return res;
		}

		/// <summary>
		/// 获取实时信号
		/// </summary> 
		/// <param name="signalPrefix">信号前缀</param>
		/// <param name="signalName">信号名</param>
		/// <returns></returns>
		public DateTime GetSignalDataValueDateTime(string signalPrefix, string signalName)
		{
			DateTime res = DateTime.MinValue;
			DateTime.TryParse(GetSignalDataValue(signalPrefix, signalName), out res);

			return res;
		}

		/// <summary>
		/// 设置实时信号
		/// </summary> 
		/// <param name="signalPrefix">信号前缀</param>
		/// <param name="signalName">信号名</param>
		/// <param name="signalValue">值</param>
		/// <returns></returns>
		public bool SetSignalDataValue(string signalPrefix, string signalName, string signalValue)
		{
			CmcsSignalData cmcsSignalData = SelfDber.Entity<CmcsSignalData>("where SignalPrefix=:SignalPrefix and SignalName=:SignalName order by UpdateTime desc", new { SignalPrefix = signalPrefix, SignalName = signalName });
			if (cmcsSignalData == null)
			{
				SelfDber.Insert(new CmcsSignalData
				{
					SignalPrefix = signalPrefix,
					SignalName = signalName,
					SignalValue = signalValue,
					UpdateTime = DateTime.Now
				});
			}

			return SelfDber.Execute("update " + EntityReflectionUtil.GetTableName<CmcsSignalData>() + " set SignalValue=:SignalValue,UpdateTime=sysdate where SignalPrefix=:SignalPrefix and  SignalName=:SignalName", new { SignalPrefix = signalPrefix, SignalName = signalName, SignalValue = signalValue }) > 0;
		}

		#endregion

		#region 设备管理

		/// <summary>
		/// 根据设备编码获取设备
		/// </summary>
		/// <param name="machineCode">设备编码</param>
		/// <returns></returns>
		public CmcsCMEquipment GetCMEquipmentByMachineCode(string machineCode)
		{
			return SelfDber.Entity<CmcsCMEquipment>("where EquipmentCode=:EquipmentCode", new { EquipmentCode = machineCode });
		}

		/// <summary>
		/// 根据设备编码获取设备名称
		/// </summary>
		/// <param name="machineCode">设备编码</param>
		/// <returns></returns>
		public string GetMachineNameByCode(string machineCode)
		{
			CmcsCMEquipment entity = GetCMEquipmentByMachineCode(machineCode);
			return entity != null ? entity.EquipmentName : string.Empty;
		}

		/// <summary>
		/// 根据设备编码获取接口类型
		/// </summary>
		/// <param name="machineCode">设备编码</param>
		/// <returns></returns>
		public string GetMachineInterfaceTypeByCode(string machineCode)
		{
			CmcsCMEquipment entity = GetCMEquipmentByMachineCode(machineCode);
			return entity != null ? entity.InterfaceType : machineCode;
		}

		/// <summary>
		/// 根据父节点设备编码获取所有子设备集合
		/// </summary>
		/// <param name="machineCode">设备编码</param>
		/// <returns></returns>
		public List<CmcsCMEquipment> GetChildrenMachinesByCode(string machineCode)
		{
			List<CmcsCMEquipment> list = new List<CmcsCMEquipment>();

			CmcsCMEquipment entity = GetCMEquipmentByMachineCode(machineCode);
			if (entity != null) list = SelfDber.Entities<CmcsCMEquipment>(" where ParentId=:ParentId  order by Sequence asc", new { ParentId = entity.Id });

			return list;
		}

		#endregion

		#region 第三方设备接口

		/// <summary>
		/// 获取未读第三方设备故障信息
		/// </summary>
		/// <param name="machineCode">设备编码</param>
		/// <returns></returns>
		public List<InfEquInfHitch> GetUnReadEquInfHitchs(string machineCode)
		{
			List<InfEquInfHitch> res = SelfDber.Entities<InfEquInfHitch>("where MachineCode=:machineCode and DataFlag=0", new { MachineCode = machineCode });
			SelfDber.Execute("update " + EntityReflectionUtil.GetTableName<InfEquInfHitch>() + " set DataFlag=1 where MachineCode=:machineCode and DataFlag=0", new { MachineCode = machineCode });
			return res;
		}

		/// <summary>
		/// 获取当日第三方设备故障信息
		/// </summary>
		/// <param name="machineCode">设备编码</param>
		/// <param name="dtTime">时间</param>
		/// <returns></returns>
		public List<InfEquInfHitch> GetEquInfHitchsByTime(string machineCode, DateTime dtTime)
		{
			List<InfEquInfHitch> res = SelfDber.Entities<InfEquInfHitch>("where MachineCode=:machineCode and HitchTime like '%" + dtTime.ToString("yyyy-MM-dd") + "%' order by HitchTime desc", new { MachineCode = machineCode });
			return res;
		}

		/// <summary>
		/// 保存第三方设备接口 - 实时集样罐表
		/// </summary>
		/// <param name="equInfSampleBarrel"></param>
		/// <returns></returns>
		public bool SaveEquInfSampleBarrel(InfEquInfSampleBarrel equInfSampleBarrel)
		{
			InfEquInfSampleBarrel oldEquInfSampleBarrel = SelfDber.Entity<InfEquInfSampleBarrel>("where InterfaceType=:InterfaceType and MachineCode=:MachineCode and BarrelNumber=:BarrelNumber and BarrelType=:BarrelType"
				, new { InterfaceType = equInfSampleBarrel.InterfaceType, MachineCode = equInfSampleBarrel.MachineCode, BarrelNumber = equInfSampleBarrel.BarrelNumber, BarrelType = equInfSampleBarrel.BarrelType });

			if (oldEquInfSampleBarrel == null)
				return SelfDber.Insert(equInfSampleBarrel) > 0;
			else
			{
				oldEquInfSampleBarrel.BarrelNumber = equInfSampleBarrel.BarrelNumber;
				oldEquInfSampleBarrel.BarrelStatus = equInfSampleBarrel.BarrelStatus;
				oldEquInfSampleBarrel.InFactoryBatchId = equInfSampleBarrel.InFactoryBatchId;
				oldEquInfSampleBarrel.IsCurrent = equInfSampleBarrel.IsCurrent;
				oldEquInfSampleBarrel.SampleCode = equInfSampleBarrel.SampleCode;
				oldEquInfSampleBarrel.SampleCount = equInfSampleBarrel.SampleCount;
				oldEquInfSampleBarrel.UpdateTime = equInfSampleBarrel.UpdateTime;
				oldEquInfSampleBarrel.DataFlag = equInfSampleBarrel.DataFlag;

				return SelfDber.Update(oldEquInfSampleBarrel) > 0;
			}
		}

		/// <summary>
		/// 保持第三方设备接口 - 故障信息表
		/// </summary> 
		/// <param name="machineCode">设备编码</param>
		/// <param name="hitchTime">故障时间</param>
		/// <param name="hitchDescribe">故障描述</param>
		/// <returns></returns>
		public bool SaveEquInfHitch(string machineCode, DateTime hitchTime, string hitchDescribe)
		{
			return SelfDber.Insert(new InfEquInfHitch
			{
				DataFlag = 0,
				HitchDescribe = hitchDescribe,
				HitchTime = hitchTime,
				InterfaceType = GetMachineInterfaceTypeByCode(machineCode),
				MachineCode = machineCode
			}) > 0;
		}

		#endregion

		#region 系统消息弹框

		/// <summary>
		/// 获取当日未读取的异常信息，按先进先出、同一设备异常分组的原则,
		/// </summary>
		/// <returns></returns>
		public List<InfEquInfHitch> GetWarnEquInfHitch()
		{
			List<InfEquInfHitch> cmcsequinfhitch = SelfDber.Entities<InfEquInfHitch>(" where IsRead=0 and HitchTime like '%" + DateTime.Now.ToString("yyyy-MM-dd") + "%' order by HitchTime ");
			if (cmcsequinfhitch.Count > 0)
				return cmcsequinfhitch.GroupBy(a => a.MachineCode).First().ToList();
			else
				return new List<InfEquInfHitch>();
		}

		/// <summary>
		/// 根据异常时间查询异常信息
		/// </summary>
		/// <param name="dtStart"></param>
		/// <param name="dtEnd"></param>
		/// <returns></returns>
		public List<InfEquInfHitch> GetCmcsEquInfHitchs(DateTime dtStart, DateTime dtEnd)
		{
			List<InfEquInfHitch> equinfhitchs = SelfDber.Entities<InfEquInfHitch>(" where HitchTime>='" + dtStart + "' and HitchTime<='" + dtEnd + "' order by HitchTime ");
			return equinfhitchs;
		}

		/// <summary>
		/// 将异常信息值为已读
		/// </summary>
		/// <param name="EquInfHitchId"></param>
		public void UpdateReadEquInfHitch(string EquInfHitchId)
		{
			SelfDber.Execute(" update " + EntityReflectionUtil.GetTableName<InfEquInfHitch>() + " t set t.isread=1 where t.id='" + EquInfHitchId + "' ");
		}

		/// <summary>
		/// 获取当日未读取的管控系统消息
		/// </summary>
		/// <returns></returns>
		public CmcsSysMessage GetTodayTopSysMessage()
		{
			return SelfDber.Entity<CmcsSysMessage>("where MsgStatus=:MsgStatus and MsgTime like '%' || :MsgTime || '%' order by MsgTime", new { MsgStatus = eSysMessageStatus.默认.ToString(), MsgTime = DateTime.Now.ToString("yyyy-MM-dd") });
		}

		/// <summary>
		/// 更改系统消息的状态
		/// </summary>
		/// <param name="sysMessageId"></param>
		/// <param name="sysMessageStatus"></param>
		public void ChangeSysMessageStatus(string sysMessageId, eSysMessageStatus sysMessageStatus)
		{
			SelfDber.Execute("update " + EntityReflectionUtil.GetTableName<CmcsSysMessage>() + " t set t.MsgStatus=:MsgStatus where t.Id=:Id", new { Id = sysMessageId, MsgStatus = sysMessageStatus.ToString() });
		}

		/// <summary>
		/// 将所有上次提醒数据置换为已处理
		/// </summary>
		public void ResetAllSysMessageStatus()
		{
			SelfDber.Execute("update " + EntityReflectionUtil.GetTableName<CmcsSysMessage>() + " t set t.MsgStatus=:MsgStatus1 where t.MsgStatus=:MsgStatus1", new { MsgStatus1 = eSysMessageStatus.处理中.ToString(), MsgStatus2 = eSysMessageStatus.处理中.ToString() });
		}

		/// <summary>
		/// 添加提示信息
		/// </summary>
		/// <param name="msgCode">编码</param>
		/// <param name="msgContent">内容</param>
		/// <param name="windowTitle">标题</param>
		/// <param name="msgButton">按钮名称</param>
		/// <param name="msgWarnType">是否右下角弹出</param>
		/// <param name="isAutoClose">是否自动关闭</param>
		/// <param name="msgParam">JSON</param>
		/// <returns></returns>
		public bool SaveSysMessage(String msgCode, String msgContent, String windowTitle = "提示", String msgButton = "查看|取消", bool msgWarnType = true, bool isAutoClose = false, String msgParam = "")
		{
			if (!HasSysMessage(msgCode, msgContent))
			{
				return SelfDber.Insert(new CmcsSysMessage
				{
					MsgTime = DateTime.Now,
					MsgParam = msgParam,
					MsgContent = msgContent,
					WindowsTitle = windowTitle,
					MsgWarnType = msgWarnType ? 1 : 0,
					IsAutoClose = isAutoClose ? 1 : 0,
					MsgCode = msgCode,
					MsgButton = msgButton,
				}) > 0;
			}

			return false;
		}

		/// <summary>
		/// 是否存在未处理的数据
		/// </summary>
		/// <param name="msgCode">编码</param>
		/// <param name="msgContent">内容</param>
		/// <returns></returns>
		public bool HasSysMessage(String msgCode, String msgContent)
		{
			return SelfDber.Count<CmcsSysMessage>("where (MsgStatus=:MsgStatus1 or MsgStatus=:MsgStatus2) and MsgContent=:MsgContent and MsgCode=:MsgCode", new { MsgStatus1 = eSysMessageStatus.处理中.ToString(), MsgStatus2 = eSysMessageStatus.处理中.ToString(), MsgContent = msgContent, MsgCode = msgCode }) > 0;
		}

		#endregion

		#region 基础信息

		/// <summary>
		/// 根据供煤单位名称或首字母模糊查询数据
		/// </summary>
		/// <param name="name"></param>
		/// <param name="sqlWhere">条件语句</param>
		/// <returns></returns>
		public List<CmcsSupplier> GetSupplierByNameOrChs(string name, string sqlWhere)
		{
			List<CmcsSupplier> res = SelfDber.Entities<CmcsSupplier>(sqlWhere);

			if (!string.IsNullOrEmpty(name))
			{
				return res.Where(a =>
				{
					if (a.Name.ToUpper().Contains(name.ToUpper())) return true;
					if (ChsSpeller.GetFirst(a.Name).ToUpper().Contains(name.ToUpper())) return true;

					return false;
				}).ToList();
			}
			else
				return res;
		}

		/// <summary>
		/// 根据矿点名称或首字母模糊查询数据
		/// </summary>
		/// <param name="name"></param>
		/// <param name="sqlWhere">条件语句</param>
		/// <returns></returns>
		public List<CmcsMine> GetMineByNameOrChs(string name, string sqlWhere)
		{
			List<CmcsMine> res = SelfDber.Entities<CmcsMine>(sqlWhere);

			if (!string.IsNullOrEmpty(name))
			{
				return res.Where(a =>
				{
					if (a.Name.ToUpper().Contains(name.ToUpper())) return true;
					if (ChsSpeller.GetFirst(a.Name).ToUpper().Contains(name.ToUpper())) return true;

					return false;
				}).ToList();
			}
			else
				return res;
		}

		/// <summary>
		/// 根据运输单位名称或首字母模糊查询数据
		/// </summary>
		/// <param name="name"></param>
		/// <param name="sqlWhere">条件语句</param>
		/// <returns></returns>
		public List<CmcsTransportCompany> GetTransportCompanyByNameOrChs(string name, string sqlWhere)
		{
			List<CmcsTransportCompany> res = SelfDber.Entities<CmcsTransportCompany>(sqlWhere);

			if (!string.IsNullOrEmpty(name))
			{
				return res.Where(a =>
				{
					if (a.Name.ToUpper().Contains(name.ToUpper())) return true;
					if (ChsSpeller.GetFirst(a.Name).ToUpper().Contains(name.ToUpper())) return true;

					return false;
				}).ToList();
			}
			else
				return res;
		}

		/// <summary>
		/// 根据供货收货单位名称或首字母模糊查询数据
		/// </summary>
		/// <param name="name"></param>
		/// <param name="sqlWhere">条件语句</param>
		/// <returns></returns>
		public List<CmcsSupplyReceive> GetSupplyReceiveByNameOrChs(string name, string sqlWhere)
		{
			List<CmcsSupplyReceive> res = SelfDber.Entities<CmcsSupplyReceive>(sqlWhere);

			if (!string.IsNullOrEmpty(name))
			{
				return res.Where(a =>
				{
					if (a.UnitName.ToUpper().Contains(name.ToUpper())) return true;
					if (ChsSpeller.GetFirst(a.UnitName).ToUpper().Contains(name.ToUpper())) return true;

					return false;
				}).ToList();
			}
			else
				return res;
		}

		#region 煤种
		/// <summary>
		/// 获取煤种编码
		/// </summary>
		/// <param name="fuelCode"></param>
		/// <returns></returns>
		public string GetFuelKindNewChildCode(string fuelCode)
		{
			string strNewCode = fuelCode;

			for (int i = 1; i < 100; i++)
			{
				strNewCode = fuelCode + i.ToString().PadLeft(2, '0');
				//判断该编码是否已经存在
				if (!IsExistFuelKindCode(strNewCode)) break;
			}
			return strNewCode;
		}

		/// <summary>
		/// 获取煤种顺序号
		/// </summary>
		/// <param name="fuelCode"></param>
		/// <returns></returns>
		public int GetFuelKindNewSort(CmcsFuelKind fuelKind)
		{
			int Sequence = 1;
			CmcsFuelKind kind = Dbers.GetInstance().SelfDber.Entity<CmcsFuelKind>("where ParentId=:ParentId order by Sequence desc", new { ParentId = fuelKind.Id });
			if (kind != null)
				Sequence = kind.Sequence + 1;
			return Sequence;
		}

		/// <summary>
		/// 判断煤种编码是否存在
		/// </summary>
		/// <param name="fuelCode">煤种编码</param>
		/// <returns></returns>
		public bool IsExistFuelKindCode(string code)
		{
			CmcsFuelKind entity = Dbers.GetInstance().SelfDber.Entity<CmcsFuelKind>("where Code=:code", new { Code = code });
			if (entity != null)
				return true;
			return false;
		}

		/// <summary>
		/// 判断煤种名称是否存在
		/// </summary>
		/// <param name="Name">煤种名称</param>
		/// <returns></returns>
		public bool IsExistFuelKindName(string name, string id)
		{
			CmcsFuelKind entity = Dbers.GetInstance().SelfDber.Entity<CmcsFuelKind>("where Name=:Name and Id<>:Id", new { Name = name, Id = id });
			if (entity != null)
				return true;
			return false;
		}
		#endregion

		#region 矿点
		/// <summary>
		/// 获取矿点编码
		/// </summary>
		/// <param name="code"></param>
		/// <returns></returns>
		public string GetMineNewChildCode(string code)
		{
			string strNewCode = code;

			for (int i = 1; i < 100; i++)
			{
				strNewCode = code + i.ToString().PadLeft(2, '0');
				//判断该编码是否已经存在
				if (!IsExistMineCode(strNewCode)) break;
			}
			return strNewCode;
		}

		/// <summary>
		/// 获取矿点顺序号
		/// </summary>
		/// <param name="fuelCode"></param>
		/// <returns></returns>
		public int GetMineNewSort(CmcsMine mine)
		{
			int Sequence = 1;
			CmcsMine kind = Dbers.GetInstance().SelfDber.Entity<CmcsMine>("where ParentId=:ParentId order by Sequence desc", new { ParentId = mine.Id });
			if (kind != null)
				Sequence = kind.Sequence + 1;
			return Sequence;
		}

		/// <summary>
		/// 判断煤种编码是否存在
		/// </summary>
		/// <param name="fuelCode">煤种编码</param>
		/// <returns></returns>
		public bool IsExistMineCode(string code)
		{
			CmcsMine entity = Dbers.GetInstance().SelfDber.Entity<CmcsMine>("where Code=:code", new { Code = code });
			if (entity != null)
				return true;
			return false;
		}

		/// <summary>
		/// 判断煤种名称是否存在
		/// </summary>
		/// <param name="Name">煤种名称</param>
		/// <returns></returns>
		public bool IsExistMineName(string name, string id)
		{
			CmcsMine entity = Dbers.GetInstance().SelfDber.Entity<CmcsMine>("where Name=:Name and Id<>:Id", new { Name = name, Id = id });
			if (entity != null)
				return true;
			return false;
		}
		#endregion

		#endregion

		#region 车辆管理

		/// <summary>
		/// 根据车牌号获取车辆信息
		/// </summary>
		/// <param name="carNumber"></param>
		/// <returns></returns>
		public CmcsAutotruck GetAutotruckByCarNumber(string carNumber)
		{
			return SelfDber.Entity<CmcsAutotruck>("where CarNumber=:CarNumber", new { CarNumber = carNumber });
		}

		/// <summary>
		/// 根据标签卡号获取车辆信息
		/// </summary>
		/// <param name="carNumber"></param>
		/// <returns></returns>
		public CmcsAutotruck GetAutotruckByTagId(string tagId)
		{
			CmcsEPCCard ePCCard = SelfDber.Entity<CmcsEPCCard>("where TagId=:TagId", new { TagId = tagId });
			if (ePCCard != null) return SelfDber.Entity<CmcsAutotruck>("where EPCCardId=:EPCCardId", new { EPCCardId = ePCCard.Id });

			return null;
		}

		#endregion

		#region 程序远程控制

		/// <summary>
		/// 发送程序远程控制命令
		/// </summary>
		/// <param name="appIdentifier">程序唯一标识符</param>
		/// <param name="cmdCode">命令代码</param>
		/// <param name="param">参数</param>
		/// <returns></returns>
		public bool SendAppRemoteControlCmd(string appIdentifier, string cmdCode, string param = "")
		{
			return SelfDber.Insert(new CmcsAppRemoteControlCmd
			{
				AppIdentifier = appIdentifier,
				CmdCode = cmdCode,
				Param = param,
				ResultCode = eEquInfCmdResultCode.默认.ToString(),
				DataFlag = 0
			}) > 0;
		}

		/// <summary>
		/// 重置程序远程控制命令
		/// </summary>
		/// <param name="appIdentifier"></param>
		public void ResetAppRemoteControlCmd(string appIdentifier)
		{
			SelfDber.Execute("update " + EntityReflectionUtil.GetTableName<CmcsAppRemoteControlCmd>() + " set DataFlag=1 where AppIdentifier=:AppIdentifier", new { AppIdentifier = appIdentifier });
		}

		/// <summary>
		/// 获取最新的程序远程控制命令
		/// </summary>
		/// <param name="appIdentifier">程序唯一标识</param>
		/// <returns></returns>
		public CmcsAppRemoteControlCmd GetNewestAppRemoteControlCmd(string appIdentifier)
		{
			return SelfDber.Entity<CmcsAppRemoteControlCmd>("where AppIdentifier=:AppIdentifier and DataFlag=0 order by CreateDate asc", new { AppIdentifier = appIdentifier });
		}

		/// <summary>
		/// 获取最新的程序远程控制命令
		/// </summary>
		/// <param name="appIdentifier">程序唯一标识</param>
		/// <returns></returns>
		public CmcsAppRemoteControlCmd GetNewestAppRemoteControlCmd(string appIdentifier, string cmdCode)
		{
			return SelfDber.Entity<CmcsAppRemoteControlCmd>("where AppIdentifier=:AppIdentifier and CmdCode=:CmdCode order by CreateDate asc", new { AppIdentifier = appIdentifier, CmdCode = cmdCode });
		}

		/// <summary>
		/// 更改程序远程控制命令的执行结果
		/// </summary>
		/// <param name="appRemoteControlCmd"></param>
		/// <param name="cmdResultCode"></param>
		public void SetAppRemoteControlCmdResultCode(CmcsAppRemoteControlCmd appRemoteControlCmd, eEquInfCmdResultCode cmdResultCode)
		{
			appRemoteControlCmd.DataFlag = 1;
			appRemoteControlCmd.ResultCode = cmdResultCode.ToString();
			SelfDber.Update(appRemoteControlCmd);
		}

		#endregion

		#region 入厂煤批次

		/// <summary>
		/// 生成制定日期的批次编码
		/// </summary>
		/// <param name="prefix">前缀</param>
		/// <param name="dt">实际到厂时间</param>
		/// <returns></returns>
		public string CreateNewBatchNumber(string prefix, DateTime dt)
		{
			CmcsInFactoryBatch entity = SelfDber.Entity<CmcsInFactoryBatch>("where Batch like '%-'||to_char(:CreateDate,'YYYYMMDD')||'-%' and Batch like :Prefix || '%' and IsDeleted=0 order by Batch desc", new { CreateDate = dt, Prefix = prefix + "-" + dt.ToString("yyyyMMdd") });

			if (entity == null)
				return string.Format("{0}-{1}-01", prefix, dt.ToString("yyyyMMdd"));
			else
				return string.Format("{0}-{1}-{2}", prefix, dt.ToString("yyyyMMdd"), (Convert.ToInt16(entity.Batch.Replace(string.Format("{0}-{1}-", prefix, dt.ToString("yyyyMMdd")), "")) + 1).ToString().PadLeft(2, '0'));
		}

		#endregion

		#region 采制化三级编码

		/// <summary>
		/// 生成采样码，分段001-099
		/// </summary> 
		/// <param name="dt"></param>
		/// <returns></returns>
		public string CreateNewSampleCode(DateTime dt)
		{
			string res = string.Empty;
			do
			{
				res = string.Format("{0}{1}", "CY" + dt.ToString("yyMMdd"), new Random().Next(0, 100).ToString().PadLeft(3, '0'));
			} while (SelfDber.Count<CmcsRCSampling>("where SampleCode=:SampleCode and IsDeleted=0", new { SampleCode = res }) > 0);

			return res;
		}

		/// <summary>
		/// 生成采样次码
		/// </summary>
		/// <param name="sampleId"></param>
		/// <returns></returns>
		public string CreateSampleDetailCode(string sampleCode)
		{
			string res = string.Empty;
			int count = 1;
			do
			{
				res = string.Format("{0}-{1}", sampleCode, count.ToString().PadLeft(2, '0'));
				count++;
			} while (Dbers.GetInstance().SelfDber.Count<CmcsRCSampleBarrel>("where SampseCondCode=:SampseCondCode", new { SampseCondCode = res }) > 0);

			return res;
		}

		/// <summary>
		/// 生成制样码，分段100-199
		/// </summary>
		/// <returns></returns>
		public string CreateNewMakeCode(DateTime dt)
		{
			string res = string.Empty;
			do
			{
				res = string.Format("{0}{1}", "ZY" + dt.ToString("yyMMdd"), new Random().Next(100, 200).ToString().PadLeft(3, '0'));
			} while (SelfDber.Count<CmcsRCMake>("where MakeCode=:MakeCode and IsDeleted=0", new { MakeCode = res }) > 0);

			return res;
		}

		/// <summary>
		/// 根据制样码生成制样明细样品码
		/// </summary>
		/// <returns></returns>
		public string CreateNewMakeBarrelCodeByMakeCode(string makeCode, string makeType)
		{
			string res = string.Empty;

			switch (makeType.ToUpper())
			{
				case "0.2MM分析样":
					res = "021";
					break;
				case "0.2MM存查样":
					res = "022";
					break;
				case "3MM备查样":
					res = "031";
					break;
				case "6MM全水样":
					res = "061";
					break;
				default:
					break;
			}
			res = string.Format("{0}{1}", makeCode, res);

			return res;
		}

		/// <summary>
		/// 生成制样明细样品码，分段300-499
		/// </summary>
		/// <returns></returns>
		public string CreateNewMakeBarrelCode(DateTime dt)
		{
			string res = string.Empty;
			do
			{
				res = string.Format("{0}{1}", dt.ToString("yyMMdd"), new Random().Next(300, 500).ToString().PadLeft(3, '0'));
			} while (SelfDber.Count<CmcsRCMakeDetail>("where SampleCode=:SampleCode and IsDeleted=0", new { SampleCode = res }) > 0);

			return res;
		}

		/// <summary>
		/// 生成化验码，分段200-299
		/// </summary>
		/// <returns></returns>
		public string CreateNewAssayCode(DateTime dt)
		{
			string res = string.Empty;
			do
			{
				res = string.Format("{0}{1}", "HY" + dt.ToString("yyMMdd"), new Random().Next(200, 300).ToString().PadLeft(3, '0'));
			} while (SelfDber.Count<CmcsRCAssay>("where AssayCode=:AssayCode and IsDeleted=0", new { AssayCode = res }) > 0);

			return res;
		}

		/// <summary>
		/// 创建采制化三级数据
		/// </summary>
		/// <param name="inFactoryBatch">批次</param>
		/// <param name="samplingType">采样方式</param>
		/// <param name="remark">备注</param>
		/// <param name="assayType">化验方式</param>
		/// <returns></returns>
		public CmcsRCSampling GCSamplingMakeAssay(CmcsInFactoryBatch inFactoryBatch, string samplingType, string remark)
		{
			bool isSuccess = false;

			// 入厂煤采样
			CmcsRCSampling rCSampling = SelfDber.Entity<CmcsRCSampling>("where InFactoryBatchId=:InFactoryBatchId and SamplingType=:SamplingType and IsDeleted=0", new { InFactoryBatchId = inFactoryBatch.Id, SamplingType = samplingType });
			if (rCSampling == null)
			{
				rCSampling = new CmcsRCSampling()
				{
					InFactoryBatchId = inFactoryBatch.Id,
					SamplingType = samplingType,
					SamplingDate = inFactoryBatch.FactArriveDate,
					SamplingPle = "自动",
					SampleCode = CreateNewSampleCode(inFactoryBatch.FactArriveDate),
					Remark = remark
				};

				isSuccess = SelfDber.Insert(rCSampling) > 0;
			}

			// 入厂煤制样
			CmcsRCMake rCMake = SelfDber.Entity<CmcsRCMake>("where SamplingId=:SamplingId and IsDeleted=0", new { SamplingId = rCSampling.Id });
			if (rCMake == null)
			{
				rCMake = new CmcsRCMake()
				{
					SamplingId = rCSampling.Id,
					MakeType = "三级编码制样",
					MakeStyle = "机器制样",
					MakeDate = rCSampling.SamplingDate,
					MakeCode = CreateNewMakeCode(rCSampling.SamplingDate),
					MakePle = "自动",
					Remark = remark
				};

				isSuccess = SelfDber.Insert(rCMake) > 0;
			}

			//入厂煤制样明细  
			foreach (CodeContent item in CommonDAO.GetInstance().GetCodeContentByKind("样品类型"))
			{
				CmcsRCMakeDetail rCMakeDetail = SelfDber.Entity<CmcsRCMakeDetail>("where MakeId=:MakeId and SampleType=:SampleType", new { MakeId = rCMake.Id, SampleType = item.Content });
				if (rCMakeDetail == null)
				{
					rCMakeDetail = new CmcsRCMakeDetail();
					rCMakeDetail.MakeId = rCMake.Id;
					rCMakeDetail.SampleCode = CreateNewMakeBarrelCodeByMakeCode(rCMake.MakeCode, item.Content);
					rCMakeDetail.SampleType = item.Content;
					isSuccess = SelfDber.Insert(rCMakeDetail) > 0;
				}
			}

			// 入厂煤化验
			CmcsRCAssay rCAssay = SelfDber.Entity<CmcsRCAssay>("where MakeId=:MakeId and IsDeleted=0", new { MakeId = rCMake.Id });
			if (rCAssay == null)
			{
				// 入厂煤煤质

				CmcsFuelQuality fuelQuality = new CmcsFuelQuality()
				{
					Id = Guid.NewGuid().ToString()
				};

				if (SelfDber.Insert(fuelQuality) > 0)
				{
					rCAssay = new CmcsRCAssay()
					{
						MakeId = rCMake.Id,
						AssayCode = CreateNewAssayCode(rCMake.CreateDate),
						InFactoryBatchId = inFactoryBatch.Id,
						FuelQualityId = fuelQuality.Id,
						AssayDate = rCMake.MakeDate,
						Remark = remark,
						IsAssay = 0,
						BackBatchDate = inFactoryBatch.BackBatchDate,
					};

					isSuccess = SelfDber.Insert(rCAssay) > 0;
				}
			}

			return rCSampling;
		}

		/// <summary>
		/// 根据采样单Id获取批次
		/// </summary>
		/// <returns></returns>
		/// <param name="rCSamplingId">采样单Id</param>
		public CmcsInFactoryBatch GetBatchByRCSamplingId(string rCSamplingId)
		{
			CmcsRCSampling rCSampling = SelfDber.Get<CmcsRCSampling>(rCSamplingId);
			if (rCSampling != null) return SelfDber.Get<CmcsInFactoryBatch>(rCSampling.InFactoryBatchId);

			return null;
		}

		/// <summary>
		/// 根据批次id获取采样单明细
		/// </summary>
		/// <param name="batchId"></param>
		/// <returns></returns>
		public List<CmcsRCSampling> GetSamplings(string batchId)
		{
			return Dbers.GetInstance().SelfDber.Entities<CmcsRCSampling>("where InfactoryBatchId=:InfactoryBatchId and IsDeleted=0 order by SamplingDate asc", new { InfactoryBatchId = batchId });
		}
		#endregion

		#region 事件管理
		/// <summary>
		/// 添加事件
		/// </summary>
		/// <param name="eventCode"></param>
		/// <param name="objectId"></param>
		/// <returns></returns>
		public bool SaveHandleEvent(string eventCode, string objectId)
		{
			CmcsWaitForHandleEvent entity = new CmcsWaitForHandleEvent();
			entity.EventCode = eventCode;
			entity.ObjectId = objectId;

			return SelfDber.Insert(entity) > 0;
		}
		#endregion


		/// <summary>  
		/// Excel数据导入Datable  
		/// </summary>  
		/// <param name="fileUrl"></param>  
		/// <param name="table"></param>  
		/// <returns></returns>  
		public System.Data.DataTable GetExcelDatatable(string fileUrl, string table)
		{
			//office2007之前 仅支持.xls  
			//支持.xls和.xlsx，即包括office2010等版本的 HDR=Yes代表第一行是标题，不是数据；  
			System.Data.DataTable dt = null;
			//建立连接  
			OleDbConnection conn = new OleDbConnection(string.Format("Provider=Microsoft.Ace.OleDb.12.0;Data Source={0};Extended Properties='Excel 12.0; HDR=Yes; IMEX=1'", fileUrl));
			try
			{
				//打开连接  
				if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
				{
					conn.Open();
				}

				System.Data.DataTable schemaTable = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
				//获取Excel的第一个Sheet名称  
				string sheetName = schemaTable.Rows[0]["TABLE_NAME"].ToString().Trim();
				//查询sheet中的数据  
				string strSql = "select * from [" + sheetName + "]";
				OleDbDataAdapter da = new OleDbDataAdapter(strSql, conn);
				DataSet ds = new DataSet();
				da.Fill(ds, table);
				dt = ds.Tables[0];
				return dt;
			}
			catch (Exception exc)
			{
				throw exc;
			}
			finally
			{
				conn.Close();
				conn.Dispose();
			}
		}

	}
}
