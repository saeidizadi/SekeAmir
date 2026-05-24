
using Microsoft.AspNetCore.Http.Extensions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Persistence.Extention
{
    //using Microsoft.AspNetCore.Http.Extensions; nasb shavad
    public static  class SessionExtensions
    {
        public static T GetData<T>(this ISession session, string key)
        {
            var data = session.GetString(key);
            if (data == null)
            {
                return default;
            }
            return JsonConvert.DeserializeObject<T>(data);
        }

        public static void SetData(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }
    }
}
