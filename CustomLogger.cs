using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MelonLoader;

namespace Ritterschlag
{
    public static class CustomLogger
    {
        private static readonly Dictionary<string, List<DateTime>> Log = new();

        public static void Msg(string msg)
        {
            if (!Log.ContainsKey(msg))
            {
                Log[msg] = new List<DateTime>();
            }

            Log[msg].Add(DateTime.Now);
        }

        public static void PrintFull(int topN = 5)
        {
            MelonLogger.Msg("--- CUSTOM LOGGER ----");
            StringBuilder sb = new();
            foreach ((string msg, List<DateTime> dateTimes) in Log)
            {
                sb.Append($"{dateTimes.Count} \"{msg}\":{Environment.NewLine}");
                sb.Append(string.Join(Environment.NewLine,
                    dateTimes.Skip(Math.Max(0, dateTimes.Count - topN)).Select(time => $"  {time:O}")));
            }

            MelonLogger.Msg(sb.ToString());
            MelonLogger.Msg("----------------------");
        }
    }
}