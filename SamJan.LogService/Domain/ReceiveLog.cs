using System;

namespace SamJan.LogService.Host.Domain
{
    /// <summary>
    /// 接收HL7消息日志
    /// </summary>
    public class ReceiveLog
    {
        /// <summary>
        /// 接收到的HL7消息
        /// </summary>
        public string HL7Message { get; set; }

        /// <summary>
        /// 处理消息错误原因
        /// </summary>
        public string ErrorReason { get; set; }

        /// <summary>
        /// 接收到消息时间
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// 接收消息ID
        /// </summary>
        public string MessageID { get; set; }

        /// <summary>
        /// 接收消息类型
        /// </summary>
        public string MessageType { get; set; }

        /// <summary>
        /// 消息处理状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 发送反馈消息时间
        /// </summary>
        public DateTime SendDate { get; set; }

        /// <summary>
        /// 反馈消息
        /// </summary>
        public string SendMessage { get; set; }

        /// <summary>
        /// 反馈消息类型
        /// </summary>
        public string SendType { get; set; }

        /// <summary>
        /// 患者Id
        /// </summary>
        public string PatientID { get; set; }
    }
}
