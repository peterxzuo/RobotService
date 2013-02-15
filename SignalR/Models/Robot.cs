using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SignalR.Models
{
    public class Robot
    {
        [Key]
        public int RobotId { get; set; }

        public string ConnectionId { get; set; }

        public string RobotKey { get; set; }

        public bool IsConnected { get; set; }

        public DateTime ConnectedDtUtc { get; set; }

        public DateTime? DisconnectedDtUtc { get; set; }

        public Robot()
        {
            IsConnected = false;
        }
    }
}