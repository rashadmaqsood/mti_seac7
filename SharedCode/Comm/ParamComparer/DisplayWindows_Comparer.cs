using comm.DataContainer;
using SharedCode.Comm.DataContainer;
using System;
using System.Collections.Generic;

namespace comm
{
    public class DisplayWindows_Comparer : EqualityComparer<DisplayWindows>
    {
        public override bool Equals(DisplayWindows x, DisplayWindows y)
        {
            bool isMatch = false;
            try
            {
                if (x == null || y == null)
                    throw new ArgumentNullException("DisplayWindows ObjA");
                if (x.Windows == null || y.Windows == null)
                    throw new ArgumentNullException("DisplayWindows_Windows ObjA");
                //Reference Match
                if (x == y)
                {
                    isMatch = true;
                    return isMatch;
                }

                //Compare Window_Count
                isMatch = x.Windows.Count == y.Windows.Count;
                if (!isMatch)
                    return isMatch;

                //Compare ScrollTime
                isMatch = TimeSpan.Equals(x.ScrollTime, y.ScrollTime);
                if (!isMatch)
                    return isMatch;

                //Compare DispalyWindowsModes
                isMatch = x.WindowsMode == y.WindowsMode;
                if (!isMatch)
                    return isMatch;

                //Compare WindowsItems
                DisplayWindowItem_Comparer WinItem_Comparer = new DisplayWindowItem_Comparer();
                for (int winIndex = 0; winIndex < x.Windows.Count; winIndex++)
                {
                    //isMatch = WinItem_Comparer.Equals(x.Windows[winIndex], y.Windows[winIndex]);

                    isMatch = x.Windows[winIndex].Obis_Index == y.Windows[winIndex].Obis_Index;
                    if (!isMatch)
                        break;

                    isMatch = x.Windows[winIndex].WindowNumberToDisplay.Equals(y.Windows[winIndex].WindowNumberToDisplay);
                    if (!isMatch)
                        break;
                }
                return isMatch;
            }
            catch (ArgumentNullException)
            {
                throw;
            }
            catch (Exception)
            {
                isMatch = false;
            }
            return isMatch;
        }

        public override int GetHashCode(DisplayWindows obj)
        {
            return obj.GetHashCode();
        }
    }

    public class DisplayWindowItem_Comparer : EqualityComparer<DisplayWindowItem>
    {
        public override bool Equals(DisplayWindowItem x, DisplayWindowItem y)
        {
            bool isMatch = false;
            try
            {
                if (x == null || y == null)
                    throw new ArgumentNullException("DisplayWindowItem ObjA");
                //Reference Match
                if (x == y)
                {
                    isMatch = true;
                    return isMatch;
                }
                //Compare Window Item Name
                isMatch = String.Equals(x.Window_Name, y.Window_Name);
                if (!isMatch)
                    return isMatch;
                //Compare Obis_Index
                isMatch = x.Obis_Index == y.Obis_Index;
                if (!isMatch)
                    return isMatch;
                //Compare WindowNumberToDisplay
                isMatch = x.WindowNumberToDisplay.Value == y.WindowNumberToDisplay.Value;
                if (!isMatch)
                    return isMatch;
                //Compare AttributeSelected
                isMatch = x.AttributeSelected == y.AttributeSelected;
                if (!isMatch)
                    return isMatch;
            }
            catch (ArgumentNullException)
            {
                throw;
            }
            catch (Exception)
            {
                isMatch = false;
            }
            return isMatch;
        }

        public override int GetHashCode(DisplayWindowItem obj)
        {
            return obj.GetHashCode();
        }
    }
}
