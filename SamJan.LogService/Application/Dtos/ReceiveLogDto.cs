using System;

namespace SamJan.LogService.Host.Application.Dtos
{
    public class ReceiveLogDto
    {

        public string HL7Message { get; set; }

        public string ErrorReason { get; set; }

        public DateTime CreateDate { get; set; }

        public string MessageID { get; set; }

        public string MessageType { get; set; }

        public int Status { get; set; }

        public DateTime SendDate { get; set; }

        public string SendMessage { get; set; }

        public string SendType { get; set; }

        public string PatientID { get; set; }
    }
}
