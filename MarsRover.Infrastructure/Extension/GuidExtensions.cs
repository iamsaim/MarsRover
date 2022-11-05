using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRover.Infrastructure.Extension
{
    public static class GuidExtensions
    {
        public static Guid ToGuid(this string text)
        {
            if (string.IsNullOrEmpty(text)) throw new ArgumentNullException(nameof(text));
            if (!Guid.TryParse(text, out var result)) throw new Exception("Not a valid guid.");
            return result;
        }

    }
}
