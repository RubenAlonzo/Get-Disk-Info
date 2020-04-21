using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;
using System.IO;
using System.Collections;

namespace GetDiskInfo
{
    public class PC
    {
        public string Model { get; set; }
        public string Type { get; set; }
        public string SerialDisk { get; set; }
        public string ServiceTag { get; set; }

        ArrayList DiskInfo = new ArrayList();

        public PC()
        {
            ManagementObjectSearcher mos = new ManagementObjectSearcher("SELECT * FROM Win32_DiskDrive");

            foreach (ManagementObject wmi in mos.Get())
            {
                DiskInfo.Add(wmi["Model"].ToString());
                DiskInfo.Add( wmi["InterfaceType"].ToString());
                DiskInfo.Add(wmi["SerialNumber"].ToString().Trim(' '));
            }

            if (DiskInfo[1].ToString() != "USB")
            {
                Model = DiskInfo[0].ToString(); //Disk model
                Type = DiskInfo[1].ToString(); //Disk Type
                SerialDisk = DiskInfo[2].ToString(); //Disk Serial
            }
            else
            {
                Model = DiskInfo[3].ToString();
                Type = DiskInfo[4].ToString();
                SerialDisk = DiskInfo[5].ToString();
            }

            ManagementObjectSearcher mos2 = new ManagementObjectSearcher("select * from Win32_BIOS");

            foreach (ManagementObject sn in mos2.Get())
            {
                ServiceTag = sn["SerialNumber"].ToString();
            }
        }
    }
}
