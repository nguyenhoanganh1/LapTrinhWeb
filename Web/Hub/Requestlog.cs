using Microsoft.AspNet.SignalR;
using Model.EL;
using Web.Models;

namespace Web
{
    public class Requestlog : Hub
    {
        public void JoinGroup(ChatGroupModel model)
        {
            Groups.Add(Context.ConnectionId, model.GroupName);
            Clients.Group(model.GroupName).messageGroup(model.UserName, model.Message, model.Time);
        }

        public void LeaveGroup(ChatGroupModel model)
        {
            Groups.Remove(Context.ConnectionId, model.GroupName);
            Clients.Group(model.GroupName).messageGroup(model.UserName, model.Message, model.Time);
        }
    }
}