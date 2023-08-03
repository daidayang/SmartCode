/*
 * Copyright © 2005-2007 Danilo Mendez <danilo.mendez@kontac.net>
 * forum: http://www.kontac.net/forum.aspx
 * www.kontac.net 
 * All rights reserved.
 * Released under both BSD license and Lesser GPL library license.
 * Whenever there is any discrepancy between the two licenses,
 * the BSD license will take precedence.
 */

using System;
using System.Collections.Generic;
using System.Text;
using Common;
using Businesslayer;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            RegionBiz biz = new RegionBiz();
            RegionDS regionDS = biz.Populate(1);

            RegionDS.RegionRow row = regionDS.Region[0];
            row.RegionDescription = "Test Update";
            biz.UpdateRegion(regionDS); //biz.Persist(regionDS);

            RegionDS.RegionRow newRow = regionDS.Region.NewRegionRow();
            newRow.RegionID = 5;
            newRow.RegionDescription = "New Region";

            regionDS.Region.AddRegionRow(newRow);

            biz.InsertRegion(regionDS); //biz.Persist(regionDS);



        }
    }
}
