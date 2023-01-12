using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonProject.Result
{
    public class Response<T>
    {
        public T Data { get; set; }
        public bool HasFailed { get; set; }
        public List<string> Errors { get; set; } = new List<string>();

        public void SetData(T data)
        {
            Data = data;
            HasFailed = false;
        }

        public void Fail()
        {
            HasFailed = true;
        }

        public void Fail(string message)
        {
            Errors.Add(message);
            Fail();
        }
    }

}
