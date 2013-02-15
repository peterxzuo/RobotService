using System.Linq;
using Microsoft.AspNet.SignalR;
using SignalR.Models;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Microsoft.AspNet.SignalR.Hubs;

[HubName("robotSignalHub")]
public class RobotSignalHub : Hub
{
    static RobotDbContext robotDB;
    static RobotSignalHub()
    {
        robotDB = new RobotDbContext();
    }

    public override System.Threading.Tasks.Task OnDisconnected()
    {
        robotDB.OnRobotDisconnected(Context.ConnectionId);
        return base.OnDisconnected();
    }

    public override System.Threading.Tasks.Task OnConnected()
    {
        robotDB.OnRobotconnected(Context.ConnectionId);
        return base.OnConnected();
    }

    public override System.Threading.Tasks.Task OnReconnected()
    {
        robotDB.OnRobotconnected(Context.ConnectionId);
        return base.OnReconnected();
    }

    public void Register(string robotKey)
    {
        robotDB.OnRobotRegistered(Context.ConnectionId, robotKey);
    }

    /// <summary>
    /// Let the robot walk
    /// </summary>
    /// <param name="robotId"></param>
    /// <param name="degree"></param>
    /// <param name="speed"></param>
    /// <param name="distance"></param>
    public static void Walk(string robotKey, RobotControlSignal signal, IHubContext context)
    {
        string signalMessage = JsonConvert.SerializeObject(signal);
        List<Robot> robots = robotDB.GetConnectedRobots(robotKey);
        foreach (Robot robot in robots)
        {
            string connectionId = robot.ConnectionId;
            var client = context.Clients.Client(connectionId);
            if (client != null)
            {
                client.send(signalMessage);
            }
        }
    }
}