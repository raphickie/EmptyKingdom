using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EKSiteParser
{
    interface IDetailsPageParser
    {
        Task<IDetailsPageItem[]> ParseDetailsAsync();
    }
}
