using System;
using System.Collections.Generic;

namespace SmartRetail.Client.Models
{
    public class CloudUser 
    {
        public CloudUser()
        { }

        public string User { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public UserInfor Infor { get; set; }
        public List<Role> Roles { get; set; }

        public List<string> DataSourceList { get; set; }
        public List<int> UserDeviceList { get; set; }
        public List<int> ScenesList { get; set; }
        public List<int> ScheduleList { get; set; }
        public List<int> LocationList { get; set; }
        public List<int> RoomList { get; set; }
        public List<string> DeviceRoleList { get; set; }
    }

    public class UserInfor
    {


        public string UserName { get; set; }
        public string Email { get; set; }
        public string Location { get; set; }
        public string Company { get; set; }
        public string PhoneNumber { get; set; }
    }
    public enum Role { System, Admin, Install, View, Excute, Edit, Member }
}
