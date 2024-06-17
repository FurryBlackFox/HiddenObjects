using System;
using System.Runtime.Serialization;
using Services;

namespace Levels
{
    [Serializable] [DataContract]
    public class RuntimeLevelModel
    {
        [DataMember]
        public int Id { get; set; }
        
        [DataMember]
        public int CurrentProgress { get; set; }
        
        private RuntimeLevelModel(){}

        public RuntimeLevelModel(int id)
        {
            Id = id;
        }
    }
}