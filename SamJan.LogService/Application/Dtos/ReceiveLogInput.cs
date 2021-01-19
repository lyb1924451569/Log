using System;

namespace SamJan.LogService.Host.Application.Dtos
{
    /// <summary>
    /// 接收HL7消息日志
    /// </summary>
    public class ReceiveLogInput
    {
        /// <summary>
        /// 消息ID
        /// </summary>
        public string MessageId { get; set; }

        /// <summary>
        /// 查询起始日期
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 查询结束日期
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 消息类型
        /// </summary>
        public string MessageType { get; set; }
    }
}
