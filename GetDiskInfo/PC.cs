using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
                DiskInfo.Add(wmi["InterfaceType"].ToString());
            }
            if (DiskInfo[1].ToString() != "USB")
            {
                Model = DiskInfo[0].ToString(); //Disk model
                Type = DiskInfo[1].ToString(); //Disk Type

            }
            else
            {
                Model = DiskInfo[3].ToString();
                Type = DiskInfo[4].ToString();

            }

            ManagementObjectSearcher mos2 = new ManagementObjectSearcher("select * from win32_physicalmedia");

            foreach (ManagementObject item in mos2.Get())
            {
                if (item["SerialNumber"] != null)
                {
                    DiskInfo.Add(item["SerialNumber"].ToString().Trim(' ')); 
                }
            }

            if (DiskInfo[1].ToString() != "USB")
            {
                SerialDisk = DiskInfo[2].ToString();
            }
            else
            {
                SerialDisk = DiskInfo[5].ToString();
            }

            ManagementObjectSearcher mos3 = new ManagementObjectSearcher("select * from Win32_bios");
            foreach (ManagementObject sn in mos3.Get())
            {
                ServiceTag = sn["SerialNumber"].ToString();
            }
        }
    }
}
