using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EKSiteParser
{
    interface IDetailsPageItem
    {
        string[] ImageUrls { get; set; }
        string[] Tags { get; set; }
    }
}
