using System;
using System.Collections.Generic;
using System.Text;

namespace IncidentTool.Constants
{
    public class ApiConstants
    {
        public const string BaseApiUrl = "http://192.168.137.1:8080";
        public const string OccurredIncidentsEndpoint = "/occurredincident/all";
        public const string AddOccurredIncidentEndpoint = "/occurredincident/add";

        public static string UserByNameEndpoint(string name)
        {
            return "/user/name/" + name;
        }

        public static string OccurredIncidentsByUserIdEndpoint(int id)
        {
            return "/occurredincident/all/user/" + id;
        }

        public static string DeviceByIdEndpoint(int id)
        {
            return "/device/" + id;
        }

        public static string IncidentsByDeviceTypeId(int id)
        {
            return "/incident/devicetypeid/" + id;
        }
    }
}
