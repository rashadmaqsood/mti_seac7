using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DLMS;
using TCP_Communication;
using ucCustomControl;
using comm;
using System.Windows.Forms;
using SmartEyeControl_7.Controllers;
using System.ComponentModel;
using GUI;
using System.Xml.Serialization;
using DLMS.Comm;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace comm
{
    public class Param_CTPT_ratio_Comparer : EqualityComparer<Param_CTPT_ratio>
    {
        public override bool Equals(Param_CTPT_ratio x, Param_CTPT_ratio y)
        {
            bool isMatch = false;
            try
            {
                if (x == null || y == null)
                    throw new ArgumentNullException("Param_CTPT_ratio Param_ObjA");

                //Compare CTratio_Numerator
                isMatch = x.CTratio_Numerator == y.CTratio_Numerator;
                if (!isMatch)
                    return isMatch;
                //Compare CTratio_Denominator
                isMatch = x.CTratio_Denominator == y.CTratio_Denominator;
                if (!isMatch)
                    return isMatch;
                //Compare CTratio_Numerator
                isMatch = x.PTratio_Numerator == y.PTratio_Numerator;
                if (!isMatch)
                    return isMatch;
                //Compare PTratio_Denominator
                isMatch = x.PTratio_Denominator == y.PTratio_Denominator;

                return isMatch;
            }
            catch (ArgumentNullException)
            {
                throw;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public override int GetHashCode(Param_CTPT_ratio obj)
        {
            return obj.GetHashCode();
        }
    }
}