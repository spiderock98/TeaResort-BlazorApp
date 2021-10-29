using System;
using System.Collections.Generic;

namespace SmartRetail.Share.Models
{
    public class CloudUser : BaseNotifyModel
    {
        #region
        string _Password;
        UserInfor _Infor;
        bool _IsActive = true;
        List<Role> _Roles;
        List<int> _RoomList;
        List<string> _SourceList;
        List<int> _UserDeviceList;
        List<int> _ScenesList;
        List<int> _ScheduleList;
        List<int> _LocationList;
        List<string> _DeviceRoleList;
        string _User;
        #endregion

        public CloudUser()
        { }

        public string User
        {
            get { return _User; }
            set { SetProperty(ref _User, value); }
        }
        public string Password
        {
            get { return _Password; }
            set { SetProperty(ref _Password, value); }
        }
        public bool IsActive
        {
            get { return _IsActive; }
            set { SetProperty(ref _IsActive, value); }
        }
        public UserInfor Infor
        {
            get { return _Infor; }
            set { SetProperty(ref _Infor, value); }
        }
        public List<Role> Roles
        {
            get { return _Roles; }
            set { SetProperty(ref _Roles, value); }
        }

        public List<string> DataSourceList
        {
            get { return _SourceList; }
            set { SetProperty(ref _SourceList, value); }
        }
        public List<int> UserDeviceList
        {
            get { return _UserDeviceList; }
            set { SetProperty(ref _UserDeviceList, value); }
        }
        public List<int> ScenesList
        {
            get { return _ScenesList; }
            set { SetProperty(ref _ScenesList, value); }
        }
        public List<int> ScheduleList
        {
            get { return _ScheduleList; }
            set { SetProperty(ref _ScheduleList, value); }
        }
        public List<int> LocationList
        {
            get { return _LocationList; }
            set { SetProperty(ref _LocationList, value); }
        }
        public List<int> RoomList
        {
            get { return _RoomList; }
            set { SetProperty(ref _RoomList, value); }
        }

        public List<string> DeviceRoleList
        {
            get { return _DeviceRoleList; }
            set { SetProperty(ref _DeviceRoleList, value); }
        }

    }

    public class UserInfor : BaseNotifyModel
    {
        string _UserName;
        string _Email;
        string _Location;
        string _Company;
        string _PhoneNumber;

        public string UserName
        {
            get { return _UserName; }
            set { SetProperty(ref _UserName, value); }
        }
        public string Email
        {
            get { return _Email; }
            set { SetProperty(ref _Email, value); }
        }
        public string Location
        {
            get { return _Location; }
            set { SetProperty(ref _Location, value); }
        }
        public string Company
        {
            get { return _Company; }
            set { SetProperty(ref _Company, value); }
        }
        public string PhoneNumber
        {
            get { return _PhoneNumber; }
            set { SetProperty(ref _PhoneNumber, value); }
        }
    }
    public enum Role { System, Admin, Install, View, Excute, Edit, Member }
    // public class CurrentRole
    // {
    //     public string Result { get; set; }
    // }
}
