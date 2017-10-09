using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace HendoHealth.Library.Entity
{
    public class DownloadWeightDataEntity
    {
        /// <summary>
        /// BMI grade
        /// </summary>
        [DataMember]
        public double BMI { get; set; }
        /// <summary>
        /// Bone mineral density 
        /// </summary>
        [DataMember]
        public double BoneValue { get; set; }
        /// <summary>
        /// DCI
        /// </summary>
        [DataMember]
        public double DCI { get; set; }
        /// <summary>
        /// Body fat 
        /// </summary>
        [DataMember]
        public double FatValue { get; set; }
        /// <summary>
        /// Muscle
        /// </summary>
        [DataMember]
        public double MuscaleValue { get; set; }
        /// <summary>
        /// Water
        /// </summary>
        [DataMember]
        public double WaterValue { get; set; }
        /// <summary>
        /// Weight
        /// </summary>
        [DataMember]
        public double WeightValue { get; set; }
        /// <summary>
        /// Data only Serial number
        /// </summary>
        [DataMember]
        public string DataID { get; set; }
        /// <summary>
        /// Data measurement of time
        /// </summary>
        [DataMember]
        public long MDate { get; set; }
        /// <summary>
        /// Note
        /// </summary>
        [DataMember]
        public string Note { get; set; }
        /// <summary>
        /// Last change time
        /// </summary>
        [DataMember]
        public long LastChangeTime { get; set; }
        /// <summary>
        /// Data Source the value is "Manual" or "FromDevice"
        /// </summary>
        [DataMember]
        public string DataSource { get; set; }
        /// <summary>
        /// UserId
        /// </summary>
        [DataMember]
        public string userid { get; set; }
        /// <summary>
        /// Time zone of measurement location
        /// </summary>
        [DataMember]
        public string TimeZone { get; set; }
    }
}