using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Text.Json;
using System.IO;
using Newtonsoft.Json;

namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    public abstract class DalObject<T> where T : DalObject<T>
    {

        public T FromJson(string path)
        {
            T returnObject = JsonConvert.DeserializeObject<T>(File.ReadAllText(path));
            return returnObject;
        }
        public TT FromJson<TT>(string path)
        {
            TT returnObject = JsonConvert.DeserializeObject<TT>(File.ReadAllText(path));
            return returnObject;
        }
        public void ToJson(T t, string path)
        {
            var serObject = JsonConvert.SerializeObject(t);
            File.WriteAllText(path, serObject);
        }
        public abstract void Save();
    }
}
