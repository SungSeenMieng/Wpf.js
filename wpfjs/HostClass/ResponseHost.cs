using Microsoft.ClearScript;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace wpfjs.HostClass
{
    public sealed class ResponseHost
    {
        private string _html;
        public Task<string> text() => Task.FromResult(_html);
        public Task<object> json() => ScriptEngine.Current.Script.JSON.parse(_html);
        public static async Task<ResponseHost> fetch(string url, IScriptObject obj = null)
        {

            if (obj != null)
            {
                using var client = new HttpClient();
                return new ResponseHost { _html = await client.GetStringAsync(url) };
            }
            throw new NotImplementedException();
        }
    }
}
