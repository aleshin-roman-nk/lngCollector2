using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class RepoOperationResult<TData>
    {
        public TData data { get; }
        public string? message { get; }
        public bool success { get; }

        public RepoOperationResult( TData d, string msg, bool sc )
        {
            data = d;
            message = msg;
            success = sc;
        }
    }

    public class RepoOperationResult
    {
        public string? message { get; }
        public bool success { get; }

        public RepoOperationResult(string msg, bool sc)
        {
            message = msg;
            success = sc;
        }
    }
}
