using Microsoft.AspNetCore.Components.Web.Virtualization;
using Newtonsoft.Json;
using SmartRetail.Client.Helper;
using SmartRetail.Client.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartRetail.Client.Models
{
    public enum ChartType { Day, Month, Year }

    //internal class DevEnergyData
    //{
    //    public int DevId { get; set; }
    //    public double IntervalEnergy { get; set; }
    //}
    public class DataChartModel
    {
        const string DEFAULT_CAPATILITIES_1P = "SchneiderPowerTag_1P";
        const string DEFAULT_CAPATILITIES_3P = "SchneiderPowerTag_3P";

        public DataChartModel() { }
        public ChartType ChartT { get; set; }
        public List<DeviceModel> StackDevices { get; set; } = new List<DeviceModel>();
        public DateTime Time { get; set; }
        public double AccumulatedTotalEnergy { get; set; } 
        public Dictionary<int, double> DeviceEnergyData { get; set; } = new Dictionary<int, double>();

        private List<DataLogModel> DataLogs { get; set; } = new List<DataLogModel>();

        public bool isDevicesScheider 
        {
            get
            {
                var isAnyDevInvalid = StackDevices.Any(r => r.Capabilitie is not DEFAULT_CAPATILITIES_1P or not DEFAULT_CAPATILITIES_3P);
                return !isAnyDevInvalid;
            }
        }

        //async List<DataLogModel> GenLogByTime( string token)
        async IAsyncEnumerable<List<DataLogModel>> GenLogSplitMonth(string token)
        {
            DataLogService svLog = new DataLogService();
            
            // GET data month by month using Task
            if (ChartT == ChartType.Year)
            {
                var arrResultMonth = new List<Tuple<int, int, Task<List<DataLogModel>>, List<DataLogModel>>>();

                for(var idxMonth=1; idxMonth <= 12; idxMonth++)
                {
                    // Description: Tuple Struct (startUnix, endUnix, taskHandle, list data log result)
                    var _item1 = UnixTime.LocalTimeToUnixSecond(new DateTime(Time.Year, idxMonth, 1, 0, 0, 0));
                    var _item2 = UnixTime.LocalTimeToUnixSecond(new DateTime(Time.Year, idxMonth, 1, 23, 59, 59).AddMonths(1).AddDays(-1));
                    var _item3 = svLog.GetDataDevicesAsync(token, StackDevices.Select(r=>r.Id).ToList(), _item1, _item2);

                    arrResultMonth.Add(new Tuple<int, int, Task<List<DataLogModel>>, List<DataLogModel>>(_item1, _item2, _item3, new List<DataLogModel>()));
                }
                await Task.WhenAll(arrResultMonth.Select(r => r.Item3));

                foreach(var item in arrResultMonth)
                {
                    yield return await item.Item3;
                }
            }
        }

        public async Task<List<DataChartModel>> GetEnergyByYear(string token)
        {
            //var result = Enumerable.Repeat(new DataChartModel(), 12).ToList();
            var result = new List<DataChartModel>();

            // validate device attribute
            if (isDevicesScheider == false) return null;

            this.AccumulatedTotalEnergy = 0;

            await foreach(var logMonth in GenLogSplitMonth(token))
            {
                var chartItem = new DataChartModel()
                {
                    ChartT = this.ChartT,
                    Time = this.Time,
                    StackDevices = this.StackDevices,
                    DataLogs = this.DataLogs
                };

                var grDeviceLog = logMonth.GroupBy(r => r.DeviceId).ToList();
                
                foreach (var grDev in grDeviceLog)
                {
                    var monthlyDevEnergyData = grDev.Where(r => r.Attribute.Equals("energyPActive")).OrderByDescending(r => Convert.ToDouble(r.Value)).ToList();
                    var monthlyDevEnergyValue = Convert.ToDouble(monthlyDevEnergyData.Last().Value) - Convert.ToDouble(monthlyDevEnergyData.First().Value);

                    chartItem.DeviceEnergyData.Add(grDev.Key, monthlyDevEnergyValue);

                    this.AccumulatedTotalEnergy += monthlyDevEnergyValue;
                }
                chartItem.AccumulatedTotalEnergy = this.AccumulatedTotalEnergy;

                result.Add(chartItem);
            }

            return result;
        }
    }
}
