using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;


namespace SignalR.Models
{
    public class RobotDbContext : DbContext
    {
        public RobotDbContext() : base("RobotServiceConnection")
        {

        }

        public DbSet<Robot> Robots { get; set; }

        public void OnRobotDisconnected(string connectionId)
        {
            var robots = from r in Robots
                         where connectionId == r.ConnectionId
                         select r;

            foreach (var robot in robots)
            {
                robot.IsConnected = false;
                robot.DisconnectedDtUtc = DateTime.UtcNow;
            }

            SaveChanges();
        }

        public void OnRobotconnected(string connectionId)
        {
            var robots = from r in Robots
                         where connectionId == r.ConnectionId
                         select r;

            if (robots.Count() > 0)
            {
                foreach (var robot in robots)
                {
                    robot.IsConnected = true;
                    robot.DisconnectedDtUtc = null;
                }
            }
            else
            {
                Robot robot = new Robot()
                {
                    ConnectionId = connectionId,
                    ConnectedDtUtc = DateTime.UtcNow,
                    DisconnectedDtUtc = null,
                    IsConnected = true,
                    RobotKey = null
                };

                Robots.Add(robot);
            }

            SaveChanges();
        }

        public void OnRobotRegistered(string connectionId, string robotKey)
        {
            var robots = from r in Robots
                         where connectionId == r.ConnectionId
                         select r;

            if (robots.Count() > 0)
            {
                foreach (var robot in robots)
                {
                    robot.IsConnected = true;
                    robot.DisconnectedDtUtc = null;
                    robot.RobotKey = robotKey;
                }
            }
            else
            {
                Robot robot = new Robot()
                {
                    ConnectionId = connectionId,
                    ConnectedDtUtc = DateTime.UtcNow,
                    DisconnectedDtUtc = null,
                    IsConnected = true,
                    RobotKey = robotKey
                };

                Robots.Add(robot);
            }

            SaveChanges();
        }

        public List<Robot> GetConnectedRobots(string robotKey)
        {
            return (from r in Robots
                    where r.RobotKey == robotKey && r.IsConnected
                    select r).ToList<Robot>();
        }
    }
}