﻿using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace WoWDeveloperAssistant
{
    public static class LineGetters
    {
        public static string GetGuidFromLine(string line, bool objectFieldGuid = false, bool unitGuid = false, bool senderGuid = false, bool moverGuid = false, bool attackerGuid = false, bool casterGuid = false)
        {
            if (objectFieldGuid)
            {
                Regex guidRegex = new Regex(@"OBJECT_FIELD_GUID: Full:{1}\s*\w{20,}");
                if (guidRegex.IsMatch(line))
                    return guidRegex.Match(line).ToString().Replace("OBJECT_FIELD_GUID: Full: ", "");
            }
            else if (unitGuid)
            {
                Regex guidRegex = new Regex(@"UnitGUID: Full:{1}\s*\w{20,}");
                if (guidRegex.IsMatch(line))
                    return guidRegex.Match(line).ToString().Replace("UnitGUID: Full: ", "");
            }
            else if (senderGuid)
            {
                Regex guidRegex = new Regex(@"SenderGUID: Full:{1}\s*\w{20,}");
                if (guidRegex.IsMatch(line))
                    return guidRegex.Match(line).ToString().Replace("SenderGUID: Full: ", "");
            }
            else if (attackerGuid)
            {
                Regex guidRegex = new Regex(@"Attacker Guid: Full:{1}\s*\w{20,}");
                if (guidRegex.IsMatch(line))
                    return guidRegex.Match(line).ToString().Replace("Attacker Guid: Full: ", "");
            }
            else if (casterGuid)
            {
                Regex guidRegex = new Regex(@"CasterGUID: Full:{1}\s*\w{20,}");
                if (guidRegex.IsMatch(line))
                    return guidRegex.Match(line).ToString().Replace("CasterGUID: Full: ", "");
            }
            else
            {
                Regex guidRegex = new Regex(@"Object Guid: Full:{1}\s*\w{20,}");
                if (guidRegex.IsMatch(line))
                    return guidRegex.Match(line).ToString().Replace("Object Guid: Full: ", "");
            }

            return "";
        }

        public static TimeSpan GetTimeSpanFromLine(string dateLine)
        {
            string[] time;

            Regex timeRegex = new Regex(@"\d+:+\d+:+\d+");
            if (timeRegex.IsMatch(dateLine))
            {
                time = timeRegex.Match(dateLine).ToString().Split(':');
            }
            else
                return new TimeSpan();

            return new TimeSpan(Convert.ToInt32(time[0]), Convert.ToInt32(time[1]), Convert.ToInt32(time[2]));
        }

        public static bool IsCreatureLine(string updateTypeLine)
        {
            if (updateTypeLine.Contains("Object Guid: Full:") &&
                (updateTypeLine.Contains("Creature/0") || updateTypeLine.Contains("Vehicle/0")))
                return true;

            if (updateTypeLine.Contains("SenderGUID: Full:") &&
                (updateTypeLine.Contains("Creature/0") || updateTypeLine.Contains("Vehicle/0")))
                return true;

            if (updateTypeLine.Contains("MoverGUID: Full:") &&
                (updateTypeLine.Contains("Creature/0") || updateTypeLine.Contains("Vehicle/0")))
                return true;

            if (updateTypeLine.Contains("Attacker Guid: Full:") &&
                (updateTypeLine.Contains("Creature/0") || updateTypeLine.Contains("Vehicle/0")))
                return true;

            return false;
        }

        public static string GetPacketTimeFromStringInSeconds(string line)
        {
            Regex timeRegex = timeRegex = new Regex(@"\d+:+\d+:+\d+");

            if (timeRegex.IsMatch(line))
            {
                Packets.TimePacket packet;
                string[] splittedLine = timeRegex.Match(line).ToString().Split(':');

                packet.hours = splittedLine[0];
                packet.minutes = splittedLine[1];
                packet.seconds = splittedLine[2];

                return ((Convert.ToUInt64(packet.hours) * 3600) + (Convert.ToUInt64(packet.minutes) * 60) + Convert.ToUInt64(packet.seconds)).ToString();
            }

            return "";
        }
    }
}
