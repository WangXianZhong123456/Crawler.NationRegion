using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yaxon.NationRegion.DAL;
using System.Windows.Forms;

namespace Yaxon.NationRegion.BLL
{
    class ReadTownHtmlBLL
    {
        public string GetTownHtmlContent(int sleepTime, int index)
        {
            ReadTownHtmlDal dal = new ReadTownHtmlDal();
            return dal.GetTownHtmlContent(sleepTime, index); ;
        }


    }
}
