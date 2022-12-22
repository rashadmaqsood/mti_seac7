using System;
using System.Xml.Serialization;

namespace SharedCode.Comm.HelperClasses
{

    [XmlInclude(typeof(MeterUser))]
    public class MeterUser
    {
        private string _userName;
        private int _user_id;
        UserType _uType;

        [XmlElement(ElementName = "UserName", Type = typeof(string))]
        public string UserName
        {
            get { return _userName; }
            set { _userName = value; }
        }

        [XmlElement(ElementName = "UId", Type = typeof(int))]
        public int UId
        {
            get { return _user_id; }
            set { _user_id = value; }
        }

        [XmlElement(ElementName = "UType", Type = typeof(UserType))]
        public UserType UType
        {
            get { return _uType; }
            set { _uType = value; }
        }
        public MeterUser()
        {
            _userName = "Admin";
            _user_id = 1;
        }

        public MeterUser(string userName, int uId)
        {
            _userName = userName;
            _user_id = uId;
        }

        public MeterUser(MeterUser otherObj)
        {
            UserName = otherObj.UserName;
            UId = otherObj.UId;
        }

        public override string ToString()
        {
            string retVal = string.Empty;
            if (String.IsNullOrEmpty(retVal))
            {
                return retVal = string.Format("{0}_{1}", UId, UserName);
            }
            return retVal;
        }
    }
    public enum UserType : byte
    {
        SuperAdmin = 1,
        Admin,
        Inspector,
        Reader
    }
}
