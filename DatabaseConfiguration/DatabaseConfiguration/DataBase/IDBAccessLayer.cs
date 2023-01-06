using DatabaseConfiguration.DataSet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DatabaseConfiguration.DataBase
{
    public interface IDBAccessLayer
    {
        void Load_All_Configurations(Configs AllDataSet);
        void Update_All_Configuration(Configs AllDataSet);
    }
}
