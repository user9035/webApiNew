using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Utility.Rss.Core
{
    static class ParserHelper
    {
        internal static Tuple<string, string> GetTitleDescription(string message)
        {
            if (string.IsNullOrEmpty(message))
                throw new ArgumentNullException();
            var splitters = new[] { '\n', '.', '?', '!' };
            string[] result = message.Split(splitters, 2);
            var title = result[0];
            var description = result.Length > 1 ? result[1] : string.Empty;
            return new Tuple<string, string>(title, description);
        }
    }
}
