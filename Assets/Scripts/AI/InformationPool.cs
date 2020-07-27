using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TrpgAI
{
    public class InformationPool
    {
        private static InformationPool instance = new InformationPool();
        private InformationPool() { }
        public static InformationPool GetInstance()
        {
            return instance;
        }
        public void AddInformation(Information information)
        {
            //todo
        }
    }

    public abstract class Information{
        //todo
    }

}

