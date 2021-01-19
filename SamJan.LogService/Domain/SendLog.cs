using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SamJan.LogService.Host.Domain
{
    public class SendLog
    {
        public int ID { get; set; }

        [Column(TypeName = "text")]
        public string SendMessage { get; set; }

        [Column(TypeName = "varchar")]
        public string MessageType { get; set; }

        [Column(TypeName = "text")]
        public string ReceiveMessage { get; set; }

        public DateTime SendDate { get; set; }

        public DateTime ReceiveDate { get; set; }

        [Column(TypeName = "varchar")]
        public string MessageID { get; set; }

        [Column(TypeName = "varchar")]
        public string ResponseFlage { get; set; }

        [Column(TypeName = "varchar")]
        public string SendIP { get; set; }

        [Column(TypeName = "varchar")]
        public string OperatorCode { get; set; }

        public string OperatorName { get; set; }

        [Column(TypeName = "varchar")]
        public string PaitentID { get; set; }
    }
}
