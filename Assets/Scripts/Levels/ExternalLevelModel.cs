using System;
using System.Runtime.Serialization;

namespace Levels
{
    [Serializable] [DataContract]
    public class ExternalLevelModel
    {
        [DataMember] 
        public int Id { get; set; } = 1;

        [DataMember] 
        public string ImageUrl { get; private set; } = "Test";
        
        [DataMember]
        public string ImageName { get; private set; } = "Test";

        [DataMember] 
        public int MaxCounterProgress { get; private set; } = 0;
    }
}