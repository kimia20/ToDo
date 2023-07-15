using MediatR;
using RsjFramework.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo.Domain.Common
{
    public static class ResultExtensions
    {
        public static  Result CombineResults(this Result result, params Result[] results)
        {
            var list = new List<string>();
            foreach (Result item in results)
            {
                if (item.IsFailure)
                {
                    list.Add(item.Error);
                }
            }
            if (list.Any())
            { return Result.Fail(string.Join(",", list.ToArray())); }

            return Result.Ok();

        }

    }
}
