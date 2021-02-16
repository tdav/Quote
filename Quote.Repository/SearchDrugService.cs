using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quote.Repository
{

    public interface ISearchDrugService
    {
        public Task<List<viDrugForKassa>> GetListAsync(string s);
    }


    public class SearchDrugService : ISearchDrugService
    {
        private readonly string Sql = @" SELECT
                                          d.id,
                                          d.description,
                                          d.barcode,
                                          t.price,
                                          ROUND((t.price / d.piece), 2) AS priceforsht,
                                          t.curqty,
                                          d.piece,
                                          t.seriesno,
                                          t.expirydate,
                                          t.bookingcustomername,
                                          t.bookingtimeto,
                                          t.createdate,
                                          t.id AS comingproductid,
                                          m.name || ' (' || cu.name || ')' AS manufacturername,
                                          u.name AS unit,
                                          u.impartible,
                                          d.manufacturerid,
                                          d.unitid,
                                          t.incomingprice,
                                          t.extracharge,
                                          d.photo,
                                          c.name AS drugcategoryname,
                                          t.dpinfo,
                                          d.drugrecomendation,
                                          t.bonusforsellers,
                                          t.vatrate,
                                          cu.name AS cupboard,
                                          s.name AS drugstores
                                        FROM tbdocitems t
                                          INNER JOIN spdrugs d ON t.drugid = d.id
                                          INNER JOIN spunits u ON d.unitid = u.id
                                          INNER JOIN spmanufacturers m ON t.manufacturerid = m.id
                                          INNER JOIN spcountries cu ON m.countryid = cu.id
                                          INNER JOIN spdrugstores s ON t.drugstoreid = s.id
                                          LEFT OUTER JOIN spdrugcategorys c ON d.drugcategoryid = c.id
                                          LEFT OUTER JOIN spcupboards ON d.cupboardid = spcupboards.id
                                        WHERE t.operationtypeid IN (1, 3)
                                        AND t.status = 1
                                        AND t.curqty > 0
                                        AND Description LIKE {0}
                                        ORDER BY description, expirydate LIMIT 50";


        private readonly IUnitOfWork db;

        public SearchDrugService(IUnitOfWork _db) => db = _db;


        public async Task<List<viDrugForKassa>> GetListAsync(string s)
        {
            s = s.Replace('*', '%');
            var pa = "";
            char[] splitters = { ' ' };
            var sl = s.Split(splitters, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < sl.Length; i++) pa += $"%{sl[i]}%";


            string sql = string.Format(Sql, $" '{pa.ToUpper()}' ");

            var ls = await db.FromSqlDapperAsync<viDrugForKassa>(sql);
            var list = ls.ToList();
            list.MoveToFront(x => x.Description.StartsWith(s));
            return list;
        }
    }
}
